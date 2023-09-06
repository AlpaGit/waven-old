// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.HaapiRequest`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Configuration
{
  public class HaapiRequest<U> : HaapiRequest
  {
    private Func<U> m_function;
    private Action<U> m_onSuccess;
    private U m_result;
    private Action<Exception> m_onException;
    private Exception m_exception;
    private bool m_sentResult;

    public HaapiRequest(Func<U> function, Action<U> onSuccess, Action<Exception> onException)
    {
      this.m_function = function;
      this.m_onSuccess = onSuccess;
      this.m_onException = onException;
    }

    public override void SendRequest()
    {
      try
      {
        this.m_result = this.m_function();
      }
      catch (Exception ex)
      {
        this.m_exception = ex;
      }
      this.m_done = true;
    }

    public override void ExecuteResult()
    {
      if (!this.m_done)
        return;
      if ((object) this.m_result != null)
      {
        Action<U> onSuccess = this.m_onSuccess;
        if (onSuccess != null)
          onSuccess(this.m_result);
      }
      if (this.m_exception == null)
        return;
      Action<Exception> onException = this.m_onException;
      if (onException == null)
        return;
      onException(this.m_exception);
    }
  }
}
