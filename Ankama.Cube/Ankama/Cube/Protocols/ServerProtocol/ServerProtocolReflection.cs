// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.ServerProtocol.ServerProtocolReflection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;

namespace Ankama.Cube.Protocols.ServerProtocol
{
  public static class ServerProtocolReflection
  {
    private static FileDescriptor descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRzZXJ2ZXJQcm90b2NvbC5wcm90byLBAQoZRGlzY29ubmVjdGVkQnlTZXJ2" + "ZXJFdmVudBIxCgZyZWFzb24YASABKA4yIS5EaXNjb25uZWN0ZWRCeVNlcnZl" + "ckV2ZW50LlJlYXNvbiJxCgZSZWFzb24SCwoHVW5rbm93bhAAEgkKBUVycm9y" + "EAESFAoQU2VydmVySXNTdG9wcGluZxACEhcKE1VuYWJsZVRvTG9hZEFjY291" + "bnQQAxIgChxMb2dnZWRJbkFnYWluV2l0aFNhbWVBY2NvdW50EARCPgoVY29t" + "LmFua2FtYS5jdWJlLnByb3RvqgIkQW5rYW1hLkN1YmUuUHJvdG9jb2xzLlNl" + "cnZlclByb3RvY29sYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo((System.Type[]) null, new GeneratedClrTypeInfo[1]
    {
      new GeneratedClrTypeInfo(typeof (DisconnectedByServerEvent), (MessageParser) DisconnectedByServerEvent.Parser, new string[1]
      {
        "Reason"
      }, (string[]) null, new System.Type[1]
      {
        typeof (DisconnectedByServerEvent.Types.Reason)
      }, (GeneratedClrTypeInfo[]) null)
    }));

    public static FileDescriptor Descriptor => ServerProtocolReflection.descriptor;
  }
}
