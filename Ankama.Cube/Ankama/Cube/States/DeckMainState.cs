// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.DeckMainState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Network;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.DeckMaker;
using Ankama.Cube.UI.PlayerLayer;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.States
{
  public class DeckMainState : LoadSceneStateContext, IStateUIChildPriority
  {
    private DeckMakerFrame m_frame;
    private bool m_isBeingSave;
    private DeckUIRoot m_ui;
    private DeckEditState m_subState;
    private WeaponAndDeckModifications m_modifications;
    private bool m_resultReceived;
    public Action OnSelectedWeaponChanges;

    public UIPriority uiChildPriority => UIPriority.Front;

    protected override IEnumerator Load()
    {
      DeckMainState uiloader = this;
      uiloader.m_modifications = new WeaponAndDeckModifications();
      uiloader.m_modifications.Setup();
      uiloader.LoadAssetBundle(AssetBundlesUtility.GetUIAnimatedCharacterResourcesBundleName());
      uiloader.LoadAssetBundle("core/ui/characters/companions");
      string bundleName = AssetBundlesUtility.GetUICharacterResourcesBundleName();
      AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
      {
        Log.Error(string.Format("Error while loading bundle '{0}' error={1}", (object) bundleName, (object) bundleLoadRequest.error), 48, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\PlayerUI\\DeckMainState.cs");
      }
      else
      {
        LoadSceneStateContext.UILoader<DeckUIRoot> loader = new LoadSceneStateContext.UILoader<DeckUIRoot>((LoadSceneStateContext) uiloader, "PlayerLayer_DeckCanvas", "core/scenes/ui/deck", true);
        yield return (object) loader.Load();
        uiloader.m_ui = loader.ui;
        yield return (object) uiloader.m_ui.LoadAssets();
        uiloader.m_ui.gameObject.SetActive(true);
        uiloader.m_ui.Initialise(uiloader.m_modifications);
      }
    }

    public void RegisterToWeaponChange(Action selectionChange) => this.OnSelectedWeaponChanges = selectionChange;

    protected override IEnumerator Update()
    {
      DeckMainState deckMainState = this;
      yield return (object) deckMainState.m_ui.PlayEnterAnimation(deckMainState.GetWeaponsList());
      StateLayer stateLayer;
      if (StateManager.TryGetLayer("PlayerUI", out stateLayer))
      {
        deckMainState.m_subState = new DeckEditState();
        deckMainState.m_subState.OnCloseComplete += new Action(deckMainState.BackFromEditionState);
        stateLayer.GetChainEnd().SetChildState((StateContext) deckMainState.m_subState);
      }
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      this.m_ui.interactable = false;
      this.m_modifications.Unregister();
      float timeOutStart = Time.realtimeSinceStartup;
      if (this.m_modifications.hasModifications)
      {
        this.m_resultReceived = false;
        this.m_frame.SendSelectDecksAndWeapon(this.m_modifications.selectedWeapon, this.m_modifications.selectedDecksPerWeapon);
        while (!this.m_resultReceived && (double) Time.realtimeSinceStartup - (double) timeOutStart < 1.0)
          yield return (object) null;
      }
      yield return (object) this.m_ui.CloseUI();
      this.m_ui.UnloadAsset();
    }

    private void OnSelectDeckAndWeaponResult(SelectDeckAndWeaponResultEvent result) => this.m_resultReceived = true;

    protected override void Enable()
    {
      this.m_frame = new DeckMakerFrame()
      {
        onSelectDeckAndWeaponResult = new Action<SelectDeckAndWeaponResultEvent>(this.OnSelectDeckAndWeaponResult)
      };
      this.m_ui.OnEditRequest += new Action<DeckSlot>(this.OnEditDeckRequest);
      this.m_ui.OnGotoEditAnimComplete += new Action(this.OnGotoEditAnimComplete);
      this.m_ui.OnEquipWeaponRequest += new Action<int>(this.EquipWeapon);
      this.m_ui.OnSelectDeckForWeaponRequest += new Action<DeckSlot>(this.SelectDeckForWeaponDeckForWeapon);
      this.m_modifications.OnSelectedDecksUpdated += new Action(this.OnEquippedDecksUpdated);
      this.m_modifications.OnSelectedWeaponUpdated += new Action(this.OnSelectedWeaponUpdated);
    }

    private void OnEditDeckRequest(DeckSlot obj)
    {
      this.m_subState.SetDeckSlot(obj, this.m_modifications);
      this.m_ui.GotoEditAnim();
    }

    private void BackFromEditionState() => this.m_ui.BackFromEditAnim();

    private void OnGotoEditAnimComplete() => this.m_subState.OpenUIAnimation();

    private List<WeaponDefinition> GetWeaponsList()
    {
      List<WeaponDefinition> weaponsList = new List<WeaponDefinition>();
      foreach (KeyValuePair<int, WeaponDefinition> weaponDefinition1 in RuntimeData.weaponDefinitions)
      {
        WeaponDefinition weaponDefinition2 = weaponDefinition1.Value;
        if (weaponDefinition2.god == PlayerData.instance.god)
          weaponsList.Add(weaponDefinition2);
      }
      return weaponsList;
    }

    protected override void Disable()
    {
      this.m_ui.OnEditRequest -= new Action<DeckSlot>(this.OnEditDeckRequest);
      this.m_ui.OnGotoEditAnimComplete -= new Action(this.OnGotoEditAnimComplete);
      this.m_ui.OnEquipWeaponRequest -= new Action<int>(this.EquipWeapon);
      this.m_ui.OnSelectDeckForWeaponRequest -= new Action<DeckSlot>(this.SelectDeckForWeaponDeckForWeapon);
      this.m_modifications.OnSelectedDecksUpdated -= new Action(this.OnEquippedDecksUpdated);
      this.m_modifications.OnSelectedWeaponUpdated -= new Action(this.OnSelectedWeaponUpdated);
      DragNDropListener.instance.CancelAll();
      this.m_frame.Dispose();
    }

    private void EquipWeapon(int weaponId)
    {
      this.m_modifications.SetSelectedWeapon(weaponId);
      PlayerData.instance.RequestWeaponChange(weaponId);
    }

    private void SelectDeckForWeaponDeckForWeapon(DeckSlot slot)
    {
      DeckInfo deckInfo = slot.DeckInfo;
      if (deckInfo == null)
        return;
      DeckInfo info = deckInfo.TrimCopy();
      int? id = info.Id;
      if (!id.HasValue || !info.IsValid())
        return;
      this.m_modifications.SetSelectedDeckForWeapon(info.Weapon, id.Value);
    }

    private void OnEquippedDecksUpdated() => this.m_ui.OnEquippedDeckUpdate();

    private void OnSelectedWeaponUpdated()
    {
      this.m_ui.OnSelectedWeaponUpdate();
      Action selectedWeaponChanges = this.OnSelectedWeaponChanges;
      if (selectedWeaponChanges != null)
        selectedWeaponChanges();
      PlayerData.instance.RequestWeaponChange(this.m_ui.GetCurrentWeaponID());
    }
  }
}
