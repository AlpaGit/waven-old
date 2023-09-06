// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.MatchmakingUIGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Configuration;
using Ankama.Cube.Data;
using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public abstract class MatchmakingUIGroup : AbstractMatchmakingUI
  {
    [SerializeField]
    private PlayerPanel[] m_playerPanels;
    [SerializeField]
    private GameObject m_matchmakingButtonComponent;
    [SerializeField]
    private Button m_launchMatchmakingButton;
    [SerializeField]
    private Button m_cancelMatchmakingButton;
    private List<FightPlayerInfo> m_allies = new List<FightPlayerInfo>();

    private void Start()
    {
      this.m_playerInvitationPanel.onInvitationSend += (Action<FightPlayerInfo>) (s => this.UpdateInactivityTime());
      this.m_playerInvitationPanel.onInvitationCanceled += (Action<FightPlayerInfo>) (s => this.UpdateInactivityTime());
      this.m_launchMatchmakingButton.onClick.AddListener(new UnityAction(this.OnLaunchMatchmakingButtonClick));
      this.m_cancelMatchmakingButton.onClick.AddListener(new UnityAction(this.OnCancelMatchmakingButtonClick));
      this.m_matchmakingButtonComponent.SetActive(ApplicationConfig.debugMode);
    }

    public override void Init()
    {
      PlayerData instance = PlayerData.instance;
      int currentDeckId = instance.currentDeckId;
      Tuple<SquadDefinition, SquadFakeData> squadDataByDeckId = this.GetSquadDataByDeckId(currentDeckId);
      int level = 6;
      DeckInfo deckInfo;
      if (instance.TryGetDeckById(currentDeckId, out deckInfo))
        level = deckInfo.GetLevel((ILevelProvider) instance.weaponInventory);
      this.m_playerPanels[0].Set(instance.nickName, level, squadDataByDeckId.Item2);
      for (int index = 1; index < this.m_playerPanels.Length; ++index)
        this.m_playerPanels[index].SetEmpty();
      this.m_launchMatchmakingButton.gameObject.SetActive(true);
      this.m_cancelMatchmakingButton.gameObject.SetActive(false);
      this.m_playerInvitationPanel.Init();
      this.m_allies.Clear();
      this.m_playerInvitationPanel.SetInvitationsLocked(false);
    }

    public void OnJoinGroupFail(long inviterId) => MatchmakingPopupHandler.instance.ShowMessage(MatchmakingPopupHandler.MessageType.JoinGroupFail, (FightPlayerInfo) null);

    public void OnGroupLeft(long oldGroup)
    {
      for (int index = this.m_allies.Count - 1; index >= 0; --index)
      {
        FightPlayerInfo ally = this.m_allies[index];
        this.RemoveAlly(ally);
        this.m_allies.Remove(ally);
      }
    }

    public void OnGroupModified(long playerId, IList<FightPlayerInfo> players)
    {
      for (int index = this.m_allies.Count - 1; index >= 0; --index)
      {
        FightPlayerInfo ally = this.m_allies[index];
        if (!this.PlayerListContain(players, ally.Uid))
        {
          this.RemoveAlly(ally);
          this.m_allies.Remove(ally);
        }
      }
      for (int index = 0; index < players.Count; ++index)
      {
        FightPlayerInfo player = players[index];
        if (playerId != player.Uid && !this.PlayerListContain((IList<FightPlayerInfo>) this.m_allies, player.Uid))
        {
          this.AddAlly(player);
          this.m_allies.Add(player);
        }
      }
      this.m_playerInvitationPanel.RemoveAllReceivedInvitations();
      this.m_playerInvitationPanel.SetInvitationsLocked(this.m_allies.Count + 1 >= this.m_playerPanels.Length);
      InactivityHandler.UpdateActivity();
    }

    private bool PlayerListContain(IList<FightPlayerInfo> players, long id)
    {
      for (int index = 0; index < players.Count; ++index)
      {
        FightPlayerInfo player = players[index];
        if (id == player.Uid)
          return true;
      }
      return false;
    }

    private void RemoveAlly(FightPlayerInfo player)
    {
      MatchmakingPopupHandler.instance.ShowMessage(MatchmakingPopupHandler.MessageType.PlayerLeaveGroup, player);
      for (int index = 1; index < this.m_playerPanels.Length; ++index)
      {
        if (this.m_playerPanels[index].playerId == player.Uid)
        {
          this.m_playerPanels[index].SetEmpty(true);
          break;
        }
      }
    }

    private void AddAlly(FightPlayerInfo player)
    {
      for (int index = 1; index < this.m_playerPanels.Length; ++index)
      {
        if (this.m_playerPanels[index].isEmpty)
        {
          Tuple<SquadDefinition, SquadFakeData> squadDataByWeaponId = this.GetSquadDataByWeaponId(player.WeaponId.Value);
          this.m_playerPanels[index].Set(player, player.Level, squadDataByWeaponId.Item2, true);
          break;
        }
      }
    }

    private void OnLaunchMatchmakingButtonClick()
    {
      this.m_launchMatchmakingButton.gameObject.SetActive(false);
      this.m_cancelMatchmakingButton.gameObject.SetActive(true);
      this.m_playerInvitationPanel.SetInvitationsLocked(true);
      Action matchmakingClick = this.onLaunchMatchmakingClick;
      if (matchmakingClick == null)
        return;
      matchmakingClick();
    }

    private void OnCancelMatchmakingButtonClick()
    {
      this.m_launchMatchmakingButton.gameObject.SetActive(true);
      this.m_cancelMatchmakingButton.gameObject.SetActive(false);
      this.m_playerInvitationPanel.SetInvitationsLocked(this.m_allies.Count + 1 >= this.m_playerPanels.Length);
      Action matchmakingClick = this.onCancelMatchmakingClick;
      if (matchmakingClick == null)
        return;
      matchmakingClick();
    }
  }
}
