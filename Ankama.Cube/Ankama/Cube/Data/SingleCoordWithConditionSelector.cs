// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SingleCoordWithConditionSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SingleCoordWithConditionSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleCoordSelector,
    ICoordSelector,
    ISingleTargetSelector,
    ISingleTargetWithFiltersSelector
  {
    private ISingleCoordSelector m_from;
    private List<ICoordFilter> m_onlyIf;

    public ISingleCoordSelector from => this.m_from;

    public IReadOnlyList<ICoordFilter> onlyIf => (IReadOnlyList<ICoordFilter>) this.m_onlyIf;

    public override string ToString() => this.GetType().Name;

    public static SingleCoordWithConditionSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SingleCoordWithConditionSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SingleCoordWithConditionSelector conditionSelector = new SingleCoordWithConditionSelector();
      conditionSelector.PopulateFromJson(jsonObject);
      return conditionSelector;
    }

    public static SingleCoordWithConditionSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SingleCoordWithConditionSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SingleCoordWithConditionSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_from = ISingleCoordSelectorUtils.FromJsonProperty(jsonObject, "from");
      JArray jarray = Serialization.JsonArray(jsonObject, "onlyIf");
      this.m_onlyIf = new List<ICoordFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_onlyIf.Add(ICoordFilterUtils.FromJsonToken(token));
    }

    public IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      if (!(context is DynamicValueFightContext valueFightContext))
        return Enumerable.Empty<Coord>();
      IEnumerable<Coord> coords = valueFightContext.fightStatus.EnumerateCoords();
      int count = this.m_onlyIf.Count;
      for (int index = 0; index < count; ++index)
        coords = this.m_onlyIf[index].Filter(coords, context);
      return coords;
    }

    public bool TryGetCoord(DynamicValueContext context, out Coord coord) => throw new NotImplementedException();
  }
}
