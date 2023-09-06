// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BossFightAdditionalDataDefinition
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
  public sealed class BossFightAdditionalDataDefinition : 
    IEditableContent,
    IFightAdditionalDataDefinition
  {
    private int m_bossLife;

    public int bossLife => this.m_bossLife;

    public override string ToString() => this.GetType().Name;

    public static BossFightAdditionalDataDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (BossFightAdditionalDataDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      BossFightAdditionalDataDefinition additionalDataDefinition = new BossFightAdditionalDataDefinition();
      additionalDataDefinition.PopulateFromJson(jsonObject);
      return additionalDataDefinition;
    }

    public static BossFightAdditionalDataDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      BossFightAdditionalDataDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : BossFightAdditionalDataDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_bossLife = Serialization.JsonTokenValue<int>(jsonObject, "bossLife", 2000);
  }
}
