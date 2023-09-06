// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.FightCharacterObjectContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public sealed class FightCharacterObjectContext : CharacterObjectContext
  {
    public const string Life = "life";
    private readonly FightCharacterObject m_characterObject;

    public override Transform transform => this.m_characterObject.transform;

    public override CharacterObject characterObject => (CharacterObject) this.m_characterObject;

    public FightCharacterObjectContext(FightCharacterObject characterObject) => this.m_characterObject = characterObject;

    protected override void InitializeEventInstance(EventInstance eventInstance)
    {
      if ((Object) null == (Object) this.m_characterObject)
        return;
      ATTRIBUTES_3D attributes = FMODUtility.To3DAttributes(this.m_characterObject.transform);
      int num1 = (int) eventInstance.set3DAttributes(attributes);
      int num2 = (int) eventInstance.setParameterValue("life", (float) this.m_characterObject.life);
    }

    public override void GetVisualEffectTransformation(out Quaternion rotation, out Vector3 scale)
    {
      rotation = this.m_characterObject.direction.GetRotation();
      scale = Vector3.one;
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
