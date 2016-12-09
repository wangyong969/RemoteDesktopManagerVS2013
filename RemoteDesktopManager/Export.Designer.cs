namespace RemoteDesktopManager
{
   partial class Export
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
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem( "Group one", 0 );
         System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem( "connectie een", 1 );
         System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem( "connectie twee", 1 );
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( Export ) );
         this.btnExport = new System.Windows.Forms.Button();
         this.btnClose = new System.Windows.Forms.Button();
         this.listView1 = new System.Windows.Forms.ListView();
         this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
         this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
         this.imageList1 = new System.Windows.Forms.ImageList( this.components );
         this.btnOpenFolder = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // btnExport
         // 
         this.btnExport.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.btnExport.Location = new System.Drawing.Point( 231, 262 );
         this.btnExport.Name = "btnExport";
         this.btnExport.Size = new System.Drawing.Size( 61, 24 );
         this.btnExport.TabIndex = 4;
         this.btnExport.Text = "Export";
         this.btnExport.UseVisualStyleBackColor = true;
         // 
         // btnClose
         // 
         this.btnClose.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.btnClose.Location = new System.Drawing.Point( 298, 262 );
         this.btnClose.Name = "btnClose";
         this.btnClose.Size = new System.Drawing.Size( 61, 24 );
         this.btnClose.TabIndex = 3;
         this.btnClose.Text = "Close";
         this.btnClose.UseVisualStyleBackColor = true;
         this.btnClose.Click += new System.EventHandler( this.btnClose_Click );
         // 
         // listView1
         // 
         this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
         this.listView1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                     | System.Windows.Forms.AnchorStyles.Left )
                     | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.listView1.CheckBoxes = true;
         this.listView1.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2} );
         this.listView1.FullRowSelect = true;
         this.listView1.GridLines = true;
         this.listView1.HoverSelection = true;
         listViewItem1.StateImageIndex = 0;
         listViewItem2.IndentCount = 1;
         listViewItem2.StateImageIndex = 0;
         listViewItem3.IndentCount = 1;
         listViewItem3.StateImageIndex = 0;
         this.listView1.Items.AddRange( new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3} );
         this.listView1.LabelWrap = false;
         this.listView1.Location = new System.Drawing.Point( 7, 9 );
         this.listView1.MultiSelect = false;
         this.listView1.Name = "listView1";
         this.listView1.Size = new System.Drawing.Size( 352, 247 );
         this.listView1.SmallImageList = this.imageList1;
         this.listView1.TabIndex = 5;
         this.listView1.UseCompatibleStateImageBehavior = false;
         this.listView1.View = System.Windows.Forms.View.Details;
         this.listView1.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler( this.listView1_ItemChecked );
         // 
         // columnHeader1
         // 
         this.columnHeader1.Text = "Connection";
         this.columnHeader1.Width = 151;
         // 
         // columnHeader2
         // 
         this.columnHeader2.Text = "Group";
         this.columnHeader2.Width = 107;
         // 
         // imageList1
         // 
         this.imageList1.ImageStream = ( (System.Windows.Forms.ImageListStreamer)( resources.GetObject( "imageList1.ImageStream" ) ) );
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
         this.imageList1.Images.SetKeyName( 0, "chart_organisation.ico" );
         this.imageList1.Images.SetKeyName( 1, "computer.ico" );
         // 
         // btnOpenFolder
         // 
         this.btnOpenFolder.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
         this.btnOpenFolder.Location = new System.Drawing.Point( 164, 262 );
         this.btnOpenFolder.Name = "btnOpenFolder";
         this.btnOpenFolder.Size = new System.Drawing.Size( 61, 24 );
         this.btnOpenFolder.TabIndex = 4;
         this.btnOpenFolder.Text = "Explorer";
         this.btnOpenFolder.UseVisualStyleBackColor = true;
         // 
         // Export
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size( 366, 292 );
         this.Controls.Add( this.listView1 );
         this.Controls.Add( this.btnOpenFolder );
         this.Controls.Add( this.btnExport );
         this.Controls.Add( this.btnClose );
         this.MinimumSize = new System.Drawing.Size( 300, 250 );
         this.Name = "Export";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "RDM - Connection export";
         this.Load += new System.EventHandler( this.Export_Load );
         this.ResumeLayout( false );

      }

      #endregion

      private System.Windows.Forms.Button btnExport;
      private System.Windows.Forms.Button btnClose;
      private System.Windows.Forms.ListView listView1;
      private System.Windows.Forms.ImageList imageList1;
      private System.Windows.Forms.ColumnHeader columnHeader1;
      private System.Windows.Forms.ColumnHeader columnHeader2;
      private System.Windows.Forms.Button btnOpenFolder;
   }
}