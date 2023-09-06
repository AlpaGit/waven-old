// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.CommonProtocol.CommonProtocolReflection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using System;

namespace Ankama.Cube.Protocols.CommonProtocol
{
  public static class CommonProtocolReflection
  {
    private static FileDescriptor descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRjb21tb25Qcm90b2NvbC5wcm90bxoeZ29vZ2xlL3Byb3RvYnVmL3dyYXBw" + "ZXJzLnByb3RvIkUKCkNhc3RUYXJnZXQSGgoEY2VsbBgBIAEoCzIKLkNlbGxD" + "b29yZEgAEhIKCGVudGl0eUlkGAIgASgFSABCBwoFdmFsdWUiIQoJQ2VsbENv" + "b3JkEgkKAXgYASABKAUSCQoBeRgCIAEoBSokCglDbWRSZXN1bHQSCgoGRmFp" + "bGVkEAASCwoHU3VjY2VzcxABKo8BChNEYW1hZ2VSZWR1Y3Rpb25UeXBlEgsK" + "B1VOS05PV04QABIKCgZTSElFTEQQARILCgdDT1VOVEVSEAISDQoJUFJPVEVD" + "VE9SEAMSDgoKUkVGTEVDVElPThAEEhAKDERBTUFHRV9QUk9PRhAFEg4KClJF" + "U0lTVEFOQ0UQBhIRCg1QRVRSSUZJQ0FUSU9OEAcqYgoMTW92ZW1lbnRUeXBl" + "EhoKFk1PVkVNRU5UX1RZUEVfTk9UX1VTRUQQABIICgRXQUxLEAESBwoDUlVO" + "EAISDAoIVEVMRVBPUlQQAxIJCgVTTElERRAEEgoKBkFUVEFDSxAFKj4KDVZh" + "bHVlTW9kaWZpZXISGwoXVkFMVUVfTU9ESUZJRVJfTk9UX1VTRUQQABIHCgNB" + "REQQARIHCgNTRVQQAiowCgtGaWdodFJlc3VsdBIICgREcmF3EAASCwoHVmlj" + "dG9yeRABEgoKBkRlZmVhdBACQj4KFWNvbS5hbmthbWEuY3ViZS5wcm90b6oC" + "JEFua2FtYS5DdWJlLlByb3RvY29scy5Db21tb25Qcm90b2NvbGIGcHJvdG8z"), new FileDescriptor[1]
    {
      WrappersReflection.Descriptor
    }, new GeneratedClrTypeInfo(new System.Type[5]
    {
      typeof (CmdResult),
      typeof (DamageReductionType),
      typeof (MovementType),
      typeof (ValueModifier),
      typeof (FightResult)
    }, new GeneratedClrTypeInfo[2]
    {
      new GeneratedClrTypeInfo(typeof (CastTarget), (MessageParser) CastTarget.Parser, new string[2]
      {
        "Cell",
        "EntityId"
      }, new string[1]{ "Value" }, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
      new GeneratedClrTypeInfo(typeof (CellCoord), (MessageParser) CellCoord.Parser, new string[2]
      {
        "X",
        "Y"
      }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null)
    }));

    public static FileDescriptor Descriptor => CommonProtocolReflection.descriptor;
  }
}
