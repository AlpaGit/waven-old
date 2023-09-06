﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.IEntityWithLife
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Entities
{
  public interface IEntityWithLife : IEntity
  {
    int baseLife { get; }

    int armoredLife { get; }

    int life { get; }

    int armor { get; }

    int resistance { get; }

    int hitLimit { get; }

    bool wounded { get; }

    bool hasArmor { get; }
  }
}