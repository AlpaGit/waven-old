// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IMovableIsoObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface IMovableIsoObject : IIsoObject
  {
    [PublicAPI]
    void SetCellObject([NotNull] CellObject containerCell);

    [PublicAPI]
    void SetCellObjectInnerPosition(Vector2 innerPosition);
  }
}
