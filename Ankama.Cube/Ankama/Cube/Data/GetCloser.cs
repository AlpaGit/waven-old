// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GetCloser
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class GetCloser : MoveVector
  {
    private ISingleCoordSelector m_to;

    public ISingleCoordSelector to => this.m_to;

    public override string ToString() => string.Format("GetCloser to {0}", (object) this.m_to);

    public static GetCloser FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (GetCloser) null;
      }
      JObject jsonObject = token.Value<JObject>();
      GetCloser getCloser = new GetCloser();
      getCloser.PopulateFromJson(jsonObject);
      return getCloser;
    }

    public static GetCloser FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      GetCloser defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : GetCloser.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_to = ISingleCoordSelectorUtils.FromJsonProperty(jsonObject, "to");
    }
  }
}
