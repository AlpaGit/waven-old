// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CharacterAnimationCallback
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Maps.Objects
{
  public class CharacterAnimationCallback
  {
    private readonly IAnimator2D m_animator2D;
    private string m_animationName;
    private bool m_animationStarted;
    private Action m_onComplete;
    private Action m_onCancel;
    private Action m_onStart;

    public CharacterAnimationCallback([NotNull] IAnimator2D animator2D)
    {
      this.m_animator2D = animator2D;
      this.m_animator2D.AnimationStarted += new AnimationStartedEventHandler(this.OnAnimationStarted);
      this.m_animator2D.AnimationEnded += new AnimationEndedEventHandler(this.OnAnimationEnded);
    }

    public void Setup(
      [NotNull] string animationName,
      bool restart,
      [CanBeNull] Action onComplete = null,
      [CanBeNull] Action onCancel = null,
      [CanBeNull] Action onStart = null)
    {
      Action onCancel1 = this.m_onCancel;
      if (onCancel1 != null)
        onCancel1();
      if (restart || !animationName.Equals(this.m_animationName))
      {
        this.m_animationName = animationName;
        this.m_animationStarted = false;
      }
      this.m_onComplete = onComplete;
      this.m_onCancel = onCancel;
      this.m_onStart = onStart;
    }

    public void ChangeAnimationName(string value) => this.m_animationName = value;

    public void Release()
    {
      this.m_animator2D.AnimationStarted -= new AnimationStartedEventHandler(this.OnAnimationStarted);
      this.m_animator2D.AnimationEnded -= new AnimationEndedEventHandler(this.OnAnimationEnded);
    }

    private void OnAnimationStarted(object sender, AnimationStartedEventArgs e)
    {
      if (e.animation.Equals(this.m_animationName))
      {
        this.m_animationStarted = true;
        Action onStart = this.m_onStart;
        if (onStart == null)
          return;
        onStart();
      }
      else
      {
        Action onCancel = this.m_onCancel;
        if (onCancel != null)
          onCancel();
        this.m_animationStarted = false;
        this.m_onComplete = (Action) null;
        this.m_onCancel = (Action) null;
        this.m_onStart = (Action) null;
      }
    }

    private void OnAnimationEnded(object sender, AnimationEndedEventArgs e)
    {
      if (!this.m_animationStarted || !e.animation.Equals(this.m_animationName))
        return;
      Action onComplete = this.m_onComplete;
      if (onComplete != null)
        onComplete();
      this.m_onComplete = (Action) null;
      this.m_onCancel = (Action) null;
      this.m_onStart = (Action) null;
    }
  }
}
