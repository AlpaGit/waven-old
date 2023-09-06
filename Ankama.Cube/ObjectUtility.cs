// Decompiled with JetBrains decompiler
// Type: ObjectUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

public static class ObjectUtility
{
  public static void Destroy(Object o)
  {
    if ((Object) null == o)
      return;
    Object.Destroy(o);
  }
}
