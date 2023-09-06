// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using System;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class StatValue : AbstractStat
  {
    [SerializeField]
    private RawTextField m_valueText;
    [SerializeField]
    private StatData m_statData;

    public void Init(StatBoard.PlayerStatData statData, FightStatType type)
    {
      int num;
      if (statData.playerStats.Stats.TryGetValue((int) type, out num))
      {
        if (type == FightStatType.PlayTime)
        {
          TimeSpan timeSpan = TimeSpan.FromSeconds((double) num);
          this.m_valueText.SetText(timeSpan.Hours > 0 ? timeSpan.ToString("hh\\:mm\\:ss") : timeSpan.ToString("mm\\:ss"));
        }
        else
          this.m_valueText.SetText(num.ToString());
      }
      else
        this.m_valueText.SetText("-");
      this.m_valueText.color = statData.playerStats.Titles.Contains((int) type) ? this.m_statData.bestValueColor : this.m_statData.neutralValueColor;
    }
  }
}
