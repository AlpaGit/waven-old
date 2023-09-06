// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StreamingAssets.StreamingAssetLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;

namespace Ankama.AssetManagement.StreamingAssets
{
  internal abstract class StreamingAssetLoader : IDisposable
  {
    public AssetManagerError error { get; protected set; }

    public abstract float progress { get; }

    public abstract byte[] data { get; }

    public abstract bool Update();

    public abstract void Cancel();

    public abstract void Dispose();
  }
}
