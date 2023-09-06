// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PARAMETER_FFT
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;
using System.Runtime.InteropServices;

namespace FMOD
{
  public struct DSP_PARAMETER_FFT
  {
    public int length;
    public int numchannels;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    private readonly IntPtr[] spectrum_internal;

    public float[][] spectrum
    {
      get
      {
        float[][] spectrum = new float[this.numchannels][];
        for (int index = 0; index < this.numchannels; ++index)
        {
          spectrum[index] = new float[this.length];
          Marshal.Copy(this.spectrum_internal[index], spectrum[index], 0, this.length);
        }
        return spectrum;
      }
    }
  }
}
