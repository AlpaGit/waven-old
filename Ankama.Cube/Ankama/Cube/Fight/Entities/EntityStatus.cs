// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.EntityStatus
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Entities
{
  public abstract class EntityStatus : IEntity
  {
    private static readonly IReadOnlyCollection<PropertyId> NoProperties = (IReadOnlyCollection<PropertyId>) new PropertyId[0];
    protected readonly int m_id;
    private readonly Dictionary<CaracId, int> m_caracs = new Dictionary<CaracId, int>((IEqualityComparer<CaracId>) CaracIdComparer.instance);
    private HashSet<PropertyId> m_properties;

    public int id => this.m_id;

    public bool isDirty { get; private set; }

    public abstract EntityType type { get; }

    public IReadOnlyCollection<PropertyId> properties => (IReadOnlyCollection<PropertyId>) this.m_properties ?? EntityStatus.NoProperties;

    protected EntityStatus(int id) => this.m_id = id;

    public void MarkForRemoval() => this.isDirty = true;

    public int GetCarac(CaracId carac, int defaultValue = 0)
    {
      int num;
      return this.m_caracs.TryGetValue(carac, out num) ? num : defaultValue;
    }

    public void SetCarac(CaracId carac, int value) => this.m_caracs[carac] = value;

    public void AddProperty(PropertyId property)
    {
      if (this.m_properties == null)
        this.m_properties = new HashSet<PropertyId>();
      this.m_properties.Add(property);
    }

    public void RemoveProperty(PropertyId property) => this.m_properties?.Remove(property);

    public bool HasProperty(PropertyId property) => this.m_properties != null && this.m_properties.Contains(property);

    public bool HasAnyProperty(params PropertyId[] properties)
    {
      HashSet<PropertyId> properties1 = this.m_properties;
      if (properties1 == null)
        return false;
      int length = properties.Length;
      for (int index = 0; index < length; ++index)
      {
        if (properties1.Contains(properties[index]))
          return true;
      }
      return false;
    }
  }
}
