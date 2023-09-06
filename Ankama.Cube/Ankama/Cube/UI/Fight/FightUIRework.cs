// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.FightUIRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.States;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Debug;
using Ankama.Cube.UI.Fight.Info;
using Ankama.Cube.UI.Fight.TeamCounter;
using Ankama.Cube.UI.Fight.Windows;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class FightUIRework : AbstractUI, IUIResourceConsumer
  {
    [Header("Components")]
    [SerializeField]
    private FightUIFactory m_factory;
    [SerializeField]
    private LocalPlayerUIRework m_localPlayerUIRework;
    [SerializeField]
    private Transform m_allyPlayersUIParent;
    [SerializeField]
    private Transform m_opponentPlayersUIParent;
    [SerializeField]
    private EndOfTurnButtonRework m_endOfTurnButton;
    [SerializeField]
    private TurnFeedbackUI m_turnFeedbackUI;
    [SerializeField]
    private FightTooltip m_fightTooltip;
    [SerializeField]
    private PlaySpellCompanionUI m_playSpellCompanionUI;
    [Header("Team HUD")]
    [SerializeField]
    private Transform m_teamPointCounterParent;
    [SerializeField]
    private Transform m_messageRibbonRootParent;
    [Header("PopupMenu")]
    [SerializeField]
    private PopupMenu m_popupMenu;
    [SerializeField]
    private Button m_popupMenuButton;
    [SerializeField]
    private Button m_optionsButton;
    [SerializeField]
    private Button m_quitButton;
    [SerializeField]
    private Button m_bugReportButton;
    private static FightUIRework s_instance;
    private static bool s_tooltipEnabled = true;
    private FightUIRework.LoadingState m_loadingState;
    private readonly HashSet<IUIResourceProvider> m_uiResourceProviders = new HashSet<IUIResourceProvider>();
    private DebugFightUI m_debugUI;
    private TeamPointCounter m_teamPointCounter;
    private FightInfoMessageRoot m_messageRibbonRoot;

    public static FightUIRework instance => FightUIRework.s_instance;

    public static bool tooltipsEnabled
    {
      get => FightUIRework.s_tooltipEnabled;
      set
      {
        if (value == FightUIRework.s_tooltipEnabled)
          return;
        if (!value)
          FightUIRework.HideTooltip();
        FightUIRework.s_tooltipEnabled = value;
      }
    }

    public event Action OnQuitRequest;

    public static Vector3 WorldToUIWorld(Vector3 worldPos) => FightUIRework.ScreenToWorldUI(CameraHandler.current.camera.WorldToScreenPoint(worldPos));

    public static Vector3 ScreenToWorldUI(Vector3 screenPos)
    {
      screenPos.z = FightUIRework.s_instance.canvas.planeDistance;
      return FightUIRework.s_instance.canvas.worldCamera.ScreenToWorldPoint(screenPos);
    }

    protected override void Awake()
    {
      base.Awake();
      FightUIRework.s_instance = this;
      this.m_turnFeedbackUI.gameObject.SetActive(false);
      this.m_popupMenuButton.onClick.AddListener(new UnityAction(this.OnPopupMenu));
      this.m_optionsButton.onClick.AddListener(new UnityAction(this.OnOptions));
      this.m_quitButton.onClick.AddListener(new UnityAction(this.OnQuit));
      this.m_bugReportButton.gameObject.SetActive(ApplicationConfig.enableBugReport);
      this.m_bugReportButton.onClick.AddListener(new UnityAction(this.OnBugReport));
      if (!ApplicationConfig.debugMode)
        return;
      this.m_debugUI = this.m_factory.CreateDebugUI(this.transform);
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      this.m_popupMenuButton.onClick.RemoveListener(new UnityAction(this.OnPopupMenu));
      this.m_optionsButton.onClick.RemoveListener(new UnityAction(this.OnOptions));
      this.m_quitButton.onClick.RemoveListener(new UnityAction(this.OnQuit));
      this.m_bugReportButton.onClick.RemoveListener(new UnityAction(this.OnBugReport));
      if (!((UnityEngine.Object) FightUIRework.s_instance == (UnityEngine.Object) this))
        return;
      FightUIRework.s_instance = (FightUIRework) null;
    }

    public void Init(FightType fightType, FightDefinition fightDefinition)
    {
      this.CreateMessageRibbonRoot();
      if (fightType != FightType.TeamVersus)
        return;
      this.CreateTeamPointCounter();
    }

    public IEnumerator Load()
    {
      this.m_loadingState = FightUIRework.LoadingState.Loading;
      do
      {
        yield return (object) null;
      }
      while (this.m_uiResourceProviders.Count > 0);
      this.m_loadingState = FightUIRework.LoadingState.Loaded;
    }

    public LocalPlayerUIRework GetLocalPlayerUI(PlayerStatus playerStatus) => this.m_localPlayerUIRework;

    public PlayerUIRework AddPlayer(PlayerStatus playerStatus)
    {
      Transform parent = playerStatus.teamIndex != GameStatus.localPlayerTeamIndex ? this.m_opponentPlayersUIParent : this.m_allyPlayersUIParent;
      return this.m_factory.CreatePlayerUI(playerStatus, parent);
    }

    public void SetResignButtonEnabled(bool value) => this.m_quitButton.interactable = value;

    private void OnPopupMenu() => this.m_popupMenu.Open();

    private void OnOptions()
    {
      StateLayer stateLayer;
      if (StateManager.TryGetLayer("OptionUI", out stateLayer))
      {
        StateManager.SetActiveInputLayer(stateLayer);
        UIManager.instance.NotifyLayerIndexChange();
        OptionState childState = new OptionState()
        {
          onStateClosed = new Action(this.OnOptionsClosed)
        };
        stateLayer.GetChainEnd().SetChildState((StateContext) childState);
      }
      this.m_popupMenu.Close();
    }

    private void OnOptionsClosed()
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("OptionUI", out stateLayer))
        return;
      StateManager.DiscardInputLayer(stateLayer);
      UIManager.instance.NotifyLayerIndexChange();
    }

    private void OnQuit()
    {
      this.m_popupMenu.Close();
      Action onQuitRequest = this.OnQuitRequest;
      if (onQuitRequest == null)
        return;
      onQuitRequest();
    }

    private void OnBugReport()
    {
      if (!BugReportState.isReady)
        return;
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("OptionUI", out stateLayer))
        stateLayer = StateManager.GetDefaultLayer();
      StateManager.SetActiveInputLayer(stateLayer);
      UIManager.instance.NotifyLayerIndexChange();
      BugReportState childState = new BugReportState();
      childState.Initialize();
      stateLayer.GetChainEnd().SetChildState((StateContext) childState);
      this.m_popupMenu.Close();
    }

    public static IEnumerator ShowPlayingSpell(SpellStatus spellStatus, CellObject cell)
    {
      PlaySpellCompanionUI spellCompanionUi = FightUIRework.s_instance.m_playSpellCompanionUI;
      if ((UnityEngine.Object) null != (UnityEngine.Object) spellCompanionUi)
        yield return (object) spellCompanionUi.ShowPlaying(spellStatus, cell);
    }

    public static IEnumerator ShowPlayingCompanion(
      ReserveCompanionStatus reserveCompanion,
      CellObject cell)
    {
      PlaySpellCompanionUI spellCompanionUi = FightUIRework.s_instance.m_playSpellCompanionUI;
      if ((UnityEngine.Object) null != (UnityEngine.Object) spellCompanionUi)
        yield return (object) spellCompanionUi.ShowPlaying(reserveCompanion, cell);
    }

    public static void ShowTooltip(
      ITooltipDataProvider tooltipDataProvider,
      TooltipPosition position,
      RectTransform rectTransform)
    {
      FightTooltip fightTooltip = FightUIRework.GetFightTooltip();
      if (!((UnityEngine.Object) fightTooltip != (UnityEngine.Object) null))
        return;
      fightTooltip.Initialize(tooltipDataProvider);
      fightTooltip.ShowAt(position, rectTransform);
    }

    public static void ShowTooltip(
      ITooltipDataProvider tooltipDataProvider,
      TooltipPosition position,
      Vector3 worldPosition)
    {
      FightTooltip fightTooltip = FightUIRework.GetFightTooltip();
      if (!((UnityEngine.Object) fightTooltip != (UnityEngine.Object) null))
        return;
      fightTooltip.Initialize(tooltipDataProvider);
      fightTooltip.ShowAt(position, worldPosition);
    }

    public static void HideTooltip()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightUIRework.s_instance)
      {
        Log.Error("HideTooltip called while no instance exists.", 317, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\FightUIRework.cs");
      }
      else
      {
        FightTooltip fightTooltip = FightUIRework.s_instance.m_fightTooltip;
        if ((UnityEngine.Object) null == (UnityEngine.Object) fightTooltip)
          return;
        fightTooltip.Hide();
      }
    }

    private static FightTooltip GetFightTooltip()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) FightUIRework.s_instance)
      {
        Log.Error("ShowTooltip called while no instance exists.", 334, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\FightUIRework.cs");
        return (FightTooltip) null;
      }
      return !FightUIRework.s_tooltipEnabled ? (FightTooltip) null : FightUIRework.s_instance.m_fightTooltip;
    }

    public Action onTurnEndButtonClick
    {
      set
      {
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
          return;
        this.m_endOfTurnButton.onClick = value;
      }
    }

    public void SimulateClickTurnEndButton()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
        return;
      this.m_endOfTurnButton.SimulateClick();
    }

    public void StartTurn(int turnIndex, int turnDuration, bool isLocalPlayerTeam)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton)
      {
        if (isLocalPlayerTeam)
          this.m_endOfTurnButton.SetState(EndOfTurnButtonRework.State.LocalPlayerTeam);
        else
          this.m_endOfTurnButton.SetState(EndOfTurnButtonRework.State.OpponentTeam);
        this.m_endOfTurnButton.StartTurn(turnIndex, turnDuration);
      }
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_debugUI))
        return;
      this.m_debugUI.SetTurnIdex(turnIndex);
    }

    public void StartLocalPlayerTurn()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
        return;
      this.m_endOfTurnButton.SetState(EndOfTurnButtonRework.State.LocalPlayer);
    }

    public void EndLocalPlayerTurn()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
        return;
      this.m_endOfTurnButton.SetState(EndOfTurnButtonRework.State.LocalPlayerTeam);
    }

    public void EndTurn()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
        return;
      this.m_endOfTurnButton.EndTurn();
    }

    public IEnumerator ShowTurnFeedback(TurnFeedbackUI.Type type, int entityNameKey)
    {
      string empty;
      if (!RuntimeData.TryGetText(entityNameKey, out empty))
        empty = string.Empty;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_turnFeedbackUI)
      {
        this.m_turnFeedbackUI.Show(type, empty);
        do
        {
          yield return (object) null;
        }
        while (this.m_turnFeedbackUI.isAnimating);
      }
    }

    public void ShowEndOfTurn()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_endOfTurnButton))
        return;
      this.m_endOfTurnButton.ShowEndOfTurn();
    }

    public void SetScore(
      FightScore score,
      string playerOrigin,
      TeamsScoreModificationReason reason)
    {
      if ((UnityEngine.Object) this.m_teamPointCounter != (UnityEngine.Object) null)
        this.m_teamPointCounter.OnScoreChange(score);
      if (reason == TeamsScoreModificationReason.HeroLifeModified)
        return;
      this.DrawScore(FightInfoMessage.Score(score, reason), playerOrigin);
    }

    public void DrawScore(FightInfoMessage message, string playerOrigin)
    {
      if (!((UnityEngine.Object) this.m_messageRibbonRoot != (UnityEngine.Object) null))
        return;
      this.m_messageRibbonRoot.BuildAndDrawScoreMessage(message, playerOrigin);
    }

    public void DrawInfoMessage(FightInfoMessage message, params string[] parameters)
    {
      if (!((UnityEngine.Object) this.m_messageRibbonRoot != (UnityEngine.Object) null))
        return;
      this.m_messageRibbonRoot.BuildAndDrawInfoMessage(message, parameters);
    }

    private void CreateMessageRibbonRoot() => this.m_messageRibbonRoot = this.m_factory.CreateMessageRibbonRoot(this.m_messageRibbonRootParent);

    private void CreateTeamPointCounter()
    {
      this.m_teamPointCounter = this.m_factory.CreateTeamPointCounter(this.m_teamPointCounterParent);
      this.m_teamPointCounter.InitialiseScore(0, 0);
    }

    public UIResourceDisplayMode Register(IUIResourceProvider provider)
    {
      if (!this.m_uiResourceProviders.Add(provider))
        Log.Error("A UI resource provider tried to register itself multiple times.", 499, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\FightUIRework.cs");
      return this.m_loadingState != FightUIRework.LoadingState.Loaded ? UIResourceDisplayMode.Immediate : UIResourceDisplayMode.None;
    }

    public void UnRegister(IUIResourceProvider provider)
    {
      if (this.m_uiResourceProviders.Remove(provider))
        return;
      Log.Error("A UI resource provider tried to un-register itself but it was not registered.", 509, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\FightUIRework.cs");
    }

    private enum LoadingState
    {
      None,
      Loading,
      Loaded,
    }
  }
}
