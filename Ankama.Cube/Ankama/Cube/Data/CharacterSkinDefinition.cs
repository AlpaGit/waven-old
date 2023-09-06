// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterSkinDefinition
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
  public sealed class CharacterSkinDefinition : EditableData
  {
    [LocalizedString("CHARACTER_SKIN_{id}_NAME", "CharacterSkins", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [SerializeField]
    private BundleCategory m_bundleCategory;
    [SerializeField]
    private AssetReference m_maleAnimatedCharacterDataReference;
    [SerializeField]
    private AssetReference m_femaleAnimatedCharacterDataReference;

    public int i18nNameId => this.m_i18nNameId;

    public BundleCategory bundleCategory => this.m_bundleCategory;

    public AssetReference maleAnimatedCharacterDataReference => this.m_maleAnimatedCharacterDataReference;

    public AssetReference femaleAnimatedCharacterDataReference => this.m_femaleAnimatedCharacterDataReference;

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public AssetReference GetAnimatedCharacterDataReference(Gender gender)
    {
      if (gender == Gender.Male)
        return this.m_maleAnimatedCharacterDataReference;
      if (gender == Gender.Female)
        return this.m_femaleAnimatedCharacterDataReference;
      throw new ArgumentOutOfRangeException(nameof (gender), (object) gender, (string) null);
    }
  }
}
