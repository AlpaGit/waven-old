// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TriggeringEventValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.Any})]
  [Serializable]
  public sealed class TriggeringEventValue : DynamicValue
  {
    public override string ToString() => "<triggering event value>";

    public static TriggeringEventValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TriggeringEventValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TriggeringEventValue triggeringEventValue = new TriggeringEventValue();
      triggeringEventValue.PopulateFromJson(jsonObject);
      return triggeringEventValue;
    }

    public static TriggeringEventValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TriggeringEventValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TriggeringEventValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Debug.LogWarning((object) "Unable to compute TriggeringEventValue client-side. Invalid data ?");
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
