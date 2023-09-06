// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUITriggerOnEnable
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUITriggerOnEnable : AudioEventUITrigger
  {
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
          this.PlaySound();
          break;
        case AudioEventUILoader.InitializationState.Error:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private IEnumerator WaitAndPlay()
    {
      AudioEventUITriggerOnEnable uiTriggerOnEnable = this;
      do
      {
        yield return (object) null;
      }
      while (uiTriggerOnEnable.m_initializationState == AudioEventUILoader.InitializationState.Loading);
      if (uiTriggerOnEnable.m_initializationState == AudioEventUILoader.InitializationState.Loaded)
        uiTriggerOnEnable.PlaySound();
    }
  }
}
