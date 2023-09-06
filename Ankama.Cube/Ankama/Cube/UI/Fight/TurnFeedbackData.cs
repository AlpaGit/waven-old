// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.TurnFeedbackData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public class TurnFeedbackData : ScriptableObject
  {
    [SerializeField]
    public TurnFeedbackData.PlayerSideData player;
    [SerializeField]
    public TurnFeedbackData.PlayerSideData playerTeam;
    [SerializeField]
    public TurnFeedbackData.PlayerSideData opponent;
    [SerializeField]
    public TurnFeedbackData.PlayerSideData opponentTeam;
    [SerializeField]
    public TurnFeedbackData.PlayerSideData boss;

    [Serializable]
    public struct PlayerSideData
    {
      [TextKey]
      [SerializeField]
      public int messageKey;
      [SerializeField]
      public Sprite icon;
      [SerializeField]
      public Color nameColor;
      [SerializeField]
      public Color titleColor;
    }
  }
}
