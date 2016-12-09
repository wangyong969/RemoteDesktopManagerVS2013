using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopManager
{
   public partial class Export : Form
   {
      private MainForm moForm;
      public Export( MainForm poForm )
      {
         moForm = poForm;
         InitializeComponent();
      }

      private void Export_Load( object sender, EventArgs e )
      {
         TreeNodeCollection lcoNodes = moForm.ComputersTreeView.Nodes;

         /*
         for(int i = 0; i < lcoNodes.Count; i++)
         {
            String lsItem = this.moForm.GroupListBox.Items[i].ToString();

            TreeNode[] laoNodes = moForm.ComputersTreeView.Nodes.Find( lsItem, true );
            foreach(TreeNode loNode in laoNodes)
            {
               lsItem += " (" + loNode.Nodes.Count + ")";
            }

            lstGroups.Items.Add( lsItem );
         }
          */
      }

      private void btnClose_Click( object sender, EventArgs e )
      {
         this.Close();
      }

      private void listView1_ItemChecked( object sender, ItemCheckedEventArgs e )
      {
         ListViewItem loItem = e.Item;

         if(loItem == null)
            return;

         if(loItem.IndentCount == 0)
         {
            for(int i = loItem.Index + 1; i < listView1.Items.Count; i++)
            {
               if(listView1.Items[i].IndentCount == 0)
               {
                  return;
               }
               listView1.Items[i].Checked = loItem.Checked;
            }
         }
      }
   }
}