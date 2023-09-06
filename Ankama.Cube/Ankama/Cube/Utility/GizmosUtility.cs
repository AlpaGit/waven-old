// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.GizmosUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using UnityEngine;

namespace Ankama.Cube.Utility
{
  public static class GizmosUtility
  {
    public static void DrawArrow(
      Vector3 pos,
      Vector3 direction,
      float arrowLength,
      float arrowHeadLength = 0.3f,
      float arrowHeadAngle = 30f)
    {
      Gizmos.DrawRay(pos, direction * arrowLength);
      Vector3 vector3_1 = Quaternion.LookRotation(direction) * Quaternion.Euler(0.0f, 180f + arrowHeadAngle, 0.0f) * new Vector3(0.0f, 0.0f, 1f);
      Vector3 vector3_2 = Quaternion.LookRotation(direction) * Quaternion.Euler(0.0f, 180f - arrowHeadAngle, 0.0f) * new Vector3(0.0f, 0.0f, 1f);
      Gizmos.DrawRay(pos + direction * arrowLength, vector3_1 * arrowHeadLength);
      Gizmos.DrawRay(pos + direction * arrowLength, vector3_2 * arrowHeadLength);
    }

    public static void DrawTransform(Transform transform)
    {
      GizmosUtility.DrawArrow(transform.position, transform.forward, 4f);
      GizmosUtility.DrawArrow(transform.position, transform.right, 2f);
      GizmosUtility.DrawArrow(transform.position, transform.up, 2f);
    }

    public static void DrawTransform(Vector3 position, Quaternion rotation)
    {
      GizmosUtility.DrawArrow(position, rotation * Vector3.forward, 4f);
      GizmosUtility.DrawArrow(position, rotation * Vector3.right, 2f);
      GizmosUtility.DrawArrow(position, rotation * Vector3.up, 2f);
    }

    public static void DrawFrustrumPoints(Vector3[] frustrumPoints)
    {
      Gizmos.DrawLine(frustrumPoints[0], frustrumPoints[1]);
      Gizmos.DrawLine(frustrumPoints[1], frustrumPoints[2]);
      Gizmos.DrawLine(frustrumPoints[2], frustrumPoints[3]);
      Gizmos.DrawLine(frustrumPoints[3], frustrumPoints[0]);
      Gizmos.DrawLine(frustrumPoints[4], frustrumPoints[5]);
      Gizmos.DrawLine(frustrumPoints[5], frustrumPoints[6]);
      Gizmos.DrawLine(frustrumPoints[6], frustrumPoints[7]);
      Gizmos.DrawLine(frustrumPoints[7], frustrumPoints[4]);
      Gizmos.DrawLine(frustrumPoints[0], frustrumPoints[4]);
      Gizmos.DrawLine(frustrumPoints[1], frustrumPoints[5]);
      Gizmos.DrawLine(frustrumPoints[2], frustrumPoints[6]);
      Gizmos.DrawLine(frustrumPoints[3], frustrumPoints[7]);
    }

    public static void DrawFrustrum(Camera cam)
    {
      Vector3[] points = new Vector3[8];
      cam.GetFrustumPoints(ref points);
      GizmosUtility.DrawFrustrumPoints(points);
    }
  }
}
