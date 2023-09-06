// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ShaderKeywords
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

namespace Ankama.Cube.SRP
{
  public static class ShaderKeywords
  {
    public const string MainLightZone = "_MAIN_LIGHT_ZONE";
    public const string AdditionalLights = "_ADDITIONAL_LIGHTS";
    public const string VertexLights = "_VERTEX_LIGHTS";
    public const string DirectionalShadows = "_DIR_SHADOWS";
    public const string CloudsShadows = "_CLOUDS_SHADOWS";
    public const string LocalShadows = "_LOCAL_SHADOWS";
    public const string ShadowPCF = "_SHADOW_PCF";
    public const string ShadowPCSS = "_SHADOW_PCSS";
    public const string DepthNoMsaa = "_DEPTH_NO_MSAA";
    public const string DepthMsaa2 = "_DEPTH_MSAA_2";
    public const string DepthMsaa4 = "_DEPTH_MSAA_4";
    public const string Fog = "_FOG";
    public const string FogVertex = "_FOG_VERTEX";
    public const string DoubleSided = "_DOUBLE_SIDED";
    public const string DoubleSidedNoY = "_DOUBLE_SIDED_NO_Y";
    public const string Unlit = "_UNLIT";
    public const string Lightmap = "LIGHTMAP_ON";
    public const string Lightprobe = "LIGHTPROBE_SH";
    public const string CameraDepthTexture = "_CAMERA_DEPTH_TEXTURE";
    public const string CameraDepthNormalTexture = "_CAMERA_DEPTH_NORMAL_TEXTURE";
    public const string CameraColorOpaqueTexture = "_CAMERA_COLOR_OPAQUE_TEXTURE";
    public const string UnityLinearFog = "FOG_LINEAR";
    public const string UnityExpFog = "FOG_EXP";
    public const string UnityExp2Fog = "FOG_EXP2";
    public const string PPStereoInstancing = "STEREO_INSTANCING_ENABLED";
    public const string PPStereoDoubleWideTraget = "STEREO_DOUBLEWIDE_TARGET";
  }
}
