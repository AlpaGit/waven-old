// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.UnableToConnectException
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Threading.Tasks;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public class UnableToConnectException : TcpConnectionException
  {
    public readonly TaskStatus taskStatus;
    public readonly System.Exception taskException;

    public UnableToConnectException(TaskStatus taskStatus, System.Exception taskException)
      : base(string.Format("TcpConnection disposed: unable to connect. Task status: {0} {1}", (object) taskStatus, (object) taskException))
    {
      this.taskStatus = taskStatus;
      this.taskException = taskException;
    }
  }
}
