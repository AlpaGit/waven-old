// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.ToStringExtensions
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Text;

namespace Ankama.Utilities
{
  public static class ToStringExtensions
  {
    private static readonly StringBuilder s_stringBuilder = new StringBuilder();

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToStringSigned(this sbyte value)
    {
      StringBuilder stringBuilder = ToStringExtensions.s_stringBuilder;
      stringBuilder.Clear();
      if (value > (sbyte) 0)
        stringBuilder.Append('+');
      stringBuilder.Append(value);
      return stringBuilder.ToString();
    }

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToStringSigned(this short value)
    {
      StringBuilder stringBuilder = ToStringExtensions.s_stringBuilder;
      stringBuilder.Clear();
      if (value > (short) 0)
        stringBuilder.Append('+');
      stringBuilder.Append(value);
      return stringBuilder.ToString();
    }

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToStringSigned(this int value)
    {
      StringBuilder stringBuilder = ToStringExtensions.s_stringBuilder;
      stringBuilder.Clear();
      if (value > 0)
        stringBuilder.Append('+');
      stringBuilder.Append(value);
      return stringBuilder.ToString();
    }

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToStringSigned(this long value)
    {
      StringBuilder stringBuilder = ToStringExtensions.s_stringBuilder;
      stringBuilder.Clear();
      if (value > 0L)
        stringBuilder.Append('+');
      stringBuilder.Append(value);
      return stringBuilder.ToString();
    }

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToHumanReadableSize(this long value, [NotNull] string format = "0.### ")
    {
      long num1 = value < 0L ? -value : value;
      string str;
      double num2;
      if (num1 >= 1152921504606846976L)
      {
        str = "EB";
        num2 = (double) (value >> 50);
      }
      else if (num1 >= 1125899906842624L)
      {
        str = "PB";
        num2 = (double) (value >> 40);
      }
      else if (num1 >= 1099511627776L)
      {
        str = "TB";
        num2 = (double) (value >> 30);
      }
      else if (num1 >= 1073741824L)
      {
        str = "GB";
        num2 = (double) (value >> 20);
      }
      else if (num1 >= 1048576L)
      {
        str = "MB";
        num2 = (double) (value >> 10);
      }
      else
      {
        if (num1 < 1024L)
          return value.ToString(format) + "B";
        str = "KB";
        num2 = (double) value;
      }
      return (num2 / 1024.0).ToString(format) + str;
    }

    [Pure]
    [NotNull]
    [PublicAPI]
    public static string ToHumanReadableSize(
      this long value,
      [NotNull] IFormatProvider formatProvider,
      [NotNull] string format = "0.### ")
    {
      long num1 = value < 0L ? -value : value;
      string str;
      double num2;
      if (num1 >= 1152921504606846976L)
      {
        str = "EB";
        num2 = (double) (value >> 50);
      }
      else if (num1 >= 1125899906842624L)
      {
        str = "PB";
        num2 = (double) (value >> 40);
      }
      else if (num1 >= 1099511627776L)
      {
        str = "TB";
        num2 = (double) (value >> 30);
      }
      else if (num1 >= 1073741824L)
      {
        str = "GB";
        num2 = (double) (value >> 20);
      }
      else if (num1 >= 1048576L)
      {
        str = "MB";
        num2 = (double) (value >> 10);
      }
      else
      {
        if (num1 < 1024L)
          return value.ToString(format, formatProvider) + "B";
        str = "KB";
        num2 = (double) value;
      }
      return (num2 / 1024.0).ToString(format, formatProvider) + str;
    }
  }
}
