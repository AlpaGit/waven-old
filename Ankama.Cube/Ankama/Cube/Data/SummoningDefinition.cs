// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SummoningDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SummoningDefinition : 
    CharacterDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    [LocalizedString("SUMMONINGS_{id}_NAME", "Summonings", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("SUMMONINGS_{id}_DESCRIPTION", "Summonings", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private List<Cost> m_cost;
    private SummonSelection m_growInto;
    [SerializeField]
    private AssetReference m_illustrationReference;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public IReadOnlyList<Cost> cost => (IReadOnlyList<Cost>) this.m_cost;

    public SummonSelection growInto => this.m_growInto;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "cost");
      this.m_cost = new List<Cost>(jarray != null ? jarray.Count : 0);
      if (jarray != null)
      {
        foreach (JToken token in jarray)
          this.m_cost.Add(Cost.FromJsonToken(token));
      }
      this.m_growInto = SummonSelection.FromJsonProperty(jsonObject, "growInto", (SummonSelection) null);
    }

    public AssetReference illustrationReference => this.m_illustrationReference;
  }
}
