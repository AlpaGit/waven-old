// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUIButtonLoopOnHighlight
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUIButtonLoopOnHighlight : 
    AudioEventUILoader,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler
  {
    [SerializeField]
    private Button m_button;
    [SerializeField]
    private AudioReferenceWithParameters m_soundOnOver;
    [SerializeField]
    private STOP_MODE m_stopMode;
    private EventInstance m_eventInstance;

    private void Awake()
    {
      AudioManager.StartCoroutine(this.Load(this.m_soundOnOver));
      if (!((Object) null != (Object) this.m_button))
        return;
      this.m_button.onClick.AddListener(new UnityAction(this.OnButtonClicked));
    }

    private void OnDisable() => this.StopSound();

    protected override void OnDestroy()
    {
      if ((Object) null != (Object) this.m_button)
        this.m_button.onClick.RemoveListener(new UnityAction(this.OnButtonClicked));
      if (this.m_eventInstance.isValid())
      {
        int num1 = (int) this.m_eventInstance.stop(STOP_MODE.IMMEDIATE);
        int num2 = (int) this.m_eventInstance.release();
        this.m_eventInstance.clearHandle();
      }
      base.OnDestroy();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (!this.m_soundOnOver.isValid || (Object) null == (Object) this.m_button || !this.m_button.interactable || !this.m_eventInstance.isValid() && (!AudioManager.isReady || !AudioManager.TryCreateInstance(this.m_soundOnOver, this.transform, out this.m_eventInstance)))
        return;
      int num = (int) this.m_eventInstance.start();
    }

    public void OnPointerExit(PointerEventData eventData) => this.StopSound();

    private void OnButtonClicked() => this.StopSound();

    private void StopSound()
    {
      if (!this.m_eventInstance.isValid())
        return;
      int num = (int) this.m_eventInstance.stop(this.m_stopMode);
    }
  }
}
