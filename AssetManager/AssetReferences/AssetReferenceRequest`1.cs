// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetReferences.AssetReferenceRequest`1
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.AssetReferences
{
  [PublicAPI]
  public class AssetReferenceRequest<T> : CustomYieldInstruction where T : Object
  {
    [PublicAPI]
    public static readonly AssetReferenceRequest<T> empty = new AssetReferenceRequest<T>();
    private ResourceRequest m_request;

    [PublicAPI]
    public T asset { get; private set; }

    [PublicAPI]
    public float progress => this.m_request != null ? this.m_request.progress : 1f;

    [PublicAPI]
    public bool isDone => !this.keepWaiting;

    [PublicAPI]
    public override bool keepWaiting
    {
      get
      {
        if (this.m_request == null)
          return false;
        if (!this.m_request.isDone)
          return true;
        this.asset = this.m_request.asset as T;
        this.m_request = (ResourceRequest) null;
        return false;
      }
    }

    internal AssetReferenceRequest(string assetPath) => this.m_request = Resources.LoadAsync<T>(assetPath);

    internal AssetReferenceRequest()
    {
    }
  }
}
