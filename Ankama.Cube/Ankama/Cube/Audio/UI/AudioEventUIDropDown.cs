﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUIDropDown
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUIDropDown : 
    AudioEventUILoader,
    IPointerClickHandler,
    IEventSystemHandler,
    IPointerEnterHandler
  {
    [SerializeField]
    private Selectable m_selectable;
    [SerializeField]
    private AudioReferenceWithParameters m_soundOnClick;
    [SerializeField]
    private AudioReferenceWithParameters m_soundOnOver;

    private void Awake() => AudioManager.StartCoroutine(this.Load(this.m_soundOnOver, this.m_soundOnClick));

    public void OnPointerClick(PointerEventData eventData)
    {
      if ((Object) null == (Object) this.m_selectable || !this.m_selectable.interactable || !this.m_soundOnClick.isValid)
        return;
      AudioManager.PlayOneShot(this.m_soundOnClick, this.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if ((Object) null == (Object) this.m_selectable || !this.m_selectable.interactable || !this.m_soundOnOver.isValid)
        return;
      AudioManager.PlayOneShot(this.m_soundOnOver, this.transform);
    }
  }
}
