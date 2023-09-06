// Decompiled with JetBrains decompiler
// Type: FMOD.SYSTEM_CALLBACK_TYPE
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  [Flags]
  public enum SYSTEM_CALLBACK_TYPE : uint
  {
    DEVICELISTCHANGED = 1,
    DEVICELOST = 2,
    MEMORYALLOCATIONFAILED = 4,
    THREADCREATED = 8,
    BADDSPCONNECTION = 16, // 0x00000010
    PREMIX = 32, // 0x00000020
    POSTMIX = 64, // 0x00000040
    ERROR = 128, // 0x00000080
    MIDMIX = 256, // 0x00000100
    THREADDESTROYED = 512, // 0x00000200
    PREUPDATE = 1024, // 0x00000400
    POSTUPDATE = 2048, // 0x00000800
    RECORDLISTCHANGED = 4096, // 0x00001000
    ALL = 4294967295, // 0xFFFFFFFF
  }
}
