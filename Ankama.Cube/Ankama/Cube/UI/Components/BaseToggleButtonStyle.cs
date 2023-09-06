// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.BaseToggleButtonStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [CreateAssetMenu(menuName = "Waven/UI/Buttons/BaseToggleButtonStyle", order = 100)]
  public class BaseToggleButtonStyle : ScriptableObject
  {
    [SerializeField]
    public Sprite toggle;
    [SerializeField]
    public Sprite background;
    [SerializeField]
    public Sprite border;
    [SerializeField]
    public Sprite outline;
    [SerializeField]
    public float transitionDuration;
  }
}
