// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.HeroCharacterObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [UsedImplicitly]
  public sealed class HeroCharacterObject : FightCharacterObject
  {
    [SerializeField]
    private WeaponDefinition m_definition;

    public override IsoObjectDefinition definition
    {
      get => (IsoObjectDefinition) this.m_definition;
      protected set => this.m_definition = (WeaponDefinition) value;
    }

    public override int GetTitleKey() => this.m_definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public override KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;
  }
}
