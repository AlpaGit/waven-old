// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleEntitySelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;

namespace Ankama.Cube.Data
{
  public interface ISingleEntitySelector : 
    IEntitySelector,
    ITargetSelector,
    IEditableContent,
    ISingleTargetSelector
  {
    bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity;
  }
}
