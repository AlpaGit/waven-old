// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.LoginUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.Extensions;
using Ankama.Cube.Player;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth
{
  public class LoginUI : AbstractUI
  {
    [SerializeField]
    private InputTextField m_login;
    [SerializeField]
    private InputTextField m_password;
    [SerializeField]
    private Button m_loginButton;
    [SerializeField]
    private Button m_backToGuestButton;
    [SerializeField]
    private CustomDropdown m_serversDropdown;
    [SerializeField]
    private CustomDropdown m_startStateDropDown;
    [SerializeField]
    private ServerList m_serverList;
    [NonSerialized]
    public Action connect;
    [NonSerialized]
    public Action onBackToGuest;
    private int m_selectedIndex;

    public ServerList serverList => this.m_serverList;

    public string login => this.m_login.GetText();

    public string password => this.m_password.GetText();

    public int serverIndex => this.m_serversDropdown.value;

    protected override void Awake()
    {
      this.m_backToGuestButton.gameObject.SetActive(CredentialProvider.HasGuestMode());
      this.m_loginButton.onClick.AddListener(new UnityAction(this.OnLoginButtonClicked));
      this.m_backToGuestButton.onClick.AddListener(new UnityAction(this.OnBackToGuestClick));
      this.m_startStateDropDown.AddOptions(((IEnumerable<LoginUI.StartState>) EnumUtility.GetValues<LoginUI.StartState>()).Select<LoginUI.StartState, string>((Func<LoginUI.StartState, string>) (s => s.ToString())).ToList<string>());
      this.m_startStateDropDown.value = PlayerPreferences.startState;
      this.m_startStateDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnStartStateChanged));
      PlayerPreferences.useGuest = false;
    }

    public void SelectNext()
    {
      this.m_selectedIndex = (this.m_selectedIndex + 1) % 2;
      switch (this.m_selectedIndex)
      {
        case 0:
          this.m_login.selectable.Select();
          break;
        case 1:
          this.m_password.selectable.Select();
          break;
      }
    }

    private void OnBackToGuestClick()
    {
      Action onBackToGuest = this.onBackToGuest;
      if (onBackToGuest == null)
        return;
      onBackToGuest();
    }

    public void SetParams(string login, string password, string serverId)
    {
      this.m_login.SetText(PlayerPreferences.lastLogin);
      this.m_password.SetText(PlayerPreferences.lastPassword);
      this.InitializeServerList(serverId);
      this.m_login.selectable.Select();
    }

    private void OnLoginButtonClicked()
    {
      PlayerPreferences.lastLogin = this.m_login.GetText();
      PlayerPreferences.lastPassword = this.m_password.GetText();
      PlayerPreferences.Save();
      Action connect = this.connect;
      if (connect == null)
        return;
      connect();
    }

    private void OnSelectServer(int serverIndex)
    {
      ServerList.ServerInfo serverInfo = this.m_serverList.GetServerInfo(serverIndex);
      ApplicationConfig.SetServerInfo(serverInfo);
      ApplicationConfig.versionCheckResult = VersionChecker.Result.Success;
      PlayerPreferences.lastServer = serverInfo.displayName;
      PlayerPreferences.Save();
    }

    private void OnStartStateChanged(int stateIndex)
    {
      PlayerPreferences.startState = stateIndex;
      PlayerPreferences.Save();
    }

    private void InitializeServerList(string defaultServer)
    {
      List<string> list = ((IEnumerable<ServerList.ServerInfo>) this.m_serverList.GetAllServers()).Select<ServerList.ServerInfo, string>((Func<ServerList.ServerInfo, string>) (s => s.displayName)).ToList<string>();
      this.m_serversDropdown.AddOptions(list);
      this.m_serversDropdown.value = Math.Max(0, list.FindIndex((Predicate<string>) (s => s == defaultServer)));
    }

    public enum StartState
    {
      MatchMaking,
      HavreDimension,
    }
  }
}
