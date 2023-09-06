// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.History.HistoryAbstractElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.History
{
  public abstract class HistoryAbstractElement : 
    MonoBehaviour,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler
  {
    [SerializeField]
    private Image m_back;
    [SerializeField]
    private Image m_illu;
    [SerializeField]
    private RectTransform m_animDummy;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private Graphic[] m_tintedGraphics;
    [SerializeField]
    private HistoryData m_data;
    private Coroutine m_loadIlluCoroutine;
    private bool m_illuLoaded;
    private Tweener m_overTween;
    private float m_overFactor;
    private float m_depthFactor = 1f;
    public Action<HistoryAbstractElement> onPointerEnter;
    public Action<HistoryAbstractElement> onPointerExit;

    public CanvasGroup canvasGroup => this.m_canvasGroup;

    public float depthFactor
    {
      set
      {
        this.m_depthFactor = value;
        this.UpdateOverFactor(this.m_overFactor);
      }
    }

    protected virtual void OnEnable() => this.UpdateIllu();

    protected virtual void OnDisable()
    {
      if (this.m_loadIlluCoroutine == null)
        return;
      this.StopCoroutine(this.m_loadIlluCoroutine);
      this.m_loadIlluCoroutine = (Coroutine) null;
    }

    protected void ApplyIllu(bool isLocalPlayer)
    {
      this.m_back.sprite = isLocalPlayer ? this.m_data.playerBg : this.m_data.opponentBg;
      this.m_illuLoaded = false;
      this.UpdateIllu();
    }

    private void UpdateIllu()
    {
      if (this.m_illuLoaded || !this.isActiveAndEnabled)
        return;
      if (!this.HasIllu())
      {
        this.m_illu.sprite = (Sprite) null;
      }
      else
      {
        if (this.m_loadIlluCoroutine != null)
        {
          this.StopCoroutine(this.m_loadIlluCoroutine);
          this.m_loadIlluCoroutine = (Coroutine) null;
          this.m_illu.sprite = (Sprite) null;
        }
        this.m_loadIlluCoroutine = this.StartCoroutine(this.LoadIllu(new Action<Sprite, string>(this.LoadIlluCallback)));
      }
    }

    private void LoadIlluCallback(Sprite sprite, string loadedBundleName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_illu)
      {
        this.m_illu.sprite = sprite;
        this.m_illuLoaded = true;
      }
      this.m_loadIlluCoroutine = (Coroutine) null;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
      if (this.m_overTween != null)
        this.m_overTween.Kill();
      this.m_overTween = (Tweener) DOTween.To((DOGetter<float>) (() => this.m_overFactor), new DOSetter<float>(this.UpdateOverFactor), 1f, this.m_data.elementsOverDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_data.elementsOverEase);
      Action<HistoryAbstractElement> onPointerEnter = this.onPointerEnter;
      if (onPointerEnter == null)
        return;
      onPointerEnter(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
      if (this.m_overTween != null)
        this.m_overTween.Kill();
      this.m_overTween = (Tweener) DOTween.To((DOGetter<float>) (() => this.m_overFactor), new DOSetter<float>(this.UpdateOverFactor), 0.0f, this.m_data.elementsOverDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_data.elementsOverEase);
      Action<HistoryAbstractElement> onPointerExit = this.onPointerExit;
      if (onPointerExit == null)
        return;
      onPointerExit(this);
    }

    private void UpdateOverFactor(float factor)
    {
      this.m_overFactor = factor;
      this.m_animDummy.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(this.m_data.elementsOverOffset, 0.0f), factor);
      Color color = Color.Lerp(Color.Lerp(this.m_data.elementsDepthColor, Color.white, this.m_depthFactor), Color.white, factor);
      for (int index = 0; index < this.m_tintedGraphics.Length; ++index)
        this.m_tintedGraphics[index].color = color;
    }

    protected abstract bool HasIllu();

    protected abstract IEnumerator LoadIllu(Action<Sprite, string> loadEndCallback);

    public abstract HistoryElementType type { get; }

    public abstract ITooltipDataProvider tooltipProvider { get; }
  }
}
