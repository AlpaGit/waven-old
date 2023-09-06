// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.UnityRemoteSettings
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public class UnityRemoteSettings
  {
    public static JObject m_enableBugReport;

    public static bool ready { get; private set; }

    public static bool GetEnableBugReport(
      bool gameServerIsLocal,
      RemoteConfig.ServerProfile gameServerProfile,
      bool defaultValue)
    {
      if (UnityRemoteSettings.m_enableBugReport == null)
        return defaultValue;
      string propertyName = UnityRemoteSettings.Identifier(gameServerIsLocal, gameServerProfile);
      JToken jtoken;
      return UnityRemoteSettings.m_enableBugReport.TryGetValue(propertyName, out jtoken) ? jtoken.Value<bool>() : defaultValue;
    }

    public static void Load()
    {
      RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(UnityRemoteSettings.RefreshSettings);
      RemoteSettings.Completed += new Action<bool, bool, int>(UnityRemoteSettings.UnityRemoteSettingsCompleted);
    }

    private static void UnityRemoteSettingsCompleted(
      bool wasUpdatedFromServer,
      bool settingsChanged,
      int serverResponse)
    {
      if (serverResponse != 200)
      {
        UnityRemoteSettings.ready = false;
        Log.Warning(string.Format("Could not get remote settings serverResponse={0}", (object) serverResponse), 39, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\UnityRemoteSettings.cs");
      }
      else
      {
        UnityRemoteSettings.ready = true;
        UnityRemoteSettings.RefreshSettings();
        Log.Info(string.Format("Unity remote settings refreshed (fromServer={0})", (object) wasUpdatedFromServer), 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\UnityRemoteSettings.cs");
      }
    }

    private static void RefreshSettings()
    {
      string json = RemoteSettings.GetString("enable-bug-report");
      try
      {
        UnityRemoteSettings.m_enableBugReport = JObject.Parse(json);
      }
      catch (Exception ex)
      {
        Log.Error("Remote settings error: enableBugReport=" + json, (object) ex, 58, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\UnityRemoteSettings.cs");
      }
    }

    private static string Identifier(
      bool gameServerIsLocal,
      RemoteConfig.ServerProfile gameServerProfile)
    {
      string str1 = gameServerIsLocal ? "internal" : "production";
      string str2;
      switch (gameServerProfile)
      {
        case RemoteConfig.ServerProfile.None:
          str2 = "dev";
          break;
        case RemoteConfig.ServerProfile.Development:
          str2 = "dev";
          break;
        case RemoteConfig.ServerProfile.Beta:
          str2 = "beta";
          break;
        case RemoteConfig.ServerProfile.Production:
          str2 = "main";
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      return str1 + "-" + str2;
    }
  }
}
