// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CharacterObjectUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Maps.Objects
{
  public static class CharacterObjectUtility
  {
    public static bool HasAnimationEnded([NotNull] IAnimator2D animator2D, string animationName) => animator2D.reachedEndOfAnimation || !animationName.Equals(animator2D.animationName);

    public static bool HasAnimationReachedLabel(
      [NotNull] IAnimator2D animator2D,
      string animationName,
      [NotNull] string label)
    {
      return animator2D.reachedEndOfAnimation || label.Equals(animator2D.currentLabel, StringComparison.OrdinalIgnoreCase) || !animationName.Equals(animator2D.animationName);
    }

    public static bool HasAnimationEnded(
      [NotNull] Animator2D animator2D,
      CharacterAnimationInfo animationInfo)
    {
      return animator2D.reachedEndOfAnimation || !animationInfo.animationName.Equals(animator2D.animationName);
    }

    public static bool HasAnimationReachedLabel(
      [NotNull] Animator2D animator2D,
      CharacterAnimationInfo animationInfo,
      [NotNull] string label)
    {
      return animator2D.reachedEndOfAnimation || label.Equals(animator2D.currentLabel, StringComparison.OrdinalIgnoreCase) || !animationInfo.animationName.Equals(animator2D.animationName);
    }
  }
}
