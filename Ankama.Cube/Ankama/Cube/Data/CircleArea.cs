// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CircleArea
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class CircleArea : Area
  {
    public readonly int radius;

    public CircleArea(Vector2Int center, int radius)
      : base(center)
    {
      this.radius = radius;
      int index = 0;
      this.occupiedCoords = new Vector2Int[4 * radius + 1];
      for (int x = -radius; x <= 0; ++x)
      {
        int num = radius + x;
        for (int y = -radius - x; y <= num; ++y)
        {
          this.occupiedCoords[index] = center + new Vector2Int(x, y);
          ++index;
        }
      }
      for (int x = 1; x <= radius; ++x)
      {
        int num = radius - x;
        for (int y = -radius + x; y <= num; ++y)
        {
          this.occupiedCoords[index] = center + new Vector2Int(x, y);
          ++index;
        }
      }
    }

    protected override bool AreaSpecificEquals(Area other) => this.radius == ((CircleArea) other).radius;

    public override Area GetCopy() => (Area) new CircleArea(this.refCoord, this.radius);

    public override Area GetAreaAt(Vector2Int position) => (Area) new CircleArea(position, this.radius);

    public override Area GetMovedArea(Vector2Int offset) => (Area) new CircleArea(this.refCoord + offset, this.radius);
  }
}
