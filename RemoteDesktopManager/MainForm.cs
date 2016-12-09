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
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace RemoteDesktopManager
{
   public partial class MainForm:Form
   {
      #region TreeView Drag and Drop
      private delegate void DelegateReadRDPFile( String psFileName ); // type
      private DelegateReadRDPFile m_DelegateReadRDPFile;     // instance
      private string NodeMap;
      private const int MAPSIZE = 128;
      private StringBuilder NewNodeMap = new StringBuilder( MAPSIZE );
      #endregion

      static String UPDATE_CHECK_URL = "http://www.tudra.net/tscm/update.php";
      public WebClient moWebClient = new WebClient();

      static MainForm moSelf = null;

      public Dictionary<String, RDPConnection> mcoConnections =
         new Dictionary<String, RDPConnection>();
      public String msCurrentConnection = "";

      String msRDPFileLocation = null;
      Boolean mbExpandOnlyOneNode = false;
      Boolean mbCheckForUpdateAtStartup = true;
      Boolean mbMinimizeToTray = true;
      Boolean mbUseSubMenusInTrayMenu = true;
      Boolean mbDontWriteUserHintRegKey = false;

      bool mbMarkFormDataDirty = false;
      bool mbFormDataIsUdating = false;

      private NotifyIcon moTrayIcon;
      private ContextMenuStrip moTrayIconMenu;

      private FormWindowState moStateBeforeMinimize;

      // To make sure we don't save the config if the load
      // failed. It might leave you with an empty XML
      //
      private Boolean mbConfigLoadError = false;

      public MainForm()
      {
         moSelf = this;

         moWebClient.DownloadProgressChanged += 
            new DownloadProgressChangedEventHandler(moWebClient_DownloadProgressChanged);
         moWebClient.DownloadFileCompleted +=
            new AsyncCompletedEventHandler(moWebClient_DownloadFileCompleted);

         InitializeComponent();

         moTrayIconMenu = new ContextMenuStrip();

         moTrayIconMenu.Items.Add(
             new ToolStripMenuItem( "Show", null, new System.EventHandler( moTrayIcon_DoubleClick ) ) );
         moTrayIconMenu.Items.Add(
             new ToolStripMenuItem( "Exit", null, new System.EventHandler( moTrayIcon_Exit_Click ) ) );

         moTrayIconMenu.AllowMerge = true;
         moTrayIconMenu.ShowCheckMargin = false;
         moTrayIconMenu.ShowImageMargin = true;

         moTrayIcon = new NotifyIcon();
         moTrayIcon.Text = "RemoteDesktopManager";
         moTrayIcon.Visible = true;

         moTrayIcon.Icon = this.Icon;
         moTrayIcon.ContextMenuStrip = moTrayIconMenu;

         moTrayIcon.Click += new EventHandler(moTrayIcon_Click);
         moTrayIcon.DoubleClick += new EventHandler( moTrayIcon_DoubleClick );
      }

      public static MainForm FormRef
      {
         get
         {
            return moSelf;
         }
      }

      public bool FormDataDirty
      {
         get
         {
            return mbMarkFormDataDirty;
         }
         set
         {
            mbMarkFormDataDirty = value;
            if(mbMarkFormDataDirty == true)
            {
               this.grpSettings.Text = "Settings*:";
            }
            else
            {
               this.grpSettings.Text = "Settings:";
            }
         }
      }

      public ComboBox GroupListBox
      {
         get
         {
            return this.cmbGroup;
         }
      }

      public TreeView ComputersTreeView
      {
         get
         {
            return this.treeComputers;
         }
      }

      public RDPConnection CurrentConnection
      {
         get
         {
            if(msCurrentConnection == null ||
                mcoConnections[msCurrentConnection] == null)
            {
               throw new Exception( "No CurrentConnection is set" );
            }
            
            return mcoConnections[msCurrentConnection]; 
         }
      }

      public String RDPFileLocation
      {
         get
         {
            if(msRDPFileLocation == null)
            {
               return Utility.getDefaultRDPConfigDir();
            }
            return msRDPFileLocation;
         }
         set
         {
            if(value == null || value.Length == 0)
            {
               msRDPFileLocation = Utility.getDefaultRDPConfigDir();
            }
            else
            {
               if(Directory.Exists( value ) == false)
               {
                  throw new Exception(
                     "Path '" + value + "' is not a directory" );
               }
               msRDPFileLocation = value;
            }
         }
      }

      public Boolean ExpandOnlyOneNode
      {
         get
         {
            return mbExpandOnlyOneNode;
         }
         set
         {
            mbExpandOnlyOneNode = value;
         }
      }

      public Boolean DontWriteUserHintRegKey
      {
         get
         {
            return mbDontWriteUserHintRegKey;
         }
         set
         {
            mbDontWriteUserHintRegKey = value;
         }
      }

      public Boolean CheckForUpdateAtStartup
      {
         get
         {
            return mbCheckForUpdateAtStartup;
         }
         set
         {
            mbCheckForUpdateAtStartup = value;
         }
      }

      public Boolean MinimizeToTray
      {
         get
         {
            return mbMinimizeToTray;
         }
         set
         {
            mbMinimizeToTray = value;
         }
      }

      public Boolean UseSubMenusInTrayMenu
      {
         get
         {
            return mbUseSubMenusInTrayMenu;
         }
         set
         {
            mbUseSubMenusInTrayMenu = value;
         }
      }

      #region Form Load

      private String getStringValue( XmlDocument poDoc, String psKey )
      {
         if(poDoc == null)
            return "";

         XmlNodeList loNode = poDoc.GetElementsByTagName( psKey );
         if(loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null)
         {
            return "" + loNode[0].FirstChild.Value;
         }

         return "";
      }

      private void MainForm_Load( object sender, System.EventArgs e )
      {
         // create delegate used for asynchronous call
         //
         m_DelegateReadRDPFile = new DelegateReadRDPFile( this.ReadRDPFile );

         string[] version = Application.ProductVersion.Split( '.' );
         lblAbout.Text = Application.ProductName + " - " + version[0] + "." + version[1];

         #region temporary 1.5 to 1.6 upgrade code

         if(File.Exists( Utility.getConfigFileName() ) == true && 
            File.Exists( Path.Combine( Utility.getApplicationPath(), 
            Utility.getApplicationName() + "_config.xml" ) ) == true )
         {
            try
            {
               File.Delete( Path.Combine( Utility.getApplicationPath(),
                  Utility.getApplicationName() + "_config.xml" ) );

               Directory.Delete( Path.Combine( 
                  Utility.getApplicationPath(), "connections" ), true );
            }
            catch( Exception ex )
            {
               Utility.showMessageBox( this, 
                  "Error deleting old config, this can be ignored", ex );
            }
         }

         #endregion

         XmlTextReader loReader = null;
         try
         {
            // In the unlikely situation the config was not
            // saved on close, we can't continue to start the app
            // manual intervention is needed by the user.
            //
            if( File.Exists( Utility.getConfigFileName() + ".tmp" ) )
            {
               throw new Exception( "Temporary config file (" + 
                  Utility.getConfigFileName() + ".tmp" + ") exists. " + 
                  "Probably previous close of application failed. Check file manually." );
            }
            
            if( File.Exists( Utility.getConfigFileName() ) )
            {
               loReader = new XmlTextReader
               (
                  new FileStream( Utility.getConfigFileName(), FileMode.Open, FileAccess.Read )
               );
               XmlDocument loDoc = new XmlDocument();
               loDoc.Load( loReader );

               String[] lasSize = getStringValue( loDoc, "windowSize" ).Split(',');
               String[] lasLocation = getStringValue( loDoc, "windowLocation" ).Split( ',' );

               try
               {
                  int liW = Convert.ToInt32( lasSize[0] );
                  int liH = Convert.ToInt32( lasSize[1] );
                  
                  int liX = Convert.ToInt32( lasLocation[0] );
                  int liY = Convert.ToInt32( lasLocation[1] );

                  switch(getStringValue( loDoc, "windowState" ))
                  {
                     case "Normal": WindowState = FormWindowState.Normal; break;
                     case "Minimized":
                     {
                        liX = ( Screen.PrimaryScreen.WorkingArea.Width / 2 ) - ( this.MinimumSize.Width / 2 );
                        liY = ( Screen.PrimaryScreen.WorkingArea.Height / 2 ) - ( this.MinimumSize.Height / 2 );
                        WindowState = FormWindowState.Minimized;
                        break;
                     }
                     case "Maximized":
                     {
                        liW = this.MinimumSize.Width;
                        liH = this.MinimumSize.Height;
                        WindowState = FormWindowState.Maximized;
                        break;
                     }
                  }

                  Location = new Point( liX, liY );
                  Size = new Size( liW, liH );
               }
               catch { }

               // temporary 1.5 to 1.6 check to see if the file needs to be upgraded
               // to the Application Data path
               // 
               string lsRdpFileLocation = getStringValue( loDoc, "RDPFileLocation" );
               if(Path.Combine( Utility.getApplicationPath(), "connections" ).Equals(
                  lsRdpFileLocation, StringComparison.OrdinalIgnoreCase ) == true)
               {
                  // do not set the RDP location property
               }

               try
               {
                  this.ExpandOnlyOneNode = Boolean.Parse(
                     getStringValue( loDoc, "ExpandOnlyOneNode" ) );
               }
               catch{/*ignore*/};

               try
               {
                  this.CheckForUpdateAtStartup = Boolean.Parse(
                     getStringValue( loDoc, "CheckForUpdateAtStartup" ) );
               }
               catch {/*ignore*/};

               try
               {
                  this.DontWriteUserHintRegKey = Boolean.Parse(
                     getStringValue( loDoc, "DontWriteUserHintRegKey" ) );
               }
               catch {/*ignore*/};

               try
               {
                  this.MinimizeToTray = Boolean.Parse(
                     getStringValue( loDoc, "MinimizeToTray" ) );
               }
               catch {/*ignore*/};

               try
               {
                  this.UseSubMenusInTrayMenu = Boolean.Parse(
                     getStringValue( loDoc, "UseSubMenusInTrayMenu" ) );
               }
               catch {/*ignore*/};

               XmlNodeList loGroupsNode = loDoc.GetElementsByTagName( "groups" );
               if( loGroupsNode != null )
               {
                  loGroupsNode = loDoc.GetElementsByTagName( "group" );
                  if( loGroupsNode != null )
                  {
                     for( int i = 0; i < loGroupsNode.Count; i++ )
                     {
                        if( loGroupsNode[i].FirstChild != null )
                        {
                           if( loGroupsNode[i].FirstChild.Value == null )
                              continue;
                           cmbGroup.Items.Add( loGroupsNode[i].FirstChild.Value );
                        }
                     }
                  }
               }

               XmlNodeList loComputersNode = loDoc.GetElementsByTagName( "computers" );
               if( loComputersNode != null )
               {
                  foreach( XmlNode loNode in loComputersNode )
                  {
                     foreach( XmlNode loInnerNode in loNode.ChildNodes )
                     {
                        if( loInnerNode.Name.Equals( "group" ) )
                        {
                           String lsGroupName = loInnerNode.Attributes.GetNamedItem( "name" ).Value;
                           Boolean lbExpanded = false;
                           if( loInnerNode.Attributes != null &&
                               loInnerNode.Attributes.GetNamedItem( "expanded" ) != null )
                           {
                              try
                              {
                                 Convert.ToBoolean( 
                                    loInnerNode.Attributes.GetNamedItem( "expanded" ).Value );
                              }
                              catch( Exception ){ /* ignore */ }
                           }
    
                           TreeNode loGroup = new TreeNode( lsGroupName, 0, 0 );
                           loGroup.Name = lsGroupName;
                           this.treeComputers.Nodes.Add( loGroup );

                           foreach( XmlNode loComputerNode in loInnerNode.ChildNodes )
                           {
                              String lsCompName = loComputerNode.FirstChild.Value;
                              String lsKey = lsCompName;
                              if( loComputerNode.Attributes != null && 
                                  loComputerNode.Attributes.GetNamedItem( "key" ) != null )
                              {
                                 lsKey = loComputerNode.Attributes.GetNamedItem( "key" ).Value;
                              }

                              TreeNode loNewNode = new TreeNode( lsCompName, 1, 1 );
                              loNewNode.Name = lsKey;
                              loGroup.Nodes.Add( loNewNode );
                           }
                           if( lbExpanded == true )
                           {
                              loGroup.Expand();
                           }
                        }
                     }
                  }
               }

               XmlNodeList loXmlNode = loDoc.GetElementsByTagName( "selectedTreeNode" );

               String[] lasPath = null;
               if(loXmlNode != null && loXmlNode.Count > 0 && loXmlNode[0].FirstChild != null)
               {
                  lasPath = loXmlNode[0].FirstChild.Value.Split( treeComputers.PathSeparator.ToCharArray() );
               }

               if(lasPath != null)
               {
                  TreeNodeCollection loNodes = treeComputers.Nodes;
                  for(int i = 0; i < lasPath.Length; i++)
                  {
                     if(loNodes != null)
                     {
                        int liIndex = loNodes.IndexOfKey( lasPath[i] );
                        if(liIndex > -1)
                        {
                           loNodes[liIndex].Expand();
                           treeComputers.SelectedNode = loNodes[liIndex];
                           loNodes = loNodes[liIndex].Nodes;
                        }
                     }
                  }
               }

               #region 0.4a check
               if(getStringValue( loDoc, "version" ) == null)
                  throw new Exception( "Update to 0.5 first" );
               #endregion

               loReader.Close();
               
            } // end config handling

            RenderTrayIconMenu();

            if( CheckForUpdateAtStartup == true )
            {
               threadCheckForUpdate.RunWorkerAsync();
            }
         }
         catch( Exception ex )
         {
            mbConfigLoadError = true;
            Utility.showMessageBox( this, "Error loading config.", ex );
         }
         finally
         {
            try
            {
               if(loReader != null)
                  loReader.Close();
            }
            catch { /* ignore */ }
         }
      }

      #endregion

      #region Form Closing

      private void WriteTreeXml( XmlTextWriter poWriter, TreeNodeCollection treeNodes )
      {
         foreach( TreeNode loNode in treeNodes )
         {
            if( loNode.Level == 0 )
            {
               poWriter.WriteStartElement( "group" );
               poWriter.WriteAttributeString( "name", loNode.Name );
               poWriter.WriteAttributeString( "expanded", loNode.IsExpanded.ToString() );
               WriteTreeXml( poWriter, loNode.Nodes );
               poWriter.WriteEndElement();
            }
            else
            {
               poWriter.WriteStartElement( "computer" );
               poWriter.WriteAttributeString( "key", loNode.Name );
               poWriter.WriteString( loNode.Text );
               poWriter.WriteEndElement();
            }
         }
      }

      public void writeApplicationConfig()
      {
         XmlTextWriter loWriter = null;
         String lsTmpFile = Utility.getConfigFileName() + ".tmp";  
         try
         {
            if(mbConfigLoadError == true)
            {
               if(Utility.showMessageBox( this, "While loading the config at startup an " +
                  "error occured. If you are confident RDM shows you the correct data, " +
                  "answer Yes. In all other situation answer No.",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2 ) == DialogResult.No)
               {
                  return;
               }
            }

            loWriter = new XmlTextWriter( lsTmpFile, System.Text.Encoding.UTF8 );
            loWriter.Formatting = Formatting.Indented;
            loWriter.WriteStartDocument();
            loWriter.WriteStartElement( "config" );
            loWriter.WriteElementString( "version", Application.ProductVersion );

            loWriter.WriteElementString( "windowState", WindowState.ToString() );
            loWriter.WriteElementString( "windowSize", Size.Width + "," + Size.Height );
            loWriter.WriteElementString( "windowLocation", Location.X + "," + Location.Y );

            loWriter.WriteElementString( "RDPFileLocation", this.RDPFileLocation );
            loWriter.WriteElementString( "ExpandOnlyOneNode", this.ExpandOnlyOneNode.ToString() );
            loWriter.WriteElementString( "DontWriteUserHintRegKey", this.DontWriteUserHintRegKey.ToString() );
            loWriter.WriteElementString( "CheckForUpdateAtStartup", this.CheckForUpdateAtStartup.ToString() );
            loWriter.WriteElementString( "MinimizeToTray", this.MinimizeToTray.ToString() );
            loWriter.WriteElementString( "UseSubMenusInTrayMenu", this.UseSubMenusInTrayMenu.ToString() );

            if(treeComputers.SelectedNode != null)
            {

               String lsPath = "";
               if(treeComputers.SelectedNode.Parent != null)
               {
                  lsPath += treeComputers.SelectedNode.Parent.FullPath;
                  lsPath += treeComputers.PathSeparator;
               }
               lsPath += treeComputers.SelectedNode.Name;
               loWriter.WriteElementString
               (
                  "selectedTreeNode",
                  lsPath
               );
            }

            loWriter.WriteStartElement( "groups" );
            for(int i = 0; i < cmbGroup.Items.Count; i++)
            {
               loWriter.WriteElementString( "group", cmbGroup.Items[i].ToString() );
            }
            loWriter.WriteEndElement();

            loWriter.WriteStartElement( "computers" );
            WriteTreeXml( loWriter, treeComputers.Nodes );
            loWriter.WriteEndElement();

            loWriter.WriteEndElement();
            loWriter.WriteEndDocument();

            loWriter.Close();

            if( File.Exists( Utility.getConfigFileName() ) )
               File.Delete( Utility.getConfigFileName() );

            File.Move( lsTmpFile, Utility.getConfigFileName() );
         }
         catch(Exception ex)
         {
            Utility.showMessageBox( this, "Error saving config.", ex );
         }
         finally
         {
            if(loWriter != null)
               loWriter.Close();
         }
      }

      private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
      {
         //#if DEBUG
         //   Utility.backupConfigFile();
         //#endif

         writeApplicationConfig();

         if(MinimizeToTray)
         {
            if(e.CloseReason == CloseReason.UserClosing)
            {
               e.Cancel = true;
               this.Hide();

               this.ShowInTaskbar = false;
            }
         }
      }

      private void MainForm_Resize( object sender, EventArgs e )
      {
         if(this.WindowState != FormWindowState.Minimized)
         {
            moStateBeforeMinimize = this.WindowState;
         }
         
         if(this.WindowState == FormWindowState.Minimized)
         {
            if(MinimizeToTray)
            {
               this.ShowInTaskbar = false;
            }
         }
      }

      #endregion

      private void sliderResolution_ValueChanged( object sender, EventArgs e )
      {
         TrackBar loBar = (TrackBar)sender;
         switch( loBar.Value )
         {
            case 0:
               this.lblResolution.Text = "800x600";
               this.CurrentConnection.ScreenSize = ScreenSizes.SZ_800x600;
               break;
            case 1:
               this.lblResolution.Text = "1024x768";
               this.CurrentConnection.ScreenSize = ScreenSizes.SZ_1024x768;
               break;
            case 2:
               this.lblResolution.Text = "1280x1024";
               this.CurrentConnection.ScreenSize = ScreenSizes.SZ_1280x1024;
               break;
            case 3:
               this.lblResolution.Text = "1600x1200";
               this.CurrentConnection.ScreenSize = ScreenSizes.SZ_1600x1200;
               break;
         }

         this.formDataChanged( sender, e );
      }

      private void setSliderValueFromConnection()
      {
         if(CurrentConnection == null)
            return;

         switch(CurrentConnection.ScreenSize)
         {
            case ScreenSizes.SZ_800x600:
               sliderResolution.Value = 0;
               break;
            case ScreenSizes.SZ_1024x768:
               sliderResolution.Value = 1;
               break;
            case ScreenSizes.SZ_1280x1024:
               sliderResolution.Value = 2;
               break;
            case ScreenSizes.SZ_1600x1200:
               sliderResolution.Value = 3;
               break;
         }
      }

      private void chkFullScreen_CheckedChanged( object sender, EventArgs e )
      {
         this.sliderResolution.Enabled = !( (CheckBox)sender ).Checked;
         this.lblResolution.Enabled = !( (CheckBox)sender ).Checked;

         this.formDataChanged( sender, e );
      }

      private void cmbGroup_KeyUp( object sender, KeyEventArgs e )
      {
         if( e.KeyCode == Keys.Enter )
         {
            ComboBox loBox = (ComboBox)sender;
            if( loBox.Items.IndexOf( loBox.Text ) == -1 )
            {
               loBox.Items.Add( loBox.Text );
            }
         }
      }

      private void btnNew_Click( object sender, EventArgs e )
      {
         RDPConnection loConn = new RDPConnection( RDPFileLocation );
         mcoConnections.Add( loConn.Id, loConn );
         msCurrentConnection = loConn.Id;
         setFormData();
      }

      private void MainForm_DragDrop( object sender, DragEventArgs e )
      {
         try
         {
            Array a = (Array)e.Data.GetData( DataFormats.FileDrop );

            if( a != null )
            {
               for( int i = 0; i < a.Length; i++ )
               {
                  string lsValue = a.GetValue( i ).ToString();

                  // Call ReadRDPFile asynchronously.
                  // Explorer instance from which file is dropped is not responding
                  // all the time when DragDrop handler is active, so we need to return
                  // immidiately (especially if ReadRDPFile shows MessageBox).
                  // 
                  this.BeginInvoke( m_DelegateReadRDPFile, new Object[] { lsValue } );
               }
               this.Activate(); // in the case Explorer overlaps this form
            }
         }
         catch( Exception ex )
         {
            Trace.WriteLine( "Error in DragDrop function: " + ex.Message );
            // don't show MessageBox here - Explorer is waiting !
         }
      }

      private void MainForm_DragEnter( object sender, DragEventArgs e )
      {
         if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
            e.Effect = DragDropEffects.Copy;
         else
            e.Effect = DragDropEffects.None;
      }

      private void btnConnect_Click( object sender, EventArgs e )
      {
         if(threadConnectButton.IsBusy == true)
            return;

         if(FormDataDirty == true)
         {
            this.btnSave_Click( this, null );
         }
         
         Utility.startRDClient
         (
            this.CurrentConnection, this.DontWriteUserHintRegKey
         );

         threadConnectButton.RunWorkerAsync();
      }

      private void btnSave_Click( object sender, EventArgs e )
      {
         if(msCurrentConnection == null)
         {
            saveGroup( sender );
         }
         else
         {
            saveConnection( sender );
         }

         RenderTrayIconMenu();
      }

      private void saveConnection( object sender )
      {
         if(txtName.Text == null || txtName.Text.Length == 0)
         {
            Utility.showMessageBox( this, "Name not set." );
            txtName.Focus();
            return;
         }

         if(cmbGroup.Text.Length > 0 && cmbGroup.Items.IndexOf( cmbGroup.Text ) == -1)
         {
            cmbGroup.Items.Add( cmbGroup.Text );
            cmbGroup.SelectedIndex = cmbGroup.Items.IndexOf( cmbGroup.Text );
         }

         if(cmbGroup.SelectedItem == null)
         {
            Utility.showMessageBox( this, "No group selected." );
            cmbGroup.Focus();
            return;
         }

         // check if the group/name combination already exist, if so raise error
         //
         TreeNode[] loNodes = this.treeComputers.Nodes.Find( cmbGroup.SelectedItem.ToString(), true );
         if(loNodes != null && loNodes.Length > 0)
         {
            foreach(TreeNode loN in loNodes)
            {
               for(int n = 0; n < loN.Nodes.Count; n++)
               {
                  if(txtName.Text.Trim().Equals( loN.Nodes[n].Text ))
                  {
                     if(CurrentConnection.Id.Equals( loN.Nodes[n].Name ))
                     {
                        // We have selected ourselves to be updated.
                     }
                     else
                     {
                        Utility.showMessageBox( this, "Group/Name combination already used." );
                        txtName.Focus();
                        txtName.SelectAll();
                        return;
                     }
                  }
               }
            }
         }

         CurrentConnection.Name = txtName.Text.Trim();
         CurrentConnection.Group = cmbGroup.SelectedItem.ToString();
         CurrentConnection.Hostname = txtComputerHost.Text.Trim();
         CurrentConnection.Port = Convert.ToInt32( txtComputerPort.Text );
         CurrentConnection.Username = txtUsername.Text.Trim();
         CurrentConnection.Domain = txtDomain.Text.Trim();
         CurrentConnection.UseLocalDrives = chkConnectLocalDrives.Checked;
         CurrentConnection.UseLocalPrinters = chkConnectLocalPrinters.Checked;
         CurrentConnection.UseConsoleSession = chkConnectToConsole.Checked;
         CurrentConnection.FullScreen = chkFullScreen.Checked;
         CurrentConnection.Span = chkSpan.Checked;
         CurrentConnection.MaximumGUIExperience = chkMaximumGUIExperience.Checked;

         CurrentConnection.PromptCredentials = chkPromptCredentials.Checked;

         this.CurrentConnection.WriteRDPConfig();

         TreeNode loNode;

         this.treeComputers.BeginUpdate();

         loNodes = this.treeComputers.Nodes.Find( CurrentConnection.Group, true );
         if(loNodes != null && loNodes.Length > 0)
         {
            loNode = loNodes[0];
         }
         else
         {
            loNode = new TreeNode( CurrentConnection.Group, 0, 0 );
            loNode.Name = CurrentConnection.Group;
            this.treeComputers.Nodes.Add( loNode );
         }
         loNodes = loNode.Nodes.Find( CurrentConnection.Id, true );
         if(loNodes == null || loNodes.Length == 0)
         {
            TreeNode loNewNode = new TreeNode( CurrentConnection.Name, 1, 1 );
            loNewNode.Name = CurrentConnection.Id;
            loNode.Nodes.Add( loNewNode );
            this.treeComputers.SelectedNode = loNewNode;
         }
         else
         {
            foreach(TreeNode loN in loNodes)
            {
               loN.Text = CurrentConnection.Name;
               this.treeComputers.SelectedNode = loN;
            }
         }

         this.treeComputers.EndUpdate();

         this.treeComputers.Select();

         writeApplicationConfig();

         this.btnConnect.Enabled = true;

         FormDataDirty = false;
      }

      private void saveGroup( object sender )
      {
         if(cmbGroup.Text.Length > 0 && cmbGroup.Items.IndexOf( cmbGroup.Text ) == -1)
         {
            cmbGroup.Items.Add( cmbGroup.Text );
            cmbGroup.SelectedIndex = cmbGroup.Items.IndexOf( cmbGroup.Text );
         }

         if(cmbGroup.SelectedItem == null)
         {
            Utility.showMessageBox( this, "No group selected." );
            cmbGroup.Focus();
            return;
         }

         TreeNode loOldGroupNode = this.treeComputers.SelectedNode;

         TreeNode loNode;
         String lsGroup = cmbGroup.SelectedItem.ToString();

         TreeNode[] loNodes = this.treeComputers.Nodes.Find( lsGroup, true );
  
         if(loNodes != null && loNodes.Length > 0)
         {
            Utility.showMessageBox( this, "Group already exists, move the items manually." );
            return;
         }
         else
         {
            loNode = loOldGroupNode;
         }

         this.treeComputers.BeginUpdate();

         loNode.Name = lsGroup;
         loNode.Text = lsGroup;

         foreach(TreeNode loN in loNode.Nodes)
         {
            try 
	         {	        
               mcoConnections[loN.Name].Group = loNode.Name;
               mcoConnections[loN.Name].WriteRDPConfig();
	         }
	         catch( Exception )
	         {
               // sometimes an exception occures that loN.Name not in connection 
               // list. Not sure why that is, but just do a retry to make sure
               try
               {
                  mcoConnections[loN.Name].Group = loNode.Name;
                  mcoConnections[loN.Name].WriteRDPConfig();
               }
               catch
               {
                  //throw new Exception( loN.Name + ": " + ex2.Message );
               }
	         }
         }

         this.treeComputers.EndUpdate();

         this.treeComputers.Select();

         writeApplicationConfig();

         FormDataDirty = false;
      }

      private void lblAbout_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
         About loFrm = new About();
         loFrm.Owner = this;
         loFrm.ShowDialog();
      }

      private void btnDelete_Click( object sender, EventArgs e )
      {
         if( Utility.showMessageBox( this,
            "Are you sure you want to delete this connection?",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
            MessageBoxDefaultButton.Button1 ) == DialogResult.No )
         {
            return;
         }

         CurrentConnection.DeleteConfig();

         TreeNode[] loNodes = 
            this.treeComputers.Nodes.Find( CurrentConnection.Id, true );

         TreeNode loNewSelection = null;
         if( loNodes != null || loNodes.Length > 0 )
         {
            foreach( TreeNode loNode in loNodes )
            {
               loNewSelection = loNode.PrevNode;
               treeComputers.SelectedNode = null;
               treeComputers.Nodes.Remove( loNode );
            }
         }

         mcoConnections.Remove( msCurrentConnection );
         msCurrentConnection = null;

         treeComputers.SelectedNode = loNewSelection;
      }

      private void btnDeleteGroup_Click( object sender, EventArgs e )
      {
         GroupList loGroupList = new GroupList( this );
         loGroupList.Owner = this;
         loGroupList.ShowDialog();
      }

      // Called from form drag&drop delegate
      //
      public void ReadRDPFile( string psFileName )
      {
         if( psFileName == null )
            return;
         
         if( psFileName.ToLower().EndsWith( ".rdp" ) == false )
         {
            Utility.showMessageBox( this, "Dropped file '" + psFileName + "' is not an RDP file" );
            return;
         }

         RDPConnection loConn = 
            RDPConnection.createConnectionFromRDPFile( psFileName, RDPFileLocation );

         if(mcoConnections.ContainsKey( loConn.Id ) == false)
         {
            mcoConnections.Add( loConn.Id, loConn );
         }
         msCurrentConnection = loConn.Id;

         setFormData();
         
         if( CurrentConnection.Name != null && CurrentConnection.Name.Length > 0 &&
             CurrentConnection.Group != null && CurrentConnection.Group.Length > 0 )
         {
            btnSave_Click( this, null );
         }
      }

      #region Tree Events

      private void treeComputers_AfterSelect( object sender, TreeViewEventArgs e )
      {
         TreeView loTree = (TreeView)sender;
         try
         {
            loTree.BeginUpdate();

            TreeNode loSelectedNode = e.Node;

            if(loSelectedNode.Name.Length > 0)
            {
               if(loSelectedNode.Level == 0)
               {
                  // single node select code
                  //
                  Point loPos = Cursor.Position;
                  if(this.ExpandOnlyOneNode == true)
                  {
                     foreach(TreeNode loN in loTree.Nodes)
                     {
                        if(loN.Level == 0 && 
                           loN.Equals( loSelectedNode ) == false )
                        {
                           loN.Collapse( true );
                        }
                     }
                  }
                  loSelectedNode.Expand();
                  Cursor.Position = loPos;
                  //
                  // end single node select code

                  msCurrentConnection = null;

                  mbFormDataIsUdating = true;
                  enableSettingsForm( false );

                  try
                  {
                     cmbGroup.SelectedIndex =
                        cmbGroup.Items.IndexOf( loSelectedNode.Name );
                  }
                  catch { }

                  mbFormDataIsUdating = false;

                  return; // group node
               }

               if(msCurrentConnection != null &&
                   loSelectedNode.Name.Equals( CurrentConnection.Id ) == true)
               {
                  return;
               }

               msCurrentConnection = loSelectedNode.Name;

               if(mcoConnections.ContainsKey( msCurrentConnection ) == false)
               {
                  RDPConnection loConn = new RDPConnection( RDPFileLocation );
                  loConn.Id = msCurrentConnection;
                  loConn.ReadRDPConfig();
                  mcoConnections.Add( msCurrentConnection, loConn );
               }

               if(CurrentConnection.Name.Equals( loSelectedNode.Text ) == false)
               {
                  Utility.showMessageBox( this, "Connection/tree name mismatch" );
               }

               if(CurrentConnection.Group.Equals( loSelectedNode.Parent.Name ) == false)
               {
                  Utility.showMessageBox( this, "Connection/tree group mismatch" );
               }

               setFormData();
            }
         }
         catch(Exception ex)
         {
            Utility.showMessageBox( this, "Error loading connection", ex );
         }
         finally
         {
            loTree.EndUpdate();
         }
      }

      private void treeComputers_DoubleClick( object sender, EventArgs e )
      {
         TreeView loTree = (TreeView)sender;
         if(loTree.SelectedNode == null || loTree.SelectedNode.Level == 0)
         {
            return;
         }
         
         if(CurrentConnection == null)
         {
            Utility.showMessageBox( this, "No connection is loaded" );
         }

         btnConnect_Click( sender, e );
      }

      private void treeComputers_ItemDrag( object sender, ItemDragEventArgs e )
      {
         DoDragDrop( e.Item, DragDropEffects.Move );
      }

      private void treeComputers_DragEnter( object sender, DragEventArgs e )
      {
         if( e.Data.GetDataPresent( "System.Windows.Forms.TreeNode", true ) ) 
         {
            e.Effect = DragDropEffects.Move;
         }
         else
         {
            e.Effect = DragDropEffects.None;
         }
      }

      private void treeComputers_DragDrop( object sender, DragEventArgs e )
      {
         if( e.Data.GetDataPresent( "System.Windows.Forms.TreeNode", false ) && this.NodeMap != "" )
         {
            TreeNode MovingNode = (TreeNode)e.Data.GetData( "System.Windows.Forms.TreeNode" );
            string[] NodeIndexes = this.NodeMap.Split( '|' );
            TreeNodeCollection InsertCollection = ((TreeView)sender).Nodes;
            for( int i = 0; i < NodeIndexes.Length - 1; i++ )
            {
               InsertCollection = InsertCollection[Int32.Parse( NodeIndexes[i] )].Nodes;
            }

            if( InsertCollection != null )
            {
               InsertCollection.Insert( Int32.Parse( NodeIndexes[NodeIndexes.Length - 1] ), (TreeNode)MovingNode.Clone() );
               ( (TreeView)sender ).SelectedNode = InsertCollection[Int32.Parse( NodeIndexes[NodeIndexes.Length - 1] )];
               MovingNode.Remove();
            }

            if(( (TreeView)sender ).SelectedNode.Parent != null)
            {
               String lsGroup = ( (TreeView)sender ).SelectedNode.Parent.Name;

               if(CurrentConnection.Group.Equals( lsGroup ) == false)
               {
                  CurrentConnection.Group = lsGroup;
                  CurrentConnection.WriteRDPConfig();
                  setFormData();
               }
            }
         }
      }

      /// <summary>
      /// All credit for this implementation goes to: Gabe Anguiano.
      /// Source: http://www.codeproject.com/cs/miscctrl/TreeViewReArr.asp
      /// </summary>
      /// <param name="sender">TreeView</param>
      /// <param name="e">Events</param>
      private void treeComputers_DragOver( object sender, DragEventArgs e )
      {
         TreeNode NodeOver = ( (TreeView)sender ).GetNodeAt( ( (TreeView)sender ).PointToClient( Cursor.Position ) );
         TreeNode NodeMoving = (TreeNode)e.Data.GetData( "System.Windows.Forms.TreeNode" );

         if( NodeOver == null )
            return;

         // Don't allow group nodes to be placed as childs of other groups
         // 
         if( cmbGroup.Items.IndexOf( NodeMoving.Name ) > -1 && 
             cmbGroup.Items.IndexOf( NodeOver.Name ) == -1 )
         {
            e.Effect = DragDropEffects.None;
            this.NodeMap = "";
            return;
         }
         else
         {
            e.Effect = DragDropEffects.Move;
         }

         // If NodeOver is a child then cancel
         //
         TreeNode tnParadox = NodeOver;
         while( tnParadox.Parent != null )
         {
            if( tnParadox.Parent == NodeMoving )
            {
               this.NodeMap = "";
               return;
            }

            tnParadox = tnParadox.Parent;
         }

         // A bit long, but to summarize, process the following code only if the nodeover is null
         // and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
         // to be the last node in the branch (so we can allow drag & drop below a parent branch)
         //
         if( NodeOver != NodeMoving || 
             ( NodeOver.Parent != null && NodeOver.Index == ( NodeOver.Parent.Nodes.Count - 1 ) ) )
         {
            int OffsetY = ( (TreeView)sender ).PointToClient( Cursor.Position ).Y - NodeOver.Bounds.Top;
            int NodeOverImageWidth = ( (TreeView)sender ).ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            Graphics g = ( (TreeView)sender ).CreateGraphics();          
            
            // Image index of 1 is the non-folder icon
            //
            if( NodeOver.ImageIndex == 1 )
            {
               // region Standard Node
               //
               if( OffsetY < ( NodeOver.Bounds.Height / 2 ) )
               {
                  // Store the placeholder info into a pipe delimited string
                  //
                  SetNewNodeMap( NodeOver, false );
                  if( SetMapsEqual() == true )
                     return;

                  this.clearPlaceholders();
                  this.DrawLeafTopPlaceholders( (TreeView)sender, NodeOver );
               }
               else
               {
                  // Allow drag drop to parent branches
                  //
                  TreeNode ParentDragDrop = null;

                  // If the node the mouse is over is the last node of the branch we should allow
                  // the ability to drop the "nodemoving" node BELOW the parent node
                  //
                  if( NodeOver.Parent != null && NodeOver.Index == ( NodeOver.Parent.Nodes.Count - 1 ) )
                  {
                     int XPos = ( (TreeView)sender ).PointToClient( Cursor.Position ).X;
                     if( XPos < NodeOver.Bounds.Left )
                     {
                        ParentDragDrop = NodeOver.Parent;

                        if( XPos < ( ParentDragDrop.Bounds.Left - ( (TreeView)sender ).ImageList.Images[ParentDragDrop.ImageIndex].Size.Width ) )
                        {
                           if( ParentDragDrop.Parent != null )
                              ParentDragDrop = ParentDragDrop.Parent;
                        }
                     }
                  }

                  // Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
                  //
                  SetNewNodeMap( ParentDragDrop != null ? ParentDragDrop : NodeOver, true );
                  if( SetMapsEqual() == true )
                     return;

                  this.clearPlaceholders();
                  this.DrawLeafBottomPlaceholders( (TreeView)sender, NodeOver, ParentDragDrop );
               }
            }
            else // ImageIndex == 0
            {
               // region Folder Node
               //
               if( OffsetY < ( NodeOver.Bounds.Height / 3 ) )
               {
                  // Store the placeholder info into a pipe delimited string
                  //
                  SetNewNodeMap( NodeOver, false );
                  if( SetMapsEqual() == true )
                     return;

                  this.clearPlaceholders();
                  this.DrawFolderTopPlaceholders( (TreeView)sender, NodeOver );
               }
               else if( ( NodeOver.Parent != null && NodeOver.Index == 0 ) && ( OffsetY > ( NodeOver.Bounds.Height - ( NodeOver.Bounds.Height / 3 ) ) ) )
               {
                  // Store the placeholder info into a pipe delimited string
                  //
                  SetNewNodeMap( NodeOver, true );
                  if( SetMapsEqual() == true )
                     return;

                  this.clearPlaceholders();
                  this.DrawFolderTopPlaceholders( (TreeView)sender, NodeOver );
               }
               else
               {
                  if( NodeOver.Nodes.Count > 0 )
                  {
                     NodeOver.Expand();
                     //this.Refresh();
                  }
                  else
                  {
                     // Prevent the node from being dragged onto itself
                     // 
                     if( NodeMoving == NodeOver )
                        return;

                     // Store the placeholder info into a pipe delimited string
                     //
                     SetNewNodeMap( NodeOver, false );
                     NewNodeMap = NewNodeMap.Insert( NewNodeMap.Length, "|0" );

                     if( SetMapsEqual() == true )
                        return;

                     this.clearPlaceholders();
                     this.DrawAddToFolderPlaceholder( (TreeView)sender, NodeOver );
                  }
               }
            }
         } // end drop allowed
      }

      #region TreeView Drag&Drop Helper Methods
      private void DrawLeafTopPlaceholders( TreeView poTree, TreeNode NodeOver )
      {
         Graphics g = poTree.CreateGraphics();

         int NodeOverImageWidth = poTree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
         int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
         int RightPos = poTree.Width - 4;

         Point[] LeftTriangle = new Point[5]
         {
		      new Point(LeftPos, NodeOver.Bounds.Top - 4),
		      new Point(LeftPos, NodeOver.Bounds.Top + 4),
		      new Point(LeftPos + 4, NodeOver.Bounds.Y),
		      new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
		      new Point(LeftPos, NodeOver.Bounds.Top - 5)
         };

         Point[] RightTriangle = new Point[5]
         {
            new Point(RightPos, NodeOver.Bounds.Top - 4),
            new Point(RightPos, NodeOver.Bounds.Top + 4),
            new Point(RightPos - 4, NodeOver.Bounds.Y),
            new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
            new Point(RightPos, NodeOver.Bounds.Top - 5)
         };

         g.FillPolygon( System.Drawing.Brushes.Black, LeftTriangle );
         g.FillPolygon( System.Drawing.Brushes.Black, RightTriangle );
         g.DrawLine( new System.Drawing.Pen( Color.Black, 2 ), 
            new Point( LeftPos, NodeOver.Bounds.Top ), new Point( RightPos, NodeOver.Bounds.Top ) );
      }

      private void DrawLeafBottomPlaceholders( TreeView poTree, TreeNode NodeOver, TreeNode ParentDragDrop )
      {
         Graphics g = poTree.CreateGraphics();

         int NodeOverImageWidth = poTree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

         // Once again, we are not dragging to node over, draw the placeholder using 
         // the ParentDragDrop bounds
         // 
         int LeftPos, RightPos;
         if( ParentDragDrop != null )
         {
            LeftPos = ParentDragDrop.Bounds.Left - ( poTree.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8 );
         }
         else
         {
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
         }
         RightPos = poTree.Width - 4;

         Point[] LeftTriangle = new Point[5]
         {
		      new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
		      new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
		      new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
		      new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
		      new Point(LeftPos, NodeOver.Bounds.Bottom - 5)
         };

         Point[] RightTriangle = new Point[5]
         {
			   new Point(RightPos, NodeOver.Bounds.Bottom - 4),
			   new Point(RightPos, NodeOver.Bounds.Bottom + 4),
			   new Point(RightPos - 4, NodeOver.Bounds.Bottom),
			   new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
			   new Point(RightPos, NodeOver.Bounds.Bottom - 5)
         };

         g.FillPolygon( System.Drawing.Brushes.Black, LeftTriangle );
         g.FillPolygon( System.Drawing.Brushes.Black, RightTriangle );
         g.DrawLine( new System.Drawing.Pen( Color.Black, 2 ), 
            new Point( LeftPos, NodeOver.Bounds.Bottom ), new Point( RightPos, NodeOver.Bounds.Bottom ) );
      }

      private void DrawFolderTopPlaceholders( TreeView poTree, TreeNode NodeOver )
      {
         Graphics g = poTree.CreateGraphics();
         int NodeOverImageWidth = poTree.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

         int LeftPos, RightPos;
         LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
         RightPos = poTree.Width - 4;

         Point[] LeftTriangle = new Point[5]
         {
		      new Point(LeftPos, NodeOver.Bounds.Top - 4),
		      new Point(LeftPos, NodeOver.Bounds.Top + 4),
		      new Point(LeftPos + 4, NodeOver.Bounds.Y),
		      new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
		      new Point(LeftPos, NodeOver.Bounds.Top - 5)
         };

         Point[] RightTriangle = new Point[5]
         {
			   new Point(RightPos, NodeOver.Bounds.Top - 4),
			   new Point(RightPos, NodeOver.Bounds.Top + 4),
			   new Point(RightPos - 4, NodeOver.Bounds.Y),
			   new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
			   new Point(RightPos, NodeOver.Bounds.Top - 5)
         };

         g.FillPolygon( System.Drawing.Brushes.Black, LeftTriangle );
         g.FillPolygon( System.Drawing.Brushes.Black, RightTriangle );
         g.DrawLine( new System.Drawing.Pen( Color.Black, 2 ), 
            new Point( LeftPos, NodeOver.Bounds.Top ), new Point( RightPos, NodeOver.Bounds.Top ) );
      }

      private void DrawAddToFolderPlaceholder( TreeView poTree, TreeNode NodeOver )
      {
         Graphics g = poTree.CreateGraphics();
         int RightPos = NodeOver.Bounds.Right + 6;
         Point[] RightTriangle = new Point[5]
         {
			   new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
			   new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
			   new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
			   new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
			   new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)
         };

         this.Refresh();
         g.FillPolygon( System.Drawing.Brushes.Black, RightTriangle );
      }

      private void SetNewNodeMap( TreeNode tnNode, bool boolBelowNode )
      {
         NewNodeMap.Length = 0;

         if( boolBelowNode )
         {
            NewNodeMap.Insert( 0, (int)tnNode.Index + 1 );
         }
         else
         {
            NewNodeMap.Insert( 0, (int)tnNode.Index );
         }

         TreeNode tnCurNode = tnNode;

         while( tnCurNode.Parent != null )
         {
            tnCurNode = tnCurNode.Parent;

            if( NewNodeMap.Length == 0 && boolBelowNode == true )
            {
               NewNodeMap.Insert( 0, ( tnCurNode.Index + 1 ) + "|" );
            }
            else
            {
               NewNodeMap.Insert( 0, tnCurNode.Index + "|" );
            }
         }
      }

      private bool SetMapsEqual()
      {
         if( this.NewNodeMap.ToString() == this.NodeMap )
         {
            return true;
         }
         else
         {
            this.NodeMap = this.NewNodeMap.ToString();
            return false;
         }
      }

      private void clearPlaceholders()
      {
         this.Refresh();
      }
      #endregion

      private void treeComputers_MouseDown( object sender, MouseEventArgs e )
      {
         //( (TreeView)sender ).SelectedNode = ( (TreeView)sender ).GetNodeAt( e.X, e.Y );
      }

      #endregion 

      private void setFormData()
      {
         if(CurrentConnection == null)
            return;

         mbFormDataIsUdating = true;
         try
         {
            FormDataDirty = false;

            enableSettingsForm( true );

            this.txtName.Text = CurrentConnection.Name;

            if(CurrentConnection.Group == null ||
               CurrentConnection.Group.Length == 0)
            {
               if(ComputersTreeView.SelectedNode != null &&
                   ComputersTreeView.SelectedNode.Level == 0)
               {
                  try
                  {
                     cmbGroup.SelectedIndex =
                        cmbGroup.Items.IndexOf( ComputersTreeView.SelectedNode.Name );
                  }
                  catch { }
               }
               else
               {
                  cmbGroup.SelectedIndex = -1;
                  cmbGroup.SelectedText = null;
                  cmbGroup.SelectedValue = null;
               }
            }
            else
            {
               if(cmbGroup.Items.IndexOf( CurrentConnection.Group ) == -1)
               {
                  cmbGroup.Items.Add( CurrentConnection.Group );
               }
               cmbGroup.SelectedIndex =
                  cmbGroup.Items.IndexOf( CurrentConnection.Group );
            }

            this.txtComputerHost.Text = CurrentConnection.Hostname;
            this.txtComputerPort.Text = "" + CurrentConnection.Port;

            this.txtUsername.Text = CurrentConnection.Username;
            this.txtDomain.Text = CurrentConnection.Domain;

            this.chkPromptCredentials.Checked = CurrentConnection.PromptCredentials;

            this.chkConnectLocalDrives.Checked = CurrentConnection.UseLocalDrives;
            this.chkConnectLocalPrinters.Checked = CurrentConnection.UseLocalPrinters;
            this.chkConnectToConsole.Checked = CurrentConnection.UseConsoleSession;
            this.chkMaximumGUIExperience.Checked = CurrentConnection.MaximumGUIExperience;

            chkFullScreen.Checked = CurrentConnection.FullScreen;
            chkSpan.Checked = CurrentConnection.Span;

            setSliderValueFromConnection();

            this.txtName.Focus();
         }
         finally
         {
            mbFormDataIsUdating = false;
         }
      }

      private void btnClone_Click( object sender, EventArgs e )
      {
         RDPConnection loConn = CurrentConnection.duplicateConnection();
         mcoConnections.Add( loConn.Id, loConn );
         msCurrentConnection = loConn.Id;
         setFormData();
      }

      private void threadConnectButton_DoWork( object sender, DoWorkEventArgs e )
      {
         Thread.Sleep( 1000 );
      }

      private void btnExport_Click( object sender, EventArgs e )
      {
         Process loProcess = new Process();
         loProcess.StartInfo.WorkingDirectory = RDPFileLocation;
         loProcess.StartInfo.FileName = "explorer.exe";
         loProcess.StartInfo.Arguments = "/root, \"" + RDPFileLocation + "\"";
         loProcess.Start();
         /*
         Export loExportDlg = new Export( this );
         loExportDlg.Owner = this;
         loExportDlg.ShowDialog();
         */
      }

      private void formDataChanged( object sender, EventArgs e )
      {
         if(mbFormDataIsUdating == false)
         {
            FormDataDirty = true;
         }
      }

      private void enableSettingsForm( Boolean pbEnable )
      {
         this.grpSettings.Enabled = true;
         if(pbEnable == false)
         {
            this.txtComputerHost.Text = "";
            this.txtComputerPort.Text = "";
            this.txtDomain.Text = "";
            this.txtName.Text = "";
            this.txtUsername.Text = "";
         }

         foreach(Control loControl in this.grpSettings.Controls)
         {
            loControl.Enabled = pbEnable;
         }
         this.cmbGroup.Enabled = true;
         this.lblGroup.Enabled = true;
         this.btnSave.Enabled = true;

         this.btnConnect.Enabled = pbEnable;
      }

      private void btnSettings_Click( object sender, EventArgs e )
      {
         Settings loSettingsDlg = new Settings( this );
         loSettingsDlg.Owner = this;
         loSettingsDlg.ShowDialog();
      }

      #region Update check

      private void lblToolStrip_Click( object sender, EventArgs e )
      {
         if(lblToolStrip.IsLink == false)
            return;
         
         try
         {
            String lsTmpFile = Path.GetTempPath() + "rdm-setup.exe";
            if( File.Exists( lsTmpFile ) )
            {
               File.Delete( lsTmpFile );
            }
            moWebClient.DownloadFileAsync( new Uri(lblToolStrip.ToolTipText), lsTmpFile );
         }
         catch( Exception ee )
         {
            Utility.showMessageBox( this, "Error downloading file", ee );
         }
      }

      private void threadCheckForUpdate_DoWork( object sender, DoWorkEventArgs e )
      {
         try
         {
            String lsResult = moWebClient.DownloadString( UPDATE_CHECK_URL +
               "?version=" + Application.ProductVersion );

            if("up_to_date".Equals( lsResult ) == false)
            {
               String[] lasResult = lsResult.Split( '|' );

               lblToolStrip.Text = "A new version (" +
                   lasResult[0] + ") is available. Click to download.";

               lblToolStrip.ToolTipText = lasResult[1];
            }
            else
            {
               lblToolStrip.Text = "You have the latest version.";
            }
         }
         catch(Exception ee)
         {
            lblToolStrip.Text = "Update check error: " + ee.Message;
         }
      }

      private void threadCheckForUpdate_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
      {
         if( lblToolStrip.Text.StartsWith( "A new version" ) == true)
         {
            lblToolStrip.IsLink = true;
         }
      }

      private void moWebClient_DownloadProgressChanged( object sender, DownloadProgressChangedEventArgs e )
      {
         lblToolStrip.IsLink = false;
         statusBar.Cursor = DefaultCursor;
         lblToolStrip.Text = "Downloading (" + e.ProgressPercentage + "% done).";
      }

      private void moWebClient_DownloadFileCompleted( object sender, AsyncCompletedEventArgs e )
      {
         String lsFilename = Path.GetTempPath() + "rdm-setup.exe";
         
         if(Utility.showMessageBox( this, "File downloaded to '" +
            lsFilename + "', install now?", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1
         ) == DialogResult.Yes)
         {
            System.Diagnostics.Process.Start( lsFilename );
            Application.Exit();
         }
         lblToolStrip.Text = "Ready.";
         lblToolStrip.IsLink = false;
         statusBar.Cursor = DefaultCursor;
      }

      #endregion

      private void treeComputers_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
      {
         //Console.WriteLine( e.ToString() );
      }

      private void txtComputerPort_KeyPress( object sender, KeyPressEventArgs e )
      {
         if(char.IsDigit( e.KeyChar ) == false)
         {
            e.Handled = true;
         }
      }

      private void btnDiscover_Click( object sender, EventArgs e )
      {
         DiscoveryForm loDiscoveryDlg = new DiscoveryForm( this );
         loDiscoveryDlg.Owner = this;
         loDiscoveryDlg.ShowDialog();
      }

      # region TrayIconMenu

      private void moTrayIcon_Click( object sender, EventArgs e )
      {
         // Use reflection to show the menu. This way it will show
         // the same behaviour as when a right click is used.
         // If ContextMenuStrip.Show is used, the menu will show in 
         // the taskbar. Also it will not show-up (X,Y location) at the
         // same location when a right click is used.
         //
         moTrayIcon.GetType().InvokeMember
         (
            "ShowContextMenu",
            System.Reflection.BindingFlags.InvokeMethod |
               System.Reflection.BindingFlags.Instance |
               System.Reflection.BindingFlags.NonPublic,
            null,
            moTrayIcon,
            null
         );
      }

      public void ShowApplication()
      {
         this.ShowInTaskbar = true;

         if(this.WindowState == FormWindowState.Minimized)
         {
            try
            {
               this.WindowState = moStateBeforeMinimize;
            }
            catch
            {
               this.WindowState = FormWindowState.Normal;
            }
         }

         this.Show();
         this.BringToFront();
      }

      private void moTrayIcon_DoubleClick( object sender, EventArgs e )
      {
         ShowApplication();
      }

      private void moTrayIcon_Exit_Click( object sender, EventArgs e )
      {
         this.Close();
         Application.Exit();
      }

      private void moTrayIcon_ComputerItem_Click( object sender, EventArgs e )
      {
         String lsId = (String)( (ToolStripMenuItem)sender ).Tag;

         TreeNode[] laoNodes = treeComputers.Nodes.Find( lsId, true );
         foreach(TreeNode loNode in laoNodes)
         {
            treeComputers.SelectedNode = loNode;
         }
         this.btnConnect_Click( sender, e );
      }

      public void RenderTrayIconMenu()
      {
         // skip show, exit items
         for(int i = moTrayIconMenu.Items.Count - 3; i >= 0; i--)
         {
            moTrayIconMenu.Items.RemoveAt( i );
         }

         moTrayIconMenu.Items.Insert( 0, new ToolStripSeparator() );

         for( int t = ComputersTreeView.Nodes.Count-1; t >= 0; t--)
         {
            TreeNode loNode = ComputersTreeView.Nodes[t];

            if(loNode.Level == 0)
            {
               ToolStripMenuItem loGroup = new ToolStripMenuItem( 
                  loNode.Name, mageList.Images[0] );

               loGroup.Enabled = true;
               loGroup.Font = new Font( loGroup.Font, FontStyle.Bold );

               ContextMenuStrip loSubMenu = new ContextMenuStrip();
               loSubMenu.ShowCheckMargin = false;
               loSubMenu.ShowImageMargin = false;
               loSubMenu.AutoSize = true;

               int liItems = 0;
               foreach(TreeNode loComp in loNode.Nodes)
               {
                  ToolStripMenuItem loCompMenu = new ToolStripMenuItem(
                     loComp.Text, mageList.Images[1] );

                  loCompMenu.Tag = loComp.Name;
                  loCompMenu.Click +=
                     new System.EventHandler( moTrayIcon_ComputerItem_Click );

                  if(UseSubMenusInTrayMenu == false)
                  {
                     loCompMenu.DisplayStyle =
                        ToolStripItemDisplayStyle.Text;

                     moTrayIconMenu.Items.Insert
                     (
                        liItems,
                        loCompMenu
                     );
                  }
                  else
                  {
                     loCompMenu.DisplayStyle =
                        ToolStripItemDisplayStyle.ImageAndText;

                     loSubMenu.Items.Insert
                     (
                        liItems,
                        loCompMenu
                     );
                  }

                  liItems++;
               }

               moTrayIconMenu.Items.Insert
               (
                  0,
                  loGroup
               );

               if(UseSubMenusInTrayMenu == true)
                  loGroup.DropDown = loSubMenu;
            }
         }
      }
      # endregion

      private void btnEditRDP_Click( object sender, EventArgs e )
      {
         if(FormDataDirty == true)
         {
            this.btnSave_Click( this, null );
         }

         Utility.startRDClientForEdit(this.CurrentConnection);
      }
   }
}