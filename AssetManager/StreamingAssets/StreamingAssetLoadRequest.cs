// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StreamingAssets.StreamingAssetLoadRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System.IO;
using System.Text;
using UnityEngine;

namespace Ankama.AssetManagement.StreamingAssets
{
  [PublicAPI]
  public sealed class StreamingAssetLoadRequest : CustomYieldInstruction
  {
    [PublicAPI]
    public readonly string assetPath;
    [PublicAPI]
    public readonly string filePath;
    private StreamingAssetLoader m_loader;

    [PublicAPI]
    public bool isDone { get; private set; }

    [PublicAPI]
    public AssetManagerError error { get; private set; }

    [PublicAPI]
    public float progress => this.m_loader != null ? this.m_loader.progress : 1f;

    public override bool keepWaiting => !this.isDone;

    [PublicAPI]
    [CanBeNull]
    public byte[] data { get; private set; }

    [PublicAPI]
    [NotNull]
    public string text => this.data != null ? Encoding.UTF8.GetString(this.data) : string.Empty;

    internal StreamingAssetLoadRequest(string assetPath, string filePath)
    {
      this.assetPath = assetPath;
      this.filePath = filePath;
      this.m_loader = (StreamingAssetLoader) new StreamingAssetArchiveLoader(filePath);
    }

    internal StreamingAssetLoadRequest(
      string assetPath,
      string filePath,
      int bufferSize,
      bool allowPatching)
    {
      this.assetPath = assetPath;
      this.filePath = filePath;
      if (File.Exists(filePath))
        this.m_loader = (StreamingAssetLoader) new StreamingAssetFileLoader(filePath, bufferSize);
      else if (allowPatching)
      {
        this.m_loader = (StreamingAssetLoader) new StreamingAssetCachingLoader(assetPath, filePath, bufferSize);
      }
      else
      {
        this.error = (AssetManagerError) 10;
        this.isDone = true;
      }
    }

    internal StreamingAssetLoadRequest(string assetPath, string filePath, AssetManagerError error)
    {
      this.assetPath = assetPath;
      this.filePath = filePath;
      this.error = error;
      this.isDone = true;
    }

    internal StreamingAssetLoadRequest(string assetPath, AssetManagerError error)
    {
      this.assetPath = assetPath;
      this.error = error;
      this.filePath = string.Empty;
      this.isDone = true;
    }

    internal bool Update()
    {
      if (this.isDone)
        return true;
      if (!this.m_loader.Update())
        return false;
      this.data = this.m_loader.data;
      this.error = this.m_loader.error;
      this.m_loader.Dispose();
      this.m_loader = (StreamingAssetLoader) null;
      this.isDone = true;
      return true;
    }

    [PublicAPI]
    public bool Cancel()
    {
      if (this.isDone)
        return false;
      this.error = (AssetManagerError) 50;
      this.isDone = true;
      if (this.m_loader != null)
      {
        this.m_loader.Cancel();
        this.m_loader.Dispose();
        this.m_loader = (StreamingAssetLoader) null;
      }
      return true;
    }
  }
}
