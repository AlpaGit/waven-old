// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IAreaDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public interface IAreaDefinition : IEditableContent
  {
    Area ToArea(Vector2Int position);
  }
}
