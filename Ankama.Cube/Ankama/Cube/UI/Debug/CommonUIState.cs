// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.CommonUIState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.States;
using System.Collections;

namespace Ankama.Cube.UI.Debug
{
  public class CommonUIState : LoadSceneStateContext
  {
    private CommonUI m_ui;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      CommonUIState uiloader = this;
      LoadSceneStateContext.UILoader<CommonUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<CommonUI>((LoadSceneStateContext) uiloader, "CommonUI", "core/scenes/ui/examples", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
