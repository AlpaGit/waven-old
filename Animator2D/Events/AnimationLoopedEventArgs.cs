// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Events.AnimationLoopedEventArgs
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;

namespace Ankama.Animations.Events
{
  [PublicAPI]
  public sealed class AnimationLoopedEventArgs : EventArgs
  {
    [PublicAPI]
    public readonly string animation;

    [PublicAPI]
    public AnimationLoopedEventArgs(string animation) => this.animation = animation;
  }
}
