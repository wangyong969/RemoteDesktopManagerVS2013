using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace SetupHelper
{
   class MainApp
   {
      public MainApp( string[] args )
      {
         if(args == null || args.Length == 0)
         {
            usage();
            Environment.Exit( 1 );
         }

         if( "/migrate".Equals( args[0] ) )
         {
            if(args.Length != 3)
            {
               Console.WriteLine( "no old and new dir specified" );
               Environment.Exit( 2 );
            }

            Environment.Exit
            ( 
               copyFiles( args[1], args[2] ) 
            );
         }

         if("/uninstall".Equals( args[0] ))
         {
            if(args.Length != 2)
            {
               Console.WriteLine( "install dir not specified" );
               Environment.Exit( 2 );
            }

            Environment.Exit
            (
               uninstallApplication( args[1] )
            );
         }
      }

      public int copyFiles( string psOldDir, string psNewDir )
      {
         if(Directory.Exists(psOldDir) == false)
         {
            Console.WriteLine( "old dir does not exist" );
            return 2;
         }

         if(Directory.Exists( psNewDir ) == false)
         {
            Console.WriteLine( "new dir does not exist" );
            return 2;
         }

         if(File.Exists( psOldDir + "\\TSCManager_config.xml" ) == false)
         {
            Console.WriteLine( "TSCManager_config.xml does not exist" );
            return 3;
         }

         String lsOldRDPFileLocation = "";

         XmlTextReader loReader = null;
         XmlTextWriter loWriter = null;
         try
         {
            XmlDocument loDoc = new XmlDocument();
            
            loReader = new XmlTextReader
            (
               new FileStream( psOldDir + "\\TSCManager_config.xml", FileMode.Open, FileAccess.Read )
            );
            loDoc.Load( loReader );

            XmlNodeList loNode = loDoc.GetElementsByTagName( "RDPFileLocation" );
            if(loNode != null && loNode.Count > 0 && loNode[0].FirstChild != null)
            {
               lsOldRDPFileLocation = "" + loNode[0].FirstChild.Value;
               loNode[0].FirstChild.Value = psNewDir + "\\connections";
            }

            loWriter = new XmlTextWriter
            ( 
               psNewDir + "\\RemoteDesktopManager_config.xml", 
               System.Text.Encoding.UTF8 
            );
            loWriter.Formatting = Formatting.Indented;
            loDoc.Save( loWriter );
         }
         catch(Exception pe)
         {
            Console.WriteLine( "Error handling config XML: " + pe.Message );
            return 3;
         }
         finally
         {
            if(loReader != null)
               loReader.Close();

            if(loWriter != null)
               loWriter.Close();
         }

         Directory.CreateDirectory( psNewDir + "\\connections" );

         String[] lasFiles = 
            Directory.GetFileSystemEntries( lsOldRDPFileLocation );

         foreach( String lsFile in lasFiles ) 
         {
            File.Copy( lsFile, psNewDir + "\\connections\\" + Path.GetFileName( lsFile ), true );
         }

         return 0;
      }

      public int uninstallApplication( string psInstallDir )
      {
         return 0;
      }

      public void usage()
      {
         Console.WriteLine( "Usage: SetupHelper /migrate <old dir> <new dir>" );
      }

      static void Main( string[] args )
      {
         new MainApp( args );
      }
   }
}
