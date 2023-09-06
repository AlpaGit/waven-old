// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IMapDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  public interface IMapDefinition
  {
    Vector3Int origin { get; }

    Vector2Int sizeMin { get; }

    Vector2Int sizeMax { get; }

    int regionCount { get; }

    FightMapRegionDefinition GetRegion(int index);

    int GetCellIndex(int x, int y);
  }
}
