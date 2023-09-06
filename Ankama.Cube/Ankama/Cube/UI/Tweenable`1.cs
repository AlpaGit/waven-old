// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Tweenable`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.UI
{
  public abstract class Tweenable<T>
  {
    private bool m_init;
    protected T m_value;
    protected T m_startValue;
    protected T m_endValue;

    public bool init => this.m_init;

    public T currentValue => this.m_value;

    public T value => this.m_endValue;

    public T startValue
    {
      set => this.m_startValue = value;
    }

    public T endValue
    {
      set => this.m_endValue = value;
    }

    public void SetValue(T v)
    {
      this.m_value = this.m_startValue = this.m_endValue = v;
      this.m_init = true;
    }

    public void SetTweenValues(T startValue, T endValue)
    {
      this.m_startValue = startValue;
      this.m_endValue = endValue;
      this.m_init = true;
    }

    public void Reset()
    {
      this.m_init = false;
      this.m_value = this.m_startValue = this.m_endValue = default (T);
    }

    public abstract void Evaluate(float percentage);
  }
}
