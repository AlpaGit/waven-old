// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.PlanarReflectionUtility
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class PlanarReflectionUtility
  {
    private static readonly Vector3[] s_viewPortCorners = new Vector3[4]
    {
      new Vector3(0.0f, 0.0f, 0.0f),
      new Vector3(0.0f, 1f, 0.0f),
      new Vector3(1f, 1f, 0.0f),
      new Vector3(1f, 0.0f, 0.0f)
    };

    public static float GetClippedFarClip(Camera cam, Plane plane)
    {
      float num = float.MaxValue;
      bool flag = false;
      for (int index = 0; index < PlanarReflectionUtility.s_viewPortCorners.Length; ++index)
      {
        Ray ray = cam.ViewportPointToRay(PlanarReflectionUtility.s_viewPortCorners[index]);
        float enter;
        if (plane.Raycast(ray, out enter))
        {
          flag = true;
          if ((double) enter < (double) num)
            num = enter;
        }
      }
      return !flag ? cam.nearClipPlane : num + cam.nearClipPlane;
    }

    public static void UpdateReflectionFrustrum(
      ref Camera reflectionCam,
      Camera cam,
      Transform plane,
      float offset = 0.0f,
      float farClip = 1000f)
    {
      Vector3 position = plane.transform.position;
      Vector3 up = plane.transform.up;
      float w = -Vector3.Dot(up, position) - offset;
      Matrix4x4 reflectionMatrix = PlanarReflectionUtility.CalculateReflectionMatrix(Matrix4x4.zero, new Vector4(up.x, up.y, up.z, w));
      reflectionCam.worldToCameraMatrix = cam.worldToCameraMatrix * reflectionMatrix;
      reflectionCam.transform.position = reflectionMatrix.MultiplyPoint(cam.transform.position);
      reflectionCam.transform.rotation = Quaternion.LookRotation(reflectionMatrix.MultiplyVector(cam.transform.forward), reflectionMatrix.MultiplyVector(cam.transform.up));
      float farClipPlane = cam.farClipPlane;
      cam.farClipPlane = farClip;
      Vector4 clipPlane = PlanarReflectionUtility.CalculateClipPlane(reflectionCam, position, up, 1f, offset);
      reflectionCam.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);
      cam.farClipPlane = farClipPlane;
    }

    private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
    {
      reflectionMat.m00 = (float) (1.0 - 2.0 * (double) plane[0] * (double) plane[0]);
      reflectionMat.m01 = -2f * plane[0] * plane[1];
      reflectionMat.m02 = -2f * plane[0] * plane[2];
      reflectionMat.m03 = -2f * plane[3] * plane[0];
      reflectionMat.m10 = -2f * plane[1] * plane[0];
      reflectionMat.m11 = (float) (1.0 - 2.0 * (double) plane[1] * (double) plane[1]);
      reflectionMat.m12 = -2f * plane[1] * plane[2];
      reflectionMat.m13 = -2f * plane[3] * plane[1];
      reflectionMat.m20 = -2f * plane[2] * plane[0];
      reflectionMat.m21 = -2f * plane[2] * plane[1];
      reflectionMat.m22 = (float) (1.0 - 2.0 * (double) plane[2] * (double) plane[2]);
      reflectionMat.m23 = -2f * plane[3] * plane[2];
      reflectionMat.m30 = 0.0f;
      reflectionMat.m31 = 0.0f;
      reflectionMat.m32 = 0.0f;
      reflectionMat.m33 = 1f;
      return reflectionMat;
    }

    private static Vector4 CalculateClipPlane(
      Camera cam,
      Vector3 pos,
      Vector3 normal,
      float sideSign,
      float offset = 0.0f)
    {
      Vector3 point = pos + normal * offset;
      Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
      Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
      Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
      return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
    }
  }
}
