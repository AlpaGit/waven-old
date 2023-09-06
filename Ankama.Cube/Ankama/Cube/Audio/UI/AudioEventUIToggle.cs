// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUIToggle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUIToggle : 
    AudioEventUILoader,
    IPointerEnterHandler,
    IEventSystemHandler
  {
    [SerializeField]
    private Toggle m_toggle;
    [SerializeField]
    private AudioReferenceWithParameters m_soundOnClick;
    [SerializeField]
    private AudioReferenceWithParameters m_soundOnOver;

    private void Awake()
    {
      AudioManager.StartCoroutine(this.Load(this.m_soundOnOver, this.m_soundOnClick));
      if (!((Object) null != (Object) this.m_toggle))
        return;
      this.m_toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
    }

    protected override void OnDestroy()
    {
      if (!((Object) null != (Object) this.m_toggle))
        return;
      this.m_toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
    }

    private void OnValueChanged(bool arg0)
    {
      if (!this.m_soundOnClick.isValid)
        return;
      AudioManager.PlayOneShot(this.m_soundOnClick, this.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (!this.m_soundOnOver.isValid || (Object) null == (Object) this.m_toggle || !this.m_toggle.interactable || this.m_toggle.isOn)
        return;
      AudioManager.PlayOneShot(this.m_soundOnOver, this.transform);
    }
  }
}
