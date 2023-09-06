// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.States.EndGameStatsStateDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.States;
using JetBrains.Annotations;
using System;
using System.Collections;

namespace Ankama.Cube.Demo.States
{
  public class EndGameStatsStateDemo : AbstractEndGameState
  {
    private EndGameStatsUIDemo m_ui;
    private readonly GameStatistics m_gameStatistics;
    private FightResult m_endResult;
    private FightInfo m_fightInfo;
    private int m_fightTime;

    public EndGameStatsStateDemo(
      FightResult endResult,
      GameStatistics gameStatistics,
      int fightTime)
    {
      this.m_gameStatistics = gameStatistics;
      this.m_endResult = endResult;
      this.m_fightTime = fightTime;
    }

    protected override IEnumerator Load()
    {
      EndGameStatsStateDemo uiHoster = this;
      LoadSceneStateContext.UILoader<EndGameStatsUIDemo> loader = new LoadSceneStateContext.UILoader<EndGameStatsUIDemo>((LoadSceneStateContext) FightState.instance, (StateContext) uiHoster, "EndGameStats", "demo/scenes/ui/fight", true);
      yield return (object) loader.Load();
      uiHoster.m_ui = loader.ui;
      yield return (object) uiHoster.m_ui.Init(uiHoster.m_endResult, uiHoster.m_gameStatistics, uiHoster.m_fightTime);
    }

    protected override IEnumerator Update()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      EndGameStatsStateDemo gameStatsStateDemo = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        gameStatsStateDemo.m_ui.onContinueClick = new Action(gameStatsStateDemo.OnContinueClick);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      gameStatsStateDemo.m_ui.gameObject.SetActive(true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) gameStatsStateDemo.m_ui.OpenCoroutine();
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

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      if (inputState.id != 2)
        return base.UseInput(inputState);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        this.m_ui.DoContinueClick();
      return true;
    }
  }
}
