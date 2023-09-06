// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.PerCameraBuffer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class PerCameraBuffer
  {
    public static readonly int _MainLightZoneParams1 = Shader.PropertyToID(nameof (_MainLightZoneParams1));
    public static readonly int _MainLightZoneParams2 = Shader.PropertyToID(nameof (_MainLightZoneParams2));
    public static readonly int _MainLightPosition = Shader.PropertyToID(nameof (_MainLightPosition));
    public static readonly int _MainLightColor = Shader.PropertyToID(nameof (_MainLightColor));
    public static readonly int _MainLightCookie = Shader.PropertyToID(nameof (_MainLightCookie));
    public static readonly int _WorldToLight = Shader.PropertyToID(nameof (_WorldToLight));
    public static readonly int _AdditionalLightCount = Shader.PropertyToID(nameof (_AdditionalLightCount));
    public static readonly int _AdditionalLightPosition = Shader.PropertyToID(nameof (_AdditionalLightPosition));
    public static readonly int _AdditionalLightColor = Shader.PropertyToID(nameof (_AdditionalLightColor));
    public static readonly int _AdditionalLightDistanceAttenuation = Shader.PropertyToID(nameof (_AdditionalLightDistanceAttenuation));
    public static readonly int _AdditionalLightSpotDir = Shader.PropertyToID(nameof (_AdditionalLightSpotDir));
    public static readonly int _AdditionalLightSpotAttenuation = Shader.PropertyToID(nameof (_AdditionalLightSpotAttenuation));
    public static readonly int _LightIndexBuffer = Shader.PropertyToID(nameof (_LightIndexBuffer));
    public static readonly int _ScaledScreenParams = Shader.PropertyToID(nameof (_ScaledScreenParams));
  }
}
