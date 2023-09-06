// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public abstract class SpellEffect : ScriptableEffect
  {
    [SerializeField]
    private SpellEffect.WaitMethod m_waitMethod;
    [SerializeField]
    private float m_waitDelay;
    [SerializeField]
    private SpellEffect.OrientationMethod m_orientationMethod;

    public SpellEffect.WaitMethod waitMethod => this.m_waitMethod;

    public float waitDelay => this.m_waitDelay;

    public SpellEffect.OrientationMethod orientationMethod => this.m_orientationMethod;

    [CanBeNull]
    public abstract Component Instantiate(
      [NotNull] Transform parent,
      Quaternion rotation,
      Vector3 scale,
      [CanBeNull] FightContext fightContext,
      [CanBeNull] ITimelineContextProvider contextProvider);

    public abstract IEnumerator DestroyWhenFinished([NotNull] Component instance);

    public enum WaitMethod
    {
      None,
      Delay,
      Destruction,
    }

    public enum OrientationMethod
    {
      None,
      Context,
      SpellEffectTarget,
    }
  }
}
