// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StreamingAssets.StreamingAssetDownloadHandler
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Ankama.AssetManagement.StreamingAssets
{
  internal sealed class StreamingAssetDownloadHandler : DownloadHandlerScript
  {
    private int m_position;
    private byte[] m_data;
    private readonly string m_filePath;
    private FileStream m_fileStream;

    public StreamingAssetDownloadHandler(string filePath, byte[] preallocatedBuffer)
      : base(preallocatedBuffer)
    {
      this.m_filePath = filePath;
      try
      {
        string directoryName = Path.GetDirectoryName(filePath);
        if (directoryName == null)
          return;
        if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        int length = preallocatedBuffer.Length;
        this.m_fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write, length, true);
      }
      catch (Exception ex)
      {
        Debug.LogWarning((object) ("[AssetManager] Could not create patched streaming asset cached file at path '" + filePath + "' because an exception occured: " + ex.Message + "."));
      }
    }

    protected override byte[] GetData() => this.m_data;

    protected override string GetText() => this.m_data != null ? Encoding.UTF8.GetString(this.m_data) : string.Empty;

    protected override float GetProgress()
    {
      if (this.m_data == null)
        return 0.0f;
      int length = this.m_data.Length;
      return length == 0 ? 1f : (float) this.m_position / (float) length;
    }

    protected override void ReceiveContentLength(int contentLength)
    {
      this.m_position = 0;
      this.m_data = new byte[contentLength];
      base.ReceiveContentLength(contentLength);
    }

    protected override bool ReceiveData(byte[] receivedData, int dataLength)
    {
      int position = this.m_position;
      int index = 0;
      while (index < dataLength)
      {
        this.m_data[position] = receivedData[index];
        ++index;
        ++position;
      }
      this.m_position = position;
      if (this.m_fileStream != null)
      {
        try
        {
          this.m_fileStream.Write(receivedData, 0, dataLength);
        }
        catch (Exception ex)
        {
          Debug.LogWarning((object) ("[AssetManager] Could not write to patched streaming asset cached file at path '" + this.m_filePath + "' because an exception occured: " + ex.Message + "."));
          this.AbortFileStream();
        }
      }
      return base.ReceiveData(receivedData, dataLength);
    }

    protected override void CompleteContent()
    {
      if (this.m_fileStream != null)
      {
        this.m_fileStream.Close();
        this.m_fileStream = (FileStream) null;
      }
      base.CompleteContent();
    }

    public void Abort()
    {
      if (this.m_fileStream == null)
        return;
      this.AbortFileStream();
    }

    private void AbortFileStream()
    {
      this.m_fileStream.Close();
      this.m_fileStream = (FileStream) null;
      try
      {
        File.Delete(this.m_filePath);
      }
      catch (Exception ex)
      {
        Debug.LogWarning((object) ("[AssetManager] Could not delete incomplete patched streaming asset cached file at path '" + this.m_filePath + "' because an exception occured: " + ex.Message + "."));
      }
    }
  }
}
