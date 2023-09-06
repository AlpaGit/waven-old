// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.PCSSConstantBuffer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class PCSSConstantBuffer
  {
    public static readonly int _BlockerSamples = Shader.PropertyToID(nameof (_BlockerSamples));
    public static readonly int _PcfSamples = Shader.PropertyToID("_PCFSamples");
    public static readonly int _Softness = Shader.PropertyToID(nameof (_Softness));
    public static readonly int _BlockerGradientBias = Shader.PropertyToID(nameof (_BlockerGradientBias));
    public static readonly int _PcfGradientBias = Shader.PropertyToID("_PCFGradientBias");
  }
}
