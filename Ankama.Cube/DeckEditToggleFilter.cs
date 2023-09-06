// Decompiled with JetBrains decompiler
// Type: DeckEditToggleFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.DeckMaker;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeckEditToggleFilter : MonoBehaviour
{
  [SerializeField]
  private CaracId m_element;
  [SerializeField]
  private Element m_spellElement;
  [SerializeField]
  private bool m_SpellFilter;
  [SerializeField]
  private bool m_CompanionFilter;
  [Header("Visual")]
  [SerializeField]
  private Image m_ElementPicto;
  [SerializeField]
  private Color m_defaultColor;
  [SerializeField]
  private Color m_activColor;
  private AnimatedToggleButton m_toggle;
  private Action<bool> m_onFilterChange;

  public CaracId GetElement() => this.m_element;

  public Element GetSpellElement() => this.m_spellElement;

  public bool IsEnabled() => this.m_toggle.isOn;

  public void Initialise(Action<bool> onFilterChange)
  {
    this.m_onFilterChange = onFilterChange;
    this.m_toggle = this.GetComponent<AnimatedToggleButton>();
    this.m_toggle.onValueChanged.AddListener((UnityAction<bool>) (b => this.OnValueChange(b)));
  }

  private void OnValueChange(bool on)
  {
    this.m_ElementPicto.color = on ? this.m_activColor : this.m_defaultColor;
    Action<bool> onFilterChange = this.m_onFilterChange;
    if (onFilterChange == null)
      return;
    onFilterChange(on);
  }

  public void OnEditModeChange(EditModeSelection selection)
  {
    switch (selection)
    {
      case EditModeSelection.Spell:
        this.m_toggle.interactable = this.m_SpellFilter;
        this.gameObject.SetActive(this.m_SpellFilter);
        break;
      case EditModeSelection.Companion:
        this.gameObject.SetActive(this.m_CompanionFilter);
        this.m_toggle.interactable = this.m_CompanionFilter;
        break;
      default:
        this.m_toggle.interactable = false;
        break;
    }
    if (this.m_toggle.interactable)
      return;
    this.m_toggle.isOn = false;
  }
}
