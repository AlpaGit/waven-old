// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapQuadTreePathfinding
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class MapQuadTreePathfinding
  {
    private Dictionary<MapQuadTree.Node, MapQuadTreePathfinding.Node> m_steps = new Dictionary<MapQuadTree.Node, MapQuadTreePathfinding.Node>(16);
    private MapQuadTreePathfinding.NodePriorityQueue m_frontier = new MapQuadTreePathfinding.NodePriorityQueue(16);

    public bool FindPath(
      MapData data,
      Vector3 startWorldPos,
      Vector3 endWorldPos,
      List<Vector3> path)
    {
      Vector2 vector2 = new Vector2(startWorldPos.x, startWorldPos.z);
      Vector2 coords = new Vector2((float) Mathf.RoundToInt(endWorldPos.x), (float) Mathf.RoundToInt(endWorldPos.z));
      MapQuadTree.Node nodeAt = data.quadTree.GetNodeAt(vector2);
      if (nodeAt == null)
        return false;
      MapQuadTree.Node node = data.quadTree.GetNodeAt(coords);
      return (node != null || data.quadTree.TryGetClosestCell(new Vector2(endWorldPos.x, endWorldPos.z), out node, out coords)) && this.ComputeFullPath(nodeAt, node, vector2, coords, path);
    }

    private bool ComputeFullPath(
      MapQuadTree.Node startQuadTreeNode,
      MapQuadTree.Node endQuadTreeNode,
      Vector2 start,
      Vector2 end,
      List<Vector3> path)
    {
      MapQuadTreePathfinding.NodePriorityQueue frontier = this.m_frontier;
      Dictionary<MapQuadTree.Node, MapQuadTreePathfinding.Node> steps = this.m_steps;
      frontier.Clear();
      steps.Clear();
      start.RoundToInt();
      MapQuadTreePathfinding.Node node1 = new MapQuadTreePathfinding.Node(start, start, 0.0f, 0.0f, startQuadTreeNode, (MapQuadTree.Node) null);
      frontier.Enqueue(node1);
      steps[startQuadTreeNode] = node1;
      while (frontier.Count() != 0)
      {
        MapQuadTreePathfinding.Node lastNode = frontier.Dequeue();
        Vector2 coords = lastNode.coords;
        float cost1 = lastNode.cost;
        MapQuadTree.Node quadTreeNode = lastNode.quadTreeNode;
        MapQuadTree.Node fromQuadTreeNode = lastNode.fromQuadTreeNode;
        List<MapQuadTree.Node> connectedNodes = lastNode.quadTreeNode.connectedNodes;
        if (quadTreeNode == endQuadTreeNode)
        {
          this.ReconstructPath(end, lastNode, path);
          return true;
        }
        if (connectedNodes != null)
        {
          int count = connectedNodes.Count;
          for (int index = 0; index < count; ++index)
          {
            MapQuadTree.Node node2 = connectedNodes[index];
            if (node2 != fromQuadTreeNode)
            {
              Vector2 cell = (Vector2) node2.ClampPositionToCell(coords);
              float cost2 = cost1 + coords.DistanceTo(cell);
              float num = cell.DistanceTo(end);
              MapQuadTreePathfinding.Node node3;
              if (!steps.TryGetValue(node2, out node3) || (double) node3.cost >= (double) cost2)
              {
                node3 = new MapQuadTreePathfinding.Node(cell, coords, cost2, cost2 + num, node2, quadTreeNode);
                frontier.Enqueue(node3);
                steps[node2] = node3;
              }
            }
          }
        }
      }
      return false;
    }

    private void ReconstructPath(
      Vector2 end,
      MapQuadTreePathfinding.Node lastNode,
      List<Vector3> path)
    {
      path.Clear();
      float height1 = lastNode.quadTreeNode.height;
      path.Insert(0, new Vector3(end.x, height1, end.y));
      bool flag1 = this.AreDifferent(lastNode.coords.x, end.x);
      bool flag2 = this.AreDifferent(lastNode.coords.y, end.y);
      if (flag1 | flag2)
      {
        if (flag1 & flag2)
          path.Insert(0, new Vector3(end.x, height1, lastNode.coords.y));
        path.Insert(0, new Vector3(lastNode.coords.x, height1, lastNode.coords.y));
      }
      while (lastNode.fromQuadTreeNode != null)
      {
        Vector2 coords = lastNode.coords;
        Vector2 fromCoords = lastNode.fromCoords;
        MapQuadTreePathfinding.Node step = this.m_steps[lastNode.fromQuadTreeNode];
        float height2 = step.quadTreeNode.height;
        if (this.AreDifferent(coords.x, fromCoords.x) & this.AreDifferent(coords.y, fromCoords.y))
        {
          Vector2 vector2 = (Vector2) step.quadTreeNode.ClampPositionToCell(coords) - coords;
          if ((double) Mathf.Abs(vector2.y) > (double) Mathf.Abs(vector2.x))
            path.Insert(0, new Vector3(coords.x, height2, fromCoords.y));
          else
            path.Insert(0, new Vector3(fromCoords.x, height2, coords.y));
        }
        lastNode = step;
        path.Insert(0, new Vector3(lastNode.coords.x, height2, lastNode.coords.y));
      }
    }

    private bool AreDifferent(float v1, float v2) => (double) Mathf.Abs(v1 - v2) > 9.9999997473787516E-05;

    private struct Node
    {
      public Vector2 coords;
      public Vector2 fromCoords;
      public float cost;
      public float priority;
      public MapQuadTree.Node quadTreeNode;
      public MapQuadTree.Node fromQuadTreeNode;

      public Node(
        Vector2 coord,
        Vector2 fromCoord,
        float cost,
        float priority,
        MapQuadTree.Node node,
        MapQuadTree.Node fromNode)
      {
        this.coords = coord;
        this.fromCoords = fromCoord;
        this.cost = cost;
        this.priority = priority;
        this.quadTreeNode = node;
        this.fromQuadTreeNode = fromNode;
      }
    }

    private class NodePriorityQueue : IComparer<MapQuadTreePathfinding.Node>
    {
      private readonly List<MapQuadTreePathfinding.Node> m_data;

      public NodePriorityQueue(int capacity) => this.m_data = new List<MapQuadTreePathfinding.Node>(capacity);

      public void Enqueue(MapQuadTreePathfinding.Node item)
      {
        List<MapQuadTreePathfinding.Node> data = this.m_data;
        data.Add(item);
        int index1;
        for (int index2 = data.Count - 1; index2 > 0; index2 = index1)
        {
          index1 = (index2 - 1) / 2;
          if (this.Compare(data[index2], data[index1]) >= 0)
            break;
          MapQuadTreePathfinding.Node node = data[index2];
          data[index2] = data[index1];
          data[index1] = node;
        }
      }

      public MapQuadTreePathfinding.Node Dequeue()
      {
        List<MapQuadTreePathfinding.Node> data = this.m_data;
        int index1 = data.Count - 1;
        MapQuadTreePathfinding.Node node1 = data[0];
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
              MapQuadTreePathfinding.Node node2 = data[index2];
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

      public int Compare(MapQuadTreePathfinding.Node x, MapQuadTreePathfinding.Node y)
      {
        float priority1 = x.priority;
        float priority2 = y.priority;
        if ((double) priority1 < (double) priority2)
          return -1;
        return (double) priority1 > (double) priority2 ? 1 : y.cost.CompareTo(x.cost);
      }
    }
  }
}
