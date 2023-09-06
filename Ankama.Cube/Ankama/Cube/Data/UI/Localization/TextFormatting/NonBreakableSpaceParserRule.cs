// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.NonBreakableSpaceParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class NonBreakableSpaceParserRule : IParserRule
  {
    public const char NonBreakableSpace = ' ';

    public bool TryFormat(ref StringReader input, StringBuilder output, FormatterParams parameters)
    {
      if (input.remaining < 2 || !input.NextEquals("\\_"))
        return false;
      output.Append(' ');
      input.position += 2;
      return true;
    }
  }
}
