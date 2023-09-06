// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.CameraExtension
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class CameraExtension
  {
    public static void GetFrustumPoints(this Camera cam, ref Vector3[] points)
    {
      float nearClipPlane = cam.nearClipPlane;
      float farClipPlane = cam.farClipPlane;
      points[0] = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, nearClipPlane));
      points[1] = cam.ViewportToWorldPoint(new Vector3(0.0f, 1f, nearClipPlane));
      points[2] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, nearClipPlane));
      points[3] = cam.ViewportToWorldPoint(new Vector3(1f, 0.0f, nearClipPlane));
      points[4] = cam.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, farClipPlane));
      points[5] = cam.ViewportToWorldPoint(new Vector3(0.0f, 1f, farClipPlane));
      points[6] = cam.ViewportToWorldPoint(new Vector3(1f, 1f, farClipPlane));
      points[7] = cam.ViewportToWorldPoint(new Vector3(1f, 0.0f, farClipPlane));
    }

    public static void GetFrustumPointsWithUnity(this Camera cam, ref Vector3[] points)
    {
      Vector3[] outCorners1 = new Vector3[4];
      Vector3[] outCorners2 = new Vector3[4];
      cam.CalculateFrustumCorners(new Rect(0.0f, 0.0f, 1f, 1f), cam.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, outCorners1);
      cam.CalculateFrustumCorners(new Rect(0.0f, 0.0f, 1f, 1f), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, outCorners2);
      points[0] = outCorners1[0];
      points[1] = outCorners1[1];
      points[2] = outCorners1[2];
      points[3] = outCorners1[3];
      points[4] = outCorners2[0];
      points[5] = outCorners2[1];
      points[6] = outCorners2[2];
      points[7] = outCorners2[3];
      for (int index = 0; index < points.Length; ++index)
        points[index] = cam.transform.position + cam.transform.TransformDirection(points[index]);
    }

    public static void GetFrustumPointsWithPlanes(this Camera cam, ref Vector3[] points)
    {
      Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(cam);
      points[0] = CameraExtension.Plane3Intersect(frustumPlanes[0], frustumPlanes[2], frustumPlanes[4]);
      points[1] = CameraExtension.Plane3Intersect(frustumPlanes[0], frustumPlanes[3], frustumPlanes[4]);
      points[2] = CameraExtension.Plane3Intersect(frustumPlanes[1], frustumPlanes[3], frustumPlanes[4]);
      points[3] = CameraExtension.Plane3Intersect(frustumPlanes[1], frustumPlanes[2], frustumPlanes[4]);
      points[4] = CameraExtension.Plane3Intersect(frustumPlanes[0], frustumPlanes[2], frustumPlanes[5]);
      points[5] = CameraExtension.Plane3Intersect(frustumPlanes[0], frustumPlanes[3], frustumPlanes[5]);
      points[6] = CameraExtension.Plane3Intersect(frustumPlanes[1], frustumPlanes[3], frustumPlanes[5]);
      points[7] = CameraExtension.Plane3Intersect(frustumPlanes[1], frustumPlanes[2], frustumPlanes[5]);
    }

    public static Vector3 Plane3Intersect(Plane p1, Plane p2, Plane p3) => (-p1.distance * Vector3.Cross(p2.normal, p3.normal) + -p2.distance * Vector3.Cross(p3.normal, p1.normal) + -p3.distance * Vector3.Cross(p1.normal, p2.normal)) / Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal));
  }
}
