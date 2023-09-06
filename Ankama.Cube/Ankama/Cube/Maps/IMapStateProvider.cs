// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.IMapStateProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public interface IMapStateProvider
  {
    Vector2Int sizeMin { get; }

    Vector2Int sizeMax { get; }

    int GetCellIndex(int x, int y);

    FightCellState GetCellState(int index);
  }
}
