// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.PopupInfoStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Code.UI
{
  [Serializable]
  public struct PopupInfoStyle
  {
    [SerializeField]
    public PopupStyle style;
    [SerializeField]
    public Color titleColor;
    [SerializeField]
    public Color textColor;
  }
}
