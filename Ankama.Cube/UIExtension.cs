// Decompiled with JetBrains decompiler
// Type: UIExtension
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

public static class UIExtension
{
  public static T WithAlpha<T>(this T v, float a) where T : Graphic
  {
    Color color = v.color with { a = a };
    v.color = color;
    return v;
  }

  public static T WithRGB<T>(this T v, Color color) where T : Graphic
  {
    color.a = v.color.a;
    v.color = color;
    return v;
  }

  public static void Desaturate<T>(this T v, float desaturationFactor) where T : Graphic => v.material.SetFloat("_DesaturationFactor", desaturationFactor);
}
