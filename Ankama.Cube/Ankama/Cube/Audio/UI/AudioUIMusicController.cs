// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioUIMusicController
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMODUnity;
using UnityEngine;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioUIMusicController : MonoBehaviour
  {
    [SerializeField]
    private AudioReferenceWithParameters m_music;

    private void Awake()
    {
      if (!this.m_music.isValid)
        return;
      AudioManager.StartCoroutine(AudioManager.StartUIMusic(this.m_music));
    }

    private void OnDestroy()
    {
      if (!this.m_music.isValid)
        return;
      AudioManager.StartCoroutine(AudioManager.StopUIMusic(this.m_music));
    }
  }
}
