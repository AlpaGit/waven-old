// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Events.AnimationStartedEventArgs
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;

namespace Ankama.Animations.Events
{
  [PublicAPI]
  public sealed class AnimationStartedEventArgs : EventArgs
  {
    [PublicAPI]
    public readonly string animation;
    [PublicAPI]
    public readonly string previousAnimation;
    [PublicAPI]
    public readonly int frame;

    [PublicAPI]
    public AnimationStartedEventArgs(string animation, string previousAnimation, int frame)
    {
      this.animation = animation;
      this.previousAnimation = previousAnimation;
      this.frame = frame;
    }
  }
}
