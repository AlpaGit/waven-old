// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.VisualEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  [SelectionBase]
  [ExecuteInEditMode]
  public abstract class VisualEffect : MonoBehaviour
  {
    private static readonly int s_colorPropertyId = Shader.PropertyToID("_Color");
    [SerializeField]
    private bool m_playOnAwake = true;
    [SerializeField]
    private VisualEffectDestroyMethod m_destroyMethod;
    [SerializeField]
    private float m_delayedDestructionSeconds = 1f;
    [SerializeField]
    private List<Renderer> m_renderers = new List<Renderer>();
    [SerializeField]
    private bool m_hasParentGroup;
    private MaterialPropertyBlock m_colorModifierPropertyBlock;
    private Coroutine m_destroyMethodCoroutine;
    [NonSerialized]
    public Action<VisualEffect> destructionOverride;

    public VisualEffectState state { get; protected set; } = VisualEffectState.Stopped;

    public VisualEffectDestroyMethod destroyMethod => this.m_destroyMethod;

    public bool hasParentGroup => this.m_hasParentGroup;

    [PublicAPI]
    public abstract bool IsAlive();

    [PublicAPI]
    public void Play()
    {
      this.state = VisualEffectState.Playing;
      if (!this.isActiveAndEnabled)
        return;
      this.PlayInternal();
      if (!Application.isPlaying)
        return;
      if (this.m_destroyMethodCoroutine != null)
      {
        this.StopCoroutine(this.m_destroyMethodCoroutine);
        this.m_destroyMethodCoroutine = (Coroutine) null;
      }
      switch (this.m_destroyMethod)
      {
        case VisualEffectDestroyMethod.None:
          break;
        case VisualEffectDestroyMethod.AfterDelay:
          this.m_destroyMethodCoroutine = this.StartCoroutine(this.DelayedDestructionCheckRoutine());
          break;
        case VisualEffectDestroyMethod.WhenFinished:
          this.m_destroyMethodCoroutine = this.StartCoroutine(this.AutomaticDestructionCheckRoutine());
          break;
        case VisualEffectDestroyMethod.WhenStopped:
          break;
        case VisualEffectDestroyMethod.WhenStoppedAndFinished:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [PublicAPI]
    public void Pause()
    {
      if (this.state != VisualEffectState.Playing)
        return;
      this.state = VisualEffectState.Paused;
      if (!this.isActiveAndEnabled)
        return;
      this.PauseInternal();
    }

    [PublicAPI]
    public void Stop(VisualEffectStopMethod stopMethod = VisualEffectStopMethod.Stop)
    {
      this.state = VisualEffectState.Stopped;
      if (!this.isActiveAndEnabled)
        return;
      this.StopInternal(stopMethod);
      if (this.m_destroyMethod == VisualEffectDestroyMethod.WhenStoppedAndFinished)
      {
        if (stopMethod == VisualEffectStopMethod.StopAndClear)
        {
          if (this.destructionOverride != null)
            this.destructionOverride(this);
          else
            UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
        }
        else
        {
          if (this.m_destroyMethodCoroutine != null)
            this.StopCoroutine(this.m_destroyMethodCoroutine);
          this.m_destroyMethodCoroutine = this.StartCoroutine(this.AutomaticDestructionCheckRoutine());
        }
      }
      else
      {
        if (this.m_destroyMethod != VisualEffectDestroyMethod.WhenStopped)
          return;
        if (this.destructionOverride != null)
          this.destructionOverride(this);
        else
          UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
    }

    [PublicAPI]
    public void Clear()
    {
      if (!this.isActiveAndEnabled)
        return;
      this.ClearInternal();
    }

    [PublicAPI]
    public virtual void SetSortingOrder(int value)
    {
      List<Renderer> renderers = this.m_renderers;
      int count = renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        Renderer renderer = renderers[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) renderer)
          renderer.sortingOrder = value;
      }
    }

    [PublicAPI]
    public virtual void SetColorModifier(Color color)
    {
      MaterialPropertyBlock properties = this.m_colorModifierPropertyBlock;
      if (properties == null)
      {
        properties = new MaterialPropertyBlock();
        this.m_colorModifierPropertyBlock = properties;
      }
      properties.SetColor(VisualEffect.s_colorPropertyId, color);
      List<Renderer> renderers = this.m_renderers;
      int count = renderers.Count;
      for (int index = 0; index < count; ++index)
      {
        Renderer renderer = renderers[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) renderer)
        {
          SpriteRenderer spriteRenderer = renderer as SpriteRenderer;
          if ((UnityEngine.Object) null != (UnityEngine.Object) spriteRenderer)
            spriteRenderer.color = color;
          else
            renderer.SetPropertyBlock(properties);
        }
      }
    }

    protected abstract void PlayInternal();

    protected abstract void PauseInternal();

    protected abstract void StopInternal(VisualEffectStopMethod stopMethod);

    protected abstract void ClearInternal();

    internal void GroupPlayedInternal()
    {
      this.state = VisualEffectState.Playing;
      if (!this.isActiveAndEnabled)
        return;
      this.PlayInternal();
    }

    internal void GroupPausedInternal()
    {
      this.state = VisualEffectState.Paused;
      if (!this.isActiveAndEnabled)
        return;
      this.PauseInternal();
    }

    internal void GroupStoppedInternal(VisualEffectStopMethod stopMethod)
    {
      this.state = VisualEffectState.Stopped;
      if (!this.isActiveAndEnabled)
        return;
      this.StopInternal(stopMethod);
    }

    internal void GroupClearedInternal()
    {
      if (!this.isActiveAndEnabled)
        return;
      this.ClearInternal();
    }

    protected IEnumerator AutomaticDestructionCheckRoutine()
    {
      VisualEffect visualEffect = this;
      do
      {
        yield return (object) null;
      }
      while (visualEffect.IsAlive());
      if (visualEffect.destructionOverride != null)
        visualEffect.destructionOverride(visualEffect);
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) visualEffect.gameObject);
    }

    protected IEnumerator DelayedDestructionCheckRoutine()
    {
      VisualEffect visualEffect = this;
      float timeLeft = visualEffect.m_delayedDestructionSeconds;
      do
      {
        yield return (object) null;
        if (visualEffect.state != VisualEffectState.Paused)
          timeLeft -= Time.deltaTime;
      }
      while ((double) timeLeft > 0.0);
      if (visualEffect.destructionOverride != null)
        visualEffect.destructionOverride(visualEffect);
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) visualEffect.gameObject);
    }

    private void Awake()
    {
      if (!this.m_playOnAwake)
        return;
      this.Play();
    }

    private void OnEnable()
    {
      if (this.state != VisualEffectState.Playing)
        return;
      this.Play();
    }

    private void OnDisable()
    {
      if (this.m_hasParentGroup)
        return;
      if (this.state != VisualEffectState.Stopped)
        this.StopInternal(VisualEffectStopMethod.StopAndClear);
      else
        this.ClearInternal();
    }
  }
}
