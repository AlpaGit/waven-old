// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CameraShakeParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class CameraShakeParameters : ScriptableObject
  {
    [SerializeField]
    private Vector2 m_translationAmplitude;
    [SerializeField]
    private float m_rotationAmplitude;
    [SerializeField]
    private float m_translationXFrequency = 1f;
    [SerializeField]
    private Vector2 m_translationXPerlinStart;
    [SerializeField]
    private Vector2 m_translationXPerlinVector;
    [SerializeField]
    private float m_translationYFrequency = 1f;
    [SerializeField]
    private Vector2 m_translationYPerlinStart;
    [SerializeField]
    private Vector2 m_translationYPerlinVector;
    [SerializeField]
    private float m_rotationFrequency = 1f;
    [SerializeField]
    private Vector2 m_rotationPerlinStart;
    [SerializeField]
    private Vector2 m_rotationPerlinVector;

    public Vector2 GetTranslation(float time, float strength)
    {
      float num1 = time * this.m_translationXFrequency;
      float num2 = time * this.m_translationYFrequency;
      Vector2 vector2_1 = this.m_translationXPerlinStart + num1 * this.m_translationXPerlinVector;
      Vector2 vector2_2 = this.m_translationYPerlinStart + num2 * this.m_translationYPerlinVector;
      Vector2 translation;
      translation.x = (float) (2.0 * (double) Mathf.PerlinNoise(vector2_1.x, vector2_1.y) - 1.0);
      translation.y = (float) (2.0 * (double) Mathf.PerlinNoise(vector2_2.x, vector2_2.y) - 1.0);
      translation.Scale(strength * this.m_translationAmplitude);
      return translation;
    }

    public float GetAngle(float time, float strength)
    {
      Vector2 vector2 = this.m_rotationPerlinStart + time * this.m_rotationFrequency * this.m_rotationPerlinVector;
      return (float) ((double) strength * (double) this.m_rotationAmplitude * (2.0 * (double) Mathf.PerlinNoise(vector2.x, vector2.y) - 1.0));
    }
  }
}
