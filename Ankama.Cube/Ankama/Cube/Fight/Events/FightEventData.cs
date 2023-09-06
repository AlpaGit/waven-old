// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightEventData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ankama.Cube.Fight.Events
{
  public sealed class FightEventData : 
    IMessage<FightEventData>,
    IMessage,
    IEquatable<FightEventData>,
    IDeepCloneable<FightEventData>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<FightEventData> _parser = new MessageParser<FightEventData>((Func<FightEventData>) (() => new FightEventData()));
    private UnknownFieldSet _unknownFields;
    public const int EventTypeFieldNumber = 1;
    private FightEventData.Types.EventType eventType_;
    public const int EventIdFieldNumber = 2;
    private int eventId_;
    public const int ParentEventIdFieldNumber = 3;
    private static readonly FieldCodec<int?> _single_parentEventId_codec = FieldCodec.ForStructWrapper<int>(26U);
    private int? parentEventId_;
    public const int Int1FieldNumber = 4;
    private int int1_;
    public const int Int2FieldNumber = 5;
    private int int2_;
    public const int Int3FieldNumber = 6;
    private int int3_;
    public const int Int4FieldNumber = 7;
    private int int4_;
    public const int Int5FieldNumber = 8;
    private int int5_;
    public const int Int6FieldNumber = 9;
    private int int6_;
    public const int Int7FieldNumber = 10;
    private int int7_;
    public const int String1FieldNumber = 11;
    private string string1_ = "";
    public const int Bool1FieldNumber = 12;
    private bool bool1_;
    public const int CellCoord1FieldNumber = 13;
    private CellCoord cellCoord1_;
    public const int CellCoord2FieldNumber = 14;
    private CellCoord cellCoord2_;
    public const int CompanionReserveState1FieldNumber = 15;
    private CompanionReserveState companionReserveState1_;
    public const int CompanionReserveState2FieldNumber = 16;
    private CompanionReserveState companionReserveState2_;
    public const int DamageReductionType1FieldNumber = 17;
    private DamageReductionType damageReductionType1_;
    public const int FightResult1FieldNumber = 18;
    private FightResult fightResult1_;
    public const int GameStatistics1FieldNumber = 19;
    private GameStatistics gameStatistics1_;
    public const int TeamsScoreModificationReason1FieldNumber = 20;
    private TeamsScoreModificationReason teamsScoreModificationReason1_;
    public const int OptInt1FieldNumber = 21;
    private static readonly FieldCodec<int?> _single_optInt1_codec = FieldCodec.ForStructWrapper<int>(170U);
    private int? optInt1_;
    public const int OptInt2FieldNumber = 22;
    private static readonly FieldCodec<int?> _single_optInt2_codec = FieldCodec.ForStructWrapper<int>(178U);
    private int? optInt2_;
    public const int OptInt3FieldNumber = 23;
    private static readonly FieldCodec<int?> _single_optInt3_codec = FieldCodec.ForStructWrapper<int>(186U);
    private int? optInt3_;
    public const int OptInt4FieldNumber = 24;
    private static readonly FieldCodec<int?> _single_optInt4_codec = FieldCodec.ForStructWrapper<int>(194U);
    private int? optInt4_;
    public const int CellCoordList1FieldNumber = 25;
    private static readonly FieldCodec<CellCoord> _repeated_cellCoordList1_codec = FieldCodec.ForMessage<CellCoord>(202U, CellCoord.Parser);
    private readonly RepeatedField<CellCoord> cellCoordList1_ = new RepeatedField<CellCoord>();
    public const int SpellMovementList1FieldNumber = 26;
    private static readonly FieldCodec<SpellMovement> _repeated_spellMovementList1_codec = FieldCodec.ForMessage<SpellMovement>(210U, SpellMovement.Parser);
    private readonly RepeatedField<SpellMovement> spellMovementList1_ = new RepeatedField<SpellMovement>();
    public const int CastTargetList1FieldNumber = 27;
    private static readonly FieldCodec<CastTarget> _repeated_castTargetList1_codec = FieldCodec.ForMessage<CastTarget>(218U, CastTarget.Parser);
    private readonly RepeatedField<CastTarget> castTargetList1_ = new RepeatedField<CastTarget>();
    public const int IntList1FieldNumber = 28;
    private static readonly FieldCodec<int> _repeated_intList1_codec = FieldCodec.ForInt32(226U);
    private readonly RepeatedField<int> intList1_ = new RepeatedField<int>();
    public const int IntList2FieldNumber = 29;
    private static readonly FieldCodec<int> _repeated_intList2_codec = FieldCodec.ForInt32(234U);
    private readonly RepeatedField<int> intList2_ = new RepeatedField<int>();

    [DebuggerNonUserCode]
    public static MessageParser<FightEventData> Parser => FightEventData._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => EventsReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => FightEventData.Descriptor;

    [DebuggerNonUserCode]
    public FightEventData()
    {
    }

    [DebuggerNonUserCode]
    public FightEventData(FightEventData other)
      : this()
    {
      this.eventType_ = other.eventType_;
      this.eventId_ = other.eventId_;
      this.ParentEventId = other.ParentEventId;
      this.int1_ = other.int1_;
      this.int2_ = other.int2_;
      this.int3_ = other.int3_;
      this.int4_ = other.int4_;
      this.int5_ = other.int5_;
      this.int6_ = other.int6_;
      this.int7_ = other.int7_;
      this.string1_ = other.string1_;
      this.bool1_ = other.bool1_;
      this.cellCoord1_ = other.cellCoord1_ != null ? other.cellCoord1_.Clone() : (CellCoord) null;
      this.cellCoord2_ = other.cellCoord2_ != null ? other.cellCoord2_.Clone() : (CellCoord) null;
      this.companionReserveState1_ = other.companionReserveState1_;
      this.companionReserveState2_ = other.companionReserveState2_;
      this.damageReductionType1_ = other.damageReductionType1_;
      this.fightResult1_ = other.fightResult1_;
      this.gameStatistics1_ = other.gameStatistics1_ != null ? other.gameStatistics1_.Clone() : (GameStatistics) null;
      this.teamsScoreModificationReason1_ = other.teamsScoreModificationReason1_;
      this.OptInt1 = other.OptInt1;
      this.OptInt2 = other.OptInt2;
      this.OptInt3 = other.OptInt3;
      this.OptInt4 = other.OptInt4;
      this.cellCoordList1_ = other.cellCoordList1_.Clone();
      this.spellMovementList1_ = other.spellMovementList1_.Clone();
      this.castTargetList1_ = other.castTargetList1_.Clone();
      this.intList1_ = other.intList1_.Clone();
      this.intList2_ = other.intList2_.Clone();
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public FightEventData Clone() => new FightEventData(this);

    [DebuggerNonUserCode]
    public FightEventData.Types.EventType EventType
    {
      get => this.eventType_;
      set => this.eventType_ = value;
    }

    [DebuggerNonUserCode]
    public int EventId
    {
      get => this.eventId_;
      set => this.eventId_ = value;
    }

    [DebuggerNonUserCode]
    public int? ParentEventId
    {
      get => this.parentEventId_;
      set => this.parentEventId_ = value;
    }

    [DebuggerNonUserCode]
    public int Int1
    {
      get => this.int1_;
      set => this.int1_ = value;
    }

    [DebuggerNonUserCode]
    public int Int2
    {
      get => this.int2_;
      set => this.int2_ = value;
    }

    [DebuggerNonUserCode]
    public int Int3
    {
      get => this.int3_;
      set => this.int3_ = value;
    }

    [DebuggerNonUserCode]
    public int Int4
    {
      get => this.int4_;
      set => this.int4_ = value;
    }

    [DebuggerNonUserCode]
    public int Int5
    {
      get => this.int5_;
      set => this.int5_ = value;
    }

    [DebuggerNonUserCode]
    public int Int6
    {
      get => this.int6_;
      set => this.int6_ = value;
    }

    [DebuggerNonUserCode]
    public int Int7
    {
      get => this.int7_;
      set => this.int7_ = value;
    }

    [DebuggerNonUserCode]
    public string String1
    {
      get => this.string1_;
      set => this.string1_ = ProtoPreconditions.CheckNotNull<string>(value, nameof (value));
    }

    [DebuggerNonUserCode]
    public bool Bool1
    {
      get => this.bool1_;
      set => this.bool1_ = value;
    }

    [DebuggerNonUserCode]
    public CellCoord CellCoord1
    {
      get => this.cellCoord1_;
      set => this.cellCoord1_ = value;
    }

    [DebuggerNonUserCode]
    public CellCoord CellCoord2
    {
      get => this.cellCoord2_;
      set => this.cellCoord2_ = value;
    }

    [DebuggerNonUserCode]
    public CompanionReserveState CompanionReserveState1
    {
      get => this.companionReserveState1_;
      set => this.companionReserveState1_ = value;
    }

    [DebuggerNonUserCode]
    public CompanionReserveState CompanionReserveState2
    {
      get => this.companionReserveState2_;
      set => this.companionReserveState2_ = value;
    }

    [DebuggerNonUserCode]
    public DamageReductionType DamageReductionType1
    {
      get => this.damageReductionType1_;
      set => this.damageReductionType1_ = value;
    }

    [DebuggerNonUserCode]
    public FightResult FightResult1
    {
      get => this.fightResult1_;
      set => this.fightResult1_ = value;
    }

    [DebuggerNonUserCode]
    public GameStatistics GameStatistics1
    {
      get => this.gameStatistics1_;
      set => this.gameStatistics1_ = value;
    }

    [DebuggerNonUserCode]
    public TeamsScoreModificationReason TeamsScoreModificationReason1
    {
      get => this.teamsScoreModificationReason1_;
      set => this.teamsScoreModificationReason1_ = value;
    }

    [DebuggerNonUserCode]
    public int? OptInt1
    {
      get => this.optInt1_;
      set => this.optInt1_ = value;
    }

    [DebuggerNonUserCode]
    public int? OptInt2
    {
      get => this.optInt2_;
      set => this.optInt2_ = value;
    }

    [DebuggerNonUserCode]
    public int? OptInt3
    {
      get => this.optInt3_;
      set => this.optInt3_ = value;
    }

    [DebuggerNonUserCode]
    public int? OptInt4
    {
      get => this.optInt4_;
      set => this.optInt4_ = value;
    }

    [DebuggerNonUserCode]
    public RepeatedField<CellCoord> CellCoordList1 => this.cellCoordList1_;

    [DebuggerNonUserCode]
    public RepeatedField<SpellMovement> SpellMovementList1 => this.spellMovementList1_;

    [DebuggerNonUserCode]
    public RepeatedField<CastTarget> CastTargetList1 => this.castTargetList1_;

    [DebuggerNonUserCode]
    public RepeatedField<int> IntList1 => this.intList1_;

    [DebuggerNonUserCode]
    public RepeatedField<int> IntList2 => this.intList2_;

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as FightEventData);

    [DebuggerNonUserCode]
    public bool Equals(FightEventData other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      if (this.EventType != other.EventType || this.EventId != other.EventId)
        return false;
      int? nullable1 = this.ParentEventId;
      int? nullable2 = other.ParentEventId;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || this.Int1 != other.Int1 || this.Int2 != other.Int2 || this.Int3 != other.Int3 || this.Int4 != other.Int4 || this.Int5 != other.Int5 || this.Int6 != other.Int6 || this.Int7 != other.Int7 || this.String1 != other.String1 || this.Bool1 != other.Bool1 || !object.Equals((object) this.CellCoord1, (object) other.CellCoord1) || !object.Equals((object) this.CellCoord2, (object) other.CellCoord2) || this.CompanionReserveState1 != other.CompanionReserveState1 || this.CompanionReserveState2 != other.CompanionReserveState2 || this.DamageReductionType1 != other.DamageReductionType1 || this.FightResult1 != other.FightResult1 || !object.Equals((object) this.GameStatistics1, (object) other.GameStatistics1) || this.TeamsScoreModificationReason1 != other.TeamsScoreModificationReason1)
        return false;
      nullable2 = this.OptInt1;
      nullable1 = other.OptInt1;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        return false;
      nullable1 = this.OptInt2;
      nullable2 = other.OptInt2;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return false;
      nullable2 = this.OptInt3;
      nullable1 = other.OptInt3;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        return false;
      nullable1 = this.OptInt4;
      nullable2 = other.OptInt4;
      return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && this.cellCoordList1_.Equals(other.cellCoordList1_) && this.spellMovementList1_.Equals(other.spellMovementList1_) && this.castTargetList1_.Equals(other.castTargetList1_) && this.intList1_.Equals(other.intList1_) && this.intList2_.Equals(other.intList2_) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num1 = 1;
      if (this.EventType != FightEventData.Types.EventType.EffectStopped)
        num1 ^= this.EventType.GetHashCode();
      int num2;
      if (this.EventId != 0)
      {
        int num3 = num1;
        num2 = this.EventId;
        int hashCode = num2.GetHashCode();
        num1 = num3 ^ hashCode;
      }
      int? nullable;
      if (this.parentEventId_.HasValue)
      {
        int num4 = num1;
        nullable = this.ParentEventId;
        int hashCode = nullable.GetHashCode();
        num1 = num4 ^ hashCode;
      }
      if (this.Int1 != 0)
      {
        int num5 = num1;
        num2 = this.Int1;
        int hashCode = num2.GetHashCode();
        num1 = num5 ^ hashCode;
      }
      if (this.Int2 != 0)
      {
        int num6 = num1;
        num2 = this.Int2;
        int hashCode = num2.GetHashCode();
        num1 = num6 ^ hashCode;
      }
      if (this.Int3 != 0)
      {
        int num7 = num1;
        num2 = this.Int3;
        int hashCode = num2.GetHashCode();
        num1 = num7 ^ hashCode;
      }
      if (this.Int4 != 0)
      {
        int num8 = num1;
        num2 = this.Int4;
        int hashCode = num2.GetHashCode();
        num1 = num8 ^ hashCode;
      }
      if (this.Int5 != 0)
      {
        int num9 = num1;
        num2 = this.Int5;
        int hashCode = num2.GetHashCode();
        num1 = num9 ^ hashCode;
      }
      if (this.Int6 != 0)
      {
        int num10 = num1;
        num2 = this.Int6;
        int hashCode = num2.GetHashCode();
        num1 = num10 ^ hashCode;
      }
      if (this.Int7 != 0)
      {
        int num11 = num1;
        num2 = this.Int7;
        int hashCode = num2.GetHashCode();
        num1 = num11 ^ hashCode;
      }
      if (this.String1.Length != 0)
        num1 ^= this.String1.GetHashCode();
      if (this.Bool1)
        num1 ^= this.Bool1.GetHashCode();
      if (this.cellCoord1_ != null)
        num1 ^= this.CellCoord1.GetHashCode();
      if (this.cellCoord2_ != null)
        num1 ^= this.CellCoord2.GetHashCode();
      CompanionReserveState companionReserveState;
      if (this.CompanionReserveState1 != CompanionReserveState.Idle)
      {
        int num12 = num1;
        companionReserveState = this.CompanionReserveState1;
        int hashCode = companionReserveState.GetHashCode();
        num1 = num12 ^ hashCode;
      }
      if (this.CompanionReserveState2 != CompanionReserveState.Idle)
      {
        int num13 = num1;
        companionReserveState = this.CompanionReserveState2;
        int hashCode = companionReserveState.GetHashCode();
        num1 = num13 ^ hashCode;
      }
      if (this.DamageReductionType1 != DamageReductionType.Unknown)
        num1 ^= this.DamageReductionType1.GetHashCode();
      if (this.FightResult1 != FightResult.Draw)
        num1 ^= this.FightResult1.GetHashCode();
      if (this.gameStatistics1_ != null)
        num1 ^= this.GameStatistics1.GetHashCode();
      if (this.TeamsScoreModificationReason1 != TeamsScoreModificationReason.FirstVictory)
        num1 ^= this.TeamsScoreModificationReason1.GetHashCode();
      if (this.optInt1_.HasValue)
      {
        int num14 = num1;
        nullable = this.OptInt1;
        int hashCode = nullable.GetHashCode();
        num1 = num14 ^ hashCode;
      }
      if (this.optInt2_.HasValue)
      {
        int num15 = num1;
        nullable = this.OptInt2;
        int hashCode = nullable.GetHashCode();
        num1 = num15 ^ hashCode;
      }
      if (this.optInt3_.HasValue)
      {
        int num16 = num1;
        nullable = this.OptInt3;
        int hashCode = nullable.GetHashCode();
        num1 = num16 ^ hashCode;
      }
      if (this.optInt4_.HasValue)
      {
        int num17 = num1;
        nullable = this.OptInt4;
        int hashCode = nullable.GetHashCode();
        num1 = num17 ^ hashCode;
      }
      int hashCode1 = num1 ^ this.cellCoordList1_.GetHashCode() ^ this.spellMovementList1_.GetHashCode() ^ this.castTargetList1_.GetHashCode() ^ this.intList1_.GetHashCode() ^ this.intList2_.GetHashCode();
      if (this._unknownFields != null)
        hashCode1 ^= this._unknownFields.GetHashCode();
      return hashCode1;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.EventType != FightEventData.Types.EventType.EffectStopped)
      {
        output.WriteRawTag((byte) 8);
        output.WriteEnum((int) this.EventType);
      }
      if (this.EventId != 0)
      {
        output.WriteRawTag((byte) 16);
        output.WriteSInt32(this.EventId);
      }
      if (this.parentEventId_.HasValue)
        FightEventData._single_parentEventId_codec.WriteTagAndValue(output, this.ParentEventId);
      if (this.Int1 != 0)
      {
        output.WriteRawTag((byte) 32);
        output.WriteInt32(this.Int1);
      }
      if (this.Int2 != 0)
      {
        output.WriteRawTag((byte) 40);
        output.WriteInt32(this.Int2);
      }
      if (this.Int3 != 0)
      {
        output.WriteRawTag((byte) 48);
        output.WriteInt32(this.Int3);
      }
      if (this.Int4 != 0)
      {
        output.WriteRawTag((byte) 56);
        output.WriteInt32(this.Int4);
      }
      if (this.Int5 != 0)
      {
        output.WriteRawTag((byte) 64);
        output.WriteInt32(this.Int5);
      }
      if (this.Int6 != 0)
      {
        output.WriteRawTag((byte) 72);
        output.WriteInt32(this.Int6);
      }
      if (this.Int7 != 0)
      {
        output.WriteRawTag((byte) 80);
        output.WriteInt32(this.Int7);
      }
      if (this.String1.Length != 0)
      {
        output.WriteRawTag((byte) 90);
        output.WriteString(this.String1);
      }
      if (this.Bool1)
      {
        output.WriteRawTag((byte) 96);
        output.WriteBool(this.Bool1);
      }
      if (this.cellCoord1_ != null)
      {
        output.WriteRawTag((byte) 106);
        output.WriteMessage((IMessage) this.CellCoord1);
      }
      if (this.cellCoord2_ != null)
      {
        output.WriteRawTag((byte) 114);
        output.WriteMessage((IMessage) this.CellCoord2);
      }
      if (this.CompanionReserveState1 != CompanionReserveState.Idle)
      {
        output.WriteRawTag((byte) 120);
        output.WriteEnum((int) this.CompanionReserveState1);
      }
      if (this.CompanionReserveState2 != CompanionReserveState.Idle)
      {
        output.WriteRawTag((byte) 128, (byte) 1);
        output.WriteEnum((int) this.CompanionReserveState2);
      }
      if (this.DamageReductionType1 != DamageReductionType.Unknown)
      {
        output.WriteRawTag((byte) 136, (byte) 1);
        output.WriteEnum((int) this.DamageReductionType1);
      }
      if (this.FightResult1 != FightResult.Draw)
      {
        output.WriteRawTag((byte) 144, (byte) 1);
        output.WriteEnum((int) this.FightResult1);
      }
      if (this.gameStatistics1_ != null)
      {
        output.WriteRawTag((byte) 154, (byte) 1);
        output.WriteMessage((IMessage) this.GameStatistics1);
      }
      if (this.TeamsScoreModificationReason1 != TeamsScoreModificationReason.FirstVictory)
      {
        output.WriteRawTag((byte) 160, (byte) 1);
        output.WriteEnum((int) this.TeamsScoreModificationReason1);
      }
      if (this.optInt1_.HasValue)
        FightEventData._single_optInt1_codec.WriteTagAndValue(output, this.OptInt1);
      if (this.optInt2_.HasValue)
        FightEventData._single_optInt2_codec.WriteTagAndValue(output, this.OptInt2);
      if (this.optInt3_.HasValue)
        FightEventData._single_optInt3_codec.WriteTagAndValue(output, this.OptInt3);
      if (this.optInt4_.HasValue)
        FightEventData._single_optInt4_codec.WriteTagAndValue(output, this.OptInt4);
      this.cellCoordList1_.WriteTo(output, FightEventData._repeated_cellCoordList1_codec);
      this.spellMovementList1_.WriteTo(output, FightEventData._repeated_spellMovementList1_codec);
      this.castTargetList1_.WriteTo(output, FightEventData._repeated_castTargetList1_codec);
      this.intList1_.WriteTo(output, FightEventData._repeated_intList1_codec);
      this.intList2_.WriteTo(output, FightEventData._repeated_intList2_codec);
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int num = 0;
      if (this.EventType != FightEventData.Types.EventType.EffectStopped)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.EventType);
      if (this.EventId != 0)
        num += 1 + CodedOutputStream.ComputeSInt32Size(this.EventId);
      if (this.parentEventId_.HasValue)
        num += FightEventData._single_parentEventId_codec.CalculateSizeWithTag(this.ParentEventId);
      if (this.Int1 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int1);
      if (this.Int2 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int2);
      if (this.Int3 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int3);
      if (this.Int4 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int4);
      if (this.Int5 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int5);
      if (this.Int6 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int6);
      if (this.Int7 != 0)
        num += 1 + CodedOutputStream.ComputeInt32Size(this.Int7);
      if (this.String1.Length != 0)
        num += 1 + CodedOutputStream.ComputeStringSize(this.String1);
      if (this.Bool1)
        num += 2;
      if (this.cellCoord1_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.CellCoord1);
      if (this.cellCoord2_ != null)
        num += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.CellCoord2);
      if (this.CompanionReserveState1 != CompanionReserveState.Idle)
        num += 1 + CodedOutputStream.ComputeEnumSize((int) this.CompanionReserveState1);
      if (this.CompanionReserveState2 != CompanionReserveState.Idle)
        num += 2 + CodedOutputStream.ComputeEnumSize((int) this.CompanionReserveState2);
      if (this.DamageReductionType1 != DamageReductionType.Unknown)
        num += 2 + CodedOutputStream.ComputeEnumSize((int) this.DamageReductionType1);
      if (this.FightResult1 != FightResult.Draw)
        num += 2 + CodedOutputStream.ComputeEnumSize((int) this.FightResult1);
      if (this.gameStatistics1_ != null)
        num += 2 + CodedOutputStream.ComputeMessageSize((IMessage) this.GameStatistics1);
      if (this.TeamsScoreModificationReason1 != TeamsScoreModificationReason.FirstVictory)
        num += 2 + CodedOutputStream.ComputeEnumSize((int) this.TeamsScoreModificationReason1);
      if (this.optInt1_.HasValue)
        num += FightEventData._single_optInt1_codec.CalculateSizeWithTag(this.OptInt1);
      if (this.optInt2_.HasValue)
        num += FightEventData._single_optInt2_codec.CalculateSizeWithTag(this.OptInt2);
      if (this.optInt3_.HasValue)
        num += FightEventData._single_optInt3_codec.CalculateSizeWithTag(this.OptInt3);
      if (this.optInt4_.HasValue)
        num += FightEventData._single_optInt4_codec.CalculateSizeWithTag(this.OptInt4);
      int size = num + this.cellCoordList1_.CalculateSize(FightEventData._repeated_cellCoordList1_codec) + this.spellMovementList1_.CalculateSize(FightEventData._repeated_spellMovementList1_codec) + this.castTargetList1_.CalculateSize(FightEventData._repeated_castTargetList1_codec) + this.intList1_.CalculateSize(FightEventData._repeated_intList1_codec) + this.intList2_.CalculateSize(FightEventData._repeated_intList2_codec);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(FightEventData other)
    {
      if (other == null)
        return;
      if (other.EventType != FightEventData.Types.EventType.EffectStopped)
        this.EventType = other.EventType;
      if (other.EventId != 0)
        this.EventId = other.EventId;
      int? nullable;
      if (other.parentEventId_.HasValue)
      {
        if (this.parentEventId_.HasValue)
        {
          nullable = other.ParentEventId;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_9;
        }
        this.ParentEventId = other.ParentEventId;
      }
label_9:
      if (other.Int1 != 0)
        this.Int1 = other.Int1;
      if (other.Int2 != 0)
        this.Int2 = other.Int2;
      if (other.Int3 != 0)
        this.Int3 = other.Int3;
      if (other.Int4 != 0)
        this.Int4 = other.Int4;
      if (other.Int5 != 0)
        this.Int5 = other.Int5;
      if (other.Int6 != 0)
        this.Int6 = other.Int6;
      if (other.Int7 != 0)
        this.Int7 = other.Int7;
      if (other.String1.Length != 0)
        this.String1 = other.String1;
      if (other.Bool1)
        this.Bool1 = other.Bool1;
      if (other.cellCoord1_ != null)
      {
        if (this.cellCoord1_ == null)
          this.cellCoord1_ = new CellCoord();
        this.CellCoord1.MergeFrom(other.CellCoord1);
      }
      if (other.cellCoord2_ != null)
      {
        if (this.cellCoord2_ == null)
          this.cellCoord2_ = new CellCoord();
        this.CellCoord2.MergeFrom(other.CellCoord2);
      }
      if (other.CompanionReserveState1 != CompanionReserveState.Idle)
        this.CompanionReserveState1 = other.CompanionReserveState1;
      if (other.CompanionReserveState2 != CompanionReserveState.Idle)
        this.CompanionReserveState2 = other.CompanionReserveState2;
      if (other.DamageReductionType1 != DamageReductionType.Unknown)
        this.DamageReductionType1 = other.DamageReductionType1;
      if (other.FightResult1 != FightResult.Draw)
        this.FightResult1 = other.FightResult1;
      if (other.gameStatistics1_ != null)
      {
        if (this.gameStatistics1_ == null)
          this.gameStatistics1_ = new GameStatistics();
        this.GameStatistics1.MergeFrom(other.GameStatistics1);
      }
      if (other.TeamsScoreModificationReason1 != TeamsScoreModificationReason.FirstVictory)
        this.TeamsScoreModificationReason1 = other.TeamsScoreModificationReason1;
      if (other.optInt1_.HasValue)
      {
        if (this.optInt1_.HasValue)
        {
          nullable = other.OptInt1;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_53;
        }
        this.OptInt1 = other.OptInt1;
      }
label_53:
      if (other.optInt2_.HasValue)
      {
        if (this.optInt2_.HasValue)
        {
          nullable = other.OptInt2;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_57;
        }
        this.OptInt2 = other.OptInt2;
      }
label_57:
      if (other.optInt3_.HasValue)
      {
        if (this.optInt3_.HasValue)
        {
          nullable = other.OptInt3;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_61;
        }
        this.OptInt3 = other.OptInt3;
      }
label_61:
      if (other.optInt4_.HasValue)
      {
        if (this.optInt4_.HasValue)
        {
          nullable = other.OptInt4;
          int num = 0;
          if (nullable.GetValueOrDefault() == num & nullable.HasValue)
            goto label_65;
        }
        this.OptInt4 = other.OptInt4;
      }
label_65:
      this.cellCoordList1_.Add((IEnumerable<CellCoord>) other.cellCoordList1_);
      this.spellMovementList1_.Add((IEnumerable<SpellMovement>) other.spellMovementList1_);
      this.castTargetList1_.Add((IEnumerable<CastTarget>) other.castTargetList1_);
      this.intList1_.Add((IEnumerable<int>) other.intList1_);
      this.intList2_.Add((IEnumerable<int>) other.intList2_);
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num1;
      while ((num1 = input.ReadTag()) != 0U)
      {
        switch (num1)
        {
          case 8:
            this.eventType_ = (FightEventData.Types.EventType) input.ReadEnum();
            continue;
          case 16:
            this.EventId = input.ReadSInt32();
            continue;
          case 26:
            int? nullable1 = FightEventData._single_parentEventId_codec.Read(input);
            if (this.parentEventId_.HasValue)
            {
              int? nullable2 = nullable1;
              int num2 = 0;
              if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                continue;
            }
            this.ParentEventId = nullable1;
            continue;
          case 32:
            this.Int1 = input.ReadInt32();
            continue;
          case 40:
            this.Int2 = input.ReadInt32();
            continue;
          case 48:
            this.Int3 = input.ReadInt32();
            continue;
          case 56:
            this.Int4 = input.ReadInt32();
            continue;
          case 64:
            this.Int5 = input.ReadInt32();
            continue;
          case 72:
            this.Int6 = input.ReadInt32();
            continue;
          case 80:
            this.Int7 = input.ReadInt32();
            continue;
          case 90:
            this.String1 = input.ReadString();
            continue;
          case 96:
            this.Bool1 = input.ReadBool();
            continue;
          case 106:
            if (this.cellCoord1_ == null)
              this.cellCoord1_ = new CellCoord();
            input.ReadMessage((IMessage) this.cellCoord1_);
            continue;
          case 114:
            if (this.cellCoord2_ == null)
              this.cellCoord2_ = new CellCoord();
            input.ReadMessage((IMessage) this.cellCoord2_);
            continue;
          case 120:
            this.companionReserveState1_ = (CompanionReserveState) input.ReadEnum();
            continue;
          case 128:
            this.companionReserveState2_ = (CompanionReserveState) input.ReadEnum();
            continue;
          case 136:
            this.damageReductionType1_ = (DamageReductionType) input.ReadEnum();
            continue;
          case 144:
            this.fightResult1_ = (FightResult) input.ReadEnum();
            continue;
          case 154:
            if (this.gameStatistics1_ == null)
              this.gameStatistics1_ = new GameStatistics();
            input.ReadMessage((IMessage) this.gameStatistics1_);
            continue;
          case 160:
            this.teamsScoreModificationReason1_ = (TeamsScoreModificationReason) input.ReadEnum();
            continue;
          case 170:
            int? nullable3 = FightEventData._single_optInt1_codec.Read(input);
            if (this.optInt1_.HasValue)
            {
              int? nullable4 = nullable3;
              int num3 = 0;
              if (nullable4.GetValueOrDefault() == num3 & nullable4.HasValue)
                continue;
            }
            this.OptInt1 = nullable3;
            continue;
          case 178:
            int? nullable5 = FightEventData._single_optInt2_codec.Read(input);
            if (this.optInt2_.HasValue)
            {
              int? nullable6 = nullable5;
              int num4 = 0;
              if (nullable6.GetValueOrDefault() == num4 & nullable6.HasValue)
                continue;
            }
            this.OptInt2 = nullable5;
            continue;
          case 186:
            int? nullable7 = FightEventData._single_optInt3_codec.Read(input);
            if (this.optInt3_.HasValue)
            {
              int? nullable8 = nullable7;
              int num5 = 0;
              if (nullable8.GetValueOrDefault() == num5 & nullable8.HasValue)
                continue;
            }
            this.OptInt3 = nullable7;
            continue;
          case 194:
            int? nullable9 = FightEventData._single_optInt4_codec.Read(input);
            if (this.optInt4_.HasValue)
            {
              int? nullable10 = nullable9;
              int num6 = 0;
              if (nullable10.GetValueOrDefault() == num6 & nullable10.HasValue)
                continue;
            }
            this.OptInt4 = nullable9;
            continue;
          case 202:
            this.cellCoordList1_.AddEntriesFrom(input, FightEventData._repeated_cellCoordList1_codec);
            continue;
          case 210:
            this.spellMovementList1_.AddEntriesFrom(input, FightEventData._repeated_spellMovementList1_codec);
            continue;
          case 218:
            this.castTargetList1_.AddEntriesFrom(input, FightEventData._repeated_castTargetList1_codec);
            continue;
          case 224:
          case 226:
            this.intList1_.AddEntriesFrom(input, FightEventData._repeated_intList1_codec);
            continue;
          case 232:
          case 234:
            this.intList2_.AddEntriesFrom(input, FightEventData._repeated_intList2_codec);
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (FightEventData);

    [DebuggerNonUserCode]
    public static class Types
    {
      public enum EventType
      {
        [OriginalName("EFFECT_STOPPED")] EffectStopped = 0,
        [OriginalName("TURN_STARTED")] TurnStarted = 2,
        [OriginalName("ENTITY_AREA_MOVED")] EntityAreaMoved = 3,
        [OriginalName("PLAYER_ADDED")] PlayerAdded = 4,
        [OriginalName("HERO_ADDED")] HeroAdded = 5,
        [OriginalName("COMPANION_ADDED")] CompanionAdded = 6,
        [OriginalName("ENTITY_ACTIONED")] EntityActioned = 7,
        [OriginalName("SPELLS_MOVED")] SpellsMoved = 8,
        [OriginalName("TEAM_TURN_ENDED")] TeamTurnEnded = 9,
        [OriginalName("PLAY_SPELL")] PlaySpell = 10, // 0x0000000A
        [OriginalName("TURN_ENDED")] TurnEnded = 11, // 0x0000000B
        [OriginalName("ARMORED_LIFE_CHANGED")] ArmoredLifeChanged = 12, // 0x0000000C
        [OriginalName("ENTITY_REMOVED")] EntityRemoved = 13, // 0x0000000D
        [OriginalName("ELEMENT_POINTS_CHANGED")] ElementPointsChanged = 14, // 0x0000000E
        [OriginalName("COMPANION_ADDED_IN_RESERVE")] CompanionAddedInReserve = 15, // 0x0000000F
        [OriginalName("ACTION_POINTS_CHANGED")] ActionPointsChanged = 16, // 0x00000010
        [OriginalName("COMPANION_RESERVE_STATE_CHANGED")] CompanionReserveStateChanged = 17, // 0x00000011
        [OriginalName("ENTITY_PROTECTION_ADDED")] EntityProtectionAdded = 18, // 0x00000012
        [OriginalName("ENTITY_PROTECTION_REMOVED")] EntityProtectionRemoved = 19, // 0x00000013
        [OriginalName("MAGICAL_DAMAGE_MODIFIER_CHANGED")] MagicalDamageModifierChanged = 20, // 0x00000014
        [OriginalName("MAGICAL_HEAL_MODIFIER_CHANGED")] MagicalHealModifierChanged = 21, // 0x00000015
        [OriginalName("MOVEMENT_POINTS_CHANGED")] MovementPointsChanged = 22, // 0x00000016
        [OriginalName("DICE_THROWN")] DiceThrown = 24, // 0x00000018
        [OriginalName("SUMMONING_ADDED")] SummoningAdded = 25, // 0x00000019
        [OriginalName("ENTITY_ACTION_RESET")] EntityActionReset = 26, // 0x0000001A
        [OriginalName("RESERVE_POINTS_CHANGED")] ReservePointsChanged = 27, // 0x0000001B
        [OriginalName("RESERVE_USED")] ReserveUsed = 28, // 0x0000001C
        [OriginalName("PROPERTY_CHANGED")] PropertyChanged = 29, // 0x0000001D
        [OriginalName("FLOOR_MECHANISM_ADDED")] FloorMechanismAdded = 30, // 0x0000001E
        [OriginalName("FLOOR_MECHANISM_ACTIVATION")] FloorMechanismActivation = 31, // 0x0000001F
        [OriginalName("OBJECT_MECHANISM_ADDED")] ObjectMechanismAdded = 32, // 0x00000020
        [OriginalName("SPELL_COST_MODIFIER_ADDED")] SpellCostModifierAdded = 33, // 0x00000021
        [OriginalName("SPELL_COST_MODIFIER_REMOVED")] SpellCostModifierRemoved = 34, // 0x00000022
        [OriginalName("TEAM_ADDED")] TeamAdded = 35, // 0x00000023
        [OriginalName("FIGHT_ENDED")] FightEnded = 36, // 0x00000024
        [OriginalName("TRANSFORMATION")] Transformation = 37, // 0x00000025
        [OriginalName("ELEMENTARY_CHANGED")] ElementaryChanged = 38, // 0x00000026
        [OriginalName("DAMAGE_REDUCED")] DamageReduced = 39, // 0x00000027
        [OriginalName("ATTACK")] Attack = 40, // 0x00000028
        [OriginalName("EXPLOSION")] Explosion = 41, // 0x00000029
        [OriginalName("ENTITY_ANIMATION")] EntityAnimation = 42, // 0x0000002A
        [OriginalName("ENTITY_SKIN_CHANGED")] EntitySkinChanged = 43, // 0x0000002B
        [OriginalName("FLOATING_COUNTER_VALUE_CHANGED")] FloatingCounterValueChanged = 44, // 0x0000002C
        [OriginalName("ASSEMBLAGE_CHANGED")] AssemblageChanged = 45, // 0x0000002D
        [OriginalName("PHYSICAL_DAMAGE_MODIFIER_CHANGED")] PhysicalDamageModifierChanged = 46, // 0x0000002E
        [OriginalName("PHYSICAL_HEAL_MODIFIER_CHANGED")] PhysicalHealModifierChanged = 47, // 0x0000002F
        [OriginalName("BOSS_SUMMONINGS_WARNING")] BossSummoningsWarning = 48, // 0x00000030
        [OriginalName("BOSS_RESERVE_MODIFICATION")] BossReserveModification = 49, // 0x00000031
        [OriginalName("BOSS_EVOLUTION_STEP_MODIFICATION")] BossEvolutionStepModification = 50, // 0x00000032
        [OriginalName("BOSS_TURN_START")] BossTurnStart = 51, // 0x00000033
        [OriginalName("BOSS_LIFE_MODIFICATION")] BossLifeModification = 52, // 0x00000034
        [OriginalName("BOSS_CAST_SPELL")] BossCastSpell = 53, // 0x00000035
        [OriginalName("GAME_ENDED")] GameEnded = 54, // 0x00000036
        [OriginalName("COMPANION_GIVEN")] CompanionGiven = 55, // 0x00000037
        [OriginalName("COMPANION_RECEIVED")] CompanionReceived = 56, // 0x00000038
        [OriginalName("TEAM_TURN_STARTED")] TeamTurnStarted = 57, // 0x00000039
        [OriginalName("BOSS_TURN_END")] BossTurnEnd = 58, // 0x0000003A
        [OriginalName("TURN_SYNCHRONIZATION")] TurnSynchronization = 59, // 0x0000003B
        [OriginalName("EVENT_FOR_PARENTING")] EventForParenting = 60, // 0x0000003C
        [OriginalName("TEAMS_SCORE_MODIFICATION")] TeamsScoreModification = 61, // 0x0000003D
        [OriginalName("MAX_LIFE_CHANGED")] MaxLifeChanged = 62, // 0x0000003E
        [OriginalName("FIGHT_INITIALIZED")] FightInitialized = 63, // 0x0000003F
        [OriginalName("ENTITY_PASSIVE_FX")] EntityPassiveFx = 64, // 0x00000040
      }
    }
  }
}
