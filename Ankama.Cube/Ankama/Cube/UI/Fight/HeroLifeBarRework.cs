// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.HeroLifeBarRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class HeroLifeBarRework : MonoBehaviour
  {
    [SerializeField]
    private Image m_imageFill;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private RawTextField m_text;
    [SerializeField]
    private PointCounterStyleRework m_style;
    [SerializeField]
    private FightMapFeedbackColors m_colors;
    private int m_currentBaseLife;
    private int m_targetBaseLife;
    private Tweener m_baseLifeTween;
    private int m_currentLife;
    private int m_targetLife;
    private Tweener m_lifeTween;

    public void SetStartLife(int value, PlayerType playerType)
    {
      Color playerColor = this.m_colors.GetPlayerColor(playerType);
      this.m_background.color = (playerColor * 0.5f) with
      {
        a = playerColor.a
      };
      this.m_imageFill.color = playerColor;
      if (this.m_targetBaseLife == value && this.m_targetLife == value)
        return;
      if (this.m_lifeTween != null)
      {
        this.m_lifeTween.Kill();
        this.m_lifeTween = (Tweener) null;
      }
      if (this.m_baseLifeTween != null)
      {
        this.m_baseLifeTween.Kill();
        this.m_baseLifeTween = (Tweener) null;
      }
      this.m_currentLife = value;
      this.m_targetLife = value;
      this.m_currentBaseLife = value;
      this.m_targetBaseLife = value;
      this.Refresh();
    }

    public void ChangeBaseLife(int value)
    {
      if (value == this.m_targetBaseLife)
        return;
      float tweenDuration = this.m_style.GetTweenDuration(this.m_currentBaseLife, value);
      if (this.m_baseLifeTween != null)
        this.m_baseLifeTween.ChangeEndValue((object) value, tweenDuration);
      else
        this.m_baseLifeTween = DOTween.To(new DOGetter<int>(this.BaseLifeTweenGetter), new DOSetter<int>(this.BaseLifeTweenSetter), value, tweenDuration).SetEase<Tweener>(this.m_style.tweenEasing).OnComplete<Tweener>(new TweenCallback(this.OnBaseLifeTweenComplete));
      this.m_targetBaseLife = value;
    }

    public void ChangeLife(int value)
    {
      if (value == this.m_targetLife)
        return;
      float tweenDuration = this.m_style.GetTweenDuration(this.m_currentLife, value);
      if (this.m_lifeTween != null)
        this.m_lifeTween.ChangeEndValue((object) value, tweenDuration);
      else
        this.m_lifeTween = DOTween.To(new DOGetter<int>(this.LifeTweenGetter), new DOSetter<int>(this.LifeTweenSetter), value, tweenDuration).SetEase<Tweener>(this.m_style.tweenEasing).OnComplete<Tweener>(new TweenCallback(this.OnLifeTweenComplete));
      this.m_targetLife = value;
    }

    private int BaseLifeTweenGetter() => this.m_currentBaseLife;

    private void BaseLifeTweenSetter(int value)
    {
      this.m_currentBaseLife = value;
      this.Refresh();
    }

    private void OnBaseLifeTweenComplete() => this.m_baseLifeTween = (Tweener) null;

    private int LifeTweenGetter() => this.m_currentLife;

    private void LifeTweenSetter(int value)
    {
      this.m_currentLife = value;
      this.Refresh();
    }

    private void OnLifeTweenComplete() => this.m_lifeTween = (Tweener) null;

    private void Refresh()
    {
      this.m_text.SetText(string.Format("{0} / {1}", (object) this.m_currentLife, (object) this.m_currentBaseLife));
      this.m_imageFill.fillAmount = (float) this.m_currentLife / (float) this.m_currentBaseLife;
    }
  }
}
