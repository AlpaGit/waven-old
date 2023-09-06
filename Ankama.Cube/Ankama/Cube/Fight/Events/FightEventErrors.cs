// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightEventErrors
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.CommonProtocol;
using DataEditor;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public static class FightEventErrors
  {
    public static string PlayerNotFound(int entityId) => string.Format("Could not find player with id {0}.", (object) entityId);

    public static string PlayerNotFound(int entityId, int fightId) => string.Format("Could not find player with id {0} in fight with id {1}.", (object) entityId, (object) fightId);

    public static string EntityNotFound<T>(int entityId) where T : IEntity => string.Format("Could not find entity with id {0} or entity does not implement {1}.", (object) entityId, (object) typeof (T).Name);

    public static string EntityHasNoView(IEntityWithBoardPresence entity) => string.Format("{0} entity with id {1} doesn't have a valid view.", (object) entity.GetType().Name, (object) entity.id);

    public static string EntityHasIncompatibleView<T>(IEntityWithBoardPresence entity)
    {
      if ((Object) null == (Object) entity.view)
        return FightEventErrors.EntityHasNoView(entity);
      return string.Format("{0} entity with id {1} has a view of type {2} that does not implement {3}.", (object) entity.GetType().Name, (object) entity.id, (object) entity.view.GetType().Name, (object) typeof (T).Name);
    }

    public static string EntityCreationFailed<EntityType, DefinitionType>(
      int entityId,
      int definitionId)
    {
      return string.Format("Could not create {0} entity because no {1} with id {2} could be found.", (object) typeof (EntityType).Name, (object) typeof (DefinitionType).Name, (object) definitionId);
    }

    public static string InvalidPosition(CellCoord refCoord) => string.Format("Invalid position {{{0}, {1}}}.", (object) refCoord.X, (object) refCoord.Y);

    public static string ReserveCompanionNotFound(int definitionId, int playerId) => string.Format("Could not find a reserve companion with definition id {0} in the reserve of player with id {1}", (object) definitionId, (object) playerId);

    public static string DefinitionNotFound<DefinitionType>(int id) where DefinitionType : EditableData => string.Format("Could not find {0} with id {1}.", (object) typeof (DefinitionType).Name, (object) id);
  }
}
