// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ThisSpellElementsPointsCostValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ThisSpellElementsPointsCostValue : DynamicValue
  {
    private List<CaracId> m_elements;

    public IReadOnlyList<CaracId> elements => (IReadOnlyList<CaracId>) this.m_elements;

    public override string ToString() => this.GetType().Name;

    public static ThisSpellElementsPointsCostValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ThisSpellElementsPointsCostValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ThisSpellElementsPointsCostValue elementsPointsCostValue = new ThisSpellElementsPointsCostValue();
      elementsPointsCostValue.PopulateFromJson(jsonObject);
      return elementsPointsCostValue;
    }

    public static ThisSpellElementsPointsCostValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ThisSpellElementsPointsCostValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ThisSpellElementsPointsCostValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_elements = Serialization.JsonArrayAsList<CaracId>(jsonObject, "elements");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Log.Warning("Unable to compute ThisSpellElementsPointsCostValue client-side. Invalid data?", 467, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\DynamicValues\\DynamicValue.cs");
      value = 0;
      return false;
    }

    public override bool ToString(DynamicValueContext context, out string value)
    {
      value = "0";
      return false;
    }
  }
}
