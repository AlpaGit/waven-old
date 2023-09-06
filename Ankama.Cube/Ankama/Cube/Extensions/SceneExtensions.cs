// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.SceneExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.Cube.Extensions
{
  public static class SceneExtensions
  {
    public static T GetComponentInRootsChildren<T>(this Scene scene) where T : MonoBehaviour
    {
      GameObject[] rootGameObjects = scene.GetRootGameObjects();
      int length = rootGameObjects.Length;
      for (int index = 0; index < length; ++index)
      {
        T componentInChildren = rootGameObjects[index].GetComponentInChildren<T>();
        if ((Object) componentInChildren != (Object) null)
          return componentInChildren;
      }
      return default (T);
    }
  }
}
