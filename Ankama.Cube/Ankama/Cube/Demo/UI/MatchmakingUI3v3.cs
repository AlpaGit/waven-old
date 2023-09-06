// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.MatchmakingUI3v3
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class MatchmakingUI3v3 : MatchmakingUIGroup
  {
    [SerializeField]
    private PlayerPanel[] m_opponentPanels;

    public void SetOpponents(IList<FightInfo.Types.Player> opponents)
    {
      for (int index = 0; index < opponents.Count && index < this.m_opponentPanels.Length; ++index)
      {
        FightInfo.Types.Player opponent = opponents[index];
        Tuple<SquadDefinition, SquadFakeData> squadDataByWeaponId = this.GetSquadDataByWeaponId(opponent.WeaponId.Value);
        this.m_opponentPanels[index].Set(opponent.Name, opponent.Level, squadDataByWeaponId.Item2);
      }
    }
  }
}
