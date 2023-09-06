// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.PlayerNavRibbonState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using Ankama.Cube.UI.PlayerLayer;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class PlayerNavRibbonState : LoadSceneStateContext, IStateUIChildPriority
  {
    private PlayerLayerNavRoot m_ui;

    public UIPriority uiChildPriority => UIPriority.Back;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      PlayerNavRibbonState uiloader = this;
      LoadSceneStateContext.UILoader<PlayerLayerNavRoot> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        uiloader.m_ui.Initialise();
        uiloader.m_ui.OnCloseAction = new Action(uiloader.Quit);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<PlayerLayerNavRoot>((LoadSceneStateContext) uiloader, "PlayerLayer_NavRibbonCanvas", "core/scenes/ui/player", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      yield return (object) this.m_ui.PlayEnterAnimation();
      base.Update();
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      yield return (object) this.m_ui.OnClose();
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated)
        PlayerIconRoot.instance.ReducePanel();
      return true;
    }

    private void Quit() => StateManager.SimulateInput(1, InputState.State.Activated);
  }
}
