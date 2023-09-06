// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightDefinition
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
  public sealed class FightDefinition : EditableData
  {
    [LocalizedString("FIGHT_{id}_NAME", "Fight", 1)]
    [SerializeField]
    private int m_i18nNameId;
    private int m_maxSpellInHand;
    private FightType m_fightType;
    private int m_playersPerTeam;
    private int m_fightsCount;
    private bool m_versusAI;
    private IFightAdditionalDataDefinition m_additionalData;
    [SerializeField]
    private AssetReference m_illustrationReference;
    [SerializeField]
    private AssetReference m_fullIllustrationReference;
    [SerializeField]
    private string m_bundleName;

    public int i18nNameId => this.m_i18nNameId;

    public int maxSpellInHand => this.m_maxSpellInHand;

    public FightType fightType => this.m_fightType;

    public int playersPerTeam => this.m_playersPerTeam;

    public int fightsCount => this.m_fightsCount;

    public bool versusAI => this.m_versusAI;

    public IFightAdditionalDataDefinition additionalData => this.m_additionalData;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_maxSpellInHand = Serialization.JsonTokenValue<int>(jsonObject, "maxSpellInHand", 7);
      this.m_fightType = (FightType) Serialization.JsonTokenValue<int>(jsonObject, "fightType");
      this.m_playersPerTeam = Serialization.JsonTokenValue<int>(jsonObject, "playersPerTeam");
      this.m_fightsCount = Serialization.JsonTokenValue<int>(jsonObject, "fightsCount");
      this.m_versusAI = Serialization.JsonTokenValue<bool>(jsonObject, "versusAI");
      this.m_additionalData = IFightAdditionalDataDefinitionUtils.FromJsonProperty(jsonObject, "additionalData");
    }

    public string bundleName => this.m_bundleName;

    public AssetReference illustrationReference => this.m_illustrationReference;

    public AssetReference fullIllustrationReference => this.m_fullIllustrationReference;
  }
}
