using System;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace RemoteDesktopManager
{
   /// <summary>
   /// based on the code from article: 
   /// http://www.codeproject.com/cs/threads/SingletonApp.asp
   /// </summary>
   [Serializable]
   class SingleInstanceController : MarshalByRefObject
   {
      private static IChannel moIpcChannel;
      private static Mutex moMutex = null;
      private static bool mbIsFirstInstance;

      public delegate void ReceiveDelegate( string psMsg );

      static private ReceiveDelegate moReceive = null;
      static public ReceiveDelegate Receiver
      {
         get
         {
             return moReceive;
         }
         set
         {
             moReceive = value;
         }
      }

      public static bool FirstInstance( ReceiveDelegate poRD )
      {
         if(FirstInstance())
         {
            Receiver += poRD;
            return true;
         }
         else
         {
            return false;
         }
      }

      public static bool FirstInstance()
      {
         try
         {
            moMutex = new Mutex( true, getUniqueId(), out mbIsFirstInstance );

            if(mbIsFirstInstance == true)
            {
               moIpcChannel = new IpcServerChannel( getUniqueId() );
               ChannelServices.RegisterChannel( moIpcChannel, false );

               RemotingConfiguration.RegisterWellKnownServiceType(
                  typeof( SingleInstanceController ), "SingleInstanceController",
                  WellKnownObjectMode.Singleton
               );

               return true;
            }
            else // We are not the first instance
            {
               moMutex.Close();
               moMutex = null;
               return false;
            }
         }
         catch(Exception e)
         {
            throw e;
         }
      }

      public static void Cleanup()
      {
         try
         {
            if(moMutex != null)
            {
               moMutex.Close();
            }
         } 
         catch { /* ignore */ }

         try
         {
            if(moIpcChannel != null)
            {
               ChannelServices.UnregisterChannel( moIpcChannel );
            }
         } 
         catch { /* ignore */ }

         moMutex = null;
         moIpcChannel = null;
      }

      public static void Send( string psMsg )
      {
         try
         {
            SingleInstanceController loController;
            ChannelServices.RegisterChannel( new IpcClientChannel(), false );
            
            loController = (SingleInstanceController)Activator.GetObject(typeof(SingleInstanceController), 
               "ipc://" + getUniqueId() + "/SingleInstanceController" );
            
            loController.Receive( psMsg );
         }
         catch( Exception e )
         {
            throw e;
         }
      }

      public void Receive( string psMsg )
      {
         if( moReceive != null )
         {
            moReceive( psMsg );
         }
      }

      private static string getUniqueId()
      {
         return System.Reflection.Assembly.GetExecutingAssembly().
            ManifestModule.ModuleVersionId.ToString();
      }
   }
}
