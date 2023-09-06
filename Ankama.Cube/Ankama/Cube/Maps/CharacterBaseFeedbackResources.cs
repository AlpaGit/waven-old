// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.CharacterBaseFeedbackResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class CharacterBaseFeedbackResources : ScriptableObject
  {
    [SerializeField]
    private Sprite m_baseSprite;
    [SerializeField]
    private Sprite m_attackedSprite;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_notPlayableAlpha = 1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_actionUsedAlpha = 0.75f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_actionAvailableAlpha = 1f;
    [SerializeField]
    private FightMapFeedbackColors m_feedbackColors;

    public Sprite baseSprite => this.m_baseSprite;

    public Sprite attackedSprite => this.m_attackedSprite;

    public float notPlayableAlpha => this.m_notPlayableAlpha;

    public float actionUsedAlpha => this.m_actionUsedAlpha;

    public float actionAvailableAlpha => this.m_actionAvailableAlpha;

    public FightMapFeedbackColors feedbackColors => this.m_feedbackColors;
  }
}
