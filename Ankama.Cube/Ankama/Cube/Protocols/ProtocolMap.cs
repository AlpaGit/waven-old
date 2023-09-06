// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.ProtocolMap
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.AdminCommandsProtocol;
using Ankama.Cube.Protocols.FightAdminProtocol;
using Ankama.Cube.Protocols.FightPreparationProtocol;
using Ankama.Cube.Protocols.FightProtocol;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.Protocols.ServerProtocol;
using Google.Protobuf;
using System.Collections.Generic;

namespace Ankama.Cube.Protocols
{
  public static class ProtocolMap
  {
    public static readonly Dictionary<int, MessageParser> parsers = new Dictionary<int, MessageParser>()
    {
      {
        -334659754,
        (MessageParser) AdminCmd.Parser
      },
      {
        60088106,
        (MessageParser) AdminCmdResultEvent.Parser
      },
      {
        1356709588,
        (MessageParser) AdminRequestCmd.Parser
      },
      {
        228994893,
        (MessageParser) AdminRequestCmd.Types.DealDamageAdminCmd.Parser
      },
      {
        422127927,
        (MessageParser) AdminRequestCmd.Types.DiscardSpellsCmd.Parser
      },
      {
        829273942,
        (MessageParser) AdminRequestCmd.Types.DrawSpellsCmd.Parser
      },
      {
        -900290262,
        (MessageParser) AdminRequestCmd.Types.GainActionPointsCmd.Parser
      },
      {
        -1517860085,
        (MessageParser) AdminRequestCmd.Types.GainElementPointsCmd.Parser
      },
      {
        -1467878125,
        (MessageParser) AdminRequestCmd.Types.GainReservePointsCmd.Parser
      },
      {
        -721119604,
        (MessageParser) AdminRequestCmd.Types.HealAdminCmd.Parser
      },
      {
        2001948744,
        (MessageParser) AdminRequestCmd.Types.InvokeCompanionAdminCmd.Parser
      },
      {
        442093669,
        (MessageParser) AdminRequestCmd.Types.InvokeSummoningAdminCmd.Parser
      },
      {
        1568562443,
        (MessageParser) AdminRequestCmd.Types.KillAdminCmd.Parser
      },
      {
        -1775539808,
        (MessageParser) AdminRequestCmd.Types.PickSpellCmd.Parser
      },
      {
        1232774193,
        (MessageParser) AdminRequestCmd.Types.SetElementaryStateAdminCmd.Parser
      },
      {
        1856834915,
        (MessageParser) AdminRequestCmd.Types.SetPropertyCmd.Parser
      },
      {
        1511543888,
        (MessageParser) AdminRequestCmd.Types.TeleportAdminCmd.Parser
      },
      {
        707793572,
        (MessageParser) ChangeGodCmd.Parser
      },
      {
        -2076582981,
        (MessageParser) ChangeGodResultEvent.Parser
      },
      {
        -1643305561,
        (MessageParser) CommandHandledEvent.Parser
      },
      {
        -1048243365,
        (MessageParser) CreateFightGroupCmd.Parser
      },
      {
        -1502625650,
        (MessageParser) DisconnectedByServerEvent.Parser
      },
      {
        584751914,
        (MessageParser) EndOfTurnCmd.Parser
      },
      {
        431325039,
        (MessageParser) FightEventsEvent.Parser
      },
      {
        -671859248,
        (MessageParser) FightGroupUpdatedEvent.Parser
      },
      {
        838914837,
        (MessageParser) FightInfoEvent.Parser
      },
      {
        -1344764902,
        (MessageParser) FightNotStartedEvent.Parser
      },
      {
        1919278337,
        (MessageParser) FightSnapshotEvent.Parser
      },
      {
        1887642534,
        (MessageParser) FightStartedEvent.Parser
      },
      {
        1931285371,
        (MessageParser) ForceMatchmakingAgainstAICmd.Parser
      },
      {
        -2108874322,
        (MessageParser) GetFightInfoCmd.Parser
      },
      {
        -36819535,
        (MessageParser) GetFightSnapshotCmd.Parser
      },
      {
        -295175818,
        (MessageParser) GetPlayerDataCmd.Parser
      },
      {
        -1701648494,
        (MessageParser) GiveCompanionCmd.Parser
      },
      {
        -843061208,
        (MessageParser) InvokeCompanionCmd.Parser
      },
      {
        -1578075766,
        (MessageParser) LaunchMatchmakingCmd.Parser
      },
      {
        917122295,
        (MessageParser) LaunchMatchmakingResultEvent.Parser
      },
      {
        -1827920358,
        (MessageParser) LeaveCmd.Parser
      },
      {
        1350973640,
        (MessageParser) LeaveFightGroupCmd.Parser
      },
      {
        1498448740,
        (MessageParser) MatchmakingStartedEvent.Parser
      },
      {
        -316303957,
        (MessageParser) MatchmakingStoppedEvent.Parser
      },
      {
        -1923321655,
        (MessageParser) MatchmakingSuccessEvent.Parser
      },
      {
        449275882,
        (MessageParser) MoveEntityCmd.Parser
      },
      {
        -706778754,
        (MessageParser) PlaySpellCmd.Parser
      },
      {
        1042687719,
        (MessageParser) PlayerDataEvent.Parser
      },
      {
        -316767217,
        (MessageParser) PlayerLeftFightEvent.Parser
      },
      {
        571100966,
        (MessageParser) PlayerReadyCmd.Parser
      },
      {
        1154842316,
        (MessageParser) RemoveDeckCmd.Parser
      },
      {
        -1414920903,
        (MessageParser) RemoveDeckResultEvent.Parser
      },
      {
        -932777990,
        (MessageParser) ResignCmd.Parser
      },
      {
        -1647034327,
        (MessageParser) SaveDeckCmd.Parser
      },
      {
        -679526722,
        (MessageParser) SaveDeckResultEvent.Parser
      },
      {
        -869270773,
        (MessageParser) SelectDeckAndWeaponCmd.Parser
      },
      {
        880299886,
        (MessageParser) SelectDeckAndWeaponResultEvent.Parser
      },
      {
        -317525609,
        (MessageParser) UseReserveCmd.Parser
      }
    };
    public static readonly Dictionary<System.Type, int> identifiers = new Dictionary<System.Type, int>()
    {
      {
        typeof (AdminCmd),
        -334659754
      },
      {
        typeof (AdminCmdResultEvent),
        60088106
      },
      {
        typeof (AdminRequestCmd),
        1356709588
      },
      {
        typeof (AdminRequestCmd.Types.DealDamageAdminCmd),
        228994893
      },
      {
        typeof (AdminRequestCmd.Types.DiscardSpellsCmd),
        422127927
      },
      {
        typeof (AdminRequestCmd.Types.DrawSpellsCmd),
        829273942
      },
      {
        typeof (AdminRequestCmd.Types.GainActionPointsCmd),
        -900290262
      },
      {
        typeof (AdminRequestCmd.Types.GainElementPointsCmd),
        -1517860085
      },
      {
        typeof (AdminRequestCmd.Types.GainReservePointsCmd),
        -1467878125
      },
      {
        typeof (AdminRequestCmd.Types.HealAdminCmd),
        -721119604
      },
      {
        typeof (AdminRequestCmd.Types.InvokeCompanionAdminCmd),
        2001948744
      },
      {
        typeof (AdminRequestCmd.Types.InvokeSummoningAdminCmd),
        442093669
      },
      {
        typeof (AdminRequestCmd.Types.KillAdminCmd),
        1568562443
      },
      {
        typeof (AdminRequestCmd.Types.PickSpellCmd),
        -1775539808
      },
      {
        typeof (AdminRequestCmd.Types.SetElementaryStateAdminCmd),
        1232774193
      },
      {
        typeof (AdminRequestCmd.Types.SetPropertyCmd),
        1856834915
      },
      {
        typeof (AdminRequestCmd.Types.TeleportAdminCmd),
        1511543888
      },
      {
        typeof (ChangeGodCmd),
        707793572
      },
      {
        typeof (ChangeGodResultEvent),
        -2076582981
      },
      {
        typeof (CommandHandledEvent),
        -1643305561
      },
      {
        typeof (CreateFightGroupCmd),
        -1048243365
      },
      {
        typeof (DisconnectedByServerEvent),
        -1502625650
      },
      {
        typeof (EndOfTurnCmd),
        584751914
      },
      {
        typeof (FightEventsEvent),
        431325039
      },
      {
        typeof (FightGroupUpdatedEvent),
        -671859248
      },
      {
        typeof (FightInfoEvent),
        838914837
      },
      {
        typeof (FightNotStartedEvent),
        -1344764902
      },
      {
        typeof (FightSnapshotEvent),
        1919278337
      },
      {
        typeof (FightStartedEvent),
        1887642534
      },
      {
        typeof (ForceMatchmakingAgainstAICmd),
        1931285371
      },
      {
        typeof (GetFightInfoCmd),
        -2108874322
      },
      {
        typeof (GetFightSnapshotCmd),
        -36819535
      },
      {
        typeof (GetPlayerDataCmd),
        -295175818
      },
      {
        typeof (GiveCompanionCmd),
        -1701648494
      },
      {
        typeof (InvokeCompanionCmd),
        -843061208
      },
      {
        typeof (LaunchMatchmakingCmd),
        -1578075766
      },
      {
        typeof (LaunchMatchmakingResultEvent),
        917122295
      },
      {
        typeof (LeaveCmd),
        -1827920358
      },
      {
        typeof (LeaveFightGroupCmd),
        1350973640
      },
      {
        typeof (MatchmakingStartedEvent),
        1498448740
      },
      {
        typeof (MatchmakingStoppedEvent),
        -316303957
      },
      {
        typeof (MatchmakingSuccessEvent),
        -1923321655
      },
      {
        typeof (MoveEntityCmd),
        449275882
      },
      {
        typeof (PlaySpellCmd),
        -706778754
      },
      {
        typeof (PlayerDataEvent),
        1042687719
      },
      {
        typeof (PlayerLeftFightEvent),
        -316767217
      },
      {
        typeof (PlayerReadyCmd),
        571100966
      },
      {
        typeof (RemoveDeckCmd),
        1154842316
      },
      {
        typeof (RemoveDeckResultEvent),
        -1414920903
      },
      {
        typeof (ResignCmd),
        -932777990
      },
      {
        typeof (SaveDeckCmd),
        -1647034327
      },
      {
        typeof (SaveDeckResultEvent),
        -679526722
      },
      {
        typeof (SelectDeckAndWeaponCmd),
        -869270773
      },
      {
        typeof (SelectDeckAndWeaponResultEvent),
        880299886
      },
      {
        typeof (UseReserveCmd),
        -317525609
      }
    };
  }
}
