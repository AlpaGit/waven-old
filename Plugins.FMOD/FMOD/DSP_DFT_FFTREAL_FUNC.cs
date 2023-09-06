// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_DFT_FFTREAL_FUNC
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public delegate RESULT DSP_DFT_FFTREAL_FUNC(
    ref DSP_STATE dsp_state,
    int size,
    IntPtr signal,
    IntPtr dft,
    IntPtr window,
    int signalhop);
}
