// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.TabStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.UI
{
  [CreateAssetMenu(menuName = "Waven/UI/Buttons/TabStyle", order = 2000)]
  public class TabStyle : ScriptableObject
  {
    [SerializeField]
    public float transitionDuration = 0.1f;
    [SerializeField]
    public TabStyle.TabState normal;
    [SerializeField]
    public TabStyle.TabState highlight;
    [SerializeField]
    public TabStyle.TabState pressed;
    [SerializeField]
    public TabStyle.TabState disable;
    [SerializeField]
    public TabStyle.TabState selected;

    [Serializable]
    public struct TabState
    {
      [SerializeField]
      public Color backgroundColor;
      [SerializeField]
      public Color borderColor;
    }
  }
}
