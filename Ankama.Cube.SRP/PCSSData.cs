// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.PCSSData
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public class PCSSData : ScriptableObject
  {
    [SerializeField]
    [Range(1f, 64f)]
    private int m_blockerSampleCount = 16;
    [SerializeField]
    [Range(1f, 64f)]
    private int m_pcfSampleCount = 16;
    [SerializeField]
    [Range(0.0f, 10f)]
    private float m_softness = 1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_softnessMin;

    public int blockerSampleCount => this.m_blockerSampleCount;

    public int pcfSampleCount => this.m_pcfSampleCount;

    public float softness => this.m_softness;

    public float softnessMin => this.m_softnessMin;

    public float blockerGradientBias => 0.0f;

    public float pcfGradientBias => 0.0f;
  }
}
