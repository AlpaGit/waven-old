// Decompiled with JetBrains decompiler
// Type: FMODUnity.AudioEventReferenceAttribute
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using UnityEngine;

namespace FMODUnity
{
  public class AudioEventReferenceAttribute : PropertyAttribute
  {
    public readonly AudioEventReferenceType propertyType;

    public AudioEventReferenceAttribute(AudioEventReferenceType propertyType = AudioEventReferenceType.Path) => this.propertyType = propertyType;
  }
}
