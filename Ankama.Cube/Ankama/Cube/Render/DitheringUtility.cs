// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.DitheringUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Render
{
  public static class DitheringUtility
  {
    public static float GetDitheringLevel(float value, DitheringUtility.Mode mode) => mode == DitheringUtility.Mode.Ceil ? Mathf.Ceil(value * 15f) / 16f : Mathf.Floor(value * 15f) / 16f;

    public enum Mode
    {
      Floor,
      Ceil,
    }
  }
}
