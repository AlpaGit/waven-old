// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.RuntimeDataHelper
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;

namespace Ankama.Cube
{
  public static class RuntimeDataHelper
  {
    [CanBeNull]
    public static ReserveDefinition GetReserveDefinitionFrom([NotNull] PlayerStatus playerStatus)
    {
      HeroStatus heroStatus = playerStatus.heroStatus;
      if (heroStatus == null)
        return (ReserveDefinition) null;
      ReserveDefinition reserveDefinition;
      return RuntimeData.reserveDefinitions.TryGetValue(((WeaponDefinition) heroStatus.definition).god, out reserveDefinition) ? reserveDefinition : (ReserveDefinition) null;
    }
  }
}
