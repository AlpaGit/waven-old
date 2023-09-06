// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapFeedbackColors
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class FightMapFeedbackColors : ScriptableObject
  {
    [SerializeField]
    private Color m_localPlayerColor = new Color(0.0f, 0.68235296f, 1f);
    [SerializeField]
    private Color m_allyPlayerColor = new Color(0.0f, 1f, 1f);
    [SerializeField]
    private Color m_localOpponentColor = new Color(1f, 0.0f, 0.498039216f);
    [SerializeField]
    private Color m_opponentPlayerColor = new Color(1f, 0.1764706f, 0.06666667f);
    [SerializeField]
    private Color m_targetableAreaColor = new Color(0.0f, 0.68235296f, 1f);

    public Color targetableAreaColor => this.m_targetableAreaColor;

    public Color GetPlayerColor(PlayerType playerType)
    {
      if (playerType == PlayerType.Player)
        return this.m_localPlayerColor;
      if (playerType == (PlayerType.Opponent | PlayerType.Local))
        return this.m_localOpponentColor;
      if (playerType.HasFlag((Enum) PlayerType.Ally))
        return this.m_allyPlayerColor;
      if (playerType.HasFlag((Enum) PlayerType.Opponent))
        return this.m_opponentPlayerColor;
      throw new ArgumentOutOfRangeException(nameof (playerType), (object) playerType, (string) null);
    }
  }
}
