// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.PluralRulesRU
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization
{
  public class PluralRulesRU : IPluralRules
  {
    public PluralCategory GetPluralForCardinal(int value)
    {
      int num = value % 10;
      switch (value % 100)
      {
        case 11:
        case 12:
        case 13:
        case 14:
          return PluralCategory.many;
        default:
          switch (num)
          {
            case 1:
              return PluralCategory.one;
            case 2:
            case 3:
            case 4:
              return PluralCategory.few;
            default:
              return PluralCategory.many;
          }
      }
    }

    public PluralCategory GetPluralForCardinal(float value) => PluralCategory.other;

    public PluralCategory GetPluralForOrdinal(int value) => value != 1 ? PluralCategory.other : PluralCategory.one;

    public PluralCategory GetPluralForRange(PluralCategory min, PluralCategory max) => max;
  }
}
