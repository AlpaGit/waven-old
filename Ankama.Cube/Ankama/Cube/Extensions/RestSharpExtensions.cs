// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.RestSharpExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using RestSharp;
using System.Collections.Generic;

namespace Ankama.Cube.Extensions
{
  public static class RestSharpExtensions
  {
    public static string GetFirstHeaderValue(this IList<Parameter> parameters, [NotNull] string name)
    {
      int index = 0;
      for (int count = parameters.Count; index < count; ++index)
      {
        Parameter parameter = parameters[index];
        Log.Info(string.Format("Get first header name : {0} value : {1} type : {2}", (object) parameter.Name, parameter.Value, (object) parameter.Type), 16, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Extensions\\RestSharpExtensions.cs");
        if (parameter.Type == ParameterType.HttpHeader && string.Equals(parameter.Name, name))
          return parameter.Value as string;
      }
      return (string) null;
    }
  }
}
