// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindowStyle
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.NotificationWindow
{
  [CreateAssetMenu(menuName = "Waven/UI/NotificationWindow/NotificationWindowStyle", order = 1000)]
  public class NotificationWindowStyle : ScriptableObject
  {
    [SerializeField]
    public float fadeInDuration = 0.3f;
    [SerializeField]
    public Ease fadeInEase = Ease.InOutExpo;
    [SerializeField]
    public Ease scaleFadeInEase = Ease.OutBack;
    [SerializeField]
    public float fadeOutDuration = 0.3f;
    [SerializeField]
    public Ease fadeOutEase = Ease.InOutExpo;
    [SerializeField]
    public float displayDuration = 5f;
  }
}
