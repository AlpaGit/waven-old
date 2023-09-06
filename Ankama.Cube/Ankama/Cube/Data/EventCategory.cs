// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EventCategory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum EventCategory
  {
    Any = 1,
    ActionPointsChanged = 2,
    ReserveChanged = 3,
    LifeArmorChanged = 4,
    ElementPointsChanged = 5,
    ElementaryStateChanged = 6,
    MovementPointsChanged = 7,
    EntityMoved = 8,
    EntityAddedOrRemoved = 9,
    PropertyChanged = 10, // 0x0000000A
    DamageModifierChanged = 11, // 0x0000000B
    HealModifierChanged = 12, // 0x0000000C
    SpellCostModification = 13, // 0x0000000D
    SpellsMoved = 14, // 0x0000000E
    PlaySpellForbiddenChanged = 15, // 0x0000000F
  }
}
