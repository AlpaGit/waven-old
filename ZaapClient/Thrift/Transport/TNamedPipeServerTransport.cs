// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TNamedPipeServerTransport
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.IO.Pipes;
using System.Threading;

namespace Thrift.Transport
{
  public class TNamedPipeServerTransport : TServerTransport
  {
    private readonly string pipeAddress;
    private NamedPipeServerStream stream;
    private bool asyncMode = true;

    public TNamedPipeServerTransport(string pipeAddress) => this.pipeAddress = pipeAddress;

    public override void Listen()
    {
    }

    public override void Close()
    {
      if (this.stream == null)
        return;
      try
      {
        this.stream.Close();
        this.stream.Dispose();
      }
      finally
      {
        this.stream = (NamedPipeServerStream) null;
      }
    }

    private void EnsurePipeInstance()
    {
      if (this.stream != null)
        return;
      PipeDirection direction = PipeDirection.InOut;
      int maxNumberOfServerInstances = 254;
      PipeTransmissionMode transmissionMode = PipeTransmissionMode.Byte;
      PipeOptions options1 = !this.asyncMode ? PipeOptions.None : PipeOptions.Asynchronous;
      int inBufferSize = 4096;
      int outBufferSize = 4096;
      try
      {
        this.stream = new NamedPipeServerStream(this.pipeAddress, direction, maxNumberOfServerInstances, transmissionMode, options1, inBufferSize, outBufferSize);
      }
      catch (NotImplementedException ex)
      {
        if (this.asyncMode)
        {
          PipeOptions options2 = options1 & ~PipeOptions.Asynchronous;
          this.stream = new NamedPipeServerStream(this.pipeAddress, direction, maxNumberOfServerInstances, transmissionMode, options2, inBufferSize, outBufferSize);
          this.asyncMode = false;
        }
        else
          throw;
      }
    }

    protected override TTransport AcceptImpl()
    {
      try
      {
        this.EnsurePipeInstance();
        if (this.asyncMode)
        {
          ManualResetEvent evt = new ManualResetEvent(false);
          Exception eOuter = (Exception) null;
          this.stream.BeginWaitForConnection((AsyncCallback) (asyncResult =>
          {
            try
            {
              if (this.stream != null)
                this.stream.EndWaitForConnection(asyncResult);
              else
                eOuter = (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted);
            }
            catch (Exception ex)
            {
              eOuter = this.stream == null ? (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted, ex.Message) : ex;
            }
            evt.Set();
          }), (object) null);
          evt.WaitOne();
          if (eOuter != null)
            throw eOuter;
        }
        else
          this.stream.WaitForConnection();
        TNamedPipeServerTransport.ServerTransport serverTransport = new TNamedPipeServerTransport.ServerTransport(this.stream, this.asyncMode);
        this.stream = (NamedPipeServerStream) null;
        return (TTransport) serverTransport;
      }
      catch (TTransportException ex)
      {
        this.Close();
        throw;
      }
      catch (Exception ex)
      {
        this.Close();
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, ex.Message);
      }
    }

    private class ServerTransport : TTransport
    {
      private NamedPipeServerStream stream;
      private bool asyncMode;

      public ServerTransport(NamedPipeServerStream stream, bool asyncMode)
      {
        this.stream = stream;
        this.asyncMode = asyncMode;
      }

      public override bool IsOpen => this.stream != null && this.stream.IsConnected;

      public override void Open()
      {
      }

      public override void Close()
      {
        if (this.stream == null)
          return;
        this.stream.Close();
      }

      public override int Read(byte[] buf, int off, int len)
      {
        if (this.stream == null)
          throw new TTransportException(TTransportException.ExceptionType.NotOpen);
        if (!this.asyncMode)
          return this.stream.Read(buf, off, len);
        Exception eOuter = (Exception) null;
        ManualResetEvent evt = new ManualResetEvent(false);
        int retval = 0;
        this.stream.BeginRead(buf, off, len, (AsyncCallback) (asyncResult =>
        {
          try
          {
            if (this.stream != null)
              retval = this.stream.EndRead(asyncResult);
            else
              eOuter = (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted);
          }
          catch (Exception ex)
          {
            eOuter = this.stream == null ? (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted, ex.Message) : ex;
          }
          evt.Set();
        }), (object) null);
        evt.WaitOne();
        if (eOuter != null)
          throw eOuter;
        return retval;
      }

      public override void Write(byte[] buf, int off, int len)
      {
        if (this.stream == null)
          throw new TTransportException(TTransportException.ExceptionType.NotOpen);
        if (this.asyncMode)
        {
          Exception eOuter = (Exception) null;
          ManualResetEvent evt = new ManualResetEvent(false);
          this.stream.BeginWrite(buf, off, len, (AsyncCallback) (asyncResult =>
          {
            try
            {
              if (this.stream != null)
                this.stream.EndWrite(asyncResult);
              else
                eOuter = (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted);
            }
            catch (Exception ex)
            {
              eOuter = this.stream == null ? (Exception) new TTransportException(TTransportException.ExceptionType.Interrupted, ex.Message) : ex;
            }
            evt.Set();
          }), (object) null);
          evt.WaitOne();
          if (eOuter != null)
            throw eOuter;
        }
        else
          this.stream.Write(buf, off, len);
      }

      protected override void Dispose(bool disposing)
      {
        if (this.stream == null)
          return;
        this.stream.Dispose();
      }
    }
  }
}
