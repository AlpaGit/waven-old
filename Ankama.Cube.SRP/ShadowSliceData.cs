// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ShadowSliceData
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public struct ShadowSliceData
  {
    public Matrix4x4 viewMatrix;
    public Matrix4x4 projectionMatrix;
    public Matrix4x4 shadowTransform;
    public int offsetX;
    public int offsetY;
    public int resolution;

    public void Clear()
    {
      this.viewMatrix = Matrix4x4.identity;
      this.projectionMatrix = Matrix4x4.identity;
      this.shadowTransform = Matrix4x4.identity;
      this.offsetX = this.offsetY = 0;
      this.resolution = 1024;
    }
  }
}
