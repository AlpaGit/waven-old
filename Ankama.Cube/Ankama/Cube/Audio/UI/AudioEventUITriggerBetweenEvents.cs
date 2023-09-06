// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUITriggerBetweenEvents
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using FMOD.Studio;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUITriggerBetweenEvents : AudioEventUITrigger
  {
    [SerializeField]
    private STOP_MODE m_stopMode;
    [SerializeField]
    private float m_stopDelay;
    private Coroutine m_stopDelayRoutine;
    private EventInstance m_eventInstance;

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

    [PublicAPI]
    public void ActivationTrigger()
    {
      if (!this.m_sound.isValid)
        return;
      if (!this.m_eventInstance.isValid())
      {
        if (!AudioManager.isReady || !AudioManager.TryCreateInstance(this.m_sound, out this.m_eventInstance))
          return;
      }
      else
      {
        if (this.m_stopDelayRoutine != null)
        {
          this.StopCoroutine(this.m_stopDelayRoutine);
          this.m_stopDelayRoutine = (Coroutine) null;
        }
        PLAYBACK_STATE state;
        int playbackState = (int) this.m_eventInstance.getPlaybackState(out state);
        if (state == PLAYBACK_STATE.STARTING || state == PLAYBACK_STATE.PLAYING)
          return;
      }
      int num = (int) this.m_eventInstance.start();
    }

    [PublicAPI]
    public void DeactivationTrigger()
    {
      if (!this.m_eventInstance.isValid())
        return;
      PLAYBACK_STATE state;
      int playbackState = (int) this.m_eventInstance.getPlaybackState(out state);
      if (state != PLAYBACK_STATE.PLAYING && state != PLAYBACK_STATE.STARTING)
        return;
      if ((double) this.m_stopDelay <= 1.4012984643248171E-45)
      {
        if (this.m_stopDelayRoutine != null)
        {
          this.StopCoroutine(this.m_stopDelayRoutine);
          this.m_stopDelayRoutine = (Coroutine) null;
        }
        int num = (int) this.m_eventInstance.stop(this.m_stopMode);
      }
      else
      {
        if (this.m_stopDelayRoutine != null)
          return;
        this.m_stopDelayRoutine = this.StartCoroutine(this.DelayStop());
      }
    }

    private IEnumerator DelayStop()
    {
      yield return (object) new WaitForTime(this.m_stopDelay);
      if (this.m_eventInstance.isValid())
      {
        int num = (int) this.m_eventInstance.stop(this.m_stopMode);
      }
      this.m_stopDelayRoutine = (Coroutine) null;
    }
  }
}
