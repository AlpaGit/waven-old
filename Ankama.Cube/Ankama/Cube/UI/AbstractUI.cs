// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.AbstractUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  [RequireComponent(typeof (Canvas))]
  [RequireComponent(typeof (CanvasGroup))]
  public class AbstractUI : MonoBehaviour
  {
    [Header("Animation")]
    [SerializeField]
    protected UIAnimationDirector m_animationDirector;
    private const float FullBlurAmount = 0.75f;
    [SerializeField]
    [HideInInspector]
    private Canvas m_canvas;
    [SerializeField]
    [HideInInspector]
    protected CanvasGroup m_canvasGroup;
    [SerializeField]
    [HideInInspector]
    protected bool m_useBlur;
    [SerializeField]
    [HideInInspector]
    [Range(0.0f, 1f)]
    private float m_blurAmount = 1f;
    private float m_lastFrameBlurAmount;
    public Action onBlurFactorIsFull;
    public Action onBlurFactorStartDecrease;

    public Canvas canvas => this.m_canvas;

    public CanvasGroup canvasGroup => this.m_canvasGroup;

    public float blurAmount
    {
      get => this.m_blurAmount;
      set => this.m_blurAmount = value;
    }

    public bool useBlur
    {
      get => this.m_useBlur;
      set
      {
        if (this.m_useBlur == value)
          return;
        this.m_useBlur = value;
        UIManager instance = UIManager.instance;
        if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
          return;
        instance.UseBlurChanged(this, value);
      }
    }

    public bool interactable
    {
      get => this.canvasGroup.interactable;
      set => this.canvasGroup.interactable = value;
    }

    public void SetDepth(StateContext state, int index = -1)
    {
      UIManager instance = UIManager.instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.NotifyUIDepthChanged(this, state, index);
    }

    protected virtual void Awake()
    {
      Device.ScreenStateChanged += new Device.ScreenSateChangedDelegate(this.RescaleCanvas);
      this.RescaleCanvas();
    }

    private void RescaleCanvas()
    {
      CanvasScaler component = this.GetComponent<CanvasScaler>();
      if ((UnityEngine.Object) component == (UnityEngine.Object) null)
        return;
      if ((double) Screen.width / (double) Screen.height > 1.7777777910232544)
        component.matchWidthOrHeight = 1f;
      else
        component.matchWidthOrHeight = 0.0f;
    }

    public IEnumerator LoadAssets()
    {
      if ((UnityEngine.Object) this.m_animationDirector != (UnityEngine.Object) null)
        yield return (object) this.m_animationDirector.Load();
    }

    public void UnloadAsset()
    {
      if (!((UnityEngine.Object) this.m_animationDirector != (UnityEngine.Object) null))
        return;
      this.m_animationDirector.Unload();
    }

    protected virtual void OnEnable()
    {
      this.m_lastFrameBlurAmount = this.m_blurAmount;
      UIManager instance = UIManager.instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.EnableStateChanged(this, true);
    }

    protected virtual void OnDisable()
    {
      UIManager instance = UIManager.instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.EnableStateChanged(this, false);
    }

    protected virtual void OnDestroy()
    {
      Device.ScreenStateChanged -= new Device.ScreenSateChangedDelegate(this.RescaleCanvas);
      UIManager instance = UIManager.instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.NotifyUIDestroyed(this);
    }

    protected virtual void Update()
    {
      if (!this.m_useBlur)
        return;
      this.UpdateBlurAmount();
    }

    private void UpdateBlurAmount()
    {
      if ((double) this.m_blurAmount > 0.75 && (double) this.m_lastFrameBlurAmount <= 0.75)
      {
        Action blurFactorIsFull = this.onBlurFactorIsFull;
        if (blurFactorIsFull != null)
          blurFactorIsFull();
      }
      else if ((double) this.m_blurAmount < 0.75 && (double) this.m_lastFrameBlurAmount >= 0.75)
      {
        Action factorStartDecrease = this.onBlurFactorStartDecrease;
        if (factorStartDecrease != null)
          factorStartDecrease();
      }
      this.m_lastFrameBlurAmount = this.m_blurAmount;
    }

    protected IEnumerator PlayAnimation(TimelineAsset animation)
    {
      PlayableDirector director = this.m_animationDirector.GetDirector();
      director.Play((PlayableAsset) animation);
      PlayableGraph graph = director.playableGraph;
      while (graph.IsValid() && !graph.IsDone())
        yield return (object) null;
    }
  }
}
