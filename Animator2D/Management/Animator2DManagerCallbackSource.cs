// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Management.Animator2DManagerCallbackSource
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Animations.Management
{
  [AddComponentMenu("")]
  internal sealed class Animator2DManagerCallbackSource : MonoBehaviour
  {
    [UsedImplicitly]
    private void Awake()
    {
      this.gameObject.hideFlags = HideFlags.HideInHierarchy;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }

    [UsedImplicitly]
    private void LateUpdate() => Animator2DManager.Update();

    [UsedImplicitly]
    private void OnApplicationQuit() => Animator2DManager.Release();
  }
}
