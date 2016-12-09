namespace RemoteDesktopManager
{
   partial class DiscoveryForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose( bool disposing )
      {
         if(disposing && ( components != null ))
         {
            components.Dispose();
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( DiscoveryForm ) );
         this.cmbIPAddresses = new System.Windows.Forms.ComboBox();
         this.btnDiscover = new System.Windows.Forms.Button();
         this.moDiscoveredHosts = new System.Windows.Forms.CheckedListBox();
         this.lblMask = new System.Windows.Forms.Label();
         this.lblRDPDomain = new System.Windows.Forms.Label();
         this.txtRDPDomain = new System.Windows.Forms.TextBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.chkRDPLocalDrives = new System.Windows.Forms.CheckBox();
         this.lblRDPScreenSize = new System.Windows.Forms.Label();
         this.lblRDPScreen = new System.Windows.Forms.Label();
         this.moRDPSlider = new System.Windows.Forms.TrackBar();
         this.txtRDPUsername = new System.Windows.Forms.TextBox();
         this.lblRDPUsername = new System.Windows.Forms.Label();
         this.txtRDPGroupName = new System.Windows.Forms.TextBox();
         this.lblRDPGroupName = new System.Windows.Forms.Label();
         this.chkRDPUseDomain = new System.Windows.Forms.CheckBox();
         this.btnSave = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.chkRDPStripDomain = new System.Windows.Forms.CheckBox();
         this.moStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.moStatusStrip = new System.Windows.Forms.StatusStrip();
         this.btnClose = new System.Windows.Forms.Button();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.moRDPSlider)).BeginInit();
         this.moStatusStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // cmbIPAddresses
         // 
         this.cmbIPAddresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cmbIPAddresses.FormattingEnabled = true;
         this.cmbIPAddresses.Location = new System.Drawing.Point( 58, 10 );
         this.cmbIPAddresses.Name = "cmbIPAddresses";
         this.cmbIPAddresses.Size = new System.Drawing.Size( 197, 21 );
         this.cmbIPAddresses.TabIndex = 1;
         // 
         // btnDiscover
         // 
         this.btnDiscover.Location = new System.Drawing.Point( 264, 10 );
         this.btnDiscover.Name = "btnDiscover";
         this.btnDiscover.Size = new System.Drawing.Size( 59, 22 );
         this.btnDiscover.TabIndex = 2;
         this.btnDiscover.Text = "Discover";
         this.btnDiscover.UseVisualStyleBackColor = true;
         this.btnDiscover.Click += new System.EventHandler( this.btnDiscover_Click );
         // 
         // moDiscoveredHosts
         // 
         this.moDiscoveredHosts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.moDiscoveredHosts.FormattingEnabled = true;
         this.moDiscoveredHosts.Location = new System.Drawing.Point( 10, 41 );
         this.moDiscoveredHosts.Name = "moDiscoveredHosts";
         this.moDiscoveredHosts.Size = new System.Drawing.Size( 245, 229 );
         this.moDiscoveredHosts.TabIndex = 3;
         // 
         // lblMask
         // 
         this.lblMask.AutoSize = true;
         this.lblMask.Location = new System.Drawing.Point( 7, 14 );
         this.lblMask.Name = "lblMask";
         this.lblMask.Size = new System.Drawing.Size( 48, 13 );
         this.lblMask.TabIndex = 0;
         this.lblMask.Text = "Scan IP:";
         // 
         // lblRDPDomain
         // 
         this.lblRDPDomain.AutoSize = true;
         this.lblRDPDomain.Location = new System.Drawing.Point( 28, 76 );
         this.lblRDPDomain.Name = "lblRDPDomain";
         this.lblRDPDomain.Size = new System.Drawing.Size( 46, 13 );
         this.lblRDPDomain.TabIndex = 0;
         this.lblRDPDomain.Text = "Domain:";
         // 
         // txtRDPDomain
         // 
         this.txtRDPDomain.Location = new System.Drawing.Point( 76, 73 );
         this.txtRDPDomain.Name = "txtRDPDomain";
         this.txtRDPDomain.Size = new System.Drawing.Size( 122, 20 );
         this.txtRDPDomain.TabIndex = 6;
         // 
         // groupBox1
         // 
         this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.groupBox1.Controls.Add( this.chkRDPLocalDrives );
         this.groupBox1.Controls.Add( this.lblRDPScreenSize );
         this.groupBox1.Controls.Add( this.lblRDPScreen );
         this.groupBox1.Controls.Add( this.moRDPSlider );
         this.groupBox1.Controls.Add( this.txtRDPUsername );
         this.groupBox1.Controls.Add( this.lblRDPUsername );
         this.groupBox1.Controls.Add( this.txtRDPGroupName );
         this.groupBox1.Controls.Add( this.lblRDPGroupName );
         this.groupBox1.Controls.Add( this.txtRDPDomain );
         this.groupBox1.Controls.Add( this.lblRDPDomain );
         this.groupBox1.Location = new System.Drawing.Point( 264, 40 );
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size( 206, 158 );
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "RDP Options:";
         // 
         // chkRDPLocalDrives
         // 
         this.chkRDPLocalDrives.AutoSize = true;
         this.chkRDPLocalDrives.Checked = true;
         this.chkRDPLocalDrives.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkRDPLocalDrives.Location = new System.Drawing.Point( 76, 130 );
         this.chkRDPLocalDrives.Name = "chkRDPLocalDrives";
         this.chkRDPLocalDrives.Size = new System.Drawing.Size( 122, 17 );
         this.chkRDPLocalDrives.TabIndex = 8;
         this.chkRDPLocalDrives.Text = "Connect local drives";
         this.chkRDPLocalDrives.UseVisualStyleBackColor = true;
         // 
         // lblRDPScreenSize
         // 
         this.lblRDPScreenSize.Location = new System.Drawing.Point( 137, 101 );
         this.lblRDPScreenSize.Name = "lblRDPScreenSize";
         this.lblRDPScreenSize.Size = new System.Drawing.Size( 61, 13 );
         this.lblRDPScreenSize.TabIndex = 0;
         this.lblRDPScreenSize.Text = "1024x768";
         this.lblRDPScreenSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
         // 
         // lblRDPScreen
         // 
         this.lblRDPScreen.AutoSize = true;
         this.lblRDPScreen.Location = new System.Drawing.Point( 12, 101 );
         this.lblRDPScreen.Name = "lblRDPScreen";
         this.lblRDPScreen.Size = new System.Drawing.Size( 62, 13 );
         this.lblRDPScreen.TabIndex = 0;
         this.lblRDPScreen.Text = "Screensize:";
         // 
         // moRDPSlider
         // 
         this.moRDPSlider.LargeChange = 1;
         this.moRDPSlider.Location = new System.Drawing.Point( 71, 95 );
         this.moRDPSlider.Maximum = 4;
         this.moRDPSlider.Name = "moRDPSlider";
         this.moRDPSlider.Size = new System.Drawing.Size( 71, 45 );
         this.moRDPSlider.TabIndex = 7;
         this.moRDPSlider.Value = 2;
         this.moRDPSlider.ValueChanged += new System.EventHandler( this.moRDPSlider_ValueChanged );
         // 
         // txtRDPUsername
         // 
         this.txtRDPUsername.Location = new System.Drawing.Point( 76, 47 );
         this.txtRDPUsername.Name = "txtRDPUsername";
         this.txtRDPUsername.Size = new System.Drawing.Size( 122, 20 );
         this.txtRDPUsername.TabIndex = 5;
         this.txtRDPUsername.Text = "Administrator";
         // 
         // lblRDPUsername
         // 
         this.lblRDPUsername.AutoSize = true;
         this.lblRDPUsername.Location = new System.Drawing.Point( 16, 50 );
         this.lblRDPUsername.Name = "lblRDPUsername";
         this.lblRDPUsername.Size = new System.Drawing.Size( 58, 13 );
         this.lblRDPUsername.TabIndex = 0;
         this.lblRDPUsername.Text = "Username:";
         // 
         // txtRDPGroupName
         // 
         this.txtRDPGroupName.Location = new System.Drawing.Point( 76, 21 );
         this.txtRDPGroupName.Name = "txtRDPGroupName";
         this.txtRDPGroupName.Size = new System.Drawing.Size( 122, 20 );
         this.txtRDPGroupName.TabIndex = 4;
         this.txtRDPGroupName.Text = "Auto Discovered";
         // 
         // lblRDPGroupName
         // 
         this.lblRDPGroupName.AutoSize = true;
         this.lblRDPGroupName.Location = new System.Drawing.Point( 7, 24 );
         this.lblRDPGroupName.Name = "lblRDPGroupName";
         this.lblRDPGroupName.Size = new System.Drawing.Size( 67, 13 );
         this.lblRDPGroupName.TabIndex = 0;
         this.lblRDPGroupName.Text = "GroupName:";
         // 
         // chkRDPUseDomain
         // 
         this.chkRDPUseDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRDPUseDomain.Location = new System.Drawing.Point( 265, 227 );
         this.chkRDPUseDomain.Name = "chkRDPUseDomain";
         this.chkRDPUseDomain.Size = new System.Drawing.Size( 205, 17 );
         this.chkRDPUseDomain.TabIndex = 9;
         this.chkRDPUseDomain.Text = "Use DNS domain as RDP Domain";
         this.chkRDPUseDomain.UseVisualStyleBackColor = true;
         // 
         // btnSave
         // 
         this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSave.Location = new System.Drawing.Point( 329, 250 );
         this.btnSave.Name = "btnSave";
         this.btnSave.Size = new System.Drawing.Size( 66, 23 );
         this.btnSave.TabIndex = 9;
         this.btnSave.Text = "Add";
         this.btnSave.UseVisualStyleBackColor = true;
         this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
         // 
         // btnCancel
         // 
         this.btnCancel.Location = new System.Drawing.Point( 329, 10 );
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size( 59, 22 );
         this.btnCancel.TabIndex = 2;
         this.btnCancel.Text = "Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Visible = false;
         this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
         // 
         // chkRDPStripDomain
         // 
         this.chkRDPStripDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.chkRDPStripDomain.Checked = true;
         this.chkRDPStripDomain.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkRDPStripDomain.Location = new System.Drawing.Point( 265, 205 );
         this.chkRDPStripDomain.Name = "chkRDPStripDomain";
         this.chkRDPStripDomain.Size = new System.Drawing.Size( 205, 17 );
         this.chkRDPStripDomain.TabIndex = 10;
         this.chkRDPStripDomain.Text = "Strip DNS domain from RDP name";
         this.chkRDPStripDomain.UseVisualStyleBackColor = true;
         // 
         // moStatusLabel
         // 
         this.moStatusLabel.AutoSize = false;
         this.moStatusLabel.Name = "moStatusLabel";
         this.moStatusLabel.Size = new System.Drawing.Size( 200, 17 );
         this.moStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // moStatusStrip
         // 
         this.moStatusStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.moStatusLabel} );
         this.moStatusStrip.Location = new System.Drawing.Point( 0, 281 );
         this.moStatusStrip.Name = "moStatusStrip";
         this.moStatusStrip.Size = new System.Drawing.Size( 480, 22 );
         this.moStatusStrip.TabIndex = 3;
         this.moStatusStrip.Text = "statusStrip1";
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnClose.Location = new System.Drawing.Point( 404, 250 );
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size( 66, 23 );
         this.btnClose.TabIndex = 11;
         this.btnClose.Text = "Close";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
         // 
         // DiscoveryForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size( 480, 303 );
         this.Controls.Add( this.btnClose );
         this.Controls.Add( this.chkRDPStripDomain );
         this.Controls.Add( this.chkRDPUseDomain );
         this.Controls.Add( this.btnSave );
         this.Controls.Add( this.groupBox1 );
         this.Controls.Add( this.lblMask );
         this.Controls.Add( this.moStatusStrip );
         this.Controls.Add( this.moDiscoveredHosts );
         this.Controls.Add( this.btnDiscover );
         this.Controls.Add( this.cmbIPAddresses );
         this.Controls.Add( this.btnCancel );
         this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
         this.MaximizeBox = false;
         this.MaximumSize = new System.Drawing.Size( 650, 682 );
         this.MinimizeBox = false;
         this.MinimumSize = new System.Drawing.Size( 488, 337 );
         this.Name = "DiscoveryForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "RDP Discovery";
         this.Load += new System.EventHandler( this.DiscoveryForm_Load );
         this.groupBox1.ResumeLayout( false );
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.moRDPSlider)).EndInit();
         this.moStatusStrip.ResumeLayout( false );
         this.moStatusStrip.PerformLayout();
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.ComboBox cmbIPAddresses;
      private System.Windows.Forms.Button btnDiscover;
      private System.Windows.Forms.CheckedListBox moDiscoveredHosts;
      private System.Windows.Forms.Label lblMask;
      private System.Windows.Forms.Label lblRDPDomain;
      private System.Windows.Forms.TextBox txtRDPDomain;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.TextBox txtRDPUsername;
      private System.Windows.Forms.Label lblRDPUsername;
      private System.Windows.Forms.TextBox txtRDPGroupName;
      private System.Windows.Forms.Label lblRDPGroupName;
      private System.Windows.Forms.Label lblRDPScreen;
      private System.Windows.Forms.TrackBar moRDPSlider;
      private System.Windows.Forms.Label lblRDPScreenSize;
      private System.Windows.Forms.CheckBox chkRDPLocalDrives;
      private System.Windows.Forms.Button btnSave;
      private System.Windows.Forms.CheckBox chkRDPUseDomain;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.CheckBox chkRDPStripDomain;
      private System.Windows.Forms.ToolStripStatusLabel moStatusLabel;
      private System.Windows.Forms.StatusStrip moStatusStrip;
      private System.Windows.Forms.Button btnClose;
   }
}

