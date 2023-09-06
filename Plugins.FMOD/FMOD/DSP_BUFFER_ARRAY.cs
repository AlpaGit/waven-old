// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_BUFFER_ARRAY
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public struct DSP_BUFFER_ARRAY
  {
    public int numbuffers;
    public int[] buffernumchannels;
    public CHANNELMASK[] bufferchannelmask;
    public IntPtr[] buffers;
    public SPEAKERMODE speakermode;
  }
}
