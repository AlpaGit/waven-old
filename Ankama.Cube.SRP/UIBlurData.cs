// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.UIBlurData
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public class UIBlurData : ScriptableObject
  {
    [SerializeField]
    [Range(0.0f, 2f)]
    public int downsample = 1;
    [SerializeField]
    [Range(0.0f, 10f)]
    public float blurSize = 2f;
    [SerializeField]
    [Range(1f, 4f)]
    public int blurIterations = 2;
    [SerializeField]
    public UIBlurData.BlurType blurType;

    public enum BlurType
    {
      StandardGauss,
      SgxGauss,
    }
  }
}
