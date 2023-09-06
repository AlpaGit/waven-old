// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.KeywordUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public static class KeywordUtility
  {
    public static void BeginKeyWord(this StringBuilder sb) => sb.Append("<uppercase><b><color=#3FD5D3>");

    public static void EndKeyWord(this StringBuilder sb) => sb.Append("</color></b></uppercase>");
  }
}
