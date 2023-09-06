// Decompiled with JetBrains decompiler
// Type: FMOD.CHANNEL_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public delegate RESULT CHANNEL_CALLBACK(
    IntPtr channelraw,
    CHANNELCONTROL_TYPE controltype,
    CHANNELCONTROL_CALLBACK_TYPE type,
    IntPtr commanddata1,
    IntPtr commanddata2);
}
