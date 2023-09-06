// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_LOG_FUNC
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

namespace FMOD
{
  public delegate void DSP_LOG_FUNC(
    DEBUG_FLAGS level,
    StringWrapper file,
    int line,
    StringWrapper function,
    StringWrapper format);
}
