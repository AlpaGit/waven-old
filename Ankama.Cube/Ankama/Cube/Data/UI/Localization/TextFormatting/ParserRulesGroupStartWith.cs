// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ParserRulesGroupStartWith
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class ParserRulesGroupStartWith : 
    AbstractParserRulesGroup,
    IParserRuleWithPrefix,
    IParserRule
  {
    private readonly string starts;

    public string prefix => this.starts;

    public ParserRulesGroupStartWith(string starts, params IParserRule[] rules)
      : base(rules)
    {
      this.starts = starts;
    }

    protected override bool MatchGroup(StringReader input) => input.NextEquals(this.starts);
  }
}
