// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.OptionState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Player;
using Ankama.Cube.UI;
using JetBrains.Annotations;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class OptionState : LoadSceneStateContext
  {
    private OptionUI m_ui;
    public Action onStateClosed;

    protected override IEnumerator Load()
    {
      OptionState uiloader = this;
      LoadSceneStateContext.UILoader<OptionUI> loader = new LoadSceneStateContext.UILoader<OptionUI>((LoadSceneStateContext) uiloader, "OptionUI", "core/scenes/ui/option", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.OnErrorPopupInfo = new Action<PopupInfo>(uiloader.OnErrorPopupInfo);
      uiloader.m_ui.Initialise();
      yield return (object) null;
      uiloader.m_ui.gameObject.SetActive(true);
      yield return (object) uiloader.m_ui.OpenCoroutine();
    }

    private void DisableUIEvents()
    {
      this.m_ui.OnErrorPopupInfo = (Action<PopupInfo>) null;
      this.m_ui.onCloseClick = (Action) null;
    }

    protected override void Enable() => this.m_ui.onCloseClick = new Action(this.OnCloseClick);

    protected override void Disable()
    {
      this.DisableUIEvents();
      PlayerPreferences.Save();
    }

    public override bool AllowsTransition([CanBeNull] StateContext nextState) => true;

    protected override IEnumerator Transition([CanBeNull] StateTransitionInfo transitionInfo)
    {
      this.m_ui.onCloseClick = (Action) null;
      yield return (object) this.m_ui.CloseCoroutine();
      Action onStateClosed = this.onStateClosed;
      if (onStateClosed != null)
        onStateClosed();
    }

    protected override IEnumerator Unload()
    {
      this.DisableUIEvents();
      return base.Unload();
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1)
        return base.UseInput(inputState);
      if (inputState.state == InputState.State.Activated && (UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        this.m_ui.SimulateCloseClick();
      return true;
    }

    private void OnCloseClick() => this.parent.ClearChildState();

    private void OnErrorPopupInfo(PopupInfo popupInfo) => PopupInfoManager.Show((StateContext) this, popupInfo);
  }
}
