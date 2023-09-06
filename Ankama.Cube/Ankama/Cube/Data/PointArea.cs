// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PointArea
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class PointArea : Area
  {
    public PointArea(Vector2Int point)
      : base(point)
    {
      this.occupiedCoords = new Vector2Int[1]{ point };
    }

    protected override bool AreaSpecificEquals(Area other) => true;

    public override Area GetCopy() => (Area) new PointArea(this.refCoord);

    public override Area GetAreaAt(Vector2Int position) => (Area) new PointArea(position);

    public override Area GetMovedArea(Vector2Int offset) => (Area) new PointArea(this.refCoord + offset);

    public override void Move(Vector2Int offset)
    {
      this.refCoord = this.refCoord + offset;
      this.occupiedCoords[0] = this.refCoord;
    }

    public override void MoveTo(Vector2Int newRefCoords)
    {
      this.refCoord = newRefCoords;
      this.occupiedCoords[0] = newRefCoords;
    }

    public override bool Intersects(Area other)
    {
      Vector2Int[] occupiedCoords = other.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        if (this.refCoord == occupiedCoords[index])
          return true;
      }
      return false;
    }

    public override bool Contains(Area other)
    {
      Vector2Int[] occupiedCoords = other.occupiedCoords;
      return occupiedCoords.Length <= 1 && occupiedCoords[0] == this.refCoord;
    }

    public override int MinDistanceWith(Area other)
    {
      Vector2Int[] occupiedCoords = other.occupiedCoords;
      int length = occupiedCoords.Length;
      int num1 = int.MaxValue;
      for (int index = 0; index < length; ++index)
      {
        int num2 = this.refCoord.DistanceTo(occupiedCoords[index]);
        if (num2 < num1)
          num1 = num2;
      }
      return num1;
    }

    public override int MinDistanceWith(Vector2Int other) => this.refCoord.DistanceTo(other);

    public override bool DistanceValid(Area other, int distance)
    {
      Vector2Int[] occupiedCoords = other.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        if (this.refCoord.DistanceTo(occupiedCoords[index]) == distance)
          return true;
      }
      return false;
    }

    public override bool Intersects(Vector2Int other) => this.refCoord == other;
  }
}
