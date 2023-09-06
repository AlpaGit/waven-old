// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.ObjectPool`1
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Ankama.Utilities
{
  [PublicAPI]
  public sealed class ObjectPool<T> where T : new()
  {
    private readonly Stack<T> m_stack;
    private int m_maxSize;

    public int maxSize
    {
      get => this.m_maxSize;
      set
      {
        value = Math.Max(0, value);
        if (value == this.m_maxSize)
          return;
        if (value > 0)
        {
          while (this.m_stack.Count > value)
            this.m_stack.Pop();
        }
        this.m_maxSize = value;
      }
    }

    [PublicAPI]
    public ObjectPool() => this.m_stack = new Stack<T>();

    [PublicAPI]
    public ObjectPool(int capacity) => this.m_stack = new Stack<T>(capacity);

    [PublicAPI]
    public ObjectPool(int capacity, int maxSize)
    {
      this.m_stack = new Stack<T>(capacity);
      this.m_maxSize = Math.Max(0, maxSize);
    }

    [PublicAPI]
    public T Get() => this.m_stack.Count <= 0 ? new T() : this.m_stack.Pop();

    [PublicAPI]
    public bool Release(T element)
    {
      if (this.m_maxSize != 0 && this.m_stack.Count >= this.m_maxSize)
        return false;
      this.m_stack.Push(element);
      return true;
    }

    [PublicAPI]
    public int Count() => this.m_stack.Count;

    [PublicAPI]
    public bool Contains(T element) => this.m_stack.Contains(element);

    [PublicAPI]
    public void Clear() => this.m_stack.Clear();
  }
}
