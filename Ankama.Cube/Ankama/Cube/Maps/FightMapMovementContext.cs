// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapMovementContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Fight.Movement;
using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class FightMapMovementContext
  {
    public readonly FightMapMovementContext.Cell[] grid;
    public readonly IMapStateProvider stateProvider;
    public readonly IMapEntityProvider entityProvider;
    private bool m_hasEnded;
    private readonly HashSet<IObjectTargetableByAction> m_objectsTargetableByAction = new HashSet<IObjectTargetableByAction>();

    public ICharacterEntity trackedCharacter { get; private set; }

    public IEntityWithBoardPresence targetedEntity { get; private set; }

    public bool canMove { get; private set; }

    public bool canPassThrough { get; private set; }

    public bool canDoActionOnTarget { get; private set; }

    public bool hasEnded
    {
      get
      {
        int num = this.m_hasEnded ? 1 : 0;
        this.m_hasEnded = false;
        return num != 0;
      }
    }

    public FightMapMovementContext(
      [NotNull] IMapStateProvider stateProvider,
      [NotNull] IMapEntityProvider entityProvider)
    {
      this.stateProvider = stateProvider;
      this.entityProvider = entityProvider;
      Vector2Int sizeMin = stateProvider.sizeMin;
      Vector2Int vector2Int = stateProvider.sizeMax - sizeMin;
      int length = vector2Int.y * vector2Int.x;
      int num = sizeMin.y * vector2Int.x + sizeMin.x;
      this.grid = new FightMapMovementContext.Cell[length];
      for (int index = 0; index < length; ++index)
      {
        FightCellState cellState = stateProvider.GetCellState(index);
        int x = sizeMin.x + index % vector2Int.x;
        int y = (index + num - x) / vector2Int.x;
        Vector2Int coords = new Vector2Int(x, y);
        FightMapMovementContext.CellState state;
        if (cellState != FightCellState.None)
        {
          if (cellState != FightCellState.Movement)
            throw new ArgumentOutOfRangeException();
          state = FightMapMovementContext.CellState.Movement;
        }
        else
          state = FightMapMovementContext.CellState.None;
        this.grid[index] = new FightMapMovementContext.Cell(coords, state, (IEntityWithBoardPresence) null);
      }
    }

    public FightMapMovementContext.Cell GetCell(Vector2Int coords) => this.grid[this.stateProvider.GetCellIndex(coords.x, coords.y)];

    public bool Contains(Vector2Int coords)
    {
      Vector2Int sizeMin = this.stateProvider.sizeMin;
      Vector2Int sizeMax = this.stateProvider.sizeMax;
      return coords.x >= sizeMin.x && coords.x < sizeMax.x && coords.y >= sizeMin.y && coords.y < sizeMax.y;
    }

    public void Begin([NotNull] ICharacterEntity tracked, FightPathFinder pathFinder)
    {
      IMapStateProvider stateProvider = this.stateProvider;
      IMapEntityProvider entityProvider = this.entityProvider;
      HashSet<IObjectTargetableByAction> targetableByAction = this.m_objectsTargetableByAction;
      this.canMove = tracked.canMove;
      this.canPassThrough = tracked.HasProperty(PropertyId.CanPassThrough);
      this.canDoActionOnTarget = tracked.canDoActionOnTarget;
      int length = this.grid.Length;
      for (int index = 0; index < length; ++index)
      {
        FightMapMovementContext.Cell cell = this.grid[index];
        if (cell.state != FightMapMovementContext.CellState.None)
        {
          Vector2Int coords = cell.coords;
          IEntityWithBoardPresence entity;
          FightMapMovementContext.CellState state = !entityProvider.TryGetEntityBlockingMovementAt(coords, out entity) ? FightMapMovementContext.CellState.Movement : (entity != tracked ? FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Occupied : FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Tracked);
          this.grid[index] = new FightMapMovementContext.Cell(cell.coords, state, entity);
        }
      }
      pathFinder.FloodFill(stateProvider, this.grid, tracked.area.refCoord, tracked.movementPoints, this.canPassThrough);
      ActionType actionType = tracked.actionType;
      IEntitySelector customActionTarget = tracked.customActionTarget;
      if (customActionTarget != null)
      {
        CharacterActionValueContext context = new CharacterActionValueContext((FightStatus) entityProvider, tracked);
        foreach (IEntity enumerateEntity in customActionTarget.EnumerateEntities((DynamicValueContext) context))
        {
          if (enumerateEntity != tracked && enumerateEntity is IEntityTargetableByAction entity)
          {
            Vector2Int refCoord = entity.area.refCoord;
            if (this.IsInActionRange(refCoord, tracked))
            {
              if (entity.view is IObjectTargetableByAction view)
              {
                view.ShowActionTargetFeedback(actionType, false);
                targetableByAction.Add(view);
              }
              int cellIndex = stateProvider.GetCellIndex(refCoord.x, refCoord.y);
              FightMapMovementContext.Cell cell = this.grid[cellIndex];
              this.grid[cellIndex] = new FightMapMovementContext.Cell(refCoord, cell.state | FightMapMovementContext.CellState.Targetable, (IEntityWithBoardPresence) entity);
            }
          }
        }
      }
      else
      {
        foreach (IEntityTargetableByAction enumerateEntity in entityProvider.EnumerateEntities<IEntityTargetableByAction>())
        {
          if (enumerateEntity != tracked)
          {
            Vector2Int refCoord = enumerateEntity.area.refCoord;
            if (this.IsInActionRange(refCoord, tracked))
            {
              if (enumerateEntity.view is IObjectTargetableByAction view)
              {
                view.ShowActionTargetFeedback(actionType, false);
                targetableByAction.Add(view);
              }
              int cellIndex = stateProvider.GetCellIndex(refCoord.x, refCoord.y);
              FightMapMovementContext.Cell cell = this.grid[cellIndex];
              this.grid[cellIndex] = new FightMapMovementContext.Cell(refCoord, cell.state | FightMapMovementContext.CellState.Targetable, (IEntityWithBoardPresence) enumerateEntity);
            }
          }
        }
      }
      this.trackedCharacter = tracked;
      this.targetedEntity = (IEntityWithBoardPresence) null;
    }

    public void UpdateTarget([CanBeNull] IEntityWithBoardPresence targeted)
    {
      if (this.targetedEntity == targeted)
        return;
      if (this.targetedEntity != null)
      {
        if (this.targetedEntity.view is IObjectTargetableByAction view)
        {
          view.ShowActionTargetFeedback(this.trackedCharacter.actionType, false);
          this.m_objectsTargetableByAction.Add(view);
        }
        Vector2Int refCoord = this.targetedEntity.area.refCoord;
        int cellIndex = this.stateProvider.GetCellIndex(refCoord.x, refCoord.y);
        FightMapMovementContext.Cell cell = this.grid[cellIndex];
        this.grid[cellIndex] = new FightMapMovementContext.Cell(refCoord, cell.state & ~FightMapMovementContext.CellState.Targeted, this.targetedEntity);
      }
      if (targeted != null)
      {
        if (targeted.view is IObjectTargetableByAction view)
        {
          view.ShowActionTargetFeedback(this.trackedCharacter.actionType, true);
          this.m_objectsTargetableByAction.Add(view);
        }
        Vector2Int refCoord = targeted.area.refCoord;
        int cellIndex = this.stateProvider.GetCellIndex(refCoord.x, refCoord.y);
        FightMapMovementContext.Cell cell = this.grid[cellIndex];
        this.grid[cellIndex] = new FightMapMovementContext.Cell(refCoord, cell.state | FightMapMovementContext.CellState.Targeted, targeted);
      }
      this.targetedEntity = targeted;
    }

    public void End()
    {
      foreach (IObjectTargetableByAction targetableByAction in this.m_objectsTargetableByAction)
        targetableByAction?.HideActionTargetFeedback();
      this.m_objectsTargetableByAction.Clear();
      this.m_hasEnded = true;
      this.trackedCharacter = (ICharacterEntity) null;
      this.targetedEntity = (IEntityWithBoardPresence) null;
    }

    public bool IsInActionRange(Vector2Int coord, ICharacterEntity tracked)
    {
      int num = tracked.area.MinDistanceWith(coord);
      if (tracked.hasRange)
        return num >= tracked.rangeMin && num <= tracked.rangeMax;
      if (num == 1)
        return true;
      IMapStateProvider stateProvider = this.stateProvider;
      Vector2Int sizeMin = stateProvider.sizeMin;
      Vector2Int sizeMax = stateProvider.sizeMax;
      int x1 = sizeMin.x;
      int y1 = sizeMin.y;
      int x2 = sizeMax.x;
      int y2 = sizeMax.y;
      Vector2Int vector2Int1 = new Vector2Int(coord.x, coord.y + 1);
      if (vector2Int1.x >= x1 && vector2Int1.x < x2 && vector2Int1.y >= y1 && vector2Int1.y < y2 && (this.grid[stateProvider.GetCellIndex(vector2Int1.x, vector2Int1.y)].state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable)
        return true;
      Vector2Int vector2Int2 = new Vector2Int(coord.x - 1, coord.y);
      if (vector2Int2.x >= x1 && vector2Int2.x < x2 && vector2Int2.y >= y1 && vector2Int2.y < y2 && (this.grid[stateProvider.GetCellIndex(vector2Int2.x, vector2Int2.y)].state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable)
        return true;
      Vector2Int vector2Int3 = new Vector2Int(coord.x + 1, coord.y);
      if (vector2Int3.x >= x1 && vector2Int3.x < x2 && vector2Int3.y >= y1 && vector2Int3.y < y2 && (this.grid[stateProvider.GetCellIndex(vector2Int3.x, vector2Int3.y)].state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable)
        return true;
      Vector2Int vector2Int4 = new Vector2Int(coord.x, coord.y - 1);
      return vector2Int4.x >= x1 && vector2Int4.x < x2 && vector2Int4.y >= y1 && vector2Int4.y < y2 && (this.grid[stateProvider.GetCellIndex(vector2Int4.x, vector2Int4.y)].state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable;
    }

    [Flags]
    public enum CellState
    {
      None = 0,
      Movement = 1,
      Reachable = 2,
      Occupied = 4,
      Targetable = 8,
      Targeted = 16, // 0x00000010
      Tracked = 32, // 0x00000020
    }

    public struct Cell
    {
      public readonly Vector2Int coords;
      public readonly FightMapMovementContext.CellState state;
      public readonly IEntityWithBoardPresence entity;

      public Cell(
        Vector2Int coords,
        FightMapMovementContext.CellState state,
        IEntityWithBoardPresence entity)
      {
        this.coords = coords;
        this.state = state;
        this.entity = entity;
      }
    }
  }
}
