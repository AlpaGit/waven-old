// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CenteredSquareArea
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class CenteredSquareArea : Area
  {
    public readonly int radius;

    public CenteredSquareArea(Vector2Int center, int radius)
      : base(center)
    {
      this.radius = radius;
      int num = 2 * radius + 1;
      this.occupiedCoords = new Vector2Int[num * num];
      for (int x = -radius; x <= radius; ++x)
      {
        for (int y = -radius; y <= radius; ++y)
          this.occupiedCoords[y * num + x] = center + new Vector2Int(x, y);
      }
    }

    protected override bool AreaSpecificEquals(Area other) => this.radius == ((CenteredSquareArea) other).radius;

    public override Area GetCopy() => (Area) new CenteredSquareArea(this.refCoord, this.radius);

    public override Area GetAreaAt(Vector2Int position) => (Area) new CenteredSquareArea(position, this.radius);

    public override Area GetMovedArea(Vector2Int offset) => (Area) new CenteredSquareArea(this.refCoord + offset, this.radius);
  }
}
