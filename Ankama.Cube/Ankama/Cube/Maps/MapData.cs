// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [DisallowMultipleComponent]
  public class MapData : MonoBehaviour
  {
    [SerializeField]
    [HideInInspector]
    private Vector2Int m_origin;
    [SerializeField]
    [HideInInspector]
    private Vector2Int m_size;
    [SerializeField]
    [HideInInspector]
    private int m_height;
    [SerializeField]
    [HideInInspector]
    private MapData.CellData[] m_cells = new MapData.CellData[0];
    [SerializeField]
    [HideInInspector]
    private MapQuadTree m_quadTree;
    private static List<MapData> s_list = new List<MapData>();

    public static MapData GetMapFromWorldPos(Vector3 worldPos)
    {
      for (int index = 0; index < MapData.s_list.Count; ++index)
      {
        MapData mapFromWorldPos = MapData.s_list[index];
        if (mapFromWorldPos.quadTree != null && mapFromWorldPos.quadTree.rootNode.IsInside(new Vector2(worldPos.x, worldPos.z)))
          return mapFromWorldPos;
      }
      return (MapData) null;
    }

    public Vector2Int min => this.m_origin;

    public Vector2Int max => this.m_origin * this.m_size;

    public Vector3 center => new Vector3((float) ((double) this.m_origin.x - 0.5 + (double) this.m_size.x * 0.5), (float) this.m_height, (float) ((double) this.m_origin.y - 0.5 + (double) this.m_size.y * 0.5));

    public float height => (float) this.m_height;

    public Vector2Int size => this.m_size;

    public MapData.CellData[] cells => this.m_cells;

    public MapQuadTree quadTree => this.m_quadTree;

    private void OnEnable() => MapData.s_list.Add(this);

    private void OnDisable() => MapData.s_list.Remove(this);

    public bool RayCast(Ray ray, out Vector3 hit)
    {
      float enter;
      if (new Plane(Vector3.up, this.height).Raycast(ray, out enter))
      {
        hit = ray.GetPoint(enter);
        return true;
      }
      hit = Vector3.zero;
      return false;
    }

    public void SetCell(MapData.CellData cell, Vector2Int localCoord) => this.m_cells[this.GetCellIndexLocal(localCoord.x, localCoord.y)] = cell;

    public bool TryGetCell(
      Vector2Int worldCoord,
      out MapData.CellData cell,
      out Vector2Int localCoord)
    {
      localCoord = worldCoord - this.m_origin;
      int x = localCoord.x;
      int y = localCoord.y;
      if (x < 0 || x >= this.m_size.x || y < 0 || y >= this.m_size.y)
      {
        cell = (MapData.CellData) null;
        return false;
      }
      cell = this.m_cells[this.GetCellIndexLocal(x, y)];
      return true;
    }

    public Vector2Int WorldToLocalCoord(Vector3 worldPos) => new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z)) - this.m_origin;

    public Vector3 TwoDToThreeDWorldCoord(Vector2 worldCoord) => new Vector3(worldCoord.x, (float) this.m_height, worldCoord.y);

    public Vector3 LocalToWorldCoord(Vector2Int localCoord)
    {
      Vector2Int vector2Int = this.m_origin + localCoord;
      return new Vector3((float) vector2Int.x, (float) this.m_height, (float) vector2Int.y);
    }

    public int GetCellIndexLocal(int x, int y) => x + this.m_size.x * y;

    public bool IsInsideLocal(int x, int y) => x >= 0 && x < this.m_size.x && y >= 0 && y < this.m_size.y;

    public bool IsAreaFullOfState(Vector2Int start, Vector2Int end, MapData.CellState state)
    {
      if (!this.IsInsideLocal(start.x, start.y) || !this.IsInsideLocal(end.x, end.y))
        return false;
      for (int x = start.x; x < end.x + 1; ++x)
      {
        for (int y = start.y; y < end.y + 1; ++y)
        {
          if (this.m_cells[this.GetCellIndexLocal(x, y)].state != state)
            return false;
        }
      }
      return true;
    }

    public void Clear()
    {
      this.m_size = Vector2Int.zero;
      this.m_cells = new MapData.CellData[0];
    }

    public void Init(Vector2Int origin, Vector2Int size)
    {
      this.m_origin = origin;
      this.m_size = size;
      this.m_cells = new MapData.CellData[size.x * size.y];
    }

    public void Resize(Vector2Int start, Vector2Int end)
    {
      Vector2Int vector2Int1 = this.m_origin + start;
      Vector2Int vector2Int2 = end + Vector2Int.one - start;
      MapData.CellData[] destinationArray = new MapData.CellData[vector2Int2.x * vector2Int2.y];
      Vector2Int vector2Int3 = Vector2Int.Max(start, Vector2Int.zero);
      Vector2Int vector2Int4 = Vector2Int.Min(end, this.m_size - Vector2Int.one) + Vector2Int.one - vector2Int3;
      Vector2Int vector2Int5 = Vector2Int.Max(this.m_origin - vector2Int1, Vector2Int.zero);
      for (int index = 0; index < vector2Int4.y; ++index)
        Array.Copy((Array) this.m_cells, vector2Int3.x + (index + vector2Int3.y) * this.m_size.x, (Array) destinationArray, vector2Int5.x + (index + vector2Int5.y) * vector2Int2.x, vector2Int4.x);
      this.m_origin = vector2Int1;
      this.m_size = vector2Int2;
      this.m_cells = destinationArray;
    }

    public void GenerateNodeQuadTree()
    {
      int num = Mathf.Max(Mathf.NextPowerOfTwo(this.m_size.x), Mathf.NextPowerOfTwo(this.m_size.y));
      MapQuadTree.Node nodes = this.GenerateNodes(this.m_cells, Vector2Int.zero, new Vector2Int(num - 1, num - 1));
      this.m_quadTree.SetNodes(nodes);
      this.GenerateNodeConnections((MapQuadTree.Node) null, nodes);
    }

    private void GenerateNodeConnections(MapQuadTree.Node parentNode, MapQuadTree.Node node)
    {
      if (node == null)
        return;
      if (node.hasNoChildren)
      {
        List<MapQuadTree.Node> neighbours = this.GetNeighbours(node);
        for (int index = 0; index < neighbours.Count; ++index)
        {
          MapQuadTree.Node node1 = neighbours[index];
          if (node.connectedNodes == null || !node.connectedNodes.Contains(node1))
          {
            if (node.connectedNodes == null)
              node.connectedNodes = new List<MapQuadTree.Node>();
            node.connectedNodes.Add(node1);
          }
          if (node1.connectedNodes == null || !node1.connectedNodes.Contains(node))
          {
            if (node1.connectedNodes == null)
              node1.connectedNodes = new List<MapQuadTree.Node>();
            node1.connectedNodes.Add(node);
          }
        }
      }
      this.GenerateNodeConnections(node, node.topLeft);
      this.GenerateNodeConnections(node, node.topRight);
      this.GenerateNodeConnections(node, node.bottomLeft);
      this.GenerateNodeConnections(node, node.bottomRight);
    }

    private List<MapQuadTree.Node> GetNeighbours(MapQuadTree.Node node)
    {
      List<MapQuadTree.Node> neighbours = new List<MapQuadTree.Node>();
      List<Vector2> allCellAroudNode = this.GetAllCellAroudNode(node);
      for (int index = 0; index < allCellAroudNode.Count; ++index)
      {
        MapQuadTree.Node nodeAt = this.quadTree.GetNodeAt(allCellAroudNode[index]);
        if (nodeAt != null && !neighbours.Contains(nodeAt))
          neighbours.Add(nodeAt);
      }
      return neighbours;
    }

    private List<Vector2> GetAllCellAroudNode(MapQuadTree.Node node)
    {
      List<Vector2> allCellAroudNode = new List<Vector2>();
      Vector2 vector2_1 = node.size * 0.5f;
      Vector2 vector2_2 = node.min - Vector2.one * 0.5f;
      Vector2 vector2_3 = node.max + Vector2.one * 0.5f;
      Vector2Int vector2Int1 = vector2_2.RoundToInt();
      Vector2Int vector2Int2 = vector2_3.RoundToInt();
      Vector2Int vector2Int3 = vector2Int2 + Vector2Int.one - vector2Int1;
      for (int x = vector2Int1.x + 1; x < vector2Int2.x; ++x)
      {
        allCellAroudNode.Add(new Vector2((float) x, (float) vector2Int1.y));
        if (vector2Int3.y > 1)
          allCellAroudNode.Add(new Vector2((float) x, (float) vector2Int2.y));
      }
      for (int y = vector2Int1.y + 1; y < vector2Int2.y; ++y)
      {
        allCellAroudNode.Add(new Vector2((float) vector2Int1.x, (float) y));
        if (vector2Int3.x > 1)
          allCellAroudNode.Add(new Vector2((float) vector2Int2.x, (float) y));
      }
      return allCellAroudNode;
    }

    private MapQuadTree.Node GenerateNodes(
      MapData.CellData[] cells,
      Vector2Int start,
      Vector2Int end)
    {
      Vector2Int vector2Int1 = end + Vector2Int.one - start;
      MapQuadTree.Node nodes = new MapQuadTree.Node();
      nodes.min = (Vector2) (this.m_origin + start) - Vector2.one * 0.5f;
      nodes.max = (Vector2) (this.m_origin + end) + Vector2.one * 0.5f;
      int num1 = vector2Int1.x * vector2Int1.y;
      int num2 = 0;
      for (int x = start.x; x < end.x + 1; ++x)
      {
        for (int y = start.y; y < end.y + 1; ++y)
        {
          if (this.IsInsideLocal(x, y) && cells[this.GetCellIndexLocal(x, y)].state == MapData.CellState.Walkable)
            ++num2;
        }
      }
      if (num2 == 0)
        return (MapQuadTree.Node) null;
      if (num2 == num1)
        return nodes;
      if (start == end)
        return (MapQuadTree.Node) null;
      nodes.height = this.height;
      Vector2Int vector2Int2 = (nodes.size * 0.5f).RoundToInt();
      Vector2Int vector2Int3 = vector2Int2 - Vector2Int.one;
      Vector2Int start1 = start + new Vector2Int(0, vector2Int2.y);
      nodes.topLeft = this.GenerateNodes(cells, start1, start1 + vector2Int3);
      Vector2Int start2 = start + vector2Int2;
      nodes.topRight = this.GenerateNodes(cells, start2, start2 + vector2Int3);
      Vector2Int start3 = start;
      nodes.bottomLeft = this.GenerateNodes(cells, start3, start3 + vector2Int3);
      Vector2Int start4 = start + new Vector2Int(vector2Int2.x, 0);
      nodes.bottomRight = this.GenerateNodes(cells, start4, start4 + vector2Int3);
      return nodes;
    }

    public enum CellState
    {
      Walkable,
      NotWalkable,
    }

    [Serializable]
    public class CellData
    {
      [SerializeField]
      public MapData.CellState state = MapData.CellState.NotWalkable;
    }
  }
}
