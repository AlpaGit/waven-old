// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ReflectionUtility
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using Ankama.Utilities;
using System;
using System.Reflection;

namespace Ankama.Cube.SRP
{
  public static class ReflectionUtility
  {
    public static object InvokeMethod(object o, string name, object[] parameters)
    {
      MethodInfo method = o.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      if (method != (MethodInfo) null)
        return method.Invoke(o, parameters);
      Log.Error(string.Format("Cannot find method {0} in {1}", (object) name, o), 17, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\ReflectionUtility.cs");
      return (object) null;
    }

    public static object GetField(object o, string name)
    {
      FieldInfo field = o.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      if (field != (FieldInfo) null)
        return field.GetValue(o);
      Log.Error(string.Format("Cannot find field {0} in {1}", (object) name, o), 29, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\ReflectionUtility.cs");
      return (object) null;
    }

    public static object InvokeStaticMethod(Type type, string name, object[] parameters)
    {
      MethodInfo method = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
      if (method != (MethodInfo) null)
        return method.Invoke((object) null, parameters);
      Log.Error(string.Format("Cannot find method {0} for {1}", (object) name, (object) type), 40, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\SRP\\Scripts\\ReflectionUtility.cs");
      return (object) null;
    }
  }
}
