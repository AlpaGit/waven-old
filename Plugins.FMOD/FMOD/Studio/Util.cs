// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.Util
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
  [PublicAPI]
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct Util
  {
    public static RESULT ParseID(string idString, out Guid id)
    {
      using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
        return Util.FMOD_Studio_ParseID(freeHelper.byteFromStringUTF8(idString), out id);
    }

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD_Studio_ParseID(byte[] idString, out Guid id);
  }
}
