// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ApplicationConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.IO;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public static class ApplicationConfig
  {
    private static readonly Options s_options = new Options();
    public static Action OnServerConfigLoaded;
    public static VersionChecker.Result versionCheckResult = VersionChecker.Result.None;
    public static ServerStatus serverStatus = ServerStatus.none;
    private static int m_gameAppId = 22;
    private static int m_chatAppId = 99;

    public static string persistentArgsFile => Application.persistentDataPath + "/commandline.txt";

    public static bool initialized { get; private set; }

    public static int GameAppId
    {
      get => ApplicationConfig.m_gameAppId != -1 ? ApplicationConfig.m_gameAppId : throw new Exception("GameAppId not initialized");
      private set => ApplicationConfig.m_gameAppId = value;
    }

    public static int ChatAppId
    {
      get => ApplicationConfig.m_chatAppId != -1 ? ApplicationConfig.m_chatAppId : throw new Exception("ChatAppId not initialized");
      private set => ApplicationConfig.m_chatAppId = value;
    }

    [NotNull]
    public static string gameServerHost { get; private set; } = string.Empty;

    public static int gameServerPort { get; private set; } = -1;

    public static bool gameServerIsLocal { get; private set; } = false;

    public static RemoteConfig.ServerProfile gameServerProfile { get; private set; }

    [NotNull]
    public static string bundlesUrl { get; private set; } = string.Empty;

    [NotNull]
    public static string versionFileUrl { get; private set; } = string.Empty;

    [NotNull]
    public static string haapiServerUrl { get; private set; } = string.Empty;

    [NotNull]
    public static string langCode { get; private set; } = string.Empty;

    public static bool debugMode { get; private set; }

    public static bool haapiAllowed { get; private set; } = true;

    public static bool simulateDemo { get; private set; }

    public static bool showServerSelection { get; private set; }

    [NotNull]
    public static string configUrl { get; private set; } = string.Empty;

    public static bool enableBugReport => UnityRemoteSettings.GetEnableBugReport(ApplicationConfig.gameServerIsLocal, ApplicationConfig.gameServerProfile, false);

    static ApplicationConfig()
    {
      ApplicationConfig.s_options.Register(nameof (langCode), (Action<string>) (v => ApplicationConfig.langCode = v.ToUpper()), "FR|EN");
      ApplicationConfig.s_options.Register("debug", (Action<bool>) (v => ApplicationConfig.debugMode = v));
      ApplicationConfig.s_options.Register("no-haapi", (Action<bool>) (v => ApplicationConfig.haapiAllowed = !v));
      ApplicationConfig.s_options.Register("forceValidVersion", (Action<bool>) (v => ApplicationConfig.versionCheckResult = VersionChecker.Result.Success));
      ApplicationConfig.s_options.Register("demo", (Action<bool>) (v => ApplicationConfig.simulateDemo = v));
      ApplicationConfig.s_options.Register(nameof (showServerSelection), (Action<bool>) (v => ApplicationConfig.showServerSelection = v));
      ApplicationConfig.s_options.Register(nameof (configUrl), (Action<string>) (v => ApplicationConfig.configUrl = v.Trim('"', ' ', '\t')), "file|http://path/to/config.json");
    }

    public static void Read()
    {
      ApplicationConfig.ReadFromPersistentData();
      ApplicationConfig.ReadFromCommandLine();
      if (!ApplicationConfig.showServerSelection)
        return;
      ApplicationConfig.initialized = true;
    }

    private static void ReadFromPersistentData()
    {
      string persistentArgsFile = ApplicationConfig.persistentArgsFile;
      Log.Info("Read arguments from " + persistentArgsFile, 175, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationConfig.cs");
      try
      {
        if (!File.Exists(persistentArgsFile))
          return;
        ApplicationConfig.ReadFromArguments(File.ReadAllLines(persistentArgsFile));
      }
      catch (Exception ex)
      {
        Log.Warning((object) ex, 186, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationConfig.cs");
      }
    }

    private static void ReadFromCommandLine() => ApplicationConfig.ReadFromArguments(Environment.GetCommandLineArgs());

    private static void ReadFromArguments(string[] args)
    {
      for (int index = 0; index < args.Length; ++index)
      {
        string str = args[index];
        if (!string.IsNullOrEmpty(str))
          ApplicationConfig.s_options.ParseArgument(str.Trim(), ref index);
      }
    }

    public static string Usage() => ApplicationConfig.s_options.Usage();

    public static void SetFromRemoteConfig(RemoteConfig config)
    {
      ApplicationConfig.gameServerHost = config.gameServerHost;
      ApplicationConfig.gameServerPort = config.gameServerPort;
      ApplicationConfig.gameServerIsLocal = config.gameServerIsLocal;
      ApplicationConfig.gameServerProfile = config.gameServerProfile;
      ApplicationConfig.GameAppId = config.gameAppId;
      ApplicationConfig.ChatAppId = config.chatAppId;
      ApplicationConfig.bundlesUrl = config.bundlesUrl;
      ApplicationConfig.versionFileUrl = config.versionFileUrl;
      ApplicationConfig.haapiServerUrl = config.haapiServerUrl;
      ApplicationConfig.initialized = true;
      Action serverConfigLoaded = ApplicationConfig.OnServerConfigLoaded;
      if (serverConfigLoaded == null)
        return;
      serverConfigLoaded();
    }

    public static void SetServerInfo(ServerList.ServerInfo info)
    {
      ApplicationConfig.gameServerHost = info.host;
      ApplicationConfig.gameServerPort = info.port;
      ApplicationConfig.gameServerIsLocal = info.isLocal;
      ApplicationConfig.gameServerProfile = info.profile;
      ApplicationConfig.GameAppId = info.gameAppId;
      ApplicationConfig.ChatAppId = info.chatAppId;
      ApplicationConfig.bundlesUrl = info.bundlesUrl;
      ApplicationConfig.haapiServerUrl = info.haapiServerUrl;
      ApplicationConfig.initialized = true;
      Action serverConfigLoaded = ApplicationConfig.OnServerConfigLoaded;
      if (serverConfigLoaded == null)
        return;
      serverConfigLoaded();
    }

    public static bool IsVersionValid()
    {
      switch (ApplicationConfig.versionCheckResult)
      {
        case VersionChecker.Result.Success:
        case VersionChecker.Result.PatchAvailable:
          return true;
        case VersionChecker.Result.UpdateNeeded:
        case VersionChecker.Result.VersionFileError:
        case VersionChecker.Result.RuntimeError:
          return false;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static void PrintConfig() => Log.Info(string.Format("Config: {{\ndebugMode: '{0}'\nhaapiAllowed: '{1}'\nlangCode: '{2}'\nconfigUrl: '{3}'\nbundlesUrl: '{4}'\nversionFileUrl: '{5}'\nhaapiServerUrl: '{6}'\nGameAppId: '{7}'\nChatAppId: '{8}'\ngameServerHost: '{9}'\ngameServerPort: '{10}'\ngameServerIsLocal: '{11}'\ngameServerProfile: '{12}'\n}}", (object) ApplicationConfig.debugMode, (object) ApplicationConfig.haapiAllowed, (object) ApplicationConfig.langCode, (object) ApplicationConfig.configUrl, (object) ApplicationConfig.bundlesUrl, (object) ApplicationConfig.versionFileUrl, (object) ApplicationConfig.haapiServerUrl, (object) ApplicationConfig.GameAppId, (object) ApplicationConfig.ChatAppId, (object) ApplicationConfig.gameServerHost, (object) ApplicationConfig.gameServerPort, (object) ApplicationConfig.gameServerIsLocal, (object) ApplicationConfig.gameServerProfile), 287, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\ApplicationConfig.cs");
  }
}
