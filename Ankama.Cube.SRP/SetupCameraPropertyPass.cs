// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SetupCameraPropertyPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class SetupCameraPropertyPass : AbstractPass
  {
    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      context.SetupCameraProperties(renderingData.camera, false);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
