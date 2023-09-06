// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.SelectLoginUIState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Extensions;
using Ankama.Cube.Network;
using Ankama.Cube.Player;
using Ankama.Cube.UI;
using Ankama.Cube.Utility;
using Com.Ankama.Haapi.Swagger.Api;
using Com.Ankama.Haapi.Swagger.Model;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class SelectLoginUIState : LoadSceneStateContext
  {
    private SelectLoginUI m_ui;

    protected override IEnumerator Load()
    {
      SelectLoginUIState uiloader = this;
      LoadSceneStateContext.UILoader<SelectLoginUI> loader = new LoadSceneStateContext.UILoader<SelectLoginUI>((LoadSceneStateContext) uiloader, "SelectLoginUI", "core/scenes/ui/login", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.gameObject.SetActive(true);
      if (ApplicationConfig.haapiAllowed)
      {
        uiloader.m_ui.OnConnectGuest += new Action(uiloader.OnConnectGuest);
        uiloader.m_ui.OnCreateGuest += new Action(uiloader.OnCreateGuest);
      }
      else
        uiloader.m_ui.HideGuestSelection();
      uiloader.m_ui.OnRegularAccount += new Action(uiloader.OnRegularAccount);
    }

    protected override IEnumerator Unload()
    {
      SelectLoginUIState selectLoginUiState = this;
      // ISSUE: reference to a compiler-generated method
      yield return (object) selectLoginUiState.\u003C\u003En__0();
      if (ApplicationConfig.haapiAllowed)
      {
        selectLoginUiState.m_ui.OnConnectGuest -= new Action(selectLoginUiState.OnConnectGuest);
        selectLoginUiState.m_ui.OnCreateGuest -= new Action(selectLoginUiState.OnCreateGuest);
      }
      selectLoginUiState.m_ui.OnRegularAccount -= new Action(selectLoginUiState.OnRegularAccount);
    }

    private void OnRegularAccount()
    {
      this.m_ui.interactable = false;
      StatesUtility.DoTransition((StateContext) new LoginUIState(), (StateContext) this);
    }

    private void OnCreateGuest()
    {
      this.m_ui.interactable = false;
      HaapiManager.ExecuteRequest<RAccountApi<Account>>((Func<RAccountApi<Account>>) (() => HaapiManager.accountApi.CreateGuest(new long?((long) ApplicationConfig.GameAppId), RuntimeData.currentCultureCode.GetLanguage(), string.Empty, string.Empty)), new Action<RAccountApi<Account>>(this.OnCreateGuestSuccess), new Action<System.Exception>(this.OnCreateGuestError));
    }

    private void OnCreateGuestSuccess(RAccountApi<Account> response)
    {
      string firstHeaderValue = response.RestResponse.Headers.GetFirstHeaderValue("X-Password");
      PlayerPreferences.guestLogin = response.Data.Login;
      PlayerPreferences.guestPassword = firstHeaderValue;
      PlayerPreferences.Save();
      ConnectionHandler.instance.Connect();
    }

    private void OnCreateGuestError(System.Exception obj) => this.m_ui.interactable = true;

    private void OnConnectGuest() => ConnectionHandler.instance.Connect();
  }
}
