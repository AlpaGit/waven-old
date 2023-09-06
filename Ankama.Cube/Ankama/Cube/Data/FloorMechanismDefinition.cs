// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloorMechanismDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class FloorMechanismDefinition : 
    MechanismDefinition,
    IDefinitionWithTooltip,
    IDefinitionWithDescription,
    IDefinitionWithPrecomputedData
  {
    [LocalizedString("FLOOR_MECHANISM_{id}_NAME", "Mechanisms", 1)]
    [SerializeField]
    private int m_i18nNameId;
    [LocalizedString("FLOOR_MECHANISM_{id}_DESCRIPTION", "Mechanisms", 3)]
    [SerializeField]
    private int m_i18nDescriptionId;
    private FloorMechanismType m_floorType;
    private ILevelOnlyDependant m_activationValue;
    private ActionType m_activationType;
    private FloorMechanismActivationType m_activationTrigger;
    private bool m_removeOnActivation;

    public int i18nNameId => this.m_i18nNameId;

    public int i18nDescriptionId => this.m_i18nDescriptionId;

    public FloorMechanismType floorType => this.m_floorType;

    public ILevelOnlyDependant activationValue => this.m_activationValue;

    public ActionType activationType => this.m_activationType;

    public FloorMechanismActivationType activationTrigger => this.m_activationTrigger;

    public bool removeOnActivation => this.m_removeOnActivation;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_floorType = (FloorMechanismType) Serialization.JsonTokenValue<int>(jsonObject, "floorType", 1);
      this.m_activationValue = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "activationValue");
      this.m_activationType = (ActionType) Serialization.JsonTokenValue<int>(jsonObject, "activationType");
      this.m_activationTrigger = (FloorMechanismActivationType) Serialization.JsonTokenValue<int>(jsonObject, "activationTrigger");
      this.m_removeOnActivation = Serialization.JsonTokenValue<bool>(jsonObject, "removeOnActivation", true);
    }
  }
}
