// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.SceneLoadRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class SceneLoadRequest : CustomYieldInstruction
  {
    [PublicAPI]
    public readonly string sceneName;
    [PublicAPI]
    public readonly LoadSceneParameters loadSceneParameters;
    [PublicAPI]
    public readonly string assetBundleFileName;
    private bool m_isDone;
    private bool m_isReady;
    private bool m_allowSceneActivation;
    private bool m_isAborting;
    private AsyncOperation m_sceneLoadRequest;
    private AssetBundleLoadRequest m_bundleLoadRequest;

    [PublicAPI]
    public bool allowSceneActivation
    {
      get => this.m_allowSceneActivation;
      set
      {
        if (this.m_sceneLoadRequest != null && !this.m_isAborting)
          this.m_sceneLoadRequest.allowSceneActivation = value;
        this.m_allowSceneActivation = value;
      }
    }

    [PublicAPI]
    public Action<SceneLoadRequest> completed { get; set; }

    [PublicAPI]
    public Scene scene { get; private set; }

    [PublicAPI]
    public bool isDone => this.m_isDone || this.Update();

    [PublicAPI]
    public bool isReady => this.m_isDone || this.Update() || this.m_isReady;

    [PublicAPI]
    public AssetManagerError error { get; private set; }

    internal SceneLoadRequest(
      string sceneName,
      LoadSceneParameters loadSceneParameters,
      bool allowSceneActivation,
      string assetBundleFileName,
      Action<SceneLoadRequest> completed)
    {
      this.sceneName = sceneName;
      this.loadSceneParameters = loadSceneParameters;
      this.assetBundleFileName = assetBundleFileName;
      this.completed = completed;
      this.m_allowSceneActivation = allowSceneActivation;
      this.m_sceneLoadRequest = SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
      this.m_sceneLoadRequest.allowSceneActivation = allowSceneActivation;
      this.m_sceneLoadRequest.completed += new Action<AsyncOperation>(this.OnSceneLoadComplete);
    }

    internal SceneLoadRequest(
      string sceneName,
      LoadSceneParameters loadSceneParameters,
      bool allowSceneActivation,
      AssetBundleLoadRequest bundleLoadRequest,
      Action<SceneLoadRequest> completed)
    {
      this.sceneName = sceneName;
      this.loadSceneParameters = loadSceneParameters;
      this.completed = completed;
      this.assetBundleFileName = bundleLoadRequest.assetBundleFileName;
      this.m_allowSceneActivation = allowSceneActivation;
      this.m_bundleLoadRequest = bundleLoadRequest;
    }

    internal SceneLoadRequest(
      string sceneName,
      LoadSceneParameters loadSceneParameters,
      bool allowSceneActivation,
      string assetBundleFileName,
      int errorCode,
      string errorMessage)
    {
      this.sceneName = sceneName;
      this.loadSceneParameters = loadSceneParameters;
      this.assetBundleFileName = assetBundleFileName;
      this.m_allowSceneActivation = allowSceneActivation;
      this.error = new AssetManagerError(errorCode, errorMessage);
      this.m_isDone = true;
    }

    [PublicAPI]
    public override bool keepWaiting => !this.isDone && !this.Update();

    [PublicAPI]
    public float progress
    {
      get
      {
        if (this.m_bundleLoadRequest != null)
          return 0.0f;
        return this.m_sceneLoadRequest != null ? this.m_sceneLoadRequest.progress : 1f;
      }
    }

    private bool Update()
    {
      if (this.m_bundleLoadRequest != null)
      {
        if (!this.m_bundleLoadRequest.isDone)
          return false;
        AssetManagerError error = this.m_bundleLoadRequest.error;
        this.m_bundleLoadRequest = (AssetBundleLoadRequest) null;
        if ((int) error != 0)
        {
          this.error = error;
          this.m_isDone = true;
          return true;
        }
        this.m_sceneLoadRequest = SceneManager.LoadSceneAsync(this.sceneName);
        this.m_sceneLoadRequest.allowSceneActivation = this.m_allowSceneActivation;
        this.m_sceneLoadRequest.completed += new Action<AsyncOperation>(this.OnSceneLoadComplete);
      }
      if (this.m_sceneLoadRequest != null)
      {
        if (this.m_sceneLoadRequest.isDone)
        {
          this.m_sceneLoadRequest = (AsyncOperation) null;
          this.scene = SceneManager.GetSceneByName(this.sceneName);
          if (!this.scene.IsValid())
          {
            this.error = (AssetManagerError) 30;
            this.m_isDone = true;
            return true;
          }
        }
        else
        {
          if (!this.m_allowSceneActivation && !this.m_isReady)
            this.m_isReady = (double) this.m_sceneLoadRequest.progress >= 0.89999997615814209;
          return false;
        }
      }
      if (!this.scene.isLoaded)
        return false;
      this.m_isDone = true;
      return true;
    }

    internal void Abort()
    {
      if (this.m_isDone || this.m_isAborting)
        return;
      this.m_isAborting = true;
      if (this.m_bundleLoadRequest != null)
        this.m_bundleLoadRequest = (AssetBundleLoadRequest) null;
      else if (this.m_sceneLoadRequest != null)
      {
        if (this.m_sceneLoadRequest.isDone)
        {
          this.DeactivateScene(this.m_sceneLoadRequest);
        }
        else
        {
          this.m_sceneLoadRequest.allowSceneActivation = true;
          this.m_sceneLoadRequest.completed += new Action<AsyncOperation>(this.DeactivateScene);
          return;
        }
      }
      this.error = (AssetManagerError) 50;
      this.m_isDone = true;
    }

    private void OnSceneLoadComplete(AsyncOperation asyncOperation)
    {
      if (this.completed == null || this.m_isAborting || !SceneManager.GetSceneByName(this.sceneName).IsValid())
        return;
      this.completed(this);
    }

    private void DeactivateScene(AsyncOperation operation)
    {
      Scene sceneByName = SceneManager.GetSceneByName(this.sceneName);
      if (sceneByName.IsValid())
      {
        foreach (UnityEngine.Object rootGameObject in sceneByName.GetRootGameObjects())
          UnityEngine.Object.Destroy(rootGameObject);
      }
      this.error = (AssetManagerError) 50;
      this.m_isDone = true;
    }
  }
}
