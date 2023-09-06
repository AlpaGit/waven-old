// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.AbstractPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public abstract class AbstractPass
  {
    protected List<ShaderPassName> m_shaderPassNames = new List<ShaderPassName>();

    public void RegisterShaderPassName(string passName) => this.m_shaderPassNames.Add(new ShaderPassName(passName));

    public abstract void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer);

    public abstract void ReleaseResources(CommandBuffer cmd);

    public DrawRendererSettings CreateDrawRendererSettings(
      Camera camera,
      SortFlags sortFlags,
      RendererConfiguration rendererConfiguration,
      bool supportsDynamicBatching)
    {
      DrawRendererSettings rendererSettings = new DrawRendererSettings(camera, this.m_shaderPassNames[0]);
      for (int index = 1; index < this.m_shaderPassNames.Count; ++index)
        rendererSettings.SetShaderPassName(index, this.m_shaderPassNames[index]);
      rendererSettings.sorting.flags = sortFlags;
      rendererSettings.rendererConfiguration = rendererConfiguration;
      rendererSettings.flags = DrawRendererFlags.EnableInstancing;
      if (supportsDynamicBatching)
        rendererSettings.flags |= DrawRendererFlags.EnableDynamicBatching;
      return rendererSettings;
    }
  }
}
