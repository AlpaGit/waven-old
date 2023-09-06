// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SimpleFloatingObjectData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Animations/Simple Floating Object Data")]
  public class SimpleFloatingObjectData : ScriptableObject
  {
    [SerializeField]
    public float verticalNoise = 0.2f;
    [SerializeField]
    public float verticalSpeed = 1f;
    [SerializeField]
    public float rotationNoise = 0.2f;
    [SerializeField]
    public float rotationSpeed = 1f;
  }
}
