// Decompiled with JetBrains decompiler
// Type: PlayerLayerFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.PlayerProtocol;
using System;

public class PlayerLayerFrame : CubeMessageFrame
{
  public Action<ChangeGodResultEvent> onGodChangeResult;
  public Action<SelectDeckAndWeaponResultEvent> onWeaponChangeResult;

  public PlayerLayerFrame()
  {
    this.WhenReceiveEnqueue<ChangeGodResultEvent>(new Action<ChangeGodResultEvent>(this.OnGodChange));
    this.WhenReceiveEnqueue<SelectDeckAndWeaponResultEvent>(new Action<SelectDeckAndWeaponResultEvent>(this.OnWeaponChange));
  }

  private void OnWeaponChange(SelectDeckAndWeaponResultEvent result) => this.onWeaponChangeResult(result);

  private void OnGodChange(ChangeGodResultEvent result)
  {
    Action<ChangeGodResultEvent> onGodChangeResult = this.onGodChangeResult;
    if (onGodChangeResult == null)
      return;
    onGodChangeResult(result);
  }
}
