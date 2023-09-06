// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.States.GodSelectionState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.Network;
using Ankama.Cube.States;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using System;
using System.Collections;

namespace Ankama.Cube.Demo.States
{
  public class GodSelectionState : BaseFightSelectionState, IStateUIChildPriority
  {
    public Action<God> onGodSelected;
    private GodSelectionUIDemo m_ui;
    private GodSelectionFrame m_frame;

    public UIPriority uiChildPriority => UIPriority.Back;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      GodSelectionState uiloader = this;
      LoadSceneStateContext.UILoader<GodSelectionUIDemo> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.Init();
        uiloader.m_ui.gameObject.SetActive(false);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<GodSelectionUIDemo>((LoadSceneStateContext) uiloader, "GodSelectionUIDemo", "demo/scenes/ui/godselection", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      GodSelectionState godSelectionState = this;
      godSelectionState.m_ui.gameObject.SetActive(true);
      yield return (object) godSelectionState.m_ui.OpenFrom(godSelectionState.fromSide);
      godSelectionState.m_ui.onSelect = new Action<God>(godSelectionState.OnSelectClick);
      Action uiOpeningFinished = godSelectionState.onUIOpeningFinished;
      if (uiOpeningFinished != null)
        uiOpeningFinished();
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      GodSelectionState godSelectionState = this;
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
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) godSelectionState.m_ui.CloseTo(godSelectionState.toSide);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable()
    {
      this.m_frame = new GodSelectionFrame();
      this.m_frame.OnChangeGod += new Action(this.OnChangeGod);
    }

    protected override void Disable()
    {
      this.m_ui.onSelect = (Action<God>) null;
      this.m_frame.Dispose();
      this.m_frame = (GodSelectionFrame) null;
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      switch (inputState.id)
      {
        case 2:
        case 3:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.SimulateSelectClick();
          return true;
        case 6:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.SimulateRightArrowClick();
          return true;
        case 7:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.SimulateLeftArrowClick();
          return true;
        default:
          return base.UseInput(inputState);
      }
    }

    private void OnSelectClick(God god) => this.m_frame.ChangeGodRequest(god);

    private void OnChangeGod()
    {
      Action<God> onGodSelected = this.onGodSelected;
      if (onGodSelected == null)
        return;
      onGodSelected(PlayerData.instance.god);
    }
  }
}
