// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CaracIdComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public class CaracIdComparer : IEqualityComparer<CaracId>
  {
    public static readonly CaracIdComparer instance = new CaracIdComparer();

    public bool Equals(CaracId x, CaracId y) => x == y;

    public int GetHashCode(CaracId obj) => (int) obj;
  }
}
