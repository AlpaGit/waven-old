// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.EndGameResultState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.UI.Fight;
using JetBrains.Annotations;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class EndGameResultState : AbstractEndGameState
  {
    private FightEndedUI m_ui;
    private readonly FightResult m_end;

    public EndGameResultState(FightResult end) => this.m_end = end;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      EndGameResultState uiHoster = this;
      LoadSceneStateContext.UILoader<FightEndedUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiHoster.m_ui = loader.ui;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<FightEndedUI>((LoadSceneStateContext) FightState.instance, (StateContext) uiHoster, uiHoster.GetSceneName(uiHoster.m_end), "core/scenes/ui/fight", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      EndGameResultState endGameResultState = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        endGameResultState.m_ui.onContinueClick = new Action(endGameResultState.OnContinueClick);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      endGameResultState.m_ui.gameObject.SetActive(true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) endGameResultState.m_ui.OpenCoroutine();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Transition([CanBeNull] StateTransitionInfo transitionInfo)
    {
      this.m_ui.onContinueClick = (Action) null;
      yield return (object) this.m_ui.CloseCoroutine();
      this.m_ui.gameObject.SetActive(false);
    }

    protected override void Disable() => this.m_ui.onContinueClick = (Action) null;

    private void OnContinueClick()
    {
      this.m_ui.onContinueClick = (Action) null;
      Action onContinue = this.onContinue;
      if (onContinue == null)
        return;
      onContinue();
    }

    private string GetSceneName(FightResult end)
    {
      switch (end)
      {
        case FightResult.Draw:
          return "FightEndedDrawUI";
        case FightResult.Victory:
          return "FightEndedWinUI";
        case FightResult.Defeat:
          return "FightEndedLoseUI";
        default:
          throw new ArgumentOutOfRangeException(nameof (end), (object) end, (string) null);
      }
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      if (inputState.id != 2)
        return base.UseInput(inputState);
      this.m_ui.DoContinueClick();
      return true;
    }
  }
}
