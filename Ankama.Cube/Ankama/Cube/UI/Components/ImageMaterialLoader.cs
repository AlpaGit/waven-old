// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ImageMaterialLoader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public sealed class ImageMaterialLoader : UIResourceLoader<Material>
  {
    [Header("Target")]
    [SerializeField]
    private Image m_image;
    private Material m_previousMaterial;

    protected override IEnumerator Apply(Material material, UIResourceDisplayMode displayMode)
    {
      if ((Object) null == (Object) this.m_image)
      {
        Log.Warning("No image component has been linked.", 24, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\ImageMaterialLoader.cs");
      }
      else
      {
        this.m_previousMaterial = this.m_image.material;
        this.m_image.material = material;
        yield break;
      }
    }

    protected override IEnumerator Revert(UIResourceDisplayMode displayMode)
    {
      if ((Object) null == (Object) this.m_image)
      {
        Log.Warning("No image component has been linked.", 36, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\ImageMaterialLoader.cs");
      }
      else
      {
        this.m_image.material = this.m_previousMaterial;
        yield break;
      }
    }
  }
}
