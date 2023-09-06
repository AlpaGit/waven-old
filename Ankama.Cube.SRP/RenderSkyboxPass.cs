// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.RenderSkyboxPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class RenderSkyboxPass : AbstractPass
  {
    private const string RenderSkyboxTag = "Render Skybox";

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Skybox");
      using (new ProfilingSample(commandBuffer, "Render Skybox", (CustomSampler) null))
      {
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        context.DrawSkybox(renderingData.camera);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
