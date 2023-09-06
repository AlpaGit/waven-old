// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GodComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public class GodComparer : IEqualityComparer<God>
  {
    public static readonly GodComparer instance = new GodComparer();

    public bool Equals(God x, God y) => x == y;

    public int GetHashCode(God obj) => (int) obj;
  }
}
