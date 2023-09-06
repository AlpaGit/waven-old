// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ValueFilter
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
  public abstract class ValueFilter : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static ValueFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ValueFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ValueFilter");
        return (ValueFilter) null;
      }
      string str = jtoken.Value<string>();
      ValueFilter valueFilter;
      switch (str)
      {
        case "Between":
          valueFilter = (ValueFilter) new ValueFilter.Between();
          break;
        case "EqualsTo":
          valueFilter = (ValueFilter) new ValueFilter.EqualsTo();
          break;
        case "GreaterEqualThan":
          valueFilter = (ValueFilter) new ValueFilter.GreaterEqualThan();
          break;
        case "GreaterThan":
          valueFilter = (ValueFilter) new ValueFilter.GreaterThan();
          break;
        case "IsEven":
          valueFilter = (ValueFilter) new ValueFilter.IsEven();
          break;
        case "IsOdd":
          valueFilter = (ValueFilter) new ValueFilter.IsOdd();
          break;
        case "LowerEqualThan":
          valueFilter = (ValueFilter) new ValueFilter.LowerEqualThan();
          break;
        case "LowerThan":
          valueFilter = (ValueFilter) new ValueFilter.LowerThan();
          break;
        case "NotEqualsTo":
          valueFilter = (ValueFilter) new ValueFilter.NotEqualsTo();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ValueFilter) null;
      }
      valueFilter.PopulateFromJson(jsonObject);
      return valueFilter;
    }

    public static ValueFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ValueFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract bool Matches(int value, DynamicValueContext context);

    [Serializable]
    public sealed class Between : ValueFilter
    {
      private DynamicValue m_minIncluded;
      private DynamicValue m_maxIncluded;

      public DynamicValue minIncluded => this.m_minIncluded;

      public DynamicValue maxIncluded => this.m_maxIncluded;

      public override string ToString() => string.Format("in [{0}; {1}] ", (object) this.minIncluded, (object) this.maxIncluded);

      public static ValueFilter.Between FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.Between) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.Between between = new ValueFilter.Between();
        between.PopulateFromJson(jsonObject);
        return between;
      }

      public static ValueFilter.Between FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.Between defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.Between.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_minIncluded = DynamicValue.FromJsonProperty(jsonObject, "minIncluded");
        this.m_maxIncluded = DynamicValue.FromJsonProperty(jsonObject, "maxIncluded");
      }

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num1;
        this.minIncluded.GetValue(context, out num1);
        int num2;
        this.maxIncluded.GetValue(context, out num2);
        return num1 <= value && num2 >= value;
      }
    }

    [Serializable]
    public sealed class EqualsTo : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format("== {0}", (object) this.dynamicValue);

      public static ValueFilter.EqualsTo FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.EqualsTo) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.EqualsTo equalsTo = new ValueFilter.EqualsTo();
        equalsTo.PopulateFromJson(jsonObject);
        return equalsTo;
      }

      public static ValueFilter.EqualsTo FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.EqualsTo defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.EqualsTo.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value == num;
      }
    }

    [Serializable]
    public sealed class GreaterEqualThan : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format(">= {0}", (object) this.dynamicValue);

      public static ValueFilter.GreaterEqualThan FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.GreaterEqualThan) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.GreaterEqualThan greaterEqualThan = new ValueFilter.GreaterEqualThan();
        greaterEqualThan.PopulateFromJson(jsonObject);
        return greaterEqualThan;
      }

      public static ValueFilter.GreaterEqualThan FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.GreaterEqualThan defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.GreaterEqualThan.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value >= num;
      }
    }

    [Serializable]
    public sealed class GreaterThan : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format("> {0}", (object) this.dynamicValue);

      public static ValueFilter.GreaterThan FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.GreaterThan) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.GreaterThan greaterThan = new ValueFilter.GreaterThan();
        greaterThan.PopulateFromJson(jsonObject);
        return greaterThan;
      }

      public static ValueFilter.GreaterThan FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.GreaterThan defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.GreaterThan.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value > num;
      }
    }

    [Serializable]
    public sealed class IsEven : ValueFilter
    {
      public override string ToString() => this.GetType().Name;

      public static ValueFilter.IsEven FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.IsEven) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.IsEven isEven = new ValueFilter.IsEven();
        isEven.PopulateFromJson(jsonObject);
        return isEven;
      }

      public static ValueFilter.IsEven FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.IsEven defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.IsEven.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context) => value % 2 == 0;
    }

    [Serializable]
    public sealed class IsOdd : ValueFilter
    {
      public override string ToString() => this.GetType().Name;

      public static ValueFilter.IsOdd FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.IsOdd) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.IsOdd isOdd = new ValueFilter.IsOdd();
        isOdd.PopulateFromJson(jsonObject);
        return isOdd;
      }

      public static ValueFilter.IsOdd FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.IsOdd defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.IsOdd.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context) => value % 2 == 1;
    }

    [Serializable]
    public sealed class LowerEqualThan : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format("<= {0}", (object) this.dynamicValue);

      public static ValueFilter.LowerEqualThan FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.LowerEqualThan) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.LowerEqualThan lowerEqualThan = new ValueFilter.LowerEqualThan();
        lowerEqualThan.PopulateFromJson(jsonObject);
        return lowerEqualThan;
      }

      public static ValueFilter.LowerEqualThan FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.LowerEqualThan defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.LowerEqualThan.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value <= num;
      }
    }

    [Serializable]
    public sealed class LowerThan : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format("< {0}", (object) this.dynamicValue);

      public static ValueFilter.LowerThan FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.LowerThan) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.LowerThan lowerThan = new ValueFilter.LowerThan();
        lowerThan.PopulateFromJson(jsonObject);
        return lowerThan;
      }

      public static ValueFilter.LowerThan FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.LowerThan defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.LowerThan.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value < num;
      }
    }

    [Serializable]
    public sealed class NotEqualsTo : ValueFilter.SingleValueFilter
    {
      public override string ToString() => string.Format("!= {0}", (object) this.dynamicValue);

      public static ValueFilter.NotEqualsTo FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.NotEqualsTo) null;
        }
        JObject jsonObject = token.Value<JObject>();
        ValueFilter.NotEqualsTo notEqualsTo = new ValueFilter.NotEqualsTo();
        notEqualsTo.PopulateFromJson(jsonObject);
        return notEqualsTo;
      }

      public static ValueFilter.NotEqualsTo FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.NotEqualsTo defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.NotEqualsTo.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

      public override bool Matches(int value, DynamicValueContext context)
      {
        int num;
        this.dynamicValue.GetValue(context, out num);
        return value != num;
      }
    }

    [Serializable]
    public abstract class SingleValueFilter : ValueFilter
    {
      protected DynamicValue m_dynamicValue;

      public DynamicValue dynamicValue => this.m_dynamicValue;

      public override string ToString() => this.GetType().Name;

      public static ValueFilter.SingleValueFilter FromJsonToken(JToken token)
      {
        if (token.Type != JTokenType.Object)
        {
          Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
          return (ValueFilter.SingleValueFilter) null;
        }
        JObject jsonObject = token.Value<JObject>();
        JToken jtoken;
        if (!jsonObject.TryGetValue("type", out jtoken))
        {
          Debug.LogWarning((object) "Malformed json: no 'type' property in object of class SingleValueFilter");
          return (ValueFilter.SingleValueFilter) null;
        }
        string str = jtoken.Value<string>();
        ValueFilter.SingleValueFilter singleValueFilter;
        switch (str)
        {
          case "EqualsTo":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.EqualsTo();
            break;
          case "LowerThan":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.LowerThan();
            break;
          case "GreaterThan":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.GreaterThan();
            break;
          case "NotEqualsTo":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.NotEqualsTo();
            break;
          case "LowerEqualThan":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.LowerEqualThan();
            break;
          case "GreaterEqualThan":
            singleValueFilter = (ValueFilter.SingleValueFilter) new ValueFilter.GreaterEqualThan();
            break;
          default:
            Debug.LogWarning((object) ("Unknown type: " + str));
            return (ValueFilter.SingleValueFilter) null;
        }
        singleValueFilter.PopulateFromJson(jsonObject);
        return singleValueFilter;
      }

      public static ValueFilter.SingleValueFilter FromJsonProperty(
        JObject jsonObject,
        string propertyName,
        ValueFilter.SingleValueFilter defaultValue = null)
      {
        JProperty jproperty = jsonObject.Property(propertyName);
        return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ValueFilter.SingleValueFilter.FromJsonToken(jproperty.Value);
      }

      public override void PopulateFromJson(JObject jsonObject)
      {
        base.PopulateFromJson(jsonObject);
        this.m_dynamicValue = DynamicValue.FromJsonProperty(jsonObject, "dynamicValue");
      }
    }
  }
}
