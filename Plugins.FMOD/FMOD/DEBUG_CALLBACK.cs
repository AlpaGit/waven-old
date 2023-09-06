// Decompiled with JetBrains decompiler
// Type: FMOD.DEBUG_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

namespace FMOD
{
  public delegate RESULT DEBUG_CALLBACK(
    DEBUG_FLAGS flags,
    StringWrapper file,
    int line,
    StringWrapper func,
    StringWrapper message);
}
