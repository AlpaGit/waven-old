// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.LightExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class LightExtensions
  {
    public static float GetSortingWeight(this Light l)
    {
      double num1 = 0.0 + (l.type != LightType.Directional ? 0.0 : 1000.0) + (l.renderMode == LightRenderMode.ForceVertex ? 0.0 : 100.0) + (l.shadows == LightShadows.None ? 0.0 : 10.0);
      Color color = l.color;
      color = color.linear;
      double num2 = (double) color.grayscale * (double) l.intensity;
      return (float) (num1 + num2);
    }

    public static bool HasSoftShadow(this Light l) => QualitySettings.shadows == ShadowQuality.All && l.shadows == LightShadows.Soft;
  }
}
