﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.CompanionReserveState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf.Reflection;

namespace Ankama.Cube.Protocols.FightCommonProtocol
{
  public enum CompanionReserveState
  {
    [OriginalName("IDLE")] Idle,
    [OriginalName("IN_FIGHT")] InFight,
    [OriginalName("GIVEN")] Given,
    [OriginalName("DEAD")] Dead,
  }
}
