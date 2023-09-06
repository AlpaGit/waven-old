// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.GodSelectionState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Network;
using Ankama.Cube.UI.GodSelection;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class GodSelectionState : LoadSceneStateContext
  {
    private GodSelectionFrame m_frame;
    private GodSelectionRoot m_ui;
    public Action onDisable;
    public Action onTransition;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      GodSelectionState uiloader = this;
      LoadSceneStateContext.UILoader<GodSelectionRoot> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.Initialise();
        uiloader.m_ui.gameObject.SetActive(true);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<GodSelectionRoot>((LoadSceneStateContext) uiloader, "GodSelectionUI", "core/scenes/ui/player", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      yield return (object) this.m_ui.PlayEnterAnimation();
    }

    protected override void Enable()
    {
      this.m_frame = new GodSelectionFrame();
      this.m_ui.onGodSelected = new Action<God>(this.OnGodChangeRequest);
      this.m_ui.onCloseClick = new Action(this.CloseState);
    }

    protected override void Disable()
    {
      this.m_ui.onGodSelected = (Action<God>) null;
      this.m_ui.onCloseClick = (Action) null;
      this.m_frame.Dispose();
    }

    protected override IEnumerator Unload()
    {
      Action onDisable = this.onDisable;
      if (onDisable != null)
        onDisable();
      return base.Unload();
    }

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      Action onTransition = this.onTransition;
      if (onTransition != null)
        onTransition();
      yield return (object) this.m_ui.CloseUI();
      yield return (object) base.Transition(transitionInfo);
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    private void OnGodChangeRequest(God value) => this.m_frame.ChangeGodRequest(value);

    private void CloseState() => this.parent.ClearChildState();

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated && (UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        this.m_ui.SimulateCloseClick();
      return true;
    }
  }
}
