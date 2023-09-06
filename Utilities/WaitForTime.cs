﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.WaitForTime
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public sealed class WaitForTime : IEnumerator
  {
    private readonly float m_wait;
    private float m_start;

    public WaitForTime(float seconds)
    {
      this.m_wait = seconds;
      this.m_start = Time.time;
    }

    public object Current => (object) null;

    public bool MoveNext() => (double) Time.time - (double) this.m_start < (double) this.m_wait;

    public void Reset() => this.m_start = Time.time;
  }
}
