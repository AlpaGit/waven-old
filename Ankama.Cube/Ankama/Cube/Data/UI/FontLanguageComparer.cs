// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.FontLanguageComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data.UI
{
  public class FontLanguageComparer : IEqualityComparer<FontLanguage>
  {
    public static FontLanguageComparer instance = new FontLanguageComparer();

    public bool Equals(FontLanguage x, FontLanguage y) => x == y;

    public int GetHashCode(FontLanguage obj) => (int) obj;
  }
}
