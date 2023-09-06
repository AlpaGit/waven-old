// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.PlayerIconRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.States;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class PlayerIconRoot : AbstractUI
  {
    [Header("Visual")]
    [SerializeField]
    private ImageLoader m_VisualLoader;
    [SerializeField]
    private AnimatedGraphicButton m_button;
    private bool m_isOpen;
    private PlayerUIMainState m_state;
    private WeaponDefinition m_currentWeaponDefinition;

    public static PlayerIconRoot instance { get; private set; }

    public void Initialise(PlayerUIMainState mainState)
    {
      this.m_state = mainState;
      PlayerIconRoot.instance = this;
      this.canvasGroup.alpha = 0.0f;
      this.m_button.onClick.AddListener((UnityAction) (() => this.OnClicVisual()));
    }

    public void OnClicVisual()
    {
      if (!this.m_isOpen)
        this.ExpendPanel();
      else
        this.ReducePanel();
    }

    private void ExpendPanel()
    {
      if (!this.m_state.TryExpandPanel())
        return;
      this.m_isOpen = true;
    }

    public void ReducePanel()
    {
      this.m_isOpen = false;
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
        return;
      stateLayer.GetChainRoot().ClearChildState();
    }

    public IEnumerator PlayEnterAnimation()
    {
      PlayerIconRoot playerIconRoot = this;
      yield return (object) new WaitForTime(0.5f);
      playerIconRoot.canvasGroup.DOFade(1f, 1.5f);
      yield return (object) new WaitForTime(0.5f);
    }

    public void LoadVisual(int weaponID)
    {
      WeaponDefinition weaponDefinition = RuntimeData.weaponDefinitions[weaponID];
      if (!((Object) weaponDefinition != (Object) null))
        return;
      this.StartCoroutine(this.LoadPlayerVisual(weaponDefinition));
    }

    public void LoadVisual()
    {
      DeckInfo deckInfo;
      if (!PlayerData.instance.TryGetDeckById(PlayerData.instance.currentDeckId, out deckInfo))
        return;
      foreach (KeyValuePair<int, WeaponDefinition> weaponDefinition in RuntimeData.weaponDefinitions)
      {
        if (weaponDefinition.Key == deckInfo.Weapon)
          this.StartCoroutine(this.LoadPlayerVisual(weaponDefinition.Value));
      }
    }

    private IEnumerator LoadPlayerVisual(WeaponDefinition definition)
    {
      if (!((Object) this.m_currentWeaponDefinition == (Object) definition))
      {
        this.m_currentWeaponDefinition = definition;
        this.m_VisualLoader.Setup(definition.GetIllustrationReference(), AssetBundlesUtility.GetUICharacterResourcesBundleName());
        while (this.m_VisualLoader.loadState == UIResourceLoadState.Loading)
          yield return (object) null;
      }
    }
  }
}
