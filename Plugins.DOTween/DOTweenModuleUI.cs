// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUI
// Assembly: Plugins.DOTween, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FF25450-B39C-42C8-B3DB-BB3A40E2DA5A
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.DOTween.dll

using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
  public static class DOTweenModuleUI
  {
    public static Tweener DOFade(this CanvasGroup target, float endValue, float duration) => (Tweener) DOTween.To((DOGetter<float>) (() => target.alpha), (DOSetter<float>) (x => target.alpha = x), endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>((object) target);

    public static Tweener DOColor(this Graphic target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);

    public static Tweener DOFade(this Graphic target, float endValue, float duration) => DOTween.ToAlpha((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<Tweener>((object) target);

    public static Tweener DOColor(this Image target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);

    public static Tweener DOFade(this Image target, float endValue, float duration) => DOTween.ToAlpha((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<Tweener>((object) target);

    public static Tweener DOFillAmount(this Image target, float endValue, float duration)
    {
      if ((double) endValue > 1.0)
        endValue = 1f;
      else if ((double) endValue < 0.0)
        endValue = 0.0f;
      return (Tweener) DOTween.To((DOGetter<float>) (() => target.fillAmount), (DOSetter<float>) (x => target.fillAmount = x), endValue, duration).SetTarget<TweenerCore<float, float, FloatOptions>>((object) target);
    }

    public static Sequence DOGradientColor(this Image target, Gradient gradient, float duration)
    {
      Sequence sequence = DOTween.Sequence();
      GradientColorKey[] colorKeys = gradient.colorKeys;
      int length = colorKeys.Length;
      for (int index = 0; index < length; ++index)
      {
        GradientColorKey gradientColorKey = colorKeys[index];
        if (index == 0 && (double) gradientColorKey.time <= 0.0)
        {
          target.color = gradientColorKey.color;
        }
        else
        {
          float duration1 = index == length - 1 ? duration - sequence.Duration(false) : duration * (index == 0 ? gradientColorKey.time : gradientColorKey.time - colorKeys[index - 1].time);
          sequence.Append((Tween) DOTweenModuleUI.DOColor(target, gradientColorKey.color, duration1).SetEase<Tweener>(Ease.Linear));
        }
      }
      return sequence;
    }

    public static Tweener DOFlexibleSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => new Vector2(target.flexibleWidth, target.flexibleHeight)), (DOSetter<Vector2>) (x =>
      {
        target.flexibleWidth = x.x;
        target.flexibleHeight = x.y;
      }), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOMinSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => new Vector2(target.minWidth, target.minHeight)), (DOSetter<Vector2>) (x =>
      {
        target.minWidth = x.x;
        target.minHeight = x.y;
      }), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOPreferredSize(
      this LayoutElement target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => new Vector2(target.preferredWidth, target.preferredHeight)), (DOSetter<Vector2>) (x =>
      {
        target.preferredWidth = x.x;
        target.preferredHeight = x.y;
      }), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOColor(this Outline target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.effectColor), (DOSetter<Color>) (x => target.effectColor = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);

    public static Tweener DOFade(this Outline target, float endValue, float duration) => DOTween.ToAlpha((DOGetter<Color>) (() => target.effectColor), (DOSetter<Color>) (x => target.effectColor = x), endValue, duration).SetTarget<Tweener>((object) target);

    public static Tweener DOScale(this Outline target, Vector2 endValue, float duration) => (Tweener) DOTween.To((DOGetter<Vector2>) (() => target.effectDistance), (DOSetter<Vector2>) (x => target.effectDistance = x), endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>((object) target);

    public static Tweener DOAnchorPos(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.anchoredPosition), (DOSetter<Vector2>) (x => target.anchoredPosition = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPosX(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.anchoredPosition), (DOSetter<Vector2>) (x => target.anchoredPosition = x), new Vector2(endValue, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPosY(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.anchoredPosition), (DOSetter<Vector2>) (x => target.anchoredPosition = x), new Vector2(0.0f, endValue), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPos3D(
      this RectTransform target,
      Vector3 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector3>) (() => target.anchoredPosition3D), (DOSetter<Vector3>) (x => target.anchoredPosition3D = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPos3DX(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector3>) (() => target.anchoredPosition3D), (DOSetter<Vector3>) (x => target.anchoredPosition3D = x), new Vector3(endValue, 0.0f, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPos3DY(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector3>) (() => target.anchoredPosition3D), (DOSetter<Vector3>) (x => target.anchoredPosition3D = x), new Vector3(0.0f, endValue, 0.0f), duration).SetOptions(AxisConstraint.Y, snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorPos3DZ(
      this RectTransform target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector3>) (() => target.anchoredPosition3D), (DOSetter<Vector3>) (x => target.anchoredPosition3D = x), new Vector3(0.0f, 0.0f, endValue), duration).SetOptions(AxisConstraint.Z, snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorMax(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.anchorMax), (DOSetter<Vector2>) (x => target.anchorMax = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOAnchorMin(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.anchorMin), (DOSetter<Vector2>) (x => target.anchorMin = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOPivot(this RectTransform target, Vector2 endValue, float duration) => (Tweener) DOTween.To((DOGetter<Vector2>) (() => target.pivot), (DOSetter<Vector2>) (x => target.pivot = x), endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>((object) target);

    public static Tweener DOPivotX(this RectTransform target, float endValue, float duration) => DOTween.To((DOGetter<Vector2>) (() => target.pivot), (DOSetter<Vector2>) (x => target.pivot = x), new Vector2(endValue, 0.0f), duration).SetOptions(AxisConstraint.X).SetTarget<Tweener>((object) target);

    public static Tweener DOPivotY(this RectTransform target, float endValue, float duration) => DOTween.To((DOGetter<Vector2>) (() => target.pivot), (DOSetter<Vector2>) (x => target.pivot = x), new Vector2(0.0f, endValue), duration).SetOptions(AxisConstraint.Y).SetTarget<Tweener>((object) target);

    public static Tweener DOSizeDelta(
      this RectTransform target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => target.sizeDelta), (DOSetter<Vector2>) (x => target.sizeDelta = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOPunchAnchorPos(
      this RectTransform target,
      Vector2 punch,
      float duration,
      int vibrato = 10,
      float elasticity = 1f,
      bool snapping = false)
    {
      return DOTween.Punch((DOGetter<Vector3>) (() => (Vector3) target.anchoredPosition), (DOSetter<Vector3>) (x => target.anchoredPosition = (Vector2) x), (Vector3) punch, duration, vibrato, elasticity).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>((object) target).SetOptions(snapping);
    }

    public static Tweener DOShakeAnchorPos(
      this RectTransform target,
      float duration,
      float strength = 100f,
      int vibrato = 10,
      float randomness = 90f,
      bool snapping = false,
      bool fadeOut = true)
    {
      return DOTween.Shake((DOGetter<Vector3>) (() => (Vector3) target.anchoredPosition), (DOSetter<Vector3>) (x => target.anchoredPosition = (Vector2) x), duration, strength, vibrato, randomness, fadeOut: fadeOut).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>((object) target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
    }

    public static Tweener DOShakeAnchorPos(
      this RectTransform target,
      float duration,
      Vector2 strength,
      int vibrato = 10,
      float randomness = 90f,
      bool snapping = false,
      bool fadeOut = true)
    {
      return DOTween.Shake((DOGetter<Vector3>) (() => (Vector3) target.anchoredPosition), (DOSetter<Vector3>) (x => target.anchoredPosition = (Vector2) x), duration, (Vector3) strength, vibrato, randomness, fadeOut).SetTarget<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>((object) target).SetSpecialStartupMode<TweenerCore<Vector3, Vector3[], Vector3ArrayOptions>>(SpecialStartupMode.SetShake).SetOptions(snapping);
    }

    public static Sequence DOJumpAnchorPos(
      this RectTransform target,
      Vector2 endValue,
      float jumpPower,
      int numJumps,
      float duration,
      bool snapping = false)
    {
      if (numJumps < 1)
        numJumps = 1;
      float startPosY = 0.0f;
      float offsetY = -1f;
      bool offsetYSet = false;
      Sequence s = DOTween.Sequence();
      Tween t = (Tween) DOTween.To((DOGetter<Vector2>) (() => target.anchoredPosition), (DOSetter<Vector2>) (x => target.anchoredPosition = x), new Vector2(0.0f, jumpPower), duration / (float) (numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase<Tweener>(Ease.OutQuad).SetRelative<Tweener>().SetLoops<Tweener>(numJumps * 2, LoopType.Yoyo).OnStart<Tweener>((TweenCallback) (() => startPosY = target.anchoredPosition.y));
      s.Append((Tween) DOTween.To((DOGetter<Vector2>) (() => target.anchoredPosition), (DOSetter<Vector2>) (x => target.anchoredPosition = x), new Vector2(endValue.x, 0.0f), duration).SetOptions(AxisConstraint.X, snapping).SetEase<Tweener>(Ease.Linear)).Join(t).SetTarget<Sequence>((object) target).SetEase<Sequence>(DOTween.defaultEaseType);
      s.OnUpdate<Sequence>((TweenCallback) (() =>
      {
        if (!offsetYSet)
        {
          offsetYSet = true;
          offsetY = s.isRelative ? endValue.y : endValue.y - startPosY;
        }
        Vector2 anchoredPosition = target.anchoredPosition;
        anchoredPosition.y += DOVirtual.EasedValue(0.0f, offsetY, s.ElapsedDirectionalPercentage(), Ease.OutQuad);
        target.anchoredPosition = anchoredPosition;
      }));
      return s;
    }

    public static Tweener DONormalizedPos(
      this ScrollRect target,
      Vector2 endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<Vector2>) (() => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition)), (DOSetter<Vector2>) (x =>
      {
        target.horizontalNormalizedPosition = x.x;
        target.verticalNormalizedPosition = x.y;
      }), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOHorizontalNormalizedPos(
      this ScrollRect target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<float>) (() => target.horizontalNormalizedPosition), (DOSetter<float>) (x => target.horizontalNormalizedPosition = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOVerticalNormalizedPos(
      this ScrollRect target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<float>) (() => target.verticalNormalizedPosition), (DOSetter<float>) (x => target.verticalNormalizedPosition = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOValue(
      this Slider target,
      float endValue,
      float duration,
      bool snapping = false)
    {
      return DOTween.To((DOGetter<float>) (() => target.value), (DOSetter<float>) (x => target.value = x), endValue, duration).SetOptions(snapping).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOColor(this Text target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);

    public static Tweener DOFade(this Text target, float endValue, float duration) => DOTween.ToAlpha((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<Tweener>((object) target);

    public static Tweener DOText(
      this Text target,
      string endValue,
      float duration,
      bool richTextEnabled = true,
      ScrambleMode scrambleMode = ScrambleMode.None,
      string scrambleChars = null)
    {
      return DOTween.To((DOGetter<string>) (() => target.text), (DOSetter<string>) (x => target.text = x), endValue, duration).SetOptions(richTextEnabled, scrambleMode, scrambleChars).SetTarget<Tweener>((object) target);
    }

    public static Tweener DOBlendableColor(this Graphic target, Color endValue, float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return (Tweener) DOTween.To((DOGetter<Color>) (() => to), (DOSetter<Color>) (x =>
      {
        Color color = x - to;
        to = x;
        target.color += color;
      }), endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);
    }

    public static Tweener DOBlendableColor(this Image target, Color endValue, float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return (Tweener) DOTween.To((DOGetter<Color>) (() => to), (DOSetter<Color>) (x =>
      {
        Color color = x - to;
        to = x;
        Image image = target;
        image.color = image.color + color;
      }), endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);
    }

    public static Tweener DOBlendableColor(this Text target, Color endValue, float duration)
    {
      endValue -= target.color;
      Color to = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      return (Tweener) DOTween.To((DOGetter<Color>) (() => to), (DOSetter<Color>) (x =>
      {
        Color color = x - to;
        to = x;
        Text text = target;
        text.color = text.color + color;
      }), endValue, duration).Blendable<Color, Color, ColorOptions>().SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);
    }

    public static class Utils
    {
      public static Vector2 SwitchToRectTransform(RectTransform from, RectTransform to)
      {
        Vector2 vector2_1;
        ref Vector2 local1 = ref vector2_1;
        Rect rect1 = from.rect;
        double num1 = (double) rect1.width * 0.5;
        rect1 = from.rect;
        double xMin1 = (double) rect1.xMin;
        double x1 = num1 + xMin1;
        rect1 = from.rect;
        double num2 = (double) rect1.height * 0.5;
        rect1 = from.rect;
        double yMin1 = (double) rect1.yMin;
        double y1 = num2 + yMin1;
        local1 = new Vector2((float) x1, (float) y1);
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint((Camera) null, from.position) + vector2_1;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenPoint, (Camera) null, out localPoint);
        Vector2 vector2_2;
        ref Vector2 local2 = ref vector2_2;
        Rect rect2 = to.rect;
        double num3 = (double) rect2.width * 0.5;
        rect2 = to.rect;
        double xMin2 = (double) rect2.xMin;
        double x2 = num3 + xMin2;
        rect2 = to.rect;
        double num4 = (double) rect2.height * 0.5;
        rect2 = to.rect;
        double yMin2 = (double) rect2.yMin;
        double y2 = num4 + yMin2;
        local2 = new Vector2((float) x2, (float) y2);
        return to.anchoredPosition + localPoint - vector2_2;
      }
    }
  }
}
