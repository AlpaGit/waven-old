// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DirectionAngle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum DirectionAngle
  {
    CounterClockwise180 = -4, // 0xFFFFFFFC
    CounterClockwise135 = -3, // 0xFFFFFFFD
    CounterClockwise90 = -2, // 0xFFFFFFFE
    CounterClockwise45 = -1, // 0xFFFFFFFF
    None = 0,
    Clockwise45 = 1,
    Clockwise90 = 2,
    Clockwise135 = 3,
    Clockwise180 = 4,
  }
}
