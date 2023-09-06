// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightMapStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  public class FightMapStatus : IMapStateProvider
  {
    private readonly FightCellState[] m_cellStates;
    private readonly Vector2Int m_sizeMin;
    private readonly Vector2Int m_sizeMax;
    private readonly int m_width;
    private readonly int m_delta;

    public Vector2Int sizeMin => this.m_sizeMin;

    public Vector2Int sizeMax => this.m_sizeMax;

    public FightMapStatus(FightCellState[] cellStates, Vector2Int sizeMin, Vector2Int sizeMax)
    {
      this.m_cellStates = cellStates;
      this.m_sizeMin = sizeMin;
      this.m_sizeMax = sizeMax;
      this.m_width = sizeMax.x - sizeMin.x;
      this.m_delta = sizeMin.y * this.m_width + sizeMin.x;
    }

    public int GetCellIndex(int x, int y) => y * this.m_width + x - this.m_delta;

    public FightCellState GetCellState(int index) => this.m_cellStates[index];

    public FightCellState GetCellState(int x, int y) => this.m_cellStates[y * this.m_width + x - this.m_delta];

    public bool TryGetCellState(int index, out FightCellState fightCellState)
    {
      if (index < 0 || index >= this.m_cellStates.Length)
      {
        fightCellState = FightCellState.None;
        return false;
      }
      fightCellState = this.m_cellStates[index];
      return true;
    }

    public bool TryGetCellState(int x, int y, out FightCellState fightCellState)
    {
      Vector2Int sizeMin = this.m_sizeMin;
      Vector2Int sizeMax = this.m_sizeMax;
      if (x < sizeMin.x || y < sizeMin.y || x >= sizeMax.x || y >= sizeMax.y)
      {
        fightCellState = FightCellState.None;
        return false;
      }
      fightCellState = this.m_cellStates[y * this.m_width + x - this.m_delta];
      return true;
    }

    public IEnumerable<Coord> EnumerateCoords()
    {
      Vector2Int sizeMin = this.m_sizeMin;
      Vector2Int sizeMax = this.m_sizeMax;
      int xMin = sizeMin.x;
      int y1 = sizeMin.y;
      int xMax = sizeMax.x;
      int yMax = sizeMax.y;
      FightCellState[] cellStates = this.m_cellStates;
      int index = y1 * this.m_width + xMin - this.m_delta;
      for (int y = y1; y < yMax; ++y)
      {
        for (int x = xMin; x < xMax; ++x)
        {
          switch (cellStates[index])
          {
            case FightCellState.None:
              ++index;
              continue;
            case FightCellState.Movement:
              yield return new Coord(x, y);
              goto case FightCellState.None;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
      }
    }
  }
}
