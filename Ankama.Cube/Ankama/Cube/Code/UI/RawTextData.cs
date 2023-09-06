// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.RawTextData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Code.UI
{
  public struct RawTextData
  {
    public readonly bool isValid;
    public readonly string formattedText;

    public RawTextData(string formattedText)
    {
      this.isValid = true;
      this.formattedText = formattedText;
    }

    public RawTextData(int textId, params string[] textParams)
    {
      this.isValid = true;
      this.formattedText = RuntimeData.FormattedText(textId, textParams);
    }

    public static implicit operator RawTextData(int textId) => new RawTextData(textId, Array.Empty<string>());

    public static implicit operator RawTextData(string formattedText) => new RawTextData(formattedText);
  }
}
