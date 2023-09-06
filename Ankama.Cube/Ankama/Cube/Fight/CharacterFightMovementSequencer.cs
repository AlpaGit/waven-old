// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.CharacterFightMovementSequencer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  [UsedImplicitly]
  public static class CharacterFightMovementSequencer
  {
    public static IEnumerable<CharacterAnimationInfo> ComputeMovement(
      Vector2Int[] movementCells,
      DirectionAngle mapRotation)
    {
      int cellCount = movementCells.Length;
      if (cellCount > 1)
      {
        Vector2Int from = movementCells[0];
        for (int i = 1; i < cellCount; ++i)
        {
          Vector2Int cell = movementCells[i];
          Ankama.Cube.Data.Direction directionTo = from.GetDirectionTo(cell);
          if (!directionTo.IsAxisAligned())
            throw new Exception(string.Format("[ExecuteMovementToAttack] Invalid direction {0} going from {1} to {2}", (object) directionTo, (object) from, (object) cell));
          yield return new CharacterAnimationInfo((Vector2) cell, "run", "run", true, directionTo, mapRotation);
          from = cell;
          cell = new Vector2Int();
        }
      }
    }

    public static IEnumerable<CharacterAnimationInfo> ComputeMovementToAction(
      Vector2Int[] movementCells,
      Ankama.Cube.Data.Direction actionDirection,
      DirectionAngle mapRotation)
    {
      int length = movementCells.Length;
      if (length > 1)
      {
        Ankama.Cube.Data.Direction[] directions = new Ankama.Cube.Data.Direction[length - 1];
        Vector2Int startCell = movementCells[0];
        Vector2Int from = startCell;
        for (int index = 1; index < length; ++index)
        {
          Vector2Int movementCell = movementCells[index];
          Ankama.Cube.Data.Direction directionTo = from.GetDirectionTo(movementCell);
          directions[index - 1] = directionTo.IsAxisAligned() ? directionTo : throw new Exception(string.Format("[{0}] Invalid direction {1} going from {2} to {3}", (object) nameof (ComputeMovementToAction), (object) directionTo, (object) from, (object) movementCell));
          from = movementCell;
        }
        int consecutiveStraight = 0;
        int lastCellIndex = length - 1;
        Ankama.Cube.Data.Direction currentDirection = directions[0];
        yield return new CharacterAnimationInfo((Vector2) startCell, "dashantic", "dashantic", false, directions[0], mapRotation);
        Vector2Int cell;
        for (int i = 1; i < lastCellIndex; ++i)
        {
          Ankama.Cube.Data.Direction nextDirection = directions[i];
          if (nextDirection != currentDirection)
          {
            cell = movementCells[i];
            if (consecutiveStraight > 0)
            {
              Vector2Int vector2Int = i == consecutiveStraight ? startCell : movementCells[i - 1 - consecutiveStraight];
              yield return new CharacterAnimationInfo((Vector2) vector2Int + 0.5f * (Vector2) (cell - vector2Int), "dash", "dash", false, currentDirection, mapRotation);
            }
            if (consecutiveStraight > 1)
              yield return new CharacterAnimationInfo((Vector2) cell, "dashturn", "dashturn", false, currentDirection, nextDirection, mapRotation);
            else
              yield return new CharacterAnimationInfo((Vector2) cell, "dashzig", "dashzig", false, currentDirection, nextDirection, mapRotation);
            consecutiveStraight = 0;
            cell = new Vector2Int();
          }
          else
            ++consecutiveStraight;
          currentDirection = nextDirection;
        }
        if (consecutiveStraight > 0)
        {
          cell = movementCells[lastCellIndex];
          Vector2Int vector2Int = lastCellIndex == consecutiveStraight ? startCell : movementCells[lastCellIndex - 1 - consecutiveStraight];
          yield return new CharacterAnimationInfo((Vector2) vector2Int + 0.5f * (Vector2) (cell - vector2Int), "dash", "dash", false, currentDirection, mapRotation);
          if (!actionDirection.IsAxisAligned())
            actionDirection = actionDirection.GetAxisAligned(currentDirection);
          if (actionDirection != currentDirection)
            yield return new CharacterAnimationInfo((Vector2) cell, "dashturn", "dashturn", false, currentDirection, actionDirection, mapRotation);
          cell = new Vector2Int();
        }
      }
    }
  }
}
