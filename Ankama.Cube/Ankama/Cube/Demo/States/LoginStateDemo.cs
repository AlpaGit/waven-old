// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.States.LoginStateDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Configuration;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.Network;
using Ankama.Cube.Player;
using Ankama.Cube.States;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Demo.States
{
  public class LoginStateDemo : LoadSceneStateContext
  {
    private LoginUIDemo m_ui;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      LoginStateDemo uiloader = this;
      LoadSceneStateContext.UILoader<LoginUIDemo> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(false);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<LoginUIDemo>((LoadSceneStateContext) uiloader, "LoginUIDemo", "demo/scenes/ui/login", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      this.m_ui.gameObject.SetActive(true);
      yield return (object) this.m_ui.OpenCoroutine();
      yield return (object) base.Update();
    }

    protected override void Enable()
    {
      this.m_ui.onConnect = new Action<bool, string>(this.Connect);
      if (ApplicationConfig.debugMode)
        StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.F12, 5));
      ConnectionHandler.instance.OnConnectionStatusChanged += new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
    }

    public override bool AllowsTransition([CanBeNull] StateContext nextState) => nextState is MainStateDemo;

    protected override IEnumerator Transition([CanBeNull] StateTransitionInfo transitionInfo)
    {
      yield return (object) this.m_ui.CloseCoroutine();
    }

    protected override void Disable()
    {
      this.m_ui.onConnect = (Action<bool, string>) null;
      this.m_ui.gameObject.SetActive(false);
      if (ApplicationConfig.debugMode)
        StateManager.UnregisterInputDefinition(5);
      ConnectionHandler.instance.OnConnectionStatusChanged -= new ConnectionHandler.ConnectionStatusChangedHandler(this.OnConnectionStatusChanged);
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      switch (inputState.id)
      {
        case 2:
        case 3:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.DoClickSelected();
          return true;
        case 4:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.SelectNext();
          return true;
        case 5:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
          {
            LoginDebugUI componentInChildren = this.m_ui.GetComponentInChildren<LoginDebugUI>(true);
            componentInChildren.gameObject.SetActive(!componentInChildren.gameObject.activeSelf);
          }
          return true;
        default:
          return base.UseInput(inputState);
      }
    }

    private void LockUI(bool value) => this.m_ui.canvasGroup.interactable = !value;

    private void Connect(bool asGuest, string login)
    {
      if (string.IsNullOrEmpty(login))
      {
        PopupInfoManager.Show(StateManager.GetDefaultLayer().GetChainEnd(), new PopupInfo()
        {
          message = (RawTextData) 98703,
          buttons = new ButtonData[1]
          {
            new ButtonData((TextData) 27169)
          },
          selectedButton = 1,
          style = PopupStyle.Normal
        });
      }
      else
      {
        this.LockUI(true);
        PlayerPreferences.lastLogin = login;
        if (string.IsNullOrWhiteSpace(PlayerPreferences.lastPassword))
          PlayerPreferences.lastPassword = "pass";
        PlayerPreferences.Save();
        ConnectionHandler.instance.Connect();
      }
    }

    private void OnPlayerDataInitialized(bool pendigFightFound)
    {
      PlayerData.OnPlayerDataInitialized -= new PlayerData.PlayerDataInitialized(this.OnPlayerDataInitialized);
      this.parent.SetChildState((StateContext) new MainStateDemo());
    }

    public void OnConnectionStatusChanged(
      ConnectionHandler.Status from,
      ConnectionHandler.Status to)
    {
      if (!(bool) (UnityEngine.Object) this.m_ui)
        return;
      switch (to)
      {
        case ConnectionHandler.Status.Disconnected:
          this.LockUI(false);
          break;
        case ConnectionHandler.Status.Connecting:
          this.LockUI(true);
          break;
        case ConnectionHandler.Status.Connected:
          PlayerData.OnPlayerDataInitialized += new PlayerData.PlayerDataInitialized(this.OnPlayerDataInitialized);
          this.LockUI(true);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (to), (object) to, (string) null);
      }
    }
  }
}
