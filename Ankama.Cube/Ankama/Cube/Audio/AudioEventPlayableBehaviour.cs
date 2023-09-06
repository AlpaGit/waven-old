// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioEventPlayableBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Audio
{
  public sealed class AudioEventPlayableBehaviour : PlayableBehaviour
  {
    private readonly Transform m_ownerTransform;
    private readonly Guid m_eventGuid;
    private readonly AudioEventPlayableAsset.StopMode m_stopMode;
    private readonly float m_volume;
    private readonly AudioEventParameterDictionary m_parameters;
    private readonly AudioContext m_audioContext;
    private EventInstance m_eventInstance;

    [UsedImplicitly]
    public AudioEventPlayableBehaviour() => throw new NotImplementedException();

    public AudioEventPlayableBehaviour(
      Guid eventGuid,
      AudioEventPlayableAsset.StopMode stopMode,
      float volume,
      AudioEventParameterDictionary parameters,
      AudioContext audioContext,
      Transform ownerTransform)
    {
      this.m_eventGuid = eventGuid;
      this.m_stopMode = stopMode;
      this.m_volume = volume;
      this.m_parameters = parameters;
      this.m_audioContext = audioContext;
      this.m_ownerTransform = ownerTransform;
    }

    public override void OnPlayableDestroy(Playable playable)
    {
      if (!this.m_eventInstance.isValid())
        return;
      int num1 = (int) this.m_eventInstance.stop(STOP_MODE.IMMEDIATE);
      int num2 = (int) this.m_eventInstance.release();
      this.m_eventInstance.clearHandle();
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
      if (!this.m_eventInstance.isValid())
        return;
      this.StopInstance();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
      if (!this.m_eventInstance.isValid())
      {
        if (!this.StartInstance())
          return;
      }
      else if (info.evaluationType == FrameData.EvaluationType.Playback && playable.GetPreviousTime<Playable>() > playable.GetTime<Playable>())
      {
        this.StopInstance();
        if (!this.StartInstance())
          return;
      }
      RESULT result = this.m_eventInstance.setVolume(info.weight * this.m_volume);
      if (result == RESULT.OK)
        return;
      Log.Warning(string.Format("Failed to set event instance volume: {0}.", (object) result), 112, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioEventPlayableBehaviour.cs");
    }

    private bool StartInstance()
    {
      EventInstance eventInstance = this.m_eventInstance;
      if (!AudioManager.isReady)
      {
        Log.Warning(string.Format("Tried to create event instance with guid {0} but the audio manager is not ready.", (object) this.m_eventGuid), 124, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioEventPlayableBehaviour.cs");
        return false;
      }
      if (!AudioManager.TryCreateInstance(this.m_eventGuid, out eventInstance))
        return false;
      if (this.m_parameters != null)
      {
        foreach (KeyValuePair<string, float> parameter in (Dictionary<string, float>) this.m_parameters)
        {
          int num = (int) eventInstance.setParameterValue(parameter.Key, parameter.Value);
        }
      }
      if (this.m_audioContext != null)
        this.m_audioContext.AddEventInstance(eventInstance);
      else if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ownerTransform)
      {
        int num1 = (int) eventInstance.set3DAttributes(FMODUtility.To3DAttributes(this.m_ownerTransform));
      }
      int num2 = (int) eventInstance.start();
      this.m_eventInstance = eventInstance;
      return true;
    }

    private void StopInstance()
    {
      switch (this.m_stopMode)
      {
        case AudioEventPlayableAsset.StopMode.None:
          int num1 = (int) this.m_eventInstance.release();
          this.m_eventInstance.clearHandle();
          break;
        case AudioEventPlayableAsset.StopMode.Immediate:
          int num2 = (int) this.m_eventInstance.stop(STOP_MODE.IMMEDIATE);
          goto case AudioEventPlayableAsset.StopMode.None;
        case AudioEventPlayableAsset.StopMode.AllowFadeout:
          int num3 = (int) this.m_eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
          goto case AudioEventPlayableAsset.StopMode.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
