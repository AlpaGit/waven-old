// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GodDefinition
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
  public sealed class GodDefinition : EditableData, IDefinitionWithPrecomputedData
  {
    private God m_god;
    [LocalizedString("GOD_{id}_NAME", "Gods", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("GOD_{id}_DESCRIPTION", "Gods", 1)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private PrecomputedData m_precomputedData;
    private Id<WeaponDefinition> m_defaultWeapon;
    private List<AbstractEffectDefinition> m_heroEffects;
    private bool? m_playable;
    [SerializeField]
    private AssetReference m_godIconReference;
    [SerializeField]
    private AssetReference m_statueUIReference;
    [SerializeField]
    private AssetReference m_BGVisualReference;
    [SerializeField]
    private int m_order;
    [SerializeField]
    private Color m_deckBuildingBackgroundColor2;
    [SerializeField]
    private AssetReference m_statuePrefabReference;

    public God god => this.m_god;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public PrecomputedData precomputedData => this.m_precomputedData;

    public Id<WeaponDefinition> defaultWeapon => this.m_defaultWeapon;

    public IReadOnlyList<AbstractEffectDefinition> heroEffects => (IReadOnlyList<AbstractEffectDefinition>) this.m_heroEffects;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_god = (God) Serialization.JsonTokenValue<int>(jsonObject, "god");
      this.m_precomputedData = PrecomputedData.FromJsonProperty(jsonObject, "precomputedData");
      this.m_defaultWeapon = Serialization.JsonTokenIdValue<WeaponDefinition>(jsonObject, "defaultWeapon");
      JArray jarray = Serialization.JsonArray(jsonObject, "heroEffects");
      this.m_heroEffects = new List<AbstractEffectDefinition>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_heroEffects.Add(AbstractEffectDefinition.FromJsonToken(token));
    }

    public int Order => this.m_order;

    public bool playable
    {
      get
      {
        if (!this.m_playable.HasValue)
          this.m_playable = new bool?(RuntimeData.IsPlayable(this.m_god));
        return this.m_playable.Value;
      }
    }

    public AssetReference GetUIIconReference() => this.m_godIconReference;

    public AssetReference GetUIBGReference() => this.m_BGVisualReference;

    public AssetReference GetUIStatueReference() => this.m_statueUIReference;

    public AssetReference statuePrefabReference => this.m_statuePrefabReference;
  }
}
