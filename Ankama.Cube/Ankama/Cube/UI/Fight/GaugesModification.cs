// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.GaugesModification
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System;

namespace Ankama.Cube.UI.Fight
{
  public struct GaugesModification
  {
    private int m_air;
    private int m_earth;
    private int m_fire;
    private int m_water;
    private int m_reserve;
    private int m_actionPoint;

    public int air => this.m_air;

    public int earth => this.m_earth;

    public int fire => this.m_fire;

    public int water => this.m_water;

    public int reserve => this.m_reserve;

    public int actionPoint => this.m_actionPoint;

    public void Increment(CaracId carac, int modification)
    {
      if (modification == 0)
        return;
      switch (carac)
      {
        case CaracId.ActionPoints:
          this.m_actionPoint += modification;
          break;
        case CaracId.FirePoints:
          this.m_fire += modification;
          break;
        case CaracId.WaterPoints:
          this.m_water += modification;
          break;
        case CaracId.EarthPoints:
          this.m_earth += modification;
          break;
        case CaracId.AirPoints:
          this.m_air += modification;
          break;
        case CaracId.ReservePoints:
          this.m_reserve += modification;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (carac), (object) carac, (string) null);
      }
    }
  }
}
