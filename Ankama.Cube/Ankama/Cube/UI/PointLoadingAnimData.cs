// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PointLoadingAnimData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI
{
  [CreateAssetMenu(menuName = "Waven/UI/PointLoadingAnimData")]
  public class PointLoadingAnimData : ScriptableObject
  {
    [SerializeField]
    public float scale = 1.5f;
    [SerializeField]
    public float duration = 0.1f;
    [SerializeField]
    public int vibrato = 10;
    [SerializeField]
    public float elasticity = 1f;
    [SerializeField]
    public float delayBetweenPoints = 0.1f;
    [SerializeField]
    public float delayBetweenLoops = 0.2f;
  }
}
