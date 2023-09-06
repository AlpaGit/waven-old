// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.BoundedParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Collections.Generic;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class BoundedParserRule : IParserRule
  {
    private readonly List<IParserRule> m_rules;
    public readonly char open;
    public readonly char close;

    public BoundedParserRule(char open = '{', char close = '}', params IParserRule[] rules)
    {
      this.open = open;
      this.close = close;
      this.m_rules = new List<IParserRule>((IEnumerable<IParserRule>) rules);
    }

    public bool TryFormat(
      ref StringReader input,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if ((int) input.current != (int) this.open)
        return false;
      StringReader stringReader = input.Copy();
      SubString subString = stringReader.ReadContent(this.open, this.close);
      subString.Trim();
      int position = stringReader.position;
      List<IParserRule> rules = this.m_rules;
      int count = rules.Count;
      for (int index = 0; index < count; ++index)
      {
        StringReader input1 = new StringReader(input.text);
        input1.position = subString.startIndex;
        input1.SetLimit(subString.endIndex);
        if (rules[index].TryFormat(ref input1, output, formatterParams))
        {
          input.position = position;
          return true;
        }
      }
      return false;
    }
  }
}
