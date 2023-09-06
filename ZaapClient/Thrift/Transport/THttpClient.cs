// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.THttpClient
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Thrift.Transport
{
  public class THttpClient : TTransport, IDisposable
  {
    private readonly Uri uri;
    private readonly X509Certificate[] certificates;
    private Stream inputStream;
    private MemoryStream outputStream = new MemoryStream();
    private int connectTimeout = 30000;
    private int readTimeout = 30000;
    private IDictionary<string, string> customHeaders = (IDictionary<string, string>) new Dictionary<string, string>();
    private IWebProxy proxy = WebRequest.DefaultWebProxy;
    private bool _IsDisposed;

    public THttpClient(Uri u)
      : this(u, Enumerable.Empty<X509Certificate>())
    {
    }

    public THttpClient(Uri u, IEnumerable<X509Certificate> certificates)
    {
      this.uri = u;
      this.certificates = (certificates ?? Enumerable.Empty<X509Certificate>()).ToArray<X509Certificate>();
    }

    public int ConnectTimeout
    {
      set => this.connectTimeout = value;
    }

    public int ReadTimeout
    {
      set => this.readTimeout = value;
    }

    public IDictionary<string, string> CustomHeaders => this.customHeaders;

    public IWebProxy Proxy
    {
      set => this.proxy = value;
    }

    public override bool IsOpen => true;

    public override void Open()
    {
    }

    public override void Close()
    {
      if (this.inputStream != null)
      {
        this.inputStream.Close();
        this.inputStream = (Stream) null;
      }
      if (this.outputStream == null)
        return;
      this.outputStream.Close();
      this.outputStream = (MemoryStream) null;
    }

    public override int Read(byte[] buf, int off, int len)
    {
      if (this.inputStream == null)
        throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No request has been sent");
      try
      {
        int num = this.inputStream.Read(buf, off, len);
        return num != -1 ? num : throw new TTransportException(TTransportException.ExceptionType.EndOfFile, "No more data available");
      }
      catch (IOException ex)
      {
        throw new TTransportException(TTransportException.ExceptionType.Unknown, ex.ToString());
      }
    }

    public override void Write(byte[] buf, int off, int len) => this.outputStream.Write(buf, off, len);

    public override void Flush()
    {
      try
      {
        this.SendRequest();
      }
      finally
      {
        this.outputStream = new MemoryStream();
      }
    }

    private void SendRequest()
    {
      try
      {
        HttpWebRequest request = this.CreateRequest();
        byte[] array = this.outputStream.ToArray();
        request.ContentLength = (long) array.Length;
        using (Stream requestStream = request.GetRequestStream())
        {
          requestStream.Write(array, 0, array.Length);
          using (WebResponse response = request.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              this.inputStream = (Stream) new MemoryStream();
              byte[] buffer = new byte[8096];
              int count;
              while ((count = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                this.inputStream.Write(buffer, 0, count);
              this.inputStream.Seek(0L, SeekOrigin.Begin);
            }
          }
        }
      }
      catch (IOException ex)
      {
        throw new TTransportException(TTransportException.ExceptionType.Unknown, ex.ToString());
      }
      catch (WebException ex)
      {
        throw new TTransportException(TTransportException.ExceptionType.Unknown, "Couldn't connect to server: " + (object) ex);
      }
    }

    private HttpWebRequest CreateRequest()
    {
      HttpWebRequest request = (HttpWebRequest) WebRequest.Create(this.uri);
      request.ClientCertificates.AddRange(this.certificates);
      if (this.connectTimeout > 0)
        request.Timeout = this.connectTimeout;
      if (this.readTimeout > 0)
        request.ReadWriteTimeout = this.readTimeout;
      request.ContentType = "application/x-thrift";
      request.Accept = "application/x-thrift";
      request.UserAgent = "C#/THttpClient";
      request.Method = "POST";
      request.ProtocolVersion = HttpVersion.Version10;
      foreach (KeyValuePair<string, string> customHeader in (IEnumerable<KeyValuePair<string, string>>) this.customHeaders)
        request.Headers.Add(customHeader.Key, customHeader.Value);
      request.Proxy = this.proxy;
      return request;
    }

    public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
    {
      byte[] array = this.outputStream.ToArray();
      try
      {
        THttpClient.FlushAsyncResult state1 = new THttpClient.FlushAsyncResult(callback, state);
        state1.Connection = this.CreateRequest();
        state1.Data = array;
        state1.Connection.BeginGetRequestStream(new AsyncCallback(this.GetRequestStreamCallback), (object) state1);
        return (IAsyncResult) state1;
      }
      catch (IOException ex)
      {
        throw new TTransportException(ex.ToString());
      }
    }

    public override void EndFlush(IAsyncResult asyncResult)
    {
      try
      {
        THttpClient.FlushAsyncResult flushAsyncResult = (THttpClient.FlushAsyncResult) asyncResult;
        if (!flushAsyncResult.IsCompleted)
        {
          WaitHandle asyncWaitHandle = flushAsyncResult.AsyncWaitHandle;
          asyncWaitHandle.WaitOne();
          asyncWaitHandle.Close();
        }
        if (flushAsyncResult.AsyncException != null)
          throw flushAsyncResult.AsyncException;
      }
      finally
      {
        this.outputStream = new MemoryStream();
      }
    }

    private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
    {
      THttpClient.FlushAsyncResult asyncState = (THttpClient.FlushAsyncResult) asynchronousResult.AsyncState;
      try
      {
        Stream requestStream = asyncState.Connection.EndGetRequestStream(asynchronousResult);
        requestStream.Write(asyncState.Data, 0, asyncState.Data.Length);
        requestStream.Flush();
        requestStream.Close();
        asyncState.Connection.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), (object) asyncState);
      }
      catch (Exception ex)
      {
        asyncState.AsyncException = new TTransportException(ex.ToString());
        asyncState.UpdateStatusToComplete();
        asyncState.NotifyCallbackWhenAvailable();
      }
    }

    private void GetResponseCallback(IAsyncResult asynchronousResult)
    {
      THttpClient.FlushAsyncResult asyncState = (THttpClient.FlushAsyncResult) asynchronousResult.AsyncState;
      try
      {
        this.inputStream = asyncState.Connection.EndGetResponse(asynchronousResult).GetResponseStream();
      }
      catch (Exception ex)
      {
        asyncState.AsyncException = new TTransportException(ex.ToString());
      }
      asyncState.UpdateStatusToComplete();
      asyncState.NotifyCallbackWhenAvailable();
    }

    protected override void Dispose(bool disposing)
    {
      if (!this._IsDisposed && disposing)
      {
        if (this.inputStream != null)
          this.inputStream.Dispose();
        if (this.outputStream != null)
          this.outputStream.Dispose();
      }
      this._IsDisposed = true;
    }

    private class FlushAsyncResult : IAsyncResult
    {
      private volatile bool _isCompleted;
      private ManualResetEvent _evt;
      private readonly AsyncCallback _cbMethod;
      private readonly object _state;
      private readonly object _locker = new object();

      public FlushAsyncResult(AsyncCallback cbMethod, object state)
      {
        this._cbMethod = cbMethod;
        this._state = state;
      }

      internal byte[] Data { get; set; }

      internal HttpWebRequest Connection { get; set; }

      internal TTransportException AsyncException { get; set; }

      public object AsyncState => this._state;

      public WaitHandle AsyncWaitHandle => (WaitHandle) this.GetEvtHandle();

      public bool CompletedSynchronously => false;

      public bool IsCompleted => this._isCompleted;

      private ManualResetEvent GetEvtHandle()
      {
        lock (this._locker)
        {
          if (this._evt == null)
            this._evt = new ManualResetEvent(false);
          if (this._isCompleted)
            this._evt.Set();
        }
        return this._evt;
      }

      internal void UpdateStatusToComplete()
      {
        this._isCompleted = true;
        lock (this._locker)
        {
          if (this._evt == null)
            return;
          this._evt.Set();
        }
      }

      internal void NotifyCallbackWhenAvailable()
      {
        if (this._cbMethod == null)
          return;
        this._cbMethod((IAsyncResult) this);
      }
    }
  }
}
