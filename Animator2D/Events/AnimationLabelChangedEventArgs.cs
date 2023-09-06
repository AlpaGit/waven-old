// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Events.AnimationLabelChangedEventArgs
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;

namespace Ankama.Animations.Events
{
  [PublicAPI]
  public sealed class AnimationLabelChangedEventArgs : EventArgs
  {
    [PublicAPI]
    public readonly string animation;
    [PublicAPI]
    public readonly string label;

    [PublicAPI]
    public AnimationLabelChangedEventArgs(string animation, string label)
    {
      this.animation = animation;
      this.label = label;
    }
  }
}
