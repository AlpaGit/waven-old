// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.RemoteConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public class RemoteConfig
  {
    public string gameServerDisplayName { get; private set; } = string.Empty;

    public string gameServerHost { get; private set; } = string.Empty;

    public int gameServerPort { get; private set; } = -1;

    public int gameAppId { get; private set; } = -1;

    public int chatAppId { get; private set; } = -1;

    public bool gameServerIsLocal { get; private set; }

    public RemoteConfig.ServerProfile gameServerProfile { get; private set; }

    public string bundlesUrl { get; private set; } = string.Empty;

    public string versionFileUrl { get; private set; } = string.Empty;

    public string haapiServerUrl { get; private set; } = string.Empty;

    public static RemoteConfig From(ConfigReader reader)
    {
      if (reader == null)
        return (RemoteConfig) null;
      RemoteConfig remoteConfig = new RemoteConfig()
      {
        gameServerHost = reader.GetString("gameServerHost"),
        gameServerPort = reader.GetInt("gameServerPort", -1),
        gameAppId = reader.GetInt("gameAppId", -1),
        chatAppId = reader.GetInt("chatAppId", -1),
        gameServerIsLocal = reader.GetBool("gameServerIsLocal"),
        gameServerProfile = reader.GetEnum<RemoteConfig.ServerProfile>("gameServerProfile", RemoteConfig.ServerProfile.None),
        versionFileUrl = RemoteConfig.ReplaceVars(reader.GetUrl("versionFileUrl")),
        haapiServerUrl = reader.GetString("haapiServerUrl")
      };
      remoteConfig.gameServerDisplayName = !reader.HasProperty("gameServerDisplayName") ? remoteConfig.gameServerProfile.ToString() : reader.GetString("gameServerDisplayName");
      remoteConfig.bundlesUrl = RemoteConfig.ReplaceVars(reader.GetUrl("bundlesUrl"));
      return remoteConfig;
    }

    public static string ReplaceVars(string txt)
    {
      string newValue;
      switch (Application.platform)
      {
        case RuntimePlatform.OSXEditor:
        case RuntimePlatform.OSXPlayer:
          newValue = "macosx";
          break;
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.WindowsEditor:
          newValue = "windows";
          break;
        case RuntimePlatform.IPhonePlayer:
          newValue = "ios";
          break;
        case RuntimePlatform.Android:
          newValue = "android";
          break;
        default:
          throw new ArgumentOutOfRangeException("platform", "Unsupported platform.");
      }
      return txt.Replace("$%7Bplatform%7D", newValue).Replace("${platform}", newValue).Replace("$%7Bversion%7D", "0.1.1.6169").Replace("$%7Bbuild%7D", 6169.ToString());
    }

    public enum ServerProfile
    {
      None,
      [UsedImplicitly] Development,
      [UsedImplicitly] Beta,
      [UsedImplicitly] Production,
    }
  }
}
