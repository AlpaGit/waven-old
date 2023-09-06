// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IEntityFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.EntityAddedOrRemoved})]
  public class IEntityFilterUtils
  {
    public static IEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IEntityFilter");
        return (IEntityFilter) null;
      }
      string str = jtoken.Value<string>();
      IEntityFilter entityFilter;
      switch (str)
      {
        case "ApplicationHolderFilter":
          entityFilter = (IEntityFilter) new ApplicationHolderFilter();
          break;
        case "ArmorFilter":
          entityFilter = (IEntityFilter) new ArmorFilter();
          break;
        case "AroundSquaredTargetFilter":
          entityFilter = (IEntityFilter) new AroundSquaredTargetFilter();
          break;
        case "AroundTargetFilter":
          entityFilter = (IEntityFilter) new AroundTargetFilter();
          break;
        case "CanGrowEntityFilter":
          entityFilter = (IEntityFilter) new CanGrowEntityFilter();
          break;
        case "CastTargetFilter":
          entityFilter = (IEntityFilter) new CastTargetFilter();
          break;
        case "CharacterActionTypeFilter":
          entityFilter = (IEntityFilter) new CharacterActionTypeFilter();
          break;
        case "CharacterEntityFilter":
          entityFilter = (IEntityFilter) new CharacterEntityFilter();
          break;
        case "CombinedEntityFilter":
          entityFilter = (IEntityFilter) new CombinedEntityFilter();
          break;
        case "EffectHolderFilter":
          entityFilter = (IEntityFilter) new EffectHolderFilter();
          break;
        case "ElementaryStateFilter":
          entityFilter = (IEntityFilter) new ElementaryStateFilter();
          break;
        case "EntitiesWithHighestLowestCaracFilter":
          entityFilter = (IEntityFilter) new EntitiesWithHighestLowestCaracFilter();
          break;
        case "EntityHasBeenCrossedOverFilter":
          entityFilter = (IEntityFilter) new EntityHasBeenCrossedOverFilter();
          break;
        case "EntityTypeFilter":
          entityFilter = (IEntityFilter) new EntityTypeFilter();
          break;
        case "EntityValidForMagicalDamageFilter":
          entityFilter = (IEntityFilter) new EntityValidForMagicalDamageFilter();
          break;
        case "EntityValidForMagicalHealFilter":
          entityFilter = (IEntityFilter) new EntityValidForMagicalHealFilter();
          break;
        case "EntityValidForPhysicalDamageFilter":
          entityFilter = (IEntityFilter) new EntityValidForPhysicalDamageFilter();
          break;
        case "EntityValidForPhysicalHealFilter":
          entityFilter = (IEntityFilter) new EntityValidForPhysicalHealFilter();
          break;
        case "FamilyFilter":
          entityFilter = (IEntityFilter) new FamilyFilter();
          break;
        case "FirstTargetsFilter":
          entityFilter = (IEntityFilter) new FirstTargetsFilter();
          break;
        case "FloorMechanismTypeFilter":
          entityFilter = (IEntityFilter) new FloorMechanismTypeFilter();
          break;
        case "HasEmptyPathInLineToTargetFilter":
          entityFilter = (IEntityFilter) new HasEmptyPathInLineToTargetFilter();
          break;
        case "InLineInOneDirectionTargetFilter":
          entityFilter = (IEntityFilter) new InLineInOneDirectionTargetFilter();
          break;
        case "InLineTargetFilter":
          entityFilter = (IEntityFilter) new InLineTargetFilter();
          break;
        case "IntoAreaOfEntityFilter":
          entityFilter = (IEntityFilter) new IntoAreaOfEntityFilter();
          break;
        case "LifeFilter":
          entityFilter = (IEntityFilter) new LifeFilter();
          break;
        case "NotEntityFilter":
          entityFilter = (IEntityFilter) new NotEntityFilter();
          break;
        case "OsamodasAnimalsEntityFilter":
          entityFilter = (IEntityFilter) new OsamodasAnimalsEntityFilter();
          break;
        case "OwnerFilter":
          entityFilter = (IEntityFilter) new OwnerFilter();
          break;
        case "PropertiesFilter":
          entityFilter = (IEntityFilter) new PropertiesFilter();
          break;
        case "PropertyFilter":
          entityFilter = (IEntityFilter) new PropertyFilter();
          break;
        case "RandomTargetsFilter":
          entityFilter = (IEntityFilter) new RandomTargetsFilter();
          break;
        case "RangeFilter":
          entityFilter = (IEntityFilter) new RangeFilter();
          break;
        case "SpecificCompanionFilter":
          entityFilter = (IEntityFilter) new SpecificCompanionFilter();
          break;
        case "SpecificObjectMechanismFilter":
          entityFilter = (IEntityFilter) new SpecificObjectMechanismFilter();
          break;
        case "SpecificSummoningFilter":
          entityFilter = (IEntityFilter) new SpecificSummoningFilter();
          break;
        case "TeamFilter":
          entityFilter = (IEntityFilter) new TeamFilter();
          break;
        case "UnionOfCoordOrEntityFilter":
          entityFilter = (IEntityFilter) new UnionOfCoordOrEntityFilter();
          break;
        case "UnionOfEntityFilter":
          entityFilter = (IEntityFilter) new UnionOfEntityFilter();
          break;
        case "WoundedFilter":
          entityFilter = (IEntityFilter) new WoundedFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IEntityFilter) null;
      }
      entityFilter.PopulateFromJson(jsonObject);
      return entityFilter;
    }

    public static IEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IEntityFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
