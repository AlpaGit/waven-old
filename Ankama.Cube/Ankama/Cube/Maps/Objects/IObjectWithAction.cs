// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IObjectWithAction
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface IObjectWithAction : ICharacterObject, IMovableIsoObject, IIsoObject
  {
    [PublicAPI]
    ActionType actionType { get; }

    [PublicAPI]
    int? actionValue { get; }

    [PublicAPI]
    int physicalDamageBoost { get; }

    [PublicAPI]
    int physicalHealBoost { get; }

    [PublicAPI]
    void SetPhysicalDamageBoost(int value);

    [PublicAPI]
    void SetPhysicalHealBoost(int value);

    [PublicAPI]
    void SetActionUsed(bool actionUsed, bool turnEnded);

    IEnumerator PlayActionAnimation(
      Ankama.Cube.Data.Direction directionToAttack,
      bool waitForAnimationEndOnMissingLabel);

    IEnumerator PlayRangedActionAnimation(Ankama.Cube.Data.Direction directionToAttack);

    void TriggerActionEffect(Vector2Int target);
  }
}
