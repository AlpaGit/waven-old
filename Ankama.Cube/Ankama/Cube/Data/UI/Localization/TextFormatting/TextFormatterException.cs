// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.TextFormatterException
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class TextFormatterException : Exception
  {
    public TextFormatterException(string text, int index)
      : base(string.Format("{0} at:{1}", (object) text, (object) index))
    {
    }

    public TextFormatterException(string text, int index, string message)
      : base(string.Format("{0} at:{1} : {2}", (object) text, (object) index, (object) message))
    {
    }

    public TextFormatterException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
