// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.KeywordTooltipContainer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.Windows
{
  public class KeywordTooltipContainer : MonoBehaviour
  {
    [SerializeField]
    protected TooltipWindowParameters m_parameters;
    [SerializeField]
    private KeywordTooltip m_tooltipTemplate;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [Header("Layout")]
    [SerializeField]
    private float m_spacing;
    [SerializeField]
    private KeywordTooltipContainer.VerticalAlignment m_verticalAlignment;
    [SerializeField]
    private KeywordTooltipContainer.HorizontalAlignment m_horizontalAlignment;
    [SerializeField]
    private float m_openingDuration;
    [SerializeField]
    private float m_openingDelay;
    [SerializeField]
    private float m_nextOpeningDelay = 0.1f;
    private bool m_opening;
    private Tween m_tweenAlpha;
    private readonly List<Tween> m_openingTweens = new List<Tween>();
    private readonly List<KeywordTooltip> m_activeTooltips = new List<KeywordTooltip>();
    private readonly Stack<KeywordTooltip> m_tooltipPool = new Stack<KeywordTooltip>();

    public float width => ((RectTransform) this.transform).rect.width;

    public float spacing => this.m_spacing;

    public void SetAlignement(
      KeywordTooltipContainer.HorizontalAlignment h,
      KeywordTooltipContainer.VerticalAlignment v)
    {
      this.m_horizontalAlignment = h;
      this.m_verticalAlignment = v;
      Vector2 vector2_1 = new Vector2(0.0f, 0.0f);
      Vector2 zero = Vector2.zero;
      Vector2 vector2_2 = new Vector2(1f, 0.0f);
      if (h != KeywordTooltipContainer.HorizontalAlignment.Left)
      {
        if (h != KeywordTooltipContainer.HorizontalAlignment.Right)
          throw new ArgumentOutOfRangeException(nameof (h), (object) h, (string) null);
        vector2_1.x = this.m_spacing;
        zero.x = 0.0f;
        vector2_2.x = 1f;
      }
      else
      {
        vector2_1.x = -this.m_spacing;
        zero.x = 1f;
        vector2_2.x = 0.0f;
      }
      if (v != KeywordTooltipContainer.VerticalAlignment.Up)
      {
        if (v != KeywordTooltipContainer.VerticalAlignment.Down)
          throw new ArgumentOutOfRangeException(nameof (v), (object) v, (string) null);
        zero.y = 0.0f;
        vector2_2.y = 0.0f;
      }
      else
      {
        zero.y = 1f;
        vector2_2.y = 1f;
      }
      RectTransform transform = (RectTransform) this.transform;
      transform.pivot = zero;
      transform.anchorMin = vector2_2;
      transform.anchorMax = vector2_2;
      transform.anchoredPosition = vector2_1;
    }

    public void Initialize(ITooltipDataProvider dataProvider)
    {
      this.RemoveAllTooltip();
      IFightValueProvider valueProvider = dataProvider.GetValueProvider();
      KeywordReference[] keywordReferences = dataProvider.keywordReferences;
      if (keywordReferences == null)
        return;
      for (int index = keywordReferences.Length - 1; index >= 0; --index)
      {
        KeywordReference keywordReference = keywordReferences[index];
        if (keywordReference.IsValidFor(RuntimeData.currentKeywordContext))
        {
          ITooltipDataProvider tooltipDataProvider = TooltipDataProviderFactory.Create(keywordReference, valueProvider);
          if (tooltipDataProvider != null)
          {
            KeywordTooltip tooltip = this.GetTooltip();
            tooltip.Initialize(tooltipDataProvider);
            this.m_activeTooltips.Add(tooltip);
          }
        }
      }
    }

    public void Show()
    {
      this.StopTweens();
      if (this.m_activeTooltips.Count == 0)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        this.gameObject.SetActive(true);
        this.TweenAlphaSetter(1f);
        this.m_opening = true;
      }
    }

    public void LateUpdate()
    {
      if (!this.m_opening)
        return;
      this.m_opening = false;
      int num1;
      int num2;
      switch (this.m_verticalAlignment)
      {
        case KeywordTooltipContainer.VerticalAlignment.Up:
          num1 = 1;
          num2 = 1;
          break;
        case KeywordTooltipContainer.VerticalAlignment.Down:
          num1 = 0;
          num2 = 0;
          break;
        default:
          throw new ArgumentOutOfRangeException("m_verticalAlignment", (object) this.m_verticalAlignment, (string) null);
      }
      float y1 = 0.0f;
      float y2 = 0.0f;
      float num3 = this.m_verticalAlignment == KeywordTooltipContainer.VerticalAlignment.Up ? -1f : 1f;
      for (int index = 0; index < this.m_activeTooltips.Count; ++index)
      {
        float delay = this.m_nextOpeningDelay * (float) index + this.m_openingDelay;
        KeywordTooltip tooltip = this.m_activeTooltips[index];
        RectTransform component = tooltip.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(component);
        float height = component.rect.height;
        Vector2 pivot = component.pivot with
        {
          y = (float) num1
        };
        component.pivot = pivot;
        Vector2 vector2 = component.anchorMin with
        {
          y = (float) num2
        };
        component.anchorMin = vector2;
        vector2 = component.anchorMax with
        {
          y = (float) num2
        };
        component.anchorMax = vector2;
        component.anchoredPosition = new Vector2(0.0f, y2);
        tooltip.alpha = 0.0f;
        this.m_openingTweens.Add((Tween) component.DOAnchorPos(new Vector2(0.0f, y1), this.m_openingDuration).SetDelay<Tweener>(delay));
        this.m_openingTweens.Add((Tween) DOTween.To((DOGetter<float>) (() => tooltip.alpha), (DOSetter<float>) (a => tooltip.alpha = a), 1f, this.m_openingDuration).SetDelay<TweenerCore<float, float, FloatOptions>>(delay));
        y2 = y1;
        y1 += num3 * (height + this.m_spacing);
      }
    }

    public void Hide()
    {
      this.StopTweens();
      TooltipWindowParameters parameters = this.m_parameters;
      this.m_tweenAlpha = (Tween) DOTween.To(new DOGetter<float>(this.TweenAlphaGetter), new DOSetter<float>(this.TweenAlphaSetter), 0.0f, this.TweenAlphaGetter() * parameters.closeDuration).SetEase<TweenerCore<float, float, FloatOptions>>(parameters.closeEase).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.HideCompleteCallback));
    }

    private float TweenAlphaGetter() => this.m_canvasGroup.alpha;

    private void TweenAlphaSetter(float value) => this.m_canvasGroup.alpha = value;

    private void HideCompleteCallback()
    {
      this.gameObject.SetActive(false);
      this.RemoveAllTooltip();
    }

    private void StopTweens()
    {
      Tween tweenAlpha = this.m_tweenAlpha;
      if (tweenAlpha != null)
        tweenAlpha.Kill();
      this.m_tweenAlpha = (Tween) null;
      for (int index = 0; index < this.m_openingTweens.Count; ++index)
        this.m_openingTweens[index].Kill();
      this.m_openingTweens.Clear();
    }

    private void RemoveAllTooltip()
    {
      for (int index = 0; index < this.m_activeTooltips.Count; ++index)
        this.ReleaseTooltip(this.m_activeTooltips[index]);
      this.m_activeTooltips.Clear();
    }

    private KeywordTooltip GetTooltip()
    {
      KeywordTooltip tooltip = this.m_tooltipPool.Count <= 0 ? UnityEngine.Object.Instantiate<KeywordTooltip>(this.m_tooltipTemplate, this.transform) : this.m_tooltipPool.Pop();
      tooltip.gameObject.SetActive(true);
      return tooltip;
    }

    private void ReleaseTooltip(KeywordTooltip keywordTooltip)
    {
      keywordTooltip.gameObject.SetActive(false);
      this.m_tooltipPool.Push(keywordTooltip);
    }

    public enum VerticalAlignment
    {
      Up,
      Down,
    }

    public enum HorizontalAlignment
    {
      Left,
      Right,
    }
  }
}
