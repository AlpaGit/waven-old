// Decompiled with JetBrains decompiler
// Type: DataEditor.Id`1
// Assembly: DataEditorRuntime, Version=1.0.6990.32389, Culture=neutral, PublicKeyToken=null
// MVID: 45C45C6B-0733-4518-B038-C58DEC652313
// Assembly location: E:\WAVEN\Waven_Data\Managed\DataEditorRuntime.dll

using System;

namespace DataEditor
{
  public class Id<T> : IEquatable<Id<T>> where T : EditableData
  {
    public readonly int value;

    public static bool operator ==(Id<T> a, Id<T> b)
    {
      if ((object) a == (object) b)
        return true;
      return (object) a != null && (object) b != null && a.value == b.value;
    }

    public static bool operator !=(Id<T> a, Id<T> b) => !(a == b);

    public Id(int value) => this.value = value;

    public static explicit operator int(Id<T> id) => id.value;

    public bool Equals(Id<T> other) => !((Id<T>) null == other) && this.value == other.value;

    public override bool Equals(object obj)
    {
      Id<T> other = obj as Id<T>;
      return (Id<T>) null != other && this.Equals(other);
    }

    public override string ToString() => this.value.ToString();

    public override int GetHashCode() => this.value;
  }
}
