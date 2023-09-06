// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IObjectWithMovement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface IObjectWithMovement : ICharacterObject, IMovableIsoObject, IIsoObject
  {
    [PublicAPI]
    int movementPoints { get; }

    [PublicAPI]
    int baseMovementPoints { get; }

    [PublicAPI]
    void SetMovementPoints(int value);

    [PublicAPI]
    IEnumerator Move(Vector2Int[] movementCells);

    [PublicAPI]
    IEnumerator MoveToAction(
      Vector2Int[] movementCells,
      Ankama.Cube.Data.Direction actionDirection,
      bool hasFollowUpAnimation = true);
  }
}
