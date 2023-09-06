// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenModuleUtils
// Assembly: Plugins.DOTween, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FF25450-B39C-42C8-B3DB-BB3A40E2DA5A
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.DOTween.dll

using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace DG.Tweening
{
  public static class DOTweenModuleUtils
  {
    private static bool _initialized;

    [Preserve]
    public static void Init()
    {
      if (DOTweenModuleUtils._initialized)
        return;
      DOTweenModuleUtils._initialized = true;
      DOTweenExternalCommand.SetOrientationOnPath += new Action<PathOptions, Tween, Quaternion, Transform>(DOTweenModuleUtils.Physics.SetOrientationOnPath);
    }

    [Preserve]
    private static void Preserver()
    {
      AppDomain.CurrentDomain.GetAssemblies();
      typeof (MonoBehaviour).GetMethod("Stub");
    }

    public static class Physics
    {
      public static void SetOrientationOnPath(
        PathOptions options,
        Tween t,
        Quaternion newRot,
        Transform trans)
      {
        trans.rotation = newRot;
      }

      public static bool HasRigidbody2D(Component target) => false;

      [Preserve]
      public static bool HasRigidbody(Component target) => false;

      [Preserve]
      public static TweenerCore<Vector3, Path, PathOptions> CreateDOTweenPathTween(
        MonoBehaviour target,
        bool tweenRigidbody,
        bool isLocal,
        Path path,
        float duration,
        PathMode pathMode)
      {
        return !isLocal ? target.transform.DOPath(path, duration, pathMode) : target.transform.DOLocalPath(path, duration, pathMode);
      }
    }
  }
}
