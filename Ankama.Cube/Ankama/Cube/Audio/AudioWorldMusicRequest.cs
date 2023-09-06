// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioWorldMusicRequest
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.Utilities;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  public sealed class AudioWorldMusicRequest : CustomYieldInstruction
  {
    private static readonly List<AudioWorldMusicRequest> s_instances = new List<AudioWorldMusicRequest>();
    public readonly AudioReferenceWithParameters music;
    public readonly AudioReferenceWithParameters ambiance;
    public readonly AudioContext context;
    private AudioWorldMusicRequest.PendingStateChange m_pendingState;
    private AudioBankLoadRequest m_musicBankLoadRequest;
    private AudioBankLoadRequest m_ambianceBankLoadRequest;

    public EventInstance musicEventInstance { get; private set; }

    public EventInstance ambianceEventInstance { get; private set; }

    public override bool keepWaiting
    {
      get
      {
        this.UpdateInternal();
        return this.state == AudioWorldMusicRequest.State.Loading || this.state == AudioWorldMusicRequest.State.Stopping;
      }
    }

    public AudioWorldMusicRequest.State state { get; private set; }

    public AssetManagerError error { get; private set; } = (AssetManagerError) 0;

    internal static void ClearAllRequests()
    {
      List<AudioWorldMusicRequest> instances = AudioWorldMusicRequest.s_instances;
      int count = instances.Count;
      for (int index = 0; index < count; ++index)
        instances[index].CancelInternal();
      AudioWorldMusicRequest.s_instances.Clear();
    }

    internal AudioWorldMusicRequest(
      AudioReferenceWithParameters music,
      AudioReferenceWithParameters ambiance,
      [CanBeNull] AudioContext context,
      bool playAutomatically)
    {
      this.music = music;
      this.ambiance = ambiance;
      this.context = context;
      this.m_pendingState = playAutomatically ? AudioWorldMusicRequest.PendingStateChange.Play : AudioWorldMusicRequest.PendingStateChange.None;
      Guid eventGuid1 = music.eventGuid;
      Guid eventGuid2 = ambiance.eventGuid;
      if (!AssetManager.isReady)
      {
        Log.Error("Tried to load a world music but the AudioManager isn't ready.", 85, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioWorldMusicRequest.cs");
        this.Fail((AssetManagerError) 60);
      }
      else
      {
        string bankName1;
        if (eventGuid1 != Guid.Empty)
        {
          if (!AudioManager.TryGetDefaultBankName(eventGuid1, out bankName1))
          {
            Log.Warning(string.Format("Could not get default bank name for requested world music with guid {0}.", (object) eventGuid1), 95, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioWorldMusicRequest.cs");
            this.Fail((AssetManagerError) 10);
            return;
          }
        }
        else
          bankName1 = string.Empty;
        string bankName2;
        if (eventGuid2 != Guid.Empty)
        {
          if (!AudioManager.TryGetDefaultBankName(eventGuid2, out bankName2))
          {
            Log.Warning(string.Format("Could not get default bank name for requested world ambiance with guid {0}.", (object) eventGuid1), 110, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioWorldMusicRequest.cs");
            this.Fail((AssetManagerError) 10);
            return;
          }
        }
        else
          bankName2 = string.Empty;
        if (bankName1.Length > 0)
          this.m_musicBankLoadRequest = AudioManager.LoadBankAsync(bankName1);
        if (bankName2.Length > 0 && !bankName1.Equals(bankName2))
          this.m_ambianceBankLoadRequest = AudioManager.LoadBankAsync(bankName2);
        this.state = AudioWorldMusicRequest.State.Loading;
        AudioWorldMusicRequest.s_instances.Add(this);
        this.UpdateInternal();
      }
    }

    private void UpdateInternal()
    {
      switch (this.state)
      {
        case AudioWorldMusicRequest.State.None:
          break;
        case AudioWorldMusicRequest.State.Loading:
          if (this.m_musicBankLoadRequest != null && !this.m_musicBankLoadRequest.isDone || this.m_ambianceBankLoadRequest != null && !this.m_ambianceBankLoadRequest.isDone)
            break;
          if (this.m_musicBankLoadRequest != null && (int) this.m_musicBankLoadRequest.error != 0)
          {
            Log.Warning("Could not load audio bank named '" + this.m_musicBankLoadRequest.bankName + "' for requested world music.", 149, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioWorldMusicRequest.cs");
            this.Fail(this.m_musicBankLoadRequest.error);
            break;
          }
          if (this.m_ambianceBankLoadRequest != null && (int) this.m_ambianceBankLoadRequest.error != 0)
          {
            Log.Warning("Could not load audio bank named '" + this.m_ambianceBankLoadRequest.bankName + "' for requested world ambiance.", 156, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioWorldMusicRequest.cs");
            this.Fail(this.m_ambianceBankLoadRequest.error);
            break;
          }
          EventInstance eventInstance1;
          if (this.music.isValid && AudioManager.TryCreateInstance(this.music, out eventInstance1))
          {
            if (this.context != null)
              this.context.AddEventInstance(eventInstance1);
            this.music.ApplyParameters(eventInstance1);
            this.musicEventInstance = eventInstance1;
          }
          EventInstance eventInstance2;
          if (this.ambiance.isValid && AudioManager.TryCreateInstance(this.ambiance, out eventInstance2))
          {
            if (this.context != null)
              this.context.AddEventInstance(eventInstance2);
            this.ambiance.ApplyParameters(eventInstance2);
            this.ambianceEventInstance = eventInstance2;
          }
          this.state = AudioWorldMusicRequest.State.Loaded;
          switch (this.m_pendingState)
          {
            case AudioWorldMusicRequest.PendingStateChange.None:
              return;
            case AudioWorldMusicRequest.PendingStateChange.Play:
              this.StartInternal();
              return;
            case AudioWorldMusicRequest.PendingStateChange.Stop:
              this.UnloadInternal();
              return;
            default:
              throw new ArgumentOutOfRangeException();
          }
        case AudioWorldMusicRequest.State.Loaded:
          break;
        case AudioWorldMusicRequest.State.Playing:
          break;
        case AudioWorldMusicRequest.State.Stopping:
          EventInstance musicEventInstance = this.musicEventInstance;
          EventInstance ambianceEventInstance = this.ambianceEventInstance;
          bool flag = false;
          if (musicEventInstance.isValid())
          {
            PLAYBACK_STATE state;
            if (musicEventInstance.getPlaybackState(out state) == RESULT.OK && state != PLAYBACK_STATE.STOPPED)
            {
              flag = true;
            }
            else
            {
              int num = (int) musicEventInstance.release();
              musicEventInstance.clearHandle();
            }
          }
          if (ambianceEventInstance.isValid())
          {
            PLAYBACK_STATE state;
            if (ambianceEventInstance.getPlaybackState(out state) == RESULT.OK && state != PLAYBACK_STATE.STOPPED)
            {
              flag = true;
            }
            else
            {
              int num = (int) ambianceEventInstance.release();
              ambianceEventInstance.clearHandle();
            }
          }
          if (flag)
            break;
          this.UnloadInternal();
          this.state = AudioWorldMusicRequest.State.Stopped;
          break;
        case AudioWorldMusicRequest.State.Stopped:
          break;
        case AudioWorldMusicRequest.State.Error:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void Start()
    {
      if (this.m_pendingState != AudioWorldMusicRequest.PendingStateChange.None)
        return;
      this.m_pendingState = AudioWorldMusicRequest.PendingStateChange.Play;
      if (this.state != AudioWorldMusicRequest.State.Loaded)
        return;
      this.StartInternal();
    }

    public void Stop()
    {
      if (this.m_pendingState == AudioWorldMusicRequest.PendingStateChange.Stop)
        return;
      this.m_pendingState = AudioWorldMusicRequest.PendingStateChange.Stop;
      if (this.state == AudioWorldMusicRequest.State.Playing)
      {
        this.StopInternal();
      }
      else
      {
        if (this.state != AudioWorldMusicRequest.State.Loaded)
          return;
        this.UnloadInternal();
      }
    }

    private void StartInternal()
    {
      this.state = AudioWorldMusicRequest.State.Playing;
      EventInstance musicEventInstance = this.musicEventInstance;
      if (musicEventInstance.isValid())
      {
        int num1 = (int) musicEventInstance.start();
      }
      EventInstance ambianceEventInstance = this.ambianceEventInstance;
      if (!ambianceEventInstance.isValid())
        return;
      int num2 = (int) ambianceEventInstance.start();
    }

    private void StopInternal()
    {
      this.state = AudioWorldMusicRequest.State.Stopping;
      EventInstance musicEventInstance = this.musicEventInstance;
      EventInstance ambianceEventInstance = this.ambianceEventInstance;
      if (musicEventInstance.isValid())
      {
        int num1 = (int) musicEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
      }
      if (musicEventInstance.isValid())
      {
        int num2 = (int) ambianceEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
      }
      this.UpdateInternal();
    }

    private void UnloadInternal()
    {
      if (this.m_musicBankLoadRequest != null)
      {
        if ((int) this.m_musicBankLoadRequest.error == 0)
          AudioManager.UnloadBank(this.m_musicBankLoadRequest.bankName);
        this.m_musicBankLoadRequest = (AudioBankLoadRequest) null;
      }
      if (this.m_ambianceBankLoadRequest != null)
      {
        if ((int) this.m_ambianceBankLoadRequest.error == 0)
          AudioManager.UnloadBank(this.m_ambianceBankLoadRequest.bankName);
        this.m_ambianceBankLoadRequest = (AudioBankLoadRequest) null;
      }
      AudioWorldMusicRequest.s_instances.Remove(this);
    }

    private void CancelInternal()
    {
      EventInstance musicEventInstance = this.musicEventInstance;
      if (musicEventInstance.isValid())
      {
        int num1 = (int) musicEventInstance.stop(STOP_MODE.IMMEDIATE);
        int num2 = (int) musicEventInstance.release();
        musicEventInstance.clearHandle();
      }
      EventInstance ambianceEventInstance = this.ambianceEventInstance;
      if (ambianceEventInstance.isValid())
      {
        int num3 = (int) ambianceEventInstance.stop(STOP_MODE.IMMEDIATE);
        int num4 = (int) ambianceEventInstance.release();
        ambianceEventInstance.clearHandle();
      }
      if (this.m_musicBankLoadRequest != null)
      {
        if ((int) this.m_musicBankLoadRequest.error == 0)
          AudioManager.UnloadBank(this.m_musicBankLoadRequest.bankName);
        this.m_musicBankLoadRequest = (AudioBankLoadRequest) null;
      }
      if (this.m_ambianceBankLoadRequest != null)
      {
        if ((int) this.m_ambianceBankLoadRequest.error == 0)
          AudioManager.UnloadBank(this.m_ambianceBankLoadRequest.bankName);
        this.m_ambianceBankLoadRequest = (AudioBankLoadRequest) null;
      }
      this.error = (AssetManagerError) 50;
      this.state = AudioWorldMusicRequest.State.Error;
    }

    private void Fail(AssetManagerError e)
    {
      this.error = e;
      this.state = AudioWorldMusicRequest.State.Error;
      this.UnloadInternal();
    }

    public enum State
    {
      None,
      Loading,
      Loaded,
      Playing,
      Stopping,
      Stopped,
      Error,
    }

    private enum PendingStateChange
    {
      None,
      Play,
      Stop,
    }
  }
}
