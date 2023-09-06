// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PivotBasedSquareArea
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class PivotBasedSquareArea : Area
  {
    public readonly int sideSize;

    public PivotBasedSquareArea(Vector2Int pivot, int sideSize)
      : base(pivot)
    {
      this.sideSize = sideSize;
      this.occupiedCoords = new Vector2Int[sideSize * sideSize];
      for (int x = 0; x < sideSize; ++x)
      {
        for (int y = 0; y < sideSize; ++y)
          this.occupiedCoords[y * sideSize + x] = pivot + new Vector2Int(x, y);
      }
    }

    protected override bool AreaSpecificEquals(Area other) => this.sideSize == ((PivotBasedSquareArea) other).sideSize;

    public override Area GetCopy() => (Area) new PivotBasedSquareArea(this.refCoord, this.sideSize);

    public override Area GetAreaAt(Vector2Int position) => (Area) new PivotBasedSquareArea(position, this.sideSize);

    public override Area GetMovedArea(Vector2Int offset) => (Area) new PivotBasedSquareArea(this.refCoord + offset, this.sideSize);
  }
}
