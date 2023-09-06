// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.FightContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Animations
{
  public sealed class FightContext : ScriptableObject
  {
    public int fightId { get; private set; }

    public static FightContext Create(int fightId)
    {
      FightContext instance = ScriptableObject.CreateInstance<FightContext>();
      instance.fightId = fightId;
      return instance;
    }
  }
}
