// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.Target
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.CommonProtocol;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Fight.Entities
{
  public struct Target : IEquatable<Target>, IEquatable<Coord>, IEquatable<IEntity>
  {
    public readonly Coord coord;
    public readonly IEntity entity;
    public readonly Target.Type type;

    public Target(Coord coord)
    {
      this.type = Target.Type.Coord;
      this.coord = coord;
      this.entity = (IEntity) null;
    }

    public Target(IEntity entity)
    {
      this.type = Target.Type.Entity;
      this.coord = new Coord();
      this.entity = entity;
    }

    [Pure]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (obj is Target other)
        return this.Equals(other);
      switch (this.type)
      {
        case Target.Type.Coord:
          return obj is Coord coord && this.coord == coord;
        case Target.Type.Entity:
          return this.entity == obj as IEntity;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [Pure]
    public override int GetHashCode()
    {
      switch (this.type)
      {
        case Target.Type.Coord:
          return this.coord.GetHashCode();
        case Target.Type.Entity:
          return this.entity.GetHashCode();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [Pure]
    public bool Equals(Target other)
    {
      if (this.type != other.type)
        return false;
      switch (this.type)
      {
        case Target.Type.Coord:
          return this.coord == other.coord;
        case Target.Type.Entity:
          return this.entity == other.entity;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [Pure]
    public static bool operator ==(Target value, Target other)
    {
      if (value.type != other.type)
        return false;
      switch (value.type)
      {
        case Target.Type.Coord:
          return value.coord == other.coord;
        case Target.Type.Entity:
          return value.entity == other.entity;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [Pure]
    public static bool operator !=(Target value, Target other)
    {
      if (value.type != other.type)
        return true;
      switch (value.type)
      {
        case Target.Type.Coord:
          return value.coord != other.coord;
        case Target.Type.Entity:
          return value.entity != other.entity;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [Pure]
    public bool Equals(Coord other) => this.type == Target.Type.Coord && this.coord == other;

    [Pure]
    public static bool operator ==(Target value, Coord other) => value.type == Target.Type.Coord && value.coord == other;

    [Pure]
    public static bool operator !=(Target value, Coord other) => value.type != Target.Type.Coord || value.coord != other;

    [Pure]
    public bool Equals(IEntity other) => this.type == Target.Type.Entity && this.entity == other;

    [Pure]
    public static bool operator ==(Target value, IEntity other) => value.type == Target.Type.Entity && value.entity == other;

    [Pure]
    public static bool operator !=(Target value, IEntity other) => value.type != Target.Type.Entity || value.entity != other;

    [Pure]
    public CastTarget ToCastTarget()
    {
      switch (this.type)
      {
        case Target.Type.Coord:
          return new CastTarget()
          {
            Cell = this.coord.ToCellCoord()
          };
        case Target.Type.Entity:
          return new CastTarget()
          {
            EntityId = this.entity.id
          };
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public enum Type
    {
      Coord,
      Entity,
    }
  }
}
