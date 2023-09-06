// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders.CastableFightValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders
{
  public class CastableFightValueProvider : AbstractDynamicFightValueProvider
  {
    [NotNull]
    private readonly ICastableStatus m_source;

    public CastableFightValueProvider([NotNull] ICastableStatus source)
      : base(source.GetDefinition().precomputedData.dynamicValueReferences, source.level)
    {
      this.m_source = source;
    }

    public override Tuple<int, int> GetRange()
    {
      ActionRange actionRange = this.m_source.GetDefinition() is CharacterDefinition definition ? definition.actionRange : (ActionRange) null;
      return actionRange == null ? (Tuple<int, int>) null : new Tuple<int, int>(actionRange.min.GetValueWithLevel(this.level), actionRange.max.GetValueWithLevel(this.level));
    }
  }
}
