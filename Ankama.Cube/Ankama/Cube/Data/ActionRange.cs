// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ActionRange
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ActionRange : IEditableContent
  {
    private ILevelOnlyDependant m_min;
    private ILevelOnlyDependant m_max;

    public ILevelOnlyDependant min => this.m_min;

    public ILevelOnlyDependant max => this.m_max;

    public override string ToString() => string.Format("Range: {0} to {1}", (object) this.m_min, (object) this.m_max);

    public static ActionRange FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ActionRange) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ActionRange actionRange = new ActionRange();
      actionRange.PopulateFromJson(jsonObject);
      return actionRange;
    }

    public static ActionRange FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ActionRange defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ActionRange.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_min = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "min");
      this.m_max = ILevelOnlyDependantUtils.FromJsonProperty(jsonObject, "max");
    }
  }
}
