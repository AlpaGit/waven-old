// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.AttributeExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Reflection;

namespace Ankama.Cube.Extensions
{
  public static class AttributeExtensions
  {
    public static T GetCustomAttribute<T>(this MemberInfo member) where T : class => Attribute.GetCustomAttribute(member, typeof (T)) as T;

    public static T GetCustomAttribute<T>(this Enum e) where T : class
    {
      MemberInfo[] member = e.GetType().GetMember(e.ToString());
      return member.Length == 0 ? default (T) : member[0].GetCustomAttribute<T>();
    }
  }
}
