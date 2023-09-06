// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AttachableEffect
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
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Attachable Effects/Attachable Effect")]
  public class AttachableEffect : ScriptableEffect
  {
    [SerializeField]
    private AttachableEffect.EffectData m_mainEffect;
    [SerializeField]
    private AttachableEffect.EffectData m_stopEffect;
    [NonSerialized]
    private bool m_loadedMainEffectAudioBank;
    [NonSerialized]
    private bool m_loadedStopEffectAudioBank;

    public AudioReferenceWithParameters mainEffectAudioReference => this.m_mainEffect.sound;

    public float mainEffectDelay => this.m_mainEffect.delay;

    public AudioReferenceWithParameters stopEffectAudioReference => this.m_stopEffect.sound;

    public float stopEffectDelay => this.m_stopEffect.delay;

    protected override IEnumerator LoadInternal()
    {
      AttachableEffect attachableEffect = this;
      AudioReferenceWithParameters sound1 = attachableEffect.m_mainEffect.sound;
      string bankName;
      AudioBankLoadRequest bankLoadRequest;
      if (sound1.isValid)
      {
        if (AudioManager.TryGetDefaultBankName((AudioReference) sound1, out bankName))
        {
          bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            attachableEffect.m_loadedMainEffectAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of visual character effect named '" + attachableEffect.name + "'.", 88, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AttachableEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of visual effect named '" + attachableEffect.name + "'.", 93, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AttachableEffect.cs");
        bankName = (string) null;
      }
      AudioReferenceWithParameters sound2 = attachableEffect.m_stopEffect.sound;
      if (sound2.isValid)
      {
        if (AudioManager.TryGetDefaultBankName((AudioReference) sound2, out bankName))
        {
          bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            attachableEffect.m_loadedStopEffectAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of visual character effect named '" + attachableEffect.name + "'.", 115, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AttachableEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of visual effect named '" + attachableEffect.name + "'.", 120, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AttachableEffect.cs");
        bankName = (string) null;
      }
      attachableEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
    }

    protected override void UnloadInternal()
    {
      AudioReferenceWithParameters sound1 = this.m_mainEffect.sound;
      string bankName1;
      if (sound1.isValid && this.m_loadedMainEffectAudioBank && AudioManager.TryGetDefaultBankName((AudioReference) sound1, out bankName1))
      {
        AudioManager.UnloadBank(bankName1);
        this.m_loadedMainEffectAudioBank = false;
      }
      AudioReferenceWithParameters sound2 = this.m_stopEffect.sound;
      string bankName2;
      if (sound2.isValid && this.m_loadedStopEffectAudioBank && AudioManager.TryGetDefaultBankName((AudioReference) sound2, out bankName2))
      {
        AudioManager.UnloadBank(bankName2);
        this.m_loadedStopEffectAudioBank = false;
      }
      this.m_initializationState = ScriptableEffect.InitializationState.None;
    }

    [CanBeNull]
    public VisualEffect InstantiateMainEffect(
      [NotNull] Transform parent,
      [CanBeNull] ITimelineContextProvider contextProvider)
    {
      AttachableEffect.EffectData mainEffect = this.m_mainEffect;
      VisualEffect visualEffect1 = mainEffect.visualEffect;
      if ((UnityEngine.Object) null == (UnityEngine.Object) visualEffect1)
      {
        Log.Warning("Tried to instantiate attachable effect named '" + this.name + "' without a visual effect setup.", 163, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AttachableEffect.cs");
        return (VisualEffect) null;
      }
      Vector3 position = parent.position + mainEffect.positionOffset;
      Quaternion rotation = Quaternion.identity;
      CameraHandler current = CameraHandler.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        rotation = current.mapRotation.GetInverseRotation();
      AudioReferenceWithParameters sound = mainEffect.sound;
      if (sound.isValid)
        AudioManager.PlayOneShot(sound, parent);
      VisualEffect visualEffect2 = VisualEffectFactory.Instantiate(visualEffect1, position, rotation, Vector3.one, parent);
      visualEffect2.destructionOverride = new Action<VisualEffect>(this.OnMainEffectInstanceDestructionRequest);
      return visualEffect2;
    }

    [CanBeNull]
    public VisualEffect InstantiateStopEffect(
      [NotNull] Transform parent,
      [CanBeNull] ITimelineContextProvider contextProvider)
    {
      AttachableEffect.EffectData stopEffect = this.m_stopEffect;
      VisualEffect visualEffect1 = stopEffect.visualEffect;
      if ((UnityEngine.Object) null == (UnityEngine.Object) visualEffect1)
        return (VisualEffect) null;
      Vector3 position = parent.position + stopEffect.positionOffset;
      Quaternion rotation = Quaternion.identity;
      CameraHandler current = CameraHandler.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        rotation = current.mapRotation.GetInverseRotation();
      AudioReferenceWithParameters sound = stopEffect.sound;
      if (sound.isValid)
        AudioManager.PlayOneShot(sound, parent);
      VisualEffect visualEffect2 = VisualEffectFactory.Instantiate(visualEffect1, position, rotation, Vector3.one, parent);
      visualEffect2.destructionOverride = new Action<VisualEffect>(this.OnStopEffectInstanceDestructionRequest);
      return visualEffect2;
    }

    private void OnMainEffectInstanceDestructionRequest(VisualEffect instance) => VisualEffectFactory.Release(this.m_mainEffect.visualEffect, instance);

    private void OnStopEffectInstanceDestructionRequest(VisualEffect instance) => VisualEffectFactory.Release(this.m_stopEffect.visualEffect, instance);

    [Serializable]
    private struct EffectData
    {
      [SerializeField]
      public VisualEffect visualEffect;
      [SerializeField]
      public Vector3 positionOffset;
      [SerializeField]
      public AudioReferenceWithParameters sound;
      [SerializeField]
      public float delay;
    }
  }
}
