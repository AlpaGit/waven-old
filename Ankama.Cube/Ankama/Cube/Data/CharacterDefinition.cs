// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class CharacterDefinition : 
    AnimatedIsoObjectDefinition,
    IEffectList,
    IEditableContent,
    IFamilyList
  {
    protected PrecomputedData m_precomputedData;
    protected List<Family> m_families;
    protected ILevelOnlyDependant m_life;
    protected ILevelOnlyDependant m_movementPoints;
    protected ILevelOnlyDependant m_actionValue;
    protected IEntitySelector m_customActionTarget;
    protected ActionType m_actionType;
    protected ActionRange m_actionRange;
    protected AIArchetype m_aiArchetype;

    public PrecomputedData precomputedData => this.m_precomputedData;

    public IReadOnlyList<Family> families => (IReadOnlyList<Family>) this.m_families;

    public ILevelOnlyDependant life => this.m_life;

    public ILevelOnlyDependant movementPoints => this.m_movementPoints;

    public ILevelOnlyDependant actionValue => this.m_actionValue;

    public IEntitySelector customActionTarget => this.m_customActionTarget;

    public ActionType actionType => this.m_actionType;

    public ActionRange actionRange => this.m_actionRange;

    public AIArchetype aiArchetype => this.m_aiArchetype;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_precomputedData = PrecomputedData.FromJsonProperty(jsonObject, "precomputedData");
      this.m_families = Serialization.JsonArrayAsList<Family>(jsonObject, "families");
      this.m_life = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "life");
      this.m_movementPoints = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "movementPoints");
      this.m_actionValue = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "actionValue");
      this.m_customActionTarget = IEntitySelectorUtils.FromJsonProperty(jsonObject, "customActionTarget");
      this.m_actionType = (ActionType) Serialization.JsonTokenValue<int>(jsonObject, "actionType");
      this.m_actionRange = ActionRange.FromJsonProperty(jsonObject, "actionRange");
      this.m_aiArchetype = (AIArchetype) Serialization.JsonTokenValue<int>(jsonObject, "aiArchetype");
    }
  }
}
