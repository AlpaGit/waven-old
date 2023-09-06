// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_STATE
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public struct DSP_STATE
  {
    public IntPtr instance;
    public IntPtr plugindata;
    public uint channelmask;
    public int source_speakermode;
    public IntPtr sidechaindata;
    public int sidechainchannels;
    public IntPtr functions;
    public int systemobject;
  }
}
