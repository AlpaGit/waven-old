// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GameModeButtonStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI
{
  [CreateAssetMenu]
  public class GameModeButtonStyle : ScriptableObject
  {
    [SerializeField]
    public Ease ease;
    [SerializeField]
    public float transitionDuration;
    [SerializeField]
    public GameModeButtonState normal;
    [SerializeField]
    public GameModeButtonState highlight;
    [SerializeField]
    public GameModeButtonState pressed;
    [SerializeField]
    public GameModeButtonState disable;
  }
}
