// Decompiled with JetBrains decompiler
// Type: FMODUnity.AudioReferenceWithParameters
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using FMOD.Studio;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace FMODUnity
{
  [Serializable]
  public struct AudioReferenceWithParameters : 
    IEquatable<AudioReferenceWithParameters>,
    IEquatable<AudioReference>
  {
    [UsedImplicitly]
    [SerializeField]
    [AudioEventReference(AudioEventReferenceType.Guid)]
    private string m_eventGuid;
    [SerializeField]
    private string[] m_parameterKeys;
    [SerializeField]
    private float[] m_parameterValues;

    public bool isValid => !string.IsNullOrEmpty(this.m_eventGuid);

    public Guid eventGuid => !string.IsNullOrEmpty(this.m_eventGuid) ? Guid.ParseExact(this.m_eventGuid, "N") : Guid.Empty;

    public string eventGuidString => this.m_eventGuid;

    public bool Equals(AudioReference other) => string.Equals(this.m_eventGuid, other.eventGuidString);

    public bool Equals(AudioReferenceWithParameters other) => string.Equals(this.m_eventGuid, other.m_eventGuid);

    public override bool Equals(object obj) => obj != null && obj is AudioReferenceWithParameters other && this.Equals(other);

    public override int GetHashCode()
    {
      string eventGuid = this.m_eventGuid;
      return eventGuid == null ? 0 : eventGuid.GetHashCode();
    }

    public bool TryGetParameterValue([NotNull] string key, out float value)
    {
      string[] parameterKeys = this.m_parameterKeys;
      int length = parameterKeys.Length;
      for (int index = 0; index < length; ++index)
      {
        if (parameterKeys[index].Equals(key))
        {
          value = this.m_parameterValues[index];
          return true;
        }
      }
      value = 0.0f;
      return false;
    }

    public void ApplyParameters(EventInstance eventInstance)
    {
      string[] parameterKeys = this.m_parameterKeys;
      float[] parameterValues = this.m_parameterValues;
      int length = parameterKeys.Length;
      for (int index = 0; index < length; ++index)
      {
        int num = (int) eventInstance.setParameterValue(parameterKeys[index], parameterValues[index]);
      }
    }

    public void ApplyAllParameters(EventInstance eventInstance)
    {
      string[] parameterKeys = this.m_parameterKeys;
      float[] parameterValues = this.m_parameterValues;
      int length = parameterKeys.Length;
      int count;
      int parameterCount = (int) eventInstance.getParameterCount(out count);
label_8:
      for (int index1 = 0; index1 < count; ++index1)
      {
        ParameterInstance instance;
        int parameterByIndex = (int) eventInstance.getParameterByIndex(index1, out instance);
        PARAMETER_DESCRIPTION description1;
        int description2 = (int) instance.getDescription(out description1);
        string name = (string) description1.name;
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (name.Equals(parameterKeys[index2]))
          {
            int num = (int) instance.setValue(parameterValues[index2]);
            goto label_8;
          }
        }
        int num1 = (int) instance.setValue(description1.defaultvalue);
      }
    }

    public static implicit operator AudioReference(
      AudioReferenceWithParameters audioReferenceWithParameters)
    {
      return new AudioReference(audioReferenceWithParameters.m_eventGuid);
    }
  }
}
