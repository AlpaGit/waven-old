// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatValueLine
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class StatValueLine : AbstractStatLine<StatValue>
  {
    public void Init(
      List<StatBoard.PlayerStatData> allies,
      List<StatBoard.PlayerStatData> opponents,
      FightStatType type,
      bool displayOpponents)
    {
      this.InitList(this.m_alliesStats, this.m_alliesGroup, allies, type);
      if (displayOpponents)
      {
        this.m_opponentsGroup.gameObject.SetActive(true);
        this.InitList(this.m_opponentStats, this.m_opponentsGroup, opponents, type);
      }
      else
        this.m_opponentsGroup.gameObject.SetActive(false);
    }

    private void InitList(
      List<StatValue> stats,
      LayoutGroup group,
      List<StatBoard.PlayerStatData> statDatas,
      FightStatType type)
    {
      int num = 0;
      for (int index = 0; index < statDatas.Count; ++index)
      {
        StatBoard.PlayerStatData statData = statDatas[index];
        if (index >= stats.Count)
        {
          StatValue statValue = Object.Instantiate<StatValue>(stats[0]);
          statValue.transform.SetParent(group.transform);
          stats.Add(statValue);
        }
        StatValue stat = stats[index];
        stat.Init(statData, type);
        stat.gameObject.SetActive(true);
        ++num;
      }
      for (int index = num; index < stats.Count; ++index)
        stats[index].gameObject.SetActive(false);
    }
  }
}
