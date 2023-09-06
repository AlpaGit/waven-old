// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.SlidingAnimUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  [Serializable]
  public struct SlidingAnimUI
  {
    [SerializeField]
    public SlidingAnimUIConfig openConfig;
    [SerializeField]
    public SlidingAnimUIConfig closeConfig;
    [SerializeField]
    public List<CanvasGroup> elements;
    private Sequence m_transitionTweenSequence;

    public Sequence PlayAnim(bool open, SlidingSide side, bool reverseElementOrder = false)
    {
      if (this.m_transitionTweenSequence != null && this.m_transitionTweenSequence.IsActive())
        this.m_transitionTweenSequence.Kill();
      SlidingAnimUIConfig slidingAnimUiConfig = open ? this.openConfig : this.closeConfig;
      this.m_transitionTweenSequence = DOTween.Sequence();
      float delay = slidingAnimUiConfig.delay;
      for (int index = 0; index < this.elements.Count; ++index)
      {
        CanvasGroup element = this.elements[reverseElementOrder ? this.elements.Count - 1 - index : index];
        RectTransform transform = element.transform as RectTransform;
        Vector2 anchoredPosition = transform.anchoredPosition;
        Vector2 anchorOffset = slidingAnimUiConfig.anchorOffset;
        Vector2 vector2_1 = side == SlidingSide.Right ? anchoredPosition + anchorOffset : anchoredPosition - anchorOffset;
        Vector2 vector2_2 = open ? vector2_1 : anchoredPosition;
        Vector2 endValue = open ? anchoredPosition : vector2_1;
        if (open)
          element.alpha = 0.0f;
        transform.anchoredPosition = vector2_2;
        this.m_transitionTweenSequence.Insert(delay, (Tween) transform.DOAnchorPos(endValue, slidingAnimUiConfig.duration).SetEase<Tweener>(slidingAnimUiConfig.positionCurve));
        this.m_transitionTweenSequence.Insert(delay, (Tween) element.DOFade(slidingAnimUiConfig.endAlpha, slidingAnimUiConfig.duration).SetEase<Tweener>(slidingAnimUiConfig.alphaCurve));
        delay += slidingAnimUiConfig.elementDelayOffset;
      }
      return this.m_transitionTweenSequence;
    }
  }
}
