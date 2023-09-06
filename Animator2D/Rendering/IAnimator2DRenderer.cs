// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.IAnimator2DRenderer
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Animations.Rendering
{
  internal interface IAnimator2DRenderer
  {
    void Start([NotNull] AnimationInstance animation);

    void Release();

    void Compute([NotNull] Graphic[] graphics, [NotNull] AnimationInstance animation, int frame);

    void Buffer([NotNull] Graphic[] graphics, [NotNull] AnimationInstance animation);

    void SetMaterial([NotNull] Material value, Color color);

    void SetColor(Color value);

    void EnableKeyword(string keyword);

    void DisableKeyword(string keyword);

    bool IsKeywordEnabled(string keyword);
  }
}
