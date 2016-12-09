using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;

namespace RemoteDesktopManager
{
   public class Utility
   {
      #region CenterMessageBox
      // Source: http://www.msnewsgroups.net/group/microsoft.public.dotnet.languages.csharp/topic411.aspx
      // Author: alex_f_il@hotmail.com

      static IWin32Window _owner;
      private static HookProc _hookProc;
      private static IntPtr _hHook;

      static Utility()
      {
         _hookProc = new HookProc(MessageBoxHookProc);
         _hHook = IntPtr.Zero;
      }

      // as alternative to AppDomain.GetCurrentThreadId() which is obsolete
      [DllImport( "kernel32.dll" )]
      private static extern int GetCurrentThreadId();

      [DllImport( "user32.dll" )]
      private static extern int MoveWindow( IntPtr hWnd, int X, int Y, int nWidth, 
         int nHeight, bool bRepaint );

      [DllImport( "user32.dll" )]
      public static extern IntPtr SetWindowsHookEx( int idHook, HookProc lpfn, IntPtr hInstance, int threadId );

      [DllImport( "user32.dll" )]
      public static extern IntPtr CallNextHookEx( IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam );

      [DllImport( "user32.dll" )]
      public static extern int UnhookWindowsHookEx( IntPtr idHook );

      [DllImport( "user32.dll" )]
      private static extern bool GetWindowRect( IntPtr hWnd, ref Rectangle lpRect );

      public delegate IntPtr HookProc( int nCode, IntPtr wParam, IntPtr lParam );

      public const int WH_CALLWNDPROCRET = 12;

      public enum CbtHookAction : int
      {
         HCBT_MOVESIZE = 0,
         HCBT_MINMAX = 1,
         HCBT_QS = 2,
         HCBT_CREATEWND = 3,
         HCBT_DESTROYWND = 4,
         HCBT_ACTIVATE = 5,
         HCBT_CLICKSKIPPED = 6,
         HCBT_KEYSKIPPED = 7,
         HCBT_SYSCOMMAND = 8,
         HCBT_SETFOCUS = 9
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct CWPRETSTRUCT 
      {
         public IntPtr lResult;
         public IntPtr lParam;
         public IntPtr wParam;
         public uint message;
         public IntPtr hwnd;
      };

      private static void CenterMessageBoxInitialize( MainForm poOwner ) 
      {
         _owner = poOwner;
         if(_hHook != IntPtr.Zero)
            throw new NotSupportedException("multiple calls are notsupported");

         if(_owner != null)
         {
            // the C# compiler will suggest Thread.CurrentThread.ManagedThreadId as
            // alternative to AppDomain.GetCurrentThreadId() but that does not work
            // as an workaround use the kernel32.GetCurrentThreadId call to make it 
            // work and don't get the compiler warning
            //
            _hHook = SetWindowsHookEx( WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, GetCurrentThreadId() );
         }
      }

      private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam) 
      {
         if(nCode < 0)
            return CallNextHookEx(_hHook, nCode, wParam, lParam);

         CWPRETSTRUCT msg = (CWPRETSTRUCT) Marshal.PtrToStructure(lParam, typeof (CWPRETSTRUCT));
         IntPtr hook = _hHook;

         if(msg.message == (int) CbtHookAction.HCBT_ACTIVATE) 
         {
            try {
               CenterWindow(msg.hwnd);
            } finally {
               UnhookWindowsHookEx(_hHook);
               _hHook = IntPtr.Zero;
            }
         }
         return CallNextHookEx(hook, nCode, wParam, lParam);
      }

      private static void CenterWindow(IntPtr hChildWnd) 
      {
         Rectangle recChild = new Rectangle(0, 0, 0, 0);
         bool success = GetWindowRect(hChildWnd, ref recChild);

         int width = recChild.Width - recChild.X;
         int height = recChild.Height - recChild.Y;

         Rectangle recParent = new Rectangle(0, 0, 0, 0);
         success = GetWindowRect(_owner.Handle, ref recParent);

         Point ptCenter = new Point(0, 0);
         ptCenter.X = recParent.X + ((recParent.Width - recParent.X)/2);
         ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y)/2);


         Point ptStart = new Point(0, 0);
         ptStart.X = (ptCenter.X - (width/2));
         ptStart.Y = (ptCenter.Y - (height/2));

         ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
         ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

