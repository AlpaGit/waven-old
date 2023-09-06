// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.DeckPresetPanel
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.UI.DeckMaker;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class DeckPresetPanel : MonoBehaviour
  {
    [Header("Button")]
    [SerializeField]
    private Transform m_editButtonTransform;
    [Header("Presets")]
    [SerializeField]
    private Transform m_customPresetRoot;
    [SerializeField]
    private List<Sprite> m_deckIcons;
    [SerializeField]
    private DeckPresetButton m_defaultRibbon;
    private WeaponDefinition m_definition;
    private List<DeckPresetButton> m_presets;
    private DeckSlot m_selectedSlot;
    private ToggleGroup m_toggleGroup;
    private WeaponAndDeckModifications m_modifications;

    public event Action<DeckSlot> OnSelectionChange;

    public void InitialiseUI(WeaponAndDeckModifications modifications, Action<DeckSlot> OnEditSlot)
    {
      this.m_modifications = modifications;
      if ((UnityEngine.Object) this.m_toggleGroup == (UnityEngine.Object) null)
      {
        this.m_toggleGroup = this.gameObject.GetComponent<ToggleGroup>();
        if ((UnityEngine.Object) this.m_toggleGroup == (UnityEngine.Object) null)
          this.m_toggleGroup = this.gameObject.AddComponent<ToggleGroup>();
      }
      this.m_presets = new List<DeckPresetButton>();
      this.m_presets.Add(this.m_defaultRibbon);
      this.m_defaultRibbon.Initialise(this, this.m_deckIcons[0], this.m_toggleGroup, OnEditSlot);
      for (int index = 1; index < 4; ++index)
      {
        DeckPresetButton deckPresetButton = UnityEngine.Object.Instantiate<DeckPresetButton>(this.m_defaultRibbon, this.m_customPresetRoot);
        deckPresetButton.OnSelectRequest += new Action<DeckSlot>(this.OnSelectionChanged);
        this.m_presets.Add(deckPresetButton);
        deckPresetButton.Initialise(this, this.m_deckIcons[index], this.m_toggleGroup, OnEditSlot);
      }
      this.m_defaultRibbon.SetDefaultVisual();
      this.m_defaultRibbon.OnSelectRequest += new Action<DeckSlot>(this.OnSelectionChanged);
      this.m_editButtonTransform.SetAsLastSibling();
    }

    private void OnSelectionChanged(DeckSlot deckSlot)
    {
      this.m_selectedSlot = deckSlot;
      Action<DeckSlot> onSelectionChange = this.OnSelectionChange;
      if (onSelectionChange == null)
        return;
      onSelectionChange(deckSlot);
    }

    public void OnEquippedDeckUpdate()
    {
      int selectedDeckForWeapon = this.m_modifications.GetSelectedDeckForWeapon(this.m_definition.id);
      foreach (DeckPresetButton preset in this.m_presets)
      {
        int num1;
        if (preset.GetSlot() != null)
        {
          int? id = preset.GetSlot().Id;
          int num2 = selectedDeckForWeapon;
          num1 = id.GetValueOrDefault() == num2 & id.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        bool equipped = num1 != 0;
        preset.SetEquipped(equipped);
      }
    }

    public void Populate(List<DeckSlot> deckSlots, WeaponDefinition weapon)
    {
      this.m_definition = weapon;
      int index1 = 0;
      int selectedDeckForWeapon = this.m_modifications.GetSelectedDeckForWeapon(weapon.id);
      for (int index2 = 0; index2 < deckSlots.Count; ++index2)
      {
        this.m_presets[index2].ForceSelect(false);
        DeckSlot deckSlot = deckSlots[index2];
        int? id;
        if (deckSlot.DeckInfo != null)
        {
          id = deckSlot.DeckInfo.Id;
          if (id.HasValue)
          {
            id = deckSlot.DeckInfo.Id;
            if (id.Value == selectedDeckForWeapon)
              index1 = index2;
          }
        }
        DeckInfo deckInfo = deckSlot.DeckInfo;
        int num1;
        if (deckInfo == null)
        {
          num1 = 1;
        }
        else
        {
          id = deckInfo.Id;
          num1 = !id.HasValue ? 1 : 0;
        }
        int num2 = num1 != 0 ? 1 : (deckSlot.isAvailableEmptyDeckSlot ? 1 : 0);
        this.m_presets[index2].Populate(deckSlot, selectedDeckForWeapon);
      }
      this.m_presets[index1].ForceSelect();
    }

    public void Unload()
    {
      foreach (DeckPresetButton preset in this.m_presets)
        preset.OnSelectRequest -= new Action<DeckSlot>(this.OnSelectionChanged);
    }

    public DeckSlot GetSelectedDeck() => this.m_selectedSlot;
  }
}
