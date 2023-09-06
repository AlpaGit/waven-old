// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightAdminProtocol.FightAdminProtocolReflection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;

namespace Ankama.Cube.Protocols.FightAdminProtocol
{
  public static class FightAdminProtocolReflection
  {
    private static FileDescriptor descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChhmaWdodEFkbWluUHJvdG9jb2wucHJvdG8aFGNvbW1vblByb3RvY29sLnBy" + "b3RvIr4PCg9BZG1pblJlcXVlc3RDbWQSOQoKZGVhbERhbWFnZRgBIAEoCzIj" + "LkFkbWluUmVxdWVzdENtZC5EZWFsRGFtYWdlQWRtaW5DbWRIABItCgRraWxs" + "GAIgASgLMh0uQWRtaW5SZXF1ZXN0Q21kLktpbGxBZG1pbkNtZEgAEjUKCHRl" + "bGVwb3J0GAMgASgLMiEuQWRtaW5SZXF1ZXN0Q21kLlRlbGVwb3J0QWRtaW5D" + "bWRIABI0CgpkcmF3U3BlbGxzGAQgASgLMh4uQWRtaW5SZXF1ZXN0Q21kLkRy" + "YXdTcGVsbHNDbWRIABI6Cg1kaXNjYXJkU3BlbGxzGAUgASgLMiEuQWRtaW5S" + "ZXF1ZXN0Q21kLkRpc2NhcmRTcGVsbHNDbWRIABJCChFnYWluRWxlbWVudFBv" + "aW50cxgGIAEoCzIlLkFkbWluUmVxdWVzdENtZC5HYWluRWxlbWVudFBvaW50" + "c0NtZEgAEkAKEGdhaW5BY3Rpb25Qb2ludHMYByABKAsyJC5BZG1pblJlcXVl" + "c3RDbWQuR2FpbkFjdGlvblBvaW50c0NtZEgAEkIKEWdhaW5SZXNlcnZlUG9p" + "bnRzGAggASgLMiUuQWRtaW5SZXF1ZXN0Q21kLkdhaW5SZXNlcnZlUG9pbnRz" + "Q21kSAASMgoJcGlja1NwZWxsGAkgASgLMh0uQWRtaW5SZXF1ZXN0Q21kLlBp" + "Y2tTcGVsbENtZEgAEjYKC3NldFByb3BlcnR5GAogASgLMh8uQWRtaW5SZXF1" + "ZXN0Q21kLlNldFByb3BlcnR5Q21kSAASLQoEaGVhbBgLIAEoCzIdLkFkbWlu" + "UmVxdWVzdENtZC5IZWFsQWRtaW5DbWRIABJDCg9pbnZva2VTdW1tb25pbmcY" + "DCABKAsyKC5BZG1pblJlcXVlc3RDbWQuSW52b2tlU3VtbW9uaW5nQWRtaW5D" + "bWRIABJDCg9pbnZva2VDb21wYW5pb24YDSABKAsyKC5BZG1pblJlcXVlc3RD" + "bWQuSW52b2tlQ29tcGFuaW9uQWRtaW5DbWRIABJJChJzZXRFbGVtZW50YXJ5" + "U3RhdGUYDiABKAsyKy5BZG1pblJlcXVlc3RDbWQuU2V0RWxlbWVudGFyeVN0" + "YXRlQWRtaW5DbWRIABpMCg5TZXRQcm9wZXJ0eUNtZBIWCg50YXJnZXRFbnRp" + "dHlJZBgBIAEoBRISCgpwcm9wZXJ0eUlkGAIgASgFEg4KBmFjdGl2ZRgDIAEo" + "CBpPChpTZXRFbGVtZW50YXJ5U3RhdGVBZG1pbkNtZBIWCg50YXJnZXRFbnRp" + "dHlJZBgBIAEoBRIZChFlbGVtZW50YXJ5U3RhdGVJZBgCIAEoBRpPChJEZWFs" + "RGFtYWdlQWRtaW5DbWQSFgoOdGFyZ2V0RW50aXR5SWQYASABKAUSEAoIcXVh" + "bnRpdHkYAiABKAUSDwoHbWFnaWNhbBgDIAEoCBomCgxLaWxsQWRtaW5DbWQS" + "FgoOdGFyZ2V0RW50aXR5SWQYASABKAUaSwoQVGVsZXBvcnRBZG1pbkNtZBIW" + "Cg50YXJnZXRFbnRpdHlJZBgBIAEoBRIfCgtkZXN0aW5hdGlvbhgCIAEoCzIK" + "LkNlbGxDb29yZBo5Cg1EcmF3U3BlbGxzQ21kEhYKDnBsYXllckVudGl0eUlk" + "GAEgASgFEhAKCHF1YW50aXR5GAIgASgFGjwKEERpc2NhcmRTcGVsbHNDbWQS" + "FgoOcGxheWVyRW50aXR5SWQYASABKAUSEAoIcXVhbnRpdHkYAiABKAUaQAoU" + "R2FpbkVsZW1lbnRQb2ludHNDbWQSFgoOcGxheWVyRW50aXR5SWQYASABKAUS" + "EAoIcXVhbnRpdHkYAiABKAUaPwoTR2FpbkFjdGlvblBvaW50c0NtZBIWCg5w" + "bGF5ZXJFbnRpdHlJZBgBIAEoBRIQCghxdWFudGl0eRgCIAEoBRpAChRHYWlu" + "UmVzZXJ2ZVBvaW50c0NtZBIWCg5wbGF5ZXJFbnRpdHlJZBgBIAEoBRIQCghx" + "dWFudGl0eRgCIAEoBRpnCgxQaWNrU3BlbGxDbWQSFgoOcGxheWVyRW50aXR5" + "SWQYASABKAUSEAoIcXVhbnRpdHkYAiABKAUSGQoRc3BlbGxEZWZpbml0aW9u" + "SWQYAyABKAUSEgoKc3BlbGxMZXZlbBgEIAEoBRpJCgxIZWFsQWRtaW5DbWQS" + "FgoOdGFyZ2V0RW50aXR5SWQYASABKAUSEAoIcXVhbnRpdHkYAiABKAUSDwoH" + "bWFnaWNhbBgDIAEoCBp/ChdJbnZva2VTdW1tb25pbmdBZG1pbkNtZBIUCgxk" + "ZWZpbml0aW9uSWQYASABKAUSFQoNb3duZXJFbnRpdHlJZBgCIAEoBRIWCg5z" + "dW1tb25pbmdMZXZlbBgDIAEoBRIfCgtkZXN0aW5hdGlvbhgEIAEoCzIKLkNl" + "bGxDb29yZBp/ChdJbnZva2VDb21wYW5pb25BZG1pbkNtZBIUCgxkZWZpbml0" + "aW9uSWQYASABKAUSFQoNb3duZXJFbnRpdHlJZBgCIAEoBRIWCg5jb21wYW5p" + "b25MZXZlbBgDIAEoBRIfCgtkZXN0aW5hdGlvbhgEIAEoCzIKLkNlbGxDb29y" + "ZEIFCgNjbWRCQgoVY29tLmFua2FtYS5jdWJlLnByb3RvqgIoQW5rYW1hLkN1" + "YmUuUHJvdG9jb2xzLkZpZ2h0QWRtaW5Qcm90b2NvbGIGcHJvdG8z"), new FileDescriptor[1]
    {
      CommonProtocolReflection.Descriptor
    }, new GeneratedClrTypeInfo((System.Type[]) null, new GeneratedClrTypeInfo[1]
    {
      new GeneratedClrTypeInfo(typeof (AdminRequestCmd), (MessageParser) AdminRequestCmd.Parser, new string[14]
      {
        "DealDamage",
        "Kill",
        "Teleport",
        "DrawSpells",
        "DiscardSpells",
        "GainElementPoints",
        "GainActionPoints",
        "GainReservePoints",
        "PickSpell",
        "SetProperty",
        "Heal",
        "InvokeSummoning",
        "InvokeCompanion",
        "SetElementaryState"
      }, new string[1]{ "Cmd" }, (System.Type[]) null, new GeneratedClrTypeInfo[14]
      {
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.SetPropertyCmd), (MessageParser) AdminRequestCmd.Types.SetPropertyCmd.Parser, new string[3]
        {
          "TargetEntityId",
          "PropertyId",
          "Active"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.SetElementaryStateAdminCmd), (MessageParser) AdminRequestCmd.Types.SetElementaryStateAdminCmd.Parser, new string[2]
        {
          "TargetEntityId",
          "ElementaryStateId"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.DealDamageAdminCmd), (MessageParser) AdminRequestCmd.Types.DealDamageAdminCmd.Parser, new string[3]
        {
          "TargetEntityId",
          "Quantity",
          "Magical"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.KillAdminCmd), (MessageParser) AdminRequestCmd.Types.KillAdminCmd.Parser, new string[1]
        {
          "TargetEntityId"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.TeleportAdminCmd), (MessageParser) AdminRequestCmd.Types.TeleportAdminCmd.Parser, new string[2]
        {
          "TargetEntityId",
          "Destination"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.DrawSpellsCmd), (MessageParser) AdminRequestCmd.Types.DrawSpellsCmd.Parser, new string[2]
        {
          "PlayerEntityId",
          "Quantity"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.DiscardSpellsCmd), (MessageParser) AdminRequestCmd.Types.DiscardSpellsCmd.Parser, new string[2]
        {
          "PlayerEntityId",
          "Quantity"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.GainElementPointsCmd), (MessageParser) AdminRequestCmd.Types.GainElementPointsCmd.Parser, new string[2]
        {
          "PlayerEntityId",
          "Quantity"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.GainActionPointsCmd), (MessageParser) AdminRequestCmd.Types.GainActionPointsCmd.Parser, new string[2]
        {
          "PlayerEntityId",
          "Quantity"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.GainReservePointsCmd), (MessageParser) AdminRequestCmd.Types.GainReservePointsCmd.Parser, new string[2]
        {
          "PlayerEntityId",
          "Quantity"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.PickSpellCmd), (MessageParser) AdminRequestCmd.Types.PickSpellCmd.Parser, new string[4]
        {
          "PlayerEntityId",
          "Quantity",
          "SpellDefinitionId",
          "SpellLevel"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.HealAdminCmd), (MessageParser) AdminRequestCmd.Types.HealAdminCmd.Parser, new string[3]
        {
          "TargetEntityId",
          "Quantity",
          "Magical"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.InvokeSummoningAdminCmd), (MessageParser) AdminRequestCmd.Types.InvokeSummoningAdminCmd.Parser, new string[4]
        {
          "DefinitionId",
          "OwnerEntityId",
          "SummoningLevel",
          "Destination"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (AdminRequestCmd.Types.InvokeCompanionAdminCmd), (MessageParser) AdminRequestCmd.Types.InvokeCompanionAdminCmd.Parser, new string[4]
        {
          "DefinitionId",
          "OwnerEntityId",
          "CompanionLevel",
          "Destination"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null)
      })
    }));

    public static FileDescriptor Descriptor => FightAdminProtocolReflection.descriptor;
  }
}
