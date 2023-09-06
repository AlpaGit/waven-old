// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LocalShadowPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class LocalShadowPass : AbstractShadowPass
  {
    private const string RenderLocalShadows = "Render Local Shadows";
    private Matrix4x4[] m_additionnalLightShadowMatrices;
    private ShadowSliceData[] m_additionnalLightSlices;
    private float[] m_localShadowStrength;
    private List<int> m_additionalShadowCastingLightIndex = new List<int>();

    public LocalShadowPass()
    {
      int length = 16;
      this.m_additionnalLightShadowMatrices = new Matrix4x4[length];
      this.m_additionnalLightSlices = new ShadowSliceData[length];
      this.m_localShadowStrength = new float[length];
    }

    protected void Clear()
    {
      for (int index = 0; index < this.m_additionnalLightShadowMatrices.Length; ++index)
        this.m_additionnalLightShadowMatrices[index] = Matrix4x4.identity;
      for (int index = 0; index < this.m_additionnalLightSlices.Length; ++index)
        this.m_additionnalLightSlices[index].Clear();
      for (int index = 0; index < this.m_localShadowStrength.Length; ++index)
        this.m_localShadowStrength[index] = 0.0f;
    }

    public bool Setup(ref RenderingData renderingData, ForwardRenderer renderer)
    {
      this.Clear();
      this.m_additionalShadowCastingLightIndex.Clear();
      List<int> localLightIndex = renderingData.lightHandler.localLightIndex;
      int count = localLightIndex.Count;
      if (count == 0)
        return false;
      CullResults cullResult = renderingData.cullResult;
      List<VisibleLight> visibleLights = cullResult.visibleLights;
      for (int index = 0; index < count; ++index)
      {
        int num = localLightIndex[index];
        VisibleLight visibleLight = visibleLights[num];
        Light light = visibleLight.light;
        Matrix4x4 shadowMatrix;
        Matrix4x4 viewMatrix;
        Matrix4x4 projMatrix;
        if (visibleLight.lightType == LightType.Spot && !((Object) light == (Object) null) && light.shadows != LightShadows.None && cullResult.GetShadowCasterBounds(num, out Bounds _) && LightweightShadowUtils.ExtractSpotLightMatrix(ref cullResult, num, out shadowMatrix, out viewMatrix, out projMatrix))
        {
          this.m_localShadowStrength[index] = light.shadowStrength;
          this.m_additionnalLightSlices[index].viewMatrix = viewMatrix;
          this.m_additionnalLightSlices[index].projectionMatrix = projMatrix;
          this.m_additionnalLightSlices[index].shadowTransform = shadowMatrix;
          this.m_additionalShadowCastingLightIndex.Add(index);
        }
      }
      return this.m_additionalShadowCastingLightIndex.Count > 0;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Local Shadows");
      using (new ProfilingSample(commandBuffer, "Render Local Shadows", (CustomSampler) null))
      {
        CullResults cullResult = renderingData.cullResult;
        List<VisibleLight> visibleLights = cullResult.visibleLights;
        int shadowResolution = CubeSRP.qualitySettings.additionalShadowResolution;
        int resolutionInAtlas = LightweightShadowUtils.GetMaxTileResolutionInAtlas(shadowResolution, shadowResolution, this.m_additionalShadowCastingLightIndex.Count);
        int num1 = shadowResolution / resolutionInAtlas;
        this.m_shadowmapRT = this.GetShadowRT(commandBuffer, shadowResolution, shadowResolution);
        CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) (Texture) this.m_shadowmapRT, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.Depth, Color.black);
        for (int index1 = 0; index1 < this.m_additionalShadowCastingLightIndex.Count; ++index1)
        {
          int index2 = this.m_additionalShadowCastingLightIndex[index1];
          int num2 = renderingData.lightHandler.localLightIndex[index2];
          VisibleLight visibleLight = visibleLights[num2];
          ShadowSliceData additionnalLightSlice = this.m_additionnalLightSlices[index2] with
          {
            offsetX = index1 % num1 * resolutionInAtlas,
            offsetY = index1 / num1 * resolutionInAtlas,
            resolution = resolutionInAtlas
          };
          if (this.m_additionalShadowCastingLightIndex.Count > 1)
            LightweightShadowUtils.ApplySliceTransform(ref additionnalLightSlice, shadowResolution, shadowResolution);
          this.m_additionnalLightShadowMatrices[index2] = additionnalLightSlice.shadowTransform;
          DrawShadowsSettings settings = new DrawShadowsSettings(cullResult, num2);
          LightweightShadowUtils.SetupShadowCasterConstants(commandBuffer, ref visibleLight, additionnalLightSlice.projectionMatrix, (float) additionnalLightSlice.resolution);
          LightweightShadowUtils.RenderShadowSlice(commandBuffer, ref context, ref additionnalLightSlice, additionnalLightSlice.projectionMatrix, additionnalLightSlice.viewMatrix, settings);
        }
        this.SetupLocalLightsShadowReceiverConstants(commandBuffer);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    private void SetupLocalLightsShadowReceiverConstants(CommandBuffer cmd)
    {
      float x1 = 1f / (float) this.m_shadowmapRT.width;
      float y1 = 1f / (float) this.m_shadowmapRT.height;
      float x2 = 0.5f * x1;
      float y2 = 0.5f * y1;
      cmd.SetGlobalTexture(RenderTargetIdentifiers._LocalShadowmap, (RenderTargetIdentifier) (Texture) this.m_shadowmapRT);
      cmd.SetGlobalMatrixArray(LocalShadowConstantBuffer._LocalWorldToShadowAtlas, this.m_additionnalLightShadowMatrices);
      cmd.SetGlobalFloatArray(LocalShadowConstantBuffer._LocalShadowStrength, this.m_localShadowStrength);
      cmd.SetGlobalVector(LocalShadowConstantBuffer._LocalShadowOffset0, new Vector4(-x2, -y2, 0.0f, 0.0f));
      cmd.SetGlobalVector(LocalShadowConstantBuffer._LocalShadowOffset1, new Vector4(x2, -y2, 0.0f, 0.0f));
      cmd.SetGlobalVector(LocalShadowConstantBuffer._LocalShadowOffset2, new Vector4(-x2, y2, 0.0f, 0.0f));
      cmd.SetGlobalVector(LocalShadowConstantBuffer._LocalShadowOffset3, new Vector4(x2, y2, 0.0f, 0.0f));
      cmd.SetGlobalVector(LocalShadowConstantBuffer._LocalShadowmapSize, new Vector4(x1, y1, (float) this.m_shadowmapRT.width, (float) this.m_shadowmapRT.height));
    }
  }
}
