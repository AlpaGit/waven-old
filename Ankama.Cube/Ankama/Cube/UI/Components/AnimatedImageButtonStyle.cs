// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedImageButtonStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [CreateAssetMenu(menuName = "Waven/UI/Buttons/AnimatedImageButtonStyle", order = 1001)]
  public class AnimatedImageButtonStyle : ScriptableObject
  {
    [SerializeField]
    public BaseButtonStyle baseButtonStyle;
    [SerializeField]
    public AnimatedImageButtonState normal;
    [SerializeField]
    public AnimatedImageButtonState highlight;
    [SerializeField]
    public AnimatedImageButtonState pressed;
    [SerializeField]
    public AnimatedImageButtonState disable;
  }
}
