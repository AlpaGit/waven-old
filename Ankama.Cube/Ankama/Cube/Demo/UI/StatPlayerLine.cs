// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatPlayerLine
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class StatPlayerLine : AbstractStatLine<StatPlayer>
  {
    public IEnumerator Init(
      List<StatBoard.PlayerStatData> allies,
      List<StatBoard.PlayerStatData> opponents,
      bool displayOpponents)
    {
      StatPlayerLine statPlayerLine = this;
      yield return (object) statPlayerLine.InitList(statPlayerLine.m_alliesStats, statPlayerLine.m_alliesGroup, allies);
      if (displayOpponents)
      {
        statPlayerLine.m_opponentsGroup.gameObject.SetActive(true);
        yield return (object) statPlayerLine.InitList(statPlayerLine.m_opponentStats, statPlayerLine.m_opponentsGroup, opponents);
      }
      else
        statPlayerLine.m_opponentsGroup.gameObject.SetActive(false);
    }

    private IEnumerator InitList(
      List<StatPlayer> stats,
      LayoutGroup group,
      List<StatBoard.PlayerStatData> statDatas)
    {
      int statDataCount = 0;
      for (int i = 0; i < statDatas.Count; ++i)
      {
        StatBoard.PlayerStatData statData = statDatas[i];
        if (i >= stats.Count)
        {
          StatPlayer statPlayer = Object.Instantiate<StatPlayer>(stats[0]);
          statPlayer.transform.SetParent(group.transform);
          stats.Add(statPlayer);
        }
        StatPlayer stat = stats[i];
        yield return (object) stat.Init(statData);
        stat.gameObject.SetActive(true);
        ++statDataCount;
        stat = (StatPlayer) null;
      }
      for (int index = statDataCount; index < stats.Count; ++index)
        stats[index].gameObject.SetActive(false);
    }
  }
}
