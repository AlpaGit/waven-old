// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.CommonProtocol.FightResult
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf.Reflection;

namespace Ankama.Cube.Protocols.CommonProtocol
{
  public enum FightResult
  {
    [OriginalName("Draw")] Draw,
    [OriginalName("Victory")] Victory,
    [OriginalName("Defeat")] Defeat,
  }
}
