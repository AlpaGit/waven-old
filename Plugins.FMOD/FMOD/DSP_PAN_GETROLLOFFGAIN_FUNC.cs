// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PAN_GETROLLOFFGAIN_FUNC
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

namespace FMOD
{
  public delegate RESULT DSP_PAN_GETROLLOFFGAIN_FUNC(
    ref DSP_STATE dsp_state,
    DSP_PAN_3D_ROLLOFF_TYPE rolloff,
    float distance,
    float mindistance,
    float maxdistance,
    out float gain);
}
