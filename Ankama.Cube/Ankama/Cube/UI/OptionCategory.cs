// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.OptionCategory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Code.UI;
using DG.Tweening;
using System;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public abstract class OptionCategory : MonoBehaviour
  {
    [SerializeField]
    private Canvas m_canvas;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    public Action<PopupInfo> OnErrorPopupInfo;

    private float alpha
    {
      set => this.m_canvasGroup.alpha = value;
      get => this.m_canvasGroup.alpha;
    }

    public virtual void Initialize(bool visible)
    {
      this.SetVisible(visible);
      this.alpha = visible ? 1f : 0.0f;
    }

    public Tween FadeIn(float refDuration)
    {
      float num = 1f;
      float duration = refDuration * (num - this.alpha);
      return this.DoFade(num, duration).OnStart<Tween>(new TweenCallback(this.OnFadeInStart));
    }

    private void OnFadeInStart() => this.SetVisible(true);

    public Tween FadeOut(float refDuration) => this.DoFade(0.0f, refDuration * this.alpha).OnComplete<Tween>(new TweenCallback(this.OnFadeOutComplete));

    private void OnFadeOutComplete() => this.SetVisible(false);

    private Tween DoFade(float value, float duration) => (Tween) this.m_canvasGroup.DOFade(value, duration);

    private void SetVisible(bool value)
    {
      if ((UnityEngine.Object) this.m_canvas != (UnityEngine.Object) null)
        this.m_canvas.enabled = value;
      else
        this.gameObject.SetActive(value);
      this.m_canvasGroup.enabled = value;
      if (value)
        this.OnBecameEnable();
      else
        this.OnBecameDisable();
    }

    protected abstract void OnBecameEnable();

    protected abstract void OnBecameDisable();
  }
}
