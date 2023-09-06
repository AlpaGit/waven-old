// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapVirtualGrid
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class MapVirtualGrid
  {
    private readonly IMapDefinition m_mapDefinition;
    private readonly MapVirtualGrid.Cell[] m_virtualCells;

    public MapVirtualGrid(IMapDefinition mapDefinition, CellObject[] cells)
    {
      this.m_mapDefinition = mapDefinition;
      Vector2Int vector2Int = mapDefinition.sizeMax - mapDefinition.sizeMin;
      this.m_virtualCells = new MapVirtualGrid.Cell[vector2Int.x * vector2Int.y];
      int length = cells.Length;
      for (int index = 0; index < length; ++index)
      {
        CellObject cell = cells[index];
        Vector2Int coords = cell.coords;
        this.m_virtualCells[mapDefinition.GetCellIndex(coords.x, coords.y)] = new MapVirtualGrid.Cell(cell);
      }
    }

    public bool IsReferenceCell(Vector2Int coords)
    {
      Area area = this.m_virtualCells[this.m_mapDefinition.GetCellIndex(coords.x, coords.y)].area;
      return area == null || area.refCoord == coords;
    }

    public CellObject GetReferenceCell(Vector2Int coords)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      MapVirtualGrid.Cell virtualCell = this.m_virtualCells[mapDefinition.GetCellIndex(coords.x, coords.y)];
      Area area = virtualCell.area;
      if (area == null)
        return virtualCell.cellObject;
      Vector2Int refCoord = area.refCoord;
      return this.m_virtualCells[mapDefinition.GetCellIndex(refCoord.x, refCoord.y)].cellObject;
    }

    public bool TryGetReferenceCell(Vector2Int coords, out CellObject referenceCell)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      MapVirtualGrid.Cell virtualCell = this.m_virtualCells[mapDefinition.GetCellIndex(coords.x, coords.y)];
      Area area = virtualCell.area;
      if (area == null)
      {
        referenceCell = virtualCell.cellObject;
        return true;
      }
      Vector2Int refCoord = area.refCoord;
      if (refCoord == coords)
      {
        int cellIndex = mapDefinition.GetCellIndex(refCoord.x, refCoord.y);
        referenceCell = this.m_virtualCells[cellIndex].cellObject;
        return true;
      }
      referenceCell = (CellObject) null;
      return false;
    }

    public void GetReferenceCellsNoAlloc(List<CellObject> outCells)
    {
      int length = this.m_virtualCells.Length;
      for (int index = 0; index < length; ++index)
      {
        MapVirtualGrid.Cell virtualCell = this.m_virtualCells[index];
        CellObject cellObject = virtualCell.cellObject;
        if (!((Object) null == (Object) cellObject))
        {
          Area area = virtualCell.area;
          if (area == null || area.refCoord == cellObject.coords)
            outCells.Add(cellObject);
        }
      }
    }

    public void GetAreaCellsNoAlloc(Vector2Int coords, List<CellObject> outCells)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      MapVirtualGrid.Cell virtualCell = this.m_virtualCells[mapDefinition.GetCellIndex(coords.x, coords.y)];
      Area area = virtualCell.area;
      if (area == null)
      {
        CellObject cellObject = virtualCell.cellObject;
        if (!((Object) null != (Object) cellObject))
          return;
        outCells.Add(cellObject);
      }
      else
      {
        Vector2Int[] occupiedCoords = area.occupiedCoords;
        int length = occupiedCoords.Length;
        for (int index = 0; index < length; ++index)
        {
          Vector2Int vector2Int = occupiedCoords[index];
          int cellIndex = mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y);
          outCells.Add(this.m_virtualCells[cellIndex].cellObject);
        }
      }
    }

    public void GetLinkedCellsNoAlloc(Vector2Int coords, List<CellObject> outCells)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Area area = this.m_virtualCells[mapDefinition.GetCellIndex(coords.x, coords.y)].area;
      if (area == null || area.refCoord != coords)
        return;
      Vector2Int[] occupiedCoords = area.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Vector2Int vector2Int = occupiedCoords[index];
        if (!(vector2Int == coords))
        {
          int cellIndex = mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y);
          outCells.Add(this.m_virtualCells[cellIndex].cellObject);
        }
      }
    }

    public void AddArea([NotNull] Area area)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2Int[] occupiedCoords = area.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Vector2Int vector2Int = occupiedCoords[index];
        this.m_virtualCells[mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y)].area = area;
      }
    }

    public void MoveArea([NotNull] Area from, [NotNull] Area to)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2Int[] occupiedCoords1 = from.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      for (int index = 0; index < length1; ++index)
      {
        Vector2Int vector2Int = occupiedCoords1[index];
        this.m_virtualCells[mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y)].area = (Area) null;
      }
      Vector2Int[] occupiedCoords2 = to.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      for (int index = 0; index < length2; ++index)
      {
        Vector2Int vector2Int = occupiedCoords2[index];
        this.m_virtualCells[mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y)].area = to;
      }
    }

    public void RemoveArea([NotNull] Area area)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2Int[] occupiedCoords = area.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Vector2Int vector2Int = occupiedCoords[index];
        this.m_virtualCells[mapDefinition.GetCellIndex(vector2Int.x, vector2Int.y)].area = (Area) null;
      }
    }

    private struct Cell
    {
      public readonly CellObject cellObject;
      public Area area;

      public Cell(CellObject cellObject)
      {
        this.cellObject = cellObject;
        this.area = (Area) null;
      }
    }
  }
}
