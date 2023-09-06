// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Components.Tooltip;
using Ankama.Cube.UI.Fight.Windows;
using Ankama.Utilities;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckUI : 
    AbstractUI,
    IDeckDisplayConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator
  {
    [SerializeField]
    private GameObject m_inputBlocker;
    [SerializeField]
    private DeckEditModeUI m_editModeUI;
    [SerializeField]
    private DeckDisplayer m_deck;
    [SerializeField]
    private GenericTooltipWindow m_genericTooltip;
    [SerializeField]
    private FightTooltip m_fightTooltip;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private CanvasGroup m_mainCanvasGroup;
    [Header("Sounds")]
    [SerializeField]
    private UnityEvent m_openCanvas;
    [SerializeField]
    private UnityEvent m_closeCanvas;
    private DeckSlot m_selectedSlot;
    private DeckBuildingEventController m_eventController;
    private bool m_inEdition;
    private bool m_isOpen;

    public DeckBuildingEventController eventController
    {
      get => this.m_eventController;
      set
      {
        this.m_eventController = value;
        this.m_deck.eventController = this.m_eventController;
      }
    }

    public FightTooltip tooltip => this.m_fightTooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    protected override void Awake()
    {
      base.Awake();
      this.m_inputBlocker.SetActive(false);
      this.m_deck.gameObject.SetActive(false);
      this.m_deck.SetConfigurator((ICellRendererConfigurator) this, false);
      this.m_deck.OnEditModeSelectionChanged += new Action<EditModeSelection>(this.OnEditModeSelectionChanged);
      this.m_editModeUI.OnModeSwitchEnd += new Action(this.m_deck.OnEditModeAnimationFinished);
      this.m_editModeUI.SetTooltip(this.m_fightTooltip, this.m_tooltipPosition);
      this.m_mainCanvasGroup.alpha = 0.0f;
      this.m_mainCanvasGroup.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.m_deck.OnEditModeSelectionChanged -= new Action<EditModeSelection>(this.OnEditModeSelectionChanged);
      this.m_editModeUI.OnModeSwitchEnd -= new Action(this.m_deck.OnEditModeAnimationFinished);
    }

    public void SetValue(DeckSlot selectedValue)
    {
      this.m_selectedSlot = selectedValue;
      this.m_deck.SetValue((object) selectedValue);
    }

    public DeckSlot GetSelectedSlot() => this.m_selectedSlot;

    public void RemoveCurrent(int weapon)
    {
      DeckInfo deckInfo = new DeckInfo().FillEmptySlotsCopy();
      deckInfo.Name = RuntimeData.FormattedText(92537, (IValueProvider) null);
      deckInfo.God = (int) PlayerData.instance.god;
      deckInfo.Weapon = weapon;
      DeckSlot deckSlot = new DeckSlot(deckInfo);
      this.m_selectedSlot = deckSlot;
      this.m_deck.SetValue((object) deckSlot);
      this.m_eventController.OnDeckSlotSelectionChange(this.m_selectedSlot);
    }

    public IEnumerator GotoEdit(EditModeSelection selection)
    {
      this.m_inEdition = true;
      Sequence sequence = DOTween.Sequence();
      if (!((UnityEngine.Object) this.m_deck == (UnityEngine.Object) null))
      {
        this.m_deck.gameObject.SetActive(true);
        this.m_mainCanvasGroup.gameObject.SetActive(true);
        sequence.Insert(0.0f, (Tween) this.m_mainCanvasGroup.DOFade(1f, 0.2f));
        sequence.Insert(0.0f, (Tween) this.m_deck.EnterEditMode(selection));
        sequence.Insert(0.0f, this.m_editModeUI.Display(selection, this.m_selectedSlot));
        this.m_openCanvas.Invoke();
        this.m_isOpen = true;
        yield return (object) sequence.WaitForKill();
      }
    }

    public IEnumerator GotoSelectMode()
    {
      DeckUI deckUi = this;
      if (deckUi.m_inEdition)
      {
        deckUi.m_inEdition = false;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, (Tween) deckUi.m_mainCanvasGroup.DOFade(0.0f, 0.2f));
        sequence.Insert(0.0f, (Tween) deckUi.m_deck.LeaveEditMode());
        sequence.Insert(0.0f, deckUi.m_editModeUI.Hide());
        deckUi.m_closeCanvas.Invoke();
        deckUi.m_inputBlocker.SetActive(false);
        sequence.InsertCallback(sequence.Duration(), new TweenCallback(deckUi.OnGotoSelectModeEnd));
        deckUi.m_isOpen = false;
        deckUi.m_fightTooltip.Hide();
        yield return (object) sequence.WaitForKill();
      }
    }

    private void OnGotoSelectModeEnd()
    {
      this.m_deck.gameObject.SetActive(false);
      this.m_mainCanvasGroup.gameObject.SetActive(false);
    }

    private void OnEditModeSelectionChanged(EditModeSelection obj) => this.m_editModeUI.SetEditModeSelection(new EditModeSelection?(obj));

    public DeckSlot OnCloneCanceled()
    {
      if ((UnityEngine.Object) this.m_deck == (UnityEngine.Object) null || this.m_deck.GetPreviousDeck() == null)
        return (DeckSlot) null;
      DeckSlot previousSLot = this.m_deck.GetPreviousDeck().Clone();
      this.StartCoroutine(this.CloneCanceledAnimation(previousSLot));
      return previousSLot;
    }

    private IEnumerator CloneCanceledAnimation(DeckSlot previousSLot)
    {
      this.m_deck.OnCloneCancel();
      this.SetValue(previousSLot);
      yield return (object) null;
      this.m_editModeUI.RefreshList(previousSLot);
    }

    public void OnCloneValidate(DeckSlot newSlot) => this.StartCoroutine(this.CloneAnimation(newSlot));

    private IEnumerator CloneAnimation(DeckSlot newSlot)
    {
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_mainCanvasGroup.DOFade(0.0f, 0.2f));
      sequence.Insert(0.0f, (Tween) this.m_deck.LeaveEditMode());
      sequence.Insert(0.0f, this.m_editModeUI.Hide());
      yield return (object) sequence.Play<Sequence>();
      while (sequence.IsPlaying())
        yield return (object) null;
      yield return (object) new WaitForTime(0.1f);
      this.m_selectedSlot = newSlot;
      this.m_deck.SetValue((object) this.m_selectedSlot);
      sequence = DOTween.Sequence();
      sequence.Append((Tween) this.m_mainCanvasGroup.DOFade(1f, 0.2f));
      sequence.Append((Tween) this.m_deck.EnterEditMode(this.m_editModeUI.GetCurrentMode()));
      sequence.Append(this.m_editModeUI.Display(this.m_editModeUI.GetCurrentMode(), this.m_selectedSlot));
      sequence.Play<Sequence>();
      this.m_deck.OnCloneValidate();
    }

    private void OnDeckSlotSelectionChangedRequest(DeckSlot obj) => this.eventController?.OnDeckSlotSelectionChange(obj);

    private void OnCancel() => this.m_eventController?.OnCancel();

    public bool IsOpen() => this.m_isOpen;
  }
}
