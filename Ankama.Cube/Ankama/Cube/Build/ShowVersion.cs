// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Build.ShowVersion
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.UI.Components;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Build
{
  public sealed class ShowVersion : MonoBehaviour
  {
    [SerializeField]
    private RawTextField m_textVersion;
    [SerializeField]
    private GameObject m_versionDemo;

    private void Awake()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textVersion))
        return;
      this.m_textVersion.enabled = false;
    }

    private IEnumerator Start()
    {
      ShowVersion showVersion = this;
      while (!RuntimeData.isReady)
        yield return (object) null;
      bool simulateDemo = ApplicationConfig.simulateDemo;
      if ((UnityEngine.Object) null != (UnityEngine.Object) showVersion.m_textVersion)
      {
        showVersion.m_textVersion.enabled = !simulateDemo;
        ApplicationConfig.OnServerConfigLoaded += new Action(showVersion.RefreshText);
        showVersion.RefreshText();
      }
      if ((UnityEngine.Object) null != (UnityEngine.Object) showVersion.m_versionDemo)
        showVersion.m_versionDemo.SetActive(simulateDemo);
    }

    public void RefreshText() => this.m_textVersion.SetText("0.1.1.6169" + (ApplicationConfig.gameServerIsLocal ? "-internal" : "") + ShowVersion.GetServerProfile());

    private static string GetServerProfile()
    {
      switch (ApplicationConfig.gameServerProfile)
      {
        case RemoteConfig.ServerProfile.Development:
          return "-dev";
        case RemoteConfig.ServerProfile.Beta:
          return "-alpha";
        case RemoteConfig.ServerProfile.Production:
          return "";
        default:
          return "";
      }
    }
  }
}
