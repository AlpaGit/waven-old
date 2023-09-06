// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AnimatedTextButtonStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.Serialization;

namespace Ankama.Cube.UI.Components
{
  [CreateAssetMenu(menuName = "Waven/UI/Buttons/AnimatedTextButtonStyle", order = 1000)]
  public class AnimatedTextButtonStyle : ScriptableObject
  {
    [FormerlySerializedAs("m_baseButtonStyle")]
    [SerializeField]
    public BaseButtonStyle baseButtonStyle;
    [SerializeField]
    public AnimatedTextButtonState normal;
    [SerializeField]
    public AnimatedTextButtonState highlight;
    [SerializeField]
    public AnimatedTextButtonState pressed;
    [SerializeField]
    public AnimatedTextButtonState disable;
  }
}
