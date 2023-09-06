// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.TweenableFloat
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.UI
{
  public class TweenableFloat : Tweenable<float>
  {
    public override void Evaluate(float percentage) => this.m_value = this.m_startValue + (this.m_endValue - this.m_startValue) * percentage;
  }
}
