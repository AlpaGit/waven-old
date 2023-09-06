// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.VisualEffectContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  public abstract class VisualEffectContext : AudioContext
  {
    protected readonly List<VisualEffect> m_visualEffectInstances = new List<VisualEffect>();

    [NotNull]
    public abstract Transform transform { get; }

    public abstract void GetVisualEffectTransformation(out Quaternion rotation, out Vector3 scale);

    public void AddVisualEffect([NotNull] VisualEffect visualEffect) => this.m_visualEffectInstances.Add(visualEffect);

    public void RemoveVisualEffect([NotNull] VisualEffect visualEffect) => this.m_visualEffectInstances.Remove(visualEffect);
  }
}
