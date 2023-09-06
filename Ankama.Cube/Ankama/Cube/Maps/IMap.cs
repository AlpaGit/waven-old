// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.IMap
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public interface IMap
  {
    [NotNull]
    CellObject GetCellObject(int x, int y);

    bool TryGetCellObject(int x, int y, out CellObject cellObject);

    Vector2Int GetCellCoords(Vector3 worldPosition);

    void AddArea(Area area);

    void MoveArea(Area from, Area to);

    void RemoveArea(Area area);

    MapCellIndicator GetCellIndicator(int x, int y);
  }
}
