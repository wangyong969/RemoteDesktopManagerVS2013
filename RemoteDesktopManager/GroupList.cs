using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopManager
{
   public partial class GroupList : Form
   {
      private MainForm moForm;
      public GroupList( MainForm poForm )
      {
         moForm = poForm;
         InitializeComponent();
      }

      private void btnClose_Click( object sender, EventArgs e )
      {
         this.Close();
      }

      private void btnDelete_Click( object sender, EventArgs e )
      {
         if(lstGroups.SelectedItems.Count == 0)
         {
            Utility.showMessageBox( moForm, "No group(s) selected." );
            return;
         }
         
         if( Utility.showMessageBox( moForm, 
              "Are you sure you want to delete the selected group(s)?", 
              MessageBoxButtons.YesNo, 
              MessageBoxIcon.Question, 
              MessageBoxDefaultButton.Button2 ) == DialogResult.No )
         {
            return;
         }

         for(int i = 0; i < lstGroups.SelectedItems.Count; i++)
         {
            String lsItem = lstGroups.SelectedItems[i].ToString();

            if(lsItem.EndsWith( "(0)" ) )
            {
               lsItem = lsItem.Substring( 0, lsItem.Length - 4 );

               TreeNode[] laoNodes = moForm.ComputersTreeView.Nodes.Find( lsItem, true );
               foreach( TreeNode loNode in laoNodes )
               {
                  loNode.Remove();
               }

               if( moForm.GroupListBox.Text.Equals( lsItem ) )
               {
                  moForm.GroupListBox.Text = "";
               }
               moForm.GroupListBox.Items.Remove( lsItem );
               lstGroups.Items.Remove( lstGroups.SelectedItems[i].ToString() );
            }
            else if(lsItem.LastIndexOf( '(' ) == -1)
            {
               if(moForm.GroupListBox.Text.Equals( lsItem ))
               {
                  moForm.GroupListBox.Text = "";
               }
               moForm.GroupListBox.Items.Remove( lsItem );
               lstGroups.Items.Remove( lstGroups.SelectedItems[i].ToString() );
            }
            else // group has nodes
            {
               lsItem = lsItem.Substring( 0, lsItem.LastIndexOf( '(' ) ).Trim();

               if( Utility.showMessageBox( moForm,
                    "Selected group '" + lsItem + "' has child nodes. " +
                       "Also delete all computer nodes?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2 ) == DialogResult.Yes)
               {
                  TreeNode[] laoNodes = moForm.ComputersTreeView.Nodes.Find( lsItem, true );
                  foreach(TreeNode loNode in laoNodes)
                  {
                     if(loNode.Level == 0 && loNode.Nodes != null)
                     {
                        foreach(TreeNode loChild in loNode.Nodes)
                        {
                           moForm.ComputersTreeView.SelectedNode = null;
                           moForm.ComputersTreeView.Nodes.Remove( loChild );

                           moForm.mcoConnections.Remove( loChild.Name );
                           moForm.msCurrentConnection = null;

                           moForm.ComputersTreeView.SelectedNode = loNode.PrevNode;
                        }
                     }
                     loNode.Remove();
                  }

                  if(moForm.GroupListBox.Text.Equals( lsItem ))
                  {
                     moForm.GroupListBox.Text = "";
                  }
                  moForm.GroupListBox.Items.Remove( lsItem );
                  lstGroups.Items.Remove( lstGroups.SelectedItems[i].ToString() );
               }
            }
         }
         this.Close();
      }

      private void GroupList_Load( object sender, EventArgs e )
      {
         for(int i = 0; i < this.moForm.GroupListBox.Items.Count; i++)
         {
            String lsItem = this.moForm.GroupListBox.Items[i].ToString();

            TreeNode[] laoNodes = moForm.ComputersTreeView.Nodes.Find( lsItem, true );
            foreach(TreeNode loNode in laoNodes)
            {
               lsItem += " (" + loNode.Nodes.Count + ")";
            }

            lstGroups.Items.Add( lsItem );
         }
      }
   }
}