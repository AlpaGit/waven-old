// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckDisplayer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight.Windows;
using DG.Tweening;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckDisplayer : 
    CellRenderer<DeckSlot, IDeckDisplayConfigurator>,
    ListHighlightable,
    ISpellDataCellRendererConfigurator,
    ISpellCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator,
    ICompanionDataCellRendererConfigurator,
    ICompanionCellRendererConfigurator,
    IWeaponDataCellRendererConfigurator
  {
    [SerializeField]
    private InputTextField m_nameTextField;
    [SerializeField]
    private DeckSpellList m_spellList;
    [SerializeField]
    private DeckCompanionList m_companionList;
    [SerializeField]
    private Image m_invalidDeckImage;
    [SerializeField]
    private CanvasGroup m_contentCanvasGroup;
    [SerializeField]
    private CanvasGroup m_saveCancelCanvasGroup;
    [SerializeField]
    private Animator m_editModeAnimator;
    [SerializeField]
    private float m_backgroundTweenDuration;
    [SerializeField]
    private Ease m_backgroundTweenEase;
    [SerializeField]
    private Button m_saveButton;
    [SerializeField]
    private Button m_cloneButton;
    [SerializeField]
    private Button m_deleteButton;
    private float m_highlightFactor;
    private bool m_contentVisible;
    private float m_visibilityFactor = 1f;
    private bool m_editMode;
    private DeckSlot m_previousValue;
    private FightTooltip m_fightTooltip;
    private TooltipPosition m_tooltipPosition;
    private DeckSlot m_uneditedValue;
    private bool m_settingValue;
    private DeckBuildingEventController m_eventController;

    public event Action<EditModeSelection> OnEditModeSelectionChanged;

    public float GetHighlightFactor() => this.m_highlightFactor;

    public void SetHighlightFactor(float factor)
    {
      if (Mathf.Approximately(this.m_highlightFactor, factor))
        return;
      this.m_highlightFactor = factor;
      bool flag = (double) factor >= 0.0001;
      if (flag != this.m_contentVisible)
      {
        this.m_contentVisible = flag;
        this.m_contentCanvasGroup.gameObject.SetActive(flag);
      }
      if (!flag)
        return;
      this.m_contentCanvasGroup.alpha = factor;
    }

    public float GetVisibilityFactor() => this.m_visibilityFactor;

    public void SetVisibilityFactor(float factor)
    {
      if (Mathf.Approximately(this.m_visibilityFactor, factor))
        return;
      this.m_visibilityFactor = factor;
      bool flag = Mathf.Approximately(factor, 0.0f);
      if (this.gameObject.activeSelf & flag)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        if (this.gameObject.activeSelf || flag)
          return;
        this.gameObject.SetActive(true);
      }
    }

    protected void Awake()
    {
      this.m_spellList.SetConfigurator((ICellRendererConfigurator) this);
      this.m_companionList.SetConfigurator((ICellRendererConfigurator) this);
      this.m_spellList.OnSpellChange += new Action<SpellData, SpellData, int>(this.OnSpellChange);
      this.m_spellList.OnSelected += new Action(this.OnSpellSelected);
      this.m_companionList.OnCompanionChange += new Action<Ankama.Cube.Data.CompanionData, Ankama.Cube.Data.CompanionData, int>(this.OnCompanionChange);
      this.m_companionList.OnSelected += new Action(this.OnCompanionSelected);
      this.m_nameTextField.onValueChanged.AddListener(new UnityAction<string>(this.OnNameChanged));
      this.m_nameTextField.characterLimit = 30;
      this.SetHighlightFactor(1f);
      this.m_saveButton.onClick.AddListener(new UnityAction(this.OnSave));
      this.m_deleteButton.onClick.AddListener(new UnityAction(this.OnDelete));
      this.m_cloneButton.onClick.AddListener(new UnityAction(this.OnCloneDeck));
      this.m_saveCancelCanvasGroup.gameObject.SetActive(this.m_editMode);
      this.UpdateEditModeUI();
    }

    private void OnSpellSelected()
    {
      this.SetEditModeSelection(EditModeSelection.Spell);
      Action<EditModeSelection> selectionChanged = this.OnEditModeSelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged(EditModeSelection.Spell);
    }

    private void OnCompanionSelected()
    {
      this.SetEditModeSelection(EditModeSelection.Companion);
      Action<EditModeSelection> selectionChanged = this.OnEditModeSelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged(EditModeSelection.Companion);
    }

    protected override void SetValue(DeckSlot slot)
    {
      if (this.m_previousValue != null)
        this.m_previousValue.OnModification -= new Action<DeckSlot>(this.OnDeckSlotModification);
      if (slot != null)
        slot.OnModification += new Action<DeckSlot>(this.OnDeckSlotModification);
      this.m_previousValue = slot;
      this.SetValueInternal(slot);
    }

    public DeckSlot GetPreviousDeck() => this.m_uneditedValue;

    protected override void Clear() => this.SetValue((DeckSlot) null);

    public override void OnConfiguratorUpdate(bool instant)
    {
      this.eventController = this.m_configurator?.eventController;
      if (this.m_configurator == null)
        return;
      this.SetTooltip(this.m_configurator.tooltip, this.m_configurator.tooltipPosition);
    }

    private void SetValueInternal(DeckSlot slot)
    {
      if (slot == null)
        this.HideAll();
      else if (!slot.HasDeckInfo)
      {
        this.HideAll();
      }
      else
      {
        this.UpdateInvalidDeck();
        DeckInfo deckInfo = slot.DeckInfo;
        bool editMode = this.m_editMode;
        if ((UnityEngine.Object) this.m_nameTextField == (UnityEngine.Object) null)
          return;
        this.m_nameTextField.gameObject.SetActive(editMode);
        this.m_nameTextField.SetText(deckInfo.Name.Substring(0, Math.Min(deckInfo.Name.Length, 30)));
        RepeatedField<int> companions = deckInfo.Companions;
        RepeatedField<int> spells = deckInfo.Spells;
        int level = deckInfo.GetLevel((ILevelProvider) PlayerData.instance.weaponInventory);
        this.m_spellList.SetValues((IList<int>) spells, level);
        this.m_companionList.SetValues((IList<int>) companions, level);
        if (this.m_value.Preconstructed)
        {
          ItemDragNDropListener.instance.OnDragEndSuccessful += new Action(this.OnRequestValidation);
          ItemDragNDropListener.instance.OnDragEnd += new Action(this.OnDragFail);
        }
        this.m_deleteButton.interactable = !slot.isAvailableEmptyDeckSlot && !slot.Preconstructed;
        this.m_cloneButton.interactable = !slot.isAvailableEmptyDeckSlot && DeckUtility.GetRemainingSlotsForWeapon(deckInfo.Weapon) > 0;
        if (!this.m_editMode)
          return;
        this.m_uneditedValue = this.m_value?.Clone();
        this.m_saveButton.interactable = false;
      }
    }

    public override void SetValue(object v) => base.SetValue(v);

    public override CellRenderer Clone() => base.Clone();

    private void OnDeckSlotModification(DeckSlot obj)
    {
      if (this.m_settingValue)
        return;
      this.SetValueInternal(obj);
    }

    private void HideAll()
    {
      this.m_nameTextField.gameObject.SetActive(false);
      this.m_spellList.gameObject.SetActive(false);
      this.m_companionList.gameObject.SetActive(false);
    }

    private void UpdateEditModeUI()
    {
      this.m_nameTextField.gameObject.SetActive(true);
      this.m_nameTextField.interactable = this.m_editMode && !this.m_value.Preconstructed;
      if (!this.m_contentCanvasGroup.gameObject.activeSelf)
        return;
      this.m_spellList.SetEditMode(this.m_editMode, !this.m_value.Preconstructed);
      this.m_companionList.SetEditMode(this.m_editMode, !this.m_value.Preconstructed);
      this.m_editModeAnimator.Play(this.m_editMode ? "EditMode" : "SelectMode");
    }

    public FightTooltip tooltip => this.m_fightTooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    public void SetTooltip(FightTooltip t, TooltipPosition tooltipPosition)
    {
      this.m_fightTooltip = t;
      this.m_tooltipPosition = tooltipPosition;
      this.UpdateAllChildren(true);
    }

    private void UpdateAllChildren(bool instant)
    {
      this.m_spellList.UpdateConfigurator(instant);
      this.m_companionList.UpdateConfigurator(instant);
    }

    public Sequence EnterEditMode(EditModeSelection selection)
    {
      if (this.m_editMode)
        return (Sequence) null;
      this.m_editMode = true;
      this.m_uneditedValue = this.m_value?.Clone();
      this.m_saveButton.interactable = false;
      this.m_saveCancelCanvasGroup.gameObject.SetActive(true);
      this.m_saveCancelCanvasGroup.alpha = 0.0f;
      Sequence s = DOTween.Sequence();
      s.Insert(0.0f, (Tween) this.m_saveCancelCanvasGroup.DOFade(1f, this.m_backgroundTweenDuration).SetEase<Tweener>(this.m_backgroundTweenEase).OnKill<Tweener>(new TweenCallback(this.CheckSaveButtonGroupVisibility)));
      this.UpdateEditModeUI();
      this.SetEditModeSelection(selection);
      return s;
    }

    public Sequence LeaveEditMode()
    {
      if (!this.m_editMode)
        return DOTween.Sequence();
      this.m_editMode = false;
      this.m_uneditedValue = (DeckSlot) null;
      ItemDragNDropListener.instance.OnDragEndSuccessful -= new Action(this.OnRequestValidation);
      ItemDragNDropListener.instance.OnDragEnd -= new Action(this.OnDragFail);
      this.m_saveCancelCanvasGroup.gameObject.SetActive(true);
      this.m_saveCancelCanvasGroup.alpha = 1f;
      this.UpdateEditModeUI();
      Sequence s = DOTween.Sequence();
      s.Insert(0.0f, (Tween) this.m_saveCancelCanvasGroup.DOFade(0.0f, this.m_backgroundTweenDuration).OnKill<Tweener>(new TweenCallback(this.CheckSaveButtonGroupVisibility)));
      return s;
    }

    private void CheckSaveButtonGroupVisibility()
    {
      if (this.m_editMode)
      {
        this.m_saveCancelCanvasGroup.gameObject.SetActive(true);
        this.m_saveCancelCanvasGroup.alpha = 1f;
      }
      else
      {
        this.m_saveCancelCanvasGroup.gameObject.SetActive(false);
        this.m_saveCancelCanvasGroup.alpha = 0.0f;
      }
    }

    public void SetEditModeSelection(EditModeSelection mode)
    {
      this.m_spellList.Interectable(false);
      this.m_companionList.Interectable(false);
      this.m_spellList.Select(mode == EditModeSelection.Spell);
      this.m_companionList.Select(mode == EditModeSelection.Companion);
    }

    public void OnEditModeAnimationFinished()
    {
      this.m_spellList.Interectable(true);
      this.m_companionList.Interectable(true);
    }

    private void OnNameChanged(string deckName)
    {
      this.m_settingValue = true;
      this.m_value?.SetName(deckName);
      this.m_saveButton.interactable = !DeckUtility.DecksAreEqual(this.m_value?.DeckInfo, this.m_uneditedValue?.DeckInfo);
      this.m_settingValue = false;
    }

    private void OnCompanionChange(
      Ankama.Cube.Data.CompanionData previousCompanionData,
      Ankama.Cube.Data.CompanionData companionData,
      int index)
    {
      this.m_settingValue = true;
      this.m_value?.SetCompanionAt(companionData != null ? companionData.definition.id : -1, index);
      this.m_saveButton.interactable = !DeckUtility.DecksAreEqual(this.m_value?.DeckInfo, this.m_uneditedValue?.DeckInfo);
      this.UpdateInvalidDeck();
      this.m_settingValue = false;
    }

    private void OnRequestValidation()
    {
      if (!this.m_value.Preconstructed)
        return;
      this.m_eventController.OnClone(92537, 84166);
    }

    private void OnDragFail()
    {
      if (!this.m_value.Preconstructed || this.m_value.DeckInfo.IsValid())
        return;
      this.OnRequestValidation();
    }

    private void OnSpellChange(SpellData previousSpellData, SpellData spellData, int index)
    {
      this.m_settingValue = true;
      this.m_value?.SetSpellAt(spellData != null ? spellData.definition.id : -1, index);
      this.m_saveButton.interactable = !DeckUtility.DecksAreEqual(this.m_value?.DeckInfo, this.m_uneditedValue?.DeckInfo);
      this.UpdateInvalidDeck();
      this.m_settingValue = false;
    }

    private void UpdateInvalidDeck()
    {
      DeckSlot deckSlot = this.m_value;
      if (deckSlot == null || deckSlot.isAvailableEmptyDeckSlot)
      {
        this.m_invalidDeckImage.gameObject.SetActive(false);
      }
      else
      {
        if (!((UnityEngine.Object) this.m_invalidDeckImage != (UnityEngine.Object) null))
          return;
        this.m_invalidDeckImage.gameObject.SetActive(!deckSlot.DeckInfo.IsValid());
      }
    }

    public DeckBuildingEventController eventController
    {
      set
      {
        this.m_eventController = value;
        this.m_spellList.eventController = value;
        this.m_companionList.eventController = value;
      }
    }

    private void OnSave() => this.m_eventController?.OnSave();

    private void OnCloneDeck() => this.m_eventController?.OnClone(92537, 0);

    private void OnDelete() => this.m_eventController?.OnDelete();

    public bool IsWeaponDataAvailable(WeaponData data) => true;

    public bool IsCompanionDataAvailable(Ankama.Cube.Data.CompanionData data) => true;

    public bool IsSpellDataAvailable(SpellData data) => true;

    public void OnCloneValidate()
    {
      this.ClearEvent();
      this.m_nameTextField.selectable.Select();
    }

    public void OnCloneCancel() => this.ClearEvent();

    private void ClearEvent()
    {
      ItemDragNDropListener.instance.OnDragEndSuccessful -= new Action(this.OnRequestValidation);
      ItemDragNDropListener.instance.OnDragEnd -= new Action(this.OnDragFail);
    }
  }
}
