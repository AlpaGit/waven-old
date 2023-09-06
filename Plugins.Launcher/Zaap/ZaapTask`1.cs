// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Zaap.ZaapTask`1
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using com.ankama.zaap;
using System;
using Zaap_CSharp_Client;

namespace Ankama.Launcher.Zaap
{
  internal sealed class ZaapTask<T> : ZaapTask
  {
    public Func<ZaapClient, T> m_execution;
    public readonly Action<T> m_callback;
    public readonly Action<ZaapError> m_errorCallback;

    public ZaapTask(
      Func<ZaapClient, T> execution,
      Action<T> callback,
      Action<ZaapError> errorCallback)
    {
      this.m_execution = execution;
      this.m_callback = callback;
      this.m_errorCallback = errorCallback;
    }

    public Action Execute(ZaapClient zaapClient)
    {
      try
      {
        T invokeResult = this.m_execution(zaapClient);
        return (Action) (() => this.m_callback(invokeResult));
      }
      catch (ZaapError ex)
      {
        return (Action) (() => this.m_errorCallback(ex));
      }
      catch (Exception ex)
      {
        return (Action) (() => this.m_errorCallback(new ZaapError(ErrorCode.UNKNOWN)
        {
          Details = ex.Message
        }));
      }
    }
  }
}
