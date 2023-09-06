// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.FightEndedState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Demo.States;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.States
{
  public class FightEndedState : LoadSceneStateContext
  {
    private List<AbstractEndGameState> m_subStates = new List<AbstractEndGameState>();
    private int m_currentSubStateIndex = -1;

    public FightEndedState(FightResult end, GameStatistics gameStatistics, int fightTime)
    {
      if (gameStatistics == null)
        this.m_subStates.Add((AbstractEndGameState) new EndGameResultState(end));
      else
        this.m_subStates.Add((AbstractEndGameState) new EndGameStatsStateDemo(end, gameStatistics, fightTime));
    }

    protected override void Enable() => this.GotoSubState(0);

    private void GotoSubState(int index)
    {
      if (this.m_subStates.Count == 0 || index >= this.m_subStates.Count)
      {
        this.LeaveFight();
      }
      else
      {
        if (this.m_currentSubStateIndex >= 0 && this.m_currentSubStateIndex < this.m_subStates.Count)
        {
          AbstractEndGameState subState = this.m_subStates[this.m_currentSubStateIndex];
          subState.onContinue = (Action) null;
          subState.allowTransition = true;
        }
        this.m_currentSubStateIndex = index;
        AbstractEndGameState subState1 = this.m_subStates[index];
        subState1.onContinue = new Action(this.OnSubStateContinue);
        this.SetChildState((StateContext) subState1);
      }
    }

    private void OnSubStateContinue() => this.GotoSubState(this.m_currentSubStateIndex + 1);

    private void LeaveFight() => FightState.instance.LeaveAndGotoMainState();
  }
}
