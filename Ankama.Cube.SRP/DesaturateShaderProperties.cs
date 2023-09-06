// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DesaturateShaderProperties
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class DesaturateShaderProperties
  {
    internal static readonly int _DesaturateTempRT = Shader.PropertyToID(nameof (_DesaturateTempRT));
    internal static readonly int _DesaturateFactor = Shader.PropertyToID("_Factor");
  }
}
