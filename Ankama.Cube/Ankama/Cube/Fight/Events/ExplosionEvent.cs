// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ExplosionEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class ExplosionEvent : FightEvent
  {
    public CellCoord center { get; private set; }

    public ExplosionEvent(int eventId, int? parentEventId, CellCoord center)
      : base(FightEventData.Types.EventType.Explosion, eventId, parentEventId)
    {
      this.center = center;
    }

    public ExplosionEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.Explosion, proto)
    {
      this.center = proto.CellCoord1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      ExplosionEvent explosionEvent = this;
      FightMap current1 = FightMap.current;
      if ((Object) null != (Object) current1)
      {
        CellObject cellObject;
        if (current1.TryGetCellObject(explosionEvent.center.X, explosionEvent.center.Y, out cellObject))
        {
          SpellEffect spellEffect;
          if (FightSpellEffectFactory.TryGetSpellEffect(SpellEffectKey.Explosion, fightStatus.fightId, explosionEvent.parentEventId, out spellEffect))
          {
            CameraHandler current2 = CameraHandler.current;
            Quaternion rotation = !((Object) null != (Object) current2) ? Quaternion.identity : current2.mapRotation.GetInverseRotation();
            yield return (object) FightSpellEffectFactory.PlaySpellEffect(spellEffect, cellObject.transform, rotation, Vector3.one, 0.0f, fightStatus.context, (ITimelineContextProvider) null);
          }
        }
        else
          Log.Error(FightEventErrors.InvalidPosition(explosionEvent.center), 38, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\ExplosionEvent.cs");
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;
  }
}
