// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public class ElementComparer : IEqualityComparer<Element>
  {
    public static readonly ElementComparer instance = new ElementComparer();

    public bool Equals(Element x, Element y) => x == y;

    public int GetHashCode(Element obj) => (int) obj;
  }
}
