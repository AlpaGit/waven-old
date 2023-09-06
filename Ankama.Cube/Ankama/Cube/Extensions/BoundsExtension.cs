// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.BoundsExtension
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class BoundsExtension
  {
    public static void GetPoints(this Bounds bounds, ref Vector3[] points)
    {
      Vector3 extents = bounds.extents;
      Vector3 center = bounds.center;
      float x = extents.x;
      float y = extents.y;
      float z = extents.z;
      points[0] = center + new Vector3(-x, -y, -z);
      points[1] = center + new Vector3(-x, y, -z);
      points[2] = center + new Vector3(x, y, -z);
      points[3] = center + new Vector3(x, -y, -z);
      points[4] = center + new Vector3(-x, -y, z);
      points[5] = center + new Vector3(-x, y, z);
      points[6] = center + new Vector3(x, y, z);
      points[7] = center + new Vector3(x, -y, z);
    }

    public static Bounds GetScreenBounds(this Bounds worldBounds, [NotNull] Camera camera)
    {
      Vector3 min = worldBounds.min;
      Vector3 max = worldBounds.max;
      Vector3 screenPoint1 = camera.WorldToScreenPoint(min);
      Vector3 screenPoint2 = camera.WorldToScreenPoint(max);
      Vector3 screenPoint3 = camera.WorldToScreenPoint(new Vector3(min.x, 0.0f, max.z));
      Vector3 screenPoint4 = camera.WorldToScreenPoint(new Vector3(max.x, 0.0f, min.z));
      float x1 = Mathf.Min(screenPoint1.x, screenPoint2.x, screenPoint3.x, screenPoint4.x);
      float x2 = Mathf.Max(screenPoint1.x, screenPoint2.x, screenPoint3.x, screenPoint4.x);
      float y1 = Mathf.Min(screenPoint1.y, screenPoint2.y, screenPoint3.y, screenPoint4.y);
      float y2 = Mathf.Max(screenPoint1.y, screenPoint2.y, screenPoint3.y, screenPoint4.y);
      Bounds screenBounds = new Bounds();
      screenBounds.SetMinMax(new Vector3(x1, y1, 0.0f), new Vector3(x2, y2, 0.0f));
      return screenBounds;
    }

    public static bool Contains(this Bounds bounds, Bounds value) => bounds.Contains(value.min) && bounds.Contains(value.max);

    public static Vector3 ProjectPoint(this Bounds bounds, Vector3 point, Vector3 origin)
    {
      if (bounds.Contains(point))
        return point;
      Ray ray = new Ray(origin, origin - point);
      float distance;
      return !bounds.IntersectRay(ray, out distance) ? bounds.ClosestPoint(point) : ray.GetPoint(distance);
    }
  }
}
