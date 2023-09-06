// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PROCESS_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

namespace FMOD
{
  public delegate RESULT DSP_PROCESS_CALLBACK(
    ref DSP_STATE dsp_state,
    uint length,
    ref DSP_BUFFER_ARRAY inbufferarray,
    ref DSP_BUFFER_ARRAY outbufferarray,
    bool inputsidle,
    DSP_PROCESS_OPERATION op);
}