         int result = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width, height, false);
      }

      #endregion

      public static String getApplicationPath()
      {
         String lsPath = Application.ExecutablePath;
         return Path.GetDirectoryName( lsPath );
      }

      public static String getApplicationName()
      {
         String lsPath = Application.ExecutablePath;
         return Path.GetFileNameWithoutExtension( lsPath );
      }

      public static String getAppDataPath()
      {
         string lsPath = Path.Combine( Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData ), getApplicationName() );

         if(Directory.Exists( lsPath ) == false)
            Directory.CreateDirectory( lsPath );

         return lsPath;
      }

      public static String getConfigFileName()
      {
         if(Directory.Exists( getAppDataPath() ) == true)
         {
            return Path.Combine( getAppDataPath(), getApplicationName() + "_config.xml" );
         }
         else
         {
            return Path.Combine( getApplicationPath(), getApplicationName() + "_config.xml" );
         }
      }

      public static String getDefaultRDPConfigDir()
      {
         string lsConnectionDirPath = Path.Combine( getApplicationPath(), "connections" );

         if(Directory.Exists( getAppDataPath() ) == true)
         {
            lsConnectionDirPath = Path.Combine( getAppDataPath(), "connections" );
         }

         if(Directory.Exists( lsConnectionDirPath ) == false)
            Directory.CreateDirectory( lsConnectionDirPath );

         return lsConnectionDirPath;
      }

      public static DialogResult showMessageBox( MainForm poOwner, String psMsg, Exception poException )
      {
         String lsMsg = psMsg + "\n" + poException.Message;
         if( poException.InnerException != null )
         {
            lsMsg += "\n" + poException.InnerException.Message;
         }

         if( poException.StackTrace != null && poException.StackTrace.Length > 0 )
         {
            lsMsg += "\n\nCallstack:\n" + poException.StackTrace;
         }

         return showMessageBox( poOwner, lsMsg, MessageBoxIcon.Error );
      }

      public static DialogResult showMessageBox( MainForm poOwner, String psMsg )
      {
         return showMessageBox( poOwner, psMsg, MessageBoxIcon.Information );
      }

      public static DialogResult showMessageBox( MainForm poOwner, String psMsg, 
         MessageBoxIcon poIcon )
      {
         return showMessageBox( poOwner, psMsg, MessageBoxButtons.OK, poIcon, MessageBoxDefaultButton.Button1 );
      }

      public static DialogResult showMessageBox( MainForm poOwner, String psMsg, 
         MessageBoxButtons poButtons, MessageBoxIcon poIcon, MessageBoxDefaultButton poDefaultBtn )
      {
         CenterMessageBoxInitialize( poOwner );
         return MessageBox.Show( poOwner, psMsg, poOwner.Text, poButtons, poIcon, poDefaultBtn );
      }

      /// <summary>
      /// Restore component values from a XML document
      /// </summary>
      /// <param name="poDoc">Valid XML Document reference</param>
      /// <param name="poControl">Typically current form</param>
      public static void SetValuesFromXML( XmlDocument poDoc, Control poControl )
      {
         foreach( Control loComponent in poControl.Controls )
         {
            if( loComponent is TextBox )
            {
               XmlNodeList loNode = poDoc.GetElementsByTagName( ( (TextBox)loComponent ).Name );

               if( loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null )
                  ( (TextBox)loComponent ).Text = loNode[0].FirstChild.Value;
            }
            if( loComponent is ComboBox )
            {
               XmlNodeList loNode = poDoc.GetElementsByTagName( ( (ComboBox)loComponent ).Name );

               if( loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null )
                  ( (ComboBox)loComponent ).SelectedItem = loNode[0].FirstChild.Value;
            }
            if( loComponent is TreeView )
            {
               XmlNodeList loNode = poDoc.GetElementsByTagName( ( (TreeView)loComponent ).Name );

               String[] lasPath = null;
               TreeView loTree = (TreeView)loComponent;
               if( loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null )
               {
                  lasPath = loNode[0].FirstChild.Value.Split( loTree.PathSeparator.ToCharArray() );
               }

               if( lasPath != null )
               {
                  TreeNodeCollection loNodes = loTree.Nodes;
                  for( int i = 0; i < lasPath.Length; i++ )
                  {
                     if( loNodes != null )
                     {
                        int liIndex = loNodes.IndexOfKey( lasPath[i] );
                        if( liIndex > -1 )
                        {
                           loNodes[liIndex].Expand();
                           loTree.SelectedNode = loNodes[liIndex];
                           loNodes = loNodes[liIndex].Nodes;
                        }
                     }
                  }
               }
            }
            if( loComponent is CheckBox )
            {
               XmlNodeList loNode = poDoc.GetElementsByTagName( ( (CheckBox)loComponent ).Name );

               if( loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null )
                  ( (CheckBox)loComponent ).Checked = Convert.ToBoolean( loNode[0].FirstChild.Value );
            }
            if( loComponent.Controls.Count > 0 )
            {
               SetValuesFromXML( poDoc, loComponent );
            }
         }
      }

      /// <summary>
      /// To store component values in a XML document.
      /// </summary>
      /// <param name="poWriter">Valid XML writer object</param>
      /// <param name="poControl">Typically the form that calls this method</param>
      public static void SetXMLFromValue( XmlTextWriter poWriter, Control poControl )
      {
         foreach( Control loComponent in poControl.Controls )
         {
            if( loComponent is TextBox )
            {
               poWriter.WriteElementString
               (
                  ( (TextBox)loComponent ).Name,
                  ( (TextBox)loComponent ).Text
               );
            }
            if( loComponent is CheckBox )
            {
               poWriter.WriteElementString
               (
                  ( (CheckBox)loComponent ).Name,
                  Convert.ToString( ( (CheckBox)loComponent ).Checked )
               );
            }
            if( loComponent is ComboBox )
            {
               poWriter.WriteElementString
               (
                  ( (ComboBox)loComponent ).Name,
                  Convert.ToString( ( (ComboBox)loComponent ).SelectedItem )
               );
            }
            if( loComponent is TreeView )
            {
               TreeView loTree = (TreeView)loComponent;

               if( loTree.SelectedNode != null )
               {
                  poWriter.WriteElementString
                  (
                     loTree.Name,
                     loTree.SelectedNode.FullPath
                  );
               }
            }
            if( loComponent.Controls.Count > 0 )
            {
               poWriter.WriteStartElement( loComponent.Name );
               SetXMLFromValue( poWriter, loComponent );
               poWriter.WriteEndElement();
            }
         }
      }

      public static void backupConfigFile()
      {
         try
         {
            FileInfo loFile = new FileInfo( getConfigFileName() );
            if( loFile.Exists )
            {
               if( File.Exists( getConfigFileName() + ".bak" ) )
               {
                  File.Delete( getConfigFileName() + ".bak" );
               }
               loFile.CopyTo( getConfigFileName() + ".bak" );
            }
         }
         catch( Exception ex )
         {
            showMessageBox( null, "Error occured while creating backup for config", ex );
         }
      }

      public static void startRDClient( RDPConnection poConnection, Boolean pbDontWriteUserHintRegKey )
      {
         RegistryKey loKey = null;
         try
         {
            if(poConnection.UseLocalDrives == true || IsAtLeast60MSTSCClient() == true )
            {
               loKey = Registry.CurrentUser.OpenSubKey(
                  "Software\\Microsoft\\Terminal Server Client\\LocalDevices", true );

               if( loKey == null )
               {
                  loKey = Registry.CurrentUser.CreateSubKey(
                     "Software\\Microsoft\\Terminal Server Client\\LocalDevices");
               }

               if( loKey != null )
               {
                  if( IsAtLeast60MSTSCClient() == true )
                  {
                     loKey.SetValue( poConnection.Hostname, 13, RegistryValueKind.DWord );
                  }
                  else
                  {
                     loKey.SetValue( poConnection.Hostname, 1, RegistryValueKind.DWord );
                  }

                  loKey.Close();
               }
            }

            // set login hint 
            //
            if(IsAtLeast60MSTSCClient() == true && pbDontWriteUserHintRegKey == false)
            {
               loKey = Registry.CurrentUser.OpenSubKey(
                  "Software\\Microsoft\\Terminal Server Client\\UsernameHint", true );

               if(loKey == null)
               {
                  loKey = Registry.CurrentUser.CreateSubKey(
                     "Software\\Microsoft\\Terminal Server Client\\UsernameHint" );
               }

			      string userNameHint = poConnection.Username;
			      if( string.IsNullOrEmpty(poConnection.Domain) == false )
                     userNameHint = poConnection.Domain + "\\" + userNameHint;

               if(loKey != null)
               {
                  loKey.SetValue
                  ( 
                     poConnection.Hostname, 
                     userNameHint, 
                     RegistryValueKind.String 
                  );
                  loKey.Close();
               }
            }

            Process loProcess = new Process();
            loProcess.StartInfo.WorkingDirectory = getApplicationPath();
            loProcess.StartInfo.FileName = getMSTSCExeFile().FullName;
            loProcess.StartInfo.Arguments = "\"" + poConnection.RDPFileName + "\"";

            if(poConnection.UseConsoleSession == true && 
                IsAtLeast61MSTSCClient() == true)
            {
                loProcess.StartInfo.Arguments += " /admin";
            }

            loProcess.Start();

            // TO-DO: store loProcess.Id in some monitor thread to detect if the 
            // the session was ended, can be usefull to allow post-run scripts to run
         }
         catch( Exception ex )
         {
            showMessageBox( null, "Failed to start Remote Desktop client", ex );
         }
      }

      public static void startRDClientForEdit( RDPConnection poConnection )
      {
         try
         {
            Process loProcess = new Process();
            loProcess.StartInfo.WorkingDirectory = getApplicationPath();
            loProcess.StartInfo.FileName = getMSTSCExeFile().FullName;
            loProcess.StartInfo.Arguments = "/edit \"" + poConnection.RDPFileName + "\"";

            loProcess.Start();
         }
         catch(Exception ex)
         {
            showMessageBox( null, "Failed to start Remote Desktop client", ex );
         }
      }

      public static bool SameBaseVersionAsApp( String psVersion )
      {
         if( psVersion == null )
            return false;

         String[] lasAppVersion = Application.ProductVersion.Split('.');
         String[] lasVersion = psVersion.Split('.');

         if( lasVersion.Length != 4 )
            return false;

         if(lasAppVersion[0].Equals( lasVersion[0] ) &&
            lasAppVersion[1].Equals( lasVersion[1] ) &&
            lasAppVersion[2].Equals( lasVersion[2] ))
         {
            return true;
         }
         return false;
      }

      /// <summary>
      /// The 6.0 version of Remote Desktop Connection tool is bundled
      /// with Vista and installed via update KB925876
      /// Windows XP SP3, Vista SP1 and 2008 server RTM include MSTSC 6.1
      /// </summary>
      public static Boolean IsAtLeast60MSTSCClient()
      {
         try
         {
            FileVersionInfo loFileVersionInfo =
               FileVersionInfo.GetVersionInfo( getMSTSCExeFile().FullName );

            return ( loFileVersionInfo.FileMajorPart == 6 &&
                     loFileVersionInfo.FileBuildPart >= 6000 );
         }
         catch( Exception ex )
         {
            throw ex;
         }
      }

      /// <summary>
      /// <see cref="IsAtLeast60MSTSCClient"/>
      /// </summary>
      /// <returns></returns>
      public static Boolean IsAtLeast61MSTSCClient()
      {
          try
          {
              FileVersionInfo loFileVersionInfo =
               FileVersionInfo.GetVersionInfo( getMSTSCExeFile().FullName );

              return (loFileVersionInfo.FileMajorPart == 6 &&
                     loFileVersionInfo.FileBuildPart >= 6001);
          }
          catch(Exception ex)
          {
              throw ex;
          }
      }

      public static FileInfo getMSTSCExeFile()
      {
         FileInfo loFile = new FileInfo
         (
            getApplicationPath() + "\\mstsc.exe"
         );

         if(loFile.Exists == true)
         {
            return loFile;
         }
         
         loFile = new FileInfo
         (
            Environment.GetEnvironmentVariable( "SystemRoot" ) +
               "\\System32\\mstsc.exe"
         );

         if(loFile.Exists == false)
         {
            throw new Exception( "MSTSC is not installed in system32 dir." );
         }

         return loFile;
      }
   }
}
