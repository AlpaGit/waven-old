// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.FightEndFeedbackState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.UI;
using Ankama.Utilities;
using System.Collections;

namespace Ankama.Cube.States
{
  public class FightEndFeedbackState : LoadSceneStateContext
  {
    private FightStatusEndReason m_reason;
    private BaseOpenCloseUI m_ui;
    private bool m_isActive = true;

    public bool isActive => this.m_isActive;

    public FightEndFeedbackState(FightStatusEndReason reason) => this.m_reason = reason;

    protected override IEnumerator Load()
    {
      FightEndFeedbackState uiloader = this;
      string sceneName = uiloader.m_reason == FightStatusEndReason.Win ? "FightEndedWinMatchUI" : "FightEndedDiedUI";
      LoadSceneStateContext.UILoader<BaseOpenCloseUI> loader = new LoadSceneStateContext.UILoader<BaseOpenCloseUI>((LoadSceneStateContext) uiloader, sceneName, "core/scenes/ui/fight", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
    }

    protected override IEnumerator Update()
    {
      FightEndFeedbackState endFeedbackState = this;
      endFeedbackState.m_ui.gameObject.SetActive(true);
      yield return (object) endFeedbackState.m_ui.OpenCoroutine();
      yield return (object) new WaitForTime(1.7f);
      yield return (object) endFeedbackState.m_ui.CloseCoroutine();
      endFeedbackState.parent.ClearChildState();
      endFeedbackState.m_isActive = false;
    }
  }
}
