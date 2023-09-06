// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.NoConnection
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using Ankama.Launcher.Messages;
using System;
using UnityEngine;

namespace Ankama.Launcher
{
  public class NoConnection : ILauncherLink
  {
    public static readonly ILauncherLink instance = (ILauncherLink) new NoConnection();

    private NoConnection()
    {
    }

    public bool RequestApiToken(
      int serviceId,
      Action<ApiToken> tokenCallback,
      Action<Exception> errorCallback)
    {
      Debug.LogError((object) "Error : no connection !!!!!");
      errorCallback(new Exception("LauncherLInk not connected"));
      return false;
    }

    public bool RequestLanguage(Action<string> langCallback, Action<Exception> errorCallback)
    {
      errorCallback(new Exception("LauncherLink not connected"));
      return false;
    }

    public bool UpdateLanguage(
      string lang,
      Action<bool> successCallback,
      Action<Exception> errorCallback)
    {
      errorCallback(new Exception("LauncherLink not connected"));
      return false;
    }

    public bool opening => false;

    public bool opened => false;

    public bool isSteam => false;
  }
}
