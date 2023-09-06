// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.KeywordParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class KeywordParserRule : IParserRule
  {
    public bool TryFormat(
      ref StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if (reader.current != '#')
        return false;
      ++reader.position;
      KeywordParserRule.Format(reader, output, formatterParams);
      return true;
    }

    private static void Format(
      StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      SubString subString = reader.ReadWord();
      reader.SkipSpaces();
      string str;
      SubString text = reader.current != '[' ? (RuntimeData.TryGetText(string.Format("KEYWORD.{0}.NAME", (object) subString), out str) ? (SubString) str : subString) : reader.ReadContent('[', ']');
      output.BeginKeyWord();
      formatterParams.formatter.AppendFormat(text, output, formatterParams);
      output.EndKeyWord();
    }
  }
}
