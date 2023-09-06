// Decompiled with JetBrains decompiler
// Type: FMOD.MEMORY_FREE_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public delegate void MEMORY_FREE_CALLBACK(IntPtr ptr, MEMORY_TYPE type, StringWrapper sourcestr);
}
