namespace RemoteDesktopManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System .ComponentModel .IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {  
           if( disposing )
           {
              if( components != null )
              {
                 components.Dispose();
              }
              if( moTrayIcon != null )
              {
                 moTrayIcon.Dispose();
              }
           }
           base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           this.components = new System.ComponentModel.Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
           this.mageList = new System.Windows.Forms.ImageList( this.components );
           this.lblToolStrip = new System.Windows.Forms.ToolStripStatusLabel();
           this.statusBar = new System.Windows.Forms.StatusStrip();
           this.treeComputers = new System.Windows.Forms.TreeView();
           this.pnlConnectButton = new System.Windows.Forms.Panel();
           this.lblAbout = new System.Windows.Forms.LinkLabel();
           this.btnConnect = new System.Windows.Forms.Button();
           this.toolTip = new System.Windows.Forms.ToolTip( this.components );
           this.btnSave = new System.Windows.Forms.Button();
           this.btnDelete = new System.Windows.Forms.Button();
           this.btnDeleteGroup = new System.Windows.Forms.Button();
           this.btnNew = new System.Windows.Forms.Button();
           this.btnClone = new System.Windows.Forms.Button();
           this.btnExport = new System.Windows.Forms.Button();
           this.btnSettings = new System.Windows.Forms.Button();
           this.chkMaximumGUIExperience = new System.Windows.Forms.CheckBox();
           this.chkPromptCredentials = new System.Windows.Forms.CheckBox();
           this.btnDiscover = new System.Windows.Forms.Button();
           this.chkSpan = new System.Windows.Forms.CheckBox();
           this.lblUsername = new System.Windows.Forms.Label();
           this.txtUsername = new System.Windows.Forms.TextBox();
           this.sliderResolution = new System.Windows.Forms.TrackBar();
           this.cmbGroup = new System.Windows.Forms.ComboBox();
           this.lblResolution = new System.Windows.Forms.Label();
           this.lblScreenSize = new System.Windows.Forms.Label();
           this.lblGroup = new System.Windows.Forms.Label();
           this.lblComputer = new System.Windows.Forms.Label();
           this.txtComputerHost = new System.Windows.Forms.TextBox();
           this.txtComputerPort = new System.Windows.Forms.TextBox();
           this.lblHostColon = new System.Windows.Forms.Label();
           this.lblName = new System.Windows.Forms.Label();
           this.txtName = new System.Windows.Forms.TextBox();
           this.lblDomain = new System.Windows.Forms.Label();
           this.txtDomain = new System.Windows.Forms.TextBox();
           this.chkFullScreen = new System.Windows.Forms.CheckBox();
           this.chkConnectToConsole = new System.Windows.Forms.CheckBox();
           this.chkConnectLocalDrives = new System.Windows.Forms.CheckBox();
           this.grpSettings = new System.Windows.Forms.GroupBox();
           this.chkConnectLocalPrinters = new System.Windows.Forms.CheckBox();
           this.pnlNewButton = new System.Windows.Forms.Panel();
           this.threadConnectButton = new System.ComponentModel.BackgroundWorker();
           this.threadCheckForUpdate = new System.ComponentModel.BackgroundWorker();
           this.btnEditRDP = new System.Windows.Forms.Button();
           this.statusBar.SuspendLayout();
           this.pnlConnectButton.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.sliderResolution)).BeginInit();
           this.grpSettings.SuspendLayout();
           this.pnlNewButton.SuspendLayout();
           this.SuspendLayout();
           // 
           // mageList
           // 
           this.mageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "mageList.ImageStream" )));
           this.mageList.TransparentColor = System.Drawing.Color.Transparent;
           this.mageList.Images.SetKeyName( 0, "chart_organisation.ico" );
           this.mageList.Images.SetKeyName( 1, "computer.ico" );
           this.mageList.Images.SetKeyName( 2, "computer_add.ico" );
           this.mageList.Images.SetKeyName( 3, "disk.ico" );
           this.mageList.Images.SetKeyName( 4, "delete.ico" );
           this.mageList.Images.SetKeyName( 5, "connect.ico" );
           this.mageList.Images.SetKeyName( 6, "key_delete.ico" );
           this.mageList.Images.SetKeyName( 7, "chart_organisation_delete.ico" );
           this.mageList.Images.SetKeyName( 8, "disk_multiple.ico" );
           this.mageList.Images.SetKeyName( 9, "cog_edit.ico" );
           this.mageList.Images.SetKeyName( 10, "world_add.ico" );
           this.mageList.Images.SetKeyName( 11, "application_edit.png" );
           // 
           // lblToolStrip
           // 
           this.lblToolStrip.AutoSize = false;
           this.lblToolStrip.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
           this.lblToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
           this.lblToolStrip.Margin = new System.Windows.Forms.Padding( 3, 3, 0, 2 );
           this.lblToolStrip.Name = "lblToolStrip";
           this.lblToolStrip.Size = new System.Drawing.Size( 564, 17 );
           this.lblToolStrip.Spring = true;
           this.lblToolStrip.Text = "Ready.";
           this.lblToolStrip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.lblToolStrip.Click += new System.EventHandler( this.lblToolStrip_Click );
           // 
           // statusBar
           // 
           this.statusBar.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.lblToolStrip} );
           this.statusBar.Location = new System.Drawing.Point( 0, 379 );
           this.statusBar.Name = "statusBar";
           this.statusBar.Size = new System.Drawing.Size( 582, 22 );
           this.statusBar.TabIndex = 2;
           this.statusBar.Text = "statusStrip1";
           // 
           // treeComputers
           // 
           this.treeComputers.AllowDrop = true;
           this.treeComputers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
           this.treeComputers.HideSelection = false;
           this.treeComputers.ImageIndex = 0;
           this.treeComputers.ImageList = this.mageList;
           this.treeComputers.Location = new System.Drawing.Point( 6, 7 );
           this.treeComputers.MinimumSize = new System.Drawing.Size( 200, 350 );
           this.treeComputers.Name = "treeComputers";
           this.treeComputers.SelectedImageIndex = 0;
           this.treeComputers.Size = new System.Drawing.Size( 221, 366 );
           this.treeComputers.TabIndex = 2;
           this.treeComputers.DoubleClick += new System.EventHandler( this.treeComputers_DoubleClick );
           this.treeComputers.DragDrop += new System.Windows.Forms.DragEventHandler( this.treeComputers_DragDrop );
           this.treeComputers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.treeComputers_AfterSelect );
           this.treeComputers.MouseDown += new System.Windows.Forms.MouseEventHandler( this.treeComputers_MouseDown );
           this.treeComputers.DragEnter += new System.Windows.Forms.DragEventHandler( this.treeComputers_DragEnter );
           this.treeComputers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler( this.treeComputers_NodeMouseClick );
           this.treeComputers.ItemDrag += new System.Windows.Forms.ItemDragEventHandler( this.treeComputers_ItemDrag );
           this.treeComputers.DragOver += new System.Windows.Forms.DragEventHandler( this.treeComputers_DragOver );
           // 
           // pnlConnectButton
           // 
           this.pnlConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
           this.pnlConnectButton.Controls.Add( this.lblAbout );
           this.pnlConnectButton.Controls.Add( this.btnConnect );
           this.pnlConnectButton.Location = new System.Drawing.Point( 232, 346 );
           this.pnlConnectButton.Name = "pnlConnectButton";
           this.pnlConnectButton.Size = new System.Drawing.Size( 343, 28 );
           this.pnlConnectButton.TabIndex = 26;
           // 
           // lblAbout
           // 
           this.lblAbout.Location = new System.Drawing.Point( 130, 4 );
           this.lblAbout.Name = "lblAbout";
           this.lblAbout.Size = new System.Drawing.Size( 215, 19 );
           this.lblAbout.TabIndex = 22;
           this.lblAbout.TabStop = true;
           this.lblAbout.Text = "set at startup";
           this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           this.lblAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.lblAbout_LinkClicked );
           // 
           // btnConnect
           // 
           this.btnConnect.Enabled = false;
           this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnConnect.ImageIndex = 5;
           this.btnConnect.ImageList = this.mageList;
           this.btnConnect.Location = new System.Drawing.Point( 2, 1 );
           this.btnConnect.Name = "btnConnect";
           this.btnConnect.Size = new System.Drawing.Size( 25, 25 );
           this.btnConnect.TabIndex = 21;
           this.btnConnect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnConnect, "Connect to remote computer" );
           this.btnConnect.UseVisualStyleBackColor = true;
           this.btnConnect.Click += new System.EventHandler( this.btnConnect_Click );
           // 
           // btnSave
           // 
           this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnSave.ImageIndex = 3;
           this.btnSave.ImageList = this.mageList;
           this.btnSave.Location = new System.Drawing.Point( 245, 270 );
           this.btnSave.Name = "btnSave";
           this.btnSave.Size = new System.Drawing.Size( 25, 25 );
           this.btnSave.TabIndex = 15;
           this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnSave, "Save connection settings" );
           this.btnSave.UseVisualStyleBackColor = true;
           this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
           // 
           // btnDelete
           // 
           this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnDelete.ImageIndex = 4;
           this.btnDelete.ImageList = this.mageList;
           this.btnDelete.Location = new System.Drawing.Point( 307, 270 );
           this.btnDelete.Name = "btnDelete";
           this.btnDelete.Size = new System.Drawing.Size( 25, 25 );
           this.btnDelete.TabIndex = 16;
           this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnDelete, "Delete connection" );
           this.btnDelete.UseVisualStyleBackColor = true;
           this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
           // 
           // btnDeleteGroup
           // 
           this.btnDeleteGroup.BackColor = System.Drawing.SystemColors.Control;
           this.btnDeleteGroup.FlatAppearance.BorderSize = 0;
           this.btnDeleteGroup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
           this.btnDeleteGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnDeleteGroup.ForeColor = System.Drawing.SystemColors.ControlText;
           this.btnDeleteGroup.ImageIndex = 7;
           this.btnDeleteGroup.ImageList = this.mageList;
           this.btnDeleteGroup.Location = new System.Drawing.Point( 276, 2 );
           this.btnDeleteGroup.Name = "btnDeleteGroup";
           this.btnDeleteGroup.Size = new System.Drawing.Size( 25, 25 );
           this.btnDeleteGroup.TabIndex = 25;
           this.toolTip.SetToolTip( this.btnDeleteGroup, "Delete group..." );
           this.btnDeleteGroup.UseVisualStyleBackColor = false;
           this.btnDeleteGroup.Click += new System.EventHandler( this.btnDeleteGroup_Click );
           // 
           // btnNew
           // 
           this.btnNew.FlatAppearance.BorderSize = 0;
           this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnNew.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnNew.ImageIndex = 2;
           this.btnNew.ImageList = this.mageList;
           this.btnNew.Location = new System.Drawing.Point( 2, 2 );
           this.btnNew.Name = "btnNew";
           this.btnNew.Size = new System.Drawing.Size( 25, 25 );
           this.btnNew.TabIndex = 19;
           this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnNew, "Add new connection" );
           this.btnNew.UseVisualStyleBackColor = true;
           this.btnNew.Click += new System.EventHandler( this.btnNew_Click );
           // 
           // btnClone
           // 
           this.btnClone.FlatAppearance.BorderSize = 0;
           this.btnClone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnClone.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnClone.ImageIndex = 2;
           this.btnClone.ImageList = this.mageList;
           this.btnClone.Location = new System.Drawing.Point( 276, 270 );
           this.btnClone.Name = "btnClone";
           this.btnClone.Size = new System.Drawing.Size( 25, 25 );
           this.btnClone.TabIndex = 21;
           this.btnClone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnClone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnClone, "Clone current connection" );
           this.btnClone.UseVisualStyleBackColor = true;
           this.btnClone.Click += new System.EventHandler( this.btnClone_Click );
           // 
           // btnExport
           // 
           this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnExport.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnExport.ImageIndex = 8;
           this.btnExport.ImageList = this.mageList;
           this.btnExport.Location = new System.Drawing.Point( 245, 2 );
           this.btnExport.Name = "btnExport";
           this.btnExport.Size = new System.Drawing.Size( 25, 25 );
           this.btnExport.TabIndex = 26;
           this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnExport, "Export connections..." );
           this.btnExport.UseVisualStyleBackColor = true;
           this.btnExport.Click += new System.EventHandler( this.btnExport_Click );
           // 
           // btnSettings
           // 
           this.btnSettings.BackColor = System.Drawing.SystemColors.Control;
           this.btnSettings.FlatAppearance.BorderSize = 0;
           this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
           this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnSettings.ForeColor = System.Drawing.SystemColors.ControlText;
           this.btnSettings.ImageIndex = 9;
           this.btnSettings.ImageList = this.mageList;
           this.btnSettings.Location = new System.Drawing.Point( 307, 2 );
           this.btnSettings.Name = "btnSettings";
           this.btnSettings.Size = new System.Drawing.Size( 25, 25 );
           this.btnSettings.TabIndex = 27;
           this.toolTip.SetToolTip( this.btnSettings, "Change settings..." );
           this.btnSettings.UseVisualStyleBackColor = false;
           this.btnSettings.Click += new System.EventHandler( this.btnSettings_Click );
           // 
           // chkMaximumGUIExperience
           // 
           this.chkMaximumGUIExperience.AutoSize = true;
           this.chkMaximumGUIExperience.Location = new System.Drawing.Point( 70, 250 );
           this.chkMaximumGUIExperience.Name = "chkMaximumGUIExperience";
           this.chkMaximumGUIExperience.Size = new System.Drawing.Size( 148, 17 );
           this.chkMaximumGUIExperience.TabIndex = 23;
           this.chkMaximumGUIExperience.Text = "Maximum GUI Experience";
           this.toolTip.SetToolTip( this.chkMaximumGUIExperience, "Wallpaper, FullWindowDrag, MenuAnimations, Themes, BitmapCache" );
           this.chkMaximumGUIExperience.UseVisualStyleBackColor = true;
           this.chkMaximumGUIExperience.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // chkPromptCredentials
           // 
           this.chkPromptCredentials.AutoSize = true;
           this.chkPromptCredentials.Location = new System.Drawing.Point( 10, 278 );
           this.chkPromptCredentials.Name = "chkPromptCredentials";
           this.chkPromptCredentials.Size = new System.Drawing.Size( 186, 17 );
           this.chkPromptCredentials.TabIndex = 24;
           this.chkPromptCredentials.Text = "Prompt for Credentials on connect";
           this.toolTip.SetToolTip( this.chkPromptCredentials, "Applies to RDP 6.0 client" );
           this.chkPromptCredentials.UseVisualStyleBackColor = true;
           this.chkPromptCredentials.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // btnDiscover
           // 
           this.btnDiscover.FlatAppearance.BorderSize = 0;
           this.btnDiscover.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnDiscover.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnDiscover.ImageIndex = 10;
           this.btnDiscover.ImageList = this.mageList;
           this.btnDiscover.Location = new System.Drawing.Point( 33, 2 );
           this.btnDiscover.Name = "btnDiscover";
           this.btnDiscover.Size = new System.Drawing.Size( 25, 25 );
           this.btnDiscover.TabIndex = 28;
           this.btnDiscover.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnDiscover.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnDiscover, "Discover LAN for hosts..." );
           this.btnDiscover.UseVisualStyleBackColor = true;
           this.btnDiscover.Click += new System.EventHandler( this.btnDiscover_Click );
           // 
           // chkSpan
           // 
           this.chkSpan.AutoSize = true;
           this.chkSpan.Location = new System.Drawing.Point( 259, 180 );
           this.chkSpan.Name = "chkSpan";
           this.chkSpan.Size = new System.Drawing.Size( 51, 17 );
           this.chkSpan.TabIndex = 25;
           this.chkSpan.Text = "Span";
           this.toolTip.SetToolTip( this.chkSpan, "Span screen for multi monitor systems" );
           this.chkSpan.UseVisualStyleBackColor = true;
           this.chkSpan.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // lblUsername
           // 
           this.lblUsername.AutoSize = true;
           this.lblUsername.Location = new System.Drawing.Point( 11, 106 );
           this.lblUsername.Name = "lblUsername";
           this.lblUsername.Size = new System.Drawing.Size( 58, 13 );
           this.lblUsername.TabIndex = 0;
           this.lblUsername.Text = "Username:";
           // 
           // txtUsername
           // 
           this.txtUsername.Location = new System.Drawing.Point( 70, 103 );
           this.txtUsername.Name = "txtUsername";
           this.txtUsername.Size = new System.Drawing.Size( 260, 20 );
           this.txtUsername.TabIndex = 7;
           this.txtUsername.TextChanged += new System.EventHandler( this.formDataChanged );
           // 
           // sliderResolution
           // 
           this.sliderResolution.AutoSize = false;
           this.sliderResolution.LargeChange = 1;
           this.sliderResolution.Location = new System.Drawing.Point( 65, 157 );
           this.sliderResolution.Maximum = 3;
           this.sliderResolution.Name = "sliderResolution";
           this.sliderResolution.Size = new System.Drawing.Size( 78, 24 );
           this.sliderResolution.TabIndex = 11;
           this.sliderResolution.Value = 1;
           this.sliderResolution.ValueChanged += new System.EventHandler( this.sliderResolution_ValueChanged );
           // 
           // cmbGroup
           // 
           this.cmbGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
           this.cmbGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
           this.cmbGroup.FormattingEnabled = true;
           this.cmbGroup.Location = new System.Drawing.Point( 70, 46 );
           this.cmbGroup.Name = "cmbGroup";
           this.cmbGroup.Size = new System.Drawing.Size( 260, 21 );
           this.cmbGroup.TabIndex = 4;
           this.cmbGroup.SelectedIndexChanged += new System.EventHandler( this.formDataChanged );
           this.cmbGroup.KeyUp += new System.Windows.Forms.KeyEventHandler( this.cmbGroup_KeyUp );
           this.cmbGroup.TextChanged += new System.EventHandler( this.formDataChanged );
           // 
           // lblResolution
           // 
           this.lblResolution.Location = new System.Drawing.Point( 143, 159 );
           this.lblResolution.Name = "lblResolution";
           this.lblResolution.Size = new System.Drawing.Size( 60, 13 );
           this.lblResolution.TabIndex = 3;
           this.lblResolution.Text = "1024x768";
           this.lblResolution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           // 
           // lblScreenSize
           // 
           this.lblScreenSize.AutoSize = true;
           this.lblScreenSize.Location = new System.Drawing.Point( 7, 159 );
           this.lblScreenSize.Name = "lblScreenSize";
           this.lblScreenSize.Size = new System.Drawing.Size( 62, 13 );
           this.lblScreenSize.TabIndex = 4;
           this.lblScreenSize.Text = "Screensize:";
           // 
           // lblGroup
           // 
           this.lblGroup.AutoSize = true;
           this.lblGroup.Location = new System.Drawing.Point( 30, 49 );
           this.lblGroup.Name = "lblGroup";
           this.lblGroup.Size = new System.Drawing.Size( 39, 13 );
           this.lblGroup.TabIndex = 6;
           this.lblGroup.Text = "Group:";
           // 
           // lblComputer
           // 
           this.lblComputer.AutoSize = true;
           this.lblComputer.Location = new System.Drawing.Point( 14, 78 );
           this.lblComputer.Name = "lblComputer";
           this.lblComputer.Size = new System.Drawing.Size( 55, 13 );
           this.lblComputer.TabIndex = 8;
           this.lblComputer.Text = "Computer:";
           // 
           // txtComputerHost
           // 
           this.txtComputerHost.Location = new System.Drawing.Point( 70, 75 );
           this.txtComputerHost.Name = "txtComputerHost";
           this.txtComputerHost.Size = new System.Drawing.Size( 200, 20 );
           this.txtComputerHost.TabIndex = 5;
           this.txtComputerHost.TextChanged += new System.EventHandler( this.formDataChanged );
           // 
           // txtComputerPort
           // 
           this.txtComputerPort.Location = new System.Drawing.Point( 281, 75 );
           this.txtComputerPort.Name = "txtComputerPort";
           this.txtComputerPort.Size = new System.Drawing.Size( 49, 20 );
           this.txtComputerPort.TabIndex = 6;
           this.txtComputerPort.Text = "3389";
           this.txtComputerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
           this.txtComputerPort.TextChanged += new System.EventHandler( this.formDataChanged );
           this.txtComputerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler( this.txtComputerPort_KeyPress );
           // 
           // lblHostColon
           // 
           this.lblHostColon.AutoSize = true;
           this.lblHostColon.Location = new System.Drawing.Point( 271, 78 );
           this.lblHostColon.Name = "lblHostColon";
           this.lblHostColon.Size = new System.Drawing.Size( 10, 13 );
           this.lblHostColon.TabIndex = 11;
           this.lblHostColon.Text = ":";
           // 
           // lblName
           // 
           this.lblName.AutoSize = true;
           this.lblName.Location = new System.Drawing.Point( 31, 22 );
           this.lblName.Name = "lblName";
           this.lblName.Size = new System.Drawing.Size( 38, 13 );
           this.lblName.TabIndex = 12;
           this.lblName.Text = "Name:";
           // 
           // txtName
           // 
           this.txtName.Location = new System.Drawing.Point( 70, 18 );
           this.txtName.Name = "txtName";
           this.txtName.Size = new System.Drawing.Size( 260, 20 );
           this.txtName.TabIndex = 3;
           this.txtName.TextChanged += new System.EventHandler( this.formDataChanged );
           // 
           // lblDomain
           // 
           this.lblDomain.AutoSize = true;
           this.lblDomain.Location = new System.Drawing.Point( 23, 133 );
           this.lblDomain.Name = "lblDomain";
           this.lblDomain.Size = new System.Drawing.Size( 46, 13 );
           this.lblDomain.TabIndex = 14;
           this.lblDomain.Text = "Domain:";
           // 
           // txtDomain
           // 
           this.txtDomain.Location = new System.Drawing.Point( 70, 131 );
           this.txtDomain.Name = "txtDomain";
           this.txtDomain.Size = new System.Drawing.Size( 260, 20 );
           this.txtDomain.TabIndex = 10;
           this.txtDomain.TextChanged += new System.EventHandler( this.formDataChanged );
           // 
           // chkFullScreen
           // 
           this.chkFullScreen.AutoSize = true;
           this.chkFullScreen.Location = new System.Drawing.Point( 259, 158 );
           this.chkFullScreen.Name = "chkFullScreen";
           this.chkFullScreen.Size = new System.Drawing.Size( 74, 17 );
           this.chkFullScreen.TabIndex = 12;
           this.chkFullScreen.Text = "Fullscreen";
           this.chkFullScreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           this.chkFullScreen.UseVisualStyleBackColor = true;
           this.chkFullScreen.CheckedChanged += new System.EventHandler( this.chkFullScreen_CheckedChanged );
           // 
           // chkConnectToConsole
           // 
           this.chkConnectToConsole.AutoSize = true;
           this.chkConnectToConsole.Location = new System.Drawing.Point( 70, 187 );
           this.chkConnectToConsole.Name = "chkConnectToConsole";
           this.chkConnectToConsole.Size = new System.Drawing.Size( 156, 17 );
           this.chkConnectToConsole.TabIndex = 13;
           this.chkConnectToConsole.Text = "Connect to console session";
           this.chkConnectToConsole.UseVisualStyleBackColor = true;
           this.chkConnectToConsole.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // chkConnectLocalDrives
           // 
           this.chkConnectLocalDrives.AutoSize = true;
           this.chkConnectLocalDrives.Location = new System.Drawing.Point( 70, 208 );
           this.chkConnectLocalDrives.Name = "chkConnectLocalDrives";
           this.chkConnectLocalDrives.Size = new System.Drawing.Size( 122, 17 );
           this.chkConnectLocalDrives.TabIndex = 14;
           this.chkConnectLocalDrives.Text = "Connect local drives";
           this.chkConnectLocalDrives.UseVisualStyleBackColor = true;
           this.chkConnectLocalDrives.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // grpSettings
           // 
           this.grpSettings.Anchor = System.Windows.Forms.AnchorStyles.Right;
           this.grpSettings.Controls.Add( this.btnEditRDP );
           this.grpSettings.Controls.Add( this.chkSpan );
           this.grpSettings.Controls.Add( this.chkPromptCredentials );
           this.grpSettings.Controls.Add( this.chkMaximumGUIExperience );
           this.grpSettings.Controls.Add( this.chkConnectLocalPrinters );
           this.grpSettings.Controls.Add( this.btnClone );
           this.grpSettings.Controls.Add( this.btnDelete );
           this.grpSettings.Controls.Add( this.chkConnectLocalDrives );
           this.grpSettings.Controls.Add( this.chkConnectToConsole );
           this.grpSettings.Controls.Add( this.chkFullScreen );
           this.grpSettings.Controls.Add( this.txtDomain );
           this.grpSettings.Controls.Add( this.lblDomain );
           this.grpSettings.Controls.Add( this.txtName );
           this.grpSettings.Controls.Add( this.lblName );
           this.grpSettings.Controls.Add( this.lblHostColon );
           this.grpSettings.Controls.Add( this.txtComputerPort );
           this.grpSettings.Controls.Add( this.txtComputerHost );
           this.grpSettings.Controls.Add( this.lblComputer );
           this.grpSettings.Controls.Add( this.btnSave );
           this.grpSettings.Controls.Add( this.lblGroup );
           this.grpSettings.Controls.Add( this.lblScreenSize );
           this.grpSettings.Controls.Add( this.lblResolution );
           this.grpSettings.Controls.Add( this.cmbGroup );
           this.grpSettings.Controls.Add( this.sliderResolution );
           this.grpSettings.Controls.Add( this.txtUsername );
           this.grpSettings.Controls.Add( this.lblUsername );
           this.grpSettings.Enabled = false;
           this.grpSettings.Location = new System.Drawing.Point( 234, 38 );
           this.grpSettings.Name = "grpSettings";
           this.grpSettings.Size = new System.Drawing.Size( 341, 303 );
           this.grpSettings.TabIndex = 27;
           this.grpSettings.TabStop = false;
           this.grpSettings.Text = "Settings:";
           // 
           // chkConnectLocalPrinters
           // 
           this.chkConnectLocalPrinters.AutoSize = true;
           this.chkConnectLocalPrinters.Location = new System.Drawing.Point( 70, 229 );
           this.chkConnectLocalPrinters.Name = "chkConnectLocalPrinters";
           this.chkConnectLocalPrinters.Size = new System.Drawing.Size( 128, 17 );
           this.chkConnectLocalPrinters.TabIndex = 22;
           this.chkConnectLocalPrinters.Text = "Connect local printers";
           this.chkConnectLocalPrinters.UseVisualStyleBackColor = true;
           this.chkConnectLocalPrinters.CheckedChanged += new System.EventHandler( this.formDataChanged );
           // 
           // pnlNewButton
           // 
           this.pnlNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.pnlNewButton.Controls.Add( this.btnDiscover );
           this.pnlNewButton.Controls.Add( this.btnSettings );
           this.pnlNewButton.Controls.Add( this.btnExport );
           this.pnlNewButton.Controls.Add( this.btnDeleteGroup );
           this.pnlNewButton.Controls.Add( this.btnNew );
           this.pnlNewButton.Location = new System.Drawing.Point( 232, 6 );
           this.pnlNewButton.Name = "pnlNewButton";
           this.pnlNewButton.Size = new System.Drawing.Size( 343, 29 );
           this.pnlNewButton.TabIndex = 25;
           // 
           // threadConnectButton
           // 
           this.threadConnectButton.DoWork += new System.ComponentModel.DoWorkEventHandler( this.threadConnectButton_DoWork );
           // 
           // threadCheckForUpdate
           // 
           this.threadCheckForUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler( this.threadCheckForUpdate_DoWork );
           this.threadCheckForUpdate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.threadCheckForUpdate_RunWorkerCompleted );
           // 
           // btnEditRDP
           // 
           this.btnEditRDP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.btnEditRDP.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
           this.btnEditRDP.ImageIndex = 11;
           this.btnEditRDP.ImageList = this.mageList;
           this.btnEditRDP.Location = new System.Drawing.Point( 307, 238 );
           this.btnEditRDP.Name = "btnEditRDP";
           this.btnEditRDP.Size = new System.Drawing.Size( 25, 25 );
           this.btnEditRDP.TabIndex = 26;
           this.btnEditRDP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
           this.btnEditRDP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
           this.toolTip.SetToolTip( this.btnEditRDP, "Directly edit RDP file" );
           this.btnEditRDP.UseVisualStyleBackColor = true;
           this.btnEditRDP.Click += new System.EventHandler( this.btnEditRDP_Click );
           // 
           // MainForm
           // 
           this.AllowDrop = true;
           this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size( 582, 401 );
           this.Controls.Add( this.grpSettings );
           this.Controls.Add( this.pnlConnectButton );
           this.Controls.Add( this.pnlNewButton );
           this.Controls.Add( this.treeComputers );
           this.Controls.Add( this.statusBar );
           this.DoubleBuffered = true;
           this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
           this.MinimumSize = new System.Drawing.Size( 590, 435 );
           this.Name = "MainForm";
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
           this.Text = "RemoteDesktopManager";
           this.Load += new System.EventHandler( this.MainForm_Load );
           this.DragDrop += new System.Windows.Forms.DragEventHandler( this.MainForm_DragDrop );
           this.DragEnter += new System.Windows.Forms.DragEventHandler( this.MainForm_DragEnter );
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.MainForm_FormClosing );
           this.Resize += new System.EventHandler( this.MainForm_Resize );
           this.statusBar.ResumeLayout( false );
           this.statusBar.PerformLayout();
           this.pnlConnectButton.ResumeLayout( false );
           ((System.ComponentModel.ISupportInitialize)(this.sliderResolution)).EndInit();
           this.grpSettings.ResumeLayout( false );
           this.grpSettings.PerformLayout();
           this.pnlNewButton.ResumeLayout( false );
           this.ResumeLayout( false );
           this.PerformLayout();

        }

        #endregion

       private System.Windows.Forms.ImageList mageList;
       private System.Windows.Forms.ToolStripStatusLabel lblToolStrip;
       private System.Windows.Forms.StatusStrip statusBar;
       private System.Windows.Forms.TreeView treeComputers;
       private System.Windows.Forms.Panel pnlConnectButton;
       private System.Windows.Forms.LinkLabel lblAbout;
       private System.Windows.Forms.Button btnConnect;
       private System.Windows.Forms.ToolTip toolTip;
       private System.Windows.Forms.Label lblUsername;
       private System.Windows.Forms.TextBox txtUsername;
       private System.Windows.Forms.TrackBar sliderResolution;
       private System.Windows.Forms.ComboBox cmbGroup;
       private System.Windows.Forms.Label lblResolution;
       private System.Windows.Forms.Label lblScreenSize;
       private System.Windows.Forms.Label lblGroup;
       private System.Windows.Forms.Button btnSave;
       private System.Windows.Forms.Label lblComputer;
       private System.Windows.Forms.TextBox txtComputerHost;
       private System.Windows.Forms.TextBox txtComputerPort;
       private System.Windows.Forms.Label lblHostColon;
       private System.Windows.Forms.Label lblName;
       private System.Windows.Forms.TextBox txtName;
       private System.Windows.Forms.Label lblDomain;
       private System.Windows.Forms.TextBox txtDomain;
       private System.Windows.Forms.CheckBox chkFullScreen;
       private System.Windows.Forms.CheckBox chkConnectToConsole;
       private System.Windows.Forms.CheckBox chkConnectLocalDrives;
       private System.Windows.Forms.Button btnDelete;
       private System.Windows.Forms.Button btnDeleteGroup;
       private System.Windows.Forms.GroupBox grpSettings;
       private System.Windows.Forms.Button btnNew;
       private System.Windows.Forms.Panel pnlNewButton;
       private System.Windows.Forms.Button btnClone;
       private System.Windows.Forms.CheckBox chkConnectLocalPrinters;
       private System.ComponentModel.BackgroundWorker threadConnectButton;
       private System.Windows.Forms.Button btnExport;
       private System.Windows.Forms.CheckBox chkMaximumGUIExperience;
       private System.Windows.Forms.Button btnSettings;
       private System.ComponentModel.BackgroundWorker threadCheckForUpdate;
       private System.Windows.Forms.CheckBox chkPromptCredentials;
       private System.Windows.Forms.Button btnDiscover;
       private System.Windows.Forms.CheckBox chkSpan;
       private System.Windows.Forms.Button btnEditRDP;
    }
}

