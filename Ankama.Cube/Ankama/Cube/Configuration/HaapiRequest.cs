// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.HaapiRequest
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Configuration
{
  public abstract class HaapiRequest
  {
    protected bool m_done;

    public bool isDone => this.m_done;

    public abstract void SendRequest();

    public abstract void ExecuteResult();
  }
}
