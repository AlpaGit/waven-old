// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleUtility
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public static class AssetBundleUtility
  {
    internal static AssetBundleUtility.SupportedPlatform ToSupportedPlatform(
      this RuntimePlatform platform)
    {
      switch (platform)
      {
        case RuntimePlatform.OSXEditor:
        case RuntimePlatform.OSXPlayer:
          return AssetBundleUtility.SupportedPlatform.OSX;
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.WindowsEditor:
          return AssetBundleUtility.SupportedPlatform.Windows;
        case RuntimePlatform.IPhonePlayer:
          return AssetBundleUtility.SupportedPlatform.IOS;
        case RuntimePlatform.Android:
          return AssetBundleUtility.SupportedPlatform.Android;
        case RuntimePlatform.LinuxPlayer:
        case RuntimePlatform.LinuxEditor:
          return AssetBundleUtility.SupportedPlatform.Linux;
        default:
          throw new ArgumentException(string.Format("[AssetManager] Unsupported runtime platform: {0}", (object) platform), nameof (platform));
      }
    }

    [PublicAPI]
    [NotNull]
    public static string GetBundleNameWithoutVariant([NotNull] string fileName)
    {
      int length = fileName.LastIndexOf('.');
      return length < 0 ? fileName : fileName.Substring(0, length);
    }

    [PublicAPI]
    [NotNull]
    public static string GetBundleNameWithoutVariant([NotNull] string fileName, [NotNull] out string variant)
    {
      int length = fileName.LastIndexOf('.');
      if (length < 0)
      {
        variant = string.Empty;
        return fileName;
      }
      variant = fileName.Substring(length + 1);
      return fileName.Substring(0, length);
    }

    [NotNull]
    internal static string EnsureValidAssetBundleServerURL([NotNull] string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return string.Empty;
      AssetBundleUtility.SupportedPlatform supportedPlatform = Application.platform.ToSupportedPlatform();
      url = url.Replace('\\', '/');
      return url[url.Length - 1] != '/' ? string.Format("{0}/{1}/", (object) url, (object) supportedPlatform) : string.Format("{0}{1}/", (object) url, (object) supportedPlatform);
    }

    [NotNull]
    internal static string EnsureValidPatchedStreamingAssetsServerURL([NotNull] string url)
    {
      if (string.IsNullOrWhiteSpace(url))
        return string.Empty;
      url = url.Replace('\\', '/');
      return url[url.Length - 1] != '/' ? url + "/StreamingAssets/" : url + "StreamingAssets/";
    }

    [PublicAPI]
    public enum SupportedPlatform
    {
      [PublicAPI] Android = 1,
      [PublicAPI] IOS = 8,
      [PublicAPI] Linux = 12, // 0x0000000C
      [PublicAPI] OSX = 15, // 0x0000000F
      [PublicAPI] Windows = 23, // 0x00000017
    }
  }
}
