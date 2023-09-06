// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.DeckPresetButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.DeckMaker;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class DeckPresetButton : MonoBehaviour
  {
    private DeckSlot m_deckSlot;
    private Vector2 m_defaultSizeDelta;
    private RectTransform m_rectTransform;
    private Vector2 m_selectedSizeDelta;
    private AnimatedToggleButton m_toggle;
    [Header("Visual")]
    [SerializeField]
    private Image m_bg;
    [SerializeField]
    private Image m_deckIcon;
    [SerializeField]
    private GameObject m_equipedBG;
    [SerializeField]
    private Image m_slectedOutline;
    [SerializeField]
    private RawTextField m_presetName;
    [SerializeField]
    private Image m_invalidFeedback;
    [Header("Button")]
    [SerializeField]
    private AnimatedGraphicButton m_editButton;
    [Header("Visual")]
    [SerializeField]
    private GameObject m_bgDefaultVisual;
    [SerializeField]
    private Image m_btnIcon;
    [SerializeField]
    private Sprite m_btnIconSprite;

    public event Action<DeckSlot> OnSelectRequest;

    public void Initialise(
      DeckPresetPanel parent,
      Sprite sprite,
      ToggleGroup group,
      Action<DeckSlot> OnEdit)
    {
      this.m_bgDefaultVisual.SetActive(false);
      this.m_editButton.gameObject.SetActive(false);
      this.m_slectedOutline.enabled = false;
      this.m_deckIcon.sprite = sprite;
      this.m_toggle = this.GetComponent<AnimatedToggleButton>();
      this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
      this.m_editButton.onClick.AddListener((UnityAction) (() => OnEdit(this.m_deckSlot)));
      this.m_rectTransform = this.GetComponent<RectTransform>();
      this.m_defaultSizeDelta = this.m_rectTransform.sizeDelta;
      this.m_selectedSizeDelta = this.m_defaultSizeDelta;
      this.m_selectedSizeDelta.x *= 1.11f;
    }

    public void SetDefaultVisual()
    {
      this.m_bgDefaultVisual.SetActive(true);
      this.m_btnIcon.sprite = this.m_btnIconSprite;
    }

    private void BuildUI(bool equipped)
    {
      this.m_presetName.SetText(this.m_deckSlot.isAvailableEmptyDeckSlot ? RuntimeData.FormattedText(50970, (IValueProvider) null) : this.m_deckSlot.Name);
      this.m_presetName.color = new Color(1f, 1f, 1f, this.m_deckSlot.isAvailableEmptyDeckSlot ? 0.5f : 1f);
      this.m_deckIcon.color = new Color(1f, 1f, 1f, this.m_deckSlot.isAvailableEmptyDeckSlot ? 0.2f : 1f);
      this.m_invalidFeedback.gameObject.SetActive(this.m_deckSlot != null && !this.m_deckSlot.isAvailableEmptyDeckSlot && this.m_deckSlot.DeckInfo != null && !this.m_deckSlot.DeckInfo.IsValid());
      this.m_equipedBG.gameObject.SetActive(equipped);
    }

    public void SetEquipped(bool equipped) => this.m_equipedBG.gameObject.SetActive(equipped);

    public void ForceSelect(bool selected = true) => this.m_toggle.isOn = selected;

    private void OnValueChanged(bool selected)
    {
      this.m_rectTransform.DOSizeDelta(selected ? this.m_selectedSizeDelta : this.m_defaultSizeDelta, 0.1f);
      this.m_editButton.gameObject.SetActive(selected);
      this.m_bg.enabled = !selected;
      this.m_slectedOutline.enabled = selected;
      if (!selected)
        return;
      Action<DeckSlot> onSelectRequest = this.OnSelectRequest;
      if (onSelectRequest == null)
        return;
      onSelectRequest(this.m_deckSlot);
    }

    public void Populate(DeckSlot slot, int equippedDeckId)
    {
      this.m_deckSlot = slot;
      this.BuildUI((this.m_deckSlot.Id ?? 0) == equippedDeckId);
    }

    public DeckSlot GetSlot() => this.m_deckSlot;
  }
}
