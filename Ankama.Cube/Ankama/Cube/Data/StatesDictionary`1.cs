// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.StatesDictionary`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public abstract class StatesDictionary<T> : SerializableDictionary<ElementaryStates, T>
  {
    protected StatesDictionary()
      : base((IEqualityComparer<ElementaryStates>) ElementaryStatesComparer.instance)
    {
    }
  }
}
