// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.TooltipWindowUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.UI.Fight;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public static class TooltipWindowUtility
  {
    public static void ShowFightCharacterTooltip(
      ITooltipDataProvider dataProvider,
      Vector3 worldPosition)
    {
      Transform transform = CameraHandler.current.camera.transform;
      TooltipPosition position;
      if ((double) transform.InverseTransformPoint(worldPosition).x < 0.0)
      {
        worldPosition += 0.7071f * transform.right;
        position = TooltipPosition.Right;
      }
      else
      {
        worldPosition -= 0.7071f * transform.right;
        position = TooltipPosition.Left;
      }
      Vector3 uiWorld = FightUIRework.WorldToUIWorld(worldPosition);
      FightUIRework.ShowTooltip(dataProvider, position, uiWorld);
    }

    public static TooltipElementValues GetTooltipElementValues(
      SpellDefinition definition,
      DynamicValueContext context)
    {
      int air = 0;
      int earth = 0;
      int fire = 0;
      int water = 0;
      int reserve = 0;
      IReadOnlyList<GaugeValue> modifyOnSpellPlay = definition.gaugeToModifyOnSpellPlay;
      int count = ((IReadOnlyCollection<GaugeValue>) modifyOnSpellPlay).Count;
      for (int index = 0; index < count; ++index)
      {
        GaugeValue gaugeValue = modifyOnSpellPlay[index];
        int num;
        if (gaugeValue.value.GetValue(context, out num))
        {
          switch (gaugeValue.element)
          {
            case CaracId.FirePoints:
              fire += num;
              continue;
            case CaracId.WaterPoints:
              water += num;
              continue;
            case CaracId.EarthPoints:
              earth += num;
              continue;
            case CaracId.AirPoints:
              air += num;
              continue;
            case CaracId.ReservePoints:
              reserve += num;
              continue;
            default:
              continue;
          }
        }
      }
      return new TooltipElementValues(air, earth, fire, water, reserve);
    }

    public static TooltipActionIcon GetActionIcon(CharacterDefinition definition) => TooltipWindowUtility.GetActionIcon(definition.actionType, definition.actionRange != null);

    public static TooltipActionIcon GetActionIcon(ActionType actionType, bool hasRange)
    {
      switch (actionType)
      {
        case ActionType.None:
          return TooltipActionIcon.None;
        case ActionType.Attack:
          return !hasRange ? TooltipActionIcon.AttackCloseCombat : TooltipActionIcon.AttackRanged;
        case ActionType.Heal:
          return TooltipActionIcon.Heal;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static Vector3 ScreenOffsetInWorld(Vector3 screenOffset, Canvas parentCanvas) => parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? screenOffset * parentCanvas.scaleFactor : parentCanvas.transform.TransformVector(screenOffset);
  }
}
