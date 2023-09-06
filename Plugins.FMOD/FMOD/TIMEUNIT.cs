// Decompiled with JetBrains decompiler
// Type: FMOD.TIMEUNIT
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  [Flags]
  public enum TIMEUNIT : uint
  {
    MS = 1,
    PCM = 2,
    PCMBYTES = 4,
    RAWBYTES = 8,
    PCMFRACTION = 16, // 0x00000010
    MODORDER = 256, // 0x00000100
    MODROW = 512, // 0x00000200
    MODPATTERN = 1024, // 0x00000400
  }
}
