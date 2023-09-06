// Decompiled with JetBrains decompiler
// Type: FMOD.FILE_OPENCALLBACK
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public delegate RESULT FILE_OPENCALLBACK(
    StringWrapper name,
    ref uint filesize,
    ref IntPtr handle,
    IntPtr userdata);
}
