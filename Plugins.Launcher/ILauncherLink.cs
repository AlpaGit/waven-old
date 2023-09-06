// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.ILauncherLink
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using Ankama.Launcher.Messages;
using System;

namespace Ankama.Launcher
{
  public interface ILauncherLink
  {
    bool opening { get; }

    bool opened { get; }

    bool RequestApiToken(
      int serviceId,
      Action<ApiToken> tokenCallback,
      Action<Exception> errorCallback);

    bool RequestLanguage(Action<string> langCallback, Action<Exception> errorCallback);

    bool UpdateLanguage(string lang, Action<bool> successCallback, Action<Exception> errorCallback);

    bool isSteam { get; }
  }
}
