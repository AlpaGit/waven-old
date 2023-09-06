// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.TextData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using System;

namespace Ankama.Cube.Code.UI
{
  public struct TextData
  {
    public readonly bool isValid;
    public readonly int? textId;
    public readonly IValueProvider valueProvider;

    public TextData(int textId, params string[] textParams)
    {
      this.isValid = true;
      this.textId = new int?(textId);
      this.valueProvider = (IValueProvider) new IndexedValueProvider(textParams);
    }

    public static implicit operator TextData(int textId) => new TextData(textId, Array.Empty<string>());
  }
}
