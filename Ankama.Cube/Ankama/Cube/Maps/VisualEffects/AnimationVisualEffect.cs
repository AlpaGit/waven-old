// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.AnimationVisualEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  [SelectionBase]
  [ExecuteInEditMode]
  public class AnimationVisualEffect : VisualEffect
  {
    [SerializeField]
    private Animation m_animationController;
    [SerializeField]
    private AnimationVisualEffect.StopMode m_stopMode;
    [SerializeField]
    private float m_endCrossFadeDuration = 0.5f;
    [SerializeField]
    private AnimationClip m_startClip;
    [SerializeField]
    private AnimationClip m_idleClip;
    [SerializeField]
    private AnimationClip m_endClip;
    private Coroutine m_playQueuedCoroutine;
    private Coroutine m_stopQueuedCoroutine;

    public override bool IsAlive()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animationController)
        return false;
      return this.state != VisualEffectState.Stopped || this.m_playQueuedCoroutine != null || this.m_animationController.isPlaying || this.m_stopQueuedCoroutine != null;
    }

    protected override void PlayInternal()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animationController)
        return;
      if (this.m_playQueuedCoroutine != null)
      {
        this.StopCoroutine(this.m_playQueuedCoroutine);
        this.m_playQueuedCoroutine = (Coroutine) null;
      }
      if (this.m_stopQueuedCoroutine != null)
      {
        this.StopCoroutine(this.m_stopQueuedCoroutine);
        this.m_stopQueuedCoroutine = (Coroutine) null;
      }
      if (this.state != VisualEffectState.Paused)
      {
        this.SetSpeed(1f);
      }
      else
      {
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_startClip)
        {
          this.m_animationController.Play(this.m_startClip.name, PlayMode.StopAll);
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleClip)
            this.m_playQueuedCoroutine = this.StartCoroutine(this.PlayQueued(this.m_idleClip.name));
        }
        else if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleClip)
          this.m_animationController.Play(this.m_idleClip.name, PlayMode.StopAll);
        else
          Log.Warning("AnimationVisualEffect named '" + this.name + "' doesn't have a Start animation nor an Idle animation.", 104, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\VisualEffects\\AnimationVisualEffect.cs");
        if (this.destroyMethod != VisualEffectDestroyMethod.WhenFinished)
          return;
        this.m_stopQueuedCoroutine = this.StartCoroutine(this.StopQueued());
      }
    }

    protected override void PauseInternal()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animationController)
        return;
      this.SetSpeed(0.0f);
    }

    protected override void StopInternal(VisualEffectStopMethod stopMethod)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animationController)
        return;
      if (this.m_playQueuedCoroutine != null)
      {
        this.StopCoroutine(this.m_playQueuedCoroutine);
        this.m_playQueuedCoroutine = (Coroutine) null;
      }
      if (this.m_stopQueuedCoroutine != null)
      {
        this.StopCoroutine(this.m_stopQueuedCoroutine);
        this.m_stopQueuedCoroutine = (Coroutine) null;
      }
      if (this.state == VisualEffectState.Paused)
        this.SetSpeed(1f);
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_endClip)
      {
        this.m_animationController.Stop();
      }
      else
      {
        switch (this.m_stopMode)
        {
          case AnimationVisualEffect.StopMode.Natural:
            this.m_playQueuedCoroutine = this.StartCoroutine(this.PlayQueued(this.m_endClip.name));
            break;
          case AnimationVisualEffect.StopMode.CrossFade:
            this.m_animationController.CrossFade(this.m_endClip.name, this.m_endCrossFadeDuration);
            break;
          case AnimationVisualEffect.StopMode.Skip:
            this.m_animationController.Play(this.m_endClip.name, PlayMode.StopAll);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    protected override void ClearInternal()
    {
    }

    private IEnumerator PlayQueued(string clipName)
    {
      AnimationState currentAnimationState = (AnimationState) null;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_startClip && this.m_animationController.IsPlaying(this.m_startClip.name))
        currentAnimationState = this.m_animationController[this.m_startClip.name];
      else if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleClip && this.m_animationController.IsPlaying(this.m_idleClip.name))
        currentAnimationState = this.m_animationController[this.m_idleClip.name];
      else if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_endClip && this.m_animationController.IsPlaying(this.m_endClip.name))
        currentAnimationState = this.m_animationController[this.m_endClip.name];
      float normalizedLoopThreshold;
      if ((TrackedReference) null != (TrackedReference) currentAnimationState)
      {
        switch (currentAnimationState.wrapMode)
        {
          case WrapMode.Default:
            do
            {
              yield return (object) null;
            }
            while (currentAnimationState.enabled);
            break;
          case WrapMode.Once:
            do
            {
              yield return (object) null;
            }
            while (currentAnimationState.enabled);
            break;
          case WrapMode.Loop:
            float normalizedTime1 = currentAnimationState.normalizedTime;
            normalizedLoopThreshold = Mathf.Ceil(normalizedTime1);
            if (!Mathf.Approximately(normalizedLoopThreshold, normalizedTime1))
            {
              do
              {
                yield return (object) null;
              }
              while (currentAnimationState.enabled && (double) currentAnimationState.normalizedTime < (double) normalizedLoopThreshold);
              break;
            }
            break;
          case WrapMode.PingPong:
            float normalizedTime2 = currentAnimationState.normalizedTime;
            normalizedLoopThreshold = 2f * Mathf.Ceil(0.5f * normalizedTime2);
            if (!Mathf.Approximately(normalizedLoopThreshold, normalizedTime2))
            {
              do
              {
                yield return (object) null;
              }
              while (currentAnimationState.enabled && (double) currentAnimationState.normalizedTime < (double) normalizedLoopThreshold);
              break;
            }
            break;
          case WrapMode.ClampForever:
            do
            {
              yield return (object) null;
            }
            while (currentAnimationState.enabled && (double) currentAnimationState.normalizedTime <= 1.0);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      this.m_animationController.Play(clipName);
      this.m_playQueuedCoroutine = (Coroutine) null;
    }

    private IEnumerator StopQueued()
    {
      AnimationVisualEffect animationVisualEffect = this;
      while (animationVisualEffect.m_playQueuedCoroutine != null || animationVisualEffect.m_animationController.isPlaying)
        yield return (object) null;
      animationVisualEffect.m_stopQueuedCoroutine = (Coroutine) null;
      animationVisualEffect.Stop();
    }

    private void SetSpeed(float value)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_startClip)
        this.m_animationController[this.m_startClip.name].speed = value;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleClip)
        this.m_animationController[this.m_idleClip.name].speed = value;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endClip))
        return;
      this.m_animationController[this.m_endClip.name].speed = value;
    }

    public enum StopMode
    {
      Natural,
      CrossFade,
      Skip,
    }
  }
}
