// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.IAnimator2D
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using Ankama.Animations.Events;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Animations
{
  [PublicAPI]
  public interface IAnimator2D
  {
    [PublicAPI]
    event Animator2DInitialisedEventHandler Initialised;

    [PublicAPI]
    event AnimationLabelChangedEventHandler AnimationLabelChanged;

    [PublicAPI]
    event AnimationStartedEventHandler AnimationStarted;

    [PublicAPI]
    event AnimationLoopedEventHandler AnimationLooped;

    [PublicAPI]
    event AnimationEndedEventHandler AnimationEnded;

    [PublicAPI]
    bool paused { get; set; }

    [PublicAPI]
    int frameRate { get; set; }

    [PublicAPI]
    string animationName { get; }

    [PublicAPI]
    bool animationLoops { get; set; }

    [PublicAPI]
    int animationFrameCount { get; }

    [PublicAPI]
    int currentFrame { get; set; }

    [PublicAPI]
    string currentLabel { get; }

    [PublicAPI]
    bool reachedEndOfAnimation { get; }

    [PublicAPI]
    Color color { get; set; }

    int sortingLayerId { get; set; }

    int sortingOrder { get; set; }

    Animator2DInitialisationState GetInitialisationState();

    bool CurrentAnimationHasLabel(string labelName, out int frame);

    [PublicAPI]
    bool CurrentAnimationHasLabel(string labelName, StringComparison comparisonType, out int frame);

    [PublicAPI]
    void SetAnimation([NotNull] string animName, bool animLoops, bool async = true, bool restart = true);

    GameObject gameObject { get; }

    Transform transform { get; }

    bool enabled { get; set; }

    bool isActiveAndEnabled { get; }
  }
}
