// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUITrigger
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMODUnity;
using UnityEngine;

namespace Ankama.Cube.Audio.UI
{
  public abstract class AudioEventUITrigger : AudioEventUILoader
  {
    [SerializeField]
    protected AudioReferenceWithParameters m_sound;

    protected virtual void Awake()
    {
      if (!this.m_sound.isValid)
        return;
      AudioManager.StartCoroutine(this.Load(this.m_sound));
    }

    protected void PlaySound()
    {
      if (!this.m_sound.isValid)
        return;
      AudioManager.PlayOneShot(this.m_sound, this.transform);
    }
  }
}
