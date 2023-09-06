// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.FightState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Fight;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Protocols.FightProtocol;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.States
{
  public class FightState : LoadSceneStateContext, IStateUITransitionPriority
  {
    public FightFrame frame;
    private FightUIRework m_uiRework;
    private readonly FightInfo m_fightInfo;
    private FightDefinition m_fightDefinition;
    private readonly int m_ownFightId;
    private readonly int m_fightDefId;
    private readonly int m_fightMapId;
    private readonly int m_concurrentFightsCount;
    private readonly bool m_hardResumed;
    private Scene m_fightMapScene;

    public static FightState instance { get; private set; }

    public UIPriority uiTransitionPriority => UIPriority.Back;

    public FightUIRework uiRework => this.m_uiRework;

    public FightState(FightInfo fightInfo, bool hardResumed = false)
    {
      this.m_fightInfo = fightInfo;
      this.m_ownFightId = fightInfo.OwnFightId;
      this.m_fightDefId = fightInfo.FightDefId;
      this.m_fightMapId = fightInfo.FightMapId;
      this.m_concurrentFightsCount = fightInfo.ConcurrentFightsCount;
      this.m_hardResumed = hardResumed;
    }

    protected override IEnumerator Load()
    {
      FightState uiloader = this;
      RuntimeData.currentKeywordContext = uiloader.m_concurrentFightsCount != 1 ? KeywordContext.FightMulti : KeywordContext.FightSolo;
      FightState.instance = uiloader;
      int fightCount = uiloader.m_concurrentFightsCount;
      if (!RuntimeData.fightDefinitions.TryGetValue(uiloader.m_fightDefId, out uiloader.m_fightDefinition))
      {
        Log.Error(string.Format("Could not find {0} with id {1}.", (object) "FightDefinition", (object) uiloader.m_fightDefId), 78, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\FightState.cs");
      }
      else
      {
        yield return (object) RuntimeData.LoadTextCollectionAsync("Fight");
        yield return (object) uiloader.LoadSceneAndBundleRequest("FightMapWrapper", "core/scenes/maps/fight_maps");
        if (!SceneManager.GetSceneByName("FightMapWrapper").isLoaded)
        {
          Log.Error("Could not load scene named 'FightMapWrapper' from bundle 'core/scenes/maps/fight_maps'.", 93, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\FightState.cs");
        }
        else
        {
          yield return (object) uiloader.LoadFightMap();
          FightMap current = FightMap.current;
          if ((UnityEngine.Object) null == (UnityEngine.Object) current)
          {
            Log.Error("Failed to load fight map.", 104, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\FightState.cs");
          }
          else
          {
            FightLogicExecutor.Initialize(fightCount);
            FightMapDefinition definition = current.definition;
            FightStatus[] fightStatusArray = new FightStatus[fightCount];
            for (int index = 0; index < fightCount; ++index)
            {
              FightMapStatus fightMapStatus = definition.CreateFightMapStatus(index);
              FightStatus fightStatus = new FightStatus(index, fightMapStatus);
              if (fightStatus.fightId == uiloader.m_ownFightId)
                FightStatus.local = fightStatus;
              FightLogicExecutor.AddFightStatus(fightStatus);
              fightStatusArray[index] = fightStatus;
            }
            GameStatus.Initialize((FightType) uiloader.m_fightInfo.FightType, uiloader.m_fightDefinition, fightStatusArray);
            yield return (object) current.Initialize();
            VisualEffectFactory.Initialize();
            yield return (object) FightObjectFactory.Load();
            if (FightObjectFactory.isReady)
            {
              yield return (object) FightSpellEffectFactory.Load(fightCount);
              if (FightSpellEffectFactory.isReady)
              {
                yield return (object) FightUIFactory.Load();
                if (FightUIFactory.isReady)
                {
                  LoadSceneStateContext.UILoader<FightUIRework> loaderRework = new LoadSceneStateContext.UILoader<FightUIRework>((LoadSceneStateContext) uiloader, "FightUIRework", "core/scenes/ui/fight");
                  yield return (object) loaderRework.Load();
                  uiloader.m_uiRework = loaderRework.ui;
                  uiloader.m_uiRework.Init(GameStatus.fightType, uiloader.m_fightDefinition);
                  uiloader.frame = new FightFrame()
                  {
                    onOtherPlayerLeftFight = new Action<int>(uiloader.OnOtherPlayerLeftFight)
                  };
                  if (uiloader.m_hardResumed)
                  {
                    FightSnapshot snapshot = (FightSnapshot) null;
                    uiloader.frame.onFightSnapshot = (Action<FightSnapshot>) (fightSnapshot => snapshot = fightSnapshot);
                    uiloader.frame.SendFightSnapshotRequest();
                    while (snapshot == null)
                      yield return (object) null;
                    uiloader.frame.onFightSnapshot = (Action<FightSnapshot>) null;
                    yield return (object) uiloader.ApplyFightSnapshot(snapshot);
                  }
                  uiloader.frame.SendPlayerReady();
                  while (!FightLogicExecutor.fightInitialized)
                    yield return (object) null;
                  yield return (object) uiloader.uiRework.Load();
                }
              }
            }
          }
        }
      }
    }

    private IEnumerator LoadFightMap()
    {
      FightState fightState = this;
      string fightMapBundleName = AssetBundlesUtility.GetFightMapAssetBundleName(fightState.m_fightMapId);
      string fightMapSceneName = ScenesUtility.GetFightMapSceneName(fightState.m_fightMapId);
      yield return (object) fightState.LoadSceneAndBundleRequest(fightMapSceneName, fightMapBundleName);
      Scene sceneByName = SceneManager.GetSceneByName(fightMapSceneName);
      if (!sceneByName.IsValid())
      {
        Log.Error("Could not load scene named '" + fightMapSceneName + "' from bundle named '" + fightMapBundleName + "'.", 217, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\FightState.cs");
      }
      else
      {
        fightState.m_fightMapScene = sceneByName;
        FightMap inRootGameObjects = ScenesUtility.GetComponentInRootGameObjects<FightMap>(sceneByName);
        if ((UnityEngine.Object) null == (UnityEngine.Object) inRootGameObjects)
        {
          Log.Error("Could not find a FightMap in scene named '" + fightMapSceneName + "'.", 226, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\FightState.cs");
        }
        else
        {
          FightMap.current = inRootGameObjects;
          MapRenderSettings.ApplyToScene(FightMap.current.ambience);
        }
      }
    }

    private IEnumerator ApplyFightSnapshot(FightSnapshot snapshot)
    {
      yield break;
    }

    protected override void Enable()
    {
      this.m_uiRework.OnQuitRequest += new Action(this.OnQuitClick);
      this.m_uiRework.onTurnEndButtonClick = new Action(this.EndTurn);
      SceneManager.SetActiveScene(this.m_fightMapScene);
      RenderSettings.ambientLight = FightMap.current.ambience.lightSettings.ambientColor;
      FightLogicExecutor.Start();
      FightMap current = FightMap.current;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) current))
        return;
      current.Begin();
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      if (inputState.id != 2)
        return base.UseInput(inputState);
      if ((UnityEngine.Object) this.m_uiRework != (UnityEngine.Object) null)
        this.m_uiRework.SimulateClickTurnEndButton();
      return true;
    }

    protected override void Disable()
    {
      if (this.frame != null)
      {
        this.frame.Dispose();
        this.frame = (FightFrame) null;
      }
      this.m_uiRework.OnQuitRequest -= new Action(this.OnQuitClick);
      this.m_uiRework.onTurnEndButtonClick = (Action) null;
      FightMap current = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
        current.End();
      FightLogicExecutor.Stop();
    }

    protected override IEnumerator Unload()
    {
      while (FightLogicExecutor.isValid)
        yield return (object) null;
      FightMap current = FightMap.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current)
      {
        current.Release();
        FightMap.current = (FightMap) null;
      }
      if (FightSpellEffectFactory.isReady)
        yield return (object) FightSpellEffectFactory.Unload();
      if (FightObjectFactory.isReady)
        yield return (object) FightObjectFactory.Unload();
      if (FightUIFactory.isReady)
        yield return (object) FightUIFactory.Unload();
      VisualEffectFactory.Dispose();
      yield return (object) RuntimeData.UnloadTextCollectionAsync("Fight");
      yield return (object) base.Unload();
      DragNDropListener.instance.CancelSnapDrag();
      FightState.instance = (FightState) null;
    }

    public IEnumerator ShowFightEndFeedback(FightStatusEndReason endReason)
    {
      FightState fightState = this;
      FightEndFeedbackState feedbackState = new FightEndFeedbackState(endReason);
      fightState.SetChildState((StateContext) feedbackState);
      while (feedbackState.isActive)
        yield return (object) null;
    }

    public void GotoFightEndState(FightResult result, GameStatistics gameStatistics, int fightTime) => this.SetChildState((StateContext) new FightEndedState(result, gameStatistics, fightTime));

    public void LeaveAndGotoMainState()
    {
      RuntimeData.currentKeywordContext = KeywordContext.FightSolo;
      this.frame.SendLeave();
      StatesUtility.GotoMainMenu();
    }

    private void OnQuitClick()
    {
      if (FightStatus.local.isEnded)
        this.DisplayQuitPopup();
      else
        PopupInfoManager.Show(StateManager.GetDefaultLayer().GetChainEnd(), new PopupInfo()
        {
          message = (RawTextData) 99373,
          buttons = new ButtonData[2]
          {
            new ButtonData((TextData) 9912, new Action(this.OnAcceptResign), style: ButtonStyle.Negative),
            new ButtonData((TextData) 68421, new Action(this.OnRefuseQuit))
          },
          selectedButton = 2,
          style = PopupStyle.Normal,
          useBlur = true
        });
    }

    public void DisplayQuitPopup() => PopupInfoManager.Show(StateManager.GetDefaultLayer().GetChainEnd(), new PopupInfo()
    {
      message = (RawTextData) 76997,
      buttons = new ButtonData[2]
      {
        new ButtonData((TextData) 9912, new Action(this.LeaveAndGotoMainState), style: ButtonStyle.Negative),
        new ButtonData((TextData) 68421, new Action(this.OnRefuseQuit))
      },
      selectedButton = 2,
      style = PopupStyle.Normal,
      useBlur = true
    });

    private void OnRefuseQuit()
    {
      FightStatus local = FightStatus.local;
      if (local == null || local.isEnded)
        return;
      FightUIRework instance = FightUIRework.instance;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
        return;
      instance.SetResignButtonEnabled(true);
    }

    private void OnAcceptResign() => this.frame.SendResign();

    private void OnOtherPlayerLeftFight(int playerId)
    {
    }

    private void EndTurn() => this.frame.SendTurnEnd(FightStatus.local.turnIndex);
  }
}
