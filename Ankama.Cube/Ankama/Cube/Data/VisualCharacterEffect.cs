// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.VisualCharacterEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Audio;
using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Character Effects/Visual Effect")]
  public sealed class VisualCharacterEffect : CharacterEffect, ICharacterEffectWithAudioReference
  {
    [SerializeField]
    private VisualEffect m_visualEffect;
    [SerializeField]
    private Vector3 m_positionOffset = Vector3.zero;
    [SerializeField]
    private VisualCharacterEffect.OrientationMethod m_orientationMethod;
    [SerializeField]
    private AudioReferenceWithParameters m_sound;
    [NonSerialized]
    private bool m_loadedAudioBank;

    protected override IEnumerator LoadInternal()
    {
      VisualCharacterEffect visualCharacterEffect = this;
      if (visualCharacterEffect.m_sound.isValid)
      {
        string bankName;
        if (AudioManager.TryGetDefaultBankName((AudioReference) visualCharacterEffect.m_sound, out bankName))
        {
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            visualCharacterEffect.m_loadedAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of visual character effect named '" + visualCharacterEffect.name + "'.", 63, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualCharacterEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of visual effect named '" + visualCharacterEffect.name + "'.", 68, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualCharacterEffect.cs");
        bankName = (string) null;
      }
      visualCharacterEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
    }

    protected override void UnloadInternal()
    {
      string bankName;
      if (this.m_sound.isValid && this.m_loadedAudioBank && AudioManager.TryGetDefaultBankName((AudioReference) this.m_sound, out bankName))
      {
        AudioManager.UnloadBank(bankName);
        this.m_loadedAudioBank = false;
      }
      this.m_initializationState = ScriptableEffect.InitializationState.None;
    }

    public override Component Instantiate(
      Transform parent,
      ITimelineContextProvider contextProvider)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_visualEffect)
      {
        Log.Warning("Tried to instantiate visual character effect named '" + this.name + "' without a visual effect setup.", 95, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualCharacterEffect.cs");
        return (Component) null;
      }
      Vector3 position = parent.position + this.m_positionOffset;
      Quaternion rotation = Quaternion.identity;
      Vector3 scale = Vector3.one;
      switch (this.m_orientationMethod)
      {
        case VisualCharacterEffect.OrientationMethod.None:
          CameraHandler current = CameraHandler.current;
          if ((UnityEngine.Object) null != (UnityEngine.Object) current)
          {
            rotation = current.mapRotation.GetInverseRotation();
            break;
          }
          break;
        case VisualCharacterEffect.OrientationMethod.Context:
          if (contextProvider != null && contextProvider.GetTimelineContext() is VisualEffectContext timelineContext)
          {
            timelineContext.GetVisualEffectTransformation(out rotation, out scale);
            break;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      if (this.m_sound.isValid)
        AudioManager.PlayOneShot(this.m_sound, parent);
      VisualEffect visualEffect = VisualEffectFactory.Instantiate(this.m_visualEffect, position, rotation, scale, parent);
      visualEffect.destructionOverride = new Action<VisualEffect>(this.OnInstanceDestructionRequest);
      return (Component) visualEffect;
    }

    public override IEnumerator DestroyWhenFinished(Component instance)
    {
      yield break;
    }

    private void OnInstanceDestructionRequest(VisualEffect instance) => VisualEffectFactory.Release(this.m_visualEffect, instance);

    private enum OrientationMethod
    {
      None,
      Context,
    }
  }
}
