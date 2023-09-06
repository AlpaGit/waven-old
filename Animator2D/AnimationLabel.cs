// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.AnimationLabel
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

namespace Ankama.Animations
{
  public struct AnimationLabel
  {
    public readonly int frame;
    public readonly string label;

    internal AnimationLabel(int frame, string label)
    {
      this.frame = frame;
      this.label = label;
    }
  }
}
