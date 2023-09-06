// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_METERING_INFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System.Runtime.InteropServices;

namespace FMOD
{
  public struct DSP_METERING_INFO
  {
    public int numsamples;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public float[] peaklevel;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public float[] rmslevel;
    public short numchannels;
  }
}
