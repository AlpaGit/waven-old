﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IEntityFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.EntityAddedOrRemoved})]
  public interface IEntityFilter : ITargetFilter, IEditableContent
  {
    IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context);
  }
}
