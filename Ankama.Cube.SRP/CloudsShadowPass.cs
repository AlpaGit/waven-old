// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CloudsShadowPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class CloudsShadowPass : AbstractPass
  {
    private const string RenderCloudShadowmapTag = "Render Cloud Shadowmap";
    private Camera m_cloudsCamera;
    private Vector3[] m_boundsCorners = new Vector3[8];
    private CullResults m_cloudsCullResults;
    private DirectionalShadowPass m_directionalPass;
    private int m_cloudsRT;

    public CloudsShadowPass()
    {
      this.RegisterShaderPassName("LightweightForward");
      this.RegisterShaderPassName("SRPDefaultUnlit");
      this.CreateCloudsCamera();
    }

    public void RemoveResources()
    {
      if (!((Object) this.m_cloudsCamera != (Object) null))
        return;
      CoreUtils.Destroy((Object) this.m_cloudsCamera.gameObject);
      this.m_cloudsCamera = (Camera) null;
    }

    public override void ReleaseResources(CommandBuffer cmd) => SRPUtility.ReleaseTemporaryRT(cmd, ref this.m_cloudsRT);

    private void CreateCloudsCamera()
    {
      if ((Object) this.m_cloudsCamera != (Object) null)
      {
        Debug.LogError((object) "Calling CreateCamera while camera still exist -> leak");
      }
      else
      {
        this.m_cloudsCamera = new GameObject("CloudsShadowCam").AddComponent<Camera>();
        this.m_cloudsCamera.depthTextureMode = DepthTextureMode.None;
        this.m_cloudsCamera.useOcclusionCulling = false;
        this.m_cloudsCamera.allowMSAA = false;
        this.m_cloudsCamera.allowHDR = false;
        this.m_cloudsCamera.renderingPath = RenderingPath.Forward;
        this.m_cloudsCamera.clearFlags = CameraClearFlags.Color;
        this.m_cloudsCamera.backgroundColor = Color.clear;
        this.m_cloudsCamera.orthographic = true;
        this.m_cloudsCamera.aspect = 1f;
        this.m_cloudsCamera.cullingMask = LayerMask.GetMask("CloudsShadow");
        this.m_cloudsCamera.gameObject.SetActive(false);
        if (!Application.isPlaying)
          return;
        Object.DontDestroyOnLoad((Object) this.m_cloudsCamera.gameObject);
      }
    }

    public void Setup(DirectionalShadowPass directionalPass) => this.m_directionalPass = directionalPass;

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      Bounds bounds = this.m_directionalPass.bounds;
      Matrix4x4 viewMatrix = this.m_directionalPass.viewMatrix;
      Matrix4x4 projMatrix = this.m_directionalPass.projMatrix;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Cloud Shadowmap");
      using (new ProfilingSample(commandBuffer, "Render Cloud Shadowmap", (CustomSampler) null))
      {
        SRPUtility.GetBoundsCorners(bounds, ref this.m_boundsCorners);
        for (int index = 0; index < 8; ++index)
          this.m_boundsCorners[index] = viewMatrix.MultiplyPoint3x4(this.m_boundsCorners[index]);
        FrustumPlanes decomposeProjection = projMatrix.decomposeProjection;
        float a = decomposeProjection.zNear;
        float num = SystemInfo.usesReversedZBuffer ? -1f : 1f;
        for (int index = 0; index < this.m_boundsCorners.Length; ++index)
          a = Mathf.Min(a, num * this.m_boundsCorners[index].z);
        decomposeProjection.zNear = a;
        Matrix4x4 proj = Matrix4x4.Ortho(decomposeProjection.left, decomposeProjection.right, decomposeProjection.bottom, decomposeProjection.top, decomposeProjection.zNear, decomposeProjection.zFar);
        this.m_cloudsCamera.projectionMatrix = proj;
        this.m_cloudsCamera.worldToCameraMatrix = viewMatrix;
        ScriptableCullingParameters cullingParameters;
        if (!CullResults.GetCullingParameters(this.m_cloudsCamera, false, out cullingParameters))
        {
          CommandBufferPool.Release(commandBuffer);
          return;
        }
        cullingParameters.shadowDistance = 0.0f;
        cullingParameters.cullingFlags = CullFlag.None;
        CullResults.Cull(ref cullingParameters, context, ref this.m_cloudsCullResults);
        context.SetupCameraProperties(this.m_cloudsCamera);
        RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
        {
          width = qualitySettings.cloudShadowResolution
        };
        baseRtDescriptor.height = baseRtDescriptor.width;
        baseRtDescriptor.colorFormat = RenderTextureFormat.ARGB32;
        baseRtDescriptor.depthBufferBits = 0;
        baseRtDescriptor.msaaSamples = 1;
        this.m_cloudsRT = RenderTargetIdentifiers._CloudsShadowmap;
        commandBuffer.GetTemporaryRT(this.m_cloudsRT, baseRtDescriptor, FilterMode.Bilinear);
        CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) this.m_cloudsRT, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.All, Color.clear);
        commandBuffer.SetViewport(new Rect(0.0f, 0.0f, (float) baseRtDescriptor.width, (float) baseRtDescriptor.height));
        commandBuffer.SetViewProjectionMatrices(viewMatrix, proj);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        DrawRendererSettings rendererSettings = this.CreateDrawRendererSettings(this.m_cloudsCamera, SortFlags.CommonTransparent, RendererConfiguration.None, qualitySettings.dynamicBatching);
        context.DrawRenderers(this.m_cloudsCullResults.visibleRenderers, ref rendererSettings, Filters.clouds);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }
  }
}
