// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReserveDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ReserveDefinition : 
    EditableData,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    private God m_god;
    [LocalizedString("RESERVE_{id}_Name", "Effects", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("RESERVE_{id}_DESCRIPTION", "Effects", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private PrecomputedData m_precomputedData;
    [SerializeField]
    private AssetReference m_illustrationReference;
    [SerializeField]
    private AssetReference m_tooltipIllustrationReference;
    [SerializeField]
    private string m_bundleName;

    public God god => this.m_god;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public PrecomputedData precomputedData => this.m_precomputedData;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_god = (God) Serialization.JsonTokenValue<int>(jsonObject, "god");
      this.m_precomputedData = PrecomputedData.FromJsonProperty(jsonObject, "precomputedData");
    }

    public AssetReference illustrationReference => this.m_illustrationReference;

    public AssetReference tooltipIllustrationReference => this.m_tooltipIllustrationReference;

    public string bundleName => this.m_bundleName;

    public string illustrationBundleName => "core/ui/spells";
  }
}
