using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteDesktopManager
{
   public partial class Settings : Form
   {
      private MainForm moForm;
      public Settings( MainForm poForm )
      {
         moForm = poForm;
         InitializeComponent();
      }

      private void Settings_Load( object sender, EventArgs e )
      {
         this.txtRDPFileLocation.Text = moForm.RDPFileLocation;
         this.txtRDPFileLocation.Select( this.txtRDPFileLocation.Text.Length - 1, 1 );
         this.chkOnlyHaveOneGroupExpanded.Checked = moForm.ExpandOnlyOneNode;
         this.chkCheckForUpdateAtStartup.Checked = moForm.CheckForUpdateAtStartup;
         this.chkMinimizeToTray.Checked = moForm.MinimizeToTray;
         this.chkUseSubMenusInTray.Checked = moForm.UseSubMenusInTrayMenu;
      }

      private void btnClose_Click( object sender, EventArgs e )
      {
         this.Close();
      }

      private void btnSetRDPFileLocation_Click( object sender, EventArgs e )
      {
         FolderBrowserDialog loDlg = new FolderBrowserDialog();
         loDlg.Description = "Select a folder to store the RDP conection files";
         loDlg.ShowNewFolderButton = true;
         loDlg.SelectedPath = this.txtRDPFileLocation.Text;

         if(loDlg.ShowDialog() == DialogResult.OK)
         {
            this.txtRDPFileLocation.Text = loDlg.SelectedPath;
         }
      }

      private void btnSave_Click( object sender, EventArgs e )
      {
         try
         {
            moForm.ExpandOnlyOneNode = this.chkOnlyHaveOneGroupExpanded.Checked;
            moForm.CheckForUpdateAtStartup = this.chkCheckForUpdateAtStartup.Checked;
            moForm.MinimizeToTray = this.chkMinimizeToTray.Checked;
            moForm.UseSubMenusInTrayMenu = this.chkUseSubMenusInTray.Checked;

            if(moForm.RDPFileLocation.Equals(
                this.txtRDPFileLocation.Text ) == false)
            {
               String[] lasFiles = Directory.GetFiles(
                  moForm.RDPFileLocation, "*.rdp" );

               for(int f = 0; f < lasFiles.Length; f++)
               {
                  File.Move
                  ( 
                     lasFiles[f],
                     this.txtRDPFileLocation.Text + "\\" + 
                        Path.GetFileName( lasFiles[f] )
                  );
               }

               moForm.RDPFileLocation = this.txtRDPFileLocation.Text;
            }

            moForm.writeApplicationConfig();
            moForm.RenderTrayIconMenu();
         }
         catch( Exception pe )
         {
            Utility.showMessageBox( moForm, "Error occured while storing settings", pe );
         }

         this.Close();
      }
   }
}