// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.ObjectWithActionExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public static class ObjectWithActionExtensions
  {
    public static IEnumerator DoAction(
      this IObjectWithAction objectWithAction,
      Vector2Int position,
      Vector2Int target)
    {
      CellObject cellObject = objectWithAction.cellObject;
      if (cellObject.coords != position)
      {
        Log.Warning(string.Format("Was not on the specified cell of an attack sequence: {0} instead of {1} ({2}).", (object) cellObject.coords, (object) position, (object) objectWithAction.gameObject.name), 51, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\IObjectWithAction.cs");
        objectWithAction.SetCellObject(cellObject.parentMap.GetCellObject(position.x, position.y));
      }
      Ankama.Cube.Data.Direction directionToAttack = position.GetDirectionTo(target);
      if (!directionToAttack.IsAxisAligned())
        directionToAttack = directionToAttack.GetAxisAligned(objectWithAction.direction);
      bool waitForAnimationEndOnMissingLabel = !position.IsAdjacentTo(target);
      yield return (object) objectWithAction.PlayActionAnimation(directionToAttack, waitForAnimationEndOnMissingLabel);
      objectWithAction.TriggerActionEffect(target);
    }

    public static IEnumerator DoRangedAction(
      this IObjectWithAction objectWithAction,
      Vector2Int position,
      Vector2Int target)
    {
      CellObject cellObject = objectWithAction.cellObject;
      if (cellObject.coords != position)
      {
        Log.Warning(string.Format("Was not on the specified cell of ranged attack sequence: {0} instead of {1} ({2}).", (object) cellObject.coords, (object) position, (object) objectWithAction.gameObject.name), 83, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\IObjectWithAction.cs");
        objectWithAction.SetCellObject(cellObject.parentMap.GetCellObject(position.x, position.y));
      }
      Ankama.Cube.Data.Direction directionToAttack = position.GetDirectionTo(target);
      if (!directionToAttack.IsAxisAligned())
        directionToAttack = directionToAttack.GetAxisAligned(objectWithAction.direction);
      yield return (object) objectWithAction.PlayRangedActionAnimation(directionToAttack);
      objectWithAction.TriggerActionEffect(target);
    }
  }
}
