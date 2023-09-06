// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckSpellList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckSpellList : 
    MonoBehaviour,
    IPointerDownHandler,
    IEventSystemHandler,
    IPointerEnterHandler,
    IPointerExitHandler
  {
    [SerializeField]
    private List<Item> m_spellItems;
    [SerializeField]
    private Animator m_listSelectionAnimator;
    [SerializeField]
    private Animator m_editModeAnimator;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private CanvasGroup m_blockerCanvasGroup;
    [Header("Tween")]
    [SerializeField]
    private float m_minScale = 0.95f;
    [SerializeField]
    private float m_tweenDuration = 0.15f;
    private readonly List<SpellData> m_items = new List<SpellData>();
    private bool m_selected;
    private bool m_editable;
    private bool m_editMode;
    private bool m_interectable;
    private Tween m_currentTween;
    private DeckBuildingEventController m_eventController;
    private ICellRendererConfigurator m_configurator;

    public event Action<SpellData, SpellData, int> OnSpellChange;

    public event Action OnSelected;

    private void Awake()
    {
      this.m_canvasGroup.alpha = 0.0f;
      int index1 = 0;
      for (int count = this.m_spellItems.Count; index1 < count; ++index1)
      {
        int index = index1;
        Item spellItem = this.m_spellItems[index1];
        spellItem.SetCellRendererConfigurator(this.m_configurator);
        spellItem.OnValueChange += (Action<object, object>) ((previous, value) => this.OnValueChanged(previous, value, index));
      }
    }

    public void SetValues(IList<int> spellIds, int level)
    {
      this.m_items.Clear();
      int index1 = 0;
      for (int count = spellIds.Count; index1 < count; ++index1)
      {
        int spellId = spellIds[index1];
        SpellDefinition definition;
        if (RuntimeData.spellDefinitions.TryGetValue(spellId, out definition))
        {
          SpellData spellData = new SpellData(definition, level);
          this.m_items.Add(spellData);
          this.m_spellItems[index1].SetValue<SpellData>(spellData);
        }
        else
        {
          this.m_items.Add((SpellData) null);
          this.m_spellItems[index1].SetValue<SpellData>((SpellData) null);
        }
      }
      int count1 = this.m_items.Count;
      for (int index2 = 8; count1 < index2; ++count1)
      {
        this.m_items.Add((SpellData) null);
        this.m_spellItems[count1].SetValue<SpellData>((SpellData) null);
      }
    }

    private void OnValueChanged(object previousValue, object value, int index)
    {
      SpellData spellData = (SpellData) value;
      this.m_items[index] = spellData;
      Action<SpellData, SpellData, int> onSpellChange = this.OnSpellChange;
      if (onSpellChange != null)
        onSpellChange((SpellData) previousValue, spellData, index);
      DeckEditItemPointerListener componentInChildren = this.m_spellItems[index].gameObject.GetComponentInChildren<DeckEditItemPointerListener>();
      if (!((UnityEngine.Object) componentInChildren != (UnityEngine.Object) null))
        return;
      componentInChildren.RemoveComponent();
    }

    public void SetEditMode(bool editMode, bool editable)
    {
      this.m_editMode = editMode;
      this.m_editable = editable;
      this.Select(editMode);
      if (!editMode)
        this.Fade(true);
      else
        this.Fade(this.m_selected);
      this.m_editModeAnimator.Play(editMode ? "EditMode" : "SelectMode");
    }

    public void Interectable(bool i) => this.m_interectable = i;

    private DG.Tweening.Sequence Sequence()
    {
      Tween currentTween = this.m_currentTween;
      if (currentTween != null)
        currentTween.Kill(true);
      DG.Tweening.Sequence sequence = DOTween.Sequence().OnKill<DG.Tweening.Sequence>(new TweenCallback(this.OnTweenKill));
      this.m_currentTween = (Tween) sequence;
      return sequence;
    }

    private void OnTweenKill() => this.m_currentTween = (Tween) null;

    public void OnPointerDown(PointerEventData eventData)
    {
      if (!this.m_interectable)
        return;
      if (this.m_editMode)
      {
        this.Select(true);
        Action onSelected = this.OnSelected;
        if (onSelected == null)
          return;
        onSelected();
      }
      else
        this.m_eventController?.OnEdit(EditModeSelection.Spell);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (ItemDragNDropListener.instance.dragging || !this.m_interectable || this.m_selected)
        return;
      this.SetEnableVisual(this.m_selected);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if (ItemDragNDropListener.instance.dragging || !this.m_interectable || this.m_selected)
        return;
      this.SeDisableVisual(this.m_selected);
    }

    private void SeDisableVisual(bool selected)
    {
      if (!this.m_editMode || selected)
        return;
      DG.Tweening.Sequence s = this.Sequence();
      if (!this.m_editMode)
        return;
      s.Insert(0.0f, this.Fade(false));
    }

    private void SetEnableVisual(bool selected)
    {
      if (!this.m_editMode || selected)
        return;
      DG.Tweening.Sequence s = this.Sequence();
      s.Insert(0.0f, (Tween) this.m_canvasGroup.transform.DOScale(1f, this.m_tweenDuration));
      s.Insert(0.0f, this.Fade(true));
    }

    public void Select(bool select, bool fullAnimation = true)
    {
      if (this.m_selected == select)
        return;
      this.m_selected = select;
      int index = 0;
      for (int count = this.m_spellItems.Count; index < count; ++index)
        this.m_spellItems[index].enableDragAndDrop = this.m_editMode;
      this.PlaySelectAnimation(select, fullAnimation);
    }

    private void PlaySelectAnimation(bool select, bool fullAnimation = true)
    {
      if (select)
      {
        this.m_listSelectionAnimator.SetBool("Selected", true);
        if (!fullAnimation)
          return;
        DG.Tweening.Sequence s = this.Sequence();
        s.Insert(0.0f, (Tween) this.m_canvasGroup.transform.DOScale(this.m_minScale, 0.05f).SetEase<Tweener>(Ease.OutSine));
        s.Append((Tween) this.m_canvasGroup.transform.DOScale(1f, 0.1f).SetEase<Tweener>(Ease.OutSine));
        if (!this.m_editMode)
          return;
        s.Insert(0.0f, this.Fade(true));
      }
      else
      {
        this.m_listSelectionAnimator.SetBool("Selected", false);
        if (!fullAnimation)
          return;
        DG.Tweening.Sequence s = this.Sequence();
        if (!this.m_editMode)
          return;
        s.Insert(0.0f, this.Fade(false));
      }
    }

    private Tween Fade(bool visible) => (Tween) this.m_blockerCanvasGroup.DOFade(visible ? 0.0f : 1f, 0.0f);

    public DeckBuildingEventController eventController
    {
      set => this.m_eventController = value;
    }

    public void SetConfigurator(ICellRendererConfigurator configurator)
    {
      this.m_configurator = configurator;
      int index = 0;
      for (int count = this.m_spellItems.Count; index < count; ++index)
        this.m_spellItems[index].SetCellRendererConfigurator(configurator);
    }

    public void UpdateConfigurator(bool instant)
    {
      int index = 0;
      for (int count = this.m_spellItems.Count; index < count; ++index)
        this.m_spellItems[index].UpdateConfigurator(instant);
    }
  }
}
