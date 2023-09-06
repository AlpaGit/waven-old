// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Zaap.ZaapLink
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using Ankama.Launcher.Messages;
using com.ankama.zaap;
using System;
using System.Collections;
using UnityEngine;
using Zaap_CSharp_Client;

namespace Ankama.Launcher.Zaap
{
  public class ZaapLink : ILauncherLink
  {
    private readonly ZaapWorker m_worker;
    private readonly MonoBehaviour m_behaviour;

    public static ZaapLink Create()
    {
      ZaapWorker worker = new ZaapWorker();
      return worker.Start() ? new ZaapLink(worker) : (ZaapLink) null;
    }

    private ZaapLink(ZaapWorker worker)
    {
      this.m_worker = worker;
      GameObject target = new GameObject();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
      target.hideFlags = HideFlags.DontSave;
      this.m_behaviour = (MonoBehaviour) target.AddComponent<ZaapBehaviour>();
      this.m_behaviour.StartCoroutine(this.CheckResults());
    }

    private void AddTask(ZaapTask task) => this.m_worker.AddTask(task);

    private IEnumerator CheckResults()
    {
      WaitForEndOfFrame wait = new WaitForEndOfFrame();
      while ((UnityEngine.Object) null != (UnityEngine.Object) this.m_behaviour)
      {
        Action result;
        while (this.m_worker.TryGetResult(out result))
        {
          if (result != null)
            result();
        }
        yield return (object) wait;
      }
    }

    public bool RequestApiToken(
      int serviceId,
      Action<ApiToken> tokenCallback,
      Action<Exception> errorCallback)
    {
      this.AddTask((ZaapTask) new ZaapTask<string>((Func<ZaapClient, string>) (zc => zc.GetClient().auth_getGameToken(zc.Session, serviceId)), (Action<string>) (token => tokenCallback(new ApiToken(token))), (Action<ZaapError>) (ex => errorCallback((Exception) ex))));
      return true;
    }

    public bool RequestLanguage(Action<string> langCallback, Action<Exception> errorCallback)
    {
      this.AddTask((ZaapTask) new ZaapTask<string>((Func<ZaapClient, string>) (zc => zc.GetClient().settings_get(zc.Session, "language").ToUpperInvariant().Trim('"')), langCallback, (Action<ZaapError>) errorCallback));
      return true;
    }

    public bool UpdateLanguage(
      string lang,
      Action<bool> successCallback,
      Action<Exception> errorCallback)
    {
      this.AddTask((ZaapTask) new ZaapTask<bool>((Func<ZaapClient, bool>) (zc =>
      {
        zc.GetClient().settings_set(zc.Session, "language", "\"" + lang.ToLowerInvariant() + "\"");
        return true;
      }), successCallback, (Action<ZaapError>) errorCallback));
      return true;
    }

    public bool opening => this.m_worker.Is(ZaapWorker.WorkerStatus.STARTING);

    public bool opened => this.m_worker.Is(ZaapWorker.WorkerStatus.STARTED);

    public bool isSteam => false;

    public string LauncherLanguage => (string) null;
  }
}
