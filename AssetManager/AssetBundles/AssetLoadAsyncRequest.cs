// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetLoadAsyncRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public abstract class AssetLoadAsyncRequest : CustomYieldInstruction
  {
    protected AssetBundleRequest m_assetRequest;
    protected AssetBundleLoadRequest m_bundleLoadRequest;

    [PublicAPI]
    public bool isDone { get; protected set; }

    [PublicAPI]
    public AssetManagerError error { get; protected set; }

    internal AssetLoadAsyncRequest(bool done = false) => this.isDone = done;

    [PublicAPI]
    public override bool keepWaiting => !this.isDone;

    [PublicAPI]
    public float progress
    {
      get
      {
        if (this.m_bundleLoadRequest != null)
          return 0.0f;
        return this.m_assetRequest != null ? this.m_assetRequest.progress : 1f;
      }
    }

    internal abstract bool Update();
  }
}
