// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.DOTweenExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public static class DOTweenExtensions
  {
    public static Tweener DOColor(this AbstractTextField target, Color endValue, float duration) => (Tweener) DOTween.To((DOGetter<Color>) (() => target.color), (DOSetter<Color>) (x => target.color = x), endValue, duration).SetTarget<TweenerCore<Color, Color, ColorOptions>>((object) target);
  }
}
