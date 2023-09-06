// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DirectionalShadowPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class DirectionalShadowPass : AbstractShadowPass
  {
    private const string RenderDirectionalShadowmapTag = "Render Directional Shadowmap";
    private ShadowSliceData m_shadowSliceData;
    private Matrix4x4 m_worldShadowMatrices;
    private Matrix4x4 m_viewMatrix;
    private Matrix4x4 m_projMatrix;
    private Bounds m_bounds;

    public Matrix4x4 viewMatrix => this.m_viewMatrix;

    public Matrix4x4 projMatrix => this.m_projMatrix;

    public Bounds bounds => this.m_bounds;

    protected void Clear() => this.m_worldShadowMatrices = Matrix4x4.identity;

    public bool Setup(ref RenderingData renderingData, ForwardRenderer renderer)
    {
      this.Clear();
      int mainLightIndex = renderingData.lightHandler.mainLightIndex;
      if (mainLightIndex == -1)
        return false;
      CullResults cullResult = renderingData.cullResult;
      VisibleLight visibleLight = cullResult.visibleLights[mainLightIndex];
      Light light = visibleLight.light;
      if (light.shadows == LightShadows.None)
        return false;
      if (visibleLight.lightType != LightType.Directional)
        Debug.LogWarning((object) "Only directional lights are supported as main light.");
      if (!cullResult.GetShadowCasterBounds(mainLightIndex, out this.m_bounds))
        return false;
      int shadowResolution = CubeSRP.qualitySettings.mainShadowResolution;
      float shadowNearPlane = light.shadowNearPlane;
      return cullResult.ComputeDirectionalShadowMatricesAndCullingPrimitives(mainLightIndex, 0, 1, new Vector3(1f, 0.0f, 0.0f), shadowResolution, shadowNearPlane, out this.m_viewMatrix, out this.m_projMatrix, out ShadowSplitData _);
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      LightsHandler lightHandler = renderingData.lightHandler;
      CullResults cullResult = renderingData.cullResult;
      int mainLightIndex = lightHandler.mainLightIndex;
      VisibleLight visibleLight = cullResult.visibleLights[mainLightIndex];
      int shadowResolution = qualitySettings.mainShadowResolution;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Directional Shadowmap");
      using (new ProfilingSample(commandBuffer, "Render Directional Shadowmap", (CustomSampler) null))
      {
        DrawShadowsSettings settings = new DrawShadowsSettings(cullResult, mainLightIndex);
        this.m_shadowmapRT = this.GetShadowRT(commandBuffer, shadowResolution, shadowResolution);
        CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) (Texture) this.m_shadowmapRT, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.Depth, Color.black);
        this.m_shadowSliceData.offsetX = 0;
        this.m_shadowSliceData.offsetY = 0;
        this.m_shadowSliceData.resolution = shadowResolution;
        this.m_shadowSliceData.shadowTransform = LightweightShadowUtils.GetShadowTransform(this.m_projMatrix, this.m_viewMatrix);
        this.m_worldShadowMatrices = this.m_shadowSliceData.shadowTransform;
        this.SetupDirectionalShadowCasterConstants(commandBuffer, qualitySettings.mainShadowBias, qualitySettings.mainShadowNormalBias, ref visibleLight, this.m_projMatrix, (float) shadowResolution);
        LightweightShadowUtils.RenderShadowSlice(commandBuffer, ref context, ref this.m_shadowSliceData, this.m_projMatrix, this.m_viewMatrix, settings);
        this.SetupDirectionalShadowReceiverConstants(commandBuffer, ref visibleLight, lightHandler.mainLightAdditionnalData);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    private void SetupDirectionalShadowCasterConstants(
      CommandBuffer cmd,
      float shadowBias,
      float shadowNormalBias,
      ref VisibleLight visibleLight,
      Matrix4x4 proj,
      float cascadeResolution)
    {
      float num1 = SystemInfo.usesReversedZBuffer ? 1f : -1f;
      float x = shadowBias * proj.m22 * num1;
      double num2 = 2.0 / (double) proj.m00;
      double num3 = 2.0 / (double) proj.m11;
      double num4 = (double) cascadeResolution;
      float num5 = Mathf.Max((float) (num2 / num4), (float) num3 / cascadeResolution);
      float y = (float) (-(double) shadowNormalBias * (double) num5 * 3.6500000953674316);
      Vector3 vector3 = (Vector3) -visibleLight.localToWorld.GetColumn(2);
      cmd.SetGlobalVector(ShadowConstantBuffer._ShadowBias, new Vector4(x, y, 0.0f, 0.0f));
      cmd.SetGlobalVector(ShadowConstantBuffer._LightDirection, new Vector4(vector3.x, vector3.y, vector3.z, 0.0f));
    }

    private void SetupDirectionalShadowReceiverConstants(
      CommandBuffer cmd,
      ref VisibleLight shadowLight,
      SRPLight additionnalLightData)
    {
      float shadowStrength = shadowLight.light.shadowStrength;
      float y1 = (Object) additionnalLightData != (Object) null ? additionnalLightData.cloudShadowStrength : 1f;
      float x1 = 1f / (float) this.m_shadowmapRT.width;
      float y2 = 1f / (float) this.m_shadowmapRT.height;
      float x2 = 0.5f * x1;
      float y3 = 0.5f * y2;
      cmd.SetGlobalTexture(RenderTargetIdentifiers._DirectionalShadowmap, (RenderTargetIdentifier) (Texture) this.m_shadowmapRT);
      cmd.SetGlobalMatrix(DirectionalShadowConstantBuffer._WorldToShadow, this.m_worldShadowMatrices);
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowData, new Vector4(shadowStrength, y1, 0.0f, 0.0f));
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowOffset0, new Vector4(-x2, -y3, 0.0f, 0.0f));
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowOffset1, new Vector4(x2, -y3, 0.0f, 0.0f));
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowOffset2, new Vector4(-x2, y3, 0.0f, 0.0f));
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowOffset3, new Vector4(x2, y3, 0.0f, 0.0f));
      cmd.SetGlobalVector(DirectionalShadowConstantBuffer._ShadowmapSize, new Vector4(x1, y2, (float) this.m_shadowmapRT.width, (float) this.m_shadowmapRT.height));
    }
  }
}
