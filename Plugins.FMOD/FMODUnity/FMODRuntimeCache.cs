// Decompiled with JetBrains decompiler
// Type: FMODUnity.FMODRuntimeCache
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
  public class FMODRuntimeCache : ScriptableObject
  {
    [SerializeField]
    public List<string> bankNameList = new List<string>();
    [SerializeField]
    public FMODRuntimeCache.BankReferenceDictionary bankReferenceDictionary = new FMODRuntimeCache.BankReferenceDictionary();
    [SerializeField]
    public FMODRuntimeCache.EventDefaultBankDictionary eventDefaultBankDictionary = new FMODRuntimeCache.EventDefaultBankDictionary();

    [Serializable]
    public sealed class BankReferenceDictionary : SerializableDictionaryLogic<string, AssetReference>
    {
      [SerializeField]
      private string[] m_keys;
      [SerializeField]
      private AssetReference[] m_values;

      public BankReferenceDictionary()
        : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
      {
      }

      protected override string[] m_keyArray
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

    [Serializable]
    public sealed class EventDefaultBankDictionary : SerializableDictionary<string, int>
    {
      public EventDefaultBankDictionary()
        : base((IEqualityComparer<string>) StringComparer.Ordinal)
      {
      }
    }
  }
}
