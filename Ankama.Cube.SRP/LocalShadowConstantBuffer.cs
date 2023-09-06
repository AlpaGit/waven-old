// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LocalShadowConstantBuffer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class LocalShadowConstantBuffer
  {
    public static readonly int _LocalWorldToShadowAtlas = Shader.PropertyToID(nameof (_LocalWorldToShadowAtlas));
    public static readonly int _LocalShadowStrength = Shader.PropertyToID(nameof (_LocalShadowStrength));
    public static readonly int _LocalShadowOffset0 = Shader.PropertyToID(nameof (_LocalShadowOffset0));
    public static readonly int _LocalShadowOffset1 = Shader.PropertyToID(nameof (_LocalShadowOffset1));
    public static readonly int _LocalShadowOffset2 = Shader.PropertyToID(nameof (_LocalShadowOffset2));
    public static readonly int _LocalShadowOffset3 = Shader.PropertyToID(nameof (_LocalShadowOffset3));
    public static readonly int _LocalShadowmapSize = Shader.PropertyToID(nameof (_LocalShadowmapSize));
  }
}
