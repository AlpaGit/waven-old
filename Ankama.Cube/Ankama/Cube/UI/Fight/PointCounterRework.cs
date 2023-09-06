// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.PointCounterRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class PointCounterRework : MonoBehaviour
  {
    [SerializeField]
    private PointCounterStyleRework m_style;
    [SerializeField]
    [UsedImplicitly]
    private Image m_image;
    [SerializeField]
    [UsedImplicitly]
    private UISpriteTextRenderer m_text;
    private int m_currentValue;
    private int m_targetValue;
    private Tweener m_tween;
    private static readonly string[] s_intToString = new string[11]
    {
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "10"
    };

    public int targetValue => this.m_targetValue;

    private void Awake() => this.Refresh();

    public void SetValue(int value)
    {
      if (value == this.m_targetValue)
        return;
      if (this.m_tween != null)
      {
        this.m_tween.Kill();
        this.m_tween = (Tweener) null;
      }
      this.m_currentValue = value;
      this.m_targetValue = value;
      this.Refresh();
    }

    public void ChangeValue(int value)
    {
      if (value == this.m_targetValue)
        return;
      float tweenDuration = this.m_style.GetTweenDuration(this.m_currentValue, value);
      if (this.m_tween != null)
        this.m_tween.ChangeEndValue((object) value, tweenDuration, true);
      else
        this.m_tween = DOTween.To(new DOGetter<int>(this.TweenGetter), new DOSetter<int>(this.TweenSetter), value, tweenDuration).SetEase<Tweener>(this.m_style.tweenEasing).OnComplete<Tweener>(new TweenCallback(this.OnTweenComplete));
      this.m_targetValue = value;
    }

    private int TweenGetter() => this.m_currentValue;

    private void TweenSetter(int value)
    {
      this.m_currentValue = value;
      this.Refresh();
    }

    private void OnTweenComplete() => this.m_tween = (Tweener) null;

    private void Refresh()
    {
      if (!((Object) null != (Object) this.m_text))
        return;
      if (this.m_currentValue >= 0 && this.m_currentValue <= 10)
        this.m_text.text = PointCounterRework.s_intToString[this.m_currentValue];
      else
        this.m_text.text = this.m_currentValue.ToString();
    }
  }
}
