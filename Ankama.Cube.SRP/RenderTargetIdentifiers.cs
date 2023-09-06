// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.RenderTargetIdentifiers
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class RenderTargetIdentifiers
  {
    public static readonly int _TempBackBuffer = Shader.PropertyToID("_TempBackBufferRT");
    public static readonly int _Color = Shader.PropertyToID("_ColorRT");
    public static readonly int _ColorOpaqueReadable = Shader.PropertyToID("_CameraColorOpaqueTexture");
    public static readonly int _Depth = Shader.PropertyToID("_DepthRT");
    public static readonly int _DepthReadable = Shader.PropertyToID("_CameraDepthTexture");
    public static readonly int _DepthNormals = Shader.PropertyToID("_CameraDepthNormalsTexture");
    public static readonly int _DirectionalShadowmap = Shader.PropertyToID("_DirectionalShadowmapTexture");
    public static readonly int _LocalShadowmap = Shader.PropertyToID("_LocalShadowmapTexture");
    public static readonly int _CloudsShadowmap = Shader.PropertyToID("_CloudsShadowmapTexture");
    public static readonly int _PlanarReflection = Shader.PropertyToID("_PlanarReflectionTexture");
  }
}
