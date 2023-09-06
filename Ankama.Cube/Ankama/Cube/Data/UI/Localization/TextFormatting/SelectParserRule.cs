// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.SelectParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class SelectParserRule : IParserRule
  {
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
        if (reader.Read("select"))
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
      while (reader.hasNext)
      {
        reader.SkipSpaces();
        int num = reader.Read(value) ? 1 : 0;
        SubString text = reader.ReadContent('[', ']');
        if (num != 0)
        {
          text.Trim();
          formatterParams.formatter.AppendFormat(text, output, formatterParams);
          break;
        }
      }
    }
  }
}
