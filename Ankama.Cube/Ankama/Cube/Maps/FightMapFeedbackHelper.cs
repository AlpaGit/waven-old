// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapFeedbackHelper
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps.Feedbacks;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public static class FightMapFeedbackHelper
  {
    public static void SetupMovementAreaHighlight(
      [NotNull] FightMapFeedbackResources resources,
      [NotNull] FightMapMovementContext context,
      Vector2Int coords,
      [NotNull] CellHighlight highlight,
      Color color)
    {
      if ((context.GetCell(coords).state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) != FightMapMovementContext.CellState.Reachable)
      {
        highlight.ClearSprite();
      }
      else
      {
        IMapStateProvider stateProvider = context.stateProvider;
        Vector2Int sizeMin = stateProvider.sizeMin;
        Vector2Int sizeMax = stateProvider.sizeMax;
        FightMapMovementContext.Cell[] grid = context.grid;
        Sprite[] areaFeedbackSprites = resources.areaFeedbackSprites;
        Vector2Int vector2Int1 = new Vector2Int(coords.x, coords.y + 1);
        Vector2Int vector2Int2 = new Vector2Int(coords.x - 1, coords.y);
        Vector2Int vector2Int3 = new Vector2Int(coords.x + 1, coords.y);
        Vector2Int vector2Int4 = new Vector2Int(coords.x, coords.y - 1);
        FightMapMovementContext.CellState cellState1 = vector2Int1.x < sizeMin.x || vector2Int1.x >= sizeMax.x || vector2Int1.y < sizeMin.y || vector2Int1.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int1.x, vector2Int1.y)].state;
        FightMapMovementContext.CellState cellState2 = vector2Int2.x < sizeMin.x || vector2Int2.x >= sizeMax.x || vector2Int2.y < sizeMin.y || vector2Int2.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int2.x, vector2Int2.y)].state;
        FightMapMovementContext.CellState cellState3 = vector2Int3.x < sizeMin.x || vector2Int3.x >= sizeMax.x || vector2Int3.y < sizeMin.y || vector2Int3.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int3.x, vector2Int3.y)].state;
        int state1 = vector2Int4.x < sizeMin.x || vector2Int4.x >= sizeMax.x || vector2Int4.y < sizeMin.y || vector2Int4.y >= sizeMax.y ? 0 : (int) grid[stateProvider.GetCellIndex(vector2Int4.x, vector2Int4.y)].state;
        int num1 = (cellState1 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 1 : 0;
        int num2 = (cellState2 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 1 : 0;
        int num3 = (cellState3 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 1 : 0;
        int num4 = (state1 & 6) == 2 ? 1 : 0;
        switch (4 - (num1 + num2 + num3 + num4))
        {
          case 0:
          case 1:
          case 2:
            Vector2Int vector2Int5 = new Vector2Int(coords.x - 1, coords.y + 1);
            Vector2Int vector2Int6 = new Vector2Int(coords.x + 1, coords.y + 1);
            Vector2Int vector2Int7 = new Vector2Int(coords.x - 1, coords.y - 1);
            Vector2Int vector2Int8 = new Vector2Int(coords.x + 1, coords.y - 1);
            FightMapMovementContext.CellState cellState4 = vector2Int5.x < sizeMin.x || vector2Int5.x >= sizeMax.x || vector2Int5.y < sizeMin.y || vector2Int5.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int5.x, vector2Int5.y)].state;
            FightMapMovementContext.CellState cellState5 = vector2Int6.x < sizeMin.x || vector2Int6.x >= sizeMax.x || vector2Int6.y < sizeMin.y || vector2Int6.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int6.x, vector2Int6.y)].state;
            FightMapMovementContext.CellState cellState6 = vector2Int7.x < sizeMin.x || vector2Int7.x >= sizeMax.x || vector2Int7.y < sizeMin.y || vector2Int7.y >= sizeMax.y ? FightMapMovementContext.CellState.None : grid[stateProvider.GetCellIndex(vector2Int7.x, vector2Int7.y)].state;
            int state2 = vector2Int8.x < sizeMin.x || vector2Int8.x >= sizeMax.x || vector2Int8.y < sizeMin.y || vector2Int8.y >= sizeMax.y ? 0 : (int) grid[stateProvider.GetCellIndex(vector2Int8.x, vector2Int8.y)].state;
            int num5 = (cellState4 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 0 : 1;
            int num6 = (cellState5 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 0 : 1;
            int num7 = (cellState6 & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable ? 0 : 1;
            int num8 = (state2 & 6) == 2 ? 0 : 1;
            int num9 = 1 - num1;
            int num10 = 1 - num2;
            int num11 = 1 - num3;
            int num12 = 1 - num4;
            int num13 = num9 | num10 | num5 * num1 * num2;
            int num14 = num9 | num11 | num6 * num1 * num3;
            int num15 = num12 | num10 | num7 * num4 * num2;
            int num16 = num12 | num11 | num8 * num4 * num3;
            int num17 = num14 << 1;
            FightMapFeedbackHelper.Compute(num13 | num17 | num15 << 2 | num16 << 3 | num9 << 4 | num10 << 5 | num11 << 6 | num12 << 7, areaFeedbackSprites, highlight, color);
            break;
          case 3:
            Sprite sprite1 = areaFeedbackSprites[4];
            float angle = (float) ((double) num1 * -90.0 + (double) num3 * 180.0 + (double) num4 * 90.0);
            highlight.SetSprite(sprite1, color, angle);
            break;
          case 4:
            Sprite sprite2 = areaFeedbackSprites[5];
            highlight.SetSprite(sprite2, color);
            break;
          default:
            throw new ArgumentException();
        }
      }
    }

    public static void SetupSpellTargetHighlight(
      [NotNull] FightMapFeedbackResources resources,
      [NotNull] FightMapTargetContext context,
      Vector2Int coords,
      [NotNull] CellHighlight highlight,
      Color color)
    {
      if (!context.HasNonEntityTargetAt(coords))
      {
        highlight.ClearSprite();
      }
      else
      {
        Sprite areaFeedbackSprite = resources.areaFeedbackSprites[15];
        highlight.SetSprite(areaFeedbackSprite, color);
      }
    }

    private static void Compute(
      int bitSet,
      Sprite[] sprites,
      CellHighlight highlight,
      Color color)
    {
      Sprite sprite;
      float angle;
      switch (bitSet)
      {
        case 0:
          sprite = sprites[0];
          angle = 0.0f;
          break;
        case 1:
          sprite = sprites[6];
          angle = 0.0f;
          break;
        case 2:
          sprite = sprites[6];
          angle = -90f;
          break;
        case 3:
          sprite = sprites[7];
          angle = 0.0f;
          break;
        case 4:
          sprite = sprites[6];
          angle = 90f;
          break;
        case 5:
          sprite = sprites[7];
          angle = 90f;
          break;
        case 6:
          sprite = sprites[8];
          angle = 0.0f;
          break;
        case 7:
          sprite = sprites[9];
          angle = 0.0f;
          break;
        case 8:
          sprite = sprites[6];
          angle = 180f;
          break;
        case 9:
          sprite = sprites[8];
          angle = 90f;
          break;
        case 10:
          sprite = sprites[7];
          angle = -90f;
          break;
        case 11:
          sprite = sprites[9];
          angle = -90f;
          break;
        case 12:
          sprite = sprites[7];
          angle = 180f;
          break;
        case 13:
          sprite = sprites[9];
          angle = 90f;
          break;
        case 14:
          sprite = sprites[9];
          angle = 180f;
          break;
        case 15:
          sprite = sprites[10];
          angle = 0.0f;
          break;
        case 19:
          sprite = sprites[1];
          angle = 0.0f;
          break;
        case 23:
          sprite = sprites[12];
          angle = 0.0f;
          break;
        case 27:
          sprite = sprites[11];
          angle = 0.0f;
          break;
        case 31:
          sprite = sprites[13];
          angle = 0.0f;
          break;
        case 37:
          sprite = sprites[1];
          angle = 90f;
          break;
        case 39:
          sprite = sprites[11];
          angle = 90f;
          break;
        case 45:
          sprite = sprites[12];
          angle = 90f;
          break;
        case 47:
          sprite = sprites[13];
          angle = 90f;
          break;
        case 55:
          sprite = sprites[3];
          angle = 0.0f;
          break;
        case 63:
          sprite = sprites[14];
          angle = 0.0f;
          break;
        case 74:
          sprite = sprites[1];
          angle = -90f;
          break;
        case 75:
          sprite = sprites[12];
          angle = -90f;
          break;
        case 78:
          sprite = sprites[11];
          angle = -90f;
          break;
        case 79:
          sprite = sprites[13];
          angle = -90f;
          break;
        case 91:
          sprite = sprites[3];
          angle = -90f;
          break;
        case 95:
          sprite = sprites[14];
          angle = -90f;
          break;
        case 111:
          sprite = sprites[2];
          angle = 0.0f;
          break;
        case 140:
          sprite = sprites[1];
          angle = 180f;
          break;
        case 141:
          sprite = sprites[11];
          angle = 180f;
          break;
        case 142:
          sprite = sprites[12];
          angle = 180f;
          break;
        case 143:
          sprite = sprites[13];
          angle = 180f;
          break;
        case 159:
          sprite = sprites[2];
          angle = 90f;
          break;
        case 173:
          sprite = sprites[3];
          angle = 90f;
          break;
        case 175:
          sprite = sprites[14];
          angle = 90f;
          break;
        case 206:
          sprite = sprites[3];
          angle = 180f;
          break;
        case 207:
          sprite = sprites[14];
          angle = 180f;
          break;
        default:
          throw new ArgumentException(string.Format("[{0}] Impossible configuration: {1}", (object) "SetupMovementAreaHighlight", (object) bitSet));
      }
      highlight.SetSprite(sprite, color, angle);
    }
  }
}
