// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.ParameterInstance
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct ParameterInstance
  {
    public IntPtr handle;

    public RESULT getDescription(out PARAMETER_DESCRIPTION description) => ParameterInstance.FMOD_Studio_ParameterInstance_GetDescription(this.handle, out description);

    public RESULT getValue(out float value) => ParameterInstance.FMOD_Studio_ParameterInstance_GetValue(this.handle, out value);

    public RESULT setValue(float value) => ParameterInstance.FMOD_Studio_ParameterInstance_SetValue(this.handle, value);

    [DllImport("fmodstudio")]
    private static extern bool FMOD_Studio_ParameterInstance_IsValid(IntPtr parameter);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD_Studio_ParameterInstance_GetDescription(
      IntPtr parameter,
      out PARAMETER_DESCRIPTION description);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD_Studio_ParameterInstance_GetValue(
      IntPtr parameter,
      out float value);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD_Studio_ParameterInstance_SetValue(
      IntPtr parameter,
      float value);

    public bool hasHandle() => this.handle != IntPtr.Zero;

    public void clearHandle() => this.handle = IntPtr.Zero;

    public bool isValid() => this.hasHandle() && ParameterInstance.FMOD_Studio_ParameterInstance_IsValid(this.handle);
  }
}
