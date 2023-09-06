// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.SYSTEM_CALLBACK_TYPE
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMOD.Studio
{
  [Flags]
  [PublicAPI]
  public enum SYSTEM_CALLBACK_TYPE : uint
  {
    PREUPDATE = 1,
    POSTUPDATE = 2,
    BANK_UNLOAD = 4,
    ALL = 4294967295, // 0xFFFFFFFF
  }
}
