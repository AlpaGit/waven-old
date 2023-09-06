// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.PluralRulesEN
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization
{
  public class PluralRulesEN : IPluralRules
  {
    public PluralCategory GetPluralForCardinal(int value) => value == 1 ? PluralCategory.one : PluralCategory.other;

    public PluralCategory GetPluralForCardinal(float value) => PluralCategory.other;

    public PluralCategory GetPluralForOrdinal(int value)
    {
      int num1 = value % 10;
      int num2 = value % 100;
      if (num1 == 1 && num2 != 11)
        return PluralCategory.one;
      if (num1 == 2 && num2 != 12)
        return PluralCategory.two;
      return num1 == 3 && num2 != 13 ? PluralCategory.few : PluralCategory.other;
    }

    public PluralCategory GetPluralForRange(PluralCategory min, PluralCategory max) => PluralCategory.other;
  }
}
