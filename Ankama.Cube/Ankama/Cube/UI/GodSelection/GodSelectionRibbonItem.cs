// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GodSelection.GodSelectionRibbonItem
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Utility;

namespace Ankama.Cube.UI.GodSelection
{
  public class GodSelectionRibbonItem : BaseRibbonItem<GodDefinition>
  {
    public override void Initialise(GodDefinition definition)
    {
      base.Initialise(definition);
      this.SetupVisual(this.m_definition.GetUIIconReference(), AssetBundlesUtility.GetUIGodsResourcesBundleName());
    }
  }
}
