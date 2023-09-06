// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DirectionalShadowConstantBuffer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class DirectionalShadowConstantBuffer
  {
    public static readonly int _WorldToShadow = Shader.PropertyToID(nameof (_WorldToShadow));
    public static readonly int _ShadowData = Shader.PropertyToID(nameof (_ShadowData));
    public static readonly int _ShadowOffset0 = Shader.PropertyToID(nameof (_ShadowOffset0));
    public static readonly int _ShadowOffset1 = Shader.PropertyToID(nameof (_ShadowOffset1));
    public static readonly int _ShadowOffset2 = Shader.PropertyToID(nameof (_ShadowOffset2));
    public static readonly int _ShadowOffset3 = Shader.PropertyToID(nameof (_ShadowOffset3));
    public static readonly int _ShadowmapSize = Shader.PropertyToID(nameof (_ShadowmapSize));
  }
}
