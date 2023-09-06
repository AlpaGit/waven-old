// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.BufferRequest
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

namespace Ankama.Animations.Rendering
{
  internal struct BufferRequest
  {
    public readonly Animator2D target;
    public readonly AnimationInstance animation;
    public int id;

    public BufferRequest(Animator2D target, AnimationInstance animation, int id)
    {
      this.target = target;
      this.animation = animation;
      this.id = id + 1;
    }
  }
}
