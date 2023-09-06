// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.THttpHandler
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Thrift.Protocol;

namespace Thrift.Transport
{
  public class THttpHandler : IHttpHandler
  {
    protected TProcessor processor;
    protected TProtocolFactory inputProtocolFactory;
    protected TProtocolFactory outputProtocolFactory;
    protected const string contentType = "application/x-thrift";
    protected Encoding encoding = Encoding.UTF8;

    public THttpHandler(TProcessor processor)
      : this(processor, (TProtocolFactory) new TBinaryProtocol.Factory())
    {
    }

    public THttpHandler(TProcessor processor, TProtocolFactory protocolFactory)
      : this(processor, protocolFactory, protocolFactory)
    {
    }

    public THttpHandler(
      TProcessor processor,
      TProtocolFactory inputProtocolFactory,
      TProtocolFactory outputProtocolFactory)
    {
      this.processor = processor;
      this.inputProtocolFactory = inputProtocolFactory;
      this.outputProtocolFactory = outputProtocolFactory;
    }

    public void ProcessRequest(HttpListenerContext context)
    {
      context.Response.ContentType = "application/x-thrift";
      context.Response.ContentEncoding = this.encoding;
      this.ProcessRequest(context.Request.InputStream, context.Response.OutputStream);
    }

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "application/x-thrift";
      context.Response.ContentEncoding = this.encoding;
      this.ProcessRequest(context.Request.InputStream, context.Response.OutputStream);
    }

    public void ProcessRequest(Stream input, Stream output)
    {
      TTransport trans = (TTransport) new TStreamTransport(input, output);
      try
      {
        TProtocol protocol1 = this.inputProtocolFactory.GetProtocol(trans);
        TProtocol protocol2 = this.outputProtocolFactory.GetProtocol(trans);
        do
          ;
        while (this.processor.Process(protocol1, protocol2));
      }
      catch (TTransportException ex)
      {
      }
      finally
      {
        trans.Close();
      }
    }

    public bool IsReusable => true;
  }
}
