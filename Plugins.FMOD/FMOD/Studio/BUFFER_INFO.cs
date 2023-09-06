// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.BUFFER_INFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct BUFFER_INFO
  {
    public int currentusage;
    public int peakusage;
    public int capacity;
    public int stallcount;
    public float stalltime;
  }
}
