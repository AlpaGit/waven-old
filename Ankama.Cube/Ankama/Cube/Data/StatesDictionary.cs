// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.StatesDictionary
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ankama.Cube.Data
{
  public static class StatesDictionary
  {
    [PublicAPI]
    public static readonly ElementaryStates[] stateValues = ((IEnumerable<ElementaryStates>) EnumUtility.GetValues<ElementaryStates>()).Where<ElementaryStates>((Func<ElementaryStates, bool>) (s => s != ElementaryStates.None)).ToArray<ElementaryStates>();
  }
}
