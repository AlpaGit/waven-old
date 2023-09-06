// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapFeedbackResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class FightMapFeedbackResources : ScriptableObject
  {
    public const int NoBorderNoCorner = 0;
    public const int OneBorder = 1;
    public const int TwoBordersOpposite = 2;
    public const int TwoBorders = 3;
    public const int ThreeBorders = 4;
    public const int FourBorders = 5;
    public const int OneCorner = 6;
    public const int TwoCornersSameSide = 7;
    public const int TwoCornersOpposite = 8;
    public const int ThreeCorners = 9;
    public const int FourCorners = 10;
    public const int OneBorderOneCornerRight = 11;
    public const int OneBorderOneCornerLeft = 12;
    public const int OneBorderTwoCorners = 13;
    public const int TwoBordersOneCorner = 14;
    public const int SpellTarget = 15;
    [Header("Colors")]
    [SerializeField]
    private FightMapFeedbackColors m_feedbackColors;
    [Header("Area Feedbacks")]
    [SerializeField]
    private Material m_areaFeedbackMaterial;
    [SerializeField]
    private Sprite[] m_areaFeedbackSprites;
    [Header("Movement Feedbacks")]
    [SerializeField]
    private Material m_movementFeedbackMaterial;
    [SerializeField]
    private MovementFeedbackResources m_movementFeedbackResources;

    public FightMapFeedbackColors feedbackColors => this.m_feedbackColors;

    public Sprite[] areaFeedbackSprites => this.m_areaFeedbackSprites;

    public Material areaFeedbackMaterial => this.m_areaFeedbackMaterial;

    public Material movementFeedbackMaterial => this.m_movementFeedbackMaterial;

    public MovementFeedbackResources movementFeedbackResources => this.m_movementFeedbackResources;
  }
}
