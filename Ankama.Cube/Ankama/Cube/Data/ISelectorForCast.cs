// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISelectorForCast
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public interface ISelectorForCast : ITargetSelector, IEditableContent
  {
    IEnumerable<Target> EnumerateTargets(DynamicValueContext context);
  }
}
