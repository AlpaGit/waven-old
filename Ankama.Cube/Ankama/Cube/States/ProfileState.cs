// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.ProfileState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using Ankama.Cube.UI.PlayerLayer;
using System.Collections;

namespace Ankama.Cube.States
{
  public class ProfileState : LoadSceneStateContext, IStateUIChildPriority
  {
    private ProfileUIRoot m_ui;

    public UIPriority uiChildPriority => UIPriority.Front;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      ProfileState uiloader = this;
      LoadSceneStateContext.UILoader<ProfileUIRoot> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        uiloader.m_ui.Initialise();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<ProfileUIRoot>((LoadSceneStateContext) uiloader, "PlayerLayer_ProfilCanvas", "core/scenes/ui/deck", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override IEnumerator Update()
    {
      yield return (object) this.m_ui.PlayEnterAnimation();
      base.Enable();
    }

    public override bool AllowsTransition(StateContext nextState) => true;

    protected override IEnumerator Transition(StateTransitionInfo transitionInfo)
    {
      yield return (object) this.m_ui.Unload();
    }

    protected override void Enable()
    {
      this.m_ui.PlayEnterAnimation();
      base.Enable();
    }
  }
}
