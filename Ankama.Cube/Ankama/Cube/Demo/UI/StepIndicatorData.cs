// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StepIndicatorData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class StepIndicatorData : ScriptableObject
  {
    [SerializeField]
    public float transitionDuration;
    [SerializeField]
    public StepIndicatorData.StateData enableState;
    [SerializeField]
    public StepIndicatorData.StateData disableState;

    [Serializable]
    public struct StateData
    {
      [SerializeField]
      public float alpha;
      [SerializeField]
      public float scale;
    }
  }
}
