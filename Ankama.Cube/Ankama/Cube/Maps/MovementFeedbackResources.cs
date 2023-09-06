// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MovementFeedbackResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class MovementFeedbackResources : ScriptableObject
  {
    public const int CornerNegXNegZ = 0;
    public const int CornerNegXPosZ = 1;
    public const int CornerPosXNegZ = 2;
    public const int CornerPosXPosZ = 3;
    public const int EndNegX = 4;
    public const int EndPosZ = 5;
    public const int StartNegX = 6;
    public const int StartPosZ = 7;
    public const int ThroughNegZ = 8;
    public const int ThroughPosZ = 9;
    [SerializeField]
    private Sprite[] m_sprites;
    [Header("Cursors")]
    [SerializeField]
    private Sprite m_allyCursorSprite;

    public Sprite[] sprites => this.m_sprites;

    public Sprite allyCursorSprite => this.m_allyCursorSprite;
  }
}
