// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.States.MainStateDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Data;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.Network;
using Ankama.Cube.States;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Demo.States
{
  public class MainStateDemo : LoadSceneStateContext, IStateUITransitionPriority
  {
    private const float MinLoadingTime = 3f;
    private float m_startLoadingTime;
    private MainUIDemo m_ui;
    private God m_god;

    public UIPriority uiTransitionPriority => UIPriority.Back;

    public MainStateDemo()
    {
      GodSelectionState godSelectionState = new GodSelectionState();
      godSelectionState.fromSide = SlidingSide.Right;
      GodSelectionState childState = godSelectionState;
      childState.onGodSelected += new Action<God>(this.OnChildStateOnGodSelected);
      this.SetChildState((StateContext) childState);
    }

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      MainStateDemo uiloader = this;
      LoadSceneStateContext.UILoader<MainUIDemo> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.SetStateIndex(0, false);
        uiloader.m_ui.gameObject.SetActive(false);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<MainUIDemo>((LoadSceneStateContext) uiloader, "MainUIDemo", "demo/scenes/ui/main", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable()
    {
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.Backspace, 8));
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.RightArrow, 6));
      StateManager.RegisterInputDefinition((InputDefinition) new InputKeyCodeDefinition(KeyCode.LeftArrow, 7));
      this.m_ui.onReturn = new Action(this.OnReturnClick);
    }

    protected override IEnumerator Update()
    {
      this.m_ui.gameObject.SetActive(true);
      this.m_ui.Open();
      yield break;
    }

    protected override void Disable()
    {
      StateManager.UnregisterInputDefinition(8);
      StateManager.UnregisterInputDefinition(6);
      StateManager.UnregisterInputDefinition(7);
      this.m_ui.gameObject.SetActive(false);
      this.m_ui.onReturn = (Action) null;
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      if (inputState.id != 8)
        return base.UseInput(inputState);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        this.m_ui.SimulateReturnClick();
      return true;
    }

    public override bool AllowsTransition(StateContext nextState) => nextState is FightState || nextState is LoginStateDemo;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      if (transitionInfo != null && transitionInfo.stateContext != null)
      {
        if (transitionInfo.stateContext is LoginStateDemo)
          yield return (object) this.m_ui.CloseCoroutine();
        else if (transitionInfo.stateContext is FightState)
        {
          this.m_startLoadingTime = Time.time;
          while (transitionInfo.stateContext.loadState != StateLoadState.Loaded)
          {
            yield return (object) null;
            if (transitionInfo.stateContext == null)
              yield break;
          }
          while ((double) Time.time - (double) this.m_startLoadingTime < 3.0)
            yield return (object) null;
          yield return (object) this.m_ui.GotoFightAnim();
        }
      }
    }

    private void GotoSubState(MainStateDemo.SubState state, SlidingSide fromSide, bool tween = true)
    {
      InactivityHandler.UpdateActivity();
      if (state != MainStateDemo.SubState.GodSelection)
        throw new ArgumentOutOfRangeException(nameof (state), (object) state, (string) null);
      GodSelectionState godSelectionState = new GodSelectionState();
      godSelectionState.onGodSelected += new Action<God>(this.OnGodSelected);
      this.m_ui.SetStateIndex(0, tween);
      BaseFightSelectionState childState = (BaseFightSelectionState) godSelectionState;
      this.m_ui.returnButton.interactable = false;
      childState.fromSide = fromSide;
      childState.onUIOpeningFinished = new Action(this.OnChildUIOpeningFinished);
      this.SetChildState((StateContext) childState);
    }

    private void OnChildStateOnGodSelected(God god)
    {
      this.m_god = god;
      this.GotoSubState(MainStateDemo.SubState.CharacterSelection, SlidingSide.Right);
    }

    private void OnGodSelected(God god)
    {
      this.m_god = god;
      this.GotoSubState(MainStateDemo.SubState.CharacterSelection, SlidingSide.Right);
    }

    private void OnDeckSelected(int deckId)
    {
      this.GotoSubState(MainStateDemo.SubState.GameSelection, SlidingSide.Right);
      PlayerData instance = PlayerData.instance;
      SquadDefinition squadDefinition = RuntimeData.squadDefinitions[deckId];
      WeaponDefinition weaponDefinition = RuntimeData.weaponDefinitions[squadDefinition.weapon.value];
      this.m_ui.playerAvatar.nickname = instance.nickName;
      this.m_ui.playerAvatar.weaponDefinition = weaponDefinition;
      this.m_ui.ShowPlayerAvatarAnim(true);
    }

    private void OnGameSelected(int gameIdx)
    {
      MainStateDemo.SubState state = MainStateDemo.SubState.Matchmaking1V1;
      switch (gameIdx)
      {
        case 0:
          state = MainStateDemo.SubState.Matchmaking3V3;
          break;
        case 1:
          state = MainStateDemo.SubState.Matchmaking1V1;
          break;
        case 2:
          state = MainStateDemo.SubState.Matchmaking4VBoss;
          break;
      }
      this.GotoSubState(state, SlidingSide.Right);
      this.m_ui.ShowStepMenuAnim(false);
    }

    private void OnChildUIOpeningFinished() => this.m_ui.returnButton.interactable = true;

    private void OnReturnClick()
    {
      this.m_ui.returnButton.interactable = false;
      this.GotoPreviousState(this.GetChildState());
    }

    public void GotoPreviousState(StateContext currentChildState)
    {
      if (currentChildState is BaseFightSelectionState fightSelectionState)
        fightSelectionState.toSide = SlidingSide.Right;
      if (currentChildState is GodSelectionState)
      {
        ConnectionHandler.instance.Disconnect();
        this.parent.SetChildState((StateContext) new LoginStateDemo());
      }
      else
        Log.Error("We don't know where we are, this Should not happend", 330, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\States\\MainStateDemo.cs");
    }

    public enum SubState
    {
      GodSelection,
      CharacterSelection,
      GameSelection,
      Matchmaking1V1,
      Matchmaking3V3,
      Matchmaking4VBoss,
    }
  }
}
