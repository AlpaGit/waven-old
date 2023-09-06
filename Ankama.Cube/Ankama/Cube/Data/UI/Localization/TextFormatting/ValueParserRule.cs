// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class ValueParserRule : IParserRule
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
      if (reader.hasNext)
        return false;
      string str = formatterParams.valueProvider.GetValue(subString.ToString());
      output.Append(str);
      return true;
    }
  }
}
