// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.PluralRulesFR
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization
{
  public class PluralRulesFR : IPluralRules
  {
    public PluralCategory GetPluralForCardinal(int value) => value == 0 || value == 1 ? PluralCategory.one : PluralCategory.other;

    public PluralCategory GetPluralForCardinal(float value) => (double) value >= 0.0 && (double) value <= 1.0 ? PluralCategory.one : PluralCategory.other;

    public PluralCategory GetPluralForOrdinal(int value) => value != 1 ? PluralCategory.other : PluralCategory.one;

    public PluralCategory GetPluralForRange(PluralCategory min, PluralCategory max) => min == PluralCategory.one && max == PluralCategory.one ? PluralCategory.one : PluralCategory.other;
  }
}
