// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.ICharacterObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface ICharacterObject : IMovableIsoObject, IIsoObject
  {
    [PublicAPI]
    Ankama.Cube.Data.Direction direction { get; set; }

    [PublicAPI]
    IEnumerator AddPropertyEffect([NotNull] AttachableEffect attachableEffect, PropertyId propertyId);

    [PublicAPI]
    IEnumerator RemovePropertyEffect([NotNull] AttachableEffect attachableEffect, PropertyId propertyId);

    [PublicAPI]
    IEnumerator Spawn();

    [PublicAPI]
    IEnumerator Die();

    [PublicAPI]
    void CheckParentCellIndicator();

    [PublicAPI]
    void ShowSpellTargetFeedback(bool isSelected);

    [PublicAPI]
    void HideSpellTargetFeedback();

    void SetPosition([NotNull] IMap map, Vector2 position);

    void ChangeDirection(Ankama.Cube.Data.Direction newDirection);
  }
}
