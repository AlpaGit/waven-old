// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.ObjectWithActivationAnimationExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public static class ObjectWithActivationAnimationExtensions
  {
    public static IEnumerator AnimateActivation(
      this IObjectWithActivationAnimation objectWithActivationAnimation,
      Vector2Int position)
    {
      CellObject cellObject = objectWithActivationAnimation.cellObject;
      if (cellObject.coords != position)
      {
        Log.Warning(string.Format("Was not on the specified cell of an attack sequence: {0} instead of {1} ({2}).", (object) cellObject.coords, (object) position, (object) objectWithActivationAnimation.gameObject.name), 23, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\IObjectWithActivationAnimation.cs");
        objectWithActivationAnimation.SetCellObject(cellObject.parentMap.GetCellObject(position.x, position.y));
      }
      yield return (object) objectWithActivationAnimation.PlayActivationAnimation();
    }
  }
}
