// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.StringExtensions
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Globalization;
using System.Text;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class StringExtensions
  {
    private static readonly StringBuilder s_stringBuilder = new StringBuilder();

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToTitleCase([NotNull] this string value) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string RemoveDiacritics([NotNull] this string value)
    {
      StringBuilder stringBuilder = StringExtensions.s_stringBuilder;
      stringBuilder.Clear();
      int length = value.Length;
      if (stringBuilder.Capacity < length)
        stringBuilder.Capacity = length;
      foreach (char ch in value.Normalize(NormalizationForm.FormD))
      {
        if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
          stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    [Pure]
    [PublicAPI]
    public static int IndexOfIgnoreDiacritics(
      this string s,
      [NotNull] string value,
      int startIndex = 0,
      StringComparison comparisonType = StringComparison.Ordinal)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(s, value, startIndex, CompareOptions.IgnoreNonSpace);
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(s, value, startIndex, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, value, startIndex, CompareOptions.IgnoreNonSpace);
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, value, startIndex, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        case StringComparison.Ordinal:
          string str = value.RemoveDiacritics();
          int length1 = str.Length;
          if (length1 == 0)
            return startIndex;
          int num1 = -1;
          int index1 = 0;
          int num2 = startIndex;
          int num3 = s.Length - value.Length;
          foreach (char ch in (startIndex > 0 ? s.Substring(startIndex) : s).Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
              if ((int) ch == (int) str[index1])
              {
                ++index1;
                if (index1 == length1)
                  return num1 + 1;
              }
              else
              {
                index1 = 0;
                num1 = num2;
                if (num2 == num3)
                  break;
              }
              ++num2;
            }
          }
          return -1;
        case StringComparison.OrdinalIgnoreCase:
          StringBuilder stringBuilder = StringExtensions.s_stringBuilder;
          stringBuilder.Clear();
          int length2 = value.Length;
          if (stringBuilder.Capacity < length2)
            stringBuilder.Capacity = length2;
          foreach (char ch in value.Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
              stringBuilder.Append(char.ToLowerInvariant(ch));
          }
          int length3 = stringBuilder.Length;
          if (length3 == 0)
            return startIndex;
          int num4 = -1;
          int index2 = 0;
          int num5 = startIndex;
          int num6 = s.Length - value.Length;
          foreach (char ch in (startIndex > 0 ? s.Substring(startIndex) : s).Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
              if ((int) char.ToLowerInvariant(ch) == (int) stringBuilder[index2])
              {
                ++index2;
                if (index2 == length3)
                  return num4 + 1;
              }
              else
              {
                index2 = 0;
                num4 = num5;
                if (num5 == num6)
                  break;
              }
              ++num5;
            }
          }
          return -1;
        default:
          throw new ArgumentOutOfRangeException(nameof (comparisonType), (object) comparisonType, (string) null);
      }
    }

    [Pure]
    [PublicAPI]
    public static bool Contains([NotNull] this string s, [NotNull] string value, StringComparison comparisonType) => s.IndexOf(value, comparisonType) != -1;

    [Pure]
    [PublicAPI]
    public static bool ContainsIgnoreDiacritics(
      [NotNull] this string s,
      [NotNull] string value,
      StringComparison comparisonType = StringComparison.Ordinal)
    {
      switch (comparisonType)
      {
        case StringComparison.CurrentCulture:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(s, value, CompareOptions.IgnoreNonSpace) != -1;
        case StringComparison.CurrentCultureIgnoreCase:
          return CultureInfo.CurrentCulture.CompareInfo.IndexOf(s, value, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) != -1;
        case StringComparison.InvariantCulture:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, value, CompareOptions.IgnoreNonSpace) != -1;
        case StringComparison.InvariantCultureIgnoreCase:
          return CultureInfo.InvariantCulture.CompareInfo.IndexOf(s, value, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) != -1;
        case StringComparison.Ordinal:
          string str = value.RemoveDiacritics();
          int length1 = str.Length;
          if (length1 == 0)
            return true;
          int index1 = 0;
          int num1 = s.Length - value.Length;
          int num2 = 0;
          foreach (char ch in s.Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
              if ((int) ch == (int) str[index1])
              {
                ++index1;
                if (index1 == length1)
                  return true;
              }
              else
              {
                index1 = 0;
                if (num2 == num1)
                  break;
              }
              ++num2;
            }
          }
          return false;
        case StringComparison.OrdinalIgnoreCase:
          StringBuilder stringBuilder = StringExtensions.s_stringBuilder;
          stringBuilder.Clear();
          int length2 = value.Length;
          if (stringBuilder.Capacity < length2)
            stringBuilder.Capacity = length2;
          foreach (char ch in value.Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
              stringBuilder.Append(char.ToLowerInvariant(ch));
          }
          int length3 = stringBuilder.Length;
          if (length3 == 0)
            return true;
          int index2 = 0;
          int num3 = s.Length - value.Length;
          int num4 = 0;
          foreach (char ch in s.Normalize(NormalizationForm.FormD))
          {
            if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
            {
              if ((int) char.ToLowerInvariant(ch) == (int) stringBuilder[index2])
              {
                ++index2;
                if (index2 == length3)
                  return true;
              }
              else
              {
                index2 = 0;
                if (num4 == num3)
                  break;
              }
              ++num4;
            }
          }
          return false;
        default:
          throw new ArgumentOutOfRangeException(nameof (comparisonType), (object) comparisonType, (string) null);
      }
    }

    [Pure]
    [PublicAPI]
    public static bool StartsWithFast([NotNull] this string s, string value)
    {
      int length1 = s.Length;
      int length2 = value.Length;
      if (length1 < length2)
        return false;
      int index1 = 0;
      int index2;
      for (index2 = 0; index1 < length1 && index2 < length2 && (int) s[index1] == (int) value[index2]; ++index2)
        ++index1;
      if (index2 == length2 && length1 >= length2)
        return true;
      return index1 == length1 && length2 >= length1;
    }

    public static bool EndsWithFast([NotNull] this string s, string value)
    {
      int length1 = s.Length;
      int length2 = value.Length;
      if (length1 < length2)
        return false;
      int index1 = length1 - 1;
      int index2;
      for (index2 = length2 - 1; index1 >= 0 && index2 >= 0 && (int) s[index1] == (int) value[index2]; --index2)
        --index1;
      if (index2 < 0 && length1 >= length2)
        return true;
      return index1 < 0 && length2 >= length1;
    }
  }
}
