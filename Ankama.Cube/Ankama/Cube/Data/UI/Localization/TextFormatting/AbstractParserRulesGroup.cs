// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.AbstractParserRulesGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public abstract class AbstractParserRulesGroup : IParserRule
  {
    private readonly List<IParserRule> m_rules;

    protected AbstractParserRulesGroup(params IParserRule[] rules) => this.m_rules = new List<IParserRule>((IEnumerable<IParserRule>) rules);

    public bool TryFormat(
      ref StringReader input,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if (!this.MatchGroup(input))
        return false;
      List<IParserRule> rules = this.m_rules;
      int count = rules.Count;
      for (int index = 0; index < count; ++index)
      {
        if (rules[index].TryFormat(ref input, output, formatterParams))
          return true;
      }
      return false;
    }

    protected abstract bool MatchGroup(StringReader input);
  }
}
