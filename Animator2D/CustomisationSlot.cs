// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.CustomisationSlot
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Animations
{
  [Serializable]
  internal sealed class CustomisationSlot
  {
    [SerializeField]
    public string nodeName = string.Empty;
    [UsedImplicitly]
    [SerializeField]
    public GraphicAsset graphicAsset;
  }
}
