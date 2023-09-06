// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioEventPlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Utilities;
using FMODUnity;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Audio
{
  [Serializable]
  public sealed class AudioEventPlayableAsset : 
    PlayableAsset,
    ITimelineClipAsset,
    ITimelineResourcesProvider
  {
    [UsedImplicitly]
    [SerializeField]
    [AudioEventReference(AudioEventReferenceType.Guid)]
    private string m_eventGuid;
    [UsedImplicitly]
    [SerializeField]
    private AudioEventPlayableAsset.StopMode m_stopMode;
    [UsedImplicitly]
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_volume = 1f;
    [UsedImplicitly]
    [SerializeField]
    private AudioEventParameterDictionary m_parameters = new AudioEventParameterDictionary();
    private bool m_loadedResources;

    public IEnumerator LoadResources()
    {
      if (!string.IsNullOrEmpty(this.m_eventGuid))
      {
        while (!AudioManager.isReady)
        {
          if ((int) AudioManager.error != 0)
            yield break;
          else
            yield return (object) null;
        }
        string bankName;
        if (AudioManager.TryGetDefaultBankName(this.m_eventGuid, out bankName))
        {
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error != 0)
          {
            Log.Error(string.Format("Failed to load bank named '{0}': {1}", (object) bankName, (object) bankLoadRequest.error), 84, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioEventPlayableAsset.cs");
          }
          else
          {
            this.m_loadedResources = true;
            bankLoadRequest = (AudioBankLoadRequest) null;
          }
        }
        else
          Log.Warning("Could not find a bank to load for event '" + this.m_eventGuid + "'.", 92, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioEventPlayableAsset.cs");
      }
    }

    public void UnloadResources()
    {
      string bankName;
      if (!this.m_loadedResources || !AudioManager.isReady || !AudioManager.TryGetDefaultBankName(this.m_eventGuid, out bankName))
        return;
      AudioManager.UnloadBank(bankName);
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      if (string.IsNullOrEmpty(this.m_eventGuid))
        return Playable.Null;
      Guid exact = Guid.ParseExact(this.m_eventGuid, "N");
      if (exact == Guid.Empty)
        return Playable.Null;
      AudioContext context = TimelineContextUtility.GetContext<AudioContext>(graph);
      AudioEventPlayableBehaviour template = new AudioEventPlayableBehaviour(exact, this.m_stopMode, this.m_volume, this.m_parameters, context, owner.transform);
      return (Playable) ScriptPlayable<AudioEventPlayableBehaviour>.Create(graph, template);
    }

    public ClipCaps clipCaps => ClipCaps.Blending;

    public enum StopMode
    {
      None,
      Immediate,
      AllowFadeout,
    }
  }
}
