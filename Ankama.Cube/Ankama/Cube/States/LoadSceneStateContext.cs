// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.LoadSceneStateContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.States
{
  public class LoadSceneStateContext : StateContext
  {
    protected IEnumerator LoadBundleRequest(string bundleName)
    {
      AssetBundleLoadRequest bundleLoadRequest = this.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
        Log.Error(bundleLoadRequest.error.ToString(), 97, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoadSceneStateContext.cs");
    }

    public IEnumerator LoadSceneAndBundleRequest(
      string sceneName,
      string bundleName,
      LoadSceneMode mode = LoadSceneMode.Additive,
      Action<SceneLoadRequest> completed = null)
    {
      LoadSceneStateContext sceneStateContext = this;
      AssetBundleLoadRequest bundleLoadRequest = sceneStateContext.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
      {
        Log.Error(bundleLoadRequest.error.ToString(), 112, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoadSceneStateContext.cs");
      }
      else
      {
        LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
        SceneLoadRequest sceneLoadRequest = sceneStateContext.LoadScene(sceneName, bundleName, loadSceneParameters, completed: completed);
        while (!sceneLoadRequest.isDone)
          yield return (object) null;
        if ((int) sceneLoadRequest.error != 0)
          Log.Error(sceneLoadRequest.error.ToString(), 126, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoadSceneStateContext.cs");
      }
    }

    protected IEnumerator UnloadSceneRequest(Scene scene)
    {
      if (scene.IsValid())
        yield return (object) SceneManager.UnloadSceneAsync(scene);
    }

    protected static bool TryGetSceneAndRoot<T>(string sceneName, out Scene scene, out T root) where T : MonoBehaviour
    {
      scene = SceneManager.GetSceneByName(sceneName);
      if (!scene.IsValid())
      {
        Log.Error("Invalid scene '" + sceneName + "'.", 144, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoadSceneStateContext.cs");
        root = default (T);
        return false;
      }
      root = ScenesUtility.GetSceneRoot<T>(scene);
      return (UnityEngine.Object) null != (UnityEngine.Object) root;
    }

    public class UILoader<S> where S : AbstractUI
    {
      private LoadSceneStateContext m_stateLoader;
      private StateContext m_stateHoster;
      private string m_bundleName;
      private string m_sceneName;
      private Scene m_uiScene;
      private S m_ui;
      private bool m_disableOnLoad;

      public S ui => this.m_ui;

      public Scene scene => this.m_uiScene;

      public UILoader(
        LoadSceneStateContext uiloader,
        string sceneName,
        string uiBundleName,
        bool disableOnLoad = false)
        : this(uiloader, (StateContext) uiloader, sceneName, uiBundleName, disableOnLoad)
      {
      }

      public UILoader(
        LoadSceneStateContext uiloader,
        StateContext uiHoster,
        string sceneName,
        string uiBundleName,
        bool disableOnLoad = false)
      {
        this.m_stateLoader = uiloader;
        this.m_stateHoster = uiHoster;
        this.m_bundleName = uiBundleName;
        this.m_sceneName = sceneName;
        this.m_uiScene = new Scene();
        this.m_ui = default (S);
        this.m_disableOnLoad = disableOnLoad;
      }

      public IEnumerator Load()
      {
        // ISSUE: reference to a compiler-generated field
        int num = this.\u003C\u003E1__state;
        LoadSceneStateContext.UILoader<S> uiLoader = this;
        if (num != 0)
        {
          if (num != 1)
            return false;
          // ISSUE: reference to a compiler-generated field
          this.\u003C\u003E1__state = -1;
          return false;
        }
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E2__current = (object) uiLoader.m_stateLoader.LoadSceneAndBundleRequest(uiLoader.m_sceneName, uiLoader.m_bundleName, completed: new Action<SceneLoadRequest>(uiLoader.OnSceneLoadRequestComplete));
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = 1;
        return true;
      }

      private void OnSceneLoadRequestComplete(SceneLoadRequest sceneLoadRequest)
      {
        if (!LoadSceneStateContext.TryGetSceneAndRoot<S>(sceneLoadRequest.sceneName, out this.m_uiScene, out this.m_ui))
          Log.Error("Something went wrong while loading scene named '" + sceneLoadRequest.sceneName + "'.", 78, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\LoadSceneStateContext.cs");
        if (this.m_disableOnLoad)
          this.m_ui.gameObject.SetActive(false);
        this.m_ui.SetDepth(this.m_stateHoster, -1);
      }
    }
  }
}
