// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.LoginUIState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Network;
using Ankama.Cube.Player;
using Ankama.Cube.TEMPFastEnterMatch.Auth;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Ankama.Cube.Utility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.States
{
  public class LoginUIState : LoadSceneStateContext
  {
    private LoginUI m_ui;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      LoginUIState uiloader = this;
      LoadSceneStateContext.UILoader<LoginUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.SetParams(PlayerPreferences.lastLogin, PlayerPreferences.lastPassword, PlayerPreferences.lastServer);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<LoginUI>((LoadSceneStateContext) uiloader, "LoginUI", "core/scenes/ui/login");
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable()
    {
      this.m_ui.connect = new Action(this.TryToConnect);
      this.m_ui.onBackToGuest = new Action(this.OnBackToGuest);
      ConnectionHandler.instance.OnConnectionStatusChanged += new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      switch (inputState.id)
      {
        case 2:
        case 3:
          this.TryToConnect();
          return true;
        case 4:
          this.m_ui.SelectNext();
          return true;
        default:
          return base.UseInput(inputState);
      }
    }

    private void OnConnectionStatusChanged(
      ConnectionHandler.Status from,
      ConnectionHandler.Status to)
    {
      switch (to)
      {
        case ConnectionHandler.Status.Disconnected:
          if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
            break;
          this.m_ui.interactable = true;
          break;
        case ConnectionHandler.Status.Connecting:
        case ConnectionHandler.Status.Connected:
          if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
            break;
          this.m_ui.interactable = false;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (to), (object) to, (string) null);
      }
    }

    protected override void Disable()
    {
      this.m_ui.connect = (Action) null;
      this.m_ui.onBackToGuest = (Action) null;
      this.m_ui.gameObject.SetActive(false);
      ConnectionHandler.instance.OnConnectionStatusChanged -= new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
    }

    private void OnBackToGuest() => StatesUtility.DoTransition((StateContext) new SelectLoginUIState(), (StateContext) this);

    private void TryToConnect()
    {
      this.m_ui.interactable = false;
      PlayerPreferences.lastLogin = this.m_ui.login;
      PlayerPreferences.lastPassword = this.m_ui.password;
      ServerList.ServerInfo serverInfo = this.m_ui.serverList.GetServerInfo(this.m_ui.serverIndex);
      ApplicationConfig.SetServerInfo(serverInfo);
      ApplicationConfig.versionCheckResult = VersionChecker.Result.Success;
      PlayerPreferences.lastServer = serverInfo.displayName;
      PlayerPreferences.Save();
      this.DoConnect();
    }

    private void DoConnect()
    {
      ApplicationConfig.PrintConfig();
      this.ReinitConnections();
      this.m_ui.interactable = false;
      ConnectionHandler.instance.Connect();
    }

    private void ReinitConnections()
    {
      CredentialProvider.DeteteCredentialProviders();
      ConnectionHandler.instance.OnConnectionStatusChanged -= new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
      ConnectionHandler.Destroy();
      if (ApplicationConfig.haapiAllowed)
        HaapiManager.Initialize();
      ConnectionHandler.Initialize();
      ConnectionHandler.instance.OnConnectionStatusChanged += new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
    }

    private void OnNicknameResult(bool success, IList<string> suggests, string key, string text)
    {
    }

    private void OnNicknameRequest()
    {
    }
  }
}
