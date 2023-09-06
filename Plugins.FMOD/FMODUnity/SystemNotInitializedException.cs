// Decompiled with JetBrains decompiler
// Type: FMODUnity.SystemNotInitializedException
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using FMOD;
using System;

namespace FMODUnity
{
  public class SystemNotInitializedException : Exception
  {
    public readonly RESULT result;
    public readonly string location;

    public SystemNotInitializedException(RESULT result, string location)
      : base("FMOD Studio initialization failed : " + location + " : " + result.ToString() + " : " + Error.String(result))
    {
      this.result = result;
      this.location = location;
    }
  }
}
