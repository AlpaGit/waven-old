// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntitySkinChangedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class EntitySkinChangedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int characterSkinId { get; private set; }

    public int gender { get; private set; }

    public EntitySkinChangedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int characterSkinId,
      int gender)
      : base(FightEventData.Types.EventType.EntitySkinChanged, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.characterSkinId = characterSkinId;
      this.gender = gender;
    }

    public EntitySkinChangedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntitySkinChanged, proto)
    {
      this.concernedEntity = proto.Int1;
      this.characterSkinId = proto.Int2;
      this.gender = proto.Int3;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        CharacterObject view = entityStatus.view as CharacterObject;
        if ((Object) null != (Object) view)
          yield return (object) view.ChangeAnimatedCharacterData(this.characterSkinId, (Gender) this.gender);
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IEntityWithBoardPresence>(entityStatus), 22, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntitySkinChangedEvent.cs");
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 27, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntitySkinChangedEvent.cs");
    }
  }
}
