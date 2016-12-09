using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.IO;
using RDPDiscovery.Amib.Threading;

namespace RemoteDesktopManager
{
   public partial class DiscoveryForm : Form
   {
      private Boolean mbMustStop = false;

      private SmartThreadPool moPool;

      private MainForm moForm;
      public DiscoveryForm( MainForm poForm )
      {
         moForm = poForm;
         InitializeComponent();
      }

      # region GetActiveIPAddresses
      private IPInfo[] GetActiveIPAddresses()
      {
         List<IPInfo> lcoIPInfoCollection = new List<IPInfo>();

         foreach(NetworkInterface loInterface in
            NetworkInterface.GetAllNetworkInterfaces())
         {
            if(loInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
               continue;

            if(loInterface.OperationalStatus != OperationalStatus.Up)
               continue;

            if(loInterface.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
               continue;

            //Console.WriteLine( loInterface.Description );

            UnicastIPAddressInformationCollection UnicastIPInfoCol =
               loInterface.GetIPProperties().UnicastAddresses;

            foreach(UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
            {
               if(!UnicatIPInfo.IPv4Mask.Equals(
                  new IPAddress( new byte[] { 0, 0, 0, 0 } ) ))
               {
                  lcoIPInfoCollection.Add( new IPInfo( UnicatIPInfo.Address, UnicatIPInfo.IPv4Mask ) );
               }
            }
         }
         return lcoIPInfoCollection.ToArray();
      }
      # endregion

      private void DiscoveryForm_Load( object sender, EventArgs e )
      {
         foreach(IPInfo loInfo in GetActiveIPAddresses())
         {
            cmbIPAddresses.Items.Add( loInfo );
         }

         if( cmbIPAddresses.Items != null && cmbIPAddresses.Items.Count > 0 )
         {
            cmbIPAddresses.SelectedIndex = 0;
         }

         btnCancel.Location = new Point( 264, 10 );
         btnCancel.BringToFront();
      }

      private bool isEnd( int a, byte b )
      {
         if(((byte)a) == b)
            return true;

         if(b == 255 && ((byte)a) == (b - 1))
            return true;

         return false;
      }

      private void btnDiscover_Click( object sender, EventArgs e )
      {
        // if(moPool == null) // do not cache because of shutdown code in cancel
            moPool = new SmartThreadPool( 500, 8, 4 );

         mbMustStop = false;

         moDiscoveredHosts.Items.Clear();
         
         btnDiscover.Enabled = false;
         btnCancel.Visible = true;

         IPInfo loIP = (IPInfo)cmbIPAddresses.SelectedItem;
         byte[] labAddress = loIP.Address.GetAddressBytes();
         byte[] labMask = loIP.Mask.GetAddressBytes();

         byte[] fromRange = new byte[4];
         byte[] toRange = new byte[4];

         #region Determine start and end IP range
         for( int i = 0; i < labAddress.Length; i++ )
         {
            fromRange[i] = (byte)((byte)labAddress[i] & (byte)labMask[i]);
         }

         for( int i = 0; i < labMask.Length; i++ )
         {
            byte mask = 0;
            mask = (byte)~labMask[i];
            mask = (byte)((byte)mask & (byte)0xff);
            toRange[i] = (byte)((byte)fromRange[i] | (byte)mask);
         }
         #endregion

         for(int i = fromRange[0]; i <= toRange[0]; i++)
         {
            for(int ii = fromRange[1]; ii <= toRange[1]; ii++)
            {
               for(int iii = fromRange[2]; iii <= toRange[2]; iii++)
               {
                  for(int iiii = fromRange[3]; iiii <= toRange[3]; iiii++)
                  {
                     if(iiii == 0)
                        continue;

                     if((i == 255) || (ii == 255) || (iii == 255) || (iiii == 255))
                        continue;
                     
                     PingObject loObj = new PingObject( i, ii, iii, iiii );
                     if(isEnd( i, toRange[0] ) && isEnd( ii, toRange[1] ) && 
                        isEnd( iii, toRange[2] ) && isEnd( iiii, toRange[3] ))
                     {
                        loObj.Last = true;
                     }
                     moPool.QueueWorkItem( new WorkItemCallback( DoThePing ), loObj );
                  }
               }
            }
         }
      }

      # region Ping Threading Code
      private object DoThePing( Object poPingObject )
      {
         PingPort loPinger = new PingPort();

         PingObject loPingObj = (PingObject)poPingObject;

         if(loPinger.Ping( loPingObj.GetIPAddress(), 3389 ) == true)
         {
            Host loHost = new Host( Dns.GetHostEntry( loPingObj.GetIPAddress() ) );

            if(moDiscoveredHosts.InvokeRequired)
            {
               moDiscoveredHosts.Invoke( new AddListItemDelegate( AddItem ),
                  new Object[] { loHost } );
            }
            else
            {
               moDiscoveredHosts.Items.Add( loHost, true );
            }
         }

         if(loPingObj.Last == true || mbMustStop == true )
         {
            if(btnDiscover.InvokeRequired)
            {
               btnDiscover.Invoke( new EnableButtonDelegate( EnableButton ),
                  new Object[] { true } );
            }
            else
            {
               btnDiscover.Enabled = true;
            }

            if( btnCancel.InvokeRequired )
            {
               btnCancel.Invoke( new ButtonVisibleDelegate( ButtonVisible ),
                  new Object[] { false } );
            }
            else
            {
               btnCancel.Visible = false;
            }

            moStatusLabel.Text = "Done.";
         }
         else
         {
            moStatusLabel.Text = loPingObj.GetIPAddress().ToString();
         }
         Application.DoEvents();
         return null;
      }

      delegate void AddListItemDelegate( Host poHost );
      public void AddItem( Host poHost )
      {
          bool lbHostAlreadyInCollection = false;
          foreach(Host loHost in moDiscoveredHosts.Items)
          {
              if(loHost.GetHostEntry().HostName == poHost.GetHostEntry().HostName)
              {
                  lbHostAlreadyInCollection = true;
              }
          }
          if(lbHostAlreadyInCollection == false)
          {
              moDiscoveredHosts.Items.Add( poHost, true );
          }
      }

      delegate void EnableButtonDelegate( Boolean pbEnable );
      public void EnableButton( Boolean pbEnable )
      {
         btnDiscover.Enabled = pbEnable;
      }

      delegate void ButtonVisibleDelegate( Boolean pbVisible );
      public void ButtonVisible( Boolean pbVisible )
      {
         btnCancel.Visible = pbVisible;
      }
      # endregion

      private void moRDPSlider_ValueChanged( object sender, EventArgs e )
      {
         switch(moRDPSlider.Value)
         {
            case 0: lblRDPScreenSize.Text = "640x480"; break;
            case 1: lblRDPScreenSize.Text = "800x600"; break;
            case 2: lblRDPScreenSize.Text = "1024x768"; break;
            case 3: lblRDPScreenSize.Text = "1280x1024"; break;
            case 4: lblRDPScreenSize.Text = "fullscreen"; break;
         }
      }
      
      private void btnSave_Click( object sender, EventArgs e )
      {
         if(moDiscoveredHosts.CheckedItems == null ||
            moDiscoveredHosts.CheckedItems.Count == 0)
         {
            Utility.showMessageBox( moForm, "No hosts selected." );
            return;
         }

         for( int h = 0; h < moDiscoveredHosts.CheckedItems.Count; h++ )
         {
            Host loHost = (Host)moDiscoveredHosts.CheckedItems[h];

            RDPConnection loConn = new RDPConnection( moForm.RDPFileLocation );
            String[] lasHostName = loHost.GetHostEntry().HostName.Split( '.' );

            if(chkRDPStripDomain.Checked == true)
            {
               loConn.Name = lasHostName[0];
            }
            else
            {
               loConn.Name = loHost.GetHostEntry().HostName;
            }

            if(chkRDPUseDomain.Checked == true)
            {
               for( int i = 1; i < lasHostName.Length; i++ )
               {
                  if(i >= (lasHostName.Length-1))
                     loConn.Domain += lasHostName[i];
                  else
                     loConn.Domain += lasHostName[i] + ".";
               }
            }
            else
            {
               loConn.Domain = txtRDPDomain.Text;
            }

            loConn.Hostname = loHost.GetFirstIPAddres().ToString();
            loConn.Group = txtRDPGroupName.Text;
            loConn.Username = txtRDPUsername.Text;
            loConn.UseLocalDrives = chkRDPLocalDrives.Checked;

            switch(moRDPSlider.Value)
            {
               case 0: loConn.ScreenSize = ScreenSizes.SZ_640x480; break;
               case 1: loConn.ScreenSize = ScreenSizes.SZ_800x600; break;
               case 2: loConn.ScreenSize = ScreenSizes.SZ_1024x768; break;
               case 3: loConn.ScreenSize = ScreenSizes.SZ_1280x1024; break;
               case 4: loConn.FullScreen = true; break;
            }
            loConn.WriteRDPConfig();

            // Act as if we dropped the file on RDM
            //
            moForm.ReadRDPFile( loConn.RDPFileName );

         }//end loop

         this.Close();
      }

      private void btnCancel_Click( object sender, EventArgs e )
      {
         try
         {
            Cursor = Cursors.WaitCursor;

            mbMustStop = true;
            moPool.Shutdown( true, 2000 );
            btnCancel.Visible = false;
            moStatusLabel.Text = "Canceled.";
         }
         finally
         {
            Cursor = Cursors.Default;
         }
      }

      private void btnClose_Click( object sender, EventArgs e )
      {
         this.Close();
      }
   }

   # region IPInfo Class
   public class IPInfo
   {
      IPAddress moAddress;
      IPAddress moMask;

      public IPInfo( IPAddress poAddress, IPAddress poMask )
      {
         moAddress = poAddress;
         moMask = poMask;
      }

      public IPAddress Address
      {
         get
         {
            return moAddress;
         }
      }

      public IPAddress Mask
      {
         get
         {
            return moMask;
         }
      }

      public override string ToString()
      {
         return moAddress.ToString() + " / " + moMask.ToString();
      }
   }
   # endregion

   # region PingObject Class
   public class PingObject
   {
      IPAddress moAddress;
      Boolean mbLast = false;

      public PingObject( int pb1, int pb2, int pb3, int pb4 )
      {
         moAddress = new IPAddress( new byte[] { (byte)pb1, (byte)pb2, (byte)pb3, (byte)pb4 } );
      }

      public PingObject( IPAddress poAddress )
      {
         moAddress = poAddress;
      }

      public IPAddress GetIPAddress()
      {
         return moAddress;
      }

      public Boolean Last
      {
         get
         {
            return mbLast;
         }
         set
         {
            mbLast = value;
         }
      }
   }
   # endregion

   # region Host Class
   public class Host
   {
      IPHostEntry moHost;

      public Host( IPHostEntry poHost )
      {
         moHost = poHost;
      }

      public IPHostEntry GetHostEntry()
      {
         return moHost;
      }

      public IPAddress GetFirstIPAddres()
      {
         foreach(IPAddress address in moHost.AddressList)
         {
            if(address.AddressFamily == AddressFamily.InterNetwork)
               return address;
         }
         return null;
      }

      public override String ToString()
      {
         if(moHost == null)
            return "";

         String lsValue = moHost.HostName + " (";
         foreach(IPAddress loAddr in moHost.AddressList)
         {
            lsValue += loAddr.ToString() + ",";
         }
         lsValue = lsValue.Substring( 0, lsValue.Length - 1 ) + ")";

         if( moHost.Aliases.Length > 0 )
         {
            lsValue += " [";
            foreach(String lsAlias in moHost.Aliases)
            {
               lsValue += lsAlias + ",";
            }
            lsValue = lsValue.Substring( 0, lsValue.Length - 1 ) + "]";
         }
         return lsValue;
      }
   }
   # endregion

   # region PingPort Class
   public class PingPort
   {
      private ManualResetEvent moConnectDone = new ManualResetEvent( false );
      private EndPoint[] maoEndpoint = new EndPoint[1];
      Ping moPinger = new Ping();

      public Boolean Ping( IPAddress poAddress, int piPort )
      {
         PingReply loReply = moPinger.Send( 
            poAddress, 25, new byte[0], new PingOptions( 8, false ) );

         if(loReply.Status == IPStatus.Success)
         {
            try
            {
               maoEndpoint[0] = null;

               Socket loClient = new Socket
               (
                  AddressFamily.InterNetwork,
                  SocketType.Stream,
                  ProtocolType.Tcp
               );

               loClient.BeginConnect
               (
                  loReply.Address, 3389,
                  new AsyncCallback( ConnectCallback ), loClient
               );

               moConnectDone.WaitOne( 50, true );

               if(maoEndpoint[0] != null)
               {
                  moConnectDone.Reset();
                  return true;
               }
            }
            catch(Exception se)
            {
               Console.WriteLine( se.Message );
            }
         }
         return false;
      }

      private void ConnectCallback( IAsyncResult ar )
      {
         try
         {
            Socket client = (Socket)ar.AsyncState;
            client.EndConnect( ar );
            maoEndpoint[0] = client.RemoteEndPoint;
            moConnectDone.Set();
         }
         catch(Exception e)
         {
            Console.WriteLine( "Socket error {0}", e.Message );
         }
      }
   }
   #endregion
}