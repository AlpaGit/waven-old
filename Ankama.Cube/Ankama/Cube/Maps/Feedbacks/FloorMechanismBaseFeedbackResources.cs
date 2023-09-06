// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Feedbacks.FloorMechanismBaseFeedbackResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps.Feedbacks
{
  public sealed class FloorMechanismBaseFeedbackResources : ScriptableObject
  {
    public const int FourBorders = 0;
    public const int ThreeBorders = 1;
    public const int TwoBordersInCorner = 2;
    public const int TwoBordersOpposite = 3;
    public const int OneBorder = 4;
    public const int NoBorder = 5;
    [SerializeField]
    private Sprite[] m_sprites;
    [SerializeField]
    private FightMapFeedbackColors m_feedbackColors;

    public Sprite[] sprites => this.m_sprites;

    public FightMapFeedbackColors feedbackColors => this.m_feedbackColors;
  }
}
