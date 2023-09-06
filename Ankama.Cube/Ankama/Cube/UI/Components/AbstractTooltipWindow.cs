// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AbstractTooltipWindow
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Fight.Windows;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public abstract class AbstractTooltipWindow : MonoBehaviour
  {
    private const float ScreenMargin = 10f;
    [SerializeField]
    protected TooltipWindowParameters m_parameters;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    private Tweener m_tweenAlpha;
    private Tweener m_tweenPosition;
    private bool m_opened;
    private AbstractTooltipWindow.RectOffset m_borderDistanceToScreen;

    public AbstractTooltipWindow.RectOffset borderDistanceToScreen => this.m_borderDistanceToScreen;

    public float alpha
    {
      get => this.m_canvasGroup.alpha;
      set
      {
        if ((double) value <= 0.0 && this.gameObject.activeSelf)
          this.gameObject.SetActive(false);
        else if ((double) value > 0.0 && !this.gameObject.activeSelf)
          this.gameObject.SetActive(true);
        this.m_canvasGroup.alpha = value;
      }
    }

    public virtual void Awake()
    {
      this.alpha = 0.0f;
      this.gameObject.SetActive(false);
    }

    public void ShowAt(TooltipPosition location, RectTransform targetRect)
    {
      RectTransform transform = (RectTransform) this.transform;
      Vector3 lossyScale = transform.lossyScale;
      Rect screenSpace1 = AbstractTooltipWindow.RectTransformToScreenSpace((RectTransform) transform.root);
      Rect screenSpace2 = AbstractTooltipWindow.RectTransformToScreenSpace(targetRect);
      AbstractTooltipWindow.Enlarge(ref screenSpace2, 10f * lossyScale.x, 10f * lossyScale.y);
      location = AbstractTooltipWindow.BestLocation(Vector2.Scale(transform.rect.size, (Vector2) lossyScale), screenSpace1, screenSpace2, location);
      Vector3 position = AbstractTooltipWindow.GetPosition(screenSpace2, location) with
      {
        z = this.transform.position.z
      };
      this.ShowAt(location, position);
    }

    public void ShowAt(TooltipPosition position, Vector3 worldPosition)
    {
      RectTransform transform = (RectTransform) this.transform;
      Vector3 lossyScale = transform.lossyScale;
      transform.pivot = AbstractTooltipWindow.GetPivotFor(position);
      RectTransform root = (RectTransform) transform.root;
      Rect screenSpace1 = AbstractTooltipWindow.RectTransformToScreenSpace(transform, worldPosition);
      Rect screenSpace2 = AbstractTooltipWindow.RectTransformToScreenSpace(root);
      AbstractTooltipWindow.Enlarge(ref screenSpace2, -10f * lossyScale.x, -10f * lossyScale.y);
      float right = screenSpace2.xMax - screenSpace1.xMax;
      float left = screenSpace1.xMin - screenSpace2.xMin;
      float top = screenSpace2.yMax - screenSpace1.yMax;
      float bottom = screenSpace1.yMin - screenSpace2.yMin;
      this.m_borderDistanceToScreen = new AbstractTooltipWindow.RectOffset(left, right, top, bottom);
      if ((double) right < 0.0)
        worldPosition.x += right;
      if ((double) left < 0.0)
        worldPosition.x -= left;
      if ((double) top < 0.0)
        worldPosition.y += top;
      if ((double) bottom < 0.0)
        worldPosition.y -= bottom;
      this.DisplayTooltip(worldPosition);
    }

    protected virtual void DisplayTooltip(Vector3 worldPosition)
    {
      Vector3 position = this.transform.position;
      worldPosition.z = position.z;
      TooltipWindowParameters parameters = this.m_parameters;
      Tweener tweenPosition = this.m_tweenPosition;
      if (tweenPosition != null)
        tweenPosition.Kill();
      Tweener tweenAlpha = this.m_tweenAlpha;
      if (tweenAlpha != null)
        tweenAlpha.Kill();
      this.gameObject.SetActive(true);
      this.m_tweenAlpha = (Tweener) DOTween.To(new DOGetter<float>(this.TweenAlphaGetter), new DOSetter<float>(this.TweenAlphaSetter), 1f, (1f - this.alpha) * parameters.openDuration).SetEase<TweenerCore<float, float, FloatOptions>>(parameters.openEase);
      Transform transform = this.transform;
      if (!this.m_opened)
      {
        transform.position = worldPosition;
        this.m_tweenAlpha.SetDelay<Tweener>(parameters.openDelay);
      }
      else
        this.m_tweenPosition = transform.DOMove(worldPosition, parameters.moveDuration).SetEase<Tweener>(parameters.moveEase);
      this.m_opened = true;
    }

    public virtual void Hide()
    {
      Tweener tweenAlpha = this.m_tweenAlpha;
      if (tweenAlpha != null)
        tweenAlpha.Kill();
      this.m_tweenAlpha = (Tweener) DOTween.To(new DOGetter<float>(this.TweenAlphaGetter), new DOSetter<float>(this.TweenAlphaSetter), 0.0f, this.alpha * this.m_parameters.closeDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_parameters.closeEase).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.TweenCompleteCallback));
    }

    private float TweenAlphaGetter() => this.alpha;

    private void TweenAlphaSetter(float value) => this.alpha = value;

    private void TweenCompleteCallback()
    {
      this.gameObject.SetActive(false);
      this.m_opened = false;
    }

    private static Vector2 GetPivotFor(TooltipPosition position)
    {
      switch (position)
      {
        case TooltipPosition.Top:
          return new Vector2(0.5f, 0.0f);
        case TooltipPosition.Bottom:
          return new Vector2(0.5f, 1f);
        case TooltipPosition.Left:
          return new Vector2(1f, 0.5f);
        case TooltipPosition.Right:
          return new Vector2(0.0f, 0.5f);
        default:
          throw new ArgumentOutOfRangeException(nameof (position), (object) position, (string) null);
      }
    }

    private static TooltipPosition BestLocation(
      Vector2 tooltipSize,
      Rect screenRect,
      Rect targetRect,
      TooltipPosition position)
    {
      switch (position)
      {
        case TooltipPosition.Top:
          return InsideTop() || !InsideBottom() ? TooltipPosition.Top : TooltipPosition.Bottom;
        case TooltipPosition.Bottom:
          return InsideBottom() || !InsideTop() ? TooltipPosition.Bottom : TooltipPosition.Top;
        case TooltipPosition.Left:
          return InsideLeft() || !InsideRight() ? TooltipPosition.Left : TooltipPosition.Right;
        case TooltipPosition.Right:
          return InsideRight() || !InsideLeft() ? TooltipPosition.Right : TooltipPosition.Left;
        default:
          throw new ArgumentOutOfRangeException(nameof (position), (object) position, (string) null);
      }

      bool InsideTop() => (double) targetRect.yMax + (double) tooltipSize.y <= (double) screenRect.yMax;

      bool InsideBottom() => (double) targetRect.yMin - (double) tooltipSize.y >= (double) screenRect.yMin;

      bool InsideLeft() => (double) targetRect.xMin + (double) tooltipSize.x >= (double) screenRect.xMin;

      bool InsideRight() => (double) targetRect.xMax + (double) tooltipSize.x <= (double) screenRect.xMax;
    }

    private static Rect RectTransformToScreenSpace(RectTransform transform) => AbstractTooltipWindow.RectTransformToScreenSpace(transform, transform.position);

    private static Rect RectTransformToScreenSpace(RectTransform transform, Vector3 worldPosition)
    {
      Vector2 pivot = transform.pivot;
      Vector2 vector2 = Vector2.Scale(transform.rect.size, (Vector2) transform.lossyScale);
      return new Rect(worldPosition.x - pivot.x * vector2.x, worldPosition.y - pivot.y * vector2.y, vector2.x, vector2.y);
    }

    private static Vector3 GetPosition(Rect target, TooltipPosition position)
    {
      switch (position)
      {
        case TooltipPosition.Top:
          return (Vector3) new Vector2(target.x + target.width * 0.5f, target.yMax);
        case TooltipPosition.Bottom:
          return (Vector3) new Vector2(target.x + target.width * 0.5f, target.yMin);
        case TooltipPosition.Left:
          return (Vector3) new Vector2(target.xMin, target.y + target.height * 0.5f);
        case TooltipPosition.Right:
          return (Vector3) new Vector2(target.xMax, target.y + target.height * 0.5f);
        default:
          throw new ArgumentOutOfRangeException(nameof (position), (object) position, (string) null);
      }
    }

    private static void Enlarge(ref Rect rect, float marginX, float marginY)
    {
      rect.x -= marginX;
      rect.width += 2f * marginX;
      rect.y -= marginY;
      rect.height += 2f * marginY;
    }

    public struct RectOffset
    {
      public float left;
      public float right;
      public float top;
      public float bottom;

      public RectOffset(float left, float right, float top, float bottom)
      {
        this.left = left;
        this.right = right;
        this.top = top;
        this.bottom = bottom;
      }
    }
  }
}
