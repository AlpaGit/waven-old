// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PlayerLayer.PlayerLayerNavRoot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.States;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Components.Tooltip;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI.PlayerLayer
{
  public class PlayerLayerNavRoot : AbstractUI
  {
    public Action OnCloseAction;
    [SerializeField]
    private CanvasGroup m_BG;
    [SerializeField]
    private List<PlayerLayerNavButton> m_navButton;
    [Header("Button")]
    [SerializeField]
    private PlayerLayerNavButton m_profilButton;
    [SerializeField]
    private PlayerLayerNavButton m_collectionButton;
    [SerializeField]
    private PlayerLayerNavButton m_deckButton;
    [SerializeField]
    private AnimatedGraphicButton m_cancelButton;
    [SerializeField]
    private GenericTooltipWindow m_genericTooltipWindow;
    private PlayerLayerNavButton m_curentButton;

    public void Initialise()
    {
      this.m_genericTooltipWindow.alpha = 0.0f;
      this.m_BG.alpha = 0.0f;
      this.m_BG.blocksRaycasts = true;
      this.Initialise(new Action(this.OpenProfileUI), new Action(this.OpenDeckUI), (Action) null);
      this.m_cancelButton.onClick.AddListener(new UnityAction(this.OnPreviousPress));
    }

    private void OnPreviousPress()
    {
      Action onCloseAction = this.OnCloseAction;
      if (onCloseAction == null)
        return;
      onCloseAction();
    }

    public void Initialise(Action ProfileAction, Action DeckAction, Action CollectionAction)
    {
      this.m_navButton = new List<PlayerLayerNavButton>();
      this.m_profilButton.SetMethode(ProfileAction);
      this.m_deckButton.SetMethode(DeckAction);
      this.m_collectionButton.SetMethode(CollectionAction);
      this.m_navButton.Add(this.m_profilButton);
      this.m_navButton.Add(this.m_deckButton);
      this.m_navButton.Add(this.m_collectionButton);
      foreach (PlayerLayerNavButton playerLayerNavButton in this.m_navButton)
      {
        playerLayerNavButton.Initialise(this);
        playerLayerNavButton.OnDeselect();
      }
    }

    private void OpenDeckUI()
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
        return;
      DeckMainState childState = new DeckMainState();
      stateLayer.GetChainRoot().GetChildState().SetChildState((StateContext) childState);
    }

    private void OpenProfileUI()
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("PlayerUI", out stateLayer))
        return;
      ProfileState childState = new ProfileState();
      stateLayer.GetChainRoot().GetChildState().SetChildState((StateContext) childState);
    }

    private void ClicOnButton(PlayerLayerNavButton button, Action action)
    {
      if ((UnityEngine.Object) this.m_curentButton == (UnityEngine.Object) button)
        return;
      this.m_curentButton = button;
      foreach (PlayerLayerNavButton playerLayerNavButton in this.m_navButton)
        playerLayerNavButton.OnDeselect();
      button.OnValidate();
      action();
    }

    public IEnumerator OnClose()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      PlayerLayerNavRoot playerLayerNavRoot = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      playerLayerNavRoot.m_BG.blocksRaycasts = true;
      playerLayerNavRoot.m_curentButton = (PlayerLayerNavButton) null;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) playerLayerNavRoot.PlayAnimation(playerLayerNavRoot.m_animationDirector.GetAnimation("Close"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public IEnumerator PlayEnterAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      PlayerLayerNavRoot playerLayerNavRoot = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        playerLayerNavRoot.OpenOnDeck();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      playerLayerNavRoot.m_BG.blocksRaycasts = true;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) playerLayerNavRoot.PlayAnimation(playerLayerNavRoot.m_animationDirector.GetAnimation("Open"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private void OpenOnProfile() => this.OnClic(this.m_profilButton);

    private void OpenOnDeck() => this.OnClic(this.m_deckButton);

    public void OnClic(PlayerLayerNavButton button) => this.ClicOnButton(button, button.GetMethod());
  }
}
