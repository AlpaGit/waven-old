// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.SOUND_INFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct SOUND_INFO
  {
    public IntPtr name_or_data;
    public MODE mode;
    public CREATESOUNDEXINFO exinfo;
    public int subsoundindex;

    public string name
    {
      get
      {
        using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
          return (this.mode & (MODE.OPENMEMORY | MODE.OPENMEMORY_POINT)) == MODE.DEFAULT ? freeHelper.stringFromNative(this.name_or_data) : string.Empty;
      }
    }
  }
}
