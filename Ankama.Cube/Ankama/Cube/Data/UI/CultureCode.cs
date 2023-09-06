// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.CultureCode
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data.UI
{
  [Serializable]
  public struct CultureCode : IEquatable<CultureCode>
  {
    public static readonly CultureCode FR_FR = new CultureCode(CultureCode.Value.FR_FR);
    public static readonly CultureCode EN_US = new CultureCode(CultureCode.Value.EN_US);
    public static readonly CultureCode ES_ES = new CultureCode(CultureCode.Value.ES_ES);
    public static readonly CultureCode Default = CultureCode.FR_FR;
    public static readonly CultureCode Fallback = CultureCode.EN_US;
    [UsedImplicitly]
    [SerializeField]
    private CultureCode.Value m_value;

    private CultureCode(CultureCode.Value value) => this.m_value = value;

    [Pure]
    public FontLanguage GetFontLanguage()
    {
      switch (this.m_value)
      {
        case CultureCode.Value.EN_US:
        case CultureCode.Value.FR_FR:
        case CultureCode.Value.ES_ES:
          return FontLanguage.Latin;
        default:
          throw new ArgumentOutOfRangeException("m_value", (object) this.m_value, (string) null);
      }
    }

    [Pure]
    public IPluralRules GetPluralRules()
    {
      switch (this.m_value)
      {
        case CultureCode.Value.EN_US:
          return (IPluralRules) new PluralRulesEN();
        case CultureCode.Value.FR_FR:
          return (IPluralRules) new PluralRulesFR();
        case CultureCode.Value.ES_ES:
          return (IPluralRules) new PluralRulesES();
        default:
          throw new ArgumentOutOfRangeException("m_value", (object) this.m_value, (string) null);
      }
    }

    [Pure]
    public string GetLanguage()
    {
      switch (this.m_value)
      {
        case CultureCode.Value.EN_US:
          return "en";
        case CultureCode.Value.FR_FR:
          return "fr";
        case CultureCode.Value.ES_ES:
          return "es";
        default:
          throw new ArgumentOutOfRangeException("m_value", (object) this.m_value, (string) null);
      }
    }

    [Pure]
    public static CultureCode GetCultureCodeByLanguage(string language)
    {
      switch (language)
      {
        case "fr":
          return CultureCode.FR_FR;
        case "en":
          return CultureCode.EN_US;
        case "es":
          return CultureCode.ES_ES;
        case "fr-FR":
          return CultureCode.FR_FR;
        case "en-US":
          return CultureCode.EN_US;
        case "es-ES":
          return CultureCode.ES_ES;
        default:
          throw new ArgumentException(language + " is not a valid language");
      }
    }

    [Pure]
    public override string ToString()
    {
      switch (this.m_value)
      {
        case CultureCode.Value.EN_US:
          return "en-US";
        case CultureCode.Value.FR_FR:
          return "fr-FR";
        case CultureCode.Value.ES_ES:
          return "es-ES";
        default:
          return string.Empty;
      }
    }

    [Pure]
    public bool IsValid()
    {
      foreach (CultureCode availableCultureCode in CultureCode.EnumerateAvailableCultureCodes())
      {
        if (availableCultureCode.m_value == this.m_value)
          return true;
      }
      return false;
    }

    [Pure]
    public static IEnumerable<CultureCode> EnumerateAvailableCultureCodes()
    {
      yield return CultureCode.EN_US;
      yield return CultureCode.FR_FR;
      yield return CultureCode.ES_ES;
    }

    [Pure]
    public static bool TryParse([NotNull] string value, out CultureCode result)
    {
      foreach (CultureCode availableCultureCode in CultureCode.EnumerateAvailableCultureCodes())
      {
        if (availableCultureCode.ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
        {
          result = availableCultureCode;
          return true;
        }
      }
      result = new CultureCode();
      return false;
    }

    [Pure]
    public bool Equals(CultureCode other) => this.m_value == other.m_value;

    [Pure]
    public override bool Equals(object obj) => obj is CultureCode cultureCode && cultureCode.m_value == this.m_value;

    [Pure]
    public static bool operator ==(CultureCode a, CultureCode b) => a.m_value == b.m_value;

    [Pure]
    public static bool operator !=(CultureCode a, CultureCode b) => a.m_value != b.m_value;

    [Pure]
    public static explicit operator CultureCode(int value) => new CultureCode((CultureCode.Value) value);

    [Pure]
    public static explicit operator int(CultureCode value) => (int) value.m_value;

    [Pure]
    public override int GetHashCode() => (int) this.m_value;

    private enum Value
    {
      EN_US = 1033, // 0x00000409
      FR_FR = 1036, // 0x0000040C
      ES_ES = 3082, // 0x00000C0A
    }
  }
}
