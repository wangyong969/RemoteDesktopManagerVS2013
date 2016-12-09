RemoteDesktopManager 1.6
------------------------

Microsoft "Remote Desktop Connection" and "Terminal Services" are becoming more and more the default protocol (also referred to as RDP) for remote computer management. 
RemoteDesktopManager helps you to keep an organized list of servers you need to connect to. 

History
-------

March 07, 2010 (1.6):
- I: 64 bit setup support
- I: Config is now written to ProgramData\RemoteDesktopManager or 
     All Users\Application Data\RemoteDesktopManager folder, this is 
	 needed for windows Vista/7 UAC
- I: Multimonitor spanning support (stretch RDP session over multiple monitors)
- I: Removed pre 1.0 upgrade support (first upgrade to 1.5 if you have an old version)
- I: Added option to edit RDP files using native Remote Desktop Connection client

September 03, 2008 (1.5):
- B: On Show click in tray menu, RDM was sent to front if already visible
- B: If no domain is set a \ was added in front of username in MSTSC connect dialog
- I: Improved netmask (read: will use the actual ip netmask) handling for RDP Discovery.

May 17, 2008 (1.4):
- I: RDP 6.1 compatibility (Vista SP1, Windows XP SP3, Windows 2008 Server)
- I: Upgraded solution to Visual Studio 2008 Express Edition (build target 
     set to .NET 2.0)
- B: RDP Discovered hosts with multiple IP addresses (but resolving to the 
     same name) are only added once

September 16, 2007 (1.3):
- B: Drag&drop fixed; Files where not copied to connection directory.
- I: Check if application is already running, if so activate/show it.

August 27, 2007 (1.2):
- I: Trayicon menu can use submenus for connections (Preference setting).
- I: If an exception occured while loading the app config. On close of the
     application an empty config was saved. Now the user is asked if RDM shows
     all that is expected.
- I: On application close the config is first written to a temp file. 
     At startup an check is added if that file still exists. It's a 
     bit paranoia I have to admit.
- B: When using a left click on the trayicon the menu would create an 
     taskbar item also.

Juli 22, 2007 (1.1):
- N: Added LAN RDP Server discovery
- N: Added trayicon for quick access to hosts
- I: Ask to delete all child notes when deleting a non-empty group
- B: duplicate button did not copy all options

Januari 28, 2007 (1.0):
- N: Group rename (select group node, edit group combo, hit save)
- I: Application renamed to RemoteDesktopManager

December 12, 2006 (0.9):
- N: Basic RDC version 6.0 support (part of Vista and update KB925876)
- N: check for update at startup
- N: setting to enable/disable update check
- N: settings dialog (a.o. for connection file location)
- N: automatic download and installation of .NET 2.0 during setup
- I: added tooltip to explain which properties are set on Maximum GUI Experience check
- I: changes to connection info now also saved on treenode doubleclick
- B: fixed group delete for groups not in tree
- B: Screensize slider and Fullscreen check eventhandling fixed

October 29, 2006 (0.8):
- N: add checkbox to set all the fancy GUI experience options
- N: add some sort of connection export feature (just opens explorer for now)
- I: center messageboxes on application (instead of desktop)
- I: present visual indicator if connection info is edited
- I: auto save connection if changed and user clicks connect
- B: connect to console check not correctly retrieved from RDP file (bug #1569635)

October 1, 2006 (0.7):
- I: for new connection set group to selected tree group (if selected)
- I: add local printers checkbox
- I: doubleclick protection for connect button
- B: size/location not correctly stored when application is minimized/maximized
- B: group dialog was not centered in front of parent
- B: after delete of connection the connection above the deleted one 
     was not selected correctly

September 10, 2006 (0.6):
- I: application window size is now saved on exit
- B: doubleclick on group node raises error
- B: fullscreen checkbox not correctly set/reset when selecting connection
- B: last selected node was not reselected on application restart

September 2, 2006 (0.5):
- N: complete redesign of connection file handling:
     - all info is stored in the RDP file
     - rdp file names are made up of connection name + group
- N: duplicate connection feature
- I: new group delete dialog (for empty groups)
- B: trim for textfield values
- B: don't write rdp port for default value 3389
- B: various other tweaks and fixes

April 17, 2006 (0.4): 
- Better tree/settings/file sync code
- new about screen
- backwards compatible with old preview release (not on sf)
- some code refactoring (changed names, moved code to Utility class)

April 14, 2006 (0.3): 
- First release.


Credits
-------

- SmartThreadPool Code (used in RDP Discovery dialog):
Ami Bar (http://www.codeproject.com/cs/threads/smartthreadpool.asp)

- Drag&Drop TreeView Code:
Gabe Anguiano (http://www.codeproject.com/cs/miscctrl/TreeViewReArr.asp)

- Silk icon set 1.3:
Mark James (http://www.famfamfam.com/lab/icons/silk/)