// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SimpleDiceValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SimpleDiceValue : DynamicValue
  {
    private int m_dice;

    public int dice => this.m_dice;

    public override string ToString() => string.Format("value of D{0}", (object) this.dice);

    public static SimpleDiceValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SimpleDiceValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SimpleDiceValue simpleDiceValue = new SimpleDiceValue();
      simpleDiceValue.PopulateFromJson(jsonObject);
      return simpleDiceValue;
    }

    public static SimpleDiceValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SimpleDiceValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SimpleDiceValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_dice = Serialization.JsonTokenValue<int>(jsonObject, "dice");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Log.Warning("Should not be call in client. Returning max value", 57, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\DynamicValues\\DynamicValue.cs");
      value = this.m_dice;
      return false;
    }

    public override bool ToString(DynamicValueContext context, out string value)
    {
      value = this.m_dice.ToString();
      return false;
    }
  }
}
