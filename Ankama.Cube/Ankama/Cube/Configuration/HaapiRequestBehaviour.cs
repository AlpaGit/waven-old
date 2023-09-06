// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.HaapiRequestBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Threading;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public class HaapiRequestBehaviour : MonoBehaviour
  {
    private HaapiRequest m_request;
    private bool m_done;

    public void ExecuteRequest<U>(
      Func<U> function,
      Action<U> onSuccess,
      Action<Exception> onException)
    {
      this.m_request = (HaapiRequest) new HaapiRequest<U>(function, onSuccess, onException);
      new Thread(new ThreadStart(this.m_request.SendRequest)).Start();
    }

    private void Update()
    {
      if (this.m_done || this.m_request == null)
        return;
      if (!this.m_request.isDone)
        return;
      try
      {
        this.m_request.ExecuteResult();
      }
      finally
      {
        this.m_done = true;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
    }
  }
}
