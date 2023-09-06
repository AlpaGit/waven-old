// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_SHOULDIPROCESS_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

namespace FMOD
{
  public delegate RESULT DSP_SHOULDIPROCESS_CALLBACK(
    ref DSP_STATE dsp_state,
    bool inputsidle,
    uint length,
    CHANNELMASK inmask,
    int inchannels,
    SPEAKERMODE speakermode);
}
