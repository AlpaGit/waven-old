// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.GameSelectionButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class GameSelectionButton : Button
  {
    [SerializeField]
    private GameSelectionButtonStyle m_style;
    [SerializeField]
    private Image m_image;
    [SerializeField]
    private Image m_outline;
    private readonly List<Tweener> m_currentTweens = new List<Tweener>();
    public Action<GameSelectionButton> onPointerEnter;
    public Action<GameSelectionButton> onPointerExit;
    private bool m_delayAnim;
    private bool m_anotherButtonIsHightlighted;

    public bool anotherButtonIsHightlighted
    {
      set
      {
        if (!this.interactable)
          return;
        if (this.m_anotherButtonIsHightlighted && !value && this.currentSelectionState == Selectable.SelectionState.Normal)
          this.m_delayAnim = true;
        this.m_anotherButtonIsHightlighted = value;
        this.DoStateTransition(this.currentSelectionState, false);
        this.m_delayAnim = false;
      }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
      base.OnPointerEnter(eventData);
      if (!this.interactable)
        return;
      Action<GameSelectionButton> onPointerEnter = this.onPointerEnter;
      if (onPointerEnter == null)
        return;
      onPointerEnter(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
      base.OnPointerExit(eventData);
      if (!this.interactable)
        return;
      Action<GameSelectionButton> onPointerExit = this.onPointerExit;
      if (onPointerExit == null)
        return;
      onPointerExit(this);
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((UnityEngine.Object) this.m_style == (UnityEngine.Object) null)
      {
        Log.Error("AnimatedTextButton " + this.name + " doesn't have a style defined !", 69, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\UI\\GameSelection\\GameSelectionButton.cs");
      }
      else
      {
        GameSelectionButtonState selectionButtonState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            selectionButtonState = !this.m_anotherButtonIsHightlighted ? this.m_style.normal : this.m_style.normalButAnotherIsHighlighted;
            break;
          case Selectable.SelectionState.Highlighted:
            selectionButtonState = this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            selectionButtonState = this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            selectionButtonState = this.m_style.disable;
            break;
        }
        if (instant)
        {
          if ((bool) (UnityEngine.Object) this.m_image)
          {
            this.m_image.color = selectionButtonState.imageColor;
            this.m_image.transform.localScale = Vector3.one * selectionButtonState.scale;
          }
          if (!(bool) (UnityEngine.Object) this.m_outline)
            return;
          this.m_outline.color = selectionButtonState.outlineColor;
        }
        else
        {
          int index1 = 0;
          for (int count = this.m_currentTweens.Count; index1 < count; ++index1)
          {
            Tweener currentTween = this.m_currentTweens[index1];
            if (currentTween.IsActive())
              currentTween.Kill();
          }
          this.m_currentTweens.Clear();
          if ((bool) (UnityEngine.Object) this.m_image)
          {
            this.m_currentTweens.Add(DOTweenModuleUI.DOColor(this.m_image, selectionButtonState.imageColor, this.m_style.transitionDuration));
            this.m_currentTweens.Add(this.m_image.transform.DOScale(selectionButtonState.scale, this.m_style.transitionDuration));
          }
          if ((bool) (UnityEngine.Object) this.m_outline)
            this.m_currentTweens.Add(DOTweenModuleUI.DOBlendableColor(this.m_outline, selectionButtonState.outlineColor, this.m_style.transitionDuration));
          int index2 = 0;
          for (int count = this.m_currentTweens.Count; index2 < count; ++index2)
          {
            Tweener currentTween = this.m_currentTweens[index2];
            currentTween.SetEase<Tweener>(this.m_style.ease);
            if (this.m_delayAnim)
              currentTween.SetDelay<Tweener>(this.m_style.fromNormalAndUnHighlightedDelay);
          }
        }
      }
    }
  }
}
