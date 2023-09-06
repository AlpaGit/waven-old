// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlurShaderProperties
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class BlurShaderProperties
  {
    internal static readonly int _BlurTempRT = Shader.PropertyToID(nameof (_BlurTempRT));
    internal static readonly int _BlurTempRT2 = Shader.PropertyToID(nameof (_BlurTempRT2));
    internal static readonly int _BlurParameter = Shader.PropertyToID("_Parameter");
  }
}
