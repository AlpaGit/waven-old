// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.MatchMaking.MatchMakingFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Network;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Protocols.FightPreparationProtocol;
using Ankama.Cube.Protocols.FightProtocol;
using Ankama.Cube.Protocols.PlayerProtocol;
using Google.Protobuf;
using System;

namespace Ankama.Cube.TEMPFastEnterMatch.MatchMaking
{
  public class MatchMakingFrame : CubeMessageFrame
  {
    public Action<FightInfo> onGameCreated;
    public Action onGameCanceled;
    public Action onGameError;
    private int m_lastFightDefIdRequested;

    public event Action<ChangeGodResultEvent> OnChangedGod;

    public event Action<SelectDeckAndWeaponResultEvent> OnSelectedWeaponAndDeck;

    public MatchMakingFrame()
    {
      this.WhenReceiveEnqueue<FightStartedEvent>(new Action<FightStartedEvent>(this.OnFightStartedEvent));
      this.WhenReceiveEnqueue<FightNotStartedEvent>(new Action<FightNotStartedEvent>(this.OnFightNotStartedEvent));
      this.WhenReceiveEnqueue<ChangeGodResultEvent>(new Action<ChangeGodResultEvent>(this.OnChangeGodResultEvent));
      this.WhenReceiveEnqueue<LaunchMatchmakingResultEvent>(new Action<LaunchMatchmakingResultEvent>(this.OnLaunchMatchmakingResultEvent));
      this.WhenReceiveEnqueue<MatchmakingStartedEvent>(new Action<MatchmakingStartedEvent>(this.OnMatchmakingStartedEvent));
      this.WhenReceiveEnqueue<MatchmakingStoppedEvent>(new Action<MatchmakingStoppedEvent>(this.OnMatchmakingStoppedEvent));
      this.WhenReceiveEnqueue<MatchmakingSuccessEvent>(new Action<MatchmakingSuccessEvent>(this.OnMatchmakingSuccessEvent));
      this.WhenReceiveEnqueue<SelectDeckAndWeaponResultEvent>(new Action<SelectDeckAndWeaponResultEvent>(this.OnSelectDeckAndWeaponResultHandler));
      this.WhenReceiveEnqueue<FightGroupUpdatedEvent>(new Action<FightGroupUpdatedEvent>(this.OnFightGroupUpdatedEvent));
    }

    private void OnMatchmakingSuccessEvent(MatchmakingSuccessEvent obj)
    {
    }

    private void OnMatchmakingStoppedEvent(MatchmakingStoppedEvent obj)
    {
    }

    private void OnMatchmakingStartedEvent(MatchmakingStartedEvent obj)
    {
    }

    public void SendCancelGame() => this.m_connection.Write((IMessage) new LeaveFightGroupCmd());

    public void SendToggleMatchmaking() => this.m_connection.Write((IMessage) new LaunchMatchmakingCmd()
    {
      FightDefId = this.m_lastFightDefIdRequested
    });

    private void OnLaunchMatchmakingResultEvent(LaunchMatchmakingResultEvent evt)
    {
    }

    public void SendForceFightVersusAI() => this.m_connection.Write((IMessage) new ForceMatchmakingAgainstAICmd());

    public void SendCreateGame(int fightDefId, int deckId, int? forcedLevel)
    {
      this.m_lastFightDefIdRequested = fightDefId;
      this.m_connection.Write((IMessage) new CreateFightGroupCmd());
      this.m_connection.Write((IMessage) new LaunchMatchmakingCmd()
      {
        FightDefId = fightDefId
      });
    }

    private void OnFightNotStartedEvent(FightNotStartedEvent evt)
    {
      Action onGameError = this.onGameError;
      if (onGameError == null)
        return;
      onGameError();
    }

    private void OnFightStartedEvent(FightStartedEvent evt)
    {
      Action<FightInfo> onGameCreated = this.onGameCreated;
      if (onGameCreated == null)
        return;
      onGameCreated(evt.FightInfo);
    }

    private void OnFightGroupUpdatedEvent(FightGroupUpdatedEvent evt)
    {
      if (!evt.GroupRemoved)
        return;
      Action onGameCanceled = this.onGameCanceled;
      if (onGameCanceled == null)
        return;
      onGameCanceled();
    }

    private void OnChangeGodResultEvent(ChangeGodResultEvent obj)
    {
      Action<ChangeGodResultEvent> onChangedGod = this.OnChangedGod;
      if (onChangedGod == null)
        return;
      onChangedGod(obj);
    }

    private void OnSelectDeckAndWeaponResultHandler(SelectDeckAndWeaponResultEvent obj)
    {
      Action<SelectDeckAndWeaponResultEvent> selectedWeaponAndDeck = this.OnSelectedWeaponAndDeck;
      if (selectedWeaponAndDeck == null)
        return;
      selectedWeaponAndDeck(obj);
    }

    public void UpdatePlayerGod(God god) => this.m_connection.Write((IMessage) new ChangeGodCmd()
    {
      God = (int) god
    });

    public void SelectDeckAndWeapon(int weaponId, int? deckId) => this.m_connection.Write((IMessage) new SelectDeckAndWeaponCmd()
    {
      SelectedWeapon = new int?(weaponId),
      SelectedDecks = {
        new SelectDeckInfo()
        {
          WeaponId = weaponId,
          DeckId = deckId
        }
      }
    });
  }
}
