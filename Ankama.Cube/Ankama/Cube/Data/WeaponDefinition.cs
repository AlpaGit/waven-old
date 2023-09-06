// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.WeaponDefinition
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
  public sealed class WeaponDefinition : 
    CharacterDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    [LocalizedString("WEAPON_{id}_NAME", "Weapons", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("WEAPON_{id}_DESCRIPTION", "Weapons", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private God m_god;
    private ILevelOnlyDependant m_playerActionPoints;
    private ILevelOnlyDependant m_maxMechanismsOnBoard;
    private ILevelOnlyDependant m_maxSummoningsOnBoard;
    private List<Id<SpellDefinition>> m_spells;
    private Id<SquadDefinition> m_defaultDeck;
    [SerializeField]
    private AssetReference m_illustrationReference;
    [SerializeField]
    private AssetReference m_femaleIllustrationReference;
    [SerializeField]
    private AssetReference m_fullMaleIllustrationReference;
    [SerializeField]
    private AssetReference m_fullFemaleIllustrationReference;
    [SerializeField]
    private AssetReference m_weaponIllustrationReference;
    [SerializeField]
    private AssetReference m_UIMatchmakingIlluReference;
    [SerializeField]
    private AssetReference m_uiAnimatedCharacterReference;
    [SerializeField]
    private AssetReference m_uiWeaponButtonMatReference;
    [SerializeField]
    private Color m_uiWeaponButtonShineColor;
    [SerializeField]
    private Color m_deckBuildingBackgroundColor;
    [SerializeField]
    private Color m_deckBuildingBackgroundColor2;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public God god => this.m_god;

    public ILevelOnlyDependant playerActionPoints => this.m_playerActionPoints;

    public ILevelOnlyDependant maxMechanismsOnBoard => this.m_maxMechanismsOnBoard;

    public ILevelOnlyDependant maxSummoningsOnBoard => this.m_maxSummoningsOnBoard;

    public IReadOnlyList<Id<SpellDefinition>> spells => (IReadOnlyList<Id<SpellDefinition>>) this.m_spells;

    public Id<SquadDefinition> defaultDeck => this.m_defaultDeck;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_god = (God) Serialization.JsonTokenValue<int>(jsonObject, "god");
      this.m_playerActionPoints = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "playerActionPoints");
      this.m_maxMechanismsOnBoard = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "maxMechanismsOnBoard");
      this.m_maxSummoningsOnBoard = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "maxSummoningsOnBoard");
      JArray jarray = Serialization.JsonArray(jsonObject, "spells");
      this.m_spells = new List<Id<SpellDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray != null)
      {
        foreach (JToken token in jarray)
          this.m_spells.Add(Serialization.JsonTokenIdValue<SpellDefinition>(token));
      }
      this.m_defaultDeck = Serialization.JsonTokenIdValue<SquadDefinition>(jsonObject, "defaultDeck");
    }

    public Color deckBuildingBackgroundColor => this.m_deckBuildingBackgroundColor;

    public Color deckBuildingBackgroundColor2 => this.m_deckBuildingBackgroundColor2;

    public Color deckBuildingWeaponShine => this.m_uiWeaponButtonShineColor;

    public AssetReference GetWeaponIllustrationReference() => this.m_weaponIllustrationReference;

    public AssetReference GetIlluMatchmakingReference() => this.m_UIMatchmakingIlluReference;

    public AssetReference GetIllustrationReference(Gender gender = Gender.Male)
    {
      if (gender == Gender.Male)
        return this.m_illustrationReference;
      if (gender == Gender.Female)
        return this.m_femaleIllustrationReference;
      throw new ArgumentOutOfRangeException(nameof (gender), (object) gender, (string) null);
    }

    public AssetReference GetFullIllustrationReference(Gender gender = Gender.Male)
    {
      if (gender == Gender.Male)
        return this.m_fullMaleIllustrationReference;
      if (gender == Gender.Female)
        return this.m_fullFemaleIllustrationReference;
      throw new ArgumentOutOfRangeException(nameof (gender), (object) gender, (string) null);
    }

    public AssetReference GetUIAnimatedCharacterReference(Gender gender = Gender.Male)
    {
      if (gender == Gender.Male || gender == Gender.Female)
        return this.m_uiAnimatedCharacterReference;
      throw new ArgumentOutOfRangeException(nameof (gender), (object) gender, (string) null);
    }

    public AssetReference GetUIWeaponButtonReference() => this.m_uiWeaponButtonMatReference;
  }
}
