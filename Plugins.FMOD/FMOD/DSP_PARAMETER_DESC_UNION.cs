// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PARAMETER_DESC_UNION
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System.Runtime.InteropServices;

namespace FMOD
{
  [StructLayout(LayoutKind.Explicit)]
  public struct DSP_PARAMETER_DESC_UNION
  {
    [FieldOffset(0)]
    public DSP_PARAMETER_DESC_FLOAT floatdesc;
    [FieldOffset(0)]
    public DSP_PARAMETER_DESC_INT intdesc;
    [FieldOffset(0)]
    public DSP_PARAMETER_DESC_BOOL booldesc;
    [FieldOffset(0)]
    public DSP_PARAMETER_DESC_DATA datadesc;
  }
}
