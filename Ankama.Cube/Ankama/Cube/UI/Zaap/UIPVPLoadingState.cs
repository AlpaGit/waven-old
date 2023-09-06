// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Zaap.UIPVPLoadingState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.States;
using Ankama.Cube.TEMPFastEnterMatch.MatchMaking;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.UI.Zaap
{
  public class UIPVPLoadingState : LoadSceneStateContext
  {
    private UIZaapPVPLoading m_ui;
    private MatchMakingFrame m_frame;
    private int m_fightDefinitionId;

    public void SetGameMode(int fightDefininitionId) => this.m_fightDefinitionId = fightDefininitionId;

    protected override IEnumerator Load()
    {
      UIPVPLoadingState uiloader = this;
      AssetManager.LoadAssetBundle(AssetBundlesUtility.GetUIAnimatedCharacterResourcesBundleName());
      LoadSceneStateContext.UILoader<UIZaapPVPLoading> loader = new LoadSceneStateContext.UILoader<UIZaapPVPLoading>((LoadSceneStateContext) uiloader, "MatchmakingUI_1v1", "core/scenes/maps/havre_maps", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.onForceAiRequested = new Action(uiloader.OnForceAiRequested);
      uiloader.m_ui.onCancelRequested = new Action(uiloader.OnCancelRequested);
      uiloader.m_ui.onEnterAnimationFinished = new Action<int, int?>(uiloader.OnPlayRequested);
      yield return (object) uiloader.m_ui.LoadAssets();
      uiloader.m_frame = new MatchMakingFrame()
      {
        onGameCreated = new Action<FightInfo>(uiloader.OnGameCreated),
        onGameCanceled = new Action(uiloader.OnGameCancel),
        onGameError = new Action(uiloader.OnGameError)
      };
    }

    protected override void Disable()
    {
      this.m_ui.onCancelRequested = (Action) null;
      this.m_ui.onForceAiRequested = (Action) null;
      this.m_ui.gameObject.SetActive(false);
      this.m_frame.Dispose();
    }

    protected override IEnumerator Update()
    {
      this.m_ui.gameObject.SetActive(true);
      this.m_ui.Init(this.m_fightDefinitionId);
      yield return (object) this.m_ui.LoadUI();
      yield return (object) null;
    }

    protected override IEnumerator Unload()
    {
      this.m_ui.UnloadAsset();
      yield return (object) base.Unload();
    }

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      yield return (object) this.m_ui.CloseUI();
      yield return (object) base.Transition(transitionInfo);
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    private void OnCancelRequested()
    {
      if (this.m_frame == null)
        return;
      this.m_frame.SendCancelGame();
    }

    private void OnPlayRequested(int fightDefinitionId, int? forcedLevel) => this.m_frame.SendCreateGame(fightDefinitionId, PlayerData.instance.currentDeckId, forcedLevel);

    private void OnGameError() => PopupInfoManager.Show((StateContext) this, new PopupInfo()
    {
      message = (RawTextData) 54306,
      buttons = new ButtonData[1]
      {
        new ButtonData(new TextData(27169, Array.Empty<string>()))
      },
      selectedButton = 1,
      style = PopupStyle.Error
    });

    private void OnForceAiRequested() => this.m_frame.SendToggleMatchmaking();

    private void OnGameCancel() => this.GetLayer().GetChainRoot().ClearChildState();

    private void OnGameCreated(FightInfo fightInfo)
    {
      if (!((UnityEngine.Object) this.m_ui != (UnityEngine.Object) null))
        return;
      this.m_ui.StartCoroutine(this.StartGame(fightInfo));
    }

    private IEnumerator StartGame(FightInfo fightInfo)
    {
      UIPVPLoadingState uipvpLoadingState = this;
      StatesUtility.ClearOptionLayer();
      uipvpLoadingState.m_ui.OnGameStarted();
      yield return (object) uipvpLoadingState.ApplyFightInfos(fightInfo);
      yield return (object) uipvpLoadingState.m_ui.GotoVersusAnim();
      yield return (object) new WaitForTime(2f);
      StateLayer defaultLayer = StateManager.GetDefaultLayer();
      StateContext currentState = defaultLayer.GetChildState();
      if (currentState != null)
      {
        defaultLayer.ClearChildState();
        while (currentState.loadState == StateLoadState.Unloading)
          yield return (object) null;
      }
      FightState fightState = new FightState(fightInfo);
      defaultLayer.SetChildState((StateContext) fightState);
      VersusState childState = new VersusState(uipvpLoadingState.m_ui, (StateContext) fightState);
      uipvpLoadingState.SetChildState((StateContext) childState);
    }

    private IEnumerator ApplyFightInfos(FightInfo fightInfo)
    {
      yield return (object) this.m_ui.SetOpponent(this.GetOpponent((IList<FightInfo.Types.Team>) fightInfo.Teams));
    }

    private FightInfo.Types.Player GetOpponent(IList<FightInfo.Types.Team> teams)
    {
      for (int index1 = 0; index1 < teams.Count; ++index1)
      {
        FightInfo.Types.Team team = teams[index1];
        int count = team.Players.Count;
        for (int index2 = 0; index2 < count; ++index2)
        {
          FightInfo.Types.Player player = team.Players[index2];
          if (player.Name != PlayerData.instance.nickName)
            return player;
        }
      }
      return (FightInfo.Types.Player) null;
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated)
        this.m_ui.SimulateCancelClic();
      return true;
    }
  }
}
