// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.SceneEventListener
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.SRP;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Utility
{
  public class SceneEventListener : Singleton<SceneEventListener>
  {
    private List<Action> m_callbacks = new List<Action>();
    private SceneEventListenerBehaviour m_listenerGameObject;

    public void AddUpdateListener(Action callback)
    {
      this.AddListener(callback);
      this.m_listenerGameObject.onUpdate += callback;
    }

    public void RemoveUpdateListener(Action callback)
    {
      this.RemoveListener(callback);
      this.m_listenerGameObject.onUpdate -= callback;
    }

    public void AddGizmosListener(Action callback)
    {
      this.AddListener(callback);
      this.m_listenerGameObject.onDrawGizmos += callback;
    }

    public void RemoveGizmosListener(Action callback)
    {
      this.RemoveListener(callback);
      this.m_listenerGameObject.onDrawGizmos -= callback;
    }

    private void AddListener(Action callback)
    {
      this.m_callbacks.Add(callback);
      if (this.m_callbacks.Count != 1)
        return;
      this.m_listenerGameObject = new GameObject(nameof (SceneEventListener)).AddComponent<SceneEventListenerBehaviour>();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.m_listenerGameObject.gameObject);
    }

    private void RemoveListener(Action callback)
    {
      this.m_callbacks.Remove(callback);
      if (this.m_callbacks.Count != 0 || !((UnityEngine.Object) this.m_listenerGameObject != (UnityEngine.Object) null))
        return;
      DestroyUtility.Destroy((UnityEngine.Object) this.m_listenerGameObject.gameObject);
    }
  }
}
