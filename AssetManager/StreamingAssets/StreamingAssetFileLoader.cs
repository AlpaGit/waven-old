// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StreamingAssets.StreamingAssetFileLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Ankama.AssetManagement.StreamingAssets
{
  internal sealed class StreamingAssetFileLoader : StreamingAssetLoader
  {
    private byte[] m_data;
    private Task m_task;
    private CancellationTokenSource m_cancellationTokenSource;

    public override float progress => !this.m_task.IsCompleted ? 0.0f : 1f;

    public override byte[] data => this.m_data;

    public StreamingAssetFileLoader(string filePath, int bufferSize)
    {
      if (bufferSize <= 0)
        throw new ArgumentException("bufferSize must be greater than zero.", nameof (bufferSize));
      this.m_cancellationTokenSource = new CancellationTokenSource();
      this.m_task = this.ReadContentFromFile(filePath, bufferSize, this.m_cancellationTokenSource.Token);
    }

    private async Task ReadContentFromFile(
      string filePath,
      int bufferSize,
      CancellationToken cancellationToken)
    {
      StreamingAssetFileLoader streamingAssetFileLoader = this;
      try
      {
        byte[] result;
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
        {
          long length = fileStream.Length;
          int count = length <= (long) int.MaxValue ? (int) length : throw new Exception(string.Format("[AssetManager] Tried to load a file at path '{0}' that is too large: {1}", (object) filePath, (object) length));
          result = new byte[count];
          int num = await fileStream.ReadAsync(result, 0, count, cancellationToken);
        }
        if (!cancellationToken.IsCancellationRequested)
          streamingAssetFileLoader.m_data = result;
        result = (byte[]) null;
      }
      catch (Exception ex)
      {
        streamingAssetFileLoader.error = (AssetManagerError) 30;
        Debug.LogError((object) ("[AssetManager] Error occured while reading the file at path '" + filePath + "': " + ex.Message));
      }
    }

    public override bool Update() => this.m_task.IsCompleted;

    public override void Cancel()
    {
      if (this.m_task.IsCompleted || this.m_cancellationTokenSource.IsCancellationRequested)
        return;
      this.error = (AssetManagerError) 50;
      this.m_cancellationTokenSource.Cancel();
    }

    public override void Dispose()
    {
      if (this.m_cancellationTokenSource != null)
      {
        this.m_cancellationTokenSource.Dispose();
        this.m_cancellationTokenSource = (CancellationTokenSource) null;
      }
      if (this.m_task == null)
        return;
      this.m_task.Dispose();
      this.m_task = (Task) null;
    }
  }
}
