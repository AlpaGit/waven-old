// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.CacheManager
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.StreamingAssets;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ankama.AssetManagement
{
  [PublicAPI]
  public static class CacheManager
  {
    [NotNull]
    internal static string streamingAssetsCachePath = string.Empty;

    [PublicAPI]
    public static bool isInitialized { get; private set; }

    internal static void Initialize()
    {
      if (!Application.isMobilePlatform)
        CacheManager.CreateStandaloneBundleCache();
      CacheManager.CreateStreamingAssetsCacheFolder();
      Cache cache = Caching.defaultCache;
      Debug.Log((object) ("[CacheManager] Default cache location: " + cache.path));
      cache = Caching.currentCacheForWriting;
      Debug.Log((object) ("[CacheManager] Current cache location: " + cache.path));
      Debug.Log((object) ("[CacheManager] Streaming assets cache location: " + CacheManager.streamingAssetsCachePath));
      CacheManager.isInitialized = true;
    }

    [PublicAPI]
    public static void RemoveObsoleteBundlesFromCache()
    {
      if (!AssetManager.isReady)
      {
        Debug.LogError((object) "[CacheManager] CacheManager.RemoveObsoleteBundlesFromCache cannot be called before the AssetManager is ready.");
      }
      else
      {
        HashSet<string> expectedFolders = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
        List<Hash128> outCachedVersions = new List<Hash128>();
        foreach (AssetBundleLoadInfo assetBundleLoadInfo in AssetManager.assetBundleLoadInfos.Values)
        {
          string name = assetBundleLoadInfo.name;
          Caching.GetCachedVersions(name, outCachedVersions);
          int count = outCachedVersions.Count;
          for (int index = 0; index < count; ++index)
          {
            Hash128 hash = outCachedVersions[index];
            if (assetBundleLoadInfo.AllowCachedFileWithHash(hash))
              expectedFolders.Add(string.Format("{0}/{1}", (object) name, (object) hash));
            else if (Caching.ClearCachedVersion(name, hash))
              Debug.Log((object) string.Format("[CacheManager] Removed obsolete bundle file from cache: {0} ({1}).", (object) name, (object) hash));
            else
              Debug.LogError((object) string.Format("[CacheManager] Could not remove obsolete bundle file from cache: {0} ({1}).", (object) name, (object) hash));
          }
        }
        int cacheCount = Caching.cacheCount;
        for (int cacheIndex = 0; cacheIndex < cacheCount; ++cacheIndex)
        {
          Cache cacheAt = Caching.GetCacheAt(cacheIndex);
          if (cacheAt.valid && !cacheAt.readOnly)
          {
            string path = cacheAt.path;
            if (!Directory.Exists(path))
              break;
            try
            {
              int length = path.Length;
              foreach (string enumerateDirectory in Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly))
              {
                if (!CacheManager.RemoveObsoleteDirectoriesFromCache(enumerateDirectory, expectedFolders, length))
                {
                  string str = enumerateDirectory.Substring(length + 1).Replace('\\', '/');
                  if (!expectedFolders.Contains(str))
                  {
                    try
                    {
                      Directory.Delete(enumerateDirectory);
                      Debug.Log((object) ("[CacheManager] Removed obsolete bundle directory from cache: " + enumerateDirectory + "."));
                    }
                    catch (Exception ex)
                    {
                      Debug.LogError((object) ("[CacheManager] An exception occurred while deleting directory at path '" + enumerateDirectory + "': " + ex.Message + ")."));
                    }
                  }
                }
              }
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ("[CacheManager] An exception occurred while enumerating directories of path '" + path + "': " + ex.Message + ")."));
            }
          }
        }
      }
    }

    [PublicAPI]
    public static void RemoveObsoletePatchedStreamingAssetsFromCache()
    {
      if (!AssetManager.isReady)
      {
        Debug.LogError((object) "[CacheManager] CacheManager.RemoveObsoletePatchedStreamingAssetsFromCache cannot be called before the AssetManager is ready.");
      }
      else
      {
        if (!Directory.Exists(CacheManager.streamingAssetsCachePath))
          return;
        HashSet<string> expectedFiles = new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal);
        foreach (StreamingAssetIdentifier streamingAssetIdentifier in AssetManager.patchedStreamingAssets.Values)
        {
          string str = Path.Combine(CacheManager.streamingAssetsCachePath, streamingAssetIdentifier.guid, streamingAssetIdentifier.md5).Replace('\\', '/');
          expectedFiles.Add(str);
        }
        try
        {
          int length = CacheManager.streamingAssetsCachePath.Length;
          foreach (string enumerateDirectory in Directory.EnumerateDirectories(CacheManager.streamingAssetsCachePath, "*", SearchOption.TopDirectoryOnly))
          {
            if (!CacheManager.RemoveObsoleteFilesFromCache(enumerateDirectory, expectedFiles, length))
            {
              try
              {
                Directory.Delete(enumerateDirectory);
                Debug.Log((object) ("[CacheManager] Removed obsolete patched streaming assets directory from cache: " + enumerateDirectory + "."));
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ("[CacheManager] An exception occurred while deleting directory at path '" + enumerateDirectory + "': " + ex.Message + ")."));
              }
            }
          }
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ("[CacheManager] An exception occurred while enumerating directories of path '" + CacheManager.streamingAssetsCachePath + "': " + ex.Message + ")."));
        }
      }
    }

    private static bool RemoveObsoleteDirectoriesFromCache(
      string directory,
      HashSet<string> expectedFolders,
      int cachePathLength)
    {
      bool flag = false;
      try
      {
        foreach (string enumerateDirectory in Directory.EnumerateDirectories(directory, "*", SearchOption.TopDirectoryOnly))
        {
          if (CacheManager.RemoveObsoleteDirectoriesFromCache(enumerateDirectory, expectedFolders, cachePathLength))
          {
            flag = true;
          }
          else
          {
            string assetBundleName = enumerateDirectory.Substring(cachePathLength + 1).Replace('\\', '/');
            if (expectedFolders.Contains(assetBundleName))
            {
              flag = true;
            }
            else
            {
              if (Caching.ClearAllCachedVersions(assetBundleName))
                Debug.Log((object) ("[CacheManager] Removed obsolete bundle files from cache: " + assetBundleName + "."));
              else
                Debug.LogError((object) ("[CacheManager] Could not remove obsolete bundle files from cache: " + enumerateDirectory + "."));
              try
              {
                Directory.Delete(enumerateDirectory);
                Debug.Log((object) ("[CacheManager] Removed obsolete bundle directory from cache: " + assetBundleName + "."));
              }
              catch (Exception ex)
              {
                Debug.LogError((object) ("[CacheManager] Could not delete obsolete bundle directory of path '" + enumerateDirectory + "': " + ex.Message + ")."));
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("[CacheManager] An exception occurred while enumerating directories of path '" + directory + "': " + ex.Message + ")."));
      }
      return flag;
    }

    private static bool RemoveObsoleteFilesFromCache(
      string directory,
      HashSet<string> expectedFiles,
      int cachePathLength)
    {
      bool flag = false;
      try
      {
        foreach (string enumerateDirectory in Directory.EnumerateDirectories(directory, "*", SearchOption.TopDirectoryOnly))
        {
          if (CacheManager.RemoveObsoleteDirectoriesFromCache(enumerateDirectory, expectedFiles, cachePathLength))
            flag = true;
        }
        foreach (string enumerateFile in Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly))
        {
          string str = enumerateFile.Substring(cachePathLength + 1).Replace('\\', '/');
          if (expectedFiles.Contains(str))
          {
            flag = true;
          }
          else
          {
            try
            {
              File.Delete(enumerateFile);
              Debug.Log((object) ("[CacheManager] Removed obsolete patched streaming asset from cache: " + str + "."));
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ("[CacheManager] Could not remove obsolete patched streaming asset from cache at path " + enumerateFile + ": " + ex.Message));
            }
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("[CacheManager] An exception occurred while enumerating directories of path '" + directory + "': " + ex.Message + ")."));
      }
      return flag;
    }

    private static void CreateStandaloneBundleCache()
    {
      string[] strArray;
      switch (Application.platform)
      {
        case RuntimePlatform.OSXPlayer:
          strArray = new string[2]
          {
            Path.Combine(Application.dataPath, "Cache/AssetBundles"),
            Path.Combine(Application.persistentDataPath, "Cache/AssetBundles")
          };
          break;
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.LinuxPlayer:
          string dataPath = Application.dataPath;
          strArray = new string[2]
          {
            dataPath.Substring(0, dataPath.Length - "Data".Length) + "Cache/AssetBundles",
            Path.Combine(Application.persistentDataPath, "Cache/AssetBundles")
          };
          break;
        case RuntimePlatform.IPhonePlayer:
        case RuntimePlatform.Android:
          strArray = new string[1]
          {
            Path.Combine(Application.persistentDataPath, "Cache/AssetBundles")
          };
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
      {
        string str = strArray[index];
        if (!Directory.Exists(str))
        {
          try
          {
            Directory.CreateDirectory(str);
          }
          catch (UnauthorizedAccessException ex)
          {
            if (index == length - 1)
            {
              Debug.LogWarning((object) ("[CacheManager] Could not create custom cache at path '" + str + "': unauthorized."));
              continue;
            }
            continue;
          }
          catch (Exception ex)
          {
            if (index == length - 1)
            {
              Debug.LogError((object) ("[CacheManager] Could not create custom cache at path '" + str + "': " + ex.Message + "."));
              continue;
            }
            continue;
          }
        }
        Cache cache = Caching.AddCache(str);
        if (!cache.valid)
        {
          Caching.RemoveCache(cache);
          if (index == length - 1)
            Debug.LogError((object) ("[CacheManager] Could not add custom cache at path '" + str + "': invalid."));
        }
        else if (cache.readOnly)
        {
          Caching.RemoveCache(cache);
          if (index == length - 1)
            Debug.LogError((object) ("[CacheManager] Could not add custom cache at path '" + str + "': read only."));
        }
        else
        {
          Caching.MoveCacheBefore(cache, Caching.defaultCache);
          Caching.currentCacheForWriting = cache;
          break;
        }
      }
    }

    private static void CreateStreamingAssetsCacheFolder()
    {
      string[] strArray;
      switch (Application.platform)
      {
        case RuntimePlatform.OSXPlayer:
          strArray = new string[2]
          {
            Path.Combine(Application.dataPath, "Cache/StreamingAssets"),
            Path.Combine(Application.persistentDataPath, "Cache/StreamingAssets")
          };
          break;
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.LinuxPlayer:
          string dataPath = Application.dataPath;
          strArray = new string[2]
          {
            dataPath.Substring(0, dataPath.Length - "Data".Length) + "Cache/StreamingAssets",
            Path.Combine(Application.persistentDataPath, "Cache/StreamingAssets")
          };
          break;
        case RuntimePlatform.IPhonePlayer:
        case RuntimePlatform.Android:
          strArray = new string[1]
          {
            Path.Combine(Application.persistentDataPath, "Cache/StreamingAssets")
          };
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      int length = strArray.Length;
      for (int index = 0; index < length; ++index)
      {
        string path = strArray[index];
        if (!Directory.Exists(path))
        {
          try
          {
            Directory.CreateDirectory(path);
          }
          catch (UnauthorizedAccessException ex)
          {
            if (index == length - 1)
            {
              Debug.LogWarning((object) ("[CacheManager] Could not create custom cache at path '" + path + "': unauthorized."));
              continue;
            }
            continue;
          }
          catch (Exception ex)
          {
            if (index == length - 1)
            {
              Debug.LogError((object) ("[CacheManager] Could not created custom cache at path '" + path + "': " + ex.Message + "."));
              continue;
            }
            continue;
          }
        }
        CacheManager.streamingAssetsCachePath = path;
        break;
      }
    }
  }
}
