// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedImageButtonState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [Serializable]
  public struct AnimatedImageButtonState
  {
    [SerializeField]
    public Color imageColor;
    [SerializeField]
    public Vector2 imageSizeDelta;
    [SerializeField]
    public Color backgroundColor;
    [SerializeField]
    public Vector2 backgroundSizeDelta;
    [SerializeField]
    public Color borderColor;
    [SerializeField]
    public Color outlineColor;
    [SerializeField]
    public Vector2 outlineSizeDelta;
  }
}
