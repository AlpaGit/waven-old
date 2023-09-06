// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.MathUtility
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;

namespace Ankama.Utilities
{
  public static class MathUtility
  {
    [PublicAPI]
    public static float InverseLerpUnclamped(float a, float b, float value) => (double) a == (double) b ? 0.0f : (float) (((double) value - (double) a) / ((double) b - (double) a));
  }
}
