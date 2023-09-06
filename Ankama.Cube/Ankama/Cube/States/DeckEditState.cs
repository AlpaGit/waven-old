// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.DeckEditState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Network;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.UI.DeckMaker;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.States
{
  public class DeckEditState : LoadSceneStateContext
  {
    private List<int> m_companions;
    private DeckMakerFrame m_frame;
    private Gender m_gender;
    private Family m_god;
    private bool m_isBeingSave;
    private string m_name;
    private DeckSlot m_selectedSlot;
    private DeckSlot m_previousSlot;
    private List<int> m_spells;
    private DeckUI m_ui;
    private int m_weapon;
    private WeaponAndDeckModifications m_modifications;
    private bool m_wasValid;
    private bool m_inAnimation;
    private bool m_safeExit;
    private bool ExitAfterSave;
    private bool m_onPopup;

    public event Action OnCloseComplete;

    public void SetDeckSlot(DeckSlot slot, WeaponAndDeckModifications modifications)
    {
      this.m_inAnimation = true;
      this.m_wasValid = slot.DeckInfo.IsValid();
      this.m_previousSlot = slot.Clone();
      this.m_selectedSlot = slot;
      this.m_weapon = slot.Weapon ?? 0;
      this.m_modifications = modifications;
    }

    public void OpenUIAnimation() => Main.monoBehaviour.StartCoroutine(this.GotoEdit());

    private IEnumerator GotoEdit()
    {
      if (!((UnityEngine.Object) this.m_ui == (UnityEngine.Object) null))
      {
        this.m_ui.interactable = true;
        this.m_ui.SetValue(this.m_selectedSlot);
        this.m_inAnimation = true;
        yield return (object) this.m_ui.GotoEdit(EditModeSelection.Spell);
        this.m_inAnimation = false;
      }
    }

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      DeckEditState uiloader = this;
      float start;
      LoadSceneStateContext.UILoader<DeckUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        Log.Info(string.Format("Scene load duration : {0}", (object) (Time.realtimeSinceStartup - start)), 89, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\PlayerUI\\DeckEditState.cs");
        DeckBuildingEventController buildingEventController = new DeckBuildingEventController();
        buildingEventController.OnCloseRequest += new Action(uiloader.OnExit);
        buildingEventController.OnSaveRequest += new Action(uiloader.OnSave);
        buildingEventController.OnCancelRequest += new Action(uiloader.OnCancel);
        buildingEventController.OnDeleteRequest += new Action(uiloader.OnRemoveRequest);
        buildingEventController.OnDeckSlotSelectionChanged += new Action<DeckSlot>(uiloader.OnSelectionChanged);
        buildingEventController.OnCloneRequest += new Action<int, int>(uiloader.OnCloneRequest);
        uiloader.m_ui.eventController = buildingEventController;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      RuntimeData.currentKeywordContext = KeywordContext.DeckBuilding;
      start = Time.realtimeSinceStartup;
      loader = new LoadSceneStateContext.UILoader<DeckUI>((LoadSceneStateContext) uiloader, "DeckUI", "core/scenes/ui/deck", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable() => this.m_frame = new DeckMakerFrame()
    {
      onRemoveConfigResult = new Action<RemoveDeckResultEvent>(this.OnRemoveDeckResult),
      onSaveConfigResult = new Action<SaveDeckResultEvent>(this.OnSaveResult)
    };

    protected override void Disable() => this.m_frame.Dispose();

    protected override IEnumerator Unload()
    {
      yield return (object) base.Unload();
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      if ((bool) (UnityEngine.Object) this.m_ui)
        this.m_ui.interactable = false;
      yield return (object) this.HideAllEnumerator();
    }

    private IEnumerator HideAllEnumerator()
    {
      while (this.m_inAnimation)
        yield return (object) null;
      this.m_inAnimation = true;
      bool wasOpen = false;
      if ((UnityEngine.Object) this.m_ui != (UnityEngine.Object) null)
        wasOpen = this.m_ui.IsOpen();
      if (wasOpen && !this.m_safeExit && !DeckUtility.DecksAreEqual(this.m_previousSlot?.DeckInfo, this.m_selectedSlot?.DeckInfo))
      {
        this.OnSaveConfirm();
        while (this.m_isBeingSave)
          yield return (object) null;
      }
      if ((UnityEngine.Object) this.m_ui != (UnityEngine.Object) null)
        yield return (object) this.m_ui.GotoSelectMode();
      this.m_safeExit = false;
      if (wasOpen)
      {
        Action onCloseComplete = this.OnCloseComplete;
        if (onCloseComplete != null)
          onCloseComplete();
      }
      this.m_inAnimation = false;
      RuntimeData.currentKeywordContext = KeywordContext.FightSolo;
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1 || inputState.state != InputState.State.Activated || (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui) || !this.m_ui.IsOpen()) && !this.m_inAnimation)
        return base.UseInput(inputState);
      this.OnSave();
      return true;
    }

    private void OnSelectionChanged(DeckSlot obj) => this.m_selectedSlot = obj;

    private void OnSave()
    {
      if (this.m_inAnimation || this.m_onPopup)
        return;
      this.m_isBeingSave = false;
      if ((this.m_selectedSlot.DeckInfo.IsValid() || !this.m_wasValid ? 0 : (!this.m_selectedSlot.Preconstructed ? 1 : 0)) != 0)
      {
        ButtonData[] buttonDataArray = new ButtonData[2]
        {
          new ButtonData((TextData) 75192, new Action(this.OnSaveConfirm)),
          new ButtonData((TextData) 38763, new Action(this.ClosePopup))
        };
        StateLayer stateLayer;
        if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
          return;
        PopupInfoManager.ClearAllMessages();
        PopupInfoManager.Show(stateLayer.GetChainEnd(), new PopupInfo()
        {
          title = (RawTextData) 56031,
          message = (RawTextData) 57158,
          buttons = buttonDataArray,
          selectedButton = 1,
          style = PopupStyle.Error
        });
      }
      else if (!DeckUtility.DecksAreEqual(this.m_previousSlot?.DeckInfo, this.m_selectedSlot?.DeckInfo))
        this.OnSaveConfirm();
      else
        this.OnExit();
    }

    private void OnSaveConfirm()
    {
      this.ClosePopup();
      this.m_safeExit = true;
      this.ExitAfterSave = true;
      this.m_isBeingSave = true;
      DeckInfo deckInfo = this.m_selectedSlot.DeckInfo.TrimCopy();
      string name = string.IsNullOrWhiteSpace(deckInfo.Name) ? RuntimeData.FormattedText(92537, (IValueProvider) null) : deckInfo.Name;
      this.m_frame.SendSaveSquadRequest(deckInfo.Id, name, (Family) deckInfo.God, deckInfo.Weapon, (IReadOnlyList<int>) deckInfo.Companions, (IReadOnlyList<int>) deckInfo.Spells);
      this.m_ui.interactable = false;
    }

    private void OnSaveResult(SaveDeckResultEvent result)
    {
      this.m_isBeingSave = false;
      this.m_ui.interactable = true;
      if (result.Result == CmdResult.Success)
        this.m_selectedSlot.SetId(new int?(result.DeckId));
      if (this.m_selectedSlot.Id.HasValue && this.m_selectedSlot.HasDeckInfo && this.m_selectedSlot.DeckInfo.IsValid())
      {
        this.m_modifications.SetSelectedDeckForWeapon(this.m_weapon, this.m_selectedSlot.Id.Value);
        this.m_modifications.SetSelectedWeapon(this.m_weapon);
      }
      if (!this.ExitAfterSave)
        return;
      this.OnExit();
    }

    private void OnCancel()
    {
      if (this.m_inAnimation || this.m_onPopup)
        return;
      if ((this.m_selectedSlot.Preconstructed ? 0 : (!DeckUtility.DecksAreEqual(this.m_previousSlot?.DeckInfo, this.m_selectedSlot?.DeckInfo) ? 1 : 0)) != 0)
      {
        this.OnSave();
      }
      else
      {
        this.m_isBeingSave = false;
        this.OnExit();
      }
    }

    private void OnRemoveRequest()
    {
      if (this.m_selectedSlot.Preconstructed)
        return;
      ButtonData[] buttonDataArray = new ButtonData[2]
      {
        new ButtonData((TextData) 9912, new Action(this.OnRemoveConfirm)),
        new ButtonData((TextData) 68421, new Action(this.ClosePopup))
      };
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
        return;
      PopupInfoManager.ClearAllMessages();
      this.m_onPopup = true;
      PopupInfoManager.Show(stateLayer.GetChainEnd(), new PopupInfo()
      {
        title = (RawTextData) 52822,
        message = (RawTextData) 76361,
        buttons = buttonDataArray,
        selectedButton = 1,
        style = PopupStyle.Error
      });
    }

    private void ClosePopup() => this.m_onPopup = false;

    private void OnRemoveConfirm()
    {
      this.ClosePopup();
      DeckSlot selectedSlot = this.m_selectedSlot;
      if (!selectedSlot.Id.HasValue)
        return;
      this.m_ui.interactable = false;
      this.m_frame.SendRemoveSquadRequest(selectedSlot.Id.Value);
    }

    private void OnExit()
    {
      this.ClosePopup();
      this.ExitAfterSave = false;
      this.m_safeExit = true;
      if (this.m_isBeingSave)
        return;
      if ((bool) (UnityEngine.Object) this.m_ui)
        this.m_ui.interactable = false;
      Main.monoBehaviour.StartCoroutine(this.HideAllEnumerator());
    }

    private void OnRemoveDeckResult(RemoveDeckResultEvent result)
    {
      this.ClosePopup();
      this.m_ui.interactable = true;
      if (result.Result != CmdResult.Success)
        return;
      this.m_isBeingSave = false;
      this.OnExit();
    }

    private void OnCloneConfirme()
    {
      this.ClosePopup();
      if (DeckUtility.GetRemainingSlotsForWeapon(this.m_weapon) == 0)
        return;
      this.m_selectedSlot = this.m_selectedSlot.Clone(false);
      string name;
      RuntimeData.TryGetText(92537, out name);
      this.m_selectedSlot.SetName(name);
      this.m_previousSlot = (DeckSlot) null;
      this.m_selectedSlot.DeckInfo.Id = new int?();
      this.m_ui.interactable = true;
      DeckInfo deckInfo1 = new DeckInfo(this.m_selectedSlot.DeckInfo)
      {
        Name = RuntimeData.FormattedText(92537, (IValueProvider) null),
        Id = new int?()
      };
      DeckInfo deckInfo2 = this.m_selectedSlot.DeckInfo.TrimCopy();
      this.m_ui.OnCloneValidate(this.m_selectedSlot);
      this.m_frame.SendSaveSquadRequest(deckInfo2.Id, deckInfo2.Name, (Family) deckInfo2.God, deckInfo2.Weapon, (IReadOnlyList<int>) deckInfo2.Companions, (IReadOnlyList<int>) deckInfo2.Spells);
    }

    private void OnCloneCanceld()
    {
      this.ClosePopup();
      this.m_selectedSlot = this.m_ui.OnCloneCanceled();
    }

    private void OnCloneRequest(int titleid, int desc)
    {
      if (this.m_inAnimation)
        return;
      if (DeckUtility.GetRemainingSlotsForWeapon(new DeckInfo(this.m_selectedSlot.DeckInfo).Weapon) > 0)
      {
        StateLayer stateLayer;
        if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
          return;
        ButtonData[] buttonDataArray = new ButtonData[2]
        {
          new ButtonData((TextData) 48064, new Action(this.OnCloneConfirme)),
          new ButtonData((TextData) 26918, new Action(this.OnCloneCanceld))
        };
        PopupInfoManager.ClearAllMessages();
        this.m_onPopup = true;
        PopupInfoManager.Show(stateLayer.GetChainEnd(), new PopupInfo()
        {
          title = (RawTextData) titleid,
          message = (RawTextData) desc,
          buttons = buttonDataArray,
          selectedButton = 1,
          style = PopupStyle.Normal
        });
      }
      else
      {
        StateLayer stateLayer;
        if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
          return;
        ButtonData[] buttonDataArray = new ButtonData[1]
        {
          new ButtonData((TextData) 27169, new Action(this.OnCloneCanceld))
        };
        this.m_onPopup = true;
        PopupInfoManager.ClearAllMessages();
        PopupInfoManager.Show(stateLayer.GetChainEnd(), new PopupInfo()
        {
          title = (RawTextData) 4176,
          message = (RawTextData) 52887,
          buttons = buttonDataArray,
          selectedButton = 1,
          style = PopupStyle.Normal
        });
      }
    }
  }
}
