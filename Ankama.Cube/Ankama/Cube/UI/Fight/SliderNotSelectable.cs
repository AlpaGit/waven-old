// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.SliderNotSelectable
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  [ExecuteInEditMode]
  public class SliderNotSelectable : MonoBehaviour
  {
    [SerializeField]
    private RectTransform m_fillRect;
    [SerializeField]
    private Slider.Direction m_direction;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_factor;

    private RectTransform.Axis axis => this.m_direction != Slider.Direction.LeftToRight && this.m_direction != Slider.Direction.RightToLeft ? RectTransform.Axis.Vertical : RectTransform.Axis.Horizontal;

    private bool reverseValue => this.m_direction == Slider.Direction.RightToLeft || this.m_direction == Slider.Direction.TopToBottom;

    private void OnEnable() => this.UpdateVisual();

    protected void OnDidApplyAnimationProperties() => this.UpdateVisual();

    private void UpdateVisual()
    {
      if (!((Object) this.m_fillRect != (Object) null))
        return;
      Vector2 zero = Vector2.zero;
      Vector2 one = Vector2.one;
      if (this.reverseValue)
        zero[(int) this.axis] = 1f - this.m_factor;
      else
        one[(int) this.axis] = this.m_factor;
      this.m_fillRect.anchorMin = zero;
      this.m_fillRect.anchorMax = one;
    }
  }
}
