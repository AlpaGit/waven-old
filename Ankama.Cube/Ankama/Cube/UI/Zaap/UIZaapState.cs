// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Zaap.UIZaapState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.States;
using Ankama.Cube.Utility;
using System;
using System.Collections;

namespace Ankama.Cube.UI.Zaap
{
  public class UIZaapState : LoadSceneStateContext
  {
    private UIZaapPVPSelection m_ui;
    public Action onDisable;
    public Action onTransition;

    protected override IEnumerator Load()
    {
      UIZaapState uiloader = this;
      LoadSceneStateContext.UILoader<UIZaapPVPSelection> loader = new LoadSceneStateContext.UILoader<UIZaapPVPSelection>((LoadSceneStateContext) uiloader, "UIZaap_PVP", "core/scenes/maps/havre_maps", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.gameObject.SetActive(true);
      yield return (object) uiloader.m_ui.LoadAssets();
    }

    protected override void Enable()
    {
      this.m_ui.onCloseClick = new Action(this.OnCloseClick);
      this.m_ui.onPlayRequested = new Action<int, int?>(this.OnPlayRequested);
    }

    protected override void Disable()
    {
      this.m_ui.UnloadAsset();
      this.DisableUIEvents();
    }

    protected override IEnumerator Unload()
    {
      Action onDisable = this.onDisable;
      if (onDisable != null)
        onDisable();
      return base.Unload();
    }

    private void DisableUIEvents()
    {
      this.m_ui.onPlayRequested = (Action<int, int?>) null;
      this.m_ui.onCloseClick = (Action) null;
    }

    protected override IEnumerator Update()
    {
      yield return (object) this.m_ui.PlayEnterAnimation();
    }

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      this.DisableUIEvents();
      Action onTransition = this.onTransition;
      if (onTransition != null)
        onTransition();
      if (transitionInfo != null && transitionInfo.stateContext != null && transitionInfo.stateContext is UIPVPLoadingState)
        yield return (object) this.m_ui.PlayTransitionToVersusAnimation();
      else
        yield return (object) this.m_ui.PlayCloseAnimation();
      yield return (object) base.Transition(transitionInfo);
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    private void OnCloseClick() => this.parent.ClearChildState();

    private void OnPlayRequested(int fightDefinitionId, int? forcedLevel)
    {
      UIPVPLoadingState childState = new UIPVPLoadingState();
      childState.SetGameMode(fightDefinitionId);
      this.parent.SetChildState((StateContext) childState);
    }

    private void OnDebugMatchmakingRequested() => StatesUtility.GotoMatchMakingState();

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated)
        this.parent.ClearChildState();
      return true;
    }
  }
}
