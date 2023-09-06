// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.TextButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class TextButton : Button
  {
    [SerializeField]
    private AbstractTextField m_text;
    [SerializeField]
    private TextButtonStyle m_style;

    public AbstractTextField textField => this.m_text;

    protected override void OnEnable() => base.OnEnable();

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      TextButtonState textButtonState = this.m_style.disable;
      switch (state)
      {
        case Selectable.SelectionState.Normal:
          textButtonState = this.m_style.normal;
          break;
        case Selectable.SelectionState.Highlighted:
          textButtonState = this.m_style.highlight;
          break;
        case Selectable.SelectionState.Pressed:
          textButtonState = this.m_style.pressed;
          break;
        case Selectable.SelectionState.Disabled:
          textButtonState = this.m_style.disable;
          break;
      }
      if ((Object) this.image != (Object) null)
        this.image.overrideSprite = textButtonState.sprite;
      if (!((Object) this.m_text != (Object) null))
        return;
      this.m_text.color = textButtonState.textColor;
    }
  }
}
