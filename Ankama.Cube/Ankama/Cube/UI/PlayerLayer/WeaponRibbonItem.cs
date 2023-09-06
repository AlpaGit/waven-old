// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.WeaponRibbonItem
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class WeaponRibbonItem : BaseRibbonItem<WeaponDefinition>
  {
    [SerializeField]
    private Image m_shine;

    public override void Initialise(WeaponDefinition definition)
    {
      base.Initialise(definition);
      this.SetupVisual(this.m_definition.GetWeaponIllustrationReference(), AssetBundlesUtility.GetUICharacterResourcesBundleName());
      this.SetupEquippedMaterial(this.m_definition.GetUIWeaponButtonReference(), "core/ui/characters/heroes");
      this.m_shine.color = this.m_definition.deckBuildingWeaponShine;
    }

    protected override void UpdateEquipped()
    {
      base.UpdateEquipped();
      this.m_shine.gameObject.SetActive(this.m_equipped);
    }
  }
}
