; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Search Book"
#define MyAppVersion "1.0.171222"
#define MyAppPublisher "KoBold, Inc."
#define MyAppURL "null"
#define MyAppExeName "SearchBook.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{C03AAC0D-7546-4B2E-B11A-9B4521271C92}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename=setup
SetupIconFile=C:\Users\28076\Downloads\setup.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\BootstrapWpfStyle.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\BootstrapWpfStyle.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\CommonServiceLocator.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.Extras.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.Extras.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.Platform.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.Platform.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\GalaSoft.MvvmLight.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.application"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\SearchBook.Service.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\�ҵ���Ŀ\SearchBook\Search-book\SearchBook\bin\Release\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
