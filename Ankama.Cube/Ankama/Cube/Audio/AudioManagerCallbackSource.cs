// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioManagerCallbackSource
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  public sealed class AudioManagerCallbackSource : MonoBehaviour
  {
    [HideInInspector]
    [SerializeField]
    private long m_cachedStudioHandle;
    [HideInInspector]
    [SerializeField]
    private long m_cachedLowLevelStudioHandle;

    public static AudioManagerCallbackSource Create()
    {
      AudioManagerCallbackSource objectOfType = UnityEngine.Object.FindObjectOfType<AudioManagerCallbackSource>();
      if ((UnityEngine.Object) null != (UnityEngine.Object) objectOfType)
      {
        if (objectOfType.m_cachedStudioHandle != 0L)
          AudioManager.RestoreSystemInternal((IntPtr) objectOfType.m_cachedStudioHandle, (IntPtr) objectOfType.m_cachedLowLevelStudioHandle);
        return objectOfType;
      }
      GameObject target = new GameObject(nameof (AudioManagerCallbackSource));
      target.hideFlags = HideFlags.HideInHierarchy;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
      return target.AddComponent<AudioManagerCallbackSource>();
    }

    private void Update() => AudioManager.UpdateInternal();

    private void OnDisable()
    {
      IntPtr studioHandle;
      IntPtr lowLevelStudioHandle;
      AudioManager.BackupSystemInternal(out studioHandle, out lowLevelStudioHandle);
      this.m_cachedStudioHandle = (long) studioHandle;
      this.m_cachedLowLevelStudioHandle = (long) lowLevelStudioHandle;
    }

    private void OnDestroy() => AudioManager.ReleaseSystemInternal();

    private void OnApplicationPause(bool paused) => AudioManager.PauseSystemInternal(paused);
  }
}
