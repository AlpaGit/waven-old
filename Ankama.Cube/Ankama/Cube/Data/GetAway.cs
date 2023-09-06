// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GetAway
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class GetAway : MoveVector
  {
    private ISingleCoordSelector m_from;

    public ISingleCoordSelector from => this.m_from;

    public override string ToString() => string.Format("GetAway from {0}", (object) this.m_from);

    public static GetAway FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (GetAway) null;
      }
      JObject jsonObject = token.Value<JObject>();
      GetAway getAway = new GetAway();
      getAway.PopulateFromJson(jsonObject);
      return getAway;
    }

    public static GetAway FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      GetAway defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : GetAway.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_from = ISingleCoordSelectorUtils.FromJsonProperty(jsonObject, "from");
    }
  }
}
