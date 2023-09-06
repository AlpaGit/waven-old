// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.TooltipElementValues
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Entities
{
  public struct TooltipElementValues
  {
    public readonly int air;
    public readonly int earth;
    public readonly int fire;
    public readonly int water;
    public readonly int reserve;

    public TooltipElementValues(int air, int earth, int fire, int water, int reserve)
    {
      this.air = air;
      this.earth = earth;
      this.fire = fire;
      this.water = water;
      this.reserve = reserve;
    }
  }
}
