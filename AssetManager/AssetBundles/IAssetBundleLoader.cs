// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.IAssetBundleLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;

namespace Ankama.AssetManagement.AssetBundles
{
  internal interface IAssetBundleLoader : IDisposable
  {
    bool isDone { get; }

    float progress { get; }

    UnityEngine.AssetBundle assetBundle { get; }

    AssetManagerError error { get; }

    void Cancel();
  }
}
