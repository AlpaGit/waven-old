// Decompiled with JetBrains decompiler
// Type: DeckEditToggleParent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.DeckMaker;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckEditToggleParent : MonoBehaviour
{
  [SerializeField]
  private List<DeckEditToggleFilter> m_filtersToogle;
  [SerializeField]
  private InputTextField m_searchTextField;
  private float m_outYPosition;
  private Action m_refreshFilter;
  private string m_previousText;
  private RectTransform m_rect;

  public IEnumerable<DeckEditToggleFilter> ToggleFilter => (IEnumerable<DeckEditToggleFilter>) this.m_filtersToogle;

  public void Initialise(Action onFilterChange, float outPosition)
  {
    this.m_previousText = this.m_searchTextField.GetText();
    this.m_rect = this.GetComponent<RectTransform>();
    this.m_refreshFilter = onFilterChange;
    foreach (DeckEditToggleFilter editToggleFilter in this.m_filtersToogle)
      editToggleFilter.Initialise(new Action<bool>(this.OnSubFilterChange));
    this.m_outYPosition = outPosition;
    this.m_searchTextField.onValueChanged.AddListener((UnityAction<string>) (s => this.OnTextChange(s)));
  }

  protected void OnSubFilterChange(bool b)
  {
    Action refreshFilter = this.m_refreshFilter;
    if (refreshFilter == null)
      return;
    refreshFilter();
  }

  private void OnTextChange(string t)
  {
    if (string.Compare(this.m_previousText, t) == 0)
      return;
    this.m_previousText = t;
    this.m_refreshFilter();
  }

  public void OnEditModeChange(EditModeSelection selection)
  {
    foreach (DeckEditToggleFilter editToggleFilter in this.m_filtersToogle)
      editToggleFilter.OnEditModeChange(selection);
    this.m_searchTextField.SetText("");
  }

  public Tween TweenOut(float duration, Ease ease) => (Tween) this.m_rect.DOAnchorPosY(this.m_outYPosition, duration).SetEase<Tweener>(ease);

  public Tween TweenIn(float duration, Ease ease) => (Tween) this.m_rect.DOAnchorPosY(0.0f, duration).SetEase<Tweener>(ease);

  public string GetTextFilter() => this.m_searchTextField.GetText();
}
