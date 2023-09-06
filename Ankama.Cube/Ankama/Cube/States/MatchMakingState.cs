// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.MatchMakingState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.MatchMaking;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.Utility;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class MatchMakingState : LoadSceneStateContext
  {
    private MatchMakingUI m_ui;
    private MatchMakingFrame m_frame;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      MatchMakingState uiloader = this;
      LoadSceneStateContext.UILoader<MatchMakingUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<MatchMakingUI>((LoadSceneStateContext) uiloader, "MatchmakingUI", "core/scenes/ui/matchmaking", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable()
    {
      this.m_frame = new MatchMakingFrame()
      {
        onGameCreated = new Action<FightInfo>(this.OnGameCreated),
        onGameError = new Action(this.OnGameError)
      };
      this.m_frame.OnChangedGod += new Action<ChangeGodResultEvent>(this.OnChangedGod);
      this.m_frame.OnSelectedWeaponAndDeck += new Action<SelectDeckAndWeaponResultEvent>(this.OnSelectedDeckAndWeapon);
      PlayerData.instance.OnSelectedDeckUpdated += new Action(this.OnSelectedDeckUpdated);
      this.m_ui.onPlayRequested = new Action<int, int?>(this.OnPlayRequested);
      this.m_ui.onCancelRequested = new Action(this.OnCancelRequested);
      this.m_ui.onForceAiRequested = new Action(this.OnForceAiRequested);
      this.m_ui.onReturnClicked = new Action(StatesUtility.GotoDimensionState);
      this.m_ui.onGodSelectedChanged += new Action<God>(this.OnSelectedGodChanged);
      this.m_ui.onSelectedWeaponChanged += new Action<int>(this.OnSelectedWeaponChanged);
      this.m_ui.onSelectedDeckChanged += new Action<int>(this.OnSelectedDeckChanged);
    }

    protected override void Disable()
    {
      this.m_ui.onPlayRequested = (Action<int, int?>) null;
      this.m_ui.onCancelRequested = (Action) null;
      this.m_ui.onReturnClicked = (Action) null;
      this.m_ui.onForceAiRequested = (Action) null;
      this.m_ui.gameObject.SetActive(false);
      this.m_frame.Dispose();
    }

    private void OnSelectedGodChanged(God god)
    {
      this.m_frame.UpdatePlayerGod(god);
      this.m_ui.interactable = false;
    }

    private void OnSelectedWeaponChanged(int weaponId)
    {
      this.m_ui.OnWeaponChanged(weaponId);
      this.m_ui.interactable = false;
    }

    private void OnSelectedDeckChanged(int deckId)
    {
      DeckInfo deckInfo;
      if (!PlayerData.instance.TryGetDeckById(deckId, out deckInfo))
        return;
      int? nullable1 = deckInfo.Id;
      int num = 0;
      int? nullable2;
      if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = deckInfo.Id;
      int? deckId1 = nullable2;
      this.m_frame.SelectDeckAndWeapon(deckInfo.Weapon, deckId1);
      this.m_ui.interactable = false;
    }

    private void OnChangedGod(ChangeGodResultEvent obj)
    {
      this.m_ui.OnGodChanged();
      this.m_ui.interactable = true;
    }

    private void OnSelectedDeckAndWeapon(SelectDeckAndWeaponResultEvent obj) => this.m_ui.interactable = true;

    private void OnSelectedDeckUpdated()
    {
      DeckInfo deckInfo;
      PlayerData.instance.TryGetDeckById(PlayerData.instance.currentDeckId, out deckInfo);
      this.m_ui.SetCurrentDeck(deckInfo);
    }

    private void OnGameError() => PopupInfoManager.Show((StateContext) this, new PopupInfo()
    {
      message = (RawTextData) 54306,
      buttons = new ButtonData[1]
      {
        new ButtonData(new TextData(27169, Array.Empty<string>()))
      },
      selectedButton = 1,
      style = PopupStyle.Error
    });

    private void OnGameCreated(FightInfo fightInfo) => StatesUtility.DoTransition((StateContext) new FightState(fightInfo), StateManager.GetDefaultLayer().GetChildState());

    private void OnCancelRequested() => this.m_frame.SendCancelGame();

    private void OnForceAiRequested() => this.m_frame.SendForceFightVersusAI();

    private void OnPlayRequested(int fightDefId, int? forcedLevel) => this.m_frame.SendCreateGame(fightDefId, PlayerData.instance.currentDeckId, forcedLevel);
  }
}
