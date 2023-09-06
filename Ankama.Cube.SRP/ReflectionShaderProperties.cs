// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ReflectionShaderProperties
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class ReflectionShaderProperties
  {
    public static readonly string _IsReflectionKeyword = "_IS_RENDER_REFLECTION";
    public static readonly string _IsRotateInReflectionKeyword = "_ROTATE_IN_REFLECTION";
    internal static readonly int _ReflectionViewMatrix = Shader.PropertyToID(nameof (_ReflectionViewMatrix));
    internal static readonly int _ReflectionFadeParams = Shader.PropertyToID(nameof (_ReflectionFadeParams));
    internal static readonly int _ReflectionPlanePos = Shader.PropertyToID(nameof (_ReflectionPlanePos));
    internal static readonly int _ReflectionPlaneNormal = Shader.PropertyToID(nameof (_ReflectionPlaneNormal));
    internal static readonly int _ReflectionTransformMatrix = Shader.PropertyToID(nameof (_ReflectionTransformMatrix));
  }
}
