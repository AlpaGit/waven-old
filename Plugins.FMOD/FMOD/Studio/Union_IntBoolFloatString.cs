// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.Union_IntBoolFloatString
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
  [PublicAPI]
  [StructLayout(LayoutKind.Explicit)]
  internal struct Union_IntBoolFloatString
  {
    [FieldOffset(0)]
    public int intvalue;
    [FieldOffset(0)]
    public bool boolvalue;
    [FieldOffset(0)]
    public float floatvalue;
    [FieldOffset(0)]
    public StringWrapper stringvalue;
  }
}
