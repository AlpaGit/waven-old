﻿// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_GETLISTENERATTRIBUTES_FUNC
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public delegate RESULT DSP_GETLISTENERATTRIBUTES_FUNC(
    ref DSP_STATE dsp_state,
    ref int numlisteners,
    IntPtr attributes);
}
