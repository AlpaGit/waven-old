// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LightsHandler
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class LightsHandler
  {
    public const int MaxConstantLocalLights = 4;
    public const int MaxVisibleLocalLights = 16;
    public const int MaxVertexLights = 4;
    private Vector4 k_DefaultLightPosition = new Vector4(0.0f, 0.0f, 1f, 0.0f);
    private Vector4 k_DefaultLightColor = (Vector4) Color.black;
    private Vector4 k_DefaultLightAttenuation = new Vector4(0.0f, 1f, 0.0f, 1f);
    private Vector4 k_DefaultLightSpotDirection = new Vector4(0.0f, 0.0f, 1f, 0.0f);
    private Vector4 k_DefaultLightSpotAttenuation = new Vector4(0.0f, 1f, 0.0f, 0.0f);
    private Vector4[] m_lightPositions;
    private Vector4[] m_lightColors;
    private Vector4[] m_lightDistanceAttenuations;
    private Vector4[] m_lightSpotDirections;
    private Vector4[] m_lightSpotAttenuations;
    private ComputeBuffer m_perObjectLightIndicesComputeBuffer;

    public int maxSupportedLocalLightsPerPass { get; private set; }

    public bool useComputeBufferForPerObjectLightIndices { get; private set; }

    public int mainLightIndex { get; private set; }

    public SRPLight mainLightAdditionnalData { get; private set; }

    public List<int> localLightIndex { get; private set; }

    public int vertexLightsCount { get; private set; }

    public int pixelLightsCount { get; private set; }

    public LightsHandler()
    {
      int length = 16;
      this.m_lightPositions = new Vector4[length];
      this.m_lightColors = new Vector4[length];
      this.m_lightDistanceAttenuations = new Vector4[length];
      this.m_lightSpotDirections = new Vector4[length];
      this.m_lightSpotAttenuations = new Vector4[length];
      this.localLightIndex = new List<int>();
      this.useComputeBufferForPerObjectLightIndices = SystemInfo.supportsComputeShaders && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLCore && !Application.isMobilePlatform && Application.platform != RuntimePlatform.WebGLPlayer;
      this.maxSupportedLocalLightsPerPass = this.useComputeBufferForPerObjectLightIndices ? 16 : 4;
    }

    public void Dispose()
    {
      if (this.m_perObjectLightIndicesComputeBuffer == null)
        return;
      this.m_perObjectLightIndicesComputeBuffer.Release();
      this.m_perObjectLightIndicesComputeBuffer = (ComputeBuffer) null;
    }

    public void Setup(ref CullResults cullResults)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      List<VisibleLight> visibleLights = cullResults.visibleLights;
      this.localLightIndex.Clear();
      for (int index = 0; index < 16; ++index)
        this.InitializeLightConstants(visibleLights, -1, out this.m_lightPositions[index], out this.m_lightColors[index], out this.m_lightDistanceAttenuations[index], out this.m_lightSpotDirections[index], out this.m_lightSpotAttenuations[index]);
      this.mainLightIndex = this.GetMainLight(visibleLights);
      this.mainLightAdditionnalData = (SRPLight) null;
      if (this.mainLightIndex >= 0)
        this.mainLightAdditionnalData = visibleLights[this.mainLightIndex].light.GetComponent<SRPLight>();
      int num = this.mainLightIndex >= 0 ? 1 : 0;
      int val1 = visibleLights.Count - num;
      this.pixelLightsCount = Math.Min(val1, qualitySettings.maxPixelLights);
      this.pixelLightsCount = Math.Min(this.pixelLightsCount, this.maxSupportedLocalLightsPerPass);
      this.vertexLightsCount = qualitySettings.vertexLights ? val1 - this.pixelLightsCount : 0;
      this.vertexLightsCount = Math.Min(this.vertexLightsCount, 4);
      if (this.pixelLightsCount + this.vertexLightsCount <= 0)
        return;
      for (int index = 0; index < visibleLights.Count && this.localLightIndex.Count < 16; ++index)
      {
        if (visibleLights[index].lightType != UnityEngine.LightType.Directional)
        {
          int count = this.localLightIndex.Count;
          this.InitializeLightConstants(visibleLights, index, out this.m_lightPositions[count], out this.m_lightColors[count], out this.m_lightDistanceAttenuations[count], out this.m_lightSpotDirections[count], out this.m_lightSpotAttenuations[count]);
          this.localLightIndex.Add(index);
        }
      }
      this.SetupPerObjectLightIndices(ref cullResults);
    }

    public void SetShaderConstants(CommandBuffer cmd, ref RenderingData renderingData)
    {
      int mainLightIndex = renderingData.lightHandler.mainLightIndex;
      List<VisibleLight> visibleLights = renderingData.cullResult.visibleLights;
      LightZone lightZone = (LightZone) null;
      Vector4 lightPos;
      Vector4 lightColor;
      this.InitializeLightConstants(visibleLights, mainLightIndex, out lightPos, out lightColor, out Vector4 _, out Vector4 _, out Vector4 _);
      if ((UnityEngine.Object) this.mainLightAdditionnalData != (UnityEngine.Object) null)
      {
        lightPos = (Vector4) -this.mainLightAdditionnalData.direction;
        lightZone = this.mainLightAdditionnalData.lightZone;
      }
      else
        lightColor = (Vector4) RenderSettings.ambientLight;
      cmd.SetGlobalVector(PerCameraBuffer._MainLightPosition, lightPos);
      cmd.SetGlobalVector(PerCameraBuffer._MainLightColor, lightColor * CubeSRP.GetLightIntensityFactor());
      if ((UnityEngine.Object) lightZone != (UnityEngine.Object) null && lightZone.isActiveAndEnabled)
      {
        CoreUtils.SetKeyword(cmd, "_MAIN_LIGHT_ZONE", true);
        cmd.SetGlobalVector(PerCameraBuffer._MainLightZoneParams1, lightZone.params1);
        cmd.SetGlobalVector(PerCameraBuffer._MainLightZoneParams2, lightZone.params2);
      }
      else
        CoreUtils.SetKeyword(cmd, "_MAIN_LIGHT_ZONE", false);
      cmd.SetGlobalVectorArray(PerCameraBuffer._AdditionalLightPosition, this.m_lightPositions);
      cmd.SetGlobalVectorArray(PerCameraBuffer._AdditionalLightColor, this.m_lightColors);
      cmd.SetGlobalVectorArray(PerCameraBuffer._AdditionalLightDistanceAttenuation, this.m_lightDistanceAttenuations);
      cmd.SetGlobalVectorArray(PerCameraBuffer._AdditionalLightSpotDir, this.m_lightSpotDirections);
      cmd.SetGlobalVectorArray(PerCameraBuffer._AdditionalLightSpotAttenuation, this.m_lightSpotAttenuations);
      if (this.localLightIndex.Count > 0)
      {
        cmd.SetGlobalVector(PerCameraBuffer._AdditionalLightCount, new Vector4((float) this.pixelLightsCount, (float) this.localLightIndex.Count, 0.0f, 0.0f));
        if (this.m_perObjectLightIndicesComputeBuffer != null)
          cmd.SetGlobalBuffer(PerCameraBuffer._LightIndexBuffer, this.m_perObjectLightIndicesComputeBuffer);
        CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHTS", true);
        CoreUtils.SetKeyword(cmd, "_VERTEX_LIGHTS", this.vertexLightsCount > 0);
      }
      else
      {
        cmd.SetGlobalVector(PerCameraBuffer._AdditionalLightCount, Vector4.zero);
        CoreUtils.SetKeyword(cmd, "_ADDITIONAL_LIGHTS", false);
        CoreUtils.SetKeyword(cmd, "_VERTEX_LIGHTS", false);
      }
    }

    private void SetupPerObjectLightIndices(ref CullResults cullResults)
    {
      List<VisibleLight> visibleLights = cullResults.visibleLights;
      int[] lightIndexMap = cullResults.GetLightIndexMap();
      int num = 0;
      for (int index = 0; index < visibleLights.Count; ++index)
      {
        if (visibleLights[index].lightType == UnityEngine.LightType.Directional)
        {
          lightIndexMap[index] = -1;
          ++num;
        }
        else
          lightIndexMap[index] -= num;
      }
      cullResults.SetLightIndexMap(lightIndexMap);
      if (!this.useComputeBufferForPerObjectLightIndices)
        return;
      int lightIndicesCount = cullResults.GetLightIndicesCount();
      if (lightIndicesCount <= 0)
        return;
      if (this.m_perObjectLightIndicesComputeBuffer == null)
        this.m_perObjectLightIndicesComputeBuffer = new ComputeBuffer(lightIndicesCount, 4);
      else if (this.m_perObjectLightIndicesComputeBuffer.count < lightIndicesCount)
      {
        this.m_perObjectLightIndicesComputeBuffer.Release();
        this.m_perObjectLightIndicesComputeBuffer = new ComputeBuffer(lightIndicesCount, 4);
      }
      cullResults.FillLightIndices(this.m_perObjectLightIndicesComputeBuffer);
    }

    private void InitializeLightConstants(
      List<VisibleLight> lights,
      int lightIndex,
      out Vector4 lightPos,
      out Vector4 lightColor,
      out Vector4 lightDistanceAttenuation,
      out Vector4 lightSpotDir,
      out Vector4 lightSpotAttenuation)
    {
      lightPos = this.k_DefaultLightPosition;
      lightColor = this.k_DefaultLightColor;
      lightDistanceAttenuation = this.k_DefaultLightSpotAttenuation;
      lightSpotDir = this.k_DefaultLightSpotDirection;
      lightSpotAttenuation = this.k_DefaultLightAttenuation;
      if (lightIndex < 0)
        return;
      VisibleLight light = lights[lightIndex];
      if (light.lightType == UnityEngine.LightType.Directional)
      {
        Vector4 vector4 = -light.localToWorld.GetColumn(2);
        lightPos = new Vector4(vector4.x, vector4.y, vector4.z, 0.0f);
      }
      else
      {
        Vector4 column = light.localToWorld.GetColumn(3);
        lightPos = new Vector4(column.x, column.y, column.z, 1f);
      }
      lightColor = (Vector4) light.finalColor;
      if (light.lightType != UnityEngine.LightType.Directional)
      {
        float num1 = light.range * light.range;
        float num2 = 0.640000045f * num1 - num1;
        float y = 1f / num2;
        float z = -num1 / num2;
        float x = 25f / num1;
        lightDistanceAttenuation = new Vector4(x, y, z, 1f);
      }
      if (light.lightType != UnityEngine.LightType.Spot)
        return;
      Vector4 column1 = light.localToWorld.GetColumn(2);
      lightSpotDir = new Vector4(-column1.x, -column1.y, -column1.z, 0.0f);
      float num = Mathf.Cos((float) (Math.PI / 180.0 * (double) light.spotAngle * 0.5));
      float x1 = 1f / Mathf.Max(1f / 1000f, (!((UnityEngine.Object) light.light != (UnityEngine.Object) null) ? Mathf.Cos((float) (2.0 * (double) Mathf.Atan((float) ((double) Mathf.Tan((float) ((double) light.spotAngle * 0.5 * (Math.PI / 180.0))) * 46.0 / 64.0)) * 0.5)) : Mathf.Cos(LightmapperUtils.ExtractInnerCone(light.light) * 0.5f)) - num);
      float y1 = -num * x1;
      lightSpotAttenuation = new Vector4(x1, y1, 0.0f);
    }

    private int GetMainLight(List<VisibleLight> visibleLights)
    {
      int count = visibleLights.Count;
      if (count == 0)
        return -1;
      for (int index = 0; index < count; ++index)
      {
        VisibleLight visibleLight = visibleLights[index];
        if (!((UnityEngine.Object) visibleLight.light == (UnityEngine.Object) null))
        {
          if (visibleLight.lightType == UnityEngine.LightType.Directional)
            return index;
        }
        else
          break;
      }
      return -1;
    }
  }
}
