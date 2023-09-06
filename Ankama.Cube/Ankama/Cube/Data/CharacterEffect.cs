// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public abstract class CharacterEffect : ScriptableEffect
  {
    [CanBeNull]
    public abstract Component Instantiate(
      [NotNull] Transform parent,
      [CanBeNull] ITimelineContextProvider contextProvider);

    public abstract IEnumerator DestroyWhenFinished([NotNull] Component instance);
  }
}
