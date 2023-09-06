// Decompiled with JetBrains decompiler
// Type: FMOD.Factory
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;
using System.Runtime.InteropServices;

namespace FMOD
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct Factory
  {
    public static RESULT System_Create(out FMOD.System system) => Factory.FMOD5_System_Create(out system.handle);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD5_System_Create(out IntPtr system);
  }
}
