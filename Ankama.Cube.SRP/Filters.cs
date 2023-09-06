// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.Filters
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine.Experimental.Rendering;

namespace Ankama.Cube.SRP
{
  public static class Filters
  {
    public static FilterRenderersSettings opaque;
    public static FilterRenderersSettings transparent;
    public static FilterRenderersSettings clouds;

    static Filters()
    {
      FilterRenderersSettings renderersSettings = new FilterRenderersSettings(true);
      renderersSettings.renderQueueRange = RenderQueueRange.opaque;
      Filters.opaque = renderersSettings;
      renderersSettings = new FilterRenderersSettings(true);
      renderersSettings.renderQueueRange = RenderQueueRange.transparent;
      Filters.transparent = renderersSettings;
      renderersSettings = new FilterRenderersSettings(true);
      renderersSettings.renderQueueRange = RenderQueueRange.all;
      renderersSettings.layerMask = LayerMaskNames.cloudsMask;
      Filters.clouds = renderersSettings;
    }
  }
}
