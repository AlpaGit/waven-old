// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.SplashState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using Ankama.Cube.Utility;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.States
{
  public class SplashState : StateContext
  {
    private const string SplashUISceneName = "SplashUI";
    private Scene m_splashUIScene;
    private AbstractUI m_ui;

    protected override IEnumerator Load()
    {
      SplashState state = this;
      yield return (object) SceneManager.LoadSceneAsync("SplashUI", LoadSceneMode.Additive);
      state.m_splashUIScene = SceneManager.GetSceneByName("SplashUI");
      while (!state.m_splashUIScene.isLoaded)
        yield return (object) null;
      state.m_ui = ScenesUtility.GetSceneRoot<AbstractUI>(state.m_splashUIScene);
      state.m_ui.SetDepth((StateContext) state);
    }

    protected override IEnumerator Unload()
    {
      if (this.m_splashUIScene.IsValid())
        yield return (object) SceneManager.UnloadSceneAsync(this.m_splashUIScene);
      yield return (object) base.Unload();
    }
  }
}
