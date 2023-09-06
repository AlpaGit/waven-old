// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ITargetFilterUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ITargetFilterUtils
  {
    public static ITargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ITargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ITargetFilter");
        return (ITargetFilter) null;
      }
      string str = jtoken.Value<string>();
      ITargetFilter targetFilter;
      switch (str)
      {
        case "ApplicationHolderFilter":
          targetFilter = (ITargetFilter) new ApplicationHolderFilter();
          break;
        case "ArmorFilter":
          targetFilter = (ITargetFilter) new ArmorFilter();
          break;
        case "AroundSquaredTargetFilter":
          targetFilter = (ITargetFilter) new AroundSquaredTargetFilter();
          break;
        case "AroundTargetFilter":
          targetFilter = (ITargetFilter) new AroundTargetFilter();
          break;
        case "CanGrowEntityFilter":
          targetFilter = (ITargetFilter) new CanGrowEntityFilter();
          break;
        case "CastTargetFilter":
          targetFilter = (ITargetFilter) new CastTargetFilter();
          break;
        case "CellValidForCharacterFilter":
          targetFilter = (ITargetFilter) new CellValidForCharacterFilter();
          break;
        case "CellValidForMechanismFilter":
          targetFilter = (ITargetFilter) new CellValidForMechanismFilter();
          break;
        case "CharacterActionTypeFilter":
          targetFilter = (ITargetFilter) new CharacterActionTypeFilter();
          break;
        case "CharacterEntityFilter":
          targetFilter = (ITargetFilter) new CharacterEntityFilter();
          break;
        case "CombinedEntityFilter":
          targetFilter = (ITargetFilter) new CombinedEntityFilter();
          break;
        case "EffectHolderFilter":
          targetFilter = (ITargetFilter) new EffectHolderFilter();
          break;
        case "ElementaryStateFilter":
          targetFilter = (ITargetFilter) new ElementaryStateFilter();
          break;
        case "EntitiesWithHighestLowestCaracFilter":
          targetFilter = (ITargetFilter) new EntitiesWithHighestLowestCaracFilter();
          break;
        case "EntityHasBeenCrossedOverFilter":
          targetFilter = (ITargetFilter) new EntityHasBeenCrossedOverFilter();
          break;
        case "EntityTypeFilter":
          targetFilter = (ITargetFilter) new EntityTypeFilter();
          break;
        case "EntityValidForMagicalDamageFilter":
          targetFilter = (ITargetFilter) new EntityValidForMagicalDamageFilter();
          break;
        case "EntityValidForMagicalHealFilter":
          targetFilter = (ITargetFilter) new EntityValidForMagicalHealFilter();
          break;
        case "EntityValidForPhysicalDamageFilter":
          targetFilter = (ITargetFilter) new EntityValidForPhysicalDamageFilter();
          break;
        case "EntityValidForPhysicalHealFilter":
          targetFilter = (ITargetFilter) new EntityValidForPhysicalHealFilter();
          break;
        case "FamilyFilter":
          targetFilter = (ITargetFilter) new FamilyFilter();
          break;
        case "FirstTargetsFilter":
          targetFilter = (ITargetFilter) new FirstTargetsFilter();
          break;
        case "FloorMechanismTypeFilter":
          targetFilter = (ITargetFilter) new FloorMechanismTypeFilter();
          break;
        case "HasEmptyPathInLineToTargetFilter":
          targetFilter = (ITargetFilter) new HasEmptyPathInLineToTargetFilter();
          break;
        case "InLineInOneDirectionTargetFilter":
          targetFilter = (ITargetFilter) new InLineInOneDirectionTargetFilter();
          break;
        case "InLineTargetFilter":
          targetFilter = (ITargetFilter) new InLineTargetFilter();
          break;
        case "IntoAreaOfEntityFilter":
          targetFilter = (ITargetFilter) new IntoAreaOfEntityFilter();
          break;
        case "LifeFilter":
          targetFilter = (ITargetFilter) new LifeFilter();
          break;
        case "NotCoordFilter":
          targetFilter = (ITargetFilter) new NotCoordFilter();
          break;
        case "NotEntityFilter":
          targetFilter = (ITargetFilter) new NotEntityFilter();
          break;
        case "OsamodasAnimalsEntityFilter":
          targetFilter = (ITargetFilter) new OsamodasAnimalsEntityFilter();
          break;
        case "OwnerFilter":
          targetFilter = (ITargetFilter) new OwnerFilter();
          break;
        case "PropertiesFilter":
          targetFilter = (ITargetFilter) new PropertiesFilter();
          break;
        case "PropertyFilter":
          targetFilter = (ITargetFilter) new PropertyFilter();
          break;
        case "RandomTargetsFilter":
          targetFilter = (ITargetFilter) new RandomTargetsFilter();
          break;
        case "RangeFilter":
          targetFilter = (ITargetFilter) new RangeFilter();
          break;
        case "SpecificCompanionFilter":
          targetFilter = (ITargetFilter) new SpecificCompanionFilter();
          break;
        case "SpecificObjectMechanismFilter":
          targetFilter = (ITargetFilter) new SpecificObjectMechanismFilter();
          break;
        case "SpecificSummoningFilter":
          targetFilter = (ITargetFilter) new SpecificSummoningFilter();
          break;
        case "TeamFilter":
          targetFilter = (ITargetFilter) new TeamFilter();
          break;
        case "UnionOfCoordFilter":
          targetFilter = (ITargetFilter) new UnionOfCoordFilter();
          break;
        case "UnionOfCoordOrEntityFilter":
          targetFilter = (ITargetFilter) new UnionOfCoordOrEntityFilter();
          break;
        case "UnionOfEntityFilter":
          targetFilter = (ITargetFilter) new UnionOfEntityFilter();
          break;
        case "WoundedFilter":
          targetFilter = (ITargetFilter) new WoundedFilter();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ITargetFilter) null;
      }
      targetFilter.PopulateFromJson(jsonObject);
      return targetFilter;
    }

    public static ITargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ITargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ITargetFilterUtils.FromJsonToken(jproperty.Value);
    }
  }
}
