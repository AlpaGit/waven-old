// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUITriggerOnDrag
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.Audio.UI
{
  public class AudioEventUITriggerOnDrag : 
    AudioEventUITrigger,
    IPointerDownHandler,
    IEventSystemHandler,
    IDragHandler,
    IPointerUpHandler
  {
    [SerializeField]
    protected AudioReferenceWithParameters m_soundOnDragStart;
    [SerializeField]
    protected AudioReferenceWithParameters m_soundOnDragEnd;
    [SerializeField]
    private STOP_MODE m_stopMode;
    private EventInstance m_dragEventInstance;

    protected override void Awake() => AudioManager.StartCoroutine(this.Load(this.m_sound, this.m_soundOnDragStart, this.m_soundOnDragEnd));

    protected override void OnDestroy()
    {
      int num1 = (int) this.m_dragEventInstance.stop(STOP_MODE.IMMEDIATE);
      if (this.m_dragEventInstance.isValid())
      {
        int num2 = (int) this.m_dragEventInstance.release();
        this.m_dragEventInstance.clearHandle();
      }
      base.OnDestroy();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      if (!this.m_soundOnDragStart.isValid)
        return;
      AudioManager.PlayOneShot(this.m_soundOnDragStart, this.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.m_sound.isValid || this.m_dragEventInstance.isValid() || !this.m_dragEventInstance.isValid() && !AudioManager.TryCreateInstance(this.m_sound, this.transform, out this.m_dragEventInstance))
        return;
      int num = (int) this.m_dragEventInstance.start();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (!this.m_soundOnDragEnd.isValid)
        return;
      if (this.m_dragEventInstance.isValid())
      {
        int num = (int) this.m_dragEventInstance.stop(this.m_stopMode);
      }
      AudioManager.PlayOneShot(this.m_soundOnDragEnd, this.transform);
    }
  }
}
