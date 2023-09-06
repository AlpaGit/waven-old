// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapCharacterObjectContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Cube.Maps.VisualEffects;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class MapCharacterObjectContext : VisualEffectContext
  {
    private readonly MapPathfindingActor m_mapCharacterObject;

    public override Transform transform => this.m_mapCharacterObject.transform;

    public MapCharacterObjectContext(MapPathfindingActor character) => this.m_mapCharacterObject = character;

    public override void GetVisualEffectTransformation(out Quaternion rotation, out Vector3 scale)
    {
      rotation = this.m_mapCharacterObject.direction.GetRotation();
      scale = Vector3.one;
    }

    protected override void InitializeEventInstance(EventInstance eventInstance)
    {
      if ((Object) null == (Object) this.m_mapCharacterObject)
        return;
      ATTRIBUTES_3D attributes = FMODUtility.To3DAttributes(this.m_mapCharacterObject.transform);
      int num = (int) eventInstance.set3DAttributes(attributes);
    }

    public void UpdateDirection(Ankama.Cube.Data.Direction from, Ankama.Cube.Data.Direction to)
    {
      Quaternion rotation = from.DirectionAngleTo(to).GetRotation();
      int count = this.m_visualEffectInstances.Count;
      for (int index = 0; index < count; ++index)
        this.m_visualEffectInstances[index].transform.rotation *= rotation;
    }
  }
}
