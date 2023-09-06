// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.PlanarReflectionPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class PlanarReflectionPass : AbstractPass
  {
    private const string RenderPlanarReflectionTag = "Render Planar Reflection";
    private SetupShaderConstantPass m_setupShaderConstantPass;
    private ClearColorRenderTargetPass m_clearColorPass;
    private RenderOpaquePass m_opaquePass;
    private RenderTransparentPass m_transparentPass;
    private Camera m_reflectionCamera;
    private Matrix4x4 m_reflectionTransformMatrix;

    public PlanarReflectionPass()
    {
      this.CreateCamera();
      this.m_setupShaderConstantPass = new SetupShaderConstantPass();
      this.m_clearColorPass = new ClearColorRenderTargetPass();
      this.m_opaquePass = new RenderOpaquePass();
      this.m_transparentPass = new RenderTransparentPass();
      this.m_reflectionTransformMatrix = Matrix4x4.Rotate(Quaternion.Euler(0.0f, 0.0f, 180f));
    }

    public void RemoveResources()
    {
      if (!((Object) this.m_reflectionCamera != (Object) null))
        return;
      CoreUtils.Destroy((Object) this.m_reflectionCamera.gameObject);
      this.m_reflectionCamera = (Camera) null;
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
      this.m_setupShaderConstantPass.ReleaseResources(cmd);
      this.m_opaquePass.ReleaseResources(cmd);
      this.m_transparentPass.ReleaseResources(cmd);
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      int count = SRPPlanarReflection.s_renderedPeflectionPlanes.Count;
      for (int index = 0; index < count; ++index)
        this.Execute(SRPPlanarReflection.s_renderedPeflectionPlanes[index], ref context, ref renderingData, renderer);
    }

    private void Execute(
      SRPPlanarReflection reflection,
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      Camera camera = renderingData.camera;
      Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
      float enter;
      if (!new Plane(reflection.transform.up, reflection.transform.position).Raycast(ray, out enter) && (double) enter == 0.0)
        return;
      this.m_reflectionCamera.allowHDR = camera.allowHDR;
      this.m_reflectionCamera.allowMSAA = camera.allowMSAA;
      this.m_reflectionCamera.clearFlags = camera.clearFlags;
      this.m_reflectionCamera.backgroundColor = camera.backgroundColor;
      this.m_reflectionCamera.cullingMask = (int) reflection.cullingMask;
      this.m_reflectionCamera.backgroundColor = this.m_reflectionCamera.backgroundColor with
      {
        a = 0.0f
      };
      Transform transform = reflection.transform;
      float farClip = reflection.farClip;
      switch (reflection.farClipMode)
      {
        case SRPPlanarReflection.FarClipMode.EqualToCamera:
          farClip = camera.farClipPlane;
          break;
        case SRPPlanarReflection.FarClipMode.ManualRelativeToCamera:
          farClip = reflection.farClip;
          break;
        case SRPPlanarReflection.FarClipMode.ManualRelativeToPlane:
          farClip = PlanarReflectionUtility.GetClippedFarClip(camera, new Plane(transform.up, transform.position - transform.up * reflection.farClip));
          break;
      }
      PlanarReflectionUtility.UpdateReflectionFrustrum(ref this.m_reflectionCamera, camera, transform, reflection.clipPlaneOffset, farClip);
      RenderTexture rt = reflection.rt;
      this.m_reflectionCamera.targetTexture = rt;
      RenderingData renderingData1;
      RenderingData.GetRenderingData(this.m_reflectionCamera, CubeSRP.qualitySettings, renderingData.lightHandler, ref renderingData.cullResult, out renderingData1);
      this.m_reflectionCamera.targetTexture = (RenderTexture) null;
      renderingData1.shadowQuality = (ShadowQuality) Mathf.Min((int) qualitySettings.shadowQuality, (int) qualitySettings.reflectionShadowQuality);
      context.SetupCameraProperties(this.m_reflectionCamera, false);
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Planar Reflection");
      using (new ProfilingSample(commandBuffer, "Render Planar Reflection", (CustomSampler) null))
      {
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        CoreUtils.SetKeyword(commandBuffer, ReflectionShaderProperties._IsReflectionKeyword, true);
        commandBuffer.SetGlobalMatrix(ReflectionShaderProperties._ReflectionViewMatrix, camera.worldToCameraMatrix);
        commandBuffer.SetGlobalVector(ReflectionShaderProperties._ReflectionFadeParams, (Vector4) new Vector3(reflection.farClip - reflection.fade, reflection.farClip, 0.0f));
        commandBuffer.SetGlobalVector(ReflectionShaderProperties._ReflectionPlanePos, (Vector4) transform.position);
        commandBuffer.SetGlobalVector(ReflectionShaderProperties._ReflectionPlaneNormal, (Vector4) transform.up);
        commandBuffer.SetGlobalMatrix(ReflectionShaderProperties._ReflectionTransformMatrix, this.m_reflectionTransformMatrix);
        commandBuffer.SetViewProjectionMatrices(this.m_reflectionCamera.worldToCameraMatrix, this.m_reflectionCamera.projectionMatrix);
        bool invertCulling = GL.invertCulling;
        commandBuffer.SetInvertCulling(true);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        this.m_setupShaderConstantPass.Execute(ref context, ref renderingData1, renderer);
        int reflectionMask = LayerMaskNames.reflectionMask;
        int reflectionRenderMask = (int) LayerMaskNames.reflectionRenderMask;
        this.m_clearColorPass.Setup((RenderTargetIdentifier) (Texture) rt, (RenderTargetIdentifier) -1);
        this.m_clearColorPass.Execute(ref context, ref renderingData1, renderer);
        this.m_opaquePass.Setup((RenderTargetIdentifier) (Texture) rt, (RenderTargetIdentifier) -1, reflectionMask, reflectionRenderMask);
        this.m_opaquePass.Execute(ref context, ref renderingData1, renderer);
        this.m_transparentPass.Setup((RenderTargetIdentifier) (Texture) rt, (RenderTargetIdentifier) -1, reflectionMask, reflectionRenderMask);
        this.m_transparentPass.Execute(ref context, ref renderingData1, renderer);
        commandBuffer.SetInvertCulling(invertCulling);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        CoreUtils.SetKeyword(commandBuffer, ReflectionShaderProperties._IsReflectionKeyword, false);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    private void CreateCamera()
    {
      if ((Object) this.m_reflectionCamera != (Object) null)
      {
        Debug.LogError((object) "calling CreateCamera while camera still exist -> leak");
      }
      else
      {
        this.m_reflectionCamera = new GameObject("Reflection Camera").AddComponent<Camera>();
        this.m_reflectionCamera.cameraType = CameraType.Reflection;
        this.m_reflectionCamera.depthTextureMode = DepthTextureMode.None;
        this.m_reflectionCamera.useOcclusionCulling = false;
        this.m_reflectionCamera.allowMSAA = false;
        this.m_reflectionCamera.allowHDR = false;
        this.m_reflectionCamera.renderingPath = RenderingPath.Forward;
        this.m_reflectionCamera.clearFlags = CameraClearFlags.Color;
        this.m_reflectionCamera.backgroundColor = Color.clear;
        this.m_reflectionCamera.orthographic = true;
        this.m_reflectionCamera.aspect = 1f;
        this.m_reflectionCamera.depth = -100f;
        this.m_reflectionCamera.gameObject.SetActive(false);
        if (!Application.isPlaying)
          return;
        Object.DontDestroyOnLoad((Object) this.m_reflectionCamera.gameObject);
      }
    }
  }
}
