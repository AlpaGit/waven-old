// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CellObjectAnimationPlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public sealed class CellObjectAnimationPlayableAsset : 
    PlayableAsset,
    ITimelineClipAsset,
    ITimelineResourcesProvider
  {
    [SerializeField]
    private CellObjectAnimationParameters m_parameters;
    [SerializeField]
    private float m_strength = 1f;
    [SerializeField]
    private CellObjectAnimationPlayableAsset.OrientationMethod m_orientationMethod;
    [SerializeField]
    private Vector2Int m_offset = Vector2Int.zero;
    private bool m_loadedResources;

    public IEnumerator LoadResources()
    {
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_parameters))
      {
        AudioReferenceWithParameters sound = this.m_parameters.sound;
        if (sound.isValid)
        {
          while (!AudioManager.isReady)
          {
            if ((int) AudioManager.error != 0)
              yield break;
            else
              yield return (object) null;
          }
          string bankName;
          if (AudioManager.TryGetDefaultBankName((AudioReference) sound, out bankName))
          {
            AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
            while (!bankLoadRequest.isDone)
              yield return (object) null;
            if ((int) bankLoadRequest.error != 0)
            {
              Log.Error(string.Format("Failed to load bank named '{0}': {1}", (object) bankName, (object) bankLoadRequest.error), 80, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\CellObjectAnimationPlayableAsset.cs");
            }
            else
            {
              this.m_loadedResources = true;
              bankLoadRequest = (AudioBankLoadRequest) null;
            }
          }
          else
            Log.Warning("Could not find a bank to load sound for cell object animation parameters named '" + this.m_parameters.name + "'.", 88, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\CellObjectAnimationPlayableAsset.cs");
        }
      }
    }

    public void UnloadResources()
    {
      string bankName;
      if (!this.m_loadedResources || !AudioManager.isReady || !AudioManager.TryGetDefaultBankName((AudioReference) this.m_parameters.sound, out bankName))
        return;
      AudioManager.UnloadBank(bankName);
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_parameters || !this.m_parameters.isValid || Mathf.Approximately(this.m_strength, 0.0f))
        return Playable.Null;
      Transform parent = owner.transform.parent;
      if ((UnityEngine.Object) null == (UnityEngine.Object) parent)
        return Playable.Null;
      CellObject componentInParent = parent.GetComponentInParent<CellObject>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) componentInParent)
        return Playable.Null;
      AbstractFightMap parentMap = componentInParent.parentMap as AbstractFightMap;
      if ((UnityEngine.Object) null == (UnityEngine.Object) parentMap)
        return Playable.Null;
      Vector2Int coords = componentInParent.coords;
      Quaternion rotation;
      switch (this.m_orientationMethod)
      {
        case CellObjectAnimationPlayableAsset.OrientationMethod.None:
          rotation = Quaternion.identity;
          break;
        case CellObjectAnimationPlayableAsset.OrientationMethod.Context:
          VisualEffectContext context = TimelineContextUtility.GetContext<VisualEffectContext>(graph);
          if (context == null)
          {
            rotation = Quaternion.identity;
            break;
          }
          context.GetVisualEffectTransformation(out rotation, out Vector3 _);
          coords += this.m_offset.Rotate(rotation);
          break;
        case CellObjectAnimationPlayableAsset.OrientationMethod.Director:
          rotation = owner.transform.rotation;
          coords += this.m_offset.Rotate(rotation);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      CellObjectAnimationPlayableBehaviour template = new CellObjectAnimationPlayableBehaviour(parentMap, this.m_parameters, coords, rotation, this.m_strength);
      return (Playable) ScriptPlayable<CellObjectAnimationPlayableBehaviour>.Create(graph, template);
    }

    public ClipCaps clipCaps { get; }

    public enum OrientationMethod
    {
      None,
      Context,
      Director,
    }
  }
}
