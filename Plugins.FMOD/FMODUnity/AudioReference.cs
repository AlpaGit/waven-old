// Decompiled with JetBrains decompiler
// Type: FMODUnity.AudioReference
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace FMODUnity
{
  [Serializable]
  public struct AudioReference : IEquatable<AudioReference>, IEquatable<AudioReferenceWithParameters>
  {
    [UsedImplicitly]
    [SerializeField]
    [AudioEventReference(AudioEventReferenceType.Guid)]
    private string m_eventGuid;

    public AudioReference(string eventGuid) => this.m_eventGuid = eventGuid;

    public bool isValid => !string.IsNullOrEmpty(this.m_eventGuid);

    public Guid eventGuid => !string.IsNullOrEmpty(this.m_eventGuid) ? Guid.ParseExact(this.m_eventGuid, "N") : Guid.Empty;

    public string eventGuidString => this.m_eventGuid;

    public bool Equals(AudioReference other) => string.Equals(this.m_eventGuid, other.m_eventGuid);

    public bool Equals(AudioReferenceWithParameters other) => string.Equals(this.m_eventGuid, other.eventGuidString);

    public override bool Equals(object obj) => obj != null && obj is AudioReference other && this.Equals(other);

    public override int GetHashCode()
    {
      string eventGuid = this.m_eventGuid;
      return eventGuid == null ? 0 : eventGuid.GetHashCode();
    }
  }
}
