// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.TextFormatter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using System.Collections.Concurrent;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class TextFormatter
  {
    private static readonly ConcurrentStack<StringBuilder> s_stringBuildersPool = new ConcurrentStack<StringBuilder>();
    private readonly IParserRule[] m_parserRules;

    public IPluralRules pluralRules { get; set; } = (IPluralRules) new PluralRulesEN();

    public TextFormatter(IParserRule[] parserRules) => this.m_parserRules = parserRules;

    public string Format(string pattern, FormatterParams formatterParams)
    {
      StringBuilder stringBuilder = TextFormatter.GetStringBuilder();
      StringReader reader = new StringReader(pattern);
      this.AppendFormat(ref reader, stringBuilder, formatterParams);
      string str = stringBuilder.ToString();
      TextFormatter.ReleaseStringBuilder(stringBuilder);
      return str;
    }

    public void AppendFormat(SubString text, StringBuilder output, FormatterParams formatterParams)
    {
      StringReader reader = new StringReader(text.originalText);
      reader.position = text.startIndex;
      reader.SetLimit(text.endIndex);
      this.AppendFormat(ref reader, output, formatterParams);
    }

    private void AppendFormat(
      ref StringReader reader,
      StringBuilder writer,
      FormatterParams formatterParams)
    {
      IParserRule[] parserRules = this.m_parserRules;
      int length = this.m_parserRules.Length;
      while (reader.hasNext)
      {
        bool flag = false;
        for (int index = 0; index < length; ++index)
        {
          if (parserRules[index].TryFormat(ref reader, writer, formatterParams))
          {
            flag = true;
            break;
          }
        }
        IParserRule[] additionnalRules = formatterParams.additionnalRules;
        if (additionnalRules != null)
        {
          for (int index = 0; index < additionnalRules.Length; ++index)
          {
            if (additionnalRules[index].TryFormat(ref reader, writer, formatterParams))
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          writer.Append(reader.ReadNext());
      }
    }

    private static StringBuilder GetStringBuilder()
    {
      StringBuilder result;
      if (TextFormatter.s_stringBuildersPool.TryPop(out result))
        result.Clear();
      else
        result = new StringBuilder(512);
      return result;
    }

    private static void ReleaseStringBuilder(StringBuilder sb) => TextFormatter.s_stringBuildersPool.Push(sb);
  }
}
