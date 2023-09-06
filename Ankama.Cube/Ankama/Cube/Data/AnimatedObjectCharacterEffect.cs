// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedObjectCharacterEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Cube.Animations;
using Ankama.Cube.Audio;
using Ankama.Cube.Fight;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using FMODUnity;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Character Effects/Animated Object")]
  public sealed class AnimatedObjectCharacterEffect : 
    CharacterEffect,
    ICharacterEffectWithAudioReference
  {
    [SerializeField]
    private AnimatedObjectDefinition m_animatedObjectDefinition;
    [SerializeField]
    private string m_animationName = string.Empty;
    [SerializeField]
    private bool m_appendDirectionSuffix;
    [SerializeField]
    private AudioReferenceWithParameters m_sound;
    [NonSerialized]
    private bool m_loadedAudioBank;

    protected override IEnumerator LoadInternal()
    {
      AnimatedObjectCharacterEffect objectCharacterEffect = this;
      if (objectCharacterEffect.m_sound.isValid)
      {
        string bankName;
        if (AudioManager.TryGetDefaultBankName((AudioReference) objectCharacterEffect.m_sound, out bankName))
        {
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            objectCharacterEffect.m_loadedAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of animated character effect named '" + objectCharacterEffect.name + "'.", 58, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AnimatedObjectCharacterEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of animated character effect named '" + objectCharacterEffect.name + "'.", 63, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AnimatedObjectCharacterEffect.cs");
        bankName = (string) null;
      }
      objectCharacterEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
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
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animatedObjectDefinition)
      {
        Log.Warning("Tried to instantiate animated object character effect named '" + this.name + "' without an animated object definition setup.", 90, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\AnimatedObjectCharacterEffect.cs");
        return (Component) null;
      }
      string animationName;
      if (!string.IsNullOrEmpty(this.m_animationName))
      {
        if (this.m_appendDirectionSuffix && contextProvider != null)
        {
          Direction direction = Direction.None;
          if (contextProvider.GetTimelineContext() is CharacterObjectContext timelineContext)
          {
            CharacterObject characterObject = timelineContext.characterObject;
            if ((UnityEngine.Object) null != (UnityEngine.Object) characterObject)
              direction = characterObject.direction;
          }
          animationName = direction != Direction.None ? this.m_animationName + (object) (int) direction : this.m_animationName;
        }
        else
          animationName = this.m_animationName;
      }
      else
        animationName = string.Empty;
      if (this.m_sound.isValid)
        AudioManager.PlayOneShot(this.m_sound, parent);
      return (Component) FightObjectFactory.CreateAnimatedObjectEffectInstance(this.m_animatedObjectDefinition, animationName, parent);
    }

    public override IEnumerator DestroyWhenFinished(Component instance)
    {
      Animator2D animator2D = (Animator2D) instance;
      do
      {
        yield return (object) null;
        if ((UnityEngine.Object) null == (UnityEngine.Object) animator2D)
          yield break;
      }
      while (!animator2D.reachedEndOfAnimation);
      FightObjectFactory.DestroyAnimatedObjectEffectInstance(animator2D);
    }
  }
}
