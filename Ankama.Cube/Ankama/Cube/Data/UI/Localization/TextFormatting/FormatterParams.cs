// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.FormatterParams
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public struct FormatterParams
  {
    public readonly TextFormatter formatter;
    public IValueProvider valueProvider;
    public IParserRule[] additionnalRules;
    public KeywordContext context;

    public FormatterParams(TextFormatter formatter, IValueProvider valueProvider)
    {
      this.formatter = formatter;
      this.valueProvider = valueProvider;
      this.additionnalRules = (IParserRule[]) null;
      this.context = KeywordContext.FightSolo;
    }
  }
}
