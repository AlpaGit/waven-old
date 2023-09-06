// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ObjectMechanismDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ObjectMechanismDefinition : 
    MechanismDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    [LocalizedString("OBJECT_MECHANISM_{id}_NAME", "Mechanisms", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("OBJECT_MECHANISM_{id}_DESCRIPTION", "Mechanisms", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private ILevelOnlyDependant m_baseMecaLife;
    [SerializeField]
    private AssetReference m_illustrationReference;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public ILevelOnlyDependant baseMecaLife => this.m_baseMecaLife;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_baseMecaLife = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "baseMecaLife");
    }

    public AssetReference illustrationReference => this.m_illustrationReference;
  }
}
