// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUIPlayWhileEnabled
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMOD.Studio;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Audio.UI
{
  public class AudioEventUIPlayWhileEnabled : AudioEventUITrigger
  {
    [SerializeField]
    private STOP_MODE m_stopMode;
    private EventInstance m_eventInstance;

    private void OnEnable()
    {
      switch (this.m_initializationState)
      {
        case AudioEventUILoader.InitializationState.None:
          break;
        case AudioEventUILoader.InitializationState.Loading:
          AudioManager.StartCoroutine(this.WaitAndPlay());
          break;
        case AudioEventUILoader.InitializationState.Loaded:
          this.Play();
          break;
        case AudioEventUILoader.InitializationState.Error:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnDisable()
    {
      if (!this.m_eventInstance.isValid())
        return;
      int num = (int) this.m_eventInstance.stop(this.m_stopMode);
    }

    protected override void OnDestroy()
    {
      if (this.m_eventInstance.isValid())
      {
        int num1 = (int) this.m_eventInstance.stop(STOP_MODE.IMMEDIATE);
        int num2 = (int) this.m_eventInstance.release();
        this.m_eventInstance.clearHandle();
      }
      base.OnDestroy();
    }

    private IEnumerator WaitAndPlay()
    {
      AudioEventUIPlayWhileEnabled playWhileEnabled = this;
      do
      {
        yield return (object) null;
      }
      while (playWhileEnabled.m_initializationState == AudioEventUILoader.InitializationState.Loading);
      if (playWhileEnabled.m_initializationState == AudioEventUILoader.InitializationState.Loaded)
        playWhileEnabled.Play();
    }

    protected void Play()
    {
      if (!this.m_sound.isValid || !this.m_eventInstance.isValid() && (!AudioManager.isReady || !AudioManager.TryCreateInstance(this.m_sound, this.transform, out this.m_eventInstance)))
        return;
      int num = (int) this.m_eventInstance.start();
    }
  }
}
