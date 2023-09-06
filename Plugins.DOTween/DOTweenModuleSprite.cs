// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleSprite
// Assembly: Plugins.DOTween, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FF25450-B39C-42C8-B3DB-BB3A40E2DA5A
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.DOTween.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModuleSprite
  {
    public static Tweener DOColor(this SpriteRenderer target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);

    public static Tweener DOFade(this SpriteRenderer target, float endValue, float duration) => DOTween.ToAlpha((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<Tweener>((object) target);

    public static Sequence DOGradientColor(
      this SpriteRenderer target,
      Gradient gradient,
      float duration)
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
          sequence.Append((Tween) target.DOColor(gradientColorKey.color, duration1).SetEase<Tweener>(Ease.Linear));
        }
      }
      return sequence;
    }

    public static Tweener DOBlendableColor(
      this SpriteRenderer target,
      Color endValue,
      float duration)
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
  }
}
