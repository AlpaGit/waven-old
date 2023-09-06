// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.OptionSlider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.UI.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class OptionSlider : MonoBehaviour
  {
    [SerializeField]
    private Slider m_slider;
    [SerializeField]
    private RawTextField m_text;

    private void OnEnable()
    {
      this.SetTextValue(this.m_slider.value);
      this.m_slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChanged));
    }

    private void OnValueChanged(float value) => this.SetTextValue(value);

    private void SetTextValue(float value)
    {
      value *= 100f;
      this.m_text.SetText((double) value > 0.10000000149011612 ? ((double) value >= 1.0 || (double) value <= 0.10000000149011612 ? Mathf.RoundToInt(value).ToString() : "1") : RuntimeData.FormattedText(33654, (IValueProvider) null));
    }
  }
}
