// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PARAMETER_DESC
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System.Runtime.InteropServices;

namespace FMOD
{
  public struct DSP_PARAMETER_DESC
  {
    public DSP_PARAMETER_TYPE type;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] name;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] label;
    public string description;
    public DSP_PARAMETER_DESC_UNION desc;
  }
}
