// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.VisualSpellEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Audio;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Spell Effects/Visual Effect")]
  public class VisualSpellEffect : SpellEffect, ISpellEffectWithAudioReference
  {
    [SerializeField]
    private VisualEffect m_visualEffect;
    [SerializeField]
    private Vector3 m_positionOffset = new Vector3(0.0f, 0.5f, 0.0f);
    [SerializeField]
    private AudioReferenceWithParameters m_sound;
    [NonSerialized]
    private bool m_loadedAudioBank;

    protected override IEnumerator LoadInternal()
    {
      VisualSpellEffect visualSpellEffect = this;
      if (visualSpellEffect.m_sound.isValid)
      {
        string bankName;
        if (AudioManager.TryGetDefaultBankName((AudioReference) visualSpellEffect.m_sound, out bankName))
        {
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            visualSpellEffect.m_loadedAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of visual character effect named '" + visualSpellEffect.name + "'.", 52, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualSpellEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of visual effect named '" + visualSpellEffect.name + "'.", 57, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualSpellEffect.cs");
        bankName = (string) null;
      }
      visualSpellEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
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
      Quaternion rotation,
      Vector3 scale,
      FightContext fightContext,
      ITimelineContextProvider contextProvider)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_visualEffect)
      {
        Log.Warning("Tried to instantiate visual character effect named '" + this.name + "' without a visual effect setup.", 84, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\VisualSpellEffect.cs");
        return (Component) null;
      }
      Vector3 position = parent.position + this.m_positionOffset;
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
  }
}
