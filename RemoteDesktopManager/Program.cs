using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RemoteDesktopManager
{
   static class Program
   {
      public static MainForm moMainForm;

      // Single instance IPC callback
      //
      static void ReceiveCallBack( string psMsg )
      {
         if( moMainForm != null )
         {
            if(moMainForm.InvokeRequired)
            {
               moMainForm.Invoke( new MethodInvoker( delegate()
               {
                  moMainForm.ShowApplication();
               } ) );
            }
            else
            {
               moMainForm.ShowApplication();
            }
         }
      }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         try
         {
            if(SingleInstanceController.FirstInstance(
               new SingleInstanceController.ReceiveDelegate( ReceiveCallBack ) ))
            {
               Application.EnableVisualStyles();
               Application.SetCompatibleTextRenderingDefault( false );
               moMainForm = new MainForm();
               Application.Run( moMainForm );
            }
            else
            {
               SingleInstanceController.Send( "activate" );
            }
         }
         finally
         {
            SingleInstanceController.Cleanup();
         } 
      }
   }
}