// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloatingCounterEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using DG.Tweening;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Attachable Effects/Floating Counter Effect")]
  public sealed class FloatingCounterEffect : ScriptableEffect, ISpellEffectOverrideProvider
  {
    [Space(10f)]
    [SerializeField]
    private FloatingCounterFloatingObject m_floatingObject;
    [SerializeField]
    private float m_radius = 0.5f;
    [SerializeField]
    private float m_height = 0.5f;
    [SerializeField]
    private float m_rotationSpeed = 100f;
    [SerializeField]
    private Vector3 m_rotationAxis = new Vector3(0.0f, 1f, 0.0f);
    [SerializeField]
    private float m_repositionDuration = 0.25f;
    [SerializeField]
    private Ease m_repositionEase = Ease.InOutExpo;
    [Space(10f)]
    [SerializeField]
    private VisualEffect m_spawnFX;
    [SerializeField]
    private Vector3 m_spawnFXOffset = new Vector3(0.0f, 0.5f, 0.0f);
    [SerializeField]
    private AudioReferenceWithParameters m_spawnSound;
    [SerializeField]
    private float m_startingAnimationDuration = 0.25f;
    [Space(10f)]
    [SerializeField]
    private float m_endAnimationDuration = 0.5f;
    [SerializeField]
    private float m_clearAnimationDuration = 0.25f;
    [Space(10f)]
    [SerializeField]
    private SpellEffectDictionary m_spellEffectOverrides;
    [NonSerialized]
    private bool m_loadedSpawnEffectAudioBank;

    public Vector3 rotation => this.m_rotationAxis * this.m_rotationSpeed;

    public float radius => this.m_radius;

    public float height => this.m_height;

    public FloatingCounterFloatingObject floatingObject => this.m_floatingObject;

    public VisualEffect spawnFX => this.m_spawnFX;

    public Vector3 spawnFXOffset => this.m_spawnFXOffset;

    public float startingAnimationDuration => this.m_startingAnimationDuration;

    public float endAnimationDuration => this.m_endAnimationDuration;

    public float clearAnimationDuration => this.m_clearAnimationDuration;

    public float repositionDuration => this.m_repositionDuration;

    public Ease repositionEase => this.m_repositionEase;

    protected override IEnumerator LoadInternal()
    {
      FloatingCounterEffect floatingCounterEffect = this;
      AudioReferenceWithParameters spawnSound = floatingCounterEffect.m_spawnSound;
      if (spawnSound.isValid)
      {
        string bankName;
        if (AudioManager.TryGetDefaultBankName((AudioReference) spawnSound, out bankName))
        {
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error == 0)
            floatingCounterEffect.m_loadedSpawnEffectAudioBank = true;
          else
            Log.Warning("Could not load bank named '" + bankName + "' for sound of visual character effect named '" + floatingCounterEffect.name + "'.", 87, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\FloatingCounterEffect.cs");
          bankLoadRequest = (AudioBankLoadRequest) null;
        }
        else
          Log.Warning("Could not get default bank name for sound of visual effect named '" + floatingCounterEffect.name + "'.", 92, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\FloatingCounterEffect.cs");
        bankName = (string) null;
      }
      if (floatingCounterEffect.m_spellEffectOverrides != null)
        yield return (object) ScriptableEffect.LoadAll<SpellEffect>((ICollection<SpellEffect>) floatingCounterEffect.m_spellEffectOverrides.Values);
      floatingCounterEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
    }

    protected override void UnloadInternal()
    {
      AudioReferenceWithParameters spawnSound = this.m_spawnSound;
      string bankName;
      if (spawnSound.isValid && this.m_loadedSpawnEffectAudioBank && AudioManager.TryGetDefaultBankName((AudioReference) spawnSound, out bankName))
      {
        AudioManager.UnloadBank(bankName);
        this.m_loadedSpawnEffectAudioBank = false;
      }
      if (this.m_spellEffectOverrides != null)
      {
        foreach (ScriptableEffect scriptableEffect in this.m_spellEffectOverrides.Values)
          scriptableEffect.Unload();
      }
      this.m_initializationState = ScriptableEffect.InitializationState.None;
    }

    public bool TryGetSpellEffectOverride(SpellEffectKey key, out SpellEffect spellEffect)
    {
      if (this.m_spellEffectOverrides != null)
        return this.m_spellEffectOverrides.TryGetValue(key, out spellEffect);
      spellEffect = (SpellEffect) null;
      return false;
    }

    public void PlaySound(Transform transform) => AudioManager.PlayOneShot(this.m_spawnSound, transform);
  }
}
