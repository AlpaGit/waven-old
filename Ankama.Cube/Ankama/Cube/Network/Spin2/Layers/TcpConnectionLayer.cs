// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.TcpConnectionLayer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public class TcpConnectionLayer : INetworkLayer<byte[]>, IDisposable
  {
    public const int ConnectionTimeoutMs = 10000;
    public const int ReceiveBufferSize = 8192;
    private Thread m_thread;
    private TcpClient m_tcpClient;
    private NetworkStream m_networkStream;
    private long m_threadExecutionUid;

    public async Task ConnectAsync(string host, int port)
    {
      TcpConnectionLayer tcpConnectionLayer = this;
      tcpConnectionLayer.m_tcpClient = new TcpClient(AddressFamily.InterNetworkV6)
      {
        Client = {
          DualMode = true
        }
      };
      Task connectTask = tcpConnectionLayer.m_tcpClient.ConnectAsync(host, port);
      Task timeoutTask = Task.Delay(10000);
      Task task = await Task.WhenAny(new Task[2]
      {
        connectTask,
        timeoutTask
      });
      if (timeoutTask.Status == TaskStatus.RanToCompletion)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (tcpConnectionLayer.Dispose());
        throw new TimeoutException(string.Format("TcpConnection disposed: Unable to connect to {0}:{1} before timeout of {2}ms", (object) host, (object) port, (object) 10000));
      }
      if (connectTask.Status != TaskStatus.RanToCompletion)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (tcpConnectionLayer.Dispose());
        throw new UnableToConnectException(connectTask.Status, (System.Exception) connectTask.Exception);
      }
      tcpConnectionLayer.m_networkStream = tcpConnectionLayer.m_tcpClient?.GetStream();
      tcpConnectionLayer.m_thread = new Thread(new ThreadStart(tcpConnectionLayer.ThreadMain))
      {
        IsBackground = true,
        Name = "TcpConnectionLayer-NetworkThread"
      };
      tcpConnectionLayer.m_thread.Start();
    }

    public bool Write(byte[] input)
    {
      this.m_networkStream.WriteAsync(input, 0, input.Length);
      return true;
    }

    private void ThreadMain()
    {
      long ticks = DateTime.Now.Ticks;
      this.m_threadExecutionUid = ticks;
      Log.Info(string.Format("Starting {0} receive thread (id {1})", (object) nameof (TcpConnectionLayer), (object) ticks), 88, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\Layers\\TcpConnectionLayer.cs");
      byte[] numArray = new byte[8192];
      try
      {
        while (this.m_networkStream != null && this.m_threadExecutionUid == ticks)
        {
          int count = this.m_networkStream.Read(numArray, 0, 8192);
          if (count <= 0)
            break;
          byte[] dst = new byte[count];
          Buffer.BlockCopy((Array) numArray, 0, (Array) dst, 0, count);
          this.OnData(dst);
        }
      }
      catch (IOException ex)
      {
        if (this.m_threadExecutionUid != ticks)
          return;
        Log.Info(string.Format("IOException on socket: {0}", (object) ex), 119, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\Layers\\TcpConnectionLayer.cs");
      }
      catch (System.Exception ex)
      {
        if (this.m_threadExecutionUid != ticks)
          return;
        Log.Error(string.Format("Unexpected Exception in {0}: {1}", (object) nameof (TcpConnectionLayer), (object) ex), 128, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\Layers\\TcpConnectionLayer.cs");
      }
      finally
      {
        Log.Info(string.Format("Exiting {0} receive thread (id {1})", (object) nameof (TcpConnectionLayer), (object) ticks), 134, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\Layers\\TcpConnectionLayer.cs");
        Action connectionClosed = this.OnConnectionClosed;
        if (connectionClosed != null)
          connectionClosed();
      }
    }

    public Action OnConnectionClosed { private get; set; }

    public Action<byte[]> OnData { private get; set; }

    public void CloseAbruptly() => this.m_networkStream.Close();

    public void Dispose()
    {
      this.m_threadExecutionUid = 0L;
      this.m_thread = (Thread) null;
      try
      {
        if (this.m_networkStream == null)
          return;
        this.m_networkStream.Dispose();
        this.m_networkStream = (NetworkStream) null;
      }
      finally
      {
        if (this.m_tcpClient != null)
        {
          this.m_tcpClient.Dispose();
          this.m_tcpClient = (TcpClient) null;
        }
      }
    }
  }
}
