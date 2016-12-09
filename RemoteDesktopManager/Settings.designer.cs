namespace RemoteDesktopManager
{
   partial class Settings
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Settings ) );
         this.btnClose = new System.Windows.Forms.Button();
         this.txtRDPFileLocation = new System.Windows.Forms.TextBox();
         this.lblRDPFileLocation = new System.Windows.Forms.Label();
         this.btnSetRDPFileLocation = new System.Windows.Forms.Button();
         this.chkOnlyHaveOneGroupExpanded = new System.Windows.Forms.CheckBox();
         this.btnSave = new System.Windows.Forms.Button();
         this.chkCheckForUpdateAtStartup = new System.Windows.Forms.CheckBox();
         this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
         this.chkUseSubMenusInTray = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnClose.Location = new System.Drawing.Point( 223, 131 );
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size( 61, 24 );
         this.btnClose.TabIndex = 3;
         this.btnClose.Text = "Close";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
         // 
         // txtRDPFileLocation
         // 
         this.txtRDPFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.txtRDPFileLocation.Location = new System.Drawing.Point( 100, 10 );
         this.txtRDPFileLocation.Name = "txtRDPFileLocation";
         this.txtRDPFileLocation.Size = new System.Drawing.Size( 154, 20 );
         this.txtRDPFileLocation.TabIndex = 4;
         // 
         // lblRDPFileLocation
         // 
         this.lblRDPFileLocation.AutoSize = true;
         this.lblRDPFileLocation.Location = new System.Drawing.Point( 6, 14 );
         this.lblRDPFileLocation.Name = "lblRDPFileLocation";
         this.lblRDPFileLocation.Size = new System.Drawing.Size( 89, 13 );
         this.lblRDPFileLocation.TabIndex = 5;
         this.lblRDPFileLocation.Text = "RDP file location:";
         // 
         // btnSetRDPFileLocation
         // 
         this.btnSetRDPFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSetRDPFileLocation.Location = new System.Drawing.Point( 260, 9 );
         this.btnSetRDPFileLocation.Name = "btnSetRDPFileLocation";
         this.btnSetRDPFileLocation.Size = new System.Drawing.Size( 24, 22 );
         this.btnSetRDPFileLocation.TabIndex = 6;
         this.btnSetRDPFileLocation.Text = "...";
         this.btnSetRDPFileLocation.UseVisualStyleBackColor = true;
         this.btnSetRDPFileLocation.Click += new System.EventHandler( this.btnSetRDPFileLocation_Click );
         // 
         // chkOnlyHaveOneGroupExpanded
         // 
         this.chkOnlyHaveOneGroupExpanded.AutoSize = true;
         this.chkOnlyHaveOneGroupExpanded.Location = new System.Drawing.Point( 9, 36 );
         this.chkOnlyHaveOneGroupExpanded.Name = "chkOnlyHaveOneGroupExpanded";
         this.chkOnlyHaveOneGroupExpanded.Size = new System.Drawing.Size( 203, 17 );
         this.chkOnlyHaveOneGroupExpanded.TabIndex = 7;
         this.chkOnlyHaveOneGroupExpanded.Text = "Only expand one groupnode at a time";
         this.chkOnlyHaveOneGroupExpanded.UseVisualStyleBackColor = true;
         // 
         // btnSave
         // 
         this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSave.Location = new System.Drawing.Point( 154, 131 );
         this.btnSave.Name = "btnSave";
         this.btnSave.Size = new System.Drawing.Size( 61, 24 );
         this.btnSave.TabIndex = 8;
         this.btnSave.Text = "Save";
         this.btnSave.UseVisualStyleBackColor = true;
         this.btnSave.Click += new System.EventHandler( this.btnSave_Click );
         // 
         // chkCheckForUpdateAtStartup
         // 
         this.chkCheckForUpdateAtStartup.AutoSize = true;
         this.chkCheckForUpdateAtStartup.Checked = true;
         this.chkCheckForUpdateAtStartup.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkCheckForUpdateAtStartup.Location = new System.Drawing.Point( 9, 60 );
         this.chkCheckForUpdateAtStartup.Name = "chkCheckForUpdateAtStartup";
         this.chkCheckForUpdateAtStartup.Size = new System.Drawing.Size( 160, 17 );
         this.chkCheckForUpdateAtStartup.TabIndex = 9;
         this.chkCheckForUpdateAtStartup.Text = "Check for updates at startup";
         this.chkCheckForUpdateAtStartup.UseVisualStyleBackColor = true;
         // 
         // chkMinimizeToTray
         // 
         this.chkMinimizeToTray.AutoSize = true;
         this.chkMinimizeToTray.Checked = true;
         this.chkMinimizeToTray.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkMinimizeToTray.Location = new System.Drawing.Point( 9, 83 );
         this.chkMinimizeToTray.Name = "chkMinimizeToTray";
         this.chkMinimizeToTray.Size = new System.Drawing.Size( 141, 17 );
         this.chkMinimizeToTray.TabIndex = 10;
         this.chkMinimizeToTray.Text = "Minimize to tray on close";
         this.chkMinimizeToTray.UseVisualStyleBackColor = true;
         // 
         // chkUseSubMenusInTray
         // 
         this.chkUseSubMenusInTray.AutoSize = true;
         this.chkUseSubMenusInTray.Checked = true;
         this.chkUseSubMenusInTray.CheckState = System.Windows.Forms.CheckState.Checked;
         this.chkUseSubMenusInTray.Location = new System.Drawing.Point( 9, 106 );
         this.chkUseSubMenusInTray.Name = "chkUseSubMenusInTray";
         this.chkUseSubMenusInTray.Size = new System.Drawing.Size( 153, 17 );
         this.chkUseSubMenusInTray.TabIndex = 11;
         this.chkUseSubMenusInTray.Text = "Use submenus in traymenu";
         this.chkUseSubMenusInTray.UseVisualStyleBackColor = true;
         // 
         // Settings
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size( 292, 162 );
         this.Controls.Add( this.chkUseSubMenusInTray );
         this.Controls.Add( this.chkMinimizeToTray );
         this.Controls.Add( this.chkCheckForUpdateAtStartup );
         this.Controls.Add( this.btnSave );
         this.Controls.Add( this.chkOnlyHaveOneGroupExpanded );
         this.Controls.Add( this.btnSetRDPFileLocation );
         this.Controls.Add( this.lblRDPFileLocation );
         this.Controls.Add( this.txtRDPFileLocation );
         this.Controls.Add( this.btnClose );
         this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
         this.MinimumSize = new System.Drawing.Size( 300, 196 );
         this.Name = "Settings";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "RDM - Settings";
         this.Load += new System.EventHandler( this.Settings_Load );
         this.ResumeLayout( false );
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.TextBox txtRDPFileLocation;
      private System.Windows.Forms.Label lblRDPFileLocation;
      private System.Windows.Forms.Button btnSetRDPFileLocation;
      private System.Windows.Forms.CheckBox chkOnlyHaveOneGroupExpanded;
      private System.Windows.Forms.Button btnSave;
      private System.Windows.Forms.CheckBox chkCheckForUpdateAtStartup;
      private System.Windows.Forms.CheckBox chkMinimizeToTray;
      private System.Windows.Forms.CheckBox chkUseSubMenusInTray;
   }
}