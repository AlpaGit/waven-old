// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IObjectWithAssemblage
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface IObjectWithAssemblage : ICharacterObject, IMovableIsoObject, IIsoObject
  {
    void RefreshAssemblage(
      IEnumerable<Vector2Int> otherObjectInAssemblagePositions);
  }
}
