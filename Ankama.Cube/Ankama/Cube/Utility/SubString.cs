// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.SubString
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System.Text;

namespace Ankama.Cube.Utility
{
  public struct SubString
  {
    public readonly string originalText;
    public int startIndex;
    public int length;

    public int endIndex => this.startIndex + this.length;

    public SubString(string originalText)
      : this(originalText, 0, originalText.Length)
    {
    }

    public SubString(string originalText, int startIndex, int length)
    {
      this.originalText = originalText;
      this.startIndex = startIndex;
      this.length = length;
    }

    [PublicAPI]
    public void Trim(params char[] trimChars)
    {
      this.TrimStart(trimChars);
      this.TrimEnd(trimChars);
    }

    [PublicAPI]
    public void TrimStart(params char[] trimChars)
    {
      int startIndex = this.startIndex;
      int endIndex = this.endIndex;
      if (trimChars == null || trimChars.Length == 0)
      {
        while (startIndex < endIndex && char.IsWhiteSpace(this.originalText[startIndex]))
          ++startIndex;
      }
      else
      {
        while (startIndex < endIndex && SubString.Contains(trimChars, this.originalText[startIndex]))
          ++startIndex;
      }
      this.startIndex = startIndex;
      this.length = endIndex - startIndex;
    }

    [PublicAPI]
    public void TrimEnd(params char[] trimChars)
    {
      int startIndex = this.startIndex;
      int index = this.endIndex - 1;
      if (trimChars == null || trimChars.Length == 0)
      {
        while (index >= startIndex && char.IsWhiteSpace(this.originalText[index]))
          --index;
      }
      else
      {
        while (index >= startIndex && SubString.Contains(trimChars, this.originalText[index]))
          --index;
      }
      this.length = index - this.startIndex + 1;
    }

    [PublicAPI]
    public void WriteTo(StringBuilder sb)
    {
      if (this.length <= 0)
        return;
      sb.Append(this.originalText, this.startIndex, this.length);
    }

    [PublicAPI]
    public override string ToString() => this.length <= 0 ? string.Empty : this.originalText.Substring(this.startIndex, this.length);

    public static explicit operator SubString(string text) => new SubString(text, 0, text.Length);

    private static bool Contains(char[] array, char ch)
    {
      int length = array.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) array[index] == (int) ch)
          return true;
      }
      return false;
    }
  }
}
