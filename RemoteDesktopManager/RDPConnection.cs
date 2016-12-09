using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace RemoteDesktopManager
{
   public class RDPConnection
   {
      private string msId;
      private string msName;
      private string msGroup;
      private string msHostname;
      private int miHostnamePort;
      private string msUsername; // Obsolete in 6.0 RDP client
      private string msPasswordStoreHash; // Obsolete in 6.0 RDP client
      private string msDomain;
      private int miScreenSize;
      private Boolean mbFullScreen;
      private Boolean mbSpan;
      private Boolean mbUseLocalDrives;
      private Boolean mbUseLocalPrinters;
      private Boolean mbUseConsoleSession;
      // GUI experience options (set to true for old connections)
      private Boolean mbDisableWallpaper = true;
      private Boolean mbDisableFullWindowDrag = true;
      private Boolean mbDisableMenuAnims = true;
      private Boolean mbDisableThemes = true;
      private Boolean mbDisableBitmapCache = true;
      private Boolean mbPromptCredentials = false; // new in 6.0 RDP
      private String msFilePath = null;
      private String msRDPFileName;

      private List<string>mcoMiscLines = new List<string>();

      public RDPConnection(){}

      public RDPConnection( String psFilePath )
      {
         msFilePath = psFilePath;
      }

      #region Getters and Setters
      public string Id
      {
         get
         {
            if(msId == null)
               msId = this.getNewId();
            return msId; 
         }
         set
         {
            if(msId != null && msId.Length > 0)
               throw new Exception( "Key already set" );
            msId = value;
         }
      }

      public string Name
      {
         get
         {
            return msName;
         }
         set
         {
            msName = value;
         }
      }

      public string Group
      {
         get
         {
            return msGroup;
         }
         set
         {
            msGroup = value;
         }
      }

      public string Hostname
      {
         get
         {
            return msHostname;
         }
         set
         {
            msHostname = value;
         }
      }

      public int Port
      {
         get
         {
            if(miHostnamePort < 1)
               miHostnamePort = 3389;
            return miHostnamePort;
         }
         set
         {
            miHostnamePort = value;
         }
      }

      public string Username
      {
         get
         {
            return msUsername;
         }
         set
         {
            msUsername = value;
         }
      }

      public string Domain
      {
         get
         {
            return msDomain;
         }
         set
         {
            msDomain = value;
         }
      }

      public int ScreenSize
      {
         get
         {
            return miScreenSize;
         }
         set
         {
            miScreenSize = value;
         }
      }

      public bool FullScreen
      {
         get
         {
            return mbFullScreen;
         }
         set
         {
            mbFullScreen = value;
         }
      }

      public bool Span
      {
         get
         {
            return mbSpan;
         }
         set
         {
            mbSpan = value;
         }
      }

      public bool UseLocalDrives
      {
         get
         {
            return mbUseLocalDrives;
         }
         set
         {
            mbUseLocalDrives = value;
         }
      }

      public bool UseLocalPrinters
      {
         get
         {
            return mbUseLocalPrinters;
         }
         set
         {
            mbUseLocalPrinters = value;
         }
      }

      public bool UseConsoleSession
      {
         get
         {
            return mbUseConsoleSession;
         }
         set
         {
            mbUseConsoleSession = value;
         }
      }

      public bool DisableWallpaper
      {
         get
         {
            return mbDisableWallpaper;
         }
         set
         {
            mbDisableWallpaper = value;
         }
      }

      public bool DisableFullWindowDrag
      {
         get
         {
            return mbDisableFullWindowDrag;
         }
         set
         {
            mbDisableFullWindowDrag = value;
         }
      }

      public bool DisableMenuAnims
      {
         get
         {
            return mbDisableMenuAnims;
         }
         set
         {
            mbDisableMenuAnims = value;
         }
      }

      public bool DisableThemes
      {
         get
         {
            return mbDisableThemes;
         }
         set
         {
            mbDisableThemes = value;
         }
      }

      public bool DisableBitmapCache
      {
         get
         {
            return mbDisableBitmapCache;
         }
         set
         {
            mbDisableBitmapCache = value;
         }
      }

      public bool MaximumGUIExperience
      {
         get
         {
            return !( mbDisableWallpaper && mbDisableFullWindowDrag &&
               mbDisableMenuAnims && mbDisableThemes && mbDisableBitmapCache );
         }
         set
         {
            mbDisableWallpaper = !value;
            mbDisableFullWindowDrag = !value;
            mbDisableMenuAnims = !value;
            mbDisableThemes = !value;
            mbDisableBitmapCache = !value;
         }
      }

      public string FilePath
      {
         get
         {
            if( msFilePath == null )
               msFilePath = Path.GetTempPath().ToString();

            return msFilePath;
         }
         set
         {
            msFilePath = value;
         }
      }

      public string RDPFileName
      {
         get
         {
            string lsFileName = FilePath + "\\" + this.formatFileName
            (
               Name + " - " + Group + ".rdp"
            );

            // check if old filename is the same as the new one.
            // If not check if the rdp file already has been written
            // if so delete it.
            //
            if( msRDPFileName != null && 
                lsFileName.Equals( msRDPFileName ) == false )
            {
               try
               {
                  if(File.Exists( msRDPFileName ))
                     File.Delete( msRDPFileName );
               }
               catch( Exception pe )
               {
                  throw pe;
               }
            }

            msRDPFileName = lsFileName;

            return lsFileName;
         }
      }

      public bool PromptCredentials
      {
         get
         {
            return mbPromptCredentials;
         }
         set
         {
            mbPromptCredentials = value;
         }
      }

      #endregion

      public static RDPConnection createConnectionFromRDPFile( string psFileName, string psConnectionPath )
      {
         if(File.Exists( psFileName ) == false)
            throw new Exception( "File '" + psFileName + "' does not exist" );

         RDPConnection loConn = new RDPConnection( psConnectionPath );
         loConn.ReadRDPFile( psFileName );
         if(loConn.Name == null)
         {
            loConn.Name = loConn.Hostname;
         }

         return loConn;
      }

      public RDPConnection duplicateConnection()
      {
         RDPConnection loConn = new RDPConnection();

         loConn.Id = getNewId();
         loConn.Name = this.Name;
         loConn.Group = this.Group;
         loConn.Hostname = this.Hostname;
         loConn.Port = this.Port;
         loConn.ScreenSize = this.ScreenSize;
         loConn.FullScreen = this.FullScreen;
         loConn.Span = this.Span;
         loConn.UseConsoleSession = this.UseConsoleSession;
         loConn.UseLocalDrives = this.UseLocalDrives;
         loConn.UseLocalPrinters = this.UseLocalPrinters;
         loConn.Username = this.Username;
         loConn.Domain = this.Domain;
         loConn.PromptCredentials = this.PromptCredentials;
         loConn.FilePath = this.FilePath;

         loConn.DisableWallpaper = this.DisableWallpaper;
         loConn.DisableThemes = this.DisableThemes;
         loConn.DisableMenuAnims = this.DisableMenuAnims;
         loConn.DisableFullWindowDrag = this.DisableFullWindowDrag;
         loConn.DisableBitmapCache = this.DisableBitmapCache;

         return loConn;
      }

      protected String GetBool( bool pbValue )
      {
         if(pbValue == true)
         {
            return "1";
         }
         else
         {
            return "0";
         }
      }

      private void WriteRDPFile( String psFileName )
      {
         FileInfo loFile = new FileInfo( psFileName );

         StreamWriter loWriter = null;
         try
         {
            loWriter = new StreamWriter( loFile.OpenWrite() );

            if( FullScreen )
            {
               loWriter.WriteLine( "screen mode id:i:2" );
               loWriter.WriteLine( "desktopwidth:i:" + 
                  Screen.PrimaryScreen.WorkingArea.Width );
               loWriter.WriteLine( "desktopheight:i:" + 
                  Screen.PrimaryScreen.WorkingArea.Height );
            }
            else
            {
               loWriter.WriteLine( "screen mode id:i:1" );
               loWriter.WriteLine( "desktopwidth:i:" + 
                  ScreenSizes.GetWidth( ScreenSize ) );
               loWriter.WriteLine( "desktopheight:i:" +
                  ScreenSizes.GetHeight( ScreenSize ) );
            }

            loWriter.WriteLine( "span monitors:i:" +
               GetBool( mbSpan ) );

            loWriter.WriteLine( "session bpp:i:24" );

            if( this.Port == 3389 )
            {
               loWriter.WriteLine( "full address:s:" + this.Hostname );
            }
            else
            {
               loWriter.WriteLine( "full address:s:" + this.Hostname + ":" +
                  this.Port );
            }

            loWriter.WriteLine( "compression:i:1" );
            loWriter.WriteLine( "keyboardhook:i:2" );
            loWriter.WriteLine( "audiomode:i:0" );

            loWriter.WriteLine( "redirectdrives:i:" +
               GetBool( mbUseLocalDrives ) );
            // loWriter.WriteLine( "drivestoredirect:s:*" ); //Since 6.0 RDP

            loWriter.WriteLine( "redirectprinters:i:" +
               GetBool( mbUseLocalPrinters ) );

            loWriter.WriteLine( "redirectcomports:i:0" );
            loWriter.WriteLine( "redirectsmartcards:i:1" );
            loWriter.WriteLine( "redirectclipboard:i:1" );
            loWriter.WriteLine( "redirectposdevices:i:0" );
            loWriter.WriteLine( "displayconnectionbar:i:1" );
            loWriter.WriteLine( "autoreconnection enabled:i:1" );
            loWriter.WriteLine( "username:s:" + this.msUsername );
            loWriter.WriteLine( "domain:s:" + this.msDomain );
            loWriter.WriteLine( "alternate shell:s:" );
            loWriter.WriteLine( "shell working directory:s:" );
            loWriter.WriteLine( "disable cursor setting:i:0" );
            loWriter.WriteLine( "connect to console:i:" + 
               GetBool( mbUseConsoleSession ) );
            loWriter.WriteLine( "disable wallpaper:i:" + 
               GetBool( mbDisableWallpaper ) );
            loWriter.WriteLine( "disable full window drag:i:" + 
               GetBool( mbDisableFullWindowDrag ) );
            loWriter.WriteLine( "disable menu anims:i:" + 
               GetBool( mbDisableMenuAnims ) );
            loWriter.WriteLine( "disable themes:i:" + 
               GetBool( mbDisableThemes ) );
            loWriter.WriteLine( "bitmapcachepersistenable:i:" + 
               GetBool( !mbDisableBitmapCache ) );

            // Since 6.0 RDP
            loWriter.WriteLine( "authentication level:i:0" );
            loWriter.WriteLine( "prompt for credentials:i:" + 
               GetBool( mbPromptCredentials ) );
            loWriter.WriteLine( "negotiate security layer:i:1" );
            loWriter.WriteLine( "remoteapplicationmode:i:0" );
            loWriter.WriteLine( "allow desktop composition:i:0" );
            loWriter.WriteLine( "allow font smoothing:i:0" );
            //following keys can be left out
            //loWriter.WriteLine( "gatewayhostname:s:" );
            //loWriter.WriteLine( "gatewayusagemethod:i:0" );
            //loWriter.WriteLine( "gatewaycredentialssource:i:4" );
            //loWriter.WriteLine( "gatewayprofileusagemethod:i:0" );

            loWriter.WriteLine( "tscm custom id:s:" + this.Id );
            loWriter.WriteLine( "tscm custom name:s:" + this.Name );
            loWriter.WriteLine( "tscm custom group:s:" + this.Group );
            DateTime loNow = DateTime.Now;
            loWriter.WriteLine( "tscm custom savetime:s:" + 
               loNow.ToLongDateString() + " " + loNow.ToShortTimeString() );

            // don't use yet, will write double entries
            //foreach(String lsLine in mcoMiscLines)
            //{
            //   loWriter.WriteLine( lsLine );
            //}

            loWriter.Close();
         }
         catch( Exception pe )
         {
            throw pe;
         }
         finally
         {
            try
            {
               if( loWriter != null )
                  loWriter.Close();
            }
            catch( Exception ) { /* ignore */ }
         }
      }

      private void ReadRDPFile( String psFileName )
      {
         StreamReader loReader = null;
         try
         {
            FileInfo loFile = new FileInfo( psFileName );

            loReader = new StreamReader( loFile.OpenRead(), Encoding.ASCII );

            String lsLine = loReader.ReadLine();
            while( lsLine != null )
            {
               if( lsLine.Equals( "screen mode id:i:2" ) )
               {
                  FullScreen = true;
               }
               else if( lsLine.Equals( "screen mode id:i:1" ) )
               {
                  FullScreen = false;
               }
               else if( lsLine.Equals( "span monitors:i:1" ) )
               {
                  Span = true;
               }
               else if( lsLine.Equals( "span monitors:i:0" ) )
               {
                  Span = false;
               }
               else if( lsLine.StartsWith( "desktopwidth:i:" ) )
               {
                  ScreenSize = 
                     ScreenSizes.SizeForValue( lsLine.Substring( 15 ) );
               }
               else if( lsLine.StartsWith( "desktopheight:i:" ) )
               {
                  ScreenSize = 
                     ScreenSizes.SizeForValue( lsLine.Substring( 16 ) );
               }
               else if( lsLine.StartsWith( "full address:s:" ) )
               {
                  String lsHostPort = lsLine.Substring( 15 );
                  if( lsHostPort.IndexOf( ":" ) != -1 )
                  {
                     this.msHostname = lsHostPort.Substring
                     (
                        0, lsHostPort.IndexOf( ":" ) 
                     );
                     this.miHostnamePort = Convert.ToInt32( 
                        lsHostPort.Substring( lsHostPort.IndexOf( ":" ) + 1 ) );
                  }
                  else
                  {
                     this.msHostname = lsHostPort;
                     this.miHostnamePort = 3389;
                  }

                  if(this.msName == null || this.msName.Length == 0)
                     this.msName = this.msHostname;
               }
               else if( lsLine.Equals( "redirectdrives:i:1" ) )
               {
                  this.mbUseLocalDrives = true;
               }
               else if( lsLine.Equals( "redirectdrives:i:0" ) )
               {
                  this.mbUseLocalDrives = false;
               }
               else if(lsLine.Equals( "redirectprinters:i:1" ))
               {
                  this.mbUseLocalPrinters = true;
               }
               else if(lsLine.Equals( "redirectprinters:i:0" ))
               {
                  this.mbUseLocalPrinters = false;
               }
               else if( lsLine.StartsWith( "username:s:" ) )
               {
                  this.msUsername = lsLine.Substring( 11 );
               }
               else if( lsLine.StartsWith( "domain:s:" ) )
               {
                  this.msDomain = lsLine.Substring( 9 );
               }
               else if( lsLine.Equals( "connect to console:i:1" ) )
               {
                  this.mbUseConsoleSession = true;
               }
               else if( lsLine.Equals( "connect to console:i:0" ) )
               {
                  this.mbUseConsoleSession = false;
               }
               else if( lsLine.Equals( "disable wallpaper:i:1" ) )
               {
                  this.mbDisableWallpaper = true;
               }
               else if( lsLine.Equals( "disable wallpaper:i:0" ) )
               {
                  this.mbDisableWallpaper = false;
               }
               else if( lsLine.Equals( "disable full window drag:i:1" ) )
               {
                  this.mbDisableFullWindowDrag = true;
               }
               else if( lsLine.Equals( "disable full window drag:i:0" ) )
               {
                  this.mbDisableFullWindowDrag = false;
               }
               else if( lsLine.Equals( "disable menu anims:i:1" ) )
               {
                  this.mbDisableMenuAnims = true;
               }
               else if( lsLine.Equals( "disable menu anims:i:0" ) )
               {
                  this.mbDisableMenuAnims = false;
               }
               else if( lsLine.Equals( "disable themes:i:1" ) )
               {
                  this.mbDisableThemes = true;
               }
               else if( lsLine.Equals( "disable themes:i:0" ) )
               {
                  this.mbDisableThemes = false;
               }
               else if( lsLine.Equals( "bitmapcachepersistenable:i:1" ) )
               {
                  this.mbDisableBitmapCache = false;
               }
               else if( lsLine.Equals( "bitmapcachepersistenable:i:0" ) )
               {
                  this.mbDisableBitmapCache = true;
               }
               else if( lsLine.StartsWith( "password 51:b:" ) )
               {
                  this.msPasswordStoreHash = lsLine.Substring( 14 );
               }
               else if( lsLine.StartsWith( "tscm custom id:s:" ) )
               {
                  this.msId = lsLine.Substring( 17 );
               }
               else if( lsLine.StartsWith( "tscm custom name:s:" ) )
               {
                  this.msName = lsLine.Substring( 19 );
               }
               else if( lsLine.StartsWith( "tscm custom group:s:" ) )
               {
                  this.msGroup = lsLine.Substring( 20 );
               }
               else if(lsLine.StartsWith( "tscm custom savetime:s:" ))
               {
                  /*ignnore*/
               }
               else if( lsLine.Equals( "prompt for credentials:i:1" ) )
               {
                  this.mbPromptCredentials = true;
               }
               else if( lsLine.Equals( "prompt for credentials:i:0" ) )
               {
                  this.mbPromptCredentials = false;
               }
               else if( lsLine.StartsWith( "authentication level:i:" ) )
               {
                  /*ignore*/
               }
               else if(lsLine.StartsWith( "negotiate security layer:i:" ))
               {
                  /*ignore*/
               }
               else if(lsLine.StartsWith( "remoteapplicationmode:i:" ))
               {
                  /*ignore*/
               }
               else if(lsLine.StartsWith( "allow desktop composition:i:" ))
               {
                  /*ignore*/
               }
               else if(lsLine.StartsWith( "allow font smoothing:i:" ))
               {
                  /*ignore*/
               }
               else
               {
                  mcoMiscLines.Add( lsLine );
               }

               lsLine = loReader.ReadLine();
            }

            loReader.Close();
         }
         catch( Exception ex )
         {
            throw ex;
         }
         finally
         {
            try
            {
               if( loReader != null )
                  loReader.Close();
            }
            catch( Exception ) { /* ignore */ }
         }
      }

      public void WriteRDPConfig()
      {
         this.WriteRDPFile( this.RDPFileName );
      }

      public void ReadRDPConfig()
      {
         if( this.msId == null ) 
            return;
         
         String[] lasFiles = Directory.GetFiles( FilePath, "*rdp" );

         for(int i = 0; i < lasFiles.Length; i++)
         {
            if( msId.Equals( getIdFromRDPFile( lasFiles[i] ) ) )
            {
               this.ReadRDPFile( lasFiles[i] );
               msRDPFileName = lasFiles[i];
            }
         }
      }

      private String getIdFromRDPFile( String psFileName )
      {
         String lsId = null;
         StreamReader loReader = null;
         try
         {
            FileInfo loFile = new FileInfo( psFileName );
            loReader = new StreamReader( loFile.OpenRead() );
            String lsLine = loReader.ReadLine();
            while(lsLine != null)
            {
               if(lsLine.StartsWith( "tscm custom id:s:" ))
               {
                  lsId = lsLine.Substring( 17 );
               }
               lsLine = loReader.ReadLine();
            }

            loReader.Close();
         }
         catch(Exception ex)
         {
            throw ex;
         }
         finally
         {
            try
            {
               if(loReader != null)
                  loReader.Close();
            }
            catch(Exception) { /* ignore */ }
         }
         return lsId;
      }

      public void DeleteConfig()
      {
         try
         {
            FileInfo loFile = new FileInfo( this.RDPFileName );
            if(loFile.Exists == true)
            {
               loFile.Delete();
            }
         }
         catch( Exception ex )
         {
            throw new Exception( "Failed to delete connection", ex );
         }
      }

      private string getNewId()
      {
         /*
         DateTime loNow = DateTime.Now;
         String lsResult = loNow.DayOfYear.ToString() + loNow.Hour.ToString() +
            loNow.Minute.ToString() + loNow.Second.ToString() + loNow.Millisecond.ToString();
         return lsResult.PadRight( 12, '0' );
         */
         return Guid.NewGuid().ToString().Replace( "-", "" );
      }

      private string formatFileName( string psFileName )
      {
         char[] lasChars = psFileName.ToCharArray();
         for( int i = 0; i < lasChars.Length; i++ )
			{
			   switch( lasChars[i] )
            {
               case '\\': lasChars[i] = ' '; break;
               case '/': lasChars[i] = ' '; break;
               case '*': lasChars[i] = ' '; break;
               case '?': lasChars[i] = ' '; break;
               case ':': lasChars[i] = ' '; break;
               case '<': lasChars[i] = ' '; break;
               case '>': lasChars[i] = ' '; break;
               case '|': lasChars[i] = ' '; break;
               case '"': lasChars[i] = ' '; break;
            }
			}
         return new String( lasChars );
      }

      public override string ToString() 
      {
         return this.msName;
      }
   }

   public class ScreenSizes
   {
      public const int SZ_640x480 = 1;
      public const int SZ_800x600 = 2;
      public const int SZ_1024x768 = 4;
      public const int SZ_1280x1024 = 8;
      public const int SZ_1600x1200 = 16;

      public const int DEFAULT_SIZE = SZ_1024x768;

      public static int SizeForValue( String psValue )
      {
         if( psValue == null )
            return DEFAULT_SIZE;

         if(psValue.Equals( "640" ) || psValue.Equals( "480" ))
            return SZ_640x480;
         if(psValue.Equals( "800" ) || psValue.Equals( "600" ))
            return SZ_800x600;
         if(psValue.Equals( "1024" ) || psValue.Equals( "768" ))
            return SZ_1024x768;
         if(psValue.Equals( "1280" ) || psValue.Equals( "1024" ))
            return SZ_1280x1024;
         if(psValue.Equals( "1600" ) || psValue.Equals( "1200" ))
            return SZ_1600x1200;

         return DEFAULT_SIZE;
      }

      public static String GetWidth( int piSize )
      {
         switch(piSize)
         {
            case 0: return "0";
            case 1: return "640";
            case 2: return "800";
            case 4: return "1024";
            case 8: return "1280";
            case 16: return "1600";
            default: return "0";
         }
      }
      public static String GetHeight( int piSize )
      {
         switch(piSize)
         {
            case 0: return "0";
            case 1: return "480";
            case 2: return "600";
            case 4: return "768";
            case 8: return "1024";
            case 16: return "1200";
            default: return "0";
         }
      }
   }
}
