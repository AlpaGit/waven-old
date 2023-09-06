// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightMapConfigurationExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  public static class FightMapConfigurationExtensions
  {
    public static int GetRegionCount(this FightMapConfiguration value)
    {
      switch (value)
      {
        case FightMapConfiguration.PVP:
          return 1;
        case FightMapConfiguration.PVM:
          return 1;
        case FightMapConfiguration.BossFight:
          return 4;
        case FightMapConfiguration.PVP3V3:
          return 3;
        case FightMapConfiguration.PVP2V2:
          return 1;
        default:
          throw new ArgumentOutOfRangeException(nameof (value), (object) value, (string) null);
      }
    }

    public static int GetPlayerCountPerRegion(this FightMapConfiguration value)
    {
      switch (value)
      {
        case FightMapConfiguration.PVP:
          return 2;
        case FightMapConfiguration.PVM:
          return 1;
        case FightMapConfiguration.BossFight:
          return 1;
        case FightMapConfiguration.PVP3V3:
          return 2;
        case FightMapConfiguration.PVP2V2:
          return 4;
        default:
          throw new ArgumentOutOfRangeException(nameof (value), (object) value, (string) null);
      }
    }
  }
}
