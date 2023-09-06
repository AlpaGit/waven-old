// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.ParticleTransformShake
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public class ParticleTransformShake : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem m_particleSystem;
    [SerializeField]
    private Transform[] m_transforms;
    [SerializeField]
    private AnimationCurve m_curve;
    [SerializeField]
    private Vector3 m_amplitude;
    private Vector3[] m_startingPositions;

    private void Awake()
    {
      this.m_startingPositions = new Vector3[this.m_transforms.Length];
      for (int index = 0; index < this.m_transforms.Length; ++index)
        this.m_startingPositions[index] = this.m_transforms[index].localPosition;
    }

    private void Update()
    {
      Vector3 vector3 = Random.insideUnitSphere * this.m_curve.Evaluate(this.m_particleSystem.time);
      vector3.x *= this.m_amplitude.x;
      vector3.y *= this.m_amplitude.y;
      vector3.z *= this.m_amplitude.z;
      for (int index = 0; index < this.m_transforms.Length; ++index)
        this.m_transforms[index].localPosition = this.m_startingPositions[index] + vector3;
    }
  }
}
