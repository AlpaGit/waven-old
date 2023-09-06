// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ImageTextCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class ImageTextCellRenderer : CellRenderer<ImageTextCellData, ICellRendererConfigurator>
  {
    [SerializeField]
    private Image m_image;
    [SerializeField]
    private Text m_text;

    protected override void SetValue(ImageTextCellData val)
    {
      this.m_image.sprite = val.Sprite;
      this.m_text.text = val.Text;
    }

    protected override void Clear()
    {
      this.m_image.sprite = (Sprite) null;
      this.m_text.text = "";
    }

    public override void OnConfiguratorUpdate(bool instant)
    {
    }
  }
}
