﻿// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.COMMANDREPLAY_FRAME_CALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMOD.Studio
{
  [PublicAPI]
  public delegate RESULT COMMANDREPLAY_FRAME_CALLBACK(
    CommandReplay replay,
    int commandIndex,
    float currentTime,
    IntPtr userdata);
}
