// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SRPUtility
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public static class SRPUtility
  {
    private static Plane[] m_cameraPlanes = new Plane[6];

    public static bool IsBoundsInsideCameraPlanes(Camera cam, Bounds bounds)
    {
      GeometryUtility.CalculateFrustumPlanes(cam, SRPUtility.m_cameraPlanes);
      return GeometryUtility.TestPlanesAABB(SRPUtility.m_cameraPlanes, bounds);
    }

    public static void GetBoundsCorners(Bounds bounds, ref Vector3[] corners)
    {
      Vector3 extents = bounds.extents;
      Vector3 center = bounds.center;
      float x = extents.x;
      float y = extents.y;
      float z = extents.z;
      corners[0] = center + new Vector3(-x, -y, -z);
      corners[1] = center + new Vector3(-x, y, -z);
      corners[2] = center + new Vector3(x, y, -z);
      corners[3] = center + new Vector3(x, -y, -z);
      corners[4] = center + new Vector3(-x, -y, z);
      corners[5] = center + new Vector3(-x, y, z);
      corners[6] = center + new Vector3(x, y, z);
      corners[7] = center + new Vector3(x, -y, z);
    }

    public static Vector3 Plane3Intersect(Plane p1, Plane p2, Plane p3) => (-p1.distance * Vector3.Cross(p2.normal, p3.normal) + -p2.distance * Vector3.Cross(p3.normal, p1.normal) + -p3.distance * Vector3.Cross(p1.normal, p2.normal)) / Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal));

    public static void SetKeyword(string keyword, bool value)
    {
      if (value)
        Shader.EnableKeyword(keyword);
      else
        Shader.DisableKeyword(keyword);
    }

    public static void DestroyRtt(RenderTexture rtt)
    {
      rtt.Release();
      DestroyUtility.Destroy((Object) rtt);
    }

    public static void ReleaseTemporaryRT(CommandBuffer cmd, ref int rt)
    {
      if (rt == -1)
        return;
      cmd.ReleaseTemporaryRT(rt);
      rt = -1;
    }

    public static void SetRenderTarget(
      CommandBuffer cmd,
      RenderTargetIdentifier colorRTI,
      RenderTargetIdentifier depthRTI)
    {
      RenderBufferLoadAction bufferLoadAction = RenderBufferLoadAction.DontCare;
      RenderBufferStoreAction bufferStoreAction = RenderBufferStoreAction.Store;
      if (depthRTI != (RenderTargetIdentifier) -1)
        cmd.SetRenderTarget(colorRTI, bufferLoadAction, bufferStoreAction, depthRTI, bufferLoadAction, bufferStoreAction);
      else
        cmd.SetRenderTarget(colorRTI, bufferLoadAction, bufferStoreAction);
    }

    public static void ClearRenderTarget(CommandBuffer cmd, ClearFlag clearFlag, Color clearColor)
    {
      if (clearFlag == ClearFlag.None)
        return;
      cmd.ClearRenderTarget((clearFlag & ClearFlag.Depth) != 0, (clearFlag & ClearFlag.Color) != 0, clearColor);
    }

    public static void BlitFullscreenTriangle(
      this CommandBuffer cmd,
      RenderTargetIdentifier source,
      RenderTargetIdentifier destination,
      Material mat,
      int pass = 0,
      bool clear = false)
    {
      cmd.SetGlobalTexture(ShaderProperties._MainTex, source);
      cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
      if (clear)
        cmd.ClearRenderTarget(true, true, Color.clear);
      cmd.DrawMesh(CubeSRP.resources.fullscreenQuad, Matrix4x4.identity, mat, 0, pass);
    }
  }
}
