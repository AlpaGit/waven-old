// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.GameObjectExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class GameObjectExtensions
  {
    public static Canvas GetRootCanvas(this GameObject go)
    {
      List<Canvas> canvasList = ListPool<Canvas>.Get();
      go.GetComponentsInParent<Canvas>(false, canvasList);
      Canvas rootCanvas = (Canvas) null;
      if (canvasList.Count != 0)
        rootCanvas = canvasList[0];
      ListPool<Canvas>.Release(canvasList);
      return rootCanvas;
    }

    public static void SetHideFlagsRecursively(this GameObject gameObject, HideFlags value)
    {
      gameObject.hideFlags = value;
      Transform transform = gameObject.transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
        transform.GetChild(index).gameObject.SetHideFlagsRecursively(value);
    }

    public static void SetLayerRecursively([NotNull] this GameObject gameObject, int value)
    {
      gameObject.layer = value;
      Transform transform = gameObject.transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
        transform.GetChild(index).gameObject.SetLayerRecursively(value);
    }
  }
}
