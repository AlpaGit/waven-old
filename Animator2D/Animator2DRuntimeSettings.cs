// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Animator2DRuntimeSettings
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using Ankama.Animations.Management;
using JetBrains.Annotations;

namespace Ankama.Animations
{
  [PublicAPI]
  public static class Animator2DRuntimeSettings
  {
    [PublicAPI]
    public static void SetAnimationDataBufferSize(int size) => AnimationManager.SetBufferCapacity(size);
  }
}
