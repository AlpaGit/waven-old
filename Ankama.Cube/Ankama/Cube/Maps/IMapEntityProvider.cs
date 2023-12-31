﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.IMapEntityProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public interface IMapEntityProvider
  {
    [PublicAPI]
    bool HasEntity<T>([NotNull] Predicate<T> predicate) where T : class, IEntity;

    [PublicAPI]
    bool TryGetEntity<T>(Predicate<T> predicate, out T entityStatus) where T : class, IEntity;

    [PublicAPI]
    IEnumerable<T> EnumerateEntities<T>() where T : class, IEntity;

    [PublicAPI]
    bool IsCharacterPlayable(ICharacterEntity character);

    [PublicAPI]
    bool TryGetEntityBlockingMovementAt(Vector2Int position, out IEntityWithBoardPresence entity);

    [PublicAPI]
    bool TryGetEntityAt<T>(Vector2Int position, out T character) where T : class, IEntityWithBoardPresence;

    [PublicAPI]
    bool TryGetPlayableCharacterAt(Vector2Int position, out ICharacterEntity character);

    [PublicAPI]
    IEnumerable<ICharacterEntity> EnumeratePlayableCharacters();
  }
}
