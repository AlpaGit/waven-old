// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.AttackableObjectExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public static class AttackableObjectExtensions
  {
    public static IEnumerator Hit(this IObjectWithArmoredLife objectWithLife, Vector2Int position)
    {
      CellObject cellObject1 = objectWithLife.cellObject;
      if (cellObject1.coords != position)
      {
        Log.Warning(string.Format("Was not on the specified cell of hit sequence: {0} instead of {1} ({2}).", (object) cellObject1.coords, (object) position, (object) objectWithLife.gameObject.name), 31, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\IObjectWithArmoredLife.cs");
        CellObject cellObject2 = cellObject1.parentMap.GetCellObject(position.x, position.y);
        objectWithLife.SetCellObject(cellObject2);
      }
      return objectWithLife.PlayHitAnimation();
    }

    public static void LethalHit(this IObjectWithArmoredLife objectWithLife, Vector2Int position)
    {
      CellObject cellObject1 = objectWithLife.cellObject;
      if (cellObject1.coords != position)
      {
        Log.Warning(string.Format("Was not on the specified cell of hit sequence: {0} instead of {1} ({2}).", (object) cellObject1.coords, (object) position, (object) objectWithLife.gameObject.name), 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\IObjectWithArmoredLife.cs");
        CellObject cellObject2 = cellObject1.parentMap.GetCellObject(position.x, position.y);
        objectWithLife.SetCellObject(cellObject2);
      }
      ((MonoBehaviour) objectWithLife).StartCoroutineImmediateSafe(objectWithLife.PlayLethalHitAnimation());
    }
  }
}
