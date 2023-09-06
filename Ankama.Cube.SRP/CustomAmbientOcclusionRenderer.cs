// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CustomAmbientOcclusionRenderer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public sealed class CustomAmbientOcclusionRenderer : 
    PostProcessEffectRenderer<CustomAmbientOcclusion>
  {
    private readonly int[] m_SampleCount = new int[5]
    {
      4,
      6,
      10,
      8,
      12
    };
    private Shader m_aoShader;

    public override void Init()
    {
      base.Init();
      this.m_aoShader = CubeSRP.resources.aoShader;
    }

    public override void Render(PostProcessRenderContext context)
    {
      CommandBuffer command = context.command;
      command.BeginSample("Ambient Occlusion");
      PropertySheet propertySheet = context.propertySheets.Get(this.m_aoShader);
      AmbientOcclusionQuality aoQuality = QualityManager.current.aoQuality;
      this.settings.radius.value = Mathf.Max(this.settings.radius.value, 0.0001f);
      int num1 = aoQuality < AmbientOcclusionQuality.High ? 1 : 0;
      float x = this.settings.intensity.value;
      float y = this.settings.radius.value;
      float z = num1 != 0 ? 0.5f : 1f;
      float w = (float) this.m_SampleCount[(int) aoQuality];
      propertySheet.ClearKeywords();
      propertySheet.properties.SetVector(CustomAmbientOcclusionRenderer.ShaderIDs.AOParams, new Vector4(x, y, z, w));
      propertySheet.properties.SetVector(CustomAmbientOcclusionRenderer.ShaderIDs.AOColor, (Vector4) (Color.white - this.settings.color.value));
      int num2 = num1 != 0 ? 2 : 1;
      int occlusionTexture1 = CustomAmbientOcclusionRenderer.ShaderIDs.OcclusionTexture1;
      int widthOverride = context.width / num2;
      int heightOverride = context.height / num2;
      context.GetScreenSpaceTemporaryRT(command, occlusionTexture1, colorFormat: RenderTextureFormat.ARGB32, readWrite: RenderTextureReadWrite.Linear, widthOverride: widthOverride, heightOverride: heightOverride);
      command.BlitFullscreenTriangle((RenderTargetIdentifier) BuiltinRenderTextureType.None, (RenderTargetIdentifier) occlusionTexture1, propertySheet, 0);
      int occlusionTexture2 = CustomAmbientOcclusionRenderer.ShaderIDs.OcclusionTexture2;
      context.GetScreenSpaceTemporaryRT(command, occlusionTexture2, colorFormat: RenderTextureFormat.ARGB32, readWrite: RenderTextureReadWrite.Linear);
      command.BlitFullscreenTriangle((RenderTargetIdentifier) occlusionTexture1, (RenderTargetIdentifier) occlusionTexture2, propertySheet, 2);
      command.ReleaseTemporaryRT(occlusionTexture1);
      int occlusionTextureResult = CustomAmbientOcclusionRenderer.ShaderIDs.OcclusionTextureResult;
      context.GetScreenSpaceTemporaryRT(command, occlusionTextureResult, colorFormat: RenderTextureFormat.ARGB32, readWrite: RenderTextureReadWrite.Linear);
      command.BlitFullscreenTriangle((RenderTargetIdentifier) occlusionTexture2, (RenderTargetIdentifier) occlusionTextureResult, propertySheet, 4);
      command.ReleaseTemporaryRT(occlusionTexture2);
      if (context.IsDebugOverlayEnabled(DebugOverlay.AmbientOcclusion))
        context.PushDebugOverlay(command, (RenderTargetIdentifier) occlusionTextureResult, propertySheet, 7);
      command.SetGlobalTexture(CustomAmbientOcclusionRenderer.ShaderIDs.SAOcclusionTexture, (RenderTargetIdentifier) occlusionTextureResult);
      command.BlitFullscreenTriangle((RenderTargetIdentifier) BuiltinRenderTextureType.None, context.destination, propertySheet, 5, RenderBufferLoadAction.DontCare);
      command.ReleaseTemporaryRT(occlusionTextureResult);
      command.EndSample("Ambient Occlusion");
    }

    private enum Pass
    {
      OcclusionEstimationForward,
      OcclusionEstimationDeferred,
      HorizontalBlurForward,
      HorizontalBlurDeferred,
      VerticalBlur,
      CompositionForward,
      CompositionDeferred,
      DebugOverlay,
    }

    private static class ShaderIDs
    {
      internal static readonly int AOParams = Shader.PropertyToID("_AOParams");
      internal static readonly int AOColor = Shader.PropertyToID("_AOColor");
      internal static readonly int OcclusionTexture1 = Shader.PropertyToID("_OcclusionTexture1");
      internal static readonly int OcclusionTexture2 = Shader.PropertyToID("_OcclusionTexture2");
      internal static readonly int OcclusionTextureResult = Shader.PropertyToID("_OcclusionTextureResult");
      internal static readonly int SAOcclusionTexture = Shader.PropertyToID("_SAOcclusionTexture");
    }
  }
}
