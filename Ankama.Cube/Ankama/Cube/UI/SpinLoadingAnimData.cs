// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.SpinLoadingAnimData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI
{
  [CreateAssetMenu(menuName = "Waven/UI/SpinLoadingAnimData")]
  public class SpinLoadingAnimData : ScriptableObject
  {
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public float step;
    [SerializeField]
    public SpinLoadingAnimData.OffsetType offsetType;
    [SerializeField]
    public float offset;

    public enum OffsetType
    {
      Value,
      WoldPos,
    }
  }
}
