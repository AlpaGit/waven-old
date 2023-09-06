// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.NicknameRequest.NicknameRequestState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.States;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.UI.NicknameRequest
{
  public class NicknameRequestState : LoadSceneStateContext
  {
    private NicknameRequestUI m_ui;
    private string m_nickname;

    public event Action<string> OnSuccess;

    protected override IEnumerator Load()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      NicknameRequestState uiloader = this;
      LoadSceneStateContext.UILoader<NicknameRequestUI> loader;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        uiloader.m_ui = loader.ui;
        uiloader.m_ui.gameObject.SetActive(true);
        uiloader.m_ui.OnNicknameRequest += new Action<string>(uiloader.OnNicknameRequest);
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      loader = new LoadSceneStateContext.UILoader<NicknameRequestUI>((LoadSceneStateContext) uiloader, "NicknameRequestUI", "core/scenes/ui/login", true);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) loader.Load();
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private void OnNicknameRequest(string nickname)
    {
      this.m_ui.interactable = false;
      this.m_nickname = nickname;
    }

    private void OnNicknameResult(
      bool success,
      IList<string> suggestList,
      string errorKey,
      string errorTranslated)
    {
      if (!success)
      {
        this.m_ui.interactable = true;
        this.m_ui.OnNicknameError(suggestList, errorKey, errorTranslated);
      }
      else
        this.parent?.ClearChildState();
    }

    protected override IEnumerator Unload()
    {
      yield return (object) base.Unload();
      Action<string> onSuccess = this.OnSuccess;
      if (onSuccess != null)
        onSuccess(this.m_nickname);
    }
  }
}
