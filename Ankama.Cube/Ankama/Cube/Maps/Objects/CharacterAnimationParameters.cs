// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CharacterAnimationParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;

namespace Ankama.Cube.Maps.Objects
{
  public struct CharacterAnimationParameters
  {
    public readonly string animationName;
    public readonly string timelineKey;
    public readonly bool loops;
    public readonly Direction firstDirection;
    public readonly Direction secondDirection;

    public CharacterAnimationParameters(
      string animationName,
      string timelineKey,
      bool loops,
      Direction firstDirection,
      Direction secondDirection)
    {
      this.animationName = animationName;
      this.timelineKey = timelineKey;
      this.loops = loops;
      this.firstDirection = firstDirection;
      this.secondDirection = secondDirection;
    }
  }
}
