// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.PopupInfoState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.States;
using Ankama.Utilities;
using System;
using System.Collections;

namespace Ankama.Cube.Code.UI
{
  public class PopupInfoState : LoadSceneStateContext
  {
    private PopupInfoUI m_ui;
    private readonly PopupInfo m_data;
    private bool m_closing;
    public Action onClose;

    public PopupInfoState(PopupInfo data) => this.m_data = data;

    protected override IEnumerator Load()
    {
      PopupInfoState uiloader = this;
      LoadSceneStateContext.UILoader<PopupInfoUI> loader = new LoadSceneStateContext.UILoader<PopupInfoUI>((LoadSceneStateContext) uiloader, "PopupInfoUI", "core/scenes/ui/popup", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      uiloader.m_ui.Initialize(uiloader.m_data);
      uiloader.m_ui.gameObject.SetActive(true);
      uiloader.m_ui.closeAction = new Action(uiloader.Close);
      yield return (object) uiloader.m_ui.OpenCoroutine();
    }

    protected override IEnumerator Update()
    {
      if (this.m_data.hasDisplayDuration)
      {
        yield return (object) new WaitForTime(this.m_data.displayDuration);
        this.Close();
      }
    }

    private void RemoveListener()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
        return;
      this.m_ui.RemoveListeners();
      this.m_ui.closeAction = (Action) null;
    }

    protected override void Disable() => this.RemoveListener();

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      this.RemoveListener();
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
        yield return (object) this.m_ui.CloseCoroutine();
      yield return (object) base.Transition(transitionInfo);
    }

    protected override IEnumerator Unload()
    {
      yield return (object) base.Unload();
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.state != InputState.State.Activated)
        return base.UseInput(inputState);
      switch (inputState.id)
      {
        case 1:
          return true;
        case 2:
        case 3:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.DoClickSelected();
          return true;
        case 4:
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui)
            this.m_ui.SelectNext();
          return true;
        default:
          return base.UseInput(inputState);
      }
    }

    public void Close()
    {
      if (this.m_closing)
        return;
      this.m_closing = true;
      this.parent?.ClearChildState();
      Action onClose = this.onClose;
      if (onClose == null)
        return;
      onClose();
    }
  }
}
