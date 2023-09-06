// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SetRenderTargetPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class SetRenderTargetPass : AbstractPass
  {
    private const string SetRtTag = "Set RT";
    protected RenderTargetIdentifier m_colorRTI;
    protected RenderTargetIdentifier m_depthRTI;
    private Color m_clearColor;
    private ClearFlag m_clearFlag;

    public void Setup(
      RenderTargetIdentifier colorRTI,
      RenderTargetIdentifier depthRTI,
      ClearFlag clearFlag,
      Color clearColor)
    {
      this.m_colorRTI = colorRTI;
      this.m_depthRTI = depthRTI;
      this.m_clearColor = clearColor;
      this.m_clearFlag = clearFlag;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Set RT");
      RenderBufferLoadAction bufferLoadAction = RenderBufferLoadAction.DontCare;
      RenderBufferStoreAction bufferStoreAction = RenderBufferStoreAction.Store;
      if (this.m_depthRTI != (RenderTargetIdentifier) -1)
        CoreUtils.SetRenderTarget(commandBuffer, this.m_colorRTI, bufferLoadAction, bufferStoreAction, this.m_depthRTI, bufferLoadAction, bufferStoreAction, this.m_clearFlag, this.m_clearColor);
      else
        CoreUtils.SetRenderTarget(commandBuffer, this.m_colorRTI, bufferLoadAction, bufferStoreAction, this.m_clearFlag, this.m_clearColor);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
