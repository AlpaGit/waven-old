// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.LOAD_BANK_FLAGS
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMOD.Studio
{
  [Flags]
  [PublicAPI]
  public enum LOAD_BANK_FLAGS : uint
  {
    NORMAL = 0,
    NONBLOCKING = 1,
    DECOMPRESS_SAMPLES = 2,
  }
}
