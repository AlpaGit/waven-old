// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.PopupInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Code.UI
{
  public struct PopupInfo
  {
    public RawTextData title;
    public RawTextData message;
    public PopupStyle style;
    public float displayDuration;
    public ButtonData[] buttons;
    public bool closeOnBackgroundClick;
    public int selectedButton;
    public bool useBlur;

    public bool hasDisplayDuration => (double) this.displayDuration > 0.0;
  }
}
