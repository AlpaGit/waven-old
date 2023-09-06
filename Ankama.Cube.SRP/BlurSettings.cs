// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlurSettings
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public struct BlurSettings
  {
    [SerializeField]
    [Range(0.0f, 2f)]
    public int downsample;
    [SerializeField]
    [Range(0.0f, 10f)]
    public float blurSize;
    [SerializeField]
    [Range(1f, 4f)]
    public int blurIterations;
    [SerializeField]
    public BlurType blurType;
    [SerializeField]
    public float factor;
  }
}
