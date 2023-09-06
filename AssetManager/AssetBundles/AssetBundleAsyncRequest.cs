// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleAsyncRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public abstract class AssetBundleAsyncRequest : CustomYieldInstruction
  {
    [PublicAPI]
    public bool isDone { get; protected internal set; }

    [PublicAPI]
    public AssetManagerError error { get; protected internal set; }

    internal AssetBundleAsyncRequest(bool done = false) => this.isDone = done;

    public override bool keepWaiting => !this.isDone;

    [PublicAPI]
    public abstract float progress { get; }
  }
}
