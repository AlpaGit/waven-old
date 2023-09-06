// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.ObjectMechanismObjectContext
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
  public sealed class ObjectMechanismObjectContext : CharacterObjectContext
  {
    private readonly ObjectMechanismObject m_characterObject;

    public override Transform transform => this.m_characterObject.transform;

    public override CharacterObject characterObject => (CharacterObject) this.m_characterObject;

    public ObjectMechanismObjectContext(ObjectMechanismObject characterObject) => this.m_characterObject = characterObject;

    protected override void InitializeEventInstance(EventInstance eventInstance)
    {
      if ((Object) null == (Object) this.m_characterObject)
        return;
      ATTRIBUTES_3D attributes = FMODUtility.To3DAttributes(this.m_characterObject.transform);
      int num = (int) eventInstance.set3DAttributes(attributes);
    }

    public override void GetVisualEffectTransformation(out Quaternion rotation, out Vector3 scale)
    {
      rotation = this.m_characterObject.mapRotation.GetInverseRotation();
      scale = this.m_characterObject.alliedWithLocalPlayer ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }
  }
}
