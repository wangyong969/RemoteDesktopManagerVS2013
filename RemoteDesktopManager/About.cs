using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopManager
{
   public partial class About:Form
   {
      public About()
      {
         InitializeComponent();
      }

      private void About_Load( object sender, EventArgs e )
      {
         lblVersion.Text = Application.ProductName + " - " + Application.ProductVersion;

         if(Utility.IsAtLeast61MSTSCClient() == true)
         {
             lblRDPVersion.Text = "6.1 (Vista SP1, Win XP SP3, 2008 Server)";
         }
         else if(Utility.IsAtLeast60MSTSCClient() == true)
         {
             lblRDPVersion.Text = "6.0 (Vista, XP Update KB925876)";
         }
         else
         {
             lblRDPVersion.Text = "5.0 or earlier version";
         }
      }

      private void lblSourceForgeUrl_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
         System.Diagnostics.Process.Start( "http://sourceforge.net/projects/tscm/" );
      }

      private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
         System.Diagnostics.Process.Start( "http://www.famfamfam.com/lab/icons/silk/" );
      }

      private void linkLabel2_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
         System.Diagnostics.Process.Start( "http://www.codeproject.com/cs/miscctrl/TreeViewReArr.asp" );
      }

      private void linkLabel3_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
      {
         System.Diagnostics.Process.Start( "http://www.tudra.net/" );
      }
   }
}