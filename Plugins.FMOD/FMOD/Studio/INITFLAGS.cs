// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.INITFLAGS
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMOD.Studio
{
  [Flags]
  [PublicAPI]
  public enum INITFLAGS : uint
  {
    NORMAL = 0,
    LIVEUPDATE = 1,
    ALLOW_MISSING_PLUGINS = 2,
    SYNCHRONOUS_UPDATE = 4,
    DEFERRED_CALLBACKS = 8,
    LOAD_FROM_UPDATE = 16, // 0x00000010
  }
}
