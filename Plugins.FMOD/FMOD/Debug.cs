// Decompiled with JetBrains decompiler
// Type: FMOD.Debug
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System.Runtime.InteropServices;

namespace FMOD
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct Debug
  {
    public static RESULT Initialize(
      DEBUG_FLAGS flags,
      DEBUG_MODE mode,
      DEBUG_CALLBACK callback,
      string filename)
    {
      using (StringHelper.ThreadSafeEncoding freeHelper = StringHelper.GetFreeHelper())
        return Debug.FMOD5_Debug_Initialize(flags, mode, callback, freeHelper.byteFromStringUTF8(filename));
    }

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD5_Debug_Initialize(
      DEBUG_FLAGS flags,
      DEBUG_MODE mode,
      DEBUG_CALLBACK callback,
      byte[] filename);
  }
}
