// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.ParametersState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.UI;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class ParametersState : LoadSceneStateContext
  {
    private ParametersUI m_ui;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ParametersState uiloader = this;
      LoadSceneStateContext.UILoader<ParametersUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<ParametersUI>((LoadSceneStateContext) uiloader, "ParametersUI", "core/scenes/ui/option");
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void Enable()
    {
      this.m_ui.onShowMenu = new Action<bool>(this.OnShowMenu);
      this.m_ui.onOptionClick = new Action(this.OnOptionClick);
      this.m_ui.onQuitClick = new Action(this.OnQuitClick);
    }

    protected override void Disable()
    {
      this.m_ui.onShowMenu = (Action<bool>) null;
      this.m_ui.onOptionClick = (Action) null;
      this.m_ui.onQuitClick = (Action) null;
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated && (UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        this.m_ui.SimulateOptionClick();
      return true;
    }

    private void OnShowMenu(bool value)
    {
      if (value)
        this.SetFocusOnLayer("OptionUI");
      else
        this.SetFocusOnLayer("PlayerUI");
    }

    private void OnOptionClick()
    {
      if (this.HasPendingStates() || this.HasChildState())
        return;
      this.SetFocusOnLayer("OptionUI");
      this.SetChildState((StateContext) new OptionState()
      {
        onStateClosed = new Action(this.OnOptionClosed)
      });
    }

    private void OnQuitClick() => PopupInfoManager.Show((StateContext) this, new PopupInfo()
    {
      message = (RawTextData) 75182,
      buttons = new ButtonData[2]
      {
        new ButtonData((TextData) 9912, new Action(Main.Quit), style: ButtonStyle.Negative),
        new ButtonData((TextData) 68421)
      },
      selectedButton = 2,
      style = PopupStyle.Normal,
      useBlur = true
    });

    private void OnOptionClosed() => this.SetFocusOnLayer("PlayerUI");

    private void SetFocusOnLayer(string layerName)
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer(layerName, out stateLayer))
        return;
      StateManager.SetActiveInputLayer(stateLayer);
    }
  }
}
