// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICastableDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public interface ICastableDefinition : 
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    string illustrationBundleName { get; }

    AssetReference illustrationReference { get; }

    IReadOnlyList<EventCategory> eventsInvalidatingCost { get; }

    IReadOnlyList<EventCategory> eventsInvalidatingCasting { get; }
  }
}
