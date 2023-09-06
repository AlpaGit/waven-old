// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUnityVersion
// Assembly: Plugins.DOTween, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FF25450-B39C-42C8-B3DB-BB3A40E2DA5A
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.DOTween.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenModuleUnityVersion
  {
    public static Sequence DOGradientColor(this Material target, Gradient gradient, float duration)
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

    public static Sequence DOGradientColor(
      this Material target,
      Gradient gradient,
      string property,
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
          target.SetColor(property, gradientColorKey.color);
        }
        else
        {
          float duration1 = index == length - 1 ? duration - sequence.Duration(false) : duration * (index == 0 ? gradientColorKey.time : gradientColorKey.time - colorKeys[index - 1].time);
          sequence.Append((Tween) target.DOColor(gradientColorKey.color, property, duration1).SetEase<Tweener>(Ease.Linear));
        }
      }
      return sequence;
    }

    public static CustomYieldInstruction WaitForCompletion(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForCompletion(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static CustomYieldInstruction WaitForRewind(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForRewind(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static CustomYieldInstruction WaitForKill(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForKill(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static CustomYieldInstruction WaitForElapsedLoops(
      this Tween t,
      int elapsedLoops,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForElapsedLoops(t, elapsedLoops);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static CustomYieldInstruction WaitForPosition(
      this Tween t,
      float position,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForPosition(t, position);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static CustomYieldInstruction WaitForStart(
      this Tween t,
      bool returnCustomYieldInstruction)
    {
      if (t.active)
        return (CustomYieldInstruction) new DOTweenCYInstruction.WaitForStart(t);
      if (Debugger.logPriority > 0)
        Debugger.LogInvalidTween(t);
      return (CustomYieldInstruction) null;
    }

    public static Tweener DOOffset(
      this Material target,
      Vector2 endValue,
      int propertyID,
      float duration)
    {
      if (target.HasProperty(propertyID))
        return (Tweener) DOTween.To((DOGetter<Vector2>) (() => target.GetTextureOffset(propertyID)), (DOSetter<Vector2>) (x => target.SetTextureOffset(propertyID, x)), endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>((object) target);
      if (Debugger.logPriority > 0)
        Debugger.LogMissingMaterialProperty(propertyID);
      return (Tweener) null;
    }

    public static Tweener DOTiling(
      this Material target,
      Vector2 endValue,
      int propertyID,
      float duration)
    {
      if (target.HasProperty(propertyID))
        return (Tweener) DOTween.To((DOGetter<Vector2>) (() => target.GetTextureScale(propertyID)), (DOSetter<Vector2>) (x => target.SetTextureScale(propertyID, x)), endValue, duration).SetTarget<TweenerCore<Vector2, Vector2, VectorOptions>>((object) target);
      if (Debugger.logPriority > 0)
        Debugger.LogMissingMaterialProperty(propertyID);
      return (Tweener) null;
    }
  }
}
