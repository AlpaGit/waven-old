// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapPathFinding
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class MapPathFinding
  {
    private Dictionary<int, MapPathFinding.Node> m_steps = new Dictionary<int, MapPathFinding.Node>(16);
    private MapPathFinding.NodePriorityQueue m_frontier = new MapPathFinding.NodePriorityQueue(16);
    private MapPathFinding.AdjacentCoord[] m_adjacentCoordsBuffer = new MapPathFinding.AdjacentCoord[4];

    public bool FindPath(
      MapData data,
      Vector3 startWorldPos,
      Vector3 endWorldPos,
      List<Vector3> path)
    {
      Vector2Int localCoord1 = data.WorldToLocalCoord(startWorldPos);
      Vector2Int localCoord2 = data.WorldToLocalCoord(endWorldPos);
      if (!data.IsInsideLocal(localCoord1.x, localCoord1.y) || !data.IsInsideLocal(localCoord2.x, localCoord2.y))
      {
        path.Clear();
        return false;
      }
      int cellIndexLocal1 = data.GetCellIndexLocal(localCoord1.x, localCoord1.y);
      if (data.cells[cellIndexLocal1].state == MapData.CellState.NotWalkable)
      {
        path.Clear();
        return false;
      }
      int cellIndexLocal2 = data.GetCellIndexLocal(localCoord2.x, localCoord2.y);
      if (data.cells[cellIndexLocal2].state != MapData.CellState.NotWalkable)
        return this.ComputeFullPath(data, localCoord1, localCoord2, path);
      path.Clear();
      return false;
    }

    private bool ComputeFullPath(
      MapData data,
      Vector2Int start,
      Vector2Int end,
      List<Vector3> path)
    {
      MapPathFinding.NodePriorityQueue frontier = this.m_frontier;
      Dictionary<int, MapPathFinding.Node> steps = this.m_steps;
      MapPathFinding.AdjacentCoord[] adjacentCoordsBuffer = this.m_adjacentCoordsBuffer;
      frontier.Clear();
      steps.Clear();
      MapPathFinding.Node node1 = new MapPathFinding.Node(start, start, 0, 0);
      frontier.Enqueue(node1);
      while (frontier.Count() != 0)
      {
        MapPathFinding.Node lastNode = frontier.Dequeue();
        Vector2Int coords1 = lastNode.coords;
        Vector2Int fromCoords = lastNode.fromCoords;
        int cost1 = lastNode.cost;
        if (coords1 == end)
        {
          this.ReconstructPath(data, start, end, lastNode, path);
          return true;
        }
        this.ComputeAdjacentCoords(data, coords1, fromCoords);
        for (int index = 0; index < 4; ++index)
        {
          MapPathFinding.AdjacentCoord adjacentCoord = adjacentCoordsBuffer[index];
          if (adjacentCoord.isValid)
          {
            Vector2Int coords2 = adjacentCoord.coords;
            int cost2 = cost1 + 1;
            int num = coords2.DistanceTo(end);
            int cellIndexLocal = data.GetCellIndexLocal(coords2.x, coords2.y);
            MapPathFinding.Node node2;
            if (!steps.TryGetValue(cellIndexLocal, out node2) || node2.cost >= cost2)
            {
              node2 = new MapPathFinding.Node(coords2, coords1, cost2, cost2 + num);
              frontier.Enqueue(node2);
              steps[cellIndexLocal] = node2;
            }
          }
        }
      }
      return false;
    }

    private void ReconstructPath(
      MapData data,
      Vector2Int start,
      Vector2Int end,
      MapPathFinding.Node lastNode,
      List<Vector3> path)
    {
      int cost = lastNode.cost;
      Dictionary<int, MapPathFinding.Node> steps = this.m_steps;
      int count = path.Count;
      int index1 = cost + 1;
      if (path.Capacity < index1)
        path.Capacity = index1;
      if (count > index1)
        path.RemoveRange(index1, count - index1);
      else if (count < index1)
      {
        for (int index2 = count; index2 < index1; ++index2)
          path.Add(new Vector3(0.0f, 0.0f, 0.0f));
      }
      path[cost] = data.LocalToWorldCoord(end);
      for (int index3 = cost - 1; index3 > 0; --index3)
      {
        Vector2Int fromCoords = lastNode.fromCoords;
        int cellIndexLocal = data.GetCellIndexLocal(fromCoords.x, fromCoords.y);
        lastNode = steps[cellIndexLocal];
        path[index3] = data.LocalToWorldCoord(lastNode.coords);
      }
      path[0] = data.LocalToWorldCoord(start);
    }

    private void ComputeAdjacentCoords(MapData data, Vector2Int coords, Vector2Int from)
    {
      int x1 = coords.x;
      int y1 = coords.y;
      Vector2Int coords1 = new Vector2Int(x1, y1 + 1);
      Vector2Int coords2 = new Vector2Int(x1 - 1, y1);
      Vector2Int coords3 = new Vector2Int(x1 + 1, y1);
      Vector2Int coords4 = new Vector2Int(x1, y1 - 1);
      int x2 = coords1.x;
      int y2 = coords1.y;
      bool isValid1;
      if (coords1 != from && data.IsInsideLocal(x2, y2))
      {
        int cellIndexLocal = data.GetCellIndexLocal(x2, y2);
        isValid1 = data.cells[cellIndexLocal].state == MapData.CellState.Walkable;
      }
      else
        isValid1 = false;
      int x3 = coords2.x;
      int y3 = coords2.y;
      bool isValid2;
      if (coords2 != from && data.IsInsideLocal(x3, y3))
      {
        int cellIndexLocal = data.GetCellIndexLocal(x3, y3);
        isValid2 = data.cells[cellIndexLocal].state == MapData.CellState.Walkable;
      }
      else
        isValid2 = false;
      int x4 = coords3.x;
      int y4 = coords3.y;
      bool isValid3;
      if (coords3 != from && data.IsInsideLocal(x4, y4))
      {
        int cellIndexLocal = data.GetCellIndexLocal(x4, y4);
        isValid3 = data.cells[cellIndexLocal].state == MapData.CellState.Walkable;
      }
      else
        isValid3 = false;
      int x5 = coords4.x;
      int y5 = coords4.y;
      bool isValid4;
      if (coords4 != from && data.IsInsideLocal(x5, y5))
      {
        int cellIndexLocal = data.GetCellIndexLocal(x5, y5);
        isValid4 = data.cells[cellIndexLocal].state == MapData.CellState.Walkable;
      }
      else
        isValid4 = false;
      MapPathFinding.AdjacentCoord[] adjacentCoordsBuffer = this.m_adjacentCoordsBuffer;
      adjacentCoordsBuffer[0] = new MapPathFinding.AdjacentCoord(coords1, isValid1);
      adjacentCoordsBuffer[1] = new MapPathFinding.AdjacentCoord(coords2, isValid2);
      adjacentCoordsBuffer[2] = new MapPathFinding.AdjacentCoord(coords3, isValid3);
      adjacentCoordsBuffer[3] = new MapPathFinding.AdjacentCoord(coords4, isValid4);
    }

    private struct Node
    {
      public Vector2Int coords;
      public Vector2Int fromCoords;
      public int cost;
      public int priority;

      public Node(Vector2Int coord, Vector2Int fromCoord, int cost, int priority)
      {
        this.coords = coord;
        this.fromCoords = fromCoord;
        this.cost = cost;
        this.priority = priority;
      }
    }

    private struct AdjacentCoord
    {
      public readonly Vector2Int coords;
      public readonly bool isValid;

      public AdjacentCoord(Vector2Int coords, bool isValid)
      {
        this.coords = coords;
        this.isValid = isValid;
      }
    }

    private class NodePriorityQueue : IComparer<MapPathFinding.Node>
    {
      private readonly List<MapPathFinding.Node> m_data;

      public NodePriorityQueue(int capacity) => this.m_data = new List<MapPathFinding.Node>(capacity);

      public void Enqueue(MapPathFinding.Node item)
      {
        List<MapPathFinding.Node> data = this.m_data;
        data.Add(item);
        int index1;
        for (int index2 = data.Count - 1; index2 > 0; index2 = index1)
        {
          index1 = (index2 - 1) / 2;
          if (this.Compare(data[index2], data[index1]) >= 0)
            break;
          MapPathFinding.Node node = data[index2];
          data[index2] = data[index1];
          data[index1] = node;
        }
      }

      public MapPathFinding.Node Dequeue()
      {
        List<MapPathFinding.Node> data = this.m_data;
        int index1 = data.Count - 1;
        MapPathFinding.Node node1 = data[0];
        data[0] = data[index1];
        data.RemoveAt(index1);
        int num = index1 - 1;
        int index2 = 0;
        while (true)
        {
          int index3 = index2 * 2 + 1;
          if (index3 <= num)
          {
            int index4 = index3 + 1;
            if (index4 <= num && this.Compare(data[index4], data[index3]) < 0)
              index3 = index4;
            if (this.Compare(data[index2], data[index3]) > 0)
            {
              MapPathFinding.Node node2 = data[index2];
              data[index2] = data[index3];
              data[index3] = node2;
              index2 = index3;
            }
            else
              break;
          }
          else
            break;
        }
        return node1;
      }

      public int Count() => this.m_data.Count;

      public void Clear() => this.m_data.Clear();

      public int Compare(MapPathFinding.Node x, MapPathFinding.Node y)
      {
        int priority1 = x.priority;
        int priority2 = y.priority;
        if (priority1 < priority2)
          return -1;
        return priority1 > priority2 ? 1 : y.cost.CompareTo(x.cost);
      }
    }
  }
}
