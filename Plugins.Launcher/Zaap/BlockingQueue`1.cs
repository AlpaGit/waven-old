// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Zaap.BlockingQueue`1
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using System.Collections.Generic;
using System.Threading;

namespace Ankama.Launcher.Zaap
{
  internal class BlockingQueue<T>
  {
    private readonly Queue<T> m_queue = new Queue<T>();
    private bool m_closing;

    public void Enqueue(T item)
    {
      lock (this.m_queue)
      {
        this.m_queue.Enqueue(item);
        Monitor.PulseAll((object) this.m_queue);
      }
    }

    public bool TryDequeue(out T value)
    {
      lock (this.m_queue)
      {
        while (this.m_queue.Count == 0)
        {
          if (this.m_closing)
          {
            value = default (T);
            return false;
          }
          Monitor.Wait((object) this.m_queue);
        }
        value = this.m_queue.Dequeue();
        return true;
      }
    }

    public T Dequeue()
    {
      lock (this.m_queue)
      {
        while (this.m_queue.Count == 0)
          Monitor.Wait((object) this.m_queue);
        return this.m_queue.Dequeue();
      }
    }

    public void Close()
    {
      lock (this.m_queue)
      {
        this.m_closing = true;
        Monitor.PulseAll((object) this.m_queue);
      }
    }
  }
}
