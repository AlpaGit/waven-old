// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.MatchmakingUI1v1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class MatchmakingUI1v1 : AbstractMatchmakingUI
  {
    [SerializeField]
    private PlayerPanel m_playerPanel;
    [SerializeField]
    private PlayerPanel m_opponentPanel;

    private void Start()
    {
      this.m_playerInvitationPanel.onInvitationSend += (Action<FightPlayerInfo>) (s => this.UpdateInactivityTime());
      this.m_playerInvitationPanel.onInvitationCanceled += (Action<FightPlayerInfo>) (s => this.UpdateInactivityTime());
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
      this.m_playerPanel.Set(instance.nickName, level, squadDataByDeckId.Item2);
      this.m_playerInvitationPanel.Init();
    }

    public void SetOpponent(FightInfo.Types.Player opponent)
    {
      Tuple<SquadDefinition, SquadFakeData> squadDataByWeaponId = this.GetSquadDataByWeaponId(opponent.WeaponId.Value);
      this.m_opponentPanel.Set(opponent.Name, opponent.Level, squadDataByWeaponId.Item2);
    }
  }
}
