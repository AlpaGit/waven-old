// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.PluralParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class PluralParserRule : IParserRule
  {
    private static readonly string[] PluralFormStrings = new string[6]
    {
      "zero",
      "one",
      "two",
      "few",
      "many",
      "other"
    };

    public bool TryFormat(
      ref StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      reader.SkipSpaces();
      SubString subString = reader.ReadWord();
      if (subString.length == 0)
        return false;
      reader.SkipSpaces();
      if (reader.Read(','))
      {
        reader.SkipSpaces();
        if (reader.Read("plural"))
        {
          reader.SkipSpaces();
          if (reader.Read(','))
          {
            this.FormatSelect(formatterParams.valueProvider.GetValue(subString.ToString()), reader, output, formatterParams);
            return true;
          }
        }
      }
      return false;
    }

    private void FormatSelect(
      string value,
      StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if (!string.IsNullOrEmpty(value))
      {
        PluralCategory pluralForOrdinal = formatterParams.formatter.pluralRules.GetPluralForOrdinal(int.Parse(value));
        int position = reader.position;
        while (reader.hasNext)
        {
          reader.SkipSpaces();
          int num = reader.Read(PluralParserRule.PluralFormStrings[(int) pluralForOrdinal]) ? 1 : 0;
          if (num == 0)
            reader.ReadUntil('[');
          SubString text = reader.ReadContent('[', ']');
          if (num != 0)
          {
            PluralParserRule.Append(text, output, formatterParams, value);
            return;
          }
        }
        reader.position = position;
        while (reader.hasNext)
        {
          reader.SkipSpaces();
          int num = reader.Read("other") ? 1 : 0;
          if (num == 0)
            reader.ReadUntil('[');
          SubString text = reader.ReadContent('[', ']');
          if (num != 0)
          {
            PluralParserRule.Append(text, output, formatterParams, value);
            return;
          }
        }
        throw new TextFormatterException(reader.text, reader.position, "'other' content is not defined");
      }
    }

    private static void Append(
      SubString text,
      StringBuilder output,
      FormatterParams formatterParams,
      string value)
    {
      text.Trim();
      formatterParams.additionnalRules = new IParserRule[1]
      {
        (IParserRule) new PluralParserRule.ReplaceNumber(value)
      };
      formatterParams.formatter.AppendFormat(text, output, formatterParams);
    }

    private class ReplaceNumber : IParserRule
    {
      public readonly string value;

      public ReplaceNumber(string value) => this.value = value;

      public bool TryFormat(
        ref StringReader input,
        StringBuilder output,
        FormatterParams formatterParams)
      {
        if (input.current != '#')
          return false;
        output.Append(this.value);
        ++input.position;
        return true;
      }
    }
  }
}
