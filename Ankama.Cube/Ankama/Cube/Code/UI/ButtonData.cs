// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.ButtonData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Code.UI
{
  public struct ButtonData
  {
    public readonly bool isValid;
    public readonly TextData textOverride;
    public readonly ButtonStyle style;
    public readonly Action onClick;
    public readonly bool closeOnClick;

    public ButtonData(TextData textOverride, Action onClick = null, bool closeOnClick = true, ButtonStyle style = ButtonStyle.Normal)
    {
      this.isValid = true;
      this.textOverride = textOverride;
      this.onClick = onClick;
      this.closeOnClick = closeOnClick;
      this.style = style;
    }
  }
}
