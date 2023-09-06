// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.AudioCategory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class AudioCategory : OptionCategory
  {
    [SerializeField]
    protected Slider m_generalSlider;
    [SerializeField]
    protected Slider m_musicSlider;
    [SerializeField]
    protected Slider m_effectSlider;
    [SerializeField]
    protected Slider m_uiSlider;

    protected void Awake()
    {
    }

    protected override void OnBecameEnable()
    {
      float volume1;
      AudioManager.TryGetVolume(AudioBusIdentifier.Master, out volume1);
      float volume2;
      AudioManager.TryGetVolume(AudioBusIdentifier.Music, out volume2);
      float volume3;
      AudioManager.TryGetVolume(AudioBusIdentifier.SFX, out volume3);
      float volume4;
      AudioManager.TryGetVolume(AudioBusIdentifier.UI, out volume4);
      this.m_generalSlider.value = volume1;
      this.m_musicSlider.value = volume2;
      this.m_effectSlider.value = volume3;
      this.m_uiSlider.value = volume4;
      this.m_generalSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnGeneralSliderChanged));
      this.m_musicSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnMusicSliderChanged));
      this.m_effectSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnEffectSliderChanged));
      this.m_uiSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnUISliderChanged));
    }

    protected override void OnBecameDisable()
    {
      this.m_generalSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnGeneralSliderChanged));
      this.m_musicSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnMusicSliderChanged));
      this.m_effectSlider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnEffectSliderChanged));
    }

    protected void OnGeneralSliderChanged(float value)
    {
      AudioManager.SetVolume(AudioBusIdentifier.Master, value);
      PlayerPreferences.audioMasterVolume = value;
    }

    protected void OnMusicSliderChanged(float value)
    {
      AudioManager.SetVolume(AudioBusIdentifier.Music, value);
      PlayerPreferences.audioMusicVolume = value;
    }

    protected void OnEffectSliderChanged(float value)
    {
      AudioManager.SetVolume(AudioBusIdentifier.SFX, value);
      PlayerPreferences.audioFxVolume = value;
    }

    protected void OnUISliderChanged(float value)
    {
      AudioManager.SetVolume(AudioBusIdentifier.UI, value);
      PlayerPreferences.audioUiVolume = value;
    }

    protected void OnMuteWhenInactiveDropdownChanged(int value)
    {
    }
  }
}
