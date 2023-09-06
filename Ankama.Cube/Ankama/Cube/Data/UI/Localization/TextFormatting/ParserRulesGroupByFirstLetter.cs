// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ParserRulesGroupByFirstLetter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class ParserRulesGroupByFirstLetter : IParserRule
  {
    private readonly char[] m_firstLetters;
    private readonly IParserRuleWithPrefix[][] m_rulesByLetters;

    public ParserRulesGroupByFirstLetter(params IParserRuleWithPrefix[] rules)
    {
      Dictionary<char, IGrouping<char, IParserRuleWithPrefix>> dictionary = ((IEnumerable<IParserRuleWithPrefix>) rules).GroupBy<IParserRuleWithPrefix, char>((Func<IParserRuleWithPrefix, char>) (r => r.prefix[0])).ToDictionary<IGrouping<char, IParserRuleWithPrefix>, char>((Func<IGrouping<char, IParserRuleWithPrefix>, char>) (g => g.Key));
      int count = dictionary.Count;
      this.m_firstLetters = new char[count];
      this.m_rulesByLetters = new IParserRuleWithPrefix[count][];
      int index = 0;
      foreach (KeyValuePair<char, IGrouping<char, IParserRuleWithPrefix>> keyValuePair in dictionary)
      {
        this.m_firstLetters[index] = keyValuePair.Key;
        this.m_rulesByLetters[index] = keyValuePair.Value.ToArray<IParserRuleWithPrefix>();
        ++index;
      }
    }

    public bool TryFormat(
      ref StringReader input,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      char current = input.current;
      char[] firstLetters = this.m_firstLetters;
      int length1 = firstLetters.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        if ((int) firstLetters[index1] == (int) current)
        {
          IParserRuleWithPrefix[] rulesByLetter = this.m_rulesByLetters[index1];
          int length2 = rulesByLetter.Length;
          for (int index2 = 0; index2 < length2; ++index2)
          {
            StringReader input1 = input;
            if (rulesByLetter[index2].TryFormat(ref input1, output, formatterParams))
            {
              input = input1;
              return true;
            }
          }
        }
      }
      return false;
    }
  }
}
