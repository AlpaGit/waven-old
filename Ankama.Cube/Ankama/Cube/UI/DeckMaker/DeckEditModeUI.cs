// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckEditModeUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.Castable;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight.Windows;
using DataEditor;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckEditModeUI : 
    MonoBehaviour,
    ISpellDataCellRendererConfigurator,
    ISpellCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator,
    ICompanionDataCellRendererConfigurator,
    ICompanionCellRendererConfigurator,
    IWeaponDataCellRendererConfigurator,
    IDragNDropValidator
  {
    [SerializeField]
    private RectTransform m_spellsTransform;
    [SerializeField]
    private DynamicList m_spellsList;
    [SerializeField]
    private RectTransform m_companionsTransform;
    [SerializeField]
    private DynamicList m_companionsList;
    [SerializeField]
    private RectTransform m_inPoint;
    [SerializeField]
    private RectTransform m_outPoint;
    [SerializeField]
    private Ease m_moveTweenEase;
    [SerializeField]
    private float m_moveTweenDuration;
    [SerializeField]
    private Ease m_fadeTweenEase;
    [SerializeField]
    private float m_fadeTweenDuration;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private CanvasGroup m_spellListCanvasGroup;
    [SerializeField]
    private CanvasGroup m_companionsCanvasGroup;
    [Header("Filters")]
    [SerializeField]
    private DeckEditToggleParent m_filterParent;
    [Header("Sounds")]
    [SerializeField]
    private UnityEvent m_openCanvasEvent;
    [SerializeField]
    private UnityEvent m_onSwapEditMode;
    [SerializeField]
    private UnityEvent m_onGrabSpell;
    [SerializeField]
    private UnityEvent m_onDropSpell;
    private DeckEditModeUI.SpellDataComparer m_spellComparer;
    private DeckEditModeUI.CompanionDataComparer m_companionDataComparer;
    private CanvasGroup m_currentListCanvasGroup;
    private CanvasGroup m_nextListCanvasGroup;
    private RectTransform m_currentDisplayedTransform;
    private RectTransform m_nextDisplayedTransform;
    private EditModeSelection? m_currentMode;
    private EditModeSelection? m_previousValideMode;
    private List<SpellData> m_allSpells;
    private List<CompanionData> m_allCompanions;
    private WeaponDefinition m_currentWeapon;
    public Action OnModeSwitchEnd;
    private DeckSlot m_slot;
    private HashSet<CaracId> m_caracIdsEnableToggle;
    private HashSet<Element> m_elementsEnableToggle;
    private FightTooltip m_tooltip;
    private TooltipPosition m_tooltipPosition;

    private void Awake()
    {
      this.m_filterParent.Initialise(new Action(this.OnFilterChange), this.m_outPoint.anchoredPosition.y - this.m_inPoint.anchoredPosition.y);
      this.gameObject.SetActive(false);
      this.m_spellsList.SetCellRendererConfigurator((ICellRendererConfigurator) this);
      this.m_companionsList.SetCellRendererConfigurator((ICellRendererConfigurator) this);
      this.m_spellsList.SetDragAndDropValidator((IDragNDropValidator) this);
      this.m_companionsList.SetDragAndDropValidator((IDragNDropValidator) this);
      this.InitPoints(this.m_spellsTransform);
      this.InitPoints(this.m_companionsTransform);
      this.InitLists();
      this.m_canvasGroup.alpha = 0.0f;
      DragNDropListener.instance.OnDragBegin += new Action(this.OnDragBegin);
      DragNDropListener.instance.OnDragEnd += new Action(this.OnDragEnd);
    }

    private void OnDestroy()
    {
      DragNDropListener.instance.OnDragBegin -= new Action(this.OnDragBegin);
      DragNDropListener.instance.OnDragEnd -= new Action(this.OnDragEnd);
    }

    private void InitPoints(RectTransform rectTransform)
    {
      rectTransform.gameObject.SetActive(false);
      rectTransform.anchorMin = this.m_outPoint.anchorMin;
      rectTransform.anchorMax = this.m_outPoint.anchorMax;
      rectTransform.pivot = this.m_outPoint.pivot;
      rectTransform.anchoredPosition = this.m_outPoint.anchoredPosition;
    }

    public void InitLists()
    {
      God god = PlayerData.instance.god;
      List<SpellData> spellDataList = new List<SpellData>();
      int level = 1;
      this.m_spellComparer = new DeckEditModeUI.SpellDataComparer();
      this.m_companionDataComparer = new DeckEditModeUI.CompanionDataComparer();
      foreach (SpellDefinition definition in RuntimeData.spellDefinitions.Values)
      {
        if (definition.god == god && definition.spellType == SpellType.Normal)
          spellDataList.Add(new SpellData(definition, level));
      }
      spellDataList.Sort((IComparer<SpellData>) this.m_spellComparer);
      this.m_allSpells = spellDataList;
      List<CompanionData> companionDataList = new List<CompanionData>();
      foreach (CompanionDefinition definition in RuntimeData.companionDefinitions.Values)
      {
        if (PlayerData.instance.companionInventory.Contains(definition.id))
          companionDataList.Add(new CompanionData(definition, level));
      }
      companionDataList.Sort((IComparer<CompanionData>) this.m_companionDataComparer);
      this.m_allCompanions = companionDataList;
      this.OnFilterChange();
      this.m_spellsList.SetValues<SpellData>((IEnumerable<SpellData>) this.m_allSpells);
      this.m_companionsList.SetValues<CompanionData>((IEnumerable<CompanionData>) this.m_allCompanions);
    }

    public void RefreshList(DeckSlot slot)
    {
      this.SetSlot(slot);
      this.m_filterParent.OnEditModeChange(this.m_currentMode.Value);
      this.OnFilterChange();
      this.UpdateAllChildren(true);
      this.m_spellsList.AccurateReLayout();
      this.m_spellsList.UpdateAllConfigurators();
      this.m_companionsList.UpdateAllConfigurators();
      this.UpdateLists();
    }

    private void UpdateLists()
    {
      this.UpdateCompanionList();
      this.UpdateSpellList();
    }

    private void UpdateCompanionList() => this.m_companionsList.UpdateFilter();

    private void UpdateSpellList() => this.m_spellsList.UpdateFilter();

    public void SetSlot(DeckSlot slot) => this.m_slot = slot;

    public Tween Display(EditModeSelection selection, DeckSlot slot)
    {
      this.m_slot = slot;
      if (this.m_slot != null)
        this.m_slot.OnModification += new Action<DeckSlot>(this.OnSlotModification);
      this.UpdateAllChildren(true);
      this.m_canvasGroup.alpha = 0.0f;
      this.m_spellsTransform.gameObject.SetActive(selection == EditModeSelection.Spell);
      this.m_companionsTransform.gameObject.SetActive(selection == EditModeSelection.Companion);
      this.gameObject.SetActive(true);
      Sequence s = DOTween.Sequence();
      s.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(1f, this.m_fadeTweenDuration).SetEase<Tweener>(this.m_fadeTweenEase));
      Tween modeSelectionTween = this.CreateEditModeSelectionTween(new EditModeSelection?(selection));
      this.OnFilterChange();
      if (modeSelectionTween != null)
        s.Insert(0.0f, modeSelectionTween);
      s.onComplete = new TweenCallback(this.UpdateLists);
      return (Tween) s;
    }

    public Tween Hide()
    {
      if (this.m_slot != null)
        this.m_slot.OnModification -= new Action<DeckSlot>(this.OnSlotModification);
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(0.0f, this.m_fadeTweenDuration).SetEase<Tweener>(this.m_fadeTweenEase));
      Tween modeSelectionTween = this.CreateEditModeSelectionTween(new EditModeSelection?());
      if (modeSelectionTween != null)
        sequence.Insert(0.0f, modeSelectionTween);
      sequence.InsertCallback(sequence.Duration(), new TweenCallback(this.OnHideEnd));
      return (Tween) sequence;
    }

    private void OnHideEnd() => this.gameObject.SetActive(false);

    private void OnSlotModification(DeckSlot obj)
    {
      this.UpdateAllChildren(false);
      this.UpdateCompanionList();
    }

    public void SetEditModeSelection(EditModeSelection? selection)
    {
      Main.monoBehaviour.StartCoroutine(this.SetEditModeSelectionCoroutine(selection));
      this.m_openCanvasEvent.Invoke();
    }

    private IEnumerator SetEditModeSelectionCoroutine(EditModeSelection? selection)
    {
      Tween modeSelectionTween = this.CreateEditModeSelectionTween(selection);
      if (modeSelectionTween != null)
        yield return (object) modeSelectionTween.WaitForKill();
    }

    private Tween CreateEditModeSelectionTween(EditModeSelection? selection)
    {
      EditModeSelection? currentMode = this.m_currentMode;
      EditModeSelection? nullable1 = selection;
      if (currentMode.GetValueOrDefault() == nullable1.GetValueOrDefault() & currentMode.HasValue == nullable1.HasValue)
      {
        Action onModeSwitchEnd = this.OnModeSwitchEnd;
        if (onModeSwitchEnd != null)
          onModeSwitchEnd();
        return (Tween) null;
      }
      this.m_currentMode = selection;
      if (selection.HasValue)
        this.m_previousValideMode = selection;
      EditModeSelection? nullable2 = selection;
      if (nullable2.HasValue)
      {
        switch (nullable2.GetValueOrDefault())
        {
          case EditModeSelection.Spell:
            this.m_nextDisplayedTransform = this.m_spellsTransform;
            this.m_nextListCanvasGroup = this.m_spellListCanvasGroup;
            this.UpdateSpellList();
            break;
          case EditModeSelection.Companion:
            this.m_nextDisplayedTransform = this.m_companionsTransform;
            this.m_nextListCanvasGroup = this.m_companionsCanvasGroup;
            this.UpdateCompanionList();
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof (selection), (object) selection, (string) null);
        }
      }
      else
        this.m_nextDisplayedTransform = (RectTransform) null;
      return this.TweenLists();
    }

    private Tween TweenLists()
    {
      Tween t1 = (Tween) null;
      Tween t2 = (Tween) null;
      if ((bool) (UnityEngine.Object) this.m_currentDisplayedTransform)
      {
        Sequence s = DOTween.Sequence();
        s.Insert(0.0f, (Tween) this.m_currentDisplayedTransform.DOAnchorPos(this.m_outPoint.anchoredPosition, this.m_moveTweenDuration).SetEase<Tweener>(this.m_moveTweenEase));
        s.Insert(0.0f, (Tween) this.m_currentListCanvasGroup.DOFade(0.0f, this.m_fadeTweenDuration)).SetEase<Sequence>(this.m_fadeTweenEase);
        s.Insert(0.0f, this.m_filterParent.TweenOut(this.m_moveTweenDuration, this.m_moveTweenEase));
        t1 = (Tween) s;
      }
      if ((bool) (UnityEngine.Object) this.m_nextDisplayedTransform)
      {
        this.m_onSwapEditMode.Invoke();
        this.m_nextListCanvasGroup.alpha = 0.0f;
        this.m_nextDisplayedTransform.gameObject.SetActive(true);
        Sequence s = DOTween.Sequence();
        s.Insert(0.0f, (Tween) this.m_nextDisplayedTransform.DOAnchorPos(this.m_inPoint.anchoredPosition, this.m_moveTweenDuration).SetEase<Tweener>(this.m_moveTweenEase));
        s.Insert(0.0f, (Tween) this.m_nextListCanvasGroup.DOFade(1f, this.m_fadeTweenDuration)).SetEase<Sequence>(this.m_fadeTweenEase);
        s.Insert(0.0f, this.m_filterParent.TweenIn(this.m_moveTweenDuration, this.m_moveTweenEase));
        t2 = (Tween) s;
      }
      if ((UnityEngine.Object) this.m_currentDisplayedTransform == (UnityEngine.Object) null && (UnityEngine.Object) this.m_nextDisplayedTransform == (UnityEngine.Object) null)
        return (Tween) null;
      t2.OnStart<Tween>(new TweenCallback(this.OnTweenMiddle));
      Sequence sequence = DOTween.Sequence();
      if (t1 != null)
        sequence.Append(t1);
      if (t2 != null)
      {
        if (t1 != null)
          sequence.Insert(this.m_moveTweenDuration / 2f, t2);
        else
          sequence.Append(t2);
      }
      sequence.OnKill<Sequence>(new TweenCallback(this.OnTweenEnd));
      return (Tween) sequence;
    }

    private void OnTweenMiddle()
    {
      if ((UnityEngine.Object) this.m_currentDisplayedTransform != (UnityEngine.Object) null)
        this.m_currentDisplayedTransform.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.m_nextDisplayedTransform != (UnityEngine.Object) null)
        this.m_nextDisplayedTransform.gameObject.SetActive(true);
      if (!this.m_currentMode.HasValue)
        return;
      this.m_filterParent.OnEditModeChange(this.m_currentMode.Value);
    }

    private void OnTweenEnd()
    {
      this.m_currentListCanvasGroup = this.m_nextListCanvasGroup;
      this.m_currentDisplayedTransform = this.m_nextDisplayedTransform;
      this.m_nextDisplayedTransform = (RectTransform) null;
      this.m_nextListCanvasGroup = (CanvasGroup) null;
      Action onModeSwitchEnd = this.OnModeSwitchEnd;
      if (onModeSwitchEnd == null)
        return;
      onModeSwitchEnd();
    }

    private void OnFilterChange()
    {
      HashSet<CaracId> caracIdSet = new HashSet<CaracId>();
      HashSet<Element> elementSet = new HashSet<Element>();
      this.m_caracIdsEnableToggle = new HashSet<CaracId>();
      this.m_elementsEnableToggle = new HashSet<Element>();
      if (this.m_slot == null)
        return;
      foreach (DeckEditToggleFilter editToggleFilter in this.m_filterParent.ToggleFilter)
      {
        if (editToggleFilter.IsEnabled())
        {
          this.m_caracIdsEnableToggle.Add(editToggleFilter.GetElement());
          this.m_elementsEnableToggle.Add(editToggleFilter.GetSpellElement());
        }
        caracIdSet.Add(editToggleFilter.GetElement());
        elementSet.Add(editToggleFilter.GetSpellElement());
      }
      if (!this.m_caracIdsEnableToggle.Any<CaracId>())
      {
        this.m_caracIdsEnableToggle = caracIdSet;
        this.m_elementsEnableToggle = elementSet;
      }
      this.RefreshValue();
    }

    private void RefreshValue()
    {
      int level = PlayerData.instance.GetCurrentWeaponLevel().Value;
      God god = PlayerData.instance.god;
      List<SpellData> values1 = new List<SpellData>();
      foreach (SpellDefinition definition in RuntimeData.spellDefinitions.Values)
      {
        if (definition.god == god && definition.spellType == SpellType.Normal)
        {
          SpellData spellData = new SpellData(definition, level);
          if (this.IsSpellValid((object) spellData))
            values1.Add(spellData);
        }
      }
      values1.Sort((IComparer<SpellData>) this.m_spellComparer);
      this.m_allSpells = values1;
      List<CompanionData> values2 = new List<CompanionData>();
      foreach (CompanionDefinition definition in RuntimeData.companionDefinitions.Values)
      {
        if (PlayerData.instance.companionInventory.Contains(definition.id) && this.IsCompanionValid((object) new CompanionData(definition, level)))
          values2.Add(new CompanionData(definition, level));
      }
      values2.Sort((IComparer<CompanionData>) this.m_companionDataComparer);
      this.m_allCompanions = values2;
      if (!this.m_currentMode.HasValue)
        return;
      this.m_spellsList.SetValues<SpellData>((IEnumerable<SpellData>) values1);
      this.m_companionsList.SetValues<CompanionData>((IEnumerable<CompanionData>) values2);
    }

    private bool IsCompanionValid(object obj)
    {
      if (!(obj is CompanionData data))
        return false;
      IReadOnlyList<Cost> cost = data.definition.cost;
      int num = 0;
      for (int count = ((IReadOnlyCollection<Cost>) cost).Count; num < count; ++num)
      {
        if (cost[num] is ElementPointsCost elementPointsCost && !this.m_caracIdsEnableToggle.Contains(elementPointsCost.element))
          return false;
      }
      return this.ValidateSearchText<CompanionData, CompanionDefinition>(data, data.definition);
    }

    private bool IsSpellValid(object obj)
    {
      if (!(obj is SpellData data))
        return false;
      bool flag1 = false;
      bool flag2 = this.m_elementsEnableToggle.Contains(data.definition.element);
      IReadOnlyList<GaugeValue> modifyOnSpellPlay = data.definition.gaugeToModifyOnSpellPlay;
      int num = 0;
      for (int count = ((IReadOnlyCollection<GaugeValue>) modifyOnSpellPlay).Count; num < count; ++num)
      {
        GaugeValue gaugeValue = modifyOnSpellPlay[num];
        flag1 = ((flag1 ? 1 : 0) | (gaugeValue == null ? 0 : (this.m_caracIdsEnableToggle.Contains(gaugeValue.element) ? 1 : 0))) != 0;
      }
      return flag2 | flag1 && this.ValidateSearchText<SpellData, SpellDefinition>(data, data.definition);
    }

    private bool ValidateSearchText<T, D>(T data, D definition)
      where T : CastableWithLevelData<D>
      where D : EditableData, IDefinitionWithTooltip
    {
      string textFilter = this.m_filterParent.GetTextFilter();
      string s;
      return textFilter.Length == 0 || !RuntimeData.TryGetText(definition.i18nNameId, out s) || s.ContainsIgnoreDiacritics(textFilter, StringComparison.OrdinalIgnoreCase) || RuntimeData.FormattedText(definition.i18nDescriptionId, (IValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) definition, data.level)).RemoveTags().ContainsIgnoreDiacritics(textFilter, StringComparison.OrdinalIgnoreCase);
    }

    private void UpdateAllChildren(bool instant)
    {
      this.m_spellsList.UpdateAllConfigurators(instant);
      this.m_companionsList.UpdateAllConfigurators(instant);
    }

    public void SetTooltip(FightTooltip tooltip, TooltipPosition tooltipPosition)
    {
      this.m_tooltip = tooltip;
      this.m_tooltipPosition = tooltipPosition;
      this.UpdateAllChildren(true);
    }

    public FightTooltip tooltip => this.m_tooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    public bool IsWeaponDataAvailable(WeaponData data) => true;

    public bool IsCompanionDataAvailable(CompanionData data)
    {
      if (this.m_slot == null || data == null || this.m_slot.Companions.Contains(data.definition.id))
        return false;
      return !ItemDragNDropListener.instance.dragging || (!(ItemDragNDropListener.instance.DraggedValue is CompanionData draggedValue) ? 0 : (draggedValue.definition.id == data.definition.id ? 1 : 0)) == 0;
    }

    public bool IsSpellDataAvailable(SpellData data)
    {
      if (this.m_slot == null || data == null || this.m_slot.Spells.Contains(data.definition.id))
        return false;
      return !ItemDragNDropListener.instance.dragging || (!(ItemDragNDropListener.instance.DraggedValue is SpellData draggedValue) ? 0 : (draggedValue.definition.id == data.definition.id ? 1 : 0)) == 0;
    }

    private void OnDragBegin()
    {
      this.m_onGrabSpell.Invoke();
      EditModeSelection? currentMode1 = this.m_currentMode;
      EditModeSelection editModeSelection1 = EditModeSelection.Spell;
      if (currentMode1.GetValueOrDefault() == editModeSelection1 & currentMode1.HasValue)
        this.m_spellsList.UpdateAllConfigurators(true);
      EditModeSelection? currentMode2 = this.m_currentMode;
      EditModeSelection editModeSelection2 = EditModeSelection.Companion;
      if (!(currentMode2.GetValueOrDefault() == editModeSelection2 & currentMode2.HasValue))
        return;
      this.m_companionsList.UpdateAllConfigurators(true);
    }

    private void OnDragEnd()
    {
      this.m_onDropSpell.Invoke();
      EditModeSelection? currentMode1 = this.m_currentMode;
      EditModeSelection editModeSelection1 = EditModeSelection.Spell;
      if (currentMode1.GetValueOrDefault() == editModeSelection1 & currentMode1.HasValue)
        this.m_spellsList.UpdateAllConfigurators();
      EditModeSelection? currentMode2 = this.m_currentMode;
      EditModeSelection editModeSelection2 = EditModeSelection.Companion;
      if (!(currentMode2.GetValueOrDefault() == editModeSelection2 & currentMode2.HasValue))
        return;
      this.m_companionsList.UpdateAllConfigurators();
    }

    public bool IsValidDrag(object value)
    {
      switch (value)
      {
        case CompanionData data1:
          return this.IsCompanionDataAvailable(data1);
        case SpellData data2:
          return this.IsSpellDataAvailable(data2);
        default:
          return true;
      }
    }

    public bool IsValidDrop(object value) => false;

    public EditModeSelection GetCurrentMode() => this.m_previousValideMode.Value;

    private class WeaponDataComparer : Comparer<WeaponData>
    {
      public override int Compare(WeaponData x, WeaponData y)
      {
        if (x == y)
          return 0;
        if (x == null)
          return 1;
        return y == null ? -1 : x.definition.id - y.definition.id;
      }
    }

    private class CompanionDataComparer : Comparer<CompanionData>
    {
      public override int Compare(CompanionData x, CompanionData y)
      {
        if (x == y)
          return 0;
        if (x == null)
          return 1;
        if (y == null)
          return -1;
        CaracId caracId1 = CaracId.Armor;
        int maxValue1 = int.MaxValue;
        CaracId caracId2 = CaracId.Armor;
        int maxValue2 = int.MaxValue;
        foreach (Cost cost in (IEnumerable<Cost>) x.definition.cost)
        {
          if (cost is ElementPointsCost elementPointsCost)
          {
            caracId1 = elementPointsCost.element;
            elementPointsCost.value.GetValue((DynamicValueContext) null, out maxValue1);
            break;
          }
        }
        foreach (Cost cost in (IEnumerable<Cost>) y.definition.cost)
        {
          if (cost is ElementPointsCost elementPointsCost)
          {
            caracId2 = elementPointsCost.element;
            elementPointsCost.value.GetValue((DynamicValueContext) null, out maxValue2);
            break;
          }
        }
        if (CaracId.Armor != caracId1 && caracId2 != CaracId.Armor)
        {
          if (caracId1 != caracId2)
            return caracId1 - caracId2;
          if (maxValue1 != maxValue2)
            return maxValue1 - maxValue2;
        }
        return x.definition.id - y.definition.id;
      }
    }

    private class SpellDataComparer : Comparer<SpellData>
    {
      public override int Compare(SpellData x, SpellData y)
      {
        if (x == y)
          return 0;
        if (x == null)
          return 1;
        if (y == null)
          return -1;
        int num = x.definition.element.CompareTo((object) y.definition.element);
        if (num != 0)
          return num;
        int maxValue1 = int.MaxValue;
        int maxValue2 = int.MaxValue;
        foreach (Cost cost in (IEnumerable<Cost>) x.definition.costs)
        {
          if (cost is ActionPointsCost actionPointsCost)
            actionPointsCost.value.GetValue((DynamicValueContext) null, out maxValue1);
        }
        foreach (Cost cost in (IEnumerable<Cost>) y.definition.costs)
        {
          if (cost is ActionPointsCost actionPointsCost)
            actionPointsCost.value.GetValue((DynamicValueContext) null, out maxValue2);
        }
        return maxValue1 != maxValue2 ? maxValue1 - maxValue2 : x.definition.id - y.definition.id;
      }
    }
  }
}
