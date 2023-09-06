// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementCaracIdSuperlativeSelector
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
  public sealed class ElementCaracIdSuperlativeSelector : IEditableContent, ICaracIdSelector
  {
    private OwnerFilter m_playerSelector;
    private Superlative m_selection;

    public OwnerFilter playerSelector => this.m_playerSelector;

    public Superlative selection => this.m_selection;

    public override string ToString() => string.Format("Elements with {0} value for {1}", (object) this.m_selection, (object) this.m_playerSelector);

    public static ElementCaracIdSuperlativeSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementCaracIdSuperlativeSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementCaracIdSuperlativeSelector superlativeSelector = new ElementCaracIdSuperlativeSelector();
      superlativeSelector.PopulateFromJson(jsonObject);
      return superlativeSelector;
    }

    public static ElementCaracIdSuperlativeSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementCaracIdSuperlativeSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementCaracIdSuperlativeSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_playerSelector = OwnerFilter.FromJsonProperty(jsonObject, "playerSelector");
      this.m_selection = (Superlative) Serialization.JsonTokenValue<int>(jsonObject, "selection", 1);
    }
  }
}
