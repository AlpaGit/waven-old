// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckSlotMiniature
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using Ankama.Cube.Utility;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckSlotMiniature : CellRenderer<DeckSlot, IDeckSlotCellRendererConfigurator>
  {
    private WeaponDefinition m_definition;
    private bool m_isOn = true;
    private DeckSlot m_previousValue;
    [SerializeField]
    private Image m_illustration;
    [SerializeField]
    private Button m_button;
    [SerializeField]
    private Image m_invalidDeck;
    [SerializeField]
    private Sprite m_emptySprite;

    private void Awake() => this.m_button.onClick.AddListener(new UnityAction(this.OnButtonClicked));

    protected override void SetValue(DeckSlot deckSlot)
    {
      if (this.m_previousValue != null)
        this.m_previousValue.OnModification -= new Action<DeckSlot>(this.OnModification);
      if (deckSlot != null)
        deckSlot.OnModification += new Action<DeckSlot>(this.OnModification);
      this.m_previousValue = deckSlot;
    }

    private void SetValueInternal(DeckSlot slot)
    {
      WeaponDefinition definition1 = this.m_definition;
      WeaponDefinition definition2;
      this.GetWeaponDefinition(slot, out definition2);
      this.m_definition = definition2;
      WeaponDefinition definition3 = this.m_definition;
      if ((UnityEngine.Object) definition1 != (UnityEngine.Object) definition3)
        this.UpdateIllustration();
      this.UpdateInvalidDeck();
    }

    private void UpdateInvalidDeck()
    {
      DeckSlot deckSlot = this.m_value;
      if (deckSlot == null || deckSlot.isAvailableEmptyDeckSlot)
        this.m_invalidDeck.gameObject.SetActive(false);
      else
        this.m_invalidDeck.gameObject.SetActive(!deckSlot.DeckInfo.IsValid());
    }

    private void GetWeaponDefinition(DeckSlot slot, out WeaponDefinition definition)
    {
      definition = (WeaponDefinition) null;
      if ((slot != null ? (!slot.Id.HasValue ? 1 : 0) : 1) != 0)
        return;
      int? weapon = slot.Weapon;
      if (!weapon.HasValue)
        return;
      RuntimeData.weaponDefinitions.TryGetValue(weapon.Value, out definition);
    }

    private void UpdateIllustration()
    {
      this.m_illustration.enabled = false;
      if ((UnityEngine.Object) this.m_definition == (UnityEngine.Object) null)
      {
        this.m_illustration.sprite = this.m_emptySprite;
        this.m_illustration.enabled = true;
      }
      else
      {
        AssetReference illustrationReference = this.m_definition.GetIllustrationReference();
        if (!illustrationReference.hasValue)
          return;
        Main.monoBehaviour.StartCoroutine(this.m_definition.LoadIllustrationAsync<Sprite>(AssetBundlesUtility.GetUICharacterResourcesBundleName(), illustrationReference, new Action<Sprite, string>(this.UpdateIllustrationCallback)));
      }
    }

    private void UpdateIllustrationCallback(Sprite sprite, string loadedBundleName)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_illustration))
        return;
      this.m_illustration.sprite = sprite;
      this.m_illustration.enabled = (UnityEngine.Object) null != (UnityEngine.Object) sprite;
    }

    protected override void Clear() => this.SetValue((DeckSlot) null);

    private void OnModification(DeckSlot obj) => this.SetValueInternal(obj);

    private void SetIsOn(bool isOn, bool instant)
    {
      if (this.m_isOn == isOn)
        return;
      this.m_isOn = isOn;
      float num = isOn ? 1.2f : 1f;
      Color endValue = isOn ? Color.white : Color.gray;
      if (instant)
      {
        this.m_illustration.transform.localScale = new Vector3(num, num, 1f);
        this.m_illustration.color = endValue;
        this.m_invalidDeck.transform.localScale = new Vector3(num, num, 1f);
        this.m_invalidDeck.color = endValue;
      }
      else
      {
        this.m_illustration.transform.DOScale(num, 0.15f);
        DOTweenModuleUI.DOColor(this.m_illustration, endValue, 0.15f);
        this.m_invalidDeck.transform.DOScale(num, 0.15f);
        DOTweenModuleUI.DOColor(this.m_invalidDeck, endValue, 0.15f);
      }
    }

    private void OnButtonClicked() => this.m_configurator?.OnClicked(this.m_value);

    public override void OnConfiguratorUpdate(bool instant)
    {
      DeckSlot slot = this.m_value;
      int? slotId1 = this.m_configurator?.currentSlot?.SlotId;
      int? slotId2 = slot?.SlotId;
      this.SetIsOn(slotId1.GetValueOrDefault() == slotId2.GetValueOrDefault() & slotId1.HasValue == slotId2.HasValue, instant);
      this.SetValueInternal(slot);
    }
  }
}
