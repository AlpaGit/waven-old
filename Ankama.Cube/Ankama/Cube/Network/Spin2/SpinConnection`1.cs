// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.SpinConnection`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2.Layers;
using Ankama.Cube.Protocols.ServerProtocol;
using Ankama.Utilities;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Ankama.Cube.Network.Spin2
{
  public class SpinConnection<T> : MonoBehaviour, IConnection<T> where T : class
  {
    public const int DefaultMaximumMessageSize = 131072;
    public const int SpinHeaderSizeBytes = 4;
    public const bool SpinHeaderSizeContainsItself = true;
    public const bool SpinIsBigIndian = true;
    public const int ReceiveBufferSize = 8092;
    public const float HeartbeatDelay = 5f;
    private readonly SpinTransportLayer m_spinTransportLayer;
    private readonly ISpinCredentialsProvider m_credentialsProvider;
    private readonly ApplicationCodec<T> m_codec;
    private SpinConnection<T>.Status m_status;
    private TaskCompletionSource<IConnectionError> m_authenticationTaskSource;
    private TaskCompletionSource<IDisconnectionInfo> m_disconnectionTaskSource;
    private BlockingCollection<T> m_messagesFifo;
    private IConnectionError m_authenticationErrorToDispatch;
    private IDisconnectionInfo m_disconnectionInfoToDispatch;
    private float m_timeElapsedWithoutWriting;

    public SpinConnection(
      ISpinCredentialsProvider credentialsProvider,
      INetworkLayer<byte[]> underlyingTransportLayer,
      ApplicationCodec<T> codec,
      int maximumMessageSize = 131072)
    {
      this.m_credentialsProvider = credentialsProvider;
      this.m_spinTransportLayer = new SpinTransportLayer((INetworkLayer<byte[]>) new FrameDelimiter(underlyingTransportLayer, maximumMessageSize, 4, true));
      this.m_codec = codec;
      this.m_status = SpinConnection<T>.Status.Disconnected;
    }

    public event Action<T> OnApplicationMessage;

    public event Action<IConnectionError> OnOpenConnectionFailed;

    public event Action OnConnectionOpened;

    public event Action<IDisconnectionInfo> OnConnectionClosed;

    public void Connect(string host, int port)
    {
      if (this.m_status != SpinConnection<T>.Status.Disconnected)
        throw new System.Exception(string.Format("Unable to connect SpinConnection: current status is {0}.", (object) this.m_status));
      this.ConnectAsync(host, port);
    }

    public void Disconnect()
    {
      if (this.m_status != SpinConnection<T>.Status.Connected && this.m_status != SpinConnection<T>.Status.Connecting)
        return;
      this.DisconnectWithInfo((IDisconnectionInfo) new ClientDisconnectionInfo());
    }

    public void DisconnectByServer(DisconnectedByServerEvent evt)
    {
      if (this.m_status != SpinConnection<T>.Status.Connected && this.m_status != SpinConnection<T>.Status.Connecting)
        return;
      this.DisconnectWithInfo((IDisconnectionInfo) new ServerDisconnectionInfo(evt.Reason));
    }

    public void Write(T message)
    {
      if (this.m_status != SpinConnection<T>.Status.Connected)
        return;
      byte[] result;
      if (this.m_codec.TrySerialize(message, out result))
        this.SendRawApplicationData(result);
      else
        Log.Error(string.Format("Unable to serialize application message {0}", (object) message), 105, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
    }

    public void Update()
    {
      if (this.m_authenticationErrorToDispatch != null)
      {
        Action<IConnectionError> connectionFailed = this.OnOpenConnectionFailed;
        if (connectionFailed != null)
          connectionFailed(this.m_authenticationErrorToDispatch);
        this.m_authenticationErrorToDispatch = (IConnectionError) null;
      }
      if (this.m_disconnectionInfoToDispatch != null)
      {
        Action<IDisconnectionInfo> connectionClosed = this.OnConnectionClosed;
        if (connectionClosed != null)
          connectionClosed(this.m_disconnectionInfoToDispatch);
        this.m_disconnectionInfoToDispatch = (IDisconnectionInfo) null;
      }
      if (this.m_status == SpinConnection<T>.Status.Connected)
      {
        this.m_timeElapsedWithoutWriting += Time.deltaTime;
        if ((double) this.m_timeElapsedWithoutWriting > 5.0)
        {
          this.m_spinTransportLayer.Write((SpinProtocol.Message) SpinProtocol.HeartbeatMessage.instance);
          this.m_timeElapsedWithoutWriting = 0.0f;
        }
      }
      if (this.m_messagesFifo == null)
        return;
      T obj;
      while (this.m_messagesFifo.TryTake(out obj))
      {
        Action<T> applicationMessage = this.OnApplicationMessage;
        if (applicationMessage != null)
          applicationMessage(obj);
      }
    }

    private async void ConnectAsync(string host, int port)
    {
      SpinConnection<T> spinConnection1 = this;
      try
      {
        spinConnection1.m_status = SpinConnection<T>.Status.Connecting;
        spinConnection1.m_messagesFifo = new BlockingCollection<T>();
        spinConnection1.m_timeElapsedWithoutWriting = 0.0f;
        Log.Info("Requesting credentials...", 157, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
        ISpinCredentials credentials = await spinConnection1.m_credentialsProvider.GetCredentials();
        if (spinConnection1.m_status == SpinConnection<T>.Status.Destroyed)
          return;
        Log.Info("Connecting with credentials...", 165, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
        await spinConnection1.m_spinTransportLayer.ConnectAsync(host, port);
        if (spinConnection1.m_status == SpinConnection<T>.Status.Destroyed)
          return;
        spinConnection1.m_spinTransportLayer.OnData = new Action<SpinProtocol.Message>(spinConnection1.OnSpinMessage);
        spinConnection1.m_spinTransportLayer.OnConnectionClosed = new Action(spinConnection1.OnUnderlyingConnectionClosed);
        byte[] bytes = Encoding.UTF8.GetBytes(credentials.CreateMessage());
        spinConnection1.SendRawApplicationData(bytes);
        spinConnection1.m_authenticationTaskSource = new TaskCompletionSource<IConnectionError>();
        IConnectionError task1 = await spinConnection1.m_authenticationTaskSource.Task;
        spinConnection1.m_authenticationTaskSource = (TaskCompletionSource<IConnectionError>) null;
        if (spinConnection1.m_status == SpinConnection<T>.Status.Destroyed)
          return;
        if (task1 == null)
        {
          spinConnection1.m_status = SpinConnection<T>.Status.Connected;
          Action connectionOpened = spinConnection1.OnConnectionOpened;
          if (connectionOpened != null)
            connectionOpened();
          spinConnection1.m_disconnectionTaskSource = new TaskCompletionSource<IDisconnectionInfo>();
          IDisconnectionInfo task2 = await spinConnection1.m_disconnectionTaskSource.Task;
          spinConnection1.m_disconnectionTaskSource = (TaskCompletionSource<IDisconnectionInfo>) null;
          if (spinConnection1.m_status == SpinConnection<T>.Status.Destroyed)
            return;
          spinConnection1.DisconnectWithInfo(task2);
          credentials = (ISpinCredentials) null;
        }
        else
        {
          spinConnection1.m_status = SpinConnection<T>.Status.Disconnected;
          spinConnection1.m_spinTransportLayer.OnConnectionClosed = (Action) null;
          spinConnection1.m_spinTransportLayer.Dispose();
          spinConnection1.m_authenticationErrorToDispatch = task1;
        }
      }
      catch (TaskCanceledException ex)
      {
        Log.Info(string.Format("Task canceled while connceting / connected to spin. Status: {0}", (object) spinConnection1.m_status), 215, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
        spinConnection1.m_status = SpinConnection<T>.Status.Disconnected;
        spinConnection1.m_spinTransportLayer.OnConnectionClosed = (Action) null;
        spinConnection1.m_spinTransportLayer.Dispose();
      }
      catch (System.Exception ex)
      {
        if (spinConnection1.m_status == SpinConnection<T>.Status.Disconnected)
          return;
        Log.Error(string.Format("Exception while connecting to Spin: {0}", (object) ex), 225, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
        spinConnection1.m_status = SpinConnection<T>.Status.Disconnected;
        spinConnection1.m_spinTransportLayer.OnConnectionClosed = (Action) null;
        spinConnection1.m_spinTransportLayer.Dispose();
        SpinConnection<T> spinConnection2 = spinConnection1;
        if (!(ex is IConnectionError connectionError))
          connectionError = (IConnectionError) new NetworkConnectionError(ex);
        spinConnection2.m_authenticationErrorToDispatch = connectionError;
      }
    }

    private void DisconnectWithInfo(IDisconnectionInfo info)
    {
      switch (this.m_status)
      {
        case SpinConnection<T>.Status.Connecting:
          this.m_status = SpinConnection<T>.Status.Disconnected;
          this.m_authenticationTaskSource?.TrySetCanceled();
          this.m_spinTransportLayer.OnConnectionClosed = (Action) null;
          this.m_spinTransportLayer.Dispose();
          this.m_authenticationErrorToDispatch = (IConnectionError) new ConnectionInterruptedError(info);
          break;
        case SpinConnection<T>.Status.Connected:
          this.m_status = SpinConnection<T>.Status.Disconnected;
          this.m_disconnectionTaskSource?.TrySetCanceled();
          this.m_spinTransportLayer.OnConnectionClosed = (Action) null;
          this.m_spinTransportLayer.Dispose();
          this.m_disconnectionInfoToDispatch = info;
          break;
      }
    }

    private void SendRawApplicationData(byte[] data)
    {
      if (!this.m_spinTransportLayer.Write((SpinProtocol.Message) new SpinProtocol.RawApplicationMessage(data)))
        return;
      this.m_timeElapsedWithoutWriting = 0.0f;
    }

    private void OnSpinMessage(SpinProtocol.Message spinMessage)
    {
      switch (spinMessage.messageType)
      {
        case SpinProtocol.MessageType.Application:
          if (this.m_status == SpinConnection<T>.Status.Connected)
          {
            T result;
            if (this.m_codec.TryDeserialize(spinMessage.payload, out result))
            {
              this.m_messagesFifo.Add(result);
              break;
            }
            Log.Error("Unable to deserializer applicationMessage !!!", 280, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
            break;
          }
          if (this.m_status != SpinConnection<T>.Status.Connecting)
            break;
          SpinProtocol.ConnectionErrors optConnError;
          if (SpinProtocol.CheckAuthentication(spinMessage.payload, out optConnError))
          {
            Log.Info("Connection to SPIN succeded.", 288, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
            this.m_status = SpinConnection<T>.Status.Connected;
            TaskCompletionSource<IConnectionError> authenticationTaskSource = this.m_authenticationTaskSource;
            if (authenticationTaskSource == null)
              break;
            authenticationTaskSource.TrySetResult((IConnectionError) null);
            break;
          }
          Log.Error(string.Format("Connection to SPIN2 failed with error {0}", (object) optConnError), 294, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
          TaskCompletionSource<IConnectionError> authenticationTaskSource1 = this.m_authenticationTaskSource;
          if (authenticationTaskSource1 == null)
            break;
          authenticationTaskSource1.TrySetResult((IConnectionError) new SpinConnectionError(optConnError));
          break;
        case SpinProtocol.MessageType.Ping:
          // ISSUE: explicit non-virtual call
          this.m_spinTransportLayer.Write((SpinProtocol.Message) new SpinProtocol.PongMessage((spinMessage is SpinProtocol.PingMessage pingMessage ? __nonvirtual (pingMessage.payload) : (byte[]) null) ?? new byte[0]));
          break;
        case SpinProtocol.MessageType.Pong:
          Log.Info("PONG RECEIVED", 308, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
          break;
      }
    }

    private void OnUnderlyingConnectionClosed()
    {
      Log.Info("Underlying tcp connection closed.", 315, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Network\\Spin2\\SpinConnection.cs");
      switch (this.m_status)
      {
        case SpinConnection<T>.Status.Connecting:
          TaskCompletionSource<IConnectionError> authenticationTaskSource = this.m_authenticationTaskSource;
          if (authenticationTaskSource == null)
            break;
          authenticationTaskSource.TrySetResult((IConnectionError) new ConnectionInterruptedError((IDisconnectionInfo) new NetworkDisconnectionInfo()));
          break;
        case SpinConnection<T>.Status.Connected:
          TaskCompletionSource<IDisconnectionInfo> disconnectionTaskSource = this.m_disconnectionTaskSource;
          if (disconnectionTaskSource == null)
            break;
          disconnectionTaskSource.TrySetResult((IDisconnectionInfo) new NetworkDisconnectionInfo());
          break;
      }
    }

    private void OnApplicationQuit()
    {
      this.m_spinTransportLayer.OnConnectionClosed = (Action) null;
      this.m_disconnectionTaskSource?.TrySetCanceled();
      this.m_authenticationTaskSource?.TrySetCanceled();
      this.m_status = SpinConnection<T>.Status.Destroyed;
      this.m_spinTransportLayer.Dispose();
    }

    private enum Status
    {
      Disconnected,
      Connecting,
      Connected,
      Destroyed,
    }
  }
}
