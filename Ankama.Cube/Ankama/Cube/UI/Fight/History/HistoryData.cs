// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.History.HistoryData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.History
{
  public class HistoryData : ScriptableObject
  {
    [Header("Layout")]
    [SerializeField]
    public int maxElements = 5;
    [SerializeField]
    public int maxHiddableElements = 2;
    [SerializeField]
    public float spacing = 5f;
    [SerializeField]
    public float positionTweenDuration = 0.2f;
    [SerializeField]
    public Ease postitionTweenEase = Ease.Linear;
    [SerializeField]
    public float outAlphaTweenDuration = 0.1f;
    [SerializeField]
    public Vector3 inScalePunchValue = Vector3.one;
    [SerializeField]
    public float inScalePunchDuration = 0.1f;
    [Header("Element")]
    [SerializeField]
    public Sprite playerBg;
    [SerializeField]
    public Sprite opponentBg;
    [SerializeField]
    public Color elementsDepthColor = Color.white;
    [SerializeField]
    public float elementsOverDuration = 0.1f;
    [SerializeField]
    public float elementsOverOffset = 20f;
    [SerializeField]
    public Ease elementsOverEase = Ease.Linear;
  }
}
