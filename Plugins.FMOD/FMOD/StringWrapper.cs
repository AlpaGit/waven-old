// Decompiled with JetBrains decompiler
// Type: FMOD.StringWrapper
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;

namespace FMOD
{
  public struct StringWrapper
  {
    private readonly IntPtr nativeUtf8Ptr;

    public static implicit operator string(StringWrapper fstring)
    {
      using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
        return freeHelper.stringFromNative(fstring.nativeUtf8Ptr);
    }
  }
}
