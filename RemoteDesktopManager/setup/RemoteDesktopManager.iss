#define MyAppName "RemoteDesktopManager"
#define MyAppVersionNr "1.6"
#define MyBaseDir "C:\CSharpProjects\RemoteDesktopManager\RemoteDesktopManager"
#define MyAppPublisher "Daniel Knippers"
#define MyAppURL "http://sourceforge.net/projects/tscm/"
#define MyAppExeName "RemoteDesktopManager.exe"
#define MyAppUrlName "RemoteDesktopManager.url"

[Setup]
AppName={#MyAppName}
AppVerName={#MyAppName} {#MyAppVersionNr}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=true
LicenseFile={#MyBaseDir}\setup\LICENSE.txt
InfoBeforeFile={#MyBaseDir}\setup\README.txt
OutputDir={#MyBaseDir}\setup
OutputBaseFilename=setup_{#MyAppVersionNr}
SetupIconFile={#MyBaseDir}\images\mstsc.ico
Compression=lzma
SolidCompression=true
PrivilegesRequired=poweruser
VersionInfoVersion={#MyAppVersionNr}
ArchitecturesAllowed=x86 x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64

[Languages]
Name: english; MessagesFile: compiler:Default.isl

[Tasks]
Name: desktopicon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked
Name: quicklaunchicon; Description: {cm:CreateQuickLaunchIcon}; GroupDescription: {cm:AdditionalIcons}; Flags: unchecked

[Dirs]
Name: {commonappdata}\{#MyAppName}

[Files]
; temp copy block for upgrade from 1.5 to 1.6
Source: {app}\connections\*; DestDir: {commonappdata}\{#MyAppName}\connections; Flags: external skipifsourcedoesntexist
Source: {app}\{#MyAppName}_config.xml; DestDir: {commonappdata}\{#MyAppName}; Flags: external skipifsourcedoesntexist

Source: {commonappdata}\{#MyAppName}\connections\*; DestDir: {localappdata}\{#MyAppName}\backup; Flags: external skipifsourcedoesntexist uninsneveruninstall
Source: {commonappdata}\{#MyAppName}\{#MyAppName}_config.xml; DestDir: {localappdata}\{#MyAppName}\backup; Flags: external skipifsourcedoesntexist uninsneveruninstall

Source: {#MyBaseDir}\bin\Release\RemoteDesktopManager.exe; DestDir: {app}; Flags: ignoreversion
Source: {#MyBaseDir}\setup\README.txt; DestDir: {app}; Flags: ignoreversion
Source: {#MyBaseDir}\setup\LICENSE.txt; DestDir: {app}; Flags: ignoreversion
Source: {#MyBaseDir}\setup\isxdl.dll; DestDir: {app}; Flags: ignoreversion deleteafterinstall

[INI]
Filename: {app}\{#MyAppUrlName}; Section: InternetShortcut; Key: URL; String: {#MyAppURL}

[Icons]
Name: {group}\{#MyAppName}; Filename: {app}\{#MyAppExeName}
Name: {group}\{cm:ProgramOnTheWeb,{#MyAppName}}; Filename: {app}\{#MyAppUrlName}
Name: {group}\{cm:UninstallProgram,{#MyAppName}}; Filename: {uninstallexe}
Name: {userdesktop}\{#MyAppName}; Filename: {app}\{#MyAppExeName}; Tasks: desktopicon
Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}; Filename: {app}\{#MyAppExeName}; Tasks: quicklaunchicon

[Run]
Filename: {app}\{#MyAppExeName}; Description: {cm:LaunchProgram,{#MyAppName}}; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: files; Name: {app}\{#MyAppUrlName}
Type: filesandordirs; Name: {commonappdata}\{#MyAppName}\connections
Type: files; Name: {commonappdata}\{#MyAppName}\{#MyAppName}_config.xml
Type: dirifempty; Name: {commonappdata}\{#MyAppName}\connections
Type: dirifempty; Name: {commonappdata}\{#MyAppName}

[Code]
var
   IsAlreadyInstalled : Boolean;
   DotNetRedistPath: String;
   DotNetDownloadNeeded: Boolean;
   DependenciesNeeded: String;

   procedure isxdl_AddFile(URL, Filename: PAnsiChar);
     external 'isxdl_AddFile@files:isxdl.dll stdcall';
   function isxdl_DownloadFiles(hWnd: Integer): Integer;
     external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
   function isxdl_SetOption(Option, Value: PAnsiChar): Integer;
     external 'isxdl_SetOption@files:isxdl.dll stdcall';

const
  DotNetRedistURL = 'http://download.microsoft.com/download/5/6/7/567758a3-759e-473e-bf8f-52154438565a/dotnetfx.exe';

/////////////////////////////////////////////////////////////////////////////////////
// common functions
//

function GetPathInstalled( AppID: String ): String;
var
  sPrevPath: String;
begin
  sPrevPath := '';
  if not RegQueryStringValue
  (
    HKLM,
    'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1',
    'Inno Setup: App Path', sPrevpath
  ) then
  begin
    RegQueryStringValue
    (
      HKCU,
      'Software\Microsoft\Windows\CurrentVersion\Uninstall\'+AppID+'_is1',
      'Inno Setup: App Path', sPrevpath
    );
  end;

  Result := sPrevPath;
end;

function IsDotNetInstalled(): Boolean;
var
  Net2Installed: Cardinal;
  lbResult: Boolean;
begin
  lbResult := false;
  if RegQueryDWordValue
  (
     HKLM,
     'Software\Microsoft\Net Framework Setup\NDP\v2.0.50727',
     'Install', Net2Installed
  ) then
  begin
    if Net2Installed = 1 then
    begin
       lbResult := true;
    end;
  end;
  Result := lbResult;
end;

/////////////////////////////////////////////////////////////////////////////////////
// Setup functions
//

function InitializeSetup(): Boolean;
begin
   Result := true;
   if IsDotNetInstalled() = false then
   begin
      if (not IsAdminLoggedOn() ) then
      begin
         MsgBox('{#MyAppName} needs the Microsoft .NET Framework 2.0 to be installed by an Administrator', mbInformation, MB_OK);
         Result := false;
      end else
      begin
         DependenciesNeeded := DependenciesNeeded + '.NET Framework 2.0' #13;
         DotNetRedistPath := ExpandConstant('{src}\dotnetfx.exe');
         if not FileExists(DotNetRedistPath) then
         begin
            isxdl_AddFile(DotNetRedistURL, DotNetRedistPath);
            DotNetDownloadNeeded := true;
         end;
         SetIniString('install', 'DotNetRedist', DotNetRedistPath, ExpandConstant('{tmp}\dep.ini'));
      end;
   end;
end;

procedure InitializeWizard();
begin
   if Length( GetPathInstalled( '{#MyAppName}' ) ) > 0 then
   begin
      IsAlreadyInstalled := true;
   end
   else
   begin
      IsAlreadyInstalled := false;
   end;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  if IsAlreadyInstalled = true then
  begin
	  if (PageID = wpSelectDir) then
		 Result := True
	  else if (PageID = wpSelectProgramGroup) then
		 Result := True
	  else if (PageID = wpSelectTasks) then
		 Result := True
	  else
		 Result := False;
  end
  else
  begin
    Result := false;
  end;

end;

procedure CurPageChanged(CurPageID: Integer);
begin
  if (CurPageID = wpReady) and IsAlreadyInstalled then
  begin
     WizardForm.NextButton.Caption := 'Update';
  end;
end;

function NextButtonClick(CurPage: Integer): Boolean;
var
  hWnd: Integer;
  ResultCode: Integer;
begin
  Result := true;

  if CurPage = wpReady then
  begin
    hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));

    // don't try to init isxdl if it's not needed because it will error on < ie 3
    if DotNetDownloadNeeded then
    begin
      isxdl_SetOption('label', 'Downloading Microsoft .NET Framework 2.0');
      isxdl_SetOption('description', '{#MyAppName} needs to install the Microsoft .NET Framework 2.0. Please wait while Setup is downloading extra files to your computer.');
      if isxdl_DownloadFiles(hWnd) = 0 then
      begin
         Result := false;
      end;
    end;
    if (Result = true) and (IsDotNetInstalled() = false) then
    begin
      if Exec(ExpandConstant(DotNetRedistPath), '', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
      begin
			// handle success if necessary; ResultCode contains the exit code
			if not (ResultCode = 0) then
			begin
			  Result := false;
			end;
      end else
      begin
        // handle failure if necessary; ResultCode contains the error code
        Result := false;
      end;
    end;
  end;
end;

function UpdateReadyMemo(Space, NewLine, MemoUserInfoInfo, MemoDirInfo, MemoTypeInfo, MemoComponentsInfo, MemoGroupInfo, MemoTasksInfo: String): String;
var
  s: string;
begin
   if DependenciesNeeded <> '' then
   begin
      s := s + 'Dependencies that will be automatically downloaded And installed:' + NewLine +
         DependenciesNeeded + NewLine;
   end;

   Result := s + MemoDirInfo + NewLine + NewLine;
end;
