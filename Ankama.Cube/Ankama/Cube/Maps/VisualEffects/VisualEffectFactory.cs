// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.VisualEffectFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  public static class VisualEffectFactory
  {
    private const int InitialCapacity = 32;
    private static PrefabInstancePool s_pool;

    public static void Initialize()
    {
      if (VisualEffectFactory.s_pool != null)
      {
        Log.Error("Initialize called but Dispose was not properly called since last call to Initialize.", 20, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\VisualEffects\\VisualEffectFactory.cs");
        VisualEffectFactory.s_pool.Dispose();
      }
      VisualEffectFactory.s_pool = new PrefabInstancePool(32);
    }

    public static void Dispose()
    {
      if (VisualEffectFactory.s_pool == null)
      {
        Log.Warning("Dispose called but Initialize was not properly called.", 33, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\VisualEffects\\VisualEffectFactory.cs");
      }
      else
      {
        VisualEffectFactory.s_pool.Dispose();
        VisualEffectFactory.s_pool = (PrefabInstancePool) null;
      }
    }

    public static void PreparePool([NotNull] GameObject prefab, int capacity = 2, int maxSize = 4)
    {
      if (VisualEffectFactory.s_pool == null)
        return;
      VisualEffectFactory.s_pool.PreparePool(prefab, capacity, maxSize);
    }

    [NotNull]
    public static VisualEffect Instantiate(
      [NotNull] VisualEffect prefab,
      Vector3 position,
      Quaternion rotation,
      Vector3 scale,
      [CanBeNull] Transform parent)
    {
      GameObject gameObject = VisualEffectFactory.s_pool != null ? VisualEffectFactory.s_pool.Instantiate(prefab.gameObject, position, rotation, parent) : ((Object) null == (Object) parent ? Object.Instantiate<GameObject>(prefab.gameObject, position, rotation) : Object.Instantiate<GameObject>(prefab.gameObject, position, rotation, parent));
      Transform transform = gameObject.transform;
      Vector3 localScale = transform.localScale;
      localScale.Scale(scale);
      transform.localScale = localScale;
      return gameObject.GetComponent<VisualEffect>();
    }

    public static void Release([NotNull] VisualEffect prefab, [NotNull] VisualEffect instance)
    {
      if (VisualEffectFactory.s_pool == null)
        Object.Destroy((Object) instance.gameObject);
      else
        VisualEffectFactory.s_pool.Release(prefab.gameObject, instance.gameObject);
    }
  }
}
