// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.OptionUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Code.UI;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class OptionUI : BaseOpenCloseUI
  {
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private TabButton[] m_categoryButtons;
    [SerializeField]
    private OptionCategory[] m_category;
    [SerializeField]
    private float m_transitionDuration = 0.15f;
    private OptionCategory m_selectedCategory;
    private OptionCategory m_previousCategory;
    private Sequence m_transitionTweenSequence;
    public Action onCloseClick;
    public Action<PopupInfo> OnErrorPopupInfo;

    public void Initialise()
    {
      for (int index = 0; index < this.m_categoryButtons.Length; ++index)
      {
        TabButton categoryButton = this.m_categoryButtons[index];
        categoryButton.isOn = index == 0;
        categoryButton.onValueChanged.AddListener(new UnityAction<bool>(this.OnCategorySelected));
      }
      for (int index = 0; index < this.m_category.Length; ++index)
      {
        OptionCategory optionCategory = this.m_category[index];
        optionCategory.OnErrorPopupInfo += new Action<PopupInfo>(this.OnCategoryErrorPopupInfo);
        optionCategory.gameObject.SetActive(true);
        if (index == 0)
        {
          this.m_selectedCategory = optionCategory;
          optionCategory.Initialize(true);
        }
        else
          optionCategory.Initialize(false);
      }
      this.m_closeButton.onClick.AddListener((UnityAction) (() => this.OnCloseClick()));
    }

    private void OnCategoryErrorPopupInfo(PopupInfo popupInfo)
    {
      Action<PopupInfo> onErrorPopupInfo = this.OnErrorPopupInfo;
      if (onErrorPopupInfo == null)
        return;
      onErrorPopupInfo(popupInfo);
    }

    private void OnCategorySelected(bool value)
    {
      if (!value)
        return;
      for (int index = 0; index < this.m_categoryButtons.Length; ++index)
      {
        if (this.m_categoryButtons[index].isOn)
        {
          this.ShowCategory(this.m_category[index]);
          break;
        }
      }
    }

    private void ShowCategory(OptionCategory selectedCategory)
    {
      if ((UnityEngine.Object) selectedCategory == (UnityEngine.Object) this.m_selectedCategory)
        return;
      if (this.m_transitionTweenSequence != null && this.m_transitionTweenSequence.IsActive())
        this.m_transitionTweenSequence.Kill();
      this.m_previousCategory = this.m_selectedCategory;
      this.m_selectedCategory = selectedCategory;
      this.m_transitionTweenSequence = DOTween.Sequence();
      if ((UnityEngine.Object) this.m_previousCategory != (UnityEngine.Object) null)
        this.m_transitionTweenSequence.Append(this.m_previousCategory.FadeOut(this.m_transitionDuration));
      if (!((UnityEngine.Object) this.m_selectedCategory != (UnityEngine.Object) null))
        return;
      this.m_transitionTweenSequence.Append(this.m_selectedCategory.FadeIn(this.m_transitionDuration));
    }

    public void OnCloseClick()
    {
      Action onCloseClick = this.onCloseClick;
      if (onCloseClick == null)
        return;
      onCloseClick();
    }

    public void SimulateCloseClick() => InputUtility.SimulateClickOn((Selectable) this.m_closeButton);
  }
}
