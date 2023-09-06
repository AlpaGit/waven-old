// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.CollectionsExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Concurrent;

namespace Ankama.Cube.Extensions
{
  public static class CollectionsExtensions
  {
    public static void Clear<T>(this ConcurrentQueue<T> queue)
    {
      do
        ;
      while (queue.TryDequeue(out T _));
    }
  }
}
