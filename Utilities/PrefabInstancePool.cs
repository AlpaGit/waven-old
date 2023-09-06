// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.PrefabInstancePool
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
  public sealed class PrefabInstancePool : IDisposable
  {
    private readonly Dictionary<int, PrefabInstancePool.StackWrapper> m_dictionary;
    private Transform m_container;
    private Scene m_activeScene;

    [PublicAPI]
    public PrefabInstancePool()
    {
      this.m_dictionary = new Dictionary<int, PrefabInstancePool.StackWrapper>();
      this.CreateContainer();
      this.InitializeSceneManagement();
    }

    [PublicAPI]
    public PrefabInstancePool(int capacity)
    {
      this.m_dictionary = new Dictionary<int, PrefabInstancePool.StackWrapper>(capacity);
      this.CreateContainer();
      this.InitializeSceneManagement();
    }

    [PublicAPI]
    public void PreparePool([NotNull] GameObject prefab, int capacity)
    {
      int instanceId = prefab.GetInstanceID();
      PrefabInstancePool.StackWrapper stackWrapper;
      if (this.m_dictionary.TryGetValue(instanceId, out stackWrapper))
        return;
      stackWrapper = new PrefabInstancePool.StackWrapper(new Stack<GameObject>(capacity));
      this.m_dictionary.Add(instanceId, stackWrapper);
    }

    [PublicAPI]
    public void PreparePool([NotNull] GameObject prefab, int capacity, int maxSize)
    {
      int instanceId = prefab.GetInstanceID();
      PrefabInstancePool.StackWrapper stackWrapper;
      if (!this.m_dictionary.TryGetValue(instanceId, out stackWrapper))
      {
        stackWrapper = new PrefabInstancePool.StackWrapper(new Stack<GameObject>(capacity), maxSize);
        this.m_dictionary.Add(instanceId, stackWrapper);
      }
      else
        stackWrapper.SetMaxSize(maxSize);
    }

    [PublicAPI]
    public bool HasPool([NotNull] GameObject prefab) => this.m_dictionary.ContainsKey(prefab.GetInstanceID());

    [PublicAPI]
    public bool RemovePool([NotNull] GameObject prefab)
    {
      int instanceId = prefab.GetInstanceID();
      PrefabInstancePool.StackWrapper stackWrapper;
      if (!this.m_dictionary.TryGetValue(instanceId, out stackWrapper))
        return false;
      stackWrapper.Clear();
      return this.m_dictionary.Remove(instanceId);
    }

    [PublicAPI]
    public int CountPools() => this.m_dictionary.Count;

    [PublicAPI]
    public void RemovePools()
    {
      foreach (PrefabInstancePool.StackWrapper stackWrapper in this.m_dictionary.Values)
        stackWrapper.Clear();
      this.m_dictionary.Clear();
    }

    [PublicAPI]
    public GameObject Instantiate(
      [NotNull] GameObject prefab,
      [CanBeNull] Transform parent,
      bool instantiateInWorldSpace = true)
    {
      int instanceId = prefab.GetInstanceID();
      PrefabInstancePool.StackWrapper stackWrapper;
      Stack<GameObject> stack;
      if (!this.m_dictionary.TryGetValue(instanceId, out stackWrapper))
      {
        stack = new Stack<GameObject>(2);
        stackWrapper = new PrefabInstancePool.StackWrapper(stack);
        this.m_dictionary.Add(instanceId, stackWrapper);
      }
      else
        stack = stackWrapper.stack;
      GameObject go;
      if (stack.Count > 0)
      {
        go = stack.Pop();
        Transform transform1 = go.transform;
        Transform transform2 = prefab.transform;
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
      }
      else
        go = UnityEngine.Object.Instantiate<GameObject>(prefab, parent, instantiateInWorldSpace);
      return go;
    }

    [PublicAPI]
    public GameObject Instantiate(
      [NotNull] GameObject prefab,
      Vector3 position,
      Quaternion rotation,
      [CanBeNull] Transform parent)
    {
      int instanceId = prefab.GetInstanceID();
      PrefabInstancePool.StackWrapper stackWrapper;
      Stack<GameObject> stack;
      if (!this.m_dictionary.TryGetValue(instanceId, out stackWrapper))
      {
        stack = new Stack<GameObject>(2);
        stackWrapper = new PrefabInstancePool.StackWrapper(stack);
        this.m_dictionary.Add(instanceId, stackWrapper);
      }
      else
        stack = stackWrapper.stack;
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
      }
      else
        go = UnityEngine.Object.Instantiate<GameObject>(prefab, position, rotation, parent);
      return go;
    }

    [PublicAPI]
    public bool Release([NotNull] GameObject prefab, [NotNull] GameObject instance)
    {
      PrefabInstancePool.StackWrapper stackWrapper;
      if (!this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper))
        throw new Exception("Tried to release a GameObject instance from a pool that doesn't exist.");
      instance.transform.SetParent(this.m_container);
      return stackWrapper.Push(instance);
    }

    public int GetMaxSize([NotNull] GameObject prefab)
    {
      PrefabInstancePool.StackWrapper stackWrapper;
      return !this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper) ? 0 : stackWrapper.maxSize;
    }

    public void SetMaxSize([NotNull] GameObject prefab, int maxSize)
    {
      PrefabInstancePool.StackWrapper stackWrapper;
      if (!this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper))
        return;
      stackWrapper.SetMaxSize(maxSize);
    }

    [PublicAPI]
    public int Count([NotNull] GameObject prefab)
    {
      PrefabInstancePool.StackWrapper stackWrapper;
      return !this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper) ? 0 : stackWrapper.stack.Count;
    }

    [PublicAPI]
    public bool Contains([NotNull] GameObject prefab, [NotNull] GameObject instance)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) instance)
        throw new ArgumentNullException(nameof (instance));
      PrefabInstancePool.StackWrapper stackWrapper;
      return this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper) && stackWrapper.stack.Contains(instance);
    }

    [PublicAPI]
    public void Clear([NotNull] GameObject prefab)
    {
      PrefabInstancePool.StackWrapper stackWrapper;
      if (!this.m_dictionary.TryGetValue(prefab.GetInstanceID(), out stackWrapper))
        return;
      stackWrapper.Clear();
    }

    [PublicAPI]
    public void Dispose()
    {
      foreach (PrefabInstancePool.StackWrapper stackWrapper in this.m_dictionary.Values)
        stackWrapper.Clear();
      this.m_dictionary.Clear();
      SceneManager.activeSceneChanged -= new UnityAction<Scene, Scene>(this.OnActiveSceneChanged);
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_container))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_container.gameObject);
      this.m_container = (Transform) null;
    }

    private void CreateContainer()
    {
      GameObject target = new GameObject(nameof (PrefabInstancePool));
      target.SetActive(false);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) target);
      this.m_container = target.transform;
    }

    private void InitializeSceneManagement()
    {
      this.m_activeScene = SceneManager.GetActiveScene();
      SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(this.OnActiveSceneChanged);
    }

    private void OnActiveSceneChanged(Scene current, Scene next) => this.m_activeScene = next;

    private struct StackWrapper
    {
      public readonly Stack<GameObject> stack;
      public int maxSize;

      public StackWrapper(Stack<GameObject> stack)
      {
        this.stack = stack;
        this.maxSize = 0;
      }

      public StackWrapper(Stack<GameObject> stack, int maxSize)
      {
        this.stack = stack;
        this.maxSize = Math.Max(0, maxSize);
      }

      public void SetMaxSize(int value)
      {
        value = Math.Max(0, value);
        if (value != this.maxSize)
          return;
        if (value > 0)
        {
          while (this.stack.Count > value)
          {
            GameObject gameObject = this.stack.Pop();
            if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
              UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
          }
        }
        this.maxSize = value;
      }

      public void Clear()
      {
        while (this.stack.Count > 0)
        {
          GameObject gameObject = this.stack.Pop();
          if ((UnityEngine.Object) null != (UnityEngine.Object) gameObject)
            UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
        }
      }

      public bool Push(GameObject instance)
      {
        if (this.maxSize == 0 || this.stack.Count < this.maxSize)
        {
          this.stack.Push(instance);
          return true;
        }
        UnityEngine.Object.Destroy((UnityEngine.Object) instance);
        return false;
      }
    }
  }
}
