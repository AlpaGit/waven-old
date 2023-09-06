// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetManagerError
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine.Networking;

namespace Ankama.AssetManagement
{
  [PublicAPI]
  public struct AssetManagerError
  {
    [PublicAPI]
    public const int None = 0;
    [PublicAPI]
    public const int Definition = 10;
    [PublicAPI]
    public const int Cache = 20;
    [PublicAPI]
    public const int Load = 30;
    [PublicAPI]
    public const int Timeout = 40;
    [PublicAPI]
    public const int Cancelled = 50;
    [PublicAPI]
    public const int InvalidState = 60;
    private const int WebRequest = 1000;
    private const int WebRequestNetwork = 1010;
    private const int WebRequestUnprocessed = 1011;
    private const int WebRequestPosUnknown = 1001;
    private const int WebRequestNegUnknown = 1099;
    private const int WebRequestMax = 1999;
    [PublicAPI]
    public readonly int code;
    [PublicAPI]
    public readonly string message;

    [PublicAPI]
    public AssetManagerError(int code)
    {
      this.code = code;
      this.message = string.Empty;
    }

    [PublicAPI]
    public AssetManagerError(int code, string message)
    {
      this.code = code;
      this.message = message;
    }

    internal static AssetManagerError WebRequestError(UnityWebRequest webRequest)
    {
      int code;
      if (webRequest.isHttpError)
      {
        long responseCode = webRequest.responseCode;
        code = responseCode != 0L ? (responseCode < 100L || responseCode >= 600L ? (responseCode >= 0L ? 1001 : (responseCode == -1L ? 1011 : 1099)) : 1000 + (int) responseCode) : 1000;
      }
      else
        code = !webRequest.isNetworkError ? 0 : 1010;
      string message = webRequest.error ?? string.Empty;
      return new AssetManagerError(code, message);
    }

    [PublicAPI]
    public static implicit operator int(AssetManagerError e) => e.code;

    [PublicAPI]
    public static implicit operator AssetManagerError(int code) => new AssetManagerError(code);

    [PublicAPI]
    public override string ToString() => string.IsNullOrEmpty(this.message) ? this.code.ToString() : string.Format("{0} ({1})", (object) this.message, (object) this.code);

    [PublicAPI]
    public bool isWebRequestError => this.code >= 1000 && this.code <= 1999;
  }
}
