// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedGraphicButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class AnimatedGraphicButton : Button
  {
    [SerializeField]
    private Graphic m_image;
    [SerializeField]
    private AnimatedImageButtonStyle m_style;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private Image m_border;
    [SerializeField]
    private Image m_outline;
    private RectTransform m_imageRectTransform;
    private RectTransform m_outlineRectTransform;
    private RectTransform m_backgroundRectTransform;
    private Sequence m_tweenSequence;

    public Graphic buttonImage => this.m_image;

    protected override void Awake()
    {
      if ((bool) (Object) this.m_outline)
        this.m_outlineRectTransform = this.m_outline.GetComponent<RectTransform>();
      if ((bool) (Object) this.m_background)
        this.m_backgroundRectTransform = this.m_background.GetComponent<RectTransform>();
      if (!(bool) (Object) this.m_image)
        return;
      this.m_imageRectTransform = this.m_image.GetComponent<RectTransform>();
    }

    protected override void OnEnable()
    {
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedImageButton " + this.name + " doesn't have a style defined !", 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedGraphicButton.cs");
        base.OnEnable();
      }
      else
      {
        if ((bool) (Object) this.m_background)
          this.m_background.sprite = this.m_style.baseButtonStyle.background;
        if ((bool) (Object) this.m_border)
          this.m_border.sprite = this.m_style.baseButtonStyle.border;
        if ((bool) (Object) this.m_outline)
          this.m_outline.sprite = this.m_style.baseButtonStyle.outline;
        base.OnEnable();
      }
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedImageButton " + this.name + " doesn't have a style defined !", 84, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedGraphicButton.cs");
      }
      else
      {
        AnimatedImageButtonState imageButtonState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            imageButtonState = this.m_style.normal;
            break;
          case Selectable.SelectionState.Highlighted:
            imageButtonState = this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            imageButtonState = this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            imageButtonState = this.m_style.disable;
            break;
        }
        Sequence tweenSequence = this.m_tweenSequence;
        if (tweenSequence != null)
          tweenSequence.Kill();
        if (instant)
        {
          if ((bool) (Object) this.m_image)
            this.m_image.color = imageButtonState.imageColor;
          if ((bool) (Object) this.m_imageRectTransform)
            this.m_imageRectTransform.sizeDelta = imageButtonState.imageSizeDelta;
          if ((bool) (Object) this.m_background)
            this.m_background.color = imageButtonState.backgroundColor;
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_backgroundRectTransform.sizeDelta = imageButtonState.backgroundSizeDelta;
          if ((bool) (Object) this.m_border)
            this.m_border.color = imageButtonState.borderColor;
          if ((bool) (Object) this.m_outline)
            this.m_outline.color = imageButtonState.outlineColor;
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_outlineRectTransform.sizeDelta = imageButtonState.outlineSizeDelta;
        }
        else
        {
          this.m_tweenSequence = DOTween.Sequence();
          if ((bool) (Object) this.m_image)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_image.DOColor(imageButtonState.imageColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_imageRectTransform)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_imageRectTransform.DOSizeDelta(imageButtonState.imageSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_background)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_background, imageButtonState.backgroundColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_backgroundRectTransform.DOSizeDelta(imageButtonState.backgroundSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_border)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_border, imageButtonState.borderColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_outline)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_outline, imageButtonState.outlineColor, this.m_style.baseButtonStyle.transitionDuration));
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_tweenSequence.Insert(0.0f, (Tween) this.m_outlineRectTransform.DOSizeDelta(imageButtonState.outlineSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
        }
      }
    }
  }
}
