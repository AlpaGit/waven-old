// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.HavreDimensionMainState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Zaap;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using DataEditor;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.States
{
  public class HavreDimensionMainState : LoadSceneStateContext
  {
    private HavreMap m_havreMap;
    private string m_lastLoadedCharacterBundle;
    private Coroutine m_loadCharacterCoroutine;
    private string m_lastLoadedGodBundle;
    private Coroutine m_loadGodCoroutine;

    protected override IEnumerator Load()
    {
      HavreDimensionMainState dimensionMainState = this;
      string sceneName = ScenesUtility.GetHavreMapSceneName(PlayerData.instance.havreMapSceneIndex);
      yield return (object) dimensionMainState.LoadSceneAndBundleRequest(sceneName, "core/scenes/maps/havre_maps");
      if (!LoadSceneStateContext.TryGetSceneAndRoot<HavreMap>(sceneName, out Scene _, out dimensionMainState.m_havreMap))
      {
        Log.Error("could not find Havre Map", 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\HavreDimension\\HavreDimensionMainState.cs");
      }
      else
      {
        yield return (object) dimensionMainState.m_havreMap.Initialize();
        yield return (object) dimensionMainState.LoadCharacterSkin();
        yield return (object) dimensionMainState.LoadGod();
        dimensionMainState.m_havreMap.InitEnterAnimFirstFrame();
      }
    }

    protected override IEnumerator Update()
    {
      yield return (object) this.m_havreMap.PlayEnerAnim();
    }

    protected override void Enable()
    {
      base.Enable();
      this.m_havreMap.onPvPTrigger = new Action(this.OnPvPTrigger);
      this.m_havreMap.onGodTrigger = new Action(this.OnGodTrigger);
      PlayerData.instance.OnPlayerHeroSkinChanged += new Action(this.UpdateSkin);
      PlayerData.instance.OnPlayerGodChanged += new Action(this.UpdateGod);
      this.m_havreMap.Begin();
    }

    protected override void Disable()
    {
      base.Disable();
      this.m_havreMap.onPvPTrigger = (Action) null;
      this.m_havreMap.onGodTrigger = (Action) null;
      PlayerData.instance.OnPlayerHeroSkinChanged -= new Action(this.UpdateSkin);
      PlayerData.instance.OnPlayerGodChanged -= new Action(this.UpdateGod);
    }

    protected override IEnumerator Unload()
    {
      if (this.m_loadGodCoroutine != null)
        Main.monoBehaviour.StopCoroutine(this.m_loadGodCoroutine);
      if (this.m_loadCharacterCoroutine != null)
        Main.monoBehaviour.StopCoroutine(this.m_loadCharacterCoroutine);
      this.m_havreMap.Release();
      yield return (object) base.Unload();
    }

    private void OnPvPTrigger()
    {
      UIZaapState state = new UIZaapState();
      state.onDisable += new Action(this.OnOpenStateDisable);
      state.onTransition += new Action(this.OnOpenStateTransition);
      this.OpenState((StateContext) state);
    }

    private void OnGodTrigger()
    {
      GodSelectionState state = new GodSelectionState();
      state.onDisable += new Action(this.OnOpenStateDisable);
      state.onTransition += new Action(this.OnOpenStateTransition);
      this.OpenState((StateContext) state);
    }

    private void OpenState(StateContext state)
    {
      this.m_havreMap.SetInteractable(false);
      StateLayer stateLayer1;
      if (StateManager.TryGetLayer("OptionUI", out stateLayer1) && stateLayer1.HasChildState() && stateLayer1.GetChildState().HasChildState())
      {
        this.m_havreMap.SetInteractable(true);
      }
      else
      {
        StateLayer stateLayer2;
        if (!StateManager.TryGetLayer("PlayerUI", out stateLayer2))
          return;
        if (stateLayer2.HasChildState() && stateLayer2.GetChildState().HasChildState())
        {
          this.m_havreMap.SetInteractable(true);
        }
        else
        {
          StateManager.SetActiveInputLayer(stateLayer2);
          stateLayer2.GetChainEnd().SetChildState(state);
        }
      }
    }

    private void OnOpenStateDisable() => this.m_havreMap.SetInteractable(true);

    private void OnOpenStateTransition() => this.m_havreMap.MoveCharacterOutsideZaap();

    private void UpdateGod()
    {
      if (this.m_loadGodCoroutine != null)
        Main.monoBehaviour.StopCoroutine(this.m_loadGodCoroutine);
      this.m_loadGodCoroutine = Main.monoBehaviour.StartCoroutine(this.LoadGod());
    }

    private void UpdateSkin()
    {
      if (this.m_loadCharacterCoroutine != null)
        Main.monoBehaviour.StopCoroutine(this.m_loadCharacterCoroutine);
      this.m_loadCharacterCoroutine = Main.monoBehaviour.StartCoroutine(this.LoadCharacterSkin());
    }

    private IEnumerator LoadGod()
    {
      HavreDimensionMainState dimensionMainState = this;
      if (!string.IsNullOrEmpty(dimensionMainState.m_lastLoadedGodBundle))
      {
        dimensionMainState.UnloadAssetBundle(dimensionMainState.m_lastLoadedGodBundle, true, true);
        dimensionMainState.m_lastLoadedGodBundle = (string) null;
      }
      God god = PlayerData.instance.god;
      GodDefinition godDefinition;
      if (RuntimeData.godDefinitions.TryGetValue(god, out godDefinition))
      {
        AssetReference statuePrefabReference = godDefinition.statuePrefabReference;
        string bundleName = AssetBundlesUtility.GetUIGodsResourcesBundleName();
        dimensionMainState.m_lastLoadedGodBundle = bundleName;
        AssetBundleLoadRequest bundleRequest = dimensionMainState.LoadAssetBundle(bundleName);
        while (!bundleRequest.isDone)
          yield return (object) null;
        if ((int) bundleRequest.error != 0)
        {
          Log.Error(string.Format("Could not load bundle named '{0}': {1}", (object) bundleName, (object) bundleRequest.error), 196, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\HavreDimension\\HavreDimensionMainState.cs");
        }
        else
        {
          AssetLoadRequest<GameObject> assetReferenceRequest = statuePrefabReference.LoadFromAssetBundleAsync<GameObject>(bundleName);
          while (!assetReferenceRequest.isDone)
            yield return (object) null;
          if ((int) assetReferenceRequest.error != 0)
          {
            Log.Error(string.Format("Could not load requested asset ({0}) from bundle named '{1}': {2}", (object) statuePrefabReference.value, (object) bundleName, (object) assetReferenceRequest.error), 208, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\HavreDimension\\HavreDimensionMainState.cs");
          }
          else
          {
            GameObject asset = assetReferenceRequest.asset;
            dimensionMainState.m_havreMap.godZaap.SetStatue(asset);
            dimensionMainState.m_loadGodCoroutine = (Coroutine) null;
          }
        }
      }
    }

    private IEnumerator LoadCharacterSkin()
    {
      HavreDimensionMainState dimensionMainState = this;
      if (!string.IsNullOrEmpty(dimensionMainState.m_lastLoadedCharacterBundle))
      {
        dimensionMainState.UnloadAssetBundle(dimensionMainState.m_lastLoadedCharacterBundle, true, true);
        dimensionMainState.m_lastLoadedCharacterBundle = (string) null;
      }
      PlayerData instance = PlayerData.instance;
      Gender gender = instance.gender;
      Id<CharacterSkinDefinition> skin = instance.Skin;
      CharacterSkinDefinition characterSkinDefinition;
      if (!(skin == (Id<CharacterSkinDefinition>) null) && RuntimeData.characterSkinDefinitions.TryGetValue(skin.value, out characterSkinDefinition))
      {
        string bundleName = AssetBundlesUtility.GetAnimatedCharacterDataBundle(characterSkinDefinition.bundleCategory);
        dimensionMainState.m_lastLoadedCharacterBundle = bundleName;
        AssetBundleLoadRequest bundleLoadRequest = dimensionMainState.LoadAssetBundle(bundleName);
        while (!bundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) bundleLoadRequest.error != 0)
        {
          Log.Error(string.Format("Failed to load asset bundle named '{0}' for character skin {1} ({2}): {3}", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) bundleLoadRequest.error), 253, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\HavreDimension\\HavreDimensionMainState.cs");
        }
        else
        {
          AssetLoadRequest<AnimatedCharacterData> animatedCharacterDataLoadRequest = characterSkinDefinition.GetAnimatedCharacterDataReference(gender).LoadFromAssetBundleAsync<AnimatedCharacterData>(bundleName);
          while (!animatedCharacterDataLoadRequest.isDone)
            yield return (object) null;
          if ((int) animatedCharacterDataLoadRequest.error != 0)
          {
            dimensionMainState.UnloadAssetBundle(bundleName, true, true);
            Log.Error(string.Format("Failed to load {0} asset from bundle '{1}' for character skin {2} ({3}): {4}", (object) "AnimatedCharacterData", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) animatedCharacterDataLoadRequest.error), 269, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\HavreDimension\\HavreDimensionMainState.cs");
          }
          else
          {
            AnimatedCharacterData animatedCharacterData = animatedCharacterDataLoadRequest.asset;
            yield return (object) animatedCharacterData.LoadTimelineResources();
            AnimatedFightCharacterData data = animatedCharacterData as AnimatedFightCharacterData;
            AnimatedObjectDefinition objectDefinition = data.animatedObjectDefinition;
            dimensionMainState.m_havreMap.character.SetCharacterData(data, objectDefinition);
            dimensionMainState.m_loadCharacterCoroutine = (Coroutine) null;
          }
        }
      }
    }
  }
}
