// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ServerList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  [CreateAssetMenu]
  public class ServerList : ScriptableObject
  {
    [SerializeField]
    private ServerList.ServerInfo[] m_servers;

    public IReadOnlyList<ServerList.ServerInfo> GetAllServers() => (IReadOnlyList<ServerList.ServerInfo>) this.m_servers;

    public ServerList.ServerInfo GetServerInfo(int index) => this.m_servers[index];

    [Serializable]
    public struct ServerInfo
    {
      [SerializeField]
      private string m_displayName;
      [SerializeField]
      private string m_host;
      [SerializeField]
      private int m_port;
      [SerializeField]
      private bool m_isLocal;
      [SerializeField]
      private RemoteConfig.ServerProfile m_profile;
      [SerializeField]
      private int m_gameAppId;
      [SerializeField]
      private int m_chatAppId;

      public string displayName => this.m_displayName;

      public string host => this.m_host;

      public int port => this.m_port;

      public bool isLocal => this.m_isLocal;

      public RemoteConfig.ServerProfile profile => this.m_profile;

      public int gameAppId => this.m_gameAppId;

      public int chatAppId => this.m_chatAppId;

      public string bundlesUrl => this.m_isLocal ? "http://omg-srv.ankama.lan/" : "http://beta.dofuscube.com";

      public string haapiServerUrl => this.m_isLocal ? "https://haapi.ankama.tst/json/Ankama/v2" : "https://haapi.ankama.com/json/Ankama/v2";
    }
  }
}
