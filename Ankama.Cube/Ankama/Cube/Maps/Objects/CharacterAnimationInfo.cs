// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CharacterAnimationInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public struct CharacterAnimationInfo
  {
    public readonly Vector2 position;
    public readonly Ankama.Cube.Data.Direction direction;
    public readonly string animationName;
    public readonly string timelineKey;
    public readonly bool flipX;
    public readonly bool loops;
    public readonly CharacterAnimationParameters parameters;

    public CharacterAnimationInfo(
      Vector2 position,
      string animationName,
      string timelineKey,
      bool loops,
      Ankama.Cube.Data.Direction direction,
      DirectionAngle mapRotation)
    {
      this.position = position;
      this.direction = direction;
      switch (direction.Rotate(mapRotation))
      {
        case Ankama.Cube.Data.Direction.None:
          this.animationName = animationName;
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.East:
          this.animationName = animationName + "4";
          this.flipX = true;
          break;
        case Ankama.Cube.Data.Direction.SouthEast:
          this.animationName = animationName + "1";
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.South:
          this.animationName = animationName + "2";
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.SouthWest:
          this.animationName = animationName + "1";
          this.flipX = true;
          break;
        case Ankama.Cube.Data.Direction.West:
          this.animationName = animationName + "4";
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.NorthWest:
          this.animationName = animationName + "5";
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.North:
          this.animationName = animationName + "6";
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.NorthEast:
          this.animationName = animationName + "5";
          this.flipX = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.timelineKey = timelineKey;
      this.loops = loops;
      this.parameters = new CharacterAnimationParameters(animationName, timelineKey, loops, direction, Ankama.Cube.Data.Direction.None);
    }

    public CharacterAnimationInfo(
      Vector2 position,
      string animationName,
      string timelineKey,
      bool loops,
      Ankama.Cube.Data.Direction previousDirection,
      Ankama.Cube.Data.Direction direction,
      DirectionAngle mapRotation)
    {
      this.position = position;
      this.direction = direction;
      Ankama.Cube.Data.Direction direction1 = previousDirection.Rotate(mapRotation);
      Ankama.Cube.Data.Direction direction2 = direction.Rotate(mapRotation);
      switch (direction1)
      {
        case Ankama.Cube.Data.Direction.SouthEast:
          switch (direction2)
          {
            case Ankama.Cube.Data.Direction.SouthWest:
              this.animationName = animationName + "31";
              break;
            case Ankama.Cube.Data.Direction.NorthWest:
              this.animationName = animationName + "35";
              break;
            case Ankama.Cube.Data.Direction.NorthEast:
              this.animationName = animationName + "35";
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (direction), string.Format("Incompatible change of direction: {0} to {1}.", (object) previousDirection, (object) direction));
          }
          this.flipX = true;
          break;
        case Ankama.Cube.Data.Direction.SouthWest:
          switch (direction2)
          {
            case Ankama.Cube.Data.Direction.SouthEast:
              this.animationName = animationName + "31";
              break;
            case Ankama.Cube.Data.Direction.NorthWest:
              this.animationName = animationName + "35";
              break;
            case Ankama.Cube.Data.Direction.NorthEast:
              this.animationName = animationName + "35";
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (direction), string.Format("Incompatible change of direction: {0} to {1}.", (object) previousDirection, (object) direction));
          }
          this.flipX = false;
          break;
        case Ankama.Cube.Data.Direction.NorthWest:
          switch (direction2)
          {
            case Ankama.Cube.Data.Direction.SouthEast:
              this.animationName = animationName + "71";
              break;
            case Ankama.Cube.Data.Direction.SouthWest:
              this.animationName = animationName + "71";
              break;
            case Ankama.Cube.Data.Direction.NorthEast:
              this.animationName = animationName + "75";
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (direction), string.Format("Incompatible change of direction: {0} to {1}.", (object) previousDirection, (object) direction));
          }
          this.flipX = true;
          break;
        case Ankama.Cube.Data.Direction.NorthEast:
          switch (direction2)
          {
            case Ankama.Cube.Data.Direction.SouthEast:
              this.animationName = animationName + "71";
              break;
            case Ankama.Cube.Data.Direction.SouthWest:
              this.animationName = animationName + "71";
              break;
            case Ankama.Cube.Data.Direction.NorthWest:
              this.animationName = animationName + "75";
              break;
            default:
              throw new ArgumentOutOfRangeException(nameof (direction), string.Format("Incompatible change of direction: {0} to {1}.", (object) previousDirection, (object) direction));
          }
          this.flipX = false;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.timelineKey = timelineKey;
      this.loops = loops;
      this.parameters = new CharacterAnimationParameters(animationName, timelineKey, loops, previousDirection, direction);
    }
  }
}
