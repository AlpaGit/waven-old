// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.SceneEventListenerBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Utility
{
  [ExecuteInEditMode]
  public class SceneEventListenerBehaviour : MonoBehaviour
  {
    public Action onUpdate;
    public Action onDrawGizmos;

    private void Update()
    {
      Action onUpdate = this.onUpdate;
      if (onUpdate == null)
        return;
      onUpdate();
    }
  }
}
