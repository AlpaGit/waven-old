// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Movement.FightPathFinder
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Movement
{
  public class FightPathFinder
  {
    private readonly FightPathFinder.NodePriorityQueue m_frontier;
    private readonly Dictionary<int, FightPathFinder.Node> m_steps;
    private readonly List<FightPathFinder.Node> m_previousStepsBuffer;
    private readonly FightPathFinder.AdjacentCoord[] m_adjacentCoordsBuffer;
    private readonly Queue<FightPathFinder.FloodFillNode> m_floodFillFrontier;
    private int m_movementPoints;
    private bool m_canPassThrough;

    public bool tracking { get; private set; }

    public List<Vector2Int> currentPath { get; }

    public FightPathFinder(int pathCapacity = 8, int priorityQueueCapacity = 16)
    {
      this.currentPath = new List<Vector2Int>(pathCapacity);
      this.m_frontier = new FightPathFinder.NodePriorityQueue(priorityQueueCapacity);
      this.m_steps = new Dictionary<int, FightPathFinder.Node>(priorityQueueCapacity);
      this.m_previousStepsBuffer = new List<FightPathFinder.Node>(pathCapacity);
      this.m_adjacentCoordsBuffer = new FightPathFinder.AdjacentCoord[4];
      this.m_floodFillFrontier = new Queue<FightPathFinder.FloodFillNode>(32);
    }

    public void FloodFill(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int position,
      int movementPoints,
      bool canPassThrough)
    {
      Queue<FightPathFinder.FloodFillNode> floodFillFrontier = this.m_floodFillFrontier;
      Vector2Int sizeMin = mapStateProvider.sizeMin;
      Vector2Int sizeMax = mapStateProvider.sizeMax;
      floodFillFrontier.Clear();
      FightPathFinder.FloodFillNode floodFillNode1 = new FightPathFinder.FloodFillNode(position, 0);
      floodFillFrontier.Enqueue(floodFillNode1);
      do
      {
        FightPathFinder.FloodFillNode floodFillNode2 = floodFillFrontier.Dequeue();
        Vector2Int coords1 = floodFillNode2.coords;
        int cost1 = floodFillNode2.cost;
        if (cost1 < movementPoints)
        {
          int cost2 = cost1 + 1;
          int x1 = coords1.x;
          int y1 = coords1.y;
          int x2 = sizeMin.x;
          int y2 = sizeMin.y;
          int x3 = sizeMax.x;
          int y3 = sizeMax.y;
          Vector2Int coords2 = new Vector2Int(x1, y1 + 1);
          Vector2Int coords3 = new Vector2Int(x1 - 1, y1);
          Vector2Int coords4 = new Vector2Int(x1 + 1, y1);
          Vector2Int coords5 = new Vector2Int(x1, y1 - 1);
          int x4 = coords2.x;
          int y4 = coords2.y;
          if (x4 >= x2 && x4 < x3 && y4 >= y2 && y4 < y3)
          {
            int cellIndex = mapStateProvider.GetCellIndex(x4, y4);
            FightMapMovementContext.Cell cell = grid[cellIndex];
            FightMapMovementContext.CellState state = cell.state;
            if ((state & (FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Reachable)) == FightMapMovementContext.CellState.Movement)
            {
              grid[cellIndex] = new FightMapMovementContext.Cell(coords2, state | FightMapMovementContext.CellState.Reachable, cell.entity);
              if ((state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough)
              {
                FightPathFinder.FloodFillNode floodFillNode3 = new FightPathFinder.FloodFillNode(coords2, cost2);
                floodFillFrontier.Enqueue(floodFillNode3);
              }
            }
          }
          int x5 = coords3.x;
          int y5 = coords3.y;
          if (x5 >= x2 && x5 < x3 && y5 >= y2 && y5 < y3)
          {
            int cellIndex = mapStateProvider.GetCellIndex(x5, y5);
            FightMapMovementContext.Cell cell = grid[cellIndex];
            FightMapMovementContext.CellState state = cell.state;
            if ((state & (FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Reachable)) == FightMapMovementContext.CellState.Movement)
            {
              grid[cellIndex] = new FightMapMovementContext.Cell(coords3, state | FightMapMovementContext.CellState.Reachable, cell.entity);
              if ((state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough)
              {
                FightPathFinder.FloodFillNode floodFillNode4 = new FightPathFinder.FloodFillNode(coords3, cost2);
                floodFillFrontier.Enqueue(floodFillNode4);
              }
            }
          }
          int x6 = coords4.x;
          int y6 = coords4.y;
          if (x6 >= x2 && x6 < x3 && y6 >= y2 && y6 < y3)
          {
            int cellIndex = mapStateProvider.GetCellIndex(x6, y6);
            FightMapMovementContext.Cell cell = grid[cellIndex];
            FightMapMovementContext.CellState state = cell.state;
            if ((state & (FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Reachable)) == FightMapMovementContext.CellState.Movement)
            {
              grid[cellIndex] = new FightMapMovementContext.Cell(coords4, state | FightMapMovementContext.CellState.Reachable, cell.entity);
              if ((state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough)
              {
                FightPathFinder.FloodFillNode floodFillNode5 = new FightPathFinder.FloodFillNode(coords4, cost2);
                floodFillFrontier.Enqueue(floodFillNode5);
              }
            }
          }
          int x7 = coords5.x;
          int y7 = coords5.y;
          if (x7 >= x2 && x7 < x3 && y7 >= y2 && y7 < y3)
          {
            int cellIndex = mapStateProvider.GetCellIndex(x7, y7);
            FightMapMovementContext.Cell cell = grid[cellIndex];
            FightMapMovementContext.CellState state = cell.state;
            if ((state & (FightMapMovementContext.CellState.Movement | FightMapMovementContext.CellState.Reachable)) == FightMapMovementContext.CellState.Movement)
            {
              grid[cellIndex] = new FightMapMovementContext.Cell(coords5, state | FightMapMovementContext.CellState.Reachable, cell.entity);
              if ((state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough)
              {
                FightPathFinder.FloodFillNode floodFillNode6 = new FightPathFinder.FloodFillNode(coords5, cost2);
                floodFillFrontier.Enqueue(floodFillNode6);
              }
            }
          }
        }
      }
      while (floodFillFrontier.Count > 0);
    }

    public void Begin(Vector2Int position, int movementPoints, bool canPassThrough)
    {
      this.m_movementPoints = movementPoints;
      this.m_canPassThrough = canPassThrough;
      this.currentPath.Clear();
      this.currentPath.Add(position);
      this.tracking = true;
    }

    public void Move(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int position,
      bool isTargeting)
    {
      List<Vector2Int> currentPath = this.currentPath;
      int count1 = currentPath.Count;
      int movementPoints1 = this.m_movementPoints;
      if (isTargeting)
      {
        int movementPoints2 = movementPoints1 + 1;
        if (currentPath[0].DistanceTo(position) > movementPoints2)
        {
          currentPath.RemoveRange(1, count1 - 1);
        }
        else
        {
          Vector2Int vector2Int = currentPath[count1 - 1];
          if (vector2Int.DistanceTo(position) == 1 && FightPathFinder.CanStopAt(mapStateProvider, grid, vector2Int))
            return;
          if (count1 == 1 || !this.AppendPartialPath(mapStateProvider, grid, position, movementPoints2, true))
            this.ComputeFullPath(mapStateProvider, grid, currentPath[0], position, movementPoints2, true);
          int count2 = currentPath.Count;
          if (count2 <= 1)
            return;
          currentPath.RemoveAt(count2 - 1);
        }
      }
      else if (currentPath[0].DistanceTo(position) > movementPoints1)
      {
        currentPath.RemoveRange(1, count1 - 1);
      }
      else
      {
        for (int index = 0; index < count1; ++index)
        {
          if (currentPath[index] == position)
          {
            currentPath.RemoveRange(index + 1, count1 - 1 - index);
            return;
          }
        }
        if (count1 <= movementPoints1 && currentPath[count1 - 1].DistanceTo(position) == 1)
        {
          currentPath.Add(position);
        }
        else
        {
          if (count1 != 1 && this.AppendPartialPath(mapStateProvider, grid, position, movementPoints1, false))
            return;
          this.ComputeFullPath(mapStateProvider, grid, currentPath[0], position, movementPoints1, false);
        }
      }
    }

    public void Reset() => this.currentPath.RemoveRange(1, this.currentPath.Count - 1);

    public void End() => this.tracking = false;

    private bool AppendPartialPath(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int end,
      int movementPoints,
      bool isTargeting)
    {
      List<Vector2Int> currentPath = this.currentPath;
      List<FightPathFinder.Node> previousStepsBuffer = this.m_previousStepsBuffer;
      FightPathFinder.NodePriorityQueue frontier = this.m_frontier;
      Dictionary<int, FightPathFinder.Node> steps = this.m_steps;
      FightPathFinder.AdjacentCoord[] adjacentCoordsBuffer = this.m_adjacentCoordsBuffer;
      int count = currentPath.Count;
      Vector2Int vector2Int1 = mapStateProvider.sizeMax;
      int x1 = vector2Int1.x;
      vector2Int1 = mapStateProvider.sizeMin;
      int x2 = vector2Int1.x;
      int num1 = x1 - x2;
      if (previousStepsBuffer.Capacity < count)
        previousStepsBuffer.Capacity = count;
      previousStepsBuffer.Clear();
      Vector2Int vector2Int2 = currentPath[0];
      previousStepsBuffer.Add(new FightPathFinder.Node(vector2Int2, vector2Int2, 0, 0, vector2Int2.GetDirectionTo(end)));
      for (int index = 1; index < count; ++index)
      {
        Vector2Int vector2Int3 = currentPath[index];
        previousStepsBuffer.Add(new FightPathFinder.Node(vector2Int3, vector2Int2, index, 0, vector2Int2.GetDirectionTo(vector2Int3)));
        vector2Int2 = vector2Int3;
      }
      for (int index1 = count - 1; index1 > 0; --index1)
      {
        FightPathFinder.Node node1 = previousStepsBuffer[index1];
        int num2 = node1.coords.DistanceTo(end);
        if (index1 + num2 <= movementPoints)
        {
          frontier.Clear();
          frontier.Enqueue(node1);
          steps.Clear();
          for (int index2 = 0; index2 < index1; ++index2)
          {
            FightPathFinder.Node node2 = previousStepsBuffer[index2];
            Vector2Int coords = node2.coords;
            int key = coords.y * num1 + coords.x;
            steps.Add(key, node2);
          }
          do
          {
            FightPathFinder.Node node3 = frontier.Dequeue();
            Vector2Int coords1 = node3.coords;
            Vector2Int fromCoords1 = node3.fromCoords;
            int cost1 = node3.cost;
            Ankama.Cube.Data.Direction direction = node3.direction;
            if (node3.coords == end)
            {
              if (isTargeting && this.m_canPassThrough && !FightPathFinder.CanStopAt(mapStateProvider, grid, fromCoords1))
              {
                int key = coords1.y * num1 + coords1.x;
                steps[key] = new FightPathFinder.Node(coords1, fromCoords1, int.MaxValue, node3.priority, direction);
              }
              else
              {
                int index3 = cost1 + 1;
                if (currentPath.Capacity < index3)
                  currentPath.Capacity = index3;
                if (count > index3)
                  currentPath.RemoveRange(index3, count - index3);
                else if (count < index3)
                {
                  for (int index4 = count; index4 < index3; ++index4)
                    currentPath.Add(end);
                }
                currentPath[cost1] = end;
                for (int index5 = cost1 - 1; index5 >= count; --index5)
                {
                  Vector2Int fromCoords2 = node3.fromCoords;
                  int key = fromCoords2.y * num1 + fromCoords2.x;
                  node3 = steps[key];
                  currentPath[index5] = node3.coords;
                }
                return true;
              }
            }
            else
            {
              this.ComputeAdjacentCoords(mapStateProvider, grid, coords1, fromCoords1);
              for (int index6 = 0; index6 < 4; ++index6)
              {
                FightPathFinder.AdjacentCoord adjacentCoord = adjacentCoordsBuffer[index6];
                if (adjacentCoord.isValid)
                {
                  Vector2Int coords2 = adjacentCoord.coords;
                  int cost2 = cost1 + 1;
                  int num3 = coords2.DistanceTo(end);
                  if (cost2 + num3 <= movementPoints)
                  {
                    int key = coords2.y * num1 + coords2.x;
                    FightPathFinder.Node node4;
                    if (!steps.TryGetValue(key, out node4) || node4.cost >= cost2)
                    {
                      Ankama.Cube.Data.Direction directionTo = coords1.GetDirectionTo(coords2);
                      int num4 = directionTo != direction ? 1 : 0;
                      int num5 = (num3 << 1) + num4;
                      node4 = new FightPathFinder.Node(coords2, coords1, cost2, cost2 + num5, directionTo);
                      frontier.Enqueue(node4);
                      steps[key] = node4;
                    }
                  }
                }
              }
            }
          }
          while (frontier.Count() > 0);
        }
        currentPath.RemoveAt(index1);
        --count;
      }
      return false;
    }

    private void ComputeFullPath(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int start,
      Vector2Int end,
      int movementPoints,
      bool isTargeting)
    {
      List<Vector2Int> currentPath = this.currentPath;
      FightPathFinder.AdjacentCoord[] adjacentCoordsBuffer = this.m_adjacentCoordsBuffer;
      FightPathFinder.NodePriorityQueue frontier = this.m_frontier;
      Dictionary<int, FightPathFinder.Node> steps = this.m_steps;
      int count = currentPath.Count;
      Vector2Int vector2Int = mapStateProvider.sizeMax;
      int x1 = vector2Int.x;
      vector2Int = mapStateProvider.sizeMin;
      int x2 = vector2Int.x;
      int num1 = x1 - x2;
      frontier.Clear();
      steps.Clear();
      if (start.DistanceTo(end) <= movementPoints)
      {
        Ankama.Cube.Data.Direction directionTo1 = start.GetDirectionTo(end);
        FightPathFinder.Node node1 = new FightPathFinder.Node(start, start, 0, 0, directionTo1);
        frontier.Enqueue(node1);
        do
        {
          FightPathFinder.Node node2 = frontier.Dequeue();
          Vector2Int coords1 = node2.coords;
          Vector2Int fromCoords1 = node2.fromCoords;
          int cost1 = node2.cost;
          Ankama.Cube.Data.Direction direction = node2.direction;
          if (coords1 == end)
          {
            if (isTargeting && this.m_canPassThrough && !FightPathFinder.CanStopAt(mapStateProvider, grid, fromCoords1))
            {
              int key = coords1.y * num1 + coords1.x;
              steps[key] = new FightPathFinder.Node(coords1, fromCoords1, int.MaxValue, node2.priority, direction);
            }
            else
            {
              int index1 = cost1 + 1;
              if (currentPath.Capacity < index1)
                currentPath.Capacity = index1;
              if (count > index1)
                currentPath.RemoveRange(index1, count - index1);
              else if (count < index1)
              {
                for (int index2 = count; index2 < index1; ++index2)
                  currentPath.Add(end);
              }
              currentPath[cost1] = end;
              for (int index3 = cost1 - 1; index3 > 0; --index3)
              {
                Vector2Int fromCoords2 = node2.fromCoords;
                int key = fromCoords2.y * num1 + fromCoords2.x;
                node2 = steps[key];
                currentPath[index3] = node2.coords;
              }
              currentPath[0] = start;
              return;
            }
          }
          else
          {
            this.ComputeAdjacentCoords(mapStateProvider, grid, coords1, fromCoords1);
            for (int index = 0; index < 4; ++index)
            {
              FightPathFinder.AdjacentCoord adjacentCoord = adjacentCoordsBuffer[index];
              if (adjacentCoord.isValid)
              {
                Vector2Int coords2 = adjacentCoord.coords;
                int cost2 = cost1 + 1;
                int num2 = coords2.DistanceTo(end);
                if (cost2 + num2 <= movementPoints)
                {
                  int key = coords2.y * num1 + coords2.x;
                  FightPathFinder.Node node3;
                  if (!steps.TryGetValue(key, out node3) || node3.cost >= cost2)
                  {
                    Ankama.Cube.Data.Direction directionTo2 = coords1.GetDirectionTo(coords2);
                    int num3 = directionTo2 != direction ? 1 : 0;
                    int num4 = (num2 << 1) + num3;
                    node3 = new FightPathFinder.Node(coords2, coords1, cost2, cost2 + num4, directionTo2);
                    frontier.Enqueue(node3);
                    steps[key] = node3;
                  }
                }
              }
            }
          }
        }
        while (frontier.Count() > 0);
      }
      if (count == 0)
      {
        currentPath.Add(start);
      }
      else
      {
        currentPath[0] = start;
        currentPath.RemoveRange(1, count - 1);
      }
    }

    private void ComputeAdjacentCoords(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int coords,
      Vector2Int from)
    {
      Vector2Int sizeMin = mapStateProvider.sizeMin;
      Vector2Int sizeMax = mapStateProvider.sizeMax;
      int x1 = coords.x;
      int y1 = coords.y;
      int x2 = sizeMin.x;
      int y2 = sizeMin.y;
      int x3 = sizeMax.x;
      int y3 = sizeMax.y;
      bool canPassThrough = this.m_canPassThrough;
      Vector2Int coords1 = new Vector2Int(x1, y1 + 1);
      Vector2Int coords2 = new Vector2Int(x1 - 1, y1);
      Vector2Int coords3 = new Vector2Int(x1 + 1, y1);
      Vector2Int coords4 = new Vector2Int(x1, y1 - 1);
      int x4 = coords1.x;
      int y4 = coords1.y;
      bool isValid1;
      if (coords1 != from && x4 >= x2 && x4 < x3 && y4 >= y2 && y4 < y3)
      {
        int cellIndex = mapStateProvider.GetCellIndex(x4, y4);
        FightMapMovementContext.CellState state = grid[cellIndex].state;
        isValid1 = (state & FightMapMovementContext.CellState.Reachable) != FightMapMovementContext.CellState.None && (state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough || (state & FightMapMovementContext.CellState.Targeted) != 0;
      }
      else
        isValid1 = false;
      int x5 = coords2.x;
      int y5 = coords2.y;
      bool isValid2;
      if (coords2 != from && x5 >= x2 && x5 < x3 && y5 >= y2 && y5 < y3)
      {
        int cellIndex = mapStateProvider.GetCellIndex(x5, y5);
        FightMapMovementContext.CellState state = grid[cellIndex].state;
        isValid2 = (state & FightMapMovementContext.CellState.Reachable) != FightMapMovementContext.CellState.None && (state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough || (state & FightMapMovementContext.CellState.Targeted) != 0;
      }
      else
        isValid2 = false;
      int x6 = coords3.x;
      int y6 = coords3.y;
      bool isValid3;
      if (coords3 != from && x6 >= x2 && x6 < x3 && y6 >= y2 && y6 < y3)
      {
        int cellIndex = mapStateProvider.GetCellIndex(x6, y6);
        FightMapMovementContext.CellState state = grid[cellIndex].state;
        isValid3 = (state & FightMapMovementContext.CellState.Reachable) != FightMapMovementContext.CellState.None && (state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough || (state & FightMapMovementContext.CellState.Targeted) != 0;
      }
      else
        isValid3 = false;
      int x7 = coords4.x;
      int y7 = coords4.y;
      bool isValid4;
      if (coords4 != from && x7 >= x2 && x7 < x3 && y7 >= y2 && y7 < y3)
      {
        int cellIndex = mapStateProvider.GetCellIndex(x7, y7);
        FightMapMovementContext.CellState state = grid[cellIndex].state;
        isValid4 = (state & FightMapMovementContext.CellState.Reachable) != FightMapMovementContext.CellState.None && (state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None | canPassThrough || (state & FightMapMovementContext.CellState.Targeted) != 0;
      }
      else
        isValid4 = false;
      FightPathFinder.AdjacentCoord[] adjacentCoordsBuffer = this.m_adjacentCoordsBuffer;
      adjacentCoordsBuffer[0] = new FightPathFinder.AdjacentCoord(coords1, isValid1);
      adjacentCoordsBuffer[1] = new FightPathFinder.AdjacentCoord(coords2, isValid2);
      adjacentCoordsBuffer[2] = new FightPathFinder.AdjacentCoord(coords3, isValid3);
      adjacentCoordsBuffer[3] = new FightPathFinder.AdjacentCoord(coords4, isValid4);
    }

    private static bool CanStopAt(
      IMapStateProvider mapStateProvider,
      FightMapMovementContext.Cell[] grid,
      Vector2Int coords)
    {
      int cellIndex = mapStateProvider.GetCellIndex(coords.x, coords.y);
      return (grid[cellIndex].state & FightMapMovementContext.CellState.Occupied) == FightMapMovementContext.CellState.None;
    }

    private struct Node
    {
      public readonly Vector2Int coords;
      public readonly Vector2Int fromCoords;
      public readonly int cost;
      public readonly int priority;
      public readonly Ankama.Cube.Data.Direction direction;

      public Node(
        Vector2Int coords,
        Vector2Int fromCoords,
        int cost,
        int priority,
        Ankama.Cube.Data.Direction direction)
      {
        this.coords = coords;
        this.fromCoords = fromCoords;
        this.cost = cost;
        this.priority = priority;
        this.direction = direction;
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

    private struct FloodFillNode
    {
      public readonly Vector2Int coords;
      public readonly int cost;

      public FloodFillNode(Vector2Int coords, int cost)
      {
        this.coords = coords;
        this.cost = cost;
      }
    }

    private class NodePriorityQueue : IComparer<FightPathFinder.Node>
    {
      private readonly List<FightPathFinder.Node> m_data;

      public NodePriorityQueue(int capacity) => this.m_data = new List<FightPathFinder.Node>(capacity);

      public void Enqueue(FightPathFinder.Node item)
      {
        List<FightPathFinder.Node> data = this.m_data;
        data.Add(item);
        int index1;
        for (int index2 = data.Count - 1; index2 > 0; index2 = index1)
        {
          index1 = (index2 - 1) / 2;
          if (this.Compare(data[index2], data[index1]) >= 0)
            break;
          FightPathFinder.Node node = data[index2];
          data[index2] = data[index1];
          data[index1] = node;
        }
      }

      public FightPathFinder.Node Dequeue()
      {
        List<FightPathFinder.Node> data = this.m_data;
        int index1 = data.Count - 1;
        FightPathFinder.Node node1 = data[0];
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
              FightPathFinder.Node node2 = data[index2];
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

      [Pure]
      public int Compare(FightPathFinder.Node x, FightPathFinder.Node y)
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
