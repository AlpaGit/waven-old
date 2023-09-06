// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.GameObjectPool
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Ankama.Utilities
{
  [PublicAPI]
  public sealed class GameObjectPool : IDisposable
  {
    private readonly Stack<GameObject> m_stack;
    private int m_maxSize;
    private Scene m_activeScene;

    [PublicAPI]
    public GameObject prefab { get; private set; }

    public int maxSize
    {
      get => this.m_maxSize;
      set
      {
        value = Math.Max(0, value);
        if (value == this.m_maxSize)
          return;
        if (value > 0)
        {
          while (this.m_stack.Count > value)
          {
            GameObject gameObject = this.m_stack.Pop();
            if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
              UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
          }
        }
        this.m_maxSize = value;
      }
    }

    [PublicAPI]
    public GameObjectPool([NotNull] GameObject prefab)
    {
      this.m_stack = new Stack<GameObject>();
      this.prefab = prefab;
      this.InitializeSceneManagement();
    }

    [PublicAPI]
    public GameObjectPool([NotNull] GameObject prefab, int capacity)
    {
      this.m_stack = new Stack<GameObject>(capacity);
      this.prefab = prefab;
      this.InitializeSceneManagement();
    }

    [PublicAPI]
    public GameObjectPool([NotNull] GameObject prefab, int capacity, int maxSize)
    {
      this.m_stack = new Stack<GameObject>(capacity);
      this.m_maxSize = maxSize;
      this.prefab = prefab;
      this.InitializeSceneManagement();
    }

    [PublicAPI]
    public void SetPrefab([NotNull] GameObject newPrefab)
    {
      this.prefab = newPrefab;
      Stack<GameObject> stack = this.m_stack;
      while (stack.Count > 0)
      {
        GameObject gameObject = stack.Pop();
        if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    [PublicAPI]
    public GameObject Instantiate([CanBeNull] Transform parent, bool instantiateInWorldSpace = true)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.prefab)
        throw new Exception("Prefab has been unloaded.");
      Stack<GameObject> stack = this.m_stack;
      GameObject go;
      if (stack.Count > 0)
      {
        go = stack.Pop();
        Transform transform1 = go.transform;
        Transform transform2 = this.prefab.transform;
        if ((UnityEngine.Object) null == (UnityEngine.Object) parent)
        {
          transform1.SetPositionAndRotation(transform2.position, transform2.rotation);
          transform1.SetParent((Transform) null, true);
          SceneManager.MoveGameObjectToScene(go, this.m_activeScene);
        }
        else if (instantiateInWorldSpace)
        {
          transform1.SetParent(parent, false);
          transform1.SetPositionAndRotation(transform2.position, transform2.rotation);
        }
        else
        {
          transform1.localPosition = transform2.localPosition;
          transform1.localRotation = transform2.localRotation;
          transform1.SetParent(parent, false);
        }
        go.SetActive(this.prefab.activeSelf);
      }
      else
        go = UnityEngine.Object.Instantiate<GameObject>(this.prefab, parent, instantiateInWorldSpace);
      return go;
    }

    [PublicAPI]
    public GameObject Instantiate(Vector3 position, Quaternion rotation, [CanBeNull] Transform parent)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.prefab)
        throw new Exception("Prefab has been unloaded.");
      Stack<GameObject> stack = this.m_stack;
      GameObject go;
      if (stack.Count > 0)
      {
        go = stack.Pop();
        Transform transform = go.transform;
        if ((UnityEngine.Object) null == (UnityEngine.Object) parent)
        {
          transform.SetPositionAndRotation(position, rotation);
          transform.SetParent((Transform) null, true);
          SceneManager.MoveGameObjectToScene(go, this.m_activeScene);
        }
        else
        {
          transform.SetParent(parent, false);
          transform.SetPositionAndRotation(position, rotation);
        }
        go.SetActive(this.prefab.activeSelf);
      }
      else
        go = UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, rotation, parent);
      return go;
    }

    [PublicAPI]
    public bool Release([NotNull] GameObject instance)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) instance)
        throw new ArgumentNullException(nameof (instance));
      if (this.m_maxSize == 0 || this.m_stack.Count < this.m_maxSize)
      {
        instance.SetActive(false);
        instance.transform.SetParent((Transform) null, true);
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) instance);
        this.m_stack.Push(instance);
        return true;
      }
      UnityEngine.Object.Destroy((UnityEngine.Object) instance);
      return false;
    }

    [PublicAPI]
    public int Count() => this.m_stack.Count;

    [PublicAPI]
    public bool Contains([NotNull] GameObject instance) => this.m_stack.Contains(instance);

    [PublicAPI]
    public void Clear()
    {
      Stack<GameObject> stack = this.m_stack;
      while (stack.Count > 0)
      {
        GameObject gameObject = stack.Pop();
        if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
    }

    [PublicAPI]
    public void Dispose()
    {
      this.prefab = (GameObject) null;
      Stack<GameObject> stack = this.m_stack;
      while (stack.Count > 0)
      {
        GameObject gameObject = stack.Pop();
        if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      }
      SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.OnActiveSceneChanged);
    }

    private void InitializeSceneManagement()
    {
      this.m_activeScene = SceneManager.GetActiveScene();
      SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.OnActiveSceneChanged);
    }

    private void OnActiveSceneChanged(Scene current, Scene next) => this.m_activeScene = next;
  }
}
