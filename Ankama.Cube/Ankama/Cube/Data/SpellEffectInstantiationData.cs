// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellEffectInstantiationData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using DataEditor;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpellEffectInstantiationData : IEditableContent
  {
    private string m_spellEffect;
    private ITargetSelector m_originTarget;
    private ISingleTargetSelector m_orientation;
    private SpellEffectInstantiationData.DelayOverDistance m_delayOverDistance;
    private Vector2Int m_delayOverDistanceOrigin;

    public string spellEffect => this.m_spellEffect;

    public ITargetSelector originTarget => this.m_originTarget;

    public ISingleTargetSelector orientation => this.m_orientation;

    public SpellEffectInstantiationData.DelayOverDistance delayOverDistance => this.m_delayOverDistance;

    public override string ToString() => string.Empty;

    public static SpellEffectInstantiationData FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellEffectInstantiationData) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellEffectInstantiationData instantiationData = new SpellEffectInstantiationData();
      instantiationData.PopulateFromJson(jsonObject);
      return instantiationData;
    }

    public static SpellEffectInstantiationData FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellEffectInstantiationData defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellEffectInstantiationData.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_spellEffect = Serialization.JsonTokenValue<string>(jsonObject, "spellEffect", "");
      this.m_originTarget = ITargetSelectorUtils.FromJsonProperty(jsonObject, "originTarget");
      this.m_orientation = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "orientation");
      this.m_delayOverDistance = SpellEffectInstantiationData.DelayOverDistance.FromJsonProperty(jsonObject, "delayOverDistance");
    }

    public IEnumerable<Vector2Int> EnumerateInstantiationPositions(
      [NotNull] DynamicValueContext castTargetContext)
    {
      if (this.m_originTarget is ICoordSelector originTarget)
      {
        foreach (Coord enumerateCoord in originTarget.EnumerateCoords(castTargetContext))
          yield return new Vector2Int(enumerateCoord.x, enumerateCoord.y);
      }
    }

    public IEnumerable<IsoObject> EnumerateInstantiationObjectTargets(
      [NotNull] DynamicValueContext castTargetContext)
    {
      if (this.m_originTarget is IEntitySelector originTarget)
      {
        foreach (IEntity enumerateEntity in originTarget.EnumerateEntities(castTargetContext))
        {
          if (enumerateEntity is IEntityWithBoardPresence withBoardPresence)
            yield return withBoardPresence.view;
        }
      }
    }

    public Quaternion GetOrientation(Vector2Int origin, [NotNull] CastTargetContext castTargetContext)
    {
      if (this.m_orientation != null)
      {
        Coord coord;
        if (this.m_orientation is ISingleCoordSelector orientation1 && orientation1.TryGetCoord((DynamicValueContext) castTargetContext, out coord))
        {
          Vector2Int to = (Vector2Int) coord;
          return origin.GetDirectionTo(to).GetRotation();
        }
        IEntityWithBoardPresence entity;
        if (this.m_orientation is ISingleEntitySelector orientation2 && orientation2.TryGetEntity<IEntityWithBoardPresence>((DynamicValueContext) castTargetContext, out entity))
        {
          Vector2Int coords = entity.view.cellObject.coords;
          return origin.GetDirectionTo(coords).GetRotation();
        }
      }
      else
        Log.Warning(string.Format("Requested orientation but not orientation target is set (spell definition id: {0}).", (object) castTargetContext.spellDefinitionId), 78, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Spells\\SpellEffectInstantiationData.cs");
      return Quaternion.identity;
    }

    public void PreComputeDelayOverDistance([NotNull] DynamicValueContext castTargetContext)
    {
      if (this.m_delayOverDistance == null)
        return;
      Coord coord;
      IEntityWithBoardPresence entity;
      switch (this.m_delayOverDistance.origin)
      {
        case ISingleCoordSelector singleCoordSelector when singleCoordSelector.TryGetCoord(castTargetContext, out coord):
          this.m_delayOverDistanceOrigin = (Vector2Int) coord;
          break;
        case ISingleEntitySelector singleEntitySelector when singleEntitySelector.TryGetEntity<IEntityWithBoardPresence>(castTargetContext, out entity):
          this.m_delayOverDistanceOrigin = entity.view.cellObject.coords;
          break;
        default:
          Log.Warning("Could not find the origin for the delay over distance.", 115, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Spells\\SpellEffectInstantiationData.cs");
          break;
      }
    }

    public float GetDelayOverDistance(Vector2Int target) => this.m_delayOverDistance == null ? 0.0f : this.m_delayOverDistance.delay * (float) this.m_delayOverDistanceOrigin.DistanceTo(target);

    [Serializable]
    public sealed class DelayOverDistance : IEditableContent
    {
      private ISingleTargetSelector m_origin;
      private float m_delay;

      public ISingleTargetSelector origin => this.m_origin;

      public float delay => this.m_delay;

      public override string ToString() => string.Format("{0}s/m from {1}", (object) this.m_delay, (object) this.origin);

      public static SpellEffectInstantiationData.DelayOverDistance FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (SpellEffectInstantiationData.DelayOverDistance) null;
        }
        JObject jsonObject = token.Value<JObject>();
        SpellEffectInstantiationData.DelayOverDistance delayOverDistance = new SpellEffectInstantiationData.DelayOverDistance();
        delayOverDistance.PopulateFromJson(jsonObject);
        return delayOverDistance;
      }

      public static SpellEffectInstantiationData.DelayOverDistance FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        SpellEffectInstantiationData.DelayOverDistance defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellEffectInstantiationData.DelayOverDistance.FromJsonToken(jproperty.Value);
      }

      public void PopulateFromJson(JObject jsonObject)
      {
        this.m_origin = ISingleTargetSelectorUtils.FromJsonProperty(jsonObject, "origin");
        this.m_delay = Serialization.JsonTokenValue<float>(jsonObject, "delay", 0.25f);
      }
    }
  }
}
