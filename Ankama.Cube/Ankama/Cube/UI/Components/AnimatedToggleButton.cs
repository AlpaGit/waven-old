// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedToggleButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class AnimatedToggleButton : Toggle
  {
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private Image m_border;
    [SerializeField]
    private Image m_outline;
    [SerializeField]
    private AnimatedToggleButtonStyle m_style;
    [SerializeField]
    private Image m_tickImage;
    private RectTransform m_backgroundRectTransform;
    private RectTransform m_graphicRectTransform;
    private RectTransform m_outlineRectTransform;
    private Sequence m_tweenSequence;

    protected override void Awake()
    {
      if ((bool) (Object) this.m_outline)
        this.m_outlineRectTransform = this.m_outline.GetComponent<RectTransform>();
      if ((bool) (Object) this.m_background)
        this.m_backgroundRectTransform = this.m_background.GetComponent<RectTransform>();
      if ((bool) (Object) this.m_tickImage)
        this.m_graphicRectTransform = this.m_tickImage.GetComponent<RectTransform>();
      this.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
      this.OnValueChanged(this.isOn);
    }

    private void OnValueChanged(bool on)
    {
      if ((Object) this.m_tickImage == (Object) null)
        return;
      Color endValue = this.m_tickImage.color;
      if (this.m_style.useOnlyAlpha)
        endValue.a = this.isOn ? this.m_style.selectedGraphicColor.a : this.m_style.baseGraphicColor.a;
      else
        endValue = on ? this.m_style.selectedGraphicColor : this.m_style.baseGraphicColor;
      if (!Application.isPlaying)
        this.m_tickImage.color = endValue;
      else
        DOTweenModuleUI.DOBlendableColor(this.m_tickImage, endValue, this.m_style.selectionTransitionDuration);
    }

    protected override void OnEnable()
    {
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedToggleButton " + this.name + " doesn't have a style defined !", 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedToggleButton.cs");
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
        if ((bool) (Object) this.m_tickImage)
          this.m_tickImage.sprite = this.m_style.baseButtonStyle.toggle;
        base.OnEnable();
      }
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedToggleButton " + this.name + " doesn't have a style defined !", 102, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedToggleButton.cs");
      }
      else
      {
        AnimatedToggleButtonState toggleButtonState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            toggleButtonState = this.m_style.normal;
            break;
          case Selectable.SelectionState.Highlighted:
            toggleButtonState = this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            toggleButtonState = this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            toggleButtonState = this.m_style.disable;
            break;
        }
        if (toggleButtonState.Equals((object) this.m_style.highlight) && (Object) this.group != (Object) null && !this.group.allowSwitchOff && this.isOn)
          return;
        Sequence tweenSequence = this.m_tweenSequence;
        if (tweenSequence != null)
          tweenSequence.Kill();
        if (instant)
        {
          if ((bool) (Object) this.m_graphicRectTransform)
            this.m_graphicRectTransform.sizeDelta = toggleButtonState.graphicSizeDelta;
          if ((bool) (Object) this.m_background)
            this.m_background.color = toggleButtonState.backgroundColor;
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_backgroundRectTransform.sizeDelta = toggleButtonState.backgroundSizeDelta;
          if ((bool) (Object) this.m_border)
            this.m_border.color = toggleButtonState.borderColor;
          if ((bool) (Object) this.m_outline)
            this.m_outline.color = toggleButtonState.outlineColor;
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_outlineRectTransform.sizeDelta = toggleButtonState.outlineSizeDelta;
        }
        else
        {
          this.m_tweenSequence = DOTween.Sequence();
          if ((bool) (Object) this.m_graphicRectTransform)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_graphicRectTransform.DOSizeDelta(toggleButtonState.graphicSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_background)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_background, toggleButtonState.backgroundColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_backgroundRectTransform.DOSizeDelta(toggleButtonState.backgroundSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_border)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_border, toggleButtonState.borderColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_outline)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_outline, toggleButtonState.outlineColor, this.m_style.baseButtonStyle.transitionDuration));
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_tweenSequence.Insert(0.0f, (Tween) this.m_outlineRectTransform.DOSizeDelta(toggleButtonState.outlineSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
        }
      }
    }
  }
}
