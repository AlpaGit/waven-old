// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.ParticleAnimation
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public class ParticleAnimation : MonoBehaviour
  {
    [SerializeField]
    private Animation m_animation;
    [SerializeField]
    private ParticleSystem m_particleSystem;

    private void OnEnable()
    {
      if (this.m_animation.GetClipCount() == 0)
        return;
      IEnumerator enumerator = this.m_animation.GetEnumerator();
      if (!enumerator.MoveNext())
        return;
      this.m_animation.Play(((AnimationState) enumerator.Current).name);
    }

    private void OnDisable() => this.m_animation.Stop();
  }
}
