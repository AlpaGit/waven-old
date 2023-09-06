// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.BugReportUpdater
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using Unity.Cloud.UserReporting.Plugin;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public class BugReportUpdater : MonoBehaviour
  {
    private UnityUserReportingUpdater m_unityUserReportingUpdater;

    private void Awake() => this.m_unityUserReportingUpdater = new UnityUserReportingUpdater();

    private void Update()
    {
      this.m_unityUserReportingUpdater.Reset();
      this.StartCoroutine((IEnumerator) this.m_unityUserReportingUpdater);
    }

    private void OnDestroy() => this.m_unityUserReportingUpdater = (UnityUserReportingUpdater) null;
  }
}
