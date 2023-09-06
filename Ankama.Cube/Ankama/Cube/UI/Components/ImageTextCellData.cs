// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ImageTextCellData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public class ImageTextCellData
  {
    private readonly Sprite m_sprite;
    private readonly string m_text;

    public ImageTextCellData(Sprite sprite, string text)
    {
      this.m_sprite = sprite;
      this.m_text = text;
    }

    public Sprite Sprite => this.m_sprite;

    public string Text => this.m_text;

    public override string ToString() => this.m_text;
  }
}
