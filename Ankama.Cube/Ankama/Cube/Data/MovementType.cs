// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MovementType
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum MovementType
  {
    Run = 1,
    Action = 2,
    Teleport = 3,
    Charge = 4,
    Push = 6,
    Pull = 7,
    Dash = 8,
  }
}
