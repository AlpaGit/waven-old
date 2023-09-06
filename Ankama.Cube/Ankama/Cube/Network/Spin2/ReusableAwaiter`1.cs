// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.ReusableAwaiter`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Runtime.CompilerServices;

namespace Ankama.Cube.Network.Spin2
{
  internal sealed class ReusableAwaiter<T> : INotifyCompletion
  {
    private Action m_continuation;
    private T m_result;
    private System.Exception m_exception;

    public bool IsCompleted { get; private set; }

    public T GetResult()
    {
      if (this.m_exception != null)
        throw this.m_exception;
      return this.m_result;
    }

    public void OnCompleted(Action continuation) => this.m_continuation = this.m_continuation == null ? continuation : throw new InvalidOperationException("This ReusableAwaiter instance has already been listened");

    public bool TrySetResult(T result)
    {
      if (this.IsCompleted)
        return false;
      this.IsCompleted = true;
      this.m_result = result;
      if (this.m_continuation != null)
        this.m_continuation();
      return true;
    }

    public bool TrySetException(System.Exception exception)
    {
      if (this.IsCompleted)
        return false;
      this.IsCompleted = true;
      this.m_exception = exception;
      if (this.m_continuation != null)
        this.m_continuation();
      return true;
    }

    public ReusableAwaiter<T> Reset()
    {
      this.m_result = default (T);
      this.m_continuation = (Action) null;
      this.m_exception = (System.Exception) null;
      this.IsCompleted = false;
      return this;
    }

    public ReusableAwaiter<T> GetAwaiter() => this;
  }
}
