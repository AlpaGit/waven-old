// Decompiled with JetBrains decompiler
// Type: FMOD.ERRORCALLBACK_INFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public struct ERRORCALLBACK_INFO
  {
    public RESULT result;
    public ERRORCALLBACK_INSTANCETYPE instancetype;
    public IntPtr instance;
    public StringWrapper functionname;
    public StringWrapper functionparams;
  }
}
