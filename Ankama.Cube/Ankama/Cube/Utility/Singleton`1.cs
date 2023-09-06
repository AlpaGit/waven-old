// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.Singleton`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Utility
{
  public class Singleton<T> where T : class, new()
  {
    private static T s_instance;

    public static T instance
    {
      get
      {
        if ((object) Singleton<T>.s_instance == null)
          Singleton<T>.s_instance = new T();
        return Singleton<T>.s_instance;
      }
    }

    public void Destroy()
    {
      if (this != (object) Singleton<T>.s_instance)
        return;
      Singleton<T>.s_instance = default (T);
    }
  }
}
