// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterElementaryStateUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  [ExecuteInEditMode]
  public sealed class CharacterElementaryStateUI : CharacterUILayoutElement
  {
    private const float TweenFullDuration = 0.25f;
    [Header("Resources")]
    [UsedImplicitly]
    [SerializeField]
    private CharacterUIResources m_resources;
    [Header("Renderers")]
    [UsedImplicitly]
    [SerializeField]
    private SpriteRenderer m_elementaryIconRenderer;
    private ElementaryStates m_elementaryState = ElementaryStates.None;
    private ElementaryStates m_currentElementaryState = ElementaryStates.None;
    private float m_alpha;
    private Tweener m_tweener;

    public override Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        Color color = value;
        color.a *= this.m_alpha;
        this.m_elementaryIconRenderer.color = color;
      }
    }

    public override int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_elementaryIconRenderer.sortingOrder = this.sortingOrder;
      }
    }

    public override void SetLayoutPosition(int value)
    {
      if (this.m_layoutPosition == value)
        return;
      this.m_layoutPosition = value;
      this.Layout();
    }

    public void Setup()
    {
      this.m_elementaryState = ElementaryStates.None;
      this.m_currentElementaryState = ElementaryStates.None;
      this.SetIcon();
      this.SetAlpha(0.0f);
    }

    public void SetValue(ElementaryStates value)
    {
      if (value == this.m_elementaryState)
        return;
      this.m_elementaryState = value;
      this.m_currentElementaryState = value;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.SetIcon();
      this.SetAlpha(value == ElementaryStates.None ? 0.0f : 1f);
    }

    public void ChangeValue(ElementaryStates value)
    {
      if (value == this.m_elementaryState)
        return;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.m_elementaryState = value;
      if (this.m_currentElementaryState == ElementaryStates.None)
      {
        this.m_currentElementaryState = value;
        this.SetIcon();
        float duration = Mathf.Lerp(0.25f, 0.0f, this.m_alpha);
        if ((double) duration <= 0.0)
          return;
        this.m_tweener = (Tweener) DOTween.To(new DOGetter<float>(this.TweenGetter), new DOSetter<float>(this.SetAlpha), 1f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnTweenComplete));
      }
      else if (value == ElementaryStates.None)
      {
        float duration = Mathf.Lerp(0.0f, 0.25f, this.m_alpha);
        if ((double) duration > 0.0)
          this.m_tweener = (Tweener) DOTween.To(new DOGetter<float>(this.TweenGetter), new DOSetter<float>(this.SetAlpha), 0.0f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnIconChangeTweenComplete));
        else
          this.OnIconChangeTweenComplete();
      }
      else
      {
        float duration = Mathf.Lerp(0.0f, 0.25f, this.m_alpha);
        if ((double) duration > 0.0)
          this.m_tweener = (Tweener) DOTween.To(new DOGetter<float>(this.TweenGetter), new DOSetter<float>(this.SetAlpha), 0.0f, duration).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnIconSwitchTweenComplete));
        else
          this.OnIconSwitchTweenComplete();
      }
    }

    private void SetIcon()
    {
      Sprite sprite;
      switch (this.m_currentElementaryState)
      {
        case ElementaryStates.None:
          sprite = (Sprite) null;
          break;
        case ElementaryStates.Muddy:
          sprite = this.m_resources.elementaryStateMuddyIcon;
          break;
        case ElementaryStates.Oiled:
          sprite = this.m_resources.elementaryStateOiledIcon;
          break;
        case ElementaryStates.Ventilated:
          sprite = this.m_resources.elementaryStateVentilatedIcon;
          break;
        case ElementaryStates.Wet:
          sprite = this.m_resources.elementaryStateWetIcon;
          break;
        default:
          throw new ArgumentOutOfRangeException("m_currentElementaryState", (object) this.m_currentElementaryState, (string) null);
      }
      if ((UnityEngine.Object) null == (UnityEngine.Object) sprite)
      {
        this.m_elementaryIconRenderer.enabled = false;
        this.m_elementaryIconRenderer.sprite = (Sprite) null;
      }
      else
      {
        this.m_elementaryIconRenderer.sprite = sprite;
        this.m_elementaryIconRenderer.enabled = true;
      }
      this.Layout();
    }

    protected override void Layout()
    {
      this.layoutWidth = Mathf.CeilToInt(100f * CharacterUILayoutElement.LayoutSetTransform(this.m_elementaryIconRenderer, 0.01f * (float) this.m_layoutPosition));
      base.Layout();
    }

    private void SetAlpha(float value)
    {
      this.m_alpha = value;
      Color color = this.color;
      color.a *= value;
      this.m_elementaryIconRenderer.color = color;
    }

    private float TweenGetter() => this.m_alpha;

    private void OnIconSwitchTweenComplete()
    {
      this.m_currentElementaryState = this.m_elementaryState;
      this.SetIcon();
      this.m_tweener = (Tweener) DOTween.To(new DOGetter<float>(this.TweenGetter), new DOSetter<float>(this.SetAlpha), 1f, 0.25f).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnTweenComplete));
    }

    private void OnIconChangeTweenComplete()
    {
      this.m_currentElementaryState = this.m_elementaryState;
      this.SetIcon();
      this.m_tweener = (Tweener) null;
    }

    private void OnTweenComplete() => this.m_tweener = (Tweener) null;

    private void OnEnable()
    {
      this.m_elementaryIconRenderer.enabled = true;
      this.Layout();
    }

    private void OnDisable()
    {
      this.m_elementaryIconRenderer.enabled = false;
      base.Layout();
    }

    private void OnDestroy()
    {
      if (this.m_tweener == null)
        return;
      this.m_tweener.Kill();
      this.m_tweener = (Tweener) null;
    }
  }
}
