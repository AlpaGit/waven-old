// Decompiled with JetBrains decompiler
// Type: FMOD.ASYNCREADINFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public struct ASYNCREADINFO
  {
    public IntPtr handle;
    public uint offset;
    public uint sizebytes;
    public int priority;
    public IntPtr userdata;
    public IntPtr buffer;
    public uint bytesread;
    public ASYNCREADINFO_DONE_CALLBACK done;
  }
}
