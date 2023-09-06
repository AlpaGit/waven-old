// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleState
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public struct AssetBundleState
  {
    [PublicAPI]
    public readonly string name;
    [PublicAPI]
    public readonly AssetBundleState.LoadState loadState;
    [PublicAPI]
    public readonly int referenceCount;
    [PublicAPI]
    public readonly int dependencyReferenceCount;

    internal AssetBundleState(
      string name,
      AssetBundleState.LoadState loadState,
      int referenceCount,
      int dependencyReferenceCount)
    {
      this.name = name;
      this.loadState = loadState;
      this.referenceCount = referenceCount;
      this.dependencyReferenceCount = dependencyReferenceCount;
    }

    public override string ToString() => string.Format("{0} (loadState: {1}, reference count: {2}, dependency reference count: {3})", (object) this.name, (object) this.loadState, (object) this.referenceCount, (object) this.dependencyReferenceCount);

    [PublicAPI]
    public enum LoadState
    {
      [PublicAPI] Undefined,
      [PublicAPI] Unloaded,
      [PublicAPI] Loading,
      [PublicAPI] Loaded,
      [PublicAPI] Unloading,
    }
  }
}
