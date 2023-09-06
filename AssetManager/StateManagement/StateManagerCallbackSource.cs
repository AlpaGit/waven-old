// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StateManagement.StateManagerCallbackSource
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.StateManagement
{
  [AddComponentMenu("")]
  internal class StateManagerCallbackSource : MonoBehaviour
  {
    [UsedImplicitly]
    private void Awake()
    {
      this.gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }

    [UsedImplicitly]
    private void Start() => this.StartCoroutine(StateManager.Update());
  }
}
