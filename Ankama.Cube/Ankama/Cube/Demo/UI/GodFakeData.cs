// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.GodFakeData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class GodFakeData : ScriptableObject
  {
    [SerializeField]
    public God god;
    [SerializeField]
    [Multiline]
    public string title;
    [SerializeField]
    [Multiline]
    public string description;
    [SerializeField]
    public Sprite illu;
    [SerializeField]
    public bool locked;
  }
}
