// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioEventGroup
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using FMODUnity;
using System;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  [Serializable]
  public struct AudioEventGroup
  {
    [SerializeField]
    private AudioReferenceWithParameters[] m_sounds;
    [SerializeField]
    private float[] m_stats;
    [NonSerialized]
    private int m_index;

    public bool isValid => this.m_index >= 0;

    public AudioReferenceWithParameters instance => this.m_sounds[this.m_index];

    public void Collapse()
    {
      int length = this.m_sounds.Length;
      if (length <= 1)
      {
        this.m_index = length - 1;
      }
      else
      {
        float num1 = UnityEngine.Random.value;
        float[] stats = this.m_stats;
        int num2 = length - 1;
        for (int index = 0; index < num2; ++index)
        {
          float num3 = stats[index];
          if ((double) num1 < (double) num3)
          {
            this.m_index = index;
            return;
          }
          num1 -= num3;
        }
        this.m_index = num2;
      }
    }
  }
}
