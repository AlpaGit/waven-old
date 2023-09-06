// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.StringExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Text;

namespace Ankama.Cube.Extensions
{
  public static class StringExtensions
  {
    public static string RemoveTags(this string s)
    {
      int startIndex1 = s.IndexOf('<');
      if (startIndex1 == -1)
        return s;
      int startIndex2 = 0;
      StringBuilder stringBuilder = new StringBuilder();
      int startIndex3;
      for (; startIndex1 != -1; startIndex1 = s.IndexOf('<', startIndex3))
      {
        if (startIndex2 < startIndex1)
          stringBuilder.Append(s.Substring(startIndex2, startIndex1 - startIndex2));
        startIndex3 = s.IndexOf('>', startIndex1);
        startIndex2 = startIndex3 == -1 ? startIndex1 : startIndex3 + 1;
      }
      if (startIndex2 != -1 && startIndex2 < s.Length)
        stringBuilder.Append(s.Substring(startIndex2, s.Length - startIndex2));
      return stringBuilder.ToString();
    }
  }
}
