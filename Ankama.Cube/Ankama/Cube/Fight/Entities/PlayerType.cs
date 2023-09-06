// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.PlayerType
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Fight.Entities
{
  [Flags]
  public enum PlayerType
  {
    None = 0,
    Ally = 1,
    Opponent = 2,
    Local = 4,
    Player = 13, // 0x0000000D
  }
}
