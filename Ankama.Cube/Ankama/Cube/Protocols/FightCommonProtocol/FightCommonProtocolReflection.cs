// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightCommonProtocol.FightCommonProtocolReflection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using System;

namespace Ankama.Cube.Protocols.FightCommonProtocol
{
  public static class FightCommonProtocolReflection
  {
    private static FileDescriptor descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChlmaWdodENvbW1vblByb3RvY29sLnByb3RvGh5nb29nbGUvcHJvdG9idWYv" + "d3JhcHBlcnMucHJvdG8aFGNvbW1vblByb3RvY29sLnByb3RvIq0BCg1TcGVs" + "bE1vdmVtZW50EhkKBXNwZWxsGAEgASgLMgouU3BlbGxJbmZvEiAKBGZyb20Y" + "AiABKA4yEi5TcGVsbE1vdmVtZW50Wm9uZRIeCgJ0bxgDIAEoDjISLlNwZWxs" + "TW92ZW1lbnRab25lEj8KG2Rpc2NhcmRlZEJlY2F1c2VIYW5kV2FzRnVsbBgE" + "IAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5Cb29sVmFsdWUijQEKCVNwZWxsSW5m" + "bxIXCg9zcGVsbEluc3RhbmNlSWQYASABKAUSNgoRc3BlbGxEZWZpbml0aW9u" + "SWQYAiABKAsyGy5nb29nbGUucHJvdG9idWYuSW50MzJWYWx1ZRIvCgpzcGVs" + "bExldmVsGAMgASgLMhsuZ29vZ2xlLnByb3RvYnVmLkludDMyVmFsdWUisQIK" + "CUZpZ2h0SW5mbxISCgpmaWdodERlZklkGAEgASgFEhIKCmZpZ2h0TWFwSWQY" + "AiABKAUSEQoJZmlnaHRUeXBlGAMgASgFEh0KFWNvbmN1cnJlbnRGaWdodHND" + "b3VudBgEIAEoBRISCgpvd25GaWdodElkGAUgASgFEhQKDG93blRlYW1JbmRl" + "eBgGIAEoBRIeCgV0ZWFtcxgHIAMoCzIPLkZpZ2h0SW5mby5UZWFtGioKBFRl" + "YW0SIgoHcGxheWVycxgBIAMoCzIRLkZpZ2h0SW5mby5QbGF5ZXIaVAoGUGxh" + "eWVyEgwKBG5hbWUYASABKAkSDQoFbGV2ZWwYAiABKAUSLQoId2VhcG9uSWQY" + "AyABKAsyGy5nb29nbGUucHJvdG9idWYuSW50MzJWYWx1ZSLqAQoOR2FtZVN0" + "YXRpc3RpY3MSMAoLcGxheWVyU3RhdHMYASADKAsyGy5HYW1lU3RhdGlzdGlj" + "cy5QbGF5ZXJTdGF0cxqlAQoLUGxheWVyU3RhdHMSEAoIcGxheWVySWQYASAB" + "KAUSDwoHZmlnaHRJZBgCIAEoBRI1CgVzdGF0cxgDIAMoCzImLkdhbWVTdGF0" + "aXN0aWNzLlBsYXllclN0YXRzLlN0YXRzRW50cnkSDgoGdGl0bGVzGAQgAygF" + "GiwKClN0YXRzRW50cnkSCwoDa2V5GAEgASgFEg0KBXZhbHVlGAIgASgFOgI4" + "ASo0ChFTcGVsbE1vdmVtZW50Wm9uZRILCgdOT1dIRVJFEAASCAoESEFORBAB" + "EggKBERFQ0sQAipEChVDb21wYW5pb25SZXNlcnZlU3RhdGUSCAoESURMRRAA" + "EgwKCElOX0ZJR0hUEAESCQoFR0lWRU4QAhIICgRERUFEEAMqbgocVGVhbXNT" + "Y29yZU1vZGlmaWNhdGlvblJlYXNvbhIRCg1GSVJTVF9WSUNUT1JZEAASDgoK" + "SEVST19ERUFUSBABEhMKD0NPTVBBTklPTl9ERUFUSBACEhYKEkhFUk9fTElG" + "RV9NT0RJRklFRBADQkMKFWNvbS5hbmthbWEuY3ViZS5wcm90b6oCKUFua2Ft" + "YS5DdWJlLlByb3RvY29scy5GaWdodENvbW1vblByb3RvY29sYgZwcm90bzM="), new FileDescriptor[2]
    {
      WrappersReflection.Descriptor,
      CommonProtocolReflection.Descriptor
    }, new GeneratedClrTypeInfo(new System.Type[3]
    {
      typeof (SpellMovementZone),
      typeof (CompanionReserveState),
      typeof (TeamsScoreModificationReason)
    }, new GeneratedClrTypeInfo[4]
    {
      new GeneratedClrTypeInfo(typeof (SpellMovement), (MessageParser) SpellMovement.Parser, new string[4]
      {
        "Spell",
        "From",
        "To",
        "DiscardedBecauseHandWasFull"
      }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
      new GeneratedClrTypeInfo(typeof (SpellInfo), (MessageParser) SpellInfo.Parser, new string[3]
      {
        "SpellInstanceId",
        "SpellDefinitionId",
        "SpellLevel"
      }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
      new GeneratedClrTypeInfo(typeof (FightInfo), (MessageParser) FightInfo.Parser, new string[7]
      {
        "FightDefId",
        "FightMapId",
        "FightType",
        "ConcurrentFightsCount",
        "OwnFightId",
        "OwnTeamIndex",
        "Teams"
      }, (string[]) null, (System.Type[]) null, new GeneratedClrTypeInfo[2]
      {
        new GeneratedClrTypeInfo(typeof (FightInfo.Types.Team), (MessageParser) FightInfo.Types.Team.Parser, new string[1]
        {
          "Players"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null),
        new GeneratedClrTypeInfo(typeof (FightInfo.Types.Player), (MessageParser) FightInfo.Types.Player.Parser, new string[3]
        {
          "Name",
          "Level",
          "WeaponId"
        }, (string[]) null, (System.Type[]) null, (GeneratedClrTypeInfo[]) null)
      }),
      new GeneratedClrTypeInfo(typeof (GameStatistics), (MessageParser) GameStatistics.Parser, new string[1]
      {
        "PlayerStats"
      }, (string[]) null, (System.Type[]) null, new GeneratedClrTypeInfo[1]
      {
        new GeneratedClrTypeInfo(typeof (GameStatistics.Types.PlayerStats), (MessageParser) GameStatistics.Types.PlayerStats.Parser, new string[4]
        {
          "PlayerId",
          "FightId",
          "Stats",
          "Titles"
        }, (string[]) null, (System.Type[]) null, new GeneratedClrTypeInfo[1])
      })
    }));

    public static FileDescriptor Descriptor => FightCommonProtocolReflection.descriptor;
  }
}
