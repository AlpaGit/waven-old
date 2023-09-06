// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementaryStatesComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public class ElementaryStatesComparer : IEqualityComparer<ElementaryStates>
  {
    public static readonly ElementaryStatesComparer instance = new ElementaryStatesComparer();

    public bool Equals(ElementaryStates x, ElementaryStates y) => x == y;

    public int GetHashCode(ElementaryStates obj) => (int) obj;
  }
}
