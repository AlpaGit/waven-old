// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.LoginDebugUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.Player;
using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI
{
  public class LoginDebugUI : MonoBehaviour
  {
    [SerializeField]
    private CustomDropdown m_serversDropdown;
    [SerializeField]
    private ServerList m_serverList;

    private void Awake() => this.gameObject.SetActive(ApplicationConfig.showServerSelection);

    private void Start() => this.InitializeServerList();

    private void OnSelectServer(int serverIndex)
    {
      ServerList.ServerInfo serverInfo = this.m_serverList.GetServerInfo(serverIndex);
      ApplicationConfig.SetServerInfo(serverInfo);
      ApplicationConfig.versionCheckResult = VersionChecker.Result.Success;
      PlayerPreferences.lastServer = serverInfo.displayName;
      PlayerPreferences.Save();
    }

    private void InitializeServerList()
    {
      List<string> list = ((IEnumerable<ServerList.ServerInfo>) this.m_serverList.GetAllServers()).Select<ServerList.ServerInfo, string>((Func<ServerList.ServerInfo, string>) (s => s.displayName)).ToList<string>();
      this.m_serversDropdown.AddOptions(list);
      string defaultServer = PlayerPreferences.lastServer;
      int serverIndex = list.FindIndex((Predicate<string>) (s => s == defaultServer));
      if (serverIndex < 0)
        serverIndex = 0;
      this.m_serversDropdown.value = serverIndex;
      this.OnSelectServer(serverIndex);
      this.m_serversDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnSelectServer));
    }
  }
}
