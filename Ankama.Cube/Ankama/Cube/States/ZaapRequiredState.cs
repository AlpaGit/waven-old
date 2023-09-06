// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.ZaapRequiredState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.Cube.Code.UI;
using System;
using System.Collections;

namespace Ankama.Cube.States
{
  public class ZaapRequiredState : LoadSceneStateContext
  {
    protected override IEnumerator Load()
    {
      yield break;
    }

    protected override IEnumerator Update()
    {
      ButtonData[] buttonDataArray = new ButtonData[1]
      {
        new ButtonData((TextData) 75192, new Action(Main.Quit))
      };
      PopupInfoManager.Show(StateManager.GetDefaultLayer().GetChainEnd(), new PopupInfo()
      {
        title = (RawTextData) 20267,
        message = (RawTextData) 21217,
        buttons = buttonDataArray,
        selectedButton = 1,
        style = PopupStyle.Error
      });
      while (true)
        yield return (object) null;
    }

    protected override IEnumerator Unload()
    {
      yield return (object) base.Unload();
    }
  }
}
