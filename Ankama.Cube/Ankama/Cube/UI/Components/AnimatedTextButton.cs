// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedTextButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class AnimatedTextButton : Button
  {
    [SerializeField]
    private TextField m_text;
    [SerializeField]
    private AnimatedTextButtonStyle m_style;
    [SerializeField]
    private Image m_background;
    [SerializeField]
    private Image m_border;
    [SerializeField]
    private Image m_outline;
    private RectTransform m_outlineRectTransform;
    private RectTransform m_backgroundRectTransform;
    private Sequence m_tweenSequence;

    public TextField textField => this.m_text;

    protected override void Awake()
    {
      if ((bool) (Object) this.m_outline)
        this.m_outlineRectTransform = this.m_outline.GetComponent<RectTransform>();
      if (!(bool) (Object) this.m_background)
        return;
      this.m_backgroundRectTransform = this.m_background.GetComponent<RectTransform>();
    }

    protected override void OnEnable()
    {
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedTextButton " + this.name + " doesn't have a style defined !", 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedTextButton.cs");
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
        if ((Object) EventSystem.current != (Object) null && (Object) EventSystem.current.currentSelectedGameObject == (Object) this.gameObject)
        {
          EventSystem.current.SetSelectedGameObject((GameObject) null);
          this.Select();
        }
        base.OnEnable();
      }
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("AnimatedTextButton " + this.name + " doesn't have a style defined !", 92, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AnimatedTextButton.cs");
      }
      else
      {
        AnimatedTextButtonState animatedTextButtonState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            animatedTextButtonState = this.m_style.normal;
            break;
          case Selectable.SelectionState.Highlighted:
            animatedTextButtonState = this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            animatedTextButtonState = this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            animatedTextButtonState = this.m_style.disable;
            break;
        }
        Sequence tweenSequence = this.m_tweenSequence;
        if (tweenSequence != null)
          tweenSequence.Kill();
        if (instant)
        {
          if ((bool) (Object) this.m_text)
            this.m_text.color = animatedTextButtonState.textColor;
          if ((bool) (Object) this.m_background)
            this.m_background.color = animatedTextButtonState.backgroundColor;
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_backgroundRectTransform.sizeDelta = animatedTextButtonState.backgroundSizeDelta;
          if ((bool) (Object) this.m_border)
            this.m_border.color = animatedTextButtonState.borderColor;
          if ((bool) (Object) this.m_outline)
            this.m_outline.color = animatedTextButtonState.outlineColor;
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_outlineRectTransform.sizeDelta = animatedTextButtonState.outlineSizeDelta;
        }
        else
        {
          this.m_tweenSequence = DOTween.Sequence();
          if ((bool) (Object) this.m_text)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_text.DOColor(animatedTextButtonState.textColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_background)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_background, animatedTextButtonState.backgroundColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_backgroundRectTransform)
            this.m_tweenSequence.Insert(0.0f, (Tween) this.m_backgroundRectTransform.DOSizeDelta(animatedTextButtonState.backgroundSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_border)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_border, animatedTextButtonState.borderColor, this.m_style.baseButtonStyle.transitionDuration));
          if ((bool) (Object) this.m_outline)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_outline, animatedTextButtonState.outlineColor, this.m_style.baseButtonStyle.transitionDuration));
          if (!(bool) (Object) this.m_outlineRectTransform)
            return;
          this.m_tweenSequence.Insert(0.0f, (Tween) this.m_outlineRectTransform.DOSizeDelta(animatedTextButtonState.outlineSizeDelta, this.m_style.baseButtonStyle.transitionDuration));
        }
      }
    }
  }
}
