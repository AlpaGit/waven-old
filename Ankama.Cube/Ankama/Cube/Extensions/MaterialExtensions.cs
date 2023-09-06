// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.MaterialExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class MaterialExtensions
  {
    public static bool GetBool(this Material mat, string name) => Mathf.RoundToInt(mat.GetFloat(name)) == 1;

    public static bool GetBool(this Material mat, int nameId) => Mathf.RoundToInt(mat.GetFloat(nameId)) == 1;

    public static void SetAlphaColor(this Material mat, float value)
    {
      Color color = mat.color with { a = value };
      mat.color = color;
    }
  }
}
