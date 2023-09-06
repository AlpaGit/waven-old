// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  public class FightStatus : IMapEntityProvider
  {
    public static FightStatus local;
    public readonly int fightId;
    public readonly FightMapStatus mapStatus;
    public readonly FightContext context;
    public int localPlayerId;
    public int turnIndex;
    public int currentTurnPlayerId;
    public FightStatusEndReason endReason;
    private readonly Dictionary<int, EntityStatus> m_entities = new Dictionary<int, EntityStatus>();
    private readonly List<int> m_dirtyEntities = new List<int>();
    private EntitiesChangedFlags m_entitiesChangedFlags;

    public event EntitiesChangedDelegate EntitiesChanged;

    public bool isEnded => this.endReason != 0;

    public FightStatus(int fightId, FightMapStatus mapStatus)
    {
      this.fightId = fightId;
      this.mapStatus = mapStatus;
      this.context = FightContext.Create(fightId);
    }

    public void Dispose()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.context);
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (entityStatus is IEntityWithBoardPresence withBoardPresence)
        {
          IsoObject view = withBoardPresence.view;
          if ((UnityEngine.Object) null != (UnityEngine.Object) view)
          {
            view.DetachFromCell();
            view.Destroy();
          }
        }
      }
      this.m_entities.Clear();
      this.m_dirtyEntities.Clear();
    }

    public void TriggerUpdateEvents()
    {
      if (this.m_entitiesChangedFlags == EntitiesChangedFlags.None)
        return;
      EntitiesChangedFlags entitiesChangedFlags = this.m_entitiesChangedFlags;
      this.m_entitiesChangedFlags = EntitiesChangedFlags.None;
      EntitiesChangedDelegate entitiesChanged = this.EntitiesChanged;
      if (entitiesChanged == null)
        return;
      entitiesChanged(this, entitiesChangedFlags);
    }

    [PublicAPI]
    public void AddEntity([NotNull] EntityStatus entity)
    {
      this.m_entities[entity.id] = entity;
      this.m_entitiesChangedFlags |= EntitiesChangedFlags.Added;
    }

    [PublicAPI]
    public void RemoveEntity(int id)
    {
      this.m_entities[id].MarkForRemoval();
      this.m_entitiesChangedFlags |= EntitiesChangedFlags.Removed;
      this.m_dirtyEntities.Add(id);
      FightLogicExecutor.NotifyEntityRemoved(this.fightId);
    }

    [PublicAPI]
    public bool TryRemoveEntity(int id)
    {
      EntityStatus entityStatus;
      if (!this.m_entities.TryGetValue(id, out entityStatus))
        return false;
      entityStatus.MarkForRemoval();
      this.m_entitiesChangedFlags |= EntitiesChangedFlags.Removed;
      this.m_dirtyEntities.Add(id);
      FightLogicExecutor.NotifyEntityRemoved(this.fightId);
      return true;
    }

    public void NotifyEntityAreaMoved() => this.m_entitiesChangedFlags |= EntitiesChangedFlags.AreaMoved;

    public void NotifyEntityPlayableStateChanged() => this.m_entitiesChangedFlags |= EntitiesChangedFlags.PlayableState;

    [PublicAPI]
    public void Cleanup(int counter)
    {
      for (int index = 0; index < counter; ++index)
        this.m_entities.Remove(this.m_dirtyEntities[index]);
      this.m_dirtyEntities.RemoveRange(0, counter);
    }

    public bool HasEntity<T>(Predicate<T> predicate) where T : class, IEntity
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is T obj && predicate(obj))
          return true;
      }
      return false;
    }

    public bool TryGetEntity<T>(Predicate<T> predicate, out T entityStatus) where T : class, IEntity
    {
      foreach (EntityStatus entityStatus1 in this.m_entities.Values)
      {
        if (!entityStatus1.isDirty && entityStatus1 is T obj && predicate(obj))
        {
          entityStatus = obj;
          return true;
        }
      }
      entityStatus = default (T);
      return false;
    }

    public IEnumerable<T> EnumerateEntities<T>() where T : class, IEntity
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is T obj)
          yield return obj;
      }
    }

    public bool IsCharacterPlayable(ICharacterEntity character)
    {
      if (character.actionUsed || character.ownerId != this.localPlayerId)
        return false;
      return character.canMove || character.canDoActionOnTarget;
    }

    public bool HasEntityBlockingMovementAt(Vector2Int position)
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is IEntityWithBoardPresence withBoardPresence && withBoardPresence.blocksMovement && withBoardPresence.area.Intersects(position))
          return true;
      }
      return false;
    }

    public bool TryGetEntityBlockingMovementAt(
      Vector2Int position,
      out IEntityWithBoardPresence entityBlockingMovement)
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is IEntityWithBoardPresence withBoardPresence && withBoardPresence.blocksMovement && withBoardPresence.area.Intersects(position))
        {
          entityBlockingMovement = withBoardPresence;
          return true;
        }
      }
      entityBlockingMovement = (IEntityWithBoardPresence) null;
      return false;
    }

    public bool TryGetEntityAt<T>(Vector2Int position, out T character) where T : class, IEntityWithBoardPresence
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is T obj && obj.area.Intersects(position))
        {
          character = obj;
          return true;
        }
      }
      character = default (T);
      return false;
    }

    public bool TryGetPlayableCharacterAt(Vector2Int position, out ICharacterEntity character)
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is ICharacterEntity character1 && character1.area.Intersects(position))
        {
          if (this.IsCharacterPlayable(character1))
          {
            character = character1;
            return true;
          }
          break;
        }
      }
      character = (ICharacterEntity) null;
      return false;
    }

    public IEnumerable<ICharacterEntity> EnumeratePlayableCharacters()
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is ICharacterEntity character && this.IsCharacterPlayable(character))
          yield return character;
      }
    }

    [PublicAPI]
    public bool HasEntity(int id) => this.m_entities.ContainsKey(id);

    [NotNull]
    [PublicAPI]
    public EntityStatus GetEntity(int id) => this.m_entities[id];

    [PublicAPI]
    public bool TryGetEntity(int id, out EntityStatus entityStatus) => this.m_entities.TryGetValue(id, out entityStatus);

    [PublicAPI]
    public bool HasEntity<T>(int id) where T : IEntity
    {
      EntityStatus entityStatus;
      return this.m_entities.TryGetValue(id, out entityStatus) && entityStatus is T;
    }

    [NotNull]
    [PublicAPI]
    public T GetEntity<T>(int id) where T : IEntity => (T) this.m_entities[id];

    [PublicAPI]
    public bool TryGetEntity<T>(int id, out T entityStatus) where T : IEntity
    {
      EntityStatus entityStatus1;
      if (this.m_entities.TryGetValue(id, out entityStatus1) && entityStatus1 is T obj)
      {
        entityStatus = obj;
        return true;
      }
      entityStatus = default (T);
      return false;
    }

    [PublicAPI]
    public bool HasEntity<T>(int id, [NotNull] Predicate<T> predicate) where T : IEntity
    {
      EntityStatus entityStatus;
      return this.m_entities.TryGetValue(id, out entityStatus) && entityStatus is T obj && predicate(obj);
    }

    [PublicAPI]
    public bool FindEntity<T>(Predicate<T> predicate, out T entityStatus) where T : IEntity
    {
      foreach (EntityStatus entityStatus1 in this.m_entities.Values)
      {
        if (entityStatus1 is T obj && predicate(obj))
        {
          entityStatus = obj;
          return true;
        }
      }
      entityStatus = default (T);
      return false;
    }

    [PublicAPI]
    public bool TryGetEntity<T>(int id, Predicate<T> predicate, out T entityStatus) where T : IEntity
    {
      EntityStatus entityStatus1;
      if (this.m_entities.TryGetValue(id, out entityStatus1) && entityStatus1 is T obj && predicate(obj))
      {
        entityStatus = obj;
        return true;
      }
      entityStatus = default (T);
      return false;
    }

    [PublicAPI]
    public IEnumerable<IEntity> EnumerateEntities()
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty)
          yield return (IEntity) entityStatus;
      }
    }

    [PublicAPI]
    public IEnumerable<T> EnumerateEntities<T>(Predicate<T> predicate) where T : IEntity
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is T obj && predicate(obj))
          yield return obj;
      }
    }

    [PublicAPI]
    public IEnumerable<Coord> EnumerateCoords() => this.mapStatus.EnumerateCoords();

    public PlayerStatus GetLocalPlayer() => (PlayerStatus) this.m_entities[this.localPlayerId];

    public IEnumerable<PlayerStatus> EnumeratePlayers()
    {
      foreach (EntityStatus entityStatus in this.m_entities.Values)
      {
        if (!entityStatus.isDirty && entityStatus is PlayerStatus playerStatus)
          yield return playerStatus;
      }
    }
  }
}
