// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityRemovedReason
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum EntityRemovedReason
  {
    None = 1,
    Death = 2,
    Transformation = 3,
    PlayerLeft = 4,
    Resurrection3V3 = 5,
    EntitySummonedAtSamePlace = 6,
    Activated = 7,
    OwnerDead = 8,
    CompanionReturned = 9,
  }
}
