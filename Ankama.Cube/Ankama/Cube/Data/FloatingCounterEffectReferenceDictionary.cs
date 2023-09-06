﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloatingCounterEffectReferenceDictionary
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public class FloatingCounterEffectReferenceDictionary : 
    SerializableDictionaryLogic<CaracId, AssetReference>
  {
    [SerializeField]
    private CaracId[] m_keys = new CaracId[0];
    [SerializeField]
    private AssetReference[] m_values = new AssetReference[0];

    protected override CaracId[] m_keyArray
    {
      get => this.m_keys;
      set => this.m_keys = value;
    }

    protected override AssetReference[] m_valueArray
    {
      get => this.m_values;
      set => this.m_values = value;
    }
  }
}
