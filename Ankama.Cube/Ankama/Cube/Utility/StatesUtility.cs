// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.StatesUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Configuration;
using Ankama.Cube.Demo.States;
using Ankama.Cube.Network;
using Ankama.Cube.States;
using Ankama.Cube.UI;
using Ankama.Utilities;
using JetBrains.Annotations;

namespace Ankama.Cube.Utility
{
  public static class StatesUtility
  {
    public const string PlayerUILayerName = "PlayerUI";
    public const string OptionUILayerName = "OptionUI";
    private static StateLayer s_optionLayer;
    private static StateLayer s_playerLayer;

    private static StateLayer optionLayer
    {
      get
      {
        if (StatesUtility.s_optionLayer == null)
          StatesUtility.s_optionLayer = StateManager.AddLayer("OptionUI");
        return StatesUtility.s_optionLayer;
      }
    }

    private static StateContext playerLayer
    {
      get
      {
        if (StatesUtility.s_playerLayer == null)
          StatesUtility.s_playerLayer = StateManager.AddLayer("PlayerUI");
        return (StateContext) StatesUtility.s_playerLayer;
      }
    }

    public static void GotoLoginState()
    {
      ConnectionHandler.instance.Disconnect();
      StateContext childState = StateManager.GetDefaultLayer().GetChildState();
      if (childState is LoginState || childState is LoginStateDemo)
        return;
      StatesUtility.DoTransition(!ApplicationConfig.simulateDemo ? (StateContext) new LoginState() : (StateContext) new LoginStateDemo(), childState);
    }

    public static void GotoMainMenu() => StatesUtility.GotoDimensionState();

    public static void GotoDimensionState()
    {
      StatesUtility.DoTransition((StateContext) new HavreDimensionMainState(), StateManager.GetDefaultLayer().GetChildState());
      StatesUtility.playerLayer.SetChildState((StateContext) new PlayerUIMainState());
      StatesUtility.optionLayer.SetChildState((StateContext) new ParametersState());
      StateManager.SetActiveInputLayer(StatesUtility.optionLayer);
      UIManager.instance.NotifyLayerIndexChange();
    }

    public static void GotoMatchMakingState()
    {
      Log.Warning("GotoMatchMakingState while not in debugMode. Goto HavreDimension instead", 87, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Utility\\StatesUtility.cs");
      StatesUtility.GotoDimensionState();
    }

    public static void GotoClearState(StateContext state)
    {
      StatesUtility.ClearSecondaryLayers();
      StatesUtility.DoTransition(state, StateManager.GetDefaultLayer().GetChildState());
    }

    public static void ClearSecondaryLayers()
    {
      StatesUtility.ClearOptionLayer();
      StatesUtility.playerLayer.ClearChildState();
    }

    public static void ClearOptionLayer() => StatesUtility.optionLayer.ClearChildState();

    public static TransitionState DoTransition(
      [NotNull] StateContext nextState,
      [CanBeNull] StateContext previousState,
      [CanBeNull] StateContext parentState = null)
    {
      TransitionState childState = new TransitionState(nextState, previousState);
      (parentState ?? previousState?.parent ?? (StateContext) StateManager.GetDefaultLayer()).SetChildState((StateContext) childState);
      return childState;
    }
  }
}
