// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.InputManagement.InputKeyCodeDefinition
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.InputManagement
{
  [PublicAPI]
  public class InputKeyCodeDefinition : InputDefinition
  {
    [PublicAPI]
    public readonly KeyCode keyCode;
    [PublicAPI]
    public float repeaterDelay;
    [PublicAPI]
    public float repeaterFrequency;
    private float m_keyDownTimer;

    [PublicAPI]
    public InputKeyCodeDefinition(
      KeyCode keyCode,
      int id,
      int priority = 0,
      float repeaterDelay = 0.4f,
      float repeaterFrequency = 0.1f)
      : base(id, priority)
    {
      this.keyCode = keyCode;
      this.repeaterDelay = repeaterDelay;
      this.repeaterFrequency = repeaterFrequency;
    }

    public override InputState.State GetInputState()
    {
      InputState.State inputState;
      if (Input.GetKeyDown(this.keyCode))
      {
        this.m_keyDownTimer = 0.0f;
        inputState = InputState.State.Activated;
      }
      else if (Input.GetKeyUp(this.keyCode))
        inputState = InputState.State.Deactivated;
      else if (Input.GetKey(this.keyCode))
      {
        this.m_keyDownTimer += Time.unscaledDeltaTime;
        if ((double) this.m_keyDownTimer < (double) this.repeaterDelay)
        {
          inputState = InputState.State.Active;
        }
        else
        {
          float num = this.m_keyDownTimer - this.repeaterDelay;
          if ((double) num > (double) this.repeaterFrequency)
          {
            this.m_keyDownTimer = this.repeaterDelay + num % this.repeaterFrequency;
            inputState = InputState.State.Repeated;
          }
          else
            inputState = InputState.State.Active;
        }
      }
      else
        inputState = InputState.State.None;
      return inputState;
    }
  }
}
