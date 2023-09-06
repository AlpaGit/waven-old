// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.CPU_USAGE
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct CPU_USAGE
  {
    public float dspusage;
    public float streamusage;
    public float geometryusage;
    public float updateusage;
    public float studiousage;
  }
}
