﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.IAfterCameraRender
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Ankama.Cube.SRP
{
  public interface IAfterCameraRender
  {
    void ExecuteAfterCameraRender(Camera camera, ref ScriptableRenderContext context);
  }
}
