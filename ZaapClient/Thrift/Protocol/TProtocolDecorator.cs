// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TProtocolDecorator
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public abstract class TProtocolDecorator : TProtocol
  {
    private TProtocol WrappedProtocol;

    public TProtocolDecorator(TProtocol protocol)
      : base(protocol.Transport)
    {
      this.WrappedProtocol = protocol;
    }

    public override void WriteMessageBegin(TMessage tMessage) => this.WrappedProtocol.WriteMessageBegin(tMessage);

    public override void WriteMessageEnd() => this.WrappedProtocol.WriteMessageEnd();

    public override void WriteStructBegin(TStruct tStruct) => this.WrappedProtocol.WriteStructBegin(tStruct);

    public override void WriteStructEnd() => this.WrappedProtocol.WriteStructEnd();

    public override void WriteFieldBegin(TField tField) => this.WrappedProtocol.WriteFieldBegin(tField);

    public override void WriteFieldEnd() => this.WrappedProtocol.WriteFieldEnd();

    public override void WriteFieldStop() => this.WrappedProtocol.WriteFieldStop();

    public override void WriteMapBegin(TMap tMap) => this.WrappedProtocol.WriteMapBegin(tMap);

    public override void WriteMapEnd() => this.WrappedProtocol.WriteMapEnd();

    public override void WriteListBegin(TList tList) => this.WrappedProtocol.WriteListBegin(tList);

    public override void WriteListEnd() => this.WrappedProtocol.WriteListEnd();

    public override void WriteSetBegin(TSet tSet) => this.WrappedProtocol.WriteSetBegin(tSet);

    public override void WriteSetEnd() => this.WrappedProtocol.WriteSetEnd();

    public override void WriteBool(bool b) => this.WrappedProtocol.WriteBool(b);

    public override void WriteByte(sbyte b) => this.WrappedProtocol.WriteByte(b);

    public override void WriteI16(short i) => this.WrappedProtocol.WriteI16(i);

    public override void WriteI32(int i) => this.WrappedProtocol.WriteI32(i);

    public override void WriteI64(long l) => this.WrappedProtocol.WriteI64(l);

    public override void WriteDouble(double v) => this.WrappedProtocol.WriteDouble(v);

    public override void WriteString(string s) => this.WrappedProtocol.WriteString(s);

    public override void WriteBinary(byte[] bytes) => this.WrappedProtocol.WriteBinary(bytes);

    public override TMessage ReadMessageBegin() => this.WrappedProtocol.ReadMessageBegin();

    public override void ReadMessageEnd() => this.WrappedProtocol.ReadMessageEnd();

    public override TStruct ReadStructBegin() => this.WrappedProtocol.ReadStructBegin();

    public override void ReadStructEnd() => this.WrappedProtocol.ReadStructEnd();

    public override TField ReadFieldBegin() => this.WrappedProtocol.ReadFieldBegin();

    public override void ReadFieldEnd() => this.WrappedProtocol.ReadFieldEnd();

    public override TMap ReadMapBegin() => this.WrappedProtocol.ReadMapBegin();

    public override void ReadMapEnd() => this.WrappedProtocol.ReadMapEnd();

    public override TList ReadListBegin() => this.WrappedProtocol.ReadListBegin();

    public override void ReadListEnd() => this.WrappedProtocol.ReadListEnd();

    public override TSet ReadSetBegin() => this.WrappedProtocol.ReadSetBegin();

    public override void ReadSetEnd() => this.WrappedProtocol.ReadSetEnd();

    public override bool ReadBool() => this.WrappedProtocol.ReadBool();

    public override sbyte ReadByte() => this.WrappedProtocol.ReadByte();

    public override short ReadI16() => this.WrappedProtocol.ReadI16();

    public override int ReadI32() => this.WrappedProtocol.ReadI32();

    public override long ReadI64() => this.WrappedProtocol.ReadI64();

    public override double ReadDouble() => this.WrappedProtocol.ReadDouble();

    public override string ReadString() => this.WrappedProtocol.ReadString();

    public override byte[] ReadBinary() => this.WrappedProtocol.ReadBinary();
  }
}
