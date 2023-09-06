// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PopupInfoStyleData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI
{
  [CreateAssetMenu(menuName = "Waven/UI/PopupInfoStyleData")]
  public class PopupInfoStyleData : ScriptableObject
  {
    [SerializeField]
    public Color titleColor = Color.white;
    [SerializeField]
    public Color textColor = Color.white;
  }
}
