// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.PlayerUIMainState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.PlayerLayer;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class PlayerUIMainState : LoadSceneStateContext
  {
    private PlayerIconRoot m_ui;

    protected override IEnumerator Load()
    {
      PlayerUIMainState playerUiMainState = this;
      string bundleName = AssetBundlesUtility.GetUICharacterResourcesBundleName();
      AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
      {
        Log.Error(string.Format("Error while loading bundle '{0}' error={1}", (object) bundleName, (object) bundleLoadRequest.error), 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\PlayerUI\\PlayerUIMainState.cs");
      }
      else
      {
        LoadSceneStateContext.UILoader<PlayerIconRoot> loader = new LoadSceneStateContext.UILoader<PlayerIconRoot>((LoadSceneStateContext) playerUiMainState, "PlayerLayerUI", "core/scenes/ui/player", true);
        yield return (object) loader.Load();
        playerUiMainState.m_ui = loader.ui;
        yield return (object) playerUiMainState.m_ui.LoadAssets();
        playerUiMainState.m_ui.gameObject.SetActive(true);
        playerUiMainState.m_ui.Initialise(playerUiMainState);
        playerUiMainState.m_ui.LoadVisual();
      }
    }

    protected override void Enable()
    {
      PlayerData.instance.OnSelectedDeckUpdated += new Action(this.RefreshVisual);
      PlayerData.instance.OnRequestVisualWeaponUpdated += new Action<int>(this.OnCurrentDeckUpdate);
    }

    protected override void Disable()
    {
      this.m_ui.UnloadAsset();
      base.Disable();
      PlayerData.instance.OnSelectedDeckUpdated -= new Action(this.RefreshVisual);
      PlayerData.instance.OnRequestVisualWeaponUpdated -= new Action<int>(this.OnCurrentDeckUpdate);
    }

    private void OnCurrentDeckUpdate(int weaponID) => this.m_ui.LoadVisual(weaponID);

    private void RefreshVisual() => this.m_ui.LoadVisual();

    private void OnSelectedGodResult(ChangeGodResultEvent evt) => this.RefreshVisual();

    private void OnWeaponChangeResult(SelectDeckAndWeaponResultEvent result) => this.RefreshVisual();

    protected override IEnumerator Update()
    {
      yield return (object) this.m_ui.PlayEnterAnimation();
    }

    public bool TryExpandPanel()
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("PlayerUI", out stateLayer) || stateLayer.GetChainEnd() != this)
        return false;
      StateManager.SetActiveInputLayer(stateLayer);
      PlayerNavRibbonState childState = new PlayerNavRibbonState();
      stateLayer.GetChainRoot().SetChildState((StateContext) childState);
      return true;
    }
  }
}
