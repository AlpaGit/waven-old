// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.DemoData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class DemoData : ScriptableObject
  {
    [SerializeField]
    public bool resetSelection;
    [SerializeField]
    public int godNbElementLockedBefore;
    [SerializeField]
    public int godNbElementLockedAfter;
    [SerializeField]
    public GodFakeData[] gods;
    [SerializeField]
    public int squadNbElementLockedBefore;
    [SerializeField]
    public int squadNbElementLockedAfter;
    [SerializeField]
    public SquadFakeData[] squads;
  }
}
