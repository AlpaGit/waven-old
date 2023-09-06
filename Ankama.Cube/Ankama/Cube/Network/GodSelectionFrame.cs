// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.GodSelectionFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.PlayerProtocol;
using Google.Protobuf;
using System;

namespace Ankama.Cube.Network
{
  public class GodSelectionFrame : CubeMessageFrame
  {
    public event Action OnChangeGod;

    public GodSelectionFrame() => this.WhenReceiveEnqueue<ChangeGodResultEvent>(new Action<ChangeGodResultEvent>(this.OnGodChanged));

    public void ChangeGodRequest(God god) => this.m_connection.Write((IMessage) new ChangeGodCmd()
    {
      God = (int) god
    });

    private void OnGodChanged(ChangeGodResultEvent obj)
    {
      Action onChangeGod = this.OnChangeGod;
      if (onChangeGod == null)
        return;
      onChangeGod();
    }
  }
}
