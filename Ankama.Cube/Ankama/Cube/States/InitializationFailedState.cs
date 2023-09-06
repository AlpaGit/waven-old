// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.InitializationFailedState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Utility;
using System;

namespace Ankama.Cube.States
{
  public class InitializationFailedState : StateContext
  {
    private readonly Main.InitializationFailure m_cause;

    public InitializationFailedState(Main.InitializationFailure cause) => this.m_cause = cause;

    protected override void Enable()
    {
      base.Enable();
      string formattedText = TextCollectionUtility.InitializationFailureKeys.GetFormattedText(this.m_cause);
      PopupInfoManager.ShowApplicationError(new PopupInfo()
      {
        title = (RawTextData) 77080,
        message = (RawTextData) formattedText,
        buttons = new ButtonData[1]
        {
          new ButtonData((TextData) 27169, new Action(Main.Quit))
        },
        selectedButton = 1
      });
    }
  }
}
