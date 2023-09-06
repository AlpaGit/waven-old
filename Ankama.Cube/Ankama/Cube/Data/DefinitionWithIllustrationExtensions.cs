// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DefinitionWithIllustrationExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using DataEditor;
using System;
using System.Collections;

namespace Ankama.Cube.Data
{
  public static class DefinitionWithIllustrationExtensions
  {
    public static IEnumerator LoadIllustrationAsync<T>(
      this EditableData definition,
      string bundleName,
      AssetReference assetReference,
      Action<T, string> onLoaded)
      where T : UnityEngine.Object
    {
      AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
      {
        Log.Error(string.Format("Error while loading bundle '{0}' for {1} {2} error={3}", (object) bundleName, (object) definition.GetType().Name, (object) definition.name, (object) bundleLoadRequest.error), 36, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Definitions\\ICastableDefinition.cs");
        Action<T, string> action = onLoaded;
        if (action != null)
          action(default (T), (string) null);
      }
      else
      {
        AssetLoadRequest<T> assetLoadRequest = assetReference.LoadFromAssetBundleAsync<T>(bundleName);
        while (!assetLoadRequest.isDone)
          yield return (object) null;
        if ((int) assetLoadRequest.error != 0)
        {
          Log.Error(string.Format("Error while loading illustration for {0} {1} error={2}", (object) definition.GetType().Name, (object) definition.name, (object) assetLoadRequest.error), 50, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Definitions\\ICastableDefinition.cs");
          Action<T, string> action = onLoaded;
          if (action != null)
            action(default (T), bundleName);
        }
        else
        {
          Action<T, string> action = onLoaded;
          if (action != null)
            action(assetLoadRequest.asset, bundleName);
        }
      }
    }

    public static void UnloadBundle(this EditableData definition, string bundleName) => AssetManager.UnloadAssetBundle(bundleName);
  }
}
