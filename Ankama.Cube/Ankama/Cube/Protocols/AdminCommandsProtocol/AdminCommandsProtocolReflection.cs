// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.AdminCommandsProtocol.AdminCommandsProtocolReflection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;

namespace Ankama.Cube.Protocols.AdminCommandsProtocol
{
  public static class AdminCommandsProtocolReflection
  {
    private static FileDescriptor descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChthZG1pbkNvbW1hbmRzUHJvdG9jb2wucHJvdG8i/QMKCEFkbWluQ21kEgoK" + "AmlkGAEgASgFEhsKEWdpdmVBbGxDb21wYW5pb25zGAIgASgISAASGAoOZ2l2" + "ZUFsbFdlYXBvbnMYAyABKAhIABIsCg5zZXRXZWFwb25MZXZlbBgEIAEoCzIS" + "LkFkbWluQ21kLlNldExldmVsSAASNAoSc2V0QWxsV2VhcG9uTGV2ZWxzGAUg" + "ASgLMhYuQWRtaW5DbWQuU2V0QWxsTGV2ZWxzSAASKAoJc2V0R2VuZGVyGAYg" + "ASgLMhMuQWRtaW5DbWQuU2V0R2VuZGVySAASPgoUZ2V0Q2x1c3RlclN0YXRp" + "c3RpY3MYByABKAsyHi5BZG1pbkNtZC5HZXRDbHVzdGVyU3RhdGlzdGljc0gA" + "GiUKCFNldExldmVsEgoKAmlkGAEgASgFEg0KBWxldmVsGAIgASgFGh0KDFNl" + "dEFsbExldmVscxINCgVsZXZlbBgBIAEoBRobCglTZXRHZW5kZXISDgoGZ2Vu" + "ZGVyGAEgASgFGnYKFEdldENsdXN0ZXJTdGF0aXN0aWNzEhMKC2ZpZ2h0c0Nv" + "dW50GAEgASgIEhkKEWNvbm5lY3Rpb25zQ291bnRzGAIgASgIEhwKFHBsYXll" + "cnNFbnRpdGllc0NvdW50GAMgASgIEhAKCGRldGFpbGVkGAQgASgIQgUKA2Nt" + "ZCJCChNBZG1pbkNtZFJlc3VsdEV2ZW50EgoKAmlkGAEgASgFEg8KB3N1Y2Nl" + "c3MYAiABKAgSDgoGcmVzdWx0GAMgASgJQkUKFWNvbS5hbmthbWEuY3ViZS5w" + "cm90b6oCK0Fua2FtYS5DdWJlLlByb3RvY29scy5BZG1pbkNvbW1hbmRzUHJv" + "dG9jb2xiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo((System.Type[]) null, new GeneratedClrTypeInfo[2]
    {
      new GeneratedClrTypeInfo(typeof (AdminCmd), (MessageParser) AdminCmd.Parser, new string[7]
      {
        "Id",
        "GiveAllCompanions",
        "GiveAllWeapons",
        "SetWeaponLevel",
        "SetAllWeaponLevels",
        "SetGender",
        "GetClusterStatistics"
      }, new string[1]{ "Cmd" }, (System.Type[]) null, new GeneratedClrTypeInfo[4]
      {
        new GeneratedClrTypeInfo(typeof (AdminCmd.Types.SetLevel), (MessageParser) AdminCmd.Types.SetLevel.Parser, new string[2]
        {
          "Id",
          "Level"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminCmd.Types.SetAllLevels), (MessageParser) AdminCmd.Types.SetAllLevels.Parser, new string[1]
        {
          "Level"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminCmd.Types.SetGender), (MessageParser) AdminCmd.Types.SetGender.Parser, new string[1]
        {
          "Gender"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminCmd.Types.GetClusterStatistics), (MessageParser) AdminCmd.Types.GetClusterStatistics.Parser, new string[4]
        {
          "FightsCount",
          "ConnectionsCounts",
          "PlayersEntitiesCount",
          "Detailed"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null)
      }),
      new GeneratedClrTypeInfo(typeof (AdminCmdResultEvent), (MessageParser) AdminCmdResultEvent.Parser, new string[3]
      {
        "Id",
        "Success",
        "Result"
      }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null)
    }));

    public static FileDescriptor Descriptor => AdminCommandsProtocolReflection.descriptor;
  }
}
