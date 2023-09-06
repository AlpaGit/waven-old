// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedToggleButtonStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [CreateAssetMenu(menuName = "Waven/UI/Buttons/AnimatedToggleButtonStyle", order = 2000)]
  public class AnimatedToggleButtonStyle : ScriptableObject
  {
    [SerializeField]
    public BaseToggleButtonStyle baseButtonStyle;
    [SerializeField]
    public Color baseGraphicColor = Color.white;
    [SerializeField]
    public Color selectedGraphicColor;
    [SerializeField]
    public bool useOnlyAlpha;
    [SerializeField]
    public float selectionTransitionDuration = 0.1f;
    [SerializeField]
    public AnimatedToggleButtonState normal;
    [SerializeField]
    public AnimatedToggleButtonState highlight;
    [SerializeField]
    public AnimatedToggleButtonState pressed;
    [SerializeField]
    public AnimatedToggleButtonState disable;
  }
}
