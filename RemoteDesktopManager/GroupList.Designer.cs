namespace RemoteDesktopManager
{
   partial class GroupList
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( GroupList ) );
         this.lstGroups = new System.Windows.Forms.ListBox();
         this.btnClose = new System.Windows.Forms.Button();
         this.btnDelete = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // lstGroups
         // 
         this.lstGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lstGroups.FormattingEnabled = true;
         this.lstGroups.Location = new System.Drawing.Point( 7, 8 );
         this.lstGroups.Name = "lstGroups";
         this.lstGroups.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
         this.lstGroups.Size = new System.Drawing.Size( 177, 186 );
         this.lstGroups.TabIndex = 0;
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnClose.Location = new System.Drawing.Point( 110, 199 );
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size( 75, 24 );
         this.btnClose.TabIndex = 1;
         this.btnClose.Text = "Close";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
         // 
         // btnDelete
         // 
         this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnDelete.Location = new System.Drawing.Point( 32, 199 );
         this.btnDelete.Name = "btnDelete";
         this.btnDelete.Size = new System.Drawing.Size( 75, 24 );
         this.btnDelete.TabIndex = 2;
         this.btnDelete.Text = "Delete";
         this.btnDelete.UseVisualStyleBackColor = true;
         this.btnDelete.Click += new System.EventHandler( this.btnDelete_Click );
         // 
         // GroupList
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size( 191, 229 );
         this.Controls.Add( this.btnDelete );
         this.Controls.Add( this.btnClose );
         this.Controls.Add( this.lstGroups );
         this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
         this.MinimumSize = new System.Drawing.Size( 173, 200 );
         this.Name = "GroupList";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "RDM - Groups";
         this.Load += new System.EventHandler( this.GroupList_Load );
         this.ResumeLayout( false );

      }

      #endregion

      private System.Windows.Forms.ListBox lstGroups;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.Button btnDelete;
   }
}