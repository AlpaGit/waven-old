// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Zaap.VersusState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Utility;
using System.Collections;

namespace Ankama.Cube.UI.Zaap
{
  public class VersusState : StateContext
  {
    private UIZaapPVPLoading m_ui;
    private readonly StateContext m_nextState;

    public VersusState(UIZaapPVPLoading ui, StateContext nextState)
    {
      this.m_ui = ui;
      this.m_nextState = nextState;
    }

    protected override IEnumerator Update()
    {
      while (this.m_nextState.loadState == StateLoadState.Loading)
        yield return (object) null;
      yield return (object) this.m_ui.CloseUI();
      StatesUtility.ClearSecondaryLayers();
    }
  }
}
