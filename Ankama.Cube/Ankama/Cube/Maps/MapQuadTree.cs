// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapQuadTree
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [Serializable]
  public class MapQuadTree : ISerializationCallbackReceiver
  {
    [SerializeField]
    private int m_serializableRootNodeIndex = -1;
    [SerializeField]
    private List<MapQuadTree.SerializableNode> m_serializableNodeList = new List<MapQuadTree.SerializableNode>();

    public MapQuadTree.Node rootNode { get; private set; }

    public void SetNodes(MapQuadTree.Node value)
    {
      this.Clear();
      this.rootNode = value;
    }

    public void Clear()
    {
      this.rootNode = (MapQuadTree.Node) null;
      this.m_serializableRootNodeIndex = -1;
      this.m_serializableNodeList.Clear();
    }

    public bool TryGetClosestCell(Vector2 worldPos, out MapQuadTree.Node node, out Vector2 coords)
    {
      node = (MapQuadTree.Node) null;
      coords = Vector2.zero;
      if (!this.TryGetClosestNodeRecurively(this.rootNode, worldPos, out node, out float _))
        return false;
      coords = (Vector2) node.ClampPositionToCell(worldPos);
      return true;
    }

    private bool TryGetClosestNodeRecurively(
      MapQuadTree.Node node,
      Vector2 worldPos,
      out MapQuadTree.Node outNode,
      out float distance)
    {
      outNode = (MapQuadTree.Node) null;
      distance = float.MaxValue;
      if (node == null)
        return false;
      if (node.hasNoChildren)
      {
        outNode = node;
        distance = node.DistanceToPoint(worldPos);
        return true;
      }
      MapQuadTree.Node node1 = (MapQuadTree.Node) null;
      float num = float.MaxValue;
      if (this.TryGetClosestNodeRecurively(node.topLeft, worldPos, out outNode, out distance) && (double) distance < (double) num)
      {
        num = distance;
        node1 = outNode;
      }
      if (this.TryGetClosestNodeRecurively(node.topRight, worldPos, out outNode, out distance) && (double) distance < (double) num)
      {
        num = distance;
        node1 = outNode;
      }
      if (this.TryGetClosestNodeRecurively(node.bottomLeft, worldPos, out outNode, out distance) && (double) distance < (double) num)
      {
        num = distance;
        node1 = outNode;
      }
      if (this.TryGetClosestNodeRecurively(node.bottomRight, worldPos, out outNode, out distance) && (double) distance < (double) num)
      {
        num = distance;
        node1 = outNode;
      }
      distance = num;
      outNode = node1;
      return true;
    }

    public MapQuadTree.Node GetNodeAt(Vector2 worldPos) => this.GetNodeAtRecurively(this.rootNode, worldPos);

    private MapQuadTree.Node GetNodeAtRecurively(MapQuadTree.Node node, Vector2 worldPos)
    {
      if (node == null)
        return (MapQuadTree.Node) null;
      if (!node.IsInside(worldPos))
        return (MapQuadTree.Node) null;
      return node.hasNoChildren ? node : ((this.GetNodeAtRecurively(node.topLeft, worldPos) ?? this.GetNodeAtRecurively(node.topRight, worldPos)) ?? this.GetNodeAtRecurively(node.bottomLeft, worldPos)) ?? this.GetNodeAtRecurively(node.bottomRight, worldPos);
    }

    public void OnAfterDeserialize()
    {
      List<MapQuadTree.Node> nodeByIndex = new List<MapQuadTree.Node>();
      for (int index = 0; index < this.m_serializableNodeList.Count; ++index)
        nodeByIndex.Add(new MapQuadTree.Node());
      this.rootNode = this.DeSerializeNode(this.m_serializableRootNodeIndex, nodeByIndex);
    }

    private MapQuadTree.Node DeSerializeNode(int index, List<MapQuadTree.Node> nodeByIndex)
    {
      if (index < 0 || index >= this.m_serializableNodeList.Count || this.m_serializableNodeList.Count == 0)
        return (MapQuadTree.Node) null;
      MapQuadTree.SerializableNode serializableNode = this.m_serializableNodeList[index];
      MapQuadTree.Node node = nodeByIndex[index];
      node.min = serializableNode.min;
      node.max = serializableNode.max;
      node.height = serializableNode.height;
      node.topLeft = this.DeSerializeNode(serializableNode.topLeftIndex, nodeByIndex);
      node.topRight = this.DeSerializeNode(serializableNode.topRightIndex, nodeByIndex);
      node.bottomLeft = this.DeSerializeNode(serializableNode.bottomLeftIndex, nodeByIndex);
      node.bottomRight = this.DeSerializeNode(serializableNode.bottomRightIndex, nodeByIndex);
      node.connectedNodes = new List<MapQuadTree.Node>();
      for (int index1 = 0; index1 < serializableNode.connectedNodeIndex.Count; ++index1)
      {
        int index2 = serializableNode.connectedNodeIndex[index1];
        node.connectedNodes.Add(nodeByIndex[index2]);
      }
      return node;
    }

    public void OnBeforeSerialize()
    {
      List<MapQuadTree.Node> nodeByIndex = new List<MapQuadTree.Node>();
      this.m_serializableNodeList.Clear();
      this.m_serializableRootNodeIndex = this.SerializeNode(this.rootNode, nodeByIndex);
      this.SerializeNodeConnection(this.rootNode, nodeByIndex);
    }

    private int SerializeNode(MapQuadTree.Node node, List<MapQuadTree.Node> nodeByIndex)
    {
      if (node == null)
        return -1;
      MapQuadTree.SerializableNode serializableNode = new MapQuadTree.SerializableNode();
      serializableNode.min = node.min;
      serializableNode.max = node.max;
      serializableNode.height = node.height;
      serializableNode.topLeftIndex = this.SerializeNode(node.topLeft, nodeByIndex);
      serializableNode.topRightIndex = this.SerializeNode(node.topRight, nodeByIndex);
      serializableNode.bottomLeftIndex = this.SerializeNode(node.bottomLeft, nodeByIndex);
      serializableNode.bottomRightIndex = this.SerializeNode(node.bottomRight, nodeByIndex);
      int count = this.m_serializableNodeList.Count;
      this.m_serializableNodeList.Add(serializableNode);
      nodeByIndex.Add(node);
      return count;
    }

    private void SerializeNodeConnection(MapQuadTree.Node node, List<MapQuadTree.Node> nodeByIndex)
    {
      if (node == null)
        return;
      int index1 = nodeByIndex.IndexOf(node);
      MapQuadTree.SerializableNode serializableNode = this.m_serializableNodeList[index1];
      if (node.connectedNodes != null)
      {
        for (int index2 = 0; index2 < node.connectedNodes.Count; ++index2)
        {
          MapQuadTree.Node connectedNode = node.connectedNodes[index2];
          int num = nodeByIndex.IndexOf(connectedNode);
          if (serializableNode.connectedNodeIndex == null)
            serializableNode.connectedNodeIndex = new List<int>();
          serializableNode.connectedNodeIndex.Add(num);
        }
      }
      this.m_serializableNodeList[index1] = serializableNode;
      this.SerializeNodeConnection(node.topLeft, nodeByIndex);
      this.SerializeNodeConnection(node.topRight, nodeByIndex);
      this.SerializeNodeConnection(node.bottomLeft, nodeByIndex);
      this.SerializeNodeConnection(node.bottomRight, nodeByIndex);
    }

    [Serializable]
    public struct SerializableNode
    {
      [SerializeField]
      public Vector2 min;
      [SerializeField]
      public Vector2 max;
      [SerializeField]
      public float height;
      [SerializeField]
      public int topLeftIndex;
      [SerializeField]
      public int topRightIndex;
      [SerializeField]
      public int bottomLeftIndex;
      [SerializeField]
      public int bottomRightIndex;
      [SerializeField]
      public List<int> connectedNodeIndex;
    }

    public class Node
    {
      public Vector2 min;
      public Vector2 max;
      public float height;
      public MapQuadTree.Node topLeft;
      public MapQuadTree.Node topRight;
      public MapQuadTree.Node bottomLeft;
      public MapQuadTree.Node bottomRight;
      public List<MapQuadTree.Node> connectedNodes;

      public Vector2 center => (this.max + this.min) / 2f;

      public Vector2 size => this.max - this.min;

      public bool hasNoChildren => this.topLeft == null && this.topRight == null && this.bottomLeft == null && this.bottomRight == null;

      public bool hasConnections => this.connectedNodes != null && this.connectedNodes.Count > 0;

      public bool IsInside(Vector2 worldPos) => (double) worldPos.x > (double) this.min.x && (double) worldPos.x <= (double) this.max.x && (double) worldPos.y > (double) this.min.y && (double) worldPos.y <= (double) this.max.y;

      public Vector2Int ClampPositionToCell(Vector2 worldPos)
      {
        Vector2Int min = (this.min + Vector2.one * 0.5f).RoundToInt();
        Vector2Int max = (this.max - Vector2.one * 0.5f).RoundToInt();
        Vector2Int cell = worldPos.RoundToInt();
        cell.Clamp(min, max);
        return cell;
      }

      public Vector2 ClampPosition(Vector2 worldPos) => worldPos.Clamp(this.min, this.max);

      public float DistanceToPoint(Vector2 worldPos) => this.ClampPosition(worldPos).DistanceTo(worldPos);
    }
  }
}
