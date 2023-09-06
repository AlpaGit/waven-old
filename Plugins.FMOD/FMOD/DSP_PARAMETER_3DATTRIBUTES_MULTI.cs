// Decompiled with JetBrains decompiler
// Type: FMOD.DSP_PARAMETER_3DATTRIBUTES_MULTI
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System.Runtime.InteropServices;

namespace FMOD
{
  public struct DSP_PARAMETER_3DATTRIBUTES_MULTI
  {
    public int numlisteners;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public ATTRIBUTES_3D[] relative;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public float[] weight;
    public ATTRIBUTES_3D absolute;
  }
}
