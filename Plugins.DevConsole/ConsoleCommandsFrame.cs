// Decompiled with JetBrains decompiler
// Type: ConsoleCommandsFrame
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.AdminCommandsProtocol;
using Google.Protobuf;
using System;

public sealed class ConsoleCommandsFrame : CubeMessageFrame
{
  public Action<AdminCmdResultEvent> OnCommandResult;

  public ConsoleCommandsFrame() => this.WhenReceiveEnqueue<AdminCmdResultEvent>(new Action<AdminCmdResultEvent>(this.CmdResultReceived));

  private void CmdResultReceived(AdminCmdResultEvent evt)
  {
    Action<AdminCmdResultEvent> onCommandResult = this.OnCommandResult;
    if (onCommandResult == null)
      return;
    onCommandResult(evt);
  }

  public void Send(AdminCmd cmd) => this.m_connection.Write((IMessage) cmd);
}
