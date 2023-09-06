// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.ParticleTransformRotation
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public class ParticleTransformRotation : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem m_particleSystem;
    [SerializeField]
    private Transform[] m_transforms;
    [SerializeField]
    private ParticleTransformRotation.RotationSpace m_rotationSpace;
    [SerializeField]
    private AnimationCurve m_curve;
    [SerializeField]
    private Vector3 m_axis;
    [SerializeField]
    private Vector3 m_offsetRotation;

    private void OnEnable()
    {
      if (this.m_rotationSpace != ParticleTransformRotation.RotationSpace.Camera)
        return;
      Camera.onPreCull += new Camera.CameraCallback(this.OnPrecullCamera);
    }

    private void OnDisable()
    {
      if (this.m_rotationSpace != ParticleTransformRotation.RotationSpace.Camera)
        return;
      Camera.onPreCull -= new Camera.CameraCallback(this.OnPrecullCamera);
    }

    private void Update()
    {
      if (this.m_rotationSpace == ParticleTransformRotation.RotationSpace.Camera)
        return;
      Quaternion quaternion = Quaternion.Euler(this.m_axis * this.m_curve.Evaluate(this.m_particleSystem.time) + this.m_offsetRotation);
      if (this.m_rotationSpace == ParticleTransformRotation.RotationSpace.Self)
      {
        for (int index = 0; index < this.m_transforms.Length; ++index)
          this.m_transforms[index].localRotation = quaternion;
      }
      else
      {
        for (int index = 0; index < this.m_transforms.Length; ++index)
          this.m_transforms[index].rotation = quaternion;
      }
    }

    private void OnPrecullCamera(Camera camera)
    {
      if (!camera.isActiveAndEnabled)
        return;
      Quaternion quaternion = Quaternion.LookRotation(-camera.transform.forward, camera.transform.up) * Quaternion.Euler(this.m_axis * this.m_curve.Evaluate(this.m_particleSystem.time));
      for (int index = 0; index < this.m_transforms.Length; ++index)
        this.m_transforms[index].rotation = quaternion;
    }

    private enum RotationSpace
    {
      Self,
      World,
      Camera,
    }
  }
}
