// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.TransitionState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.States
{
  public class TransitionState : LoadSceneStateContext
  {
    private TransitionUI m_ui;
    private readonly StateContext m_previousState;
    private readonly StateContext m_nextState;

    public TransitionState([NotNull] StateContext nextState, [CanBeNull] StateContext previousState)
    {
      this.m_previousState = previousState;
      this.m_nextState = nextState;
    }

    protected override IEnumerator Load()
    {
      TransitionState uiloader = this;
      LoadSceneStateContext.UILoader<TransitionUI> loader = new LoadSceneStateContext.UILoader<TransitionUI>((LoadSceneStateContext) uiloader, "TransitionUI", "core/scenes/ui/transition", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.gameObject.SetActive(true);
      yield return (object) uiloader.m_ui.OpenCoroutine();
    }

    protected override IEnumerator Update()
    {
      TransitionState transitionState = this;
      if (transitionState.m_previousState != null)
      {
        while (transitionState.m_previousState.loadState != StateLoadState.Unloaded)
          yield return (object) null;
      }
      transitionState.parent.SetChildState(transitionState.m_nextState);
    }

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      while (transitionInfo.stateContext != null && transitionInfo.stateContext.loadState != StateLoadState.Loaded)
        yield return (object) null;
      if ((Object) null != (Object) this.m_ui)
        yield return (object) this.m_ui.CloseCoroutine();
    }

    public override bool AllowsTransition(StateContext nextState) => true;
  }
}
