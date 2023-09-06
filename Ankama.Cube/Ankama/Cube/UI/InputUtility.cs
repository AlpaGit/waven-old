// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.InputUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public static class InputUtility
  {
    private static readonly List<IPointerClickHandler> s_clickHandlerBuffer = new List<IPointerClickHandler>();

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() => Input.simulateMouseWithTouches = false;

    [PublicAPI]
    public static Vector3 pointerPosition => Input.mousePosition;

    [PublicAPI]
    public static bool GetPointerDown() => Input.GetMouseButtonDown(0);

    [PublicAPI]
    public static bool IsPointerDown() => Input.GetMouseButton(0);

    [PublicAPI]
    public static bool GetPointerUp() => Input.GetMouseButtonUp(0);

    [PublicAPI]
    public static bool GetSecondaryDown() => Input.GetMouseButtonDown(1);

    [PublicAPI]
    public static bool IsSecondaryDown() => Input.GetMouseButton(1);

    [PublicAPI]
    public static bool GetSecondaryUp() => Input.GetMouseButtonUp(1);

    [PublicAPI]
    public static bool GetTertiaryDown() => Input.GetMouseButtonDown(2);

    [PublicAPI]
    public static bool IsTertiaryDown() => Input.GetMouseButton(2);

    [PublicAPI]
    public static bool GetTertiaryUp() => Input.GetMouseButtonUp(2);

    public static void SimulateClickOn(Selectable button)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) button || !button.IsInteractable() || !button.isActiveAndEnabled)
        return;
      List<IPointerClickHandler> clickHandlerBuffer = InputUtility.s_clickHandlerBuffer;
      button.GetComponents<IPointerClickHandler>(clickHandlerBuffer);
      try
      {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        int count = clickHandlerBuffer.Count;
        for (int index = 0; index < count; ++index)
          ExecuteEvents.pointerClickHandler(clickHandlerBuffer[index], (BaseEventData) eventData);
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      finally
      {
        InputUtility.s_clickHandlerBuffer.Clear();
      }
    }

    public static bool IsMouseOverUI => (UnityEngine.Object) EventSystem.current != (UnityEngine.Object) null && EventSystem.current.IsPointerOverGameObject();
  }
}
