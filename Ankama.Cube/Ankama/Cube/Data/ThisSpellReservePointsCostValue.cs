// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ThisSpellReservePointsCostValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ThisSpellReservePointsCostValue : DynamicValue
  {
    public override string ToString() => this.GetType().Name;

    public static ThisSpellReservePointsCostValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ThisSpellReservePointsCostValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ThisSpellReservePointsCostValue reservePointsCostValue = new ThisSpellReservePointsCostValue();
      reservePointsCostValue.PopulateFromJson(jsonObject);
      return reservePointsCostValue;
    }

    public static ThisSpellReservePointsCostValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ThisSpellReservePointsCostValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ThisSpellReservePointsCostValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Log.Warning("Unable to compute ThisSpellReservePointsCostValue client-side. Invalid data?", 450, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\DynamicValues\\DynamicValue.cs");
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
