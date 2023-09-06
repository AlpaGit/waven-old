// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightMapDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class FightMapDefinition : EditableData, IMapDefinition
  {
    [SerializeField]
    private FightMapConfiguration m_configuration;
    [SerializeField]
    private Vector3Int m_origin;
    [SerializeField]
    private Vector2Int m_sizeMin;
    [SerializeField]
    private Vector2Int m_sizeMax;
    [SerializeField]
    private FightCellState[] m_cellStates;
    [SerializeField]
    private FightMapRegionDefinition[] m_regions;

    public FightMapConfiguration configuration => this.m_configuration;

    public Vector3Int origin => this.m_origin;

    public Vector2Int sizeMin => this.m_sizeMin;

    public Vector2Int sizeMax => this.m_sizeMax;

    public FightCellState[] cellStates => this.m_cellStates;

    public FightMapRegionDefinition[] regions => this.m_regions;

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public int regionCount => this.m_regions.Length;

    public FightMapRegionDefinition GetRegion(int index) => this.m_regions[index];

    public int GetCellIndex(int x, int y)
    {
      int num1 = this.m_sizeMax.x - this.m_sizeMin.x;
      int num2 = this.m_sizeMin.y * num1 + this.m_sizeMin.x;
      return y * num1 + x - num2;
    }

    public Vector2Int GetCellCoords(int index)
    {
      int num1 = this.m_sizeMax.x - this.m_sizeMin.x;
      int num2 = this.m_sizeMin.y * num1 + this.m_sizeMin.x;
      int x = this.m_sizeMin.x + index % num1;
      int y = (index + num2 - x) / num1;
      return new Vector2Int(x, y);
    }

    public FightMapStatus CreateFightMapStatus(int regionIndex)
    {
      FightMapRegionDefinition region = this.m_regions[regionIndex];
      Vector2Int sizeMin = region.sizeMin;
      Vector2Int sizeMax = region.sizeMax;
      Vector2Int vector2Int = sizeMax - sizeMin;
      int length = vector2Int.x * vector2Int.y;
      int num1 = this.m_sizeMax.x - this.m_sizeMin.x;
      int num2 = this.m_sizeMin.y * num1 + this.m_sizeMin.x;
      int num3 = sizeMax.x - sizeMin.x;
      int num4 = sizeMin.y * num3 + sizeMin.x;
      FightCellState[] cellStates1 = this.m_cellStates;
      FightCellState[] cellStates2 = new FightCellState[length];
      for (int y = sizeMin.y; y < sizeMax.y; ++y)
      {
        for (int x = sizeMin.x; x < sizeMax.x; ++x)
        {
          int index1 = y * num1 + x - num2;
          int index2 = y * num3 + x - num4;
          cellStates2[index2] = cellStates1[index1];
        }
      }
      return new FightMapStatus(cellStates2, sizeMin, sizeMax);
    }
  }
}
