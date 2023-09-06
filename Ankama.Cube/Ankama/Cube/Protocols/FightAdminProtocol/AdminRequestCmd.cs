// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.FightAdminProtocol.AdminRequestCmd
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Diagnostics;

namespace Ankama.Cube.Protocols.FightAdminProtocol
{
  public sealed class AdminRequestCmd : 
    IMessage<AdminRequestCmd>,
    IMessage,
    IEquatable<AdminRequestCmd>,
    IDeepCloneable<AdminRequestCmd>,
    ICustomDiagnosticMessage
  {
    private static readonly MessageParser<AdminRequestCmd> _parser = new MessageParser<AdminRequestCmd>((Func<AdminRequestCmd>) (() => new AdminRequestCmd()));
    private UnknownFieldSet _unknownFields;
    public const int DealDamageFieldNumber = 1;
    public const int KillFieldNumber = 2;
    public const int TeleportFieldNumber = 3;
    public const int DrawSpellsFieldNumber = 4;
    public const int DiscardSpellsFieldNumber = 5;
    public const int GainElementPointsFieldNumber = 6;
    public const int GainActionPointsFieldNumber = 7;
    public const int GainReservePointsFieldNumber = 8;
    public const int PickSpellFieldNumber = 9;
    public const int SetPropertyFieldNumber = 10;
    public const int HealFieldNumber = 11;
    public const int InvokeSummoningFieldNumber = 12;
    public const int InvokeCompanionFieldNumber = 13;
    public const int SetElementaryStateFieldNumber = 14;
    private object cmd_;
    private AdminRequestCmd.CmdOneofCase cmdCase_;

    [DebuggerNonUserCode]
    public static MessageParser<AdminRequestCmd> Parser => AdminRequestCmd._parser;

    [DebuggerNonUserCode]
    public static MessageDescriptor Descriptor => FightAdminProtocolReflection.Descriptor.MessageTypes[0];

    [DebuggerNonUserCode]
    MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Descriptor;

    [DebuggerNonUserCode]
    public AdminRequestCmd()
    {
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd(AdminRequestCmd other)
      : this()
    {
      switch (other.CmdCase)
      {
        case AdminRequestCmd.CmdOneofCase.DealDamage:
          this.DealDamage = other.DealDamage.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.Kill:
          this.Kill = other.Kill.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.Teleport:
          this.Teleport = other.Teleport.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.DrawSpells:
          this.DrawSpells = other.DrawSpells.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.DiscardSpells:
          this.DiscardSpells = other.DiscardSpells.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.GainElementPoints:
          this.GainElementPoints = other.GainElementPoints.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.GainActionPoints:
          this.GainActionPoints = other.GainActionPoints.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.GainReservePoints:
          this.GainReservePoints = other.GainReservePoints.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.PickSpell:
          this.PickSpell = other.PickSpell.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.SetProperty:
          this.SetProperty = other.SetProperty.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.Heal:
          this.Heal = other.Heal.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.InvokeSummoning:
          this.InvokeSummoning = other.InvokeSummoning.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.InvokeCompanion:
          this.InvokeCompanion = other.InvokeCompanion.Clone();
          break;
        case AdminRequestCmd.CmdOneofCase.SetElementaryState:
          this.SetElementaryState = other.SetElementaryState.Clone();
          break;
      }
      this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd Clone() => new AdminRequestCmd(this);

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.DealDamageAdminCmd DealDamage
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.DealDamage ? (AdminRequestCmd.Types.DealDamageAdminCmd) null : (AdminRequestCmd.Types.DealDamageAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.DealDamage;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.KillAdminCmd Kill
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.Kill ? (AdminRequestCmd.Types.KillAdminCmd) null : (AdminRequestCmd.Types.KillAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.Kill;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.TeleportAdminCmd Teleport
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.Teleport ? (AdminRequestCmd.Types.TeleportAdminCmd) null : (AdminRequestCmd.Types.TeleportAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.Teleport;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.DrawSpellsCmd DrawSpells
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.DrawSpells ? (AdminRequestCmd.Types.DrawSpellsCmd) null : (AdminRequestCmd.Types.DrawSpellsCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.DrawSpells;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.DiscardSpellsCmd DiscardSpells
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.DiscardSpells ? (AdminRequestCmd.Types.DiscardSpellsCmd) null : (AdminRequestCmd.Types.DiscardSpellsCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.DiscardSpells;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.GainElementPointsCmd GainElementPoints
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.GainElementPoints ? (AdminRequestCmd.Types.GainElementPointsCmd) null : (AdminRequestCmd.Types.GainElementPointsCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.GainElementPoints;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.GainActionPointsCmd GainActionPoints
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.GainActionPoints ? (AdminRequestCmd.Types.GainActionPointsCmd) null : (AdminRequestCmd.Types.GainActionPointsCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.GainActionPoints;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.GainReservePointsCmd GainReservePoints
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.GainReservePoints ? (AdminRequestCmd.Types.GainReservePointsCmd) null : (AdminRequestCmd.Types.GainReservePointsCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.GainReservePoints;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.PickSpellCmd PickSpell
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.PickSpell ? (AdminRequestCmd.Types.PickSpellCmd) null : (AdminRequestCmd.Types.PickSpellCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.PickSpell;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.SetPropertyCmd SetProperty
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.SetProperty ? (AdminRequestCmd.Types.SetPropertyCmd) null : (AdminRequestCmd.Types.SetPropertyCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.SetProperty;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.HealAdminCmd Heal
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.Heal ? (AdminRequestCmd.Types.HealAdminCmd) null : (AdminRequestCmd.Types.HealAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.Heal;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.InvokeSummoningAdminCmd InvokeSummoning
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.InvokeSummoning ? (AdminRequestCmd.Types.InvokeSummoningAdminCmd) null : (AdminRequestCmd.Types.InvokeSummoningAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.InvokeSummoning;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.InvokeCompanionAdminCmd InvokeCompanion
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.InvokeCompanion ? (AdminRequestCmd.Types.InvokeCompanionAdminCmd) null : (AdminRequestCmd.Types.InvokeCompanionAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.InvokeCompanion;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.Types.SetElementaryStateAdminCmd SetElementaryState
    {
      get => this.cmdCase_ != AdminRequestCmd.CmdOneofCase.SetElementaryState ? (AdminRequestCmd.Types.SetElementaryStateAdminCmd) null : (AdminRequestCmd.Types.SetElementaryStateAdminCmd) this.cmd_;
      set
      {
        this.cmd_ = (object) value;
        this.cmdCase_ = value == null ? AdminRequestCmd.CmdOneofCase.None : AdminRequestCmd.CmdOneofCase.SetElementaryState;
      }
    }

    [DebuggerNonUserCode]
    public AdminRequestCmd.CmdOneofCase CmdCase => this.cmdCase_;

    [DebuggerNonUserCode]
    public void ClearCmd()
    {
      this.cmdCase_ = AdminRequestCmd.CmdOneofCase.None;
      this.cmd_ = (object) null;
    }

    [DebuggerNonUserCode]
    public override bool Equals(object other) => this.Equals(other as AdminRequestCmd);

    [DebuggerNonUserCode]
    public bool Equals(AdminRequestCmd other)
    {
      if (other == null)
        return false;
      if (other == this)
        return true;
      return object.Equals((object) this.DealDamage, (object) other.DealDamage) && object.Equals((object) this.Kill, (object) other.Kill) && object.Equals((object) this.Teleport, (object) other.Teleport) && object.Equals((object) this.DrawSpells, (object) other.DrawSpells) && object.Equals((object) this.DiscardSpells, (object) other.DiscardSpells) && object.Equals((object) this.GainElementPoints, (object) other.GainElementPoints) && object.Equals((object) this.GainActionPoints, (object) other.GainActionPoints) && object.Equals((object) this.GainReservePoints, (object) other.GainReservePoints) && object.Equals((object) this.PickSpell, (object) other.PickSpell) && object.Equals((object) this.SetProperty, (object) other.SetProperty) && object.Equals((object) this.Heal, (object) other.Heal) && object.Equals((object) this.InvokeSummoning, (object) other.InvokeSummoning) && object.Equals((object) this.InvokeCompanion, (object) other.InvokeCompanion) && object.Equals((object) this.SetElementaryState, (object) other.SetElementaryState) && this.CmdCase == other.CmdCase && object.Equals((object) this._unknownFields, (object) other._unknownFields);
    }

    [DebuggerNonUserCode]
    public override int GetHashCode()
    {
      int num = 1;
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DealDamage)
        num ^= this.DealDamage.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Kill)
        num ^= this.Kill.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Teleport)
        num ^= this.Teleport.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DrawSpells)
        num ^= this.DrawSpells.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DiscardSpells)
        num ^= this.DiscardSpells.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainElementPoints)
        num ^= this.GainElementPoints.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainActionPoints)
        num ^= this.GainActionPoints.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainReservePoints)
        num ^= this.GainReservePoints.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.PickSpell)
        num ^= this.PickSpell.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetProperty)
        num ^= this.SetProperty.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Heal)
        num ^= this.Heal.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeSummoning)
        num ^= this.InvokeSummoning.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeCompanion)
        num ^= this.InvokeCompanion.GetHashCode();
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetElementaryState)
        num ^= this.SetElementaryState.GetHashCode();
      int hashCode = (int) ((AdminRequestCmd.CmdOneofCase) num ^ this.cmdCase_);
      if (this._unknownFields != null)
        hashCode ^= this._unknownFields.GetHashCode();
      return hashCode;
    }

    [DebuggerNonUserCode]
    public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

    [DebuggerNonUserCode]
    public void WriteTo(CodedOutputStream output)
    {
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DealDamage)
      {
        output.WriteRawTag((byte) 10);
        output.WriteMessage((IMessage) this.DealDamage);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Kill)
      {
        output.WriteRawTag((byte) 18);
        output.WriteMessage((IMessage) this.Kill);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Teleport)
      {
        output.WriteRawTag((byte) 26);
        output.WriteMessage((IMessage) this.Teleport);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DrawSpells)
      {
        output.WriteRawTag((byte) 34);
        output.WriteMessage((IMessage) this.DrawSpells);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DiscardSpells)
      {
        output.WriteRawTag((byte) 42);
        output.WriteMessage((IMessage) this.DiscardSpells);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainElementPoints)
      {
        output.WriteRawTag((byte) 50);
        output.WriteMessage((IMessage) this.GainElementPoints);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainActionPoints)
      {
        output.WriteRawTag((byte) 58);
        output.WriteMessage((IMessage) this.GainActionPoints);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainReservePoints)
      {
        output.WriteRawTag((byte) 66);
        output.WriteMessage((IMessage) this.GainReservePoints);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.PickSpell)
      {
        output.WriteRawTag((byte) 74);
        output.WriteMessage((IMessage) this.PickSpell);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetProperty)
      {
        output.WriteRawTag((byte) 82);
        output.WriteMessage((IMessage) this.SetProperty);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Heal)
      {
        output.WriteRawTag((byte) 90);
        output.WriteMessage((IMessage) this.Heal);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeSummoning)
      {
        output.WriteRawTag((byte) 98);
        output.WriteMessage((IMessage) this.InvokeSummoning);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeCompanion)
      {
        output.WriteRawTag((byte) 106);
        output.WriteMessage((IMessage) this.InvokeCompanion);
      }
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetElementaryState)
      {
        output.WriteRawTag((byte) 114);
        output.WriteMessage((IMessage) this.SetElementaryState);
      }
      if (this._unknownFields == null)
        return;
      this._unknownFields.WriteTo(output);
    }

    [DebuggerNonUserCode]
    public int CalculateSize()
    {
      int size = 0;
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DealDamage)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.DealDamage);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Kill)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Kill);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Teleport)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Teleport);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DrawSpells)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.DrawSpells);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DiscardSpells)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.DiscardSpells);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainElementPoints)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.GainElementPoints);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainActionPoints)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.GainActionPoints);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainReservePoints)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.GainReservePoints);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.PickSpell)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.PickSpell);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetProperty)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SetProperty);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Heal)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Heal);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeSummoning)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.InvokeSummoning);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeCompanion)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.InvokeCompanion);
      if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetElementaryState)
        size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.SetElementaryState);
      if (this._unknownFields != null)
        size += this._unknownFields.CalculateSize();
      return size;
    }

    [DebuggerNonUserCode]
    public void MergeFrom(AdminRequestCmd other)
    {
      if (other == null)
        return;
      switch (other.CmdCase)
      {
        case AdminRequestCmd.CmdOneofCase.DealDamage:
          if (this.DealDamage == null)
            this.DealDamage = new AdminRequestCmd.Types.DealDamageAdminCmd();
          this.DealDamage.MergeFrom(other.DealDamage);
          break;
        case AdminRequestCmd.CmdOneofCase.Kill:
          if (this.Kill == null)
            this.Kill = new AdminRequestCmd.Types.KillAdminCmd();
          this.Kill.MergeFrom(other.Kill);
          break;
        case AdminRequestCmd.CmdOneofCase.Teleport:
          if (this.Teleport == null)
            this.Teleport = new AdminRequestCmd.Types.TeleportAdminCmd();
          this.Teleport.MergeFrom(other.Teleport);
          break;
        case AdminRequestCmd.CmdOneofCase.DrawSpells:
          if (this.DrawSpells == null)
            this.DrawSpells = new AdminRequestCmd.Types.DrawSpellsCmd();
          this.DrawSpells.MergeFrom(other.DrawSpells);
          break;
        case AdminRequestCmd.CmdOneofCase.DiscardSpells:
          if (this.DiscardSpells == null)
            this.DiscardSpells = new AdminRequestCmd.Types.DiscardSpellsCmd();
          this.DiscardSpells.MergeFrom(other.DiscardSpells);
          break;
        case AdminRequestCmd.CmdOneofCase.GainElementPoints:
          if (this.GainElementPoints == null)
            this.GainElementPoints = new AdminRequestCmd.Types.GainElementPointsCmd();
          this.GainElementPoints.MergeFrom(other.GainElementPoints);
          break;
        case AdminRequestCmd.CmdOneofCase.GainActionPoints:
          if (this.GainActionPoints == null)
            this.GainActionPoints = new AdminRequestCmd.Types.GainActionPointsCmd();
          this.GainActionPoints.MergeFrom(other.GainActionPoints);
          break;
        case AdminRequestCmd.CmdOneofCase.GainReservePoints:
          if (this.GainReservePoints == null)
            this.GainReservePoints = new AdminRequestCmd.Types.GainReservePointsCmd();
          this.GainReservePoints.MergeFrom(other.GainReservePoints);
          break;
        case AdminRequestCmd.CmdOneofCase.PickSpell:
          if (this.PickSpell == null)
            this.PickSpell = new AdminRequestCmd.Types.PickSpellCmd();
          this.PickSpell.MergeFrom(other.PickSpell);
          break;
        case AdminRequestCmd.CmdOneofCase.SetProperty:
          if (this.SetProperty == null)
            this.SetProperty = new AdminRequestCmd.Types.SetPropertyCmd();
          this.SetProperty.MergeFrom(other.SetProperty);
          break;
        case AdminRequestCmd.CmdOneofCase.Heal:
          if (this.Heal == null)
            this.Heal = new AdminRequestCmd.Types.HealAdminCmd();
          this.Heal.MergeFrom(other.Heal);
          break;
        case AdminRequestCmd.CmdOneofCase.InvokeSummoning:
          if (this.InvokeSummoning == null)
            this.InvokeSummoning = new AdminRequestCmd.Types.InvokeSummoningAdminCmd();
          this.InvokeSummoning.MergeFrom(other.InvokeSummoning);
          break;
        case AdminRequestCmd.CmdOneofCase.InvokeCompanion:
          if (this.InvokeCompanion == null)
            this.InvokeCompanion = new AdminRequestCmd.Types.InvokeCompanionAdminCmd();
          this.InvokeCompanion.MergeFrom(other.InvokeCompanion);
          break;
        case AdminRequestCmd.CmdOneofCase.SetElementaryState:
          if (this.SetElementaryState == null)
            this.SetElementaryState = new AdminRequestCmd.Types.SetElementaryStateAdminCmd();
          this.SetElementaryState.MergeFrom(other.SetElementaryState);
          break;
      }
      this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
    }

    [DebuggerNonUserCode]
    public void MergeFrom(CodedInputStream input)
    {
      uint num;
      while ((num = input.ReadTag()) != 0U)
      {
        switch (num)
        {
          case 10:
            AdminRequestCmd.Types.DealDamageAdminCmd builder1 = new AdminRequestCmd.Types.DealDamageAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DealDamage)
              builder1.MergeFrom(this.DealDamage);
            input.ReadMessage((IMessage) builder1);
            this.DealDamage = builder1;
            continue;
          case 18:
            AdminRequestCmd.Types.KillAdminCmd builder2 = new AdminRequestCmd.Types.KillAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Kill)
              builder2.MergeFrom(this.Kill);
            input.ReadMessage((IMessage) builder2);
            this.Kill = builder2;
            continue;
          case 26:
            AdminRequestCmd.Types.TeleportAdminCmd builder3 = new AdminRequestCmd.Types.TeleportAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Teleport)
              builder3.MergeFrom(this.Teleport);
            input.ReadMessage((IMessage) builder3);
            this.Teleport = builder3;
            continue;
          case 34:
            AdminRequestCmd.Types.DrawSpellsCmd builder4 = new AdminRequestCmd.Types.DrawSpellsCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DrawSpells)
              builder4.MergeFrom(this.DrawSpells);
            input.ReadMessage((IMessage) builder4);
            this.DrawSpells = builder4;
            continue;
          case 42:
            AdminRequestCmd.Types.DiscardSpellsCmd builder5 = new AdminRequestCmd.Types.DiscardSpellsCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.DiscardSpells)
              builder5.MergeFrom(this.DiscardSpells);
            input.ReadMessage((IMessage) builder5);
            this.DiscardSpells = builder5;
            continue;
          case 50:
            AdminRequestCmd.Types.GainElementPointsCmd builder6 = new AdminRequestCmd.Types.GainElementPointsCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainElementPoints)
              builder6.MergeFrom(this.GainElementPoints);
            input.ReadMessage((IMessage) builder6);
            this.GainElementPoints = builder6;
            continue;
          case 58:
            AdminRequestCmd.Types.GainActionPointsCmd builder7 = new AdminRequestCmd.Types.GainActionPointsCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainActionPoints)
              builder7.MergeFrom(this.GainActionPoints);
            input.ReadMessage((IMessage) builder7);
            this.GainActionPoints = builder7;
            continue;
          case 66:
            AdminRequestCmd.Types.GainReservePointsCmd builder8 = new AdminRequestCmd.Types.GainReservePointsCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.GainReservePoints)
              builder8.MergeFrom(this.GainReservePoints);
            input.ReadMessage((IMessage) builder8);
            this.GainReservePoints = builder8;
            continue;
          case 74:
            AdminRequestCmd.Types.PickSpellCmd builder9 = new AdminRequestCmd.Types.PickSpellCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.PickSpell)
              builder9.MergeFrom(this.PickSpell);
            input.ReadMessage((IMessage) builder9);
            this.PickSpell = builder9;
            continue;
          case 82:
            AdminRequestCmd.Types.SetPropertyCmd builder10 = new AdminRequestCmd.Types.SetPropertyCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetProperty)
              builder10.MergeFrom(this.SetProperty);
            input.ReadMessage((IMessage) builder10);
            this.SetProperty = builder10;
            continue;
          case 90:
            AdminRequestCmd.Types.HealAdminCmd builder11 = new AdminRequestCmd.Types.HealAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.Heal)
              builder11.MergeFrom(this.Heal);
            input.ReadMessage((IMessage) builder11);
            this.Heal = builder11;
            continue;
          case 98:
            AdminRequestCmd.Types.InvokeSummoningAdminCmd builder12 = new AdminRequestCmd.Types.InvokeSummoningAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeSummoning)
              builder12.MergeFrom(this.InvokeSummoning);
            input.ReadMessage((IMessage) builder12);
            this.InvokeSummoning = builder12;
            continue;
          case 106:
            AdminRequestCmd.Types.InvokeCompanionAdminCmd builder13 = new AdminRequestCmd.Types.InvokeCompanionAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.InvokeCompanion)
              builder13.MergeFrom(this.InvokeCompanion);
            input.ReadMessage((IMessage) builder13);
            this.InvokeCompanion = builder13;
            continue;
          case 114:
            AdminRequestCmd.Types.SetElementaryStateAdminCmd builder14 = new AdminRequestCmd.Types.SetElementaryStateAdminCmd();
            if (this.cmdCase_ == AdminRequestCmd.CmdOneofCase.SetElementaryState)
              builder14.MergeFrom(this.SetElementaryState);
            input.ReadMessage((IMessage) builder14);
            this.SetElementaryState = builder14;
            continue;
          default:
            this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            continue;
        }
      }
    }

    public string ToDiagnosticString() => nameof (AdminRequestCmd);

    public enum CmdOneofCase
    {
      None,
      DealDamage,
      Kill,
      Teleport,
      DrawSpells,
      DiscardSpells,
      GainElementPoints,
      GainActionPoints,
      GainReservePoints,
      PickSpell,
      SetProperty,
      Heal,
      InvokeSummoning,
      InvokeCompanion,
      SetElementaryState,
    }

    [DebuggerNonUserCode]
    public static class Types
    {
      public sealed class SetPropertyCmd : 
        IMessage<AdminRequestCmd.Types.SetPropertyCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.SetPropertyCmd>,
        IDeepCloneable<AdminRequestCmd.Types.SetPropertyCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.SetPropertyCmd> _parser = new MessageParser<AdminRequestCmd.Types.SetPropertyCmd>((Func<AdminRequestCmd.Types.SetPropertyCmd>) (() => new AdminRequestCmd.Types.SetPropertyCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;
        public const int PropertyIdFieldNumber = 2;
        private int propertyId_;
        public const int ActiveFieldNumber = 3;
        private bool active_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.SetPropertyCmd> Parser => AdminRequestCmd.Types.SetPropertyCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[0];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.SetPropertyCmd.Descriptor;

        [DebuggerNonUserCode]
        public SetPropertyCmd()
        {
        }

        [DebuggerNonUserCode]
        public SetPropertyCmd(AdminRequestCmd.Types.SetPropertyCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this.propertyId_ = other.propertyId_;
          this.active_ = other.active_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.SetPropertyCmd Clone() => new AdminRequestCmd.Types.SetPropertyCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int PropertyId
        {
          get => this.propertyId_;
          set => this.propertyId_ = value;
        }

        [DebuggerNonUserCode]
        public bool Active
        {
          get => this.active_;
          set => this.active_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.SetPropertyCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.SetPropertyCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && this.PropertyId == other.PropertyId && this.Active == other.Active && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.TargetEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.TargetEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.PropertyId != 0)
          {
            int num3 = hashCode1;
            num1 = this.PropertyId;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.Active)
            hashCode1 ^= this.Active.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this.PropertyId != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.PropertyId);
          }
          if (this.Active)
          {
            output.WriteRawTag((byte) 24);
            output.WriteBool(this.Active);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this.PropertyId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PropertyId);
          if (this.Active)
            size += 2;
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.SetPropertyCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          if (other.PropertyId != 0)
            this.PropertyId = other.PropertyId;
          if (other.Active)
            this.Active = other.Active;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.TargetEntityId = input.ReadInt32();
                continue;
              case 16:
                this.PropertyId = input.ReadInt32();
                continue;
              case 24:
                this.Active = input.ReadBool();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (SetPropertyCmd);
      }

      public sealed class SetElementaryStateAdminCmd : 
        IMessage<AdminRequestCmd.Types.SetElementaryStateAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.SetElementaryStateAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.SetElementaryStateAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.SetElementaryStateAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.SetElementaryStateAdminCmd>((Func<AdminRequestCmd.Types.SetElementaryStateAdminCmd>) (() => new AdminRequestCmd.Types.SetElementaryStateAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;
        public const int ElementaryStateIdFieldNumber = 2;
        private int elementaryStateId_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.SetElementaryStateAdminCmd> Parser => AdminRequestCmd.Types.SetElementaryStateAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[1];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.SetElementaryStateAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public SetElementaryStateAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public SetElementaryStateAdminCmd(
          AdminRequestCmd.Types.SetElementaryStateAdminCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this.elementaryStateId_ = other.elementaryStateId_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.SetElementaryStateAdminCmd Clone() => new AdminRequestCmd.Types.SetElementaryStateAdminCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int ElementaryStateId
        {
          get => this.elementaryStateId_;
          set => this.elementaryStateId_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.SetElementaryStateAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(
          AdminRequestCmd.Types.SetElementaryStateAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && this.ElementaryStateId == other.ElementaryStateId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.TargetEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.TargetEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.ElementaryStateId != 0)
          {
            int num3 = hashCode1;
            num1 = this.ElementaryStateId;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this.ElementaryStateId != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.ElementaryStateId);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this.ElementaryStateId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.ElementaryStateId);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(
          AdminRequestCmd.Types.SetElementaryStateAdminCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          if (other.ElementaryStateId != 0)
            this.ElementaryStateId = other.ElementaryStateId;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.TargetEntityId = input.ReadInt32();
                continue;
              case 16:
                this.ElementaryStateId = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (SetElementaryStateAdminCmd);
      }

      public sealed class DealDamageAdminCmd : 
        IMessage<AdminRequestCmd.Types.DealDamageAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.DealDamageAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.DealDamageAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.DealDamageAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.DealDamageAdminCmd>((Func<AdminRequestCmd.Types.DealDamageAdminCmd>) (() => new AdminRequestCmd.Types.DealDamageAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;
        public const int MagicalFieldNumber = 3;
        private bool magical_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.DealDamageAdminCmd> Parser => AdminRequestCmd.Types.DealDamageAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[2];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.DealDamageAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public DealDamageAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public DealDamageAdminCmd(AdminRequestCmd.Types.DealDamageAdminCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this.quantity_ = other.quantity_;
          this.magical_ = other.magical_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.DealDamageAdminCmd Clone() => new AdminRequestCmd.Types.DealDamageAdminCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public bool Magical
        {
          get => this.magical_;
          set => this.magical_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.DealDamageAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.DealDamageAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && this.Quantity == other.Quantity && this.Magical == other.Magical && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.TargetEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.TargetEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.Magical)
            hashCode1 ^= this.Magical.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this.Magical)
          {
            output.WriteRawTag((byte) 24);
            output.WriteBool(this.Magical);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this.Magical)
            size += 2;
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.DealDamageAdminCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          if (other.Magical)
            this.Magical = other.Magical;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.TargetEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              case 24:
                this.Magical = input.ReadBool();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (DealDamageAdminCmd);
      }

      public sealed class KillAdminCmd : 
        IMessage<AdminRequestCmd.Types.KillAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.KillAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.KillAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.KillAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.KillAdminCmd>((Func<AdminRequestCmd.Types.KillAdminCmd>) (() => new AdminRequestCmd.Types.KillAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.KillAdminCmd> Parser => AdminRequestCmd.Types.KillAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[3];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.KillAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public KillAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public KillAdminCmd(AdminRequestCmd.Types.KillAdminCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.KillAdminCmd Clone() => new AdminRequestCmd.Types.KillAdminCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.KillAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.KillAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.TargetEntityId != 0)
            hashCode ^= this.TargetEntityId.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.KillAdminCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            if (num != 8U)
              this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
            else
              this.TargetEntityId = input.ReadInt32();
          }
        }

        public string ToDiagnosticString() => nameof (KillAdminCmd);
      }

      public sealed class TeleportAdminCmd : 
        IMessage<AdminRequestCmd.Types.TeleportAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.TeleportAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.TeleportAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.TeleportAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.TeleportAdminCmd>((Func<AdminRequestCmd.Types.TeleportAdminCmd>) (() => new AdminRequestCmd.Types.TeleportAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;
        public const int DestinationFieldNumber = 2;
        private CellCoord destination_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.TeleportAdminCmd> Parser => AdminRequestCmd.Types.TeleportAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[4];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.TeleportAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public TeleportAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public TeleportAdminCmd(AdminRequestCmd.Types.TeleportAdminCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this.destination_ = other.destination_ != null ? other.destination_.Clone() : (CellCoord) null;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.TeleportAdminCmd Clone() => new AdminRequestCmd.Types.TeleportAdminCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public CellCoord Destination
        {
          get => this.destination_;
          set => this.destination_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.TeleportAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.TeleportAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && object.Equals((object) this.Destination, (object) other.Destination) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode = 1;
          if (this.TargetEntityId != 0)
            hashCode ^= this.TargetEntityId.GetHashCode();
          if (this.destination_ != null)
            hashCode ^= this.Destination.GetHashCode();
          if (this._unknownFields != null)
            hashCode ^= this._unknownFields.GetHashCode();
          return hashCode;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this.destination_ != null)
          {
            output.WriteRawTag((byte) 18);
            output.WriteMessage((IMessage) this.Destination);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this.destination_ != null)
            size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Destination);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.TeleportAdminCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          if (other.destination_ != null)
          {
            if (this.destination_ == null)
              this.destination_ = new CellCoord();
            this.Destination.MergeFrom(other.Destination);
          }
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.TargetEntityId = input.ReadInt32();
                continue;
              case 18:
                if (this.destination_ == null)
                  this.destination_ = new CellCoord();
                input.ReadMessage((IMessage) this.destination_);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (TeleportAdminCmd);
      }

      public sealed class DrawSpellsCmd : 
        IMessage<AdminRequestCmd.Types.DrawSpellsCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.DrawSpellsCmd>,
        IDeepCloneable<AdminRequestCmd.Types.DrawSpellsCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.DrawSpellsCmd> _parser = new MessageParser<AdminRequestCmd.Types.DrawSpellsCmd>((Func<AdminRequestCmd.Types.DrawSpellsCmd>) (() => new AdminRequestCmd.Types.DrawSpellsCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.DrawSpellsCmd> Parser => AdminRequestCmd.Types.DrawSpellsCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[5];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.DrawSpellsCmd.Descriptor;

        [DebuggerNonUserCode]
        public DrawSpellsCmd()
        {
        }

        [DebuggerNonUserCode]
        public DrawSpellsCmd(AdminRequestCmd.Types.DrawSpellsCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.DrawSpellsCmd Clone() => new AdminRequestCmd.Types.DrawSpellsCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.DrawSpellsCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.DrawSpellsCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.DrawSpellsCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (DrawSpellsCmd);
      }

      public sealed class DiscardSpellsCmd : 
        IMessage<AdminRequestCmd.Types.DiscardSpellsCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.DiscardSpellsCmd>,
        IDeepCloneable<AdminRequestCmd.Types.DiscardSpellsCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.DiscardSpellsCmd> _parser = new MessageParser<AdminRequestCmd.Types.DiscardSpellsCmd>((Func<AdminRequestCmd.Types.DiscardSpellsCmd>) (() => new AdminRequestCmd.Types.DiscardSpellsCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.DiscardSpellsCmd> Parser => AdminRequestCmd.Types.DiscardSpellsCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[6];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.DiscardSpellsCmd.Descriptor;

        [DebuggerNonUserCode]
        public DiscardSpellsCmd()
        {
        }

        [DebuggerNonUserCode]
        public DiscardSpellsCmd(AdminRequestCmd.Types.DiscardSpellsCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.DiscardSpellsCmd Clone() => new AdminRequestCmd.Types.DiscardSpellsCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.DiscardSpellsCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.DiscardSpellsCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.DiscardSpellsCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (DiscardSpellsCmd);
      }

      public sealed class GainElementPointsCmd : 
        IMessage<AdminRequestCmd.Types.GainElementPointsCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.GainElementPointsCmd>,
        IDeepCloneable<AdminRequestCmd.Types.GainElementPointsCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.GainElementPointsCmd> _parser = new MessageParser<AdminRequestCmd.Types.GainElementPointsCmd>((Func<AdminRequestCmd.Types.GainElementPointsCmd>) (() => new AdminRequestCmd.Types.GainElementPointsCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.GainElementPointsCmd> Parser => AdminRequestCmd.Types.GainElementPointsCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[7];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.GainElementPointsCmd.Descriptor;

        [DebuggerNonUserCode]
        public GainElementPointsCmd()
        {
        }

        [DebuggerNonUserCode]
        public GainElementPointsCmd(AdminRequestCmd.Types.GainElementPointsCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.GainElementPointsCmd Clone() => new AdminRequestCmd.Types.GainElementPointsCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.GainElementPointsCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.GainElementPointsCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.GainElementPointsCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (GainElementPointsCmd);
      }

      public sealed class GainActionPointsCmd : 
        IMessage<AdminRequestCmd.Types.GainActionPointsCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.GainActionPointsCmd>,
        IDeepCloneable<AdminRequestCmd.Types.GainActionPointsCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.GainActionPointsCmd> _parser = new MessageParser<AdminRequestCmd.Types.GainActionPointsCmd>((Func<AdminRequestCmd.Types.GainActionPointsCmd>) (() => new AdminRequestCmd.Types.GainActionPointsCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.GainActionPointsCmd> Parser => AdminRequestCmd.Types.GainActionPointsCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[8];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.GainActionPointsCmd.Descriptor;

        [DebuggerNonUserCode]
        public GainActionPointsCmd()
        {
        }

        [DebuggerNonUserCode]
        public GainActionPointsCmd(AdminRequestCmd.Types.GainActionPointsCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.GainActionPointsCmd Clone() => new AdminRequestCmd.Types.GainActionPointsCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.GainActionPointsCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.GainActionPointsCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.GainActionPointsCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (GainActionPointsCmd);
      }

      public sealed class GainReservePointsCmd : 
        IMessage<AdminRequestCmd.Types.GainReservePointsCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.GainReservePointsCmd>,
        IDeepCloneable<AdminRequestCmd.Types.GainReservePointsCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.GainReservePointsCmd> _parser = new MessageParser<AdminRequestCmd.Types.GainReservePointsCmd>((Func<AdminRequestCmd.Types.GainReservePointsCmd>) (() => new AdminRequestCmd.Types.GainReservePointsCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.GainReservePointsCmd> Parser => AdminRequestCmd.Types.GainReservePointsCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[9];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.GainReservePointsCmd.Descriptor;

        [DebuggerNonUserCode]
        public GainReservePointsCmd()
        {
        }

        [DebuggerNonUserCode]
        public GainReservePointsCmd(AdminRequestCmd.Types.GainReservePointsCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.GainReservePointsCmd Clone() => new AdminRequestCmd.Types.GainReservePointsCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.GainReservePointsCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.GainReservePointsCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.GainReservePointsCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (GainReservePointsCmd);
      }

      public sealed class PickSpellCmd : 
        IMessage<AdminRequestCmd.Types.PickSpellCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.PickSpellCmd>,
        IDeepCloneable<AdminRequestCmd.Types.PickSpellCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.PickSpellCmd> _parser = new MessageParser<AdminRequestCmd.Types.PickSpellCmd>((Func<AdminRequestCmd.Types.PickSpellCmd>) (() => new AdminRequestCmd.Types.PickSpellCmd()));
        private UnknownFieldSet _unknownFields;
        public const int PlayerEntityIdFieldNumber = 1;
        private int playerEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;
        public const int SpellDefinitionIdFieldNumber = 3;
        private int spellDefinitionId_;
        public const int SpellLevelFieldNumber = 4;
        private int spellLevel_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.PickSpellCmd> Parser => AdminRequestCmd.Types.PickSpellCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[10];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.PickSpellCmd.Descriptor;

        [DebuggerNonUserCode]
        public PickSpellCmd()
        {
        }

        [DebuggerNonUserCode]
        public PickSpellCmd(AdminRequestCmd.Types.PickSpellCmd other)
          : this()
        {
          this.playerEntityId_ = other.playerEntityId_;
          this.quantity_ = other.quantity_;
          this.spellDefinitionId_ = other.spellDefinitionId_;
          this.spellLevel_ = other.spellLevel_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.PickSpellCmd Clone() => new AdminRequestCmd.Types.PickSpellCmd(this);

        [DebuggerNonUserCode]
        public int PlayerEntityId
        {
          get => this.playerEntityId_;
          set => this.playerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public int SpellDefinitionId
        {
          get => this.spellDefinitionId_;
          set => this.spellDefinitionId_ = value;
        }

        [DebuggerNonUserCode]
        public int SpellLevel
        {
          get => this.spellLevel_;
          set => this.spellLevel_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.PickSpellCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.PickSpellCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.PlayerEntityId == other.PlayerEntityId && this.Quantity == other.Quantity && this.SpellDefinitionId == other.SpellDefinitionId && this.SpellLevel == other.SpellLevel && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.PlayerEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.PlayerEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.SpellDefinitionId != 0)
          {
            int num4 = hashCode1;
            num1 = this.SpellDefinitionId;
            int hashCode4 = num1.GetHashCode();
            hashCode1 = num4 ^ hashCode4;
          }
          if (this.SpellLevel != 0)
          {
            int num5 = hashCode1;
            num1 = this.SpellLevel;
            int hashCode5 = num1.GetHashCode();
            hashCode1 = num5 ^ hashCode5;
          }
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.PlayerEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.PlayerEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this.SpellDefinitionId != 0)
          {
            output.WriteRawTag((byte) 24);
            output.WriteInt32(this.SpellDefinitionId);
          }
          if (this.SpellLevel != 0)
          {
            output.WriteRawTag((byte) 32);
            output.WriteInt32(this.SpellLevel);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.PlayerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.PlayerEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this.SpellDefinitionId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.SpellDefinitionId);
          if (this.SpellLevel != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.SpellLevel);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.PickSpellCmd other)
        {
          if (other == null)
            return;
          if (other.PlayerEntityId != 0)
            this.PlayerEntityId = other.PlayerEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          if (other.SpellDefinitionId != 0)
            this.SpellDefinitionId = other.SpellDefinitionId;
          if (other.SpellLevel != 0)
            this.SpellLevel = other.SpellLevel;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.PlayerEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              case 24:
                this.SpellDefinitionId = input.ReadInt32();
                continue;
              case 32:
                this.SpellLevel = input.ReadInt32();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (PickSpellCmd);
      }

      public sealed class HealAdminCmd : 
        IMessage<AdminRequestCmd.Types.HealAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.HealAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.HealAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.HealAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.HealAdminCmd>((Func<AdminRequestCmd.Types.HealAdminCmd>) (() => new AdminRequestCmd.Types.HealAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int TargetEntityIdFieldNumber = 1;
        private int targetEntityId_;
        public const int QuantityFieldNumber = 2;
        private int quantity_;
        public const int MagicalFieldNumber = 3;
        private bool magical_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.HealAdminCmd> Parser => AdminRequestCmd.Types.HealAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[11];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.HealAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public HealAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public HealAdminCmd(AdminRequestCmd.Types.HealAdminCmd other)
          : this()
        {
          this.targetEntityId_ = other.targetEntityId_;
          this.quantity_ = other.quantity_;
          this.magical_ = other.magical_;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.HealAdminCmd Clone() => new AdminRequestCmd.Types.HealAdminCmd(this);

        [DebuggerNonUserCode]
        public int TargetEntityId
        {
          get => this.targetEntityId_;
          set => this.targetEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int Quantity
        {
          get => this.quantity_;
          set => this.quantity_ = value;
        }

        [DebuggerNonUserCode]
        public bool Magical
        {
          get => this.magical_;
          set => this.magical_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.HealAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(AdminRequestCmd.Types.HealAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.TargetEntityId == other.TargetEntityId && this.Quantity == other.Quantity && this.Magical == other.Magical && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.TargetEntityId != 0)
          {
            int num2 = hashCode1;
            num1 = this.TargetEntityId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.Quantity != 0)
          {
            int num3 = hashCode1;
            num1 = this.Quantity;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.Magical)
            hashCode1 ^= this.Magical.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.TargetEntityId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.TargetEntityId);
          }
          if (this.Quantity != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.Quantity);
          }
          if (this.Magical)
          {
            output.WriteRawTag((byte) 24);
            output.WriteBool(this.Magical);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.TargetEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.TargetEntityId);
          if (this.Quantity != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.Quantity);
          if (this.Magical)
            size += 2;
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(AdminRequestCmd.Types.HealAdminCmd other)
        {
          if (other == null)
            return;
          if (other.TargetEntityId != 0)
            this.TargetEntityId = other.TargetEntityId;
          if (other.Quantity != 0)
            this.Quantity = other.Quantity;
          if (other.Magical)
            this.Magical = other.Magical;
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.TargetEntityId = input.ReadInt32();
                continue;
              case 16:
                this.Quantity = input.ReadInt32();
                continue;
              case 24:
                this.Magical = input.ReadBool();
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (HealAdminCmd);
      }

      public sealed class InvokeSummoningAdminCmd : 
        IMessage<AdminRequestCmd.Types.InvokeSummoningAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.InvokeSummoningAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.InvokeSummoningAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.InvokeSummoningAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.InvokeSummoningAdminCmd>((Func<AdminRequestCmd.Types.InvokeSummoningAdminCmd>) (() => new AdminRequestCmd.Types.InvokeSummoningAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int DefinitionIdFieldNumber = 1;
        private int definitionId_;
        public const int OwnerEntityIdFieldNumber = 2;
        private int ownerEntityId_;
        public const int SummoningLevelFieldNumber = 3;
        private int summoningLevel_;
        public const int DestinationFieldNumber = 4;
        private CellCoord destination_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.InvokeSummoningAdminCmd> Parser => AdminRequestCmd.Types.InvokeSummoningAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[12];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.InvokeSummoningAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public InvokeSummoningAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public InvokeSummoningAdminCmd(
          AdminRequestCmd.Types.InvokeSummoningAdminCmd other)
          : this()
        {
          this.definitionId_ = other.definitionId_;
          this.ownerEntityId_ = other.ownerEntityId_;
          this.summoningLevel_ = other.summoningLevel_;
          this.destination_ = other.destination_ != null ? other.destination_.Clone() : (CellCoord) null;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.InvokeSummoningAdminCmd Clone() => new AdminRequestCmd.Types.InvokeSummoningAdminCmd(this);

        [DebuggerNonUserCode]
        public int DefinitionId
        {
          get => this.definitionId_;
          set => this.definitionId_ = value;
        }

        [DebuggerNonUserCode]
        public int OwnerEntityId
        {
          get => this.ownerEntityId_;
          set => this.ownerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int SummoningLevel
        {
          get => this.summoningLevel_;
          set => this.summoningLevel_ = value;
        }

        [DebuggerNonUserCode]
        public CellCoord Destination
        {
          get => this.destination_;
          set => this.destination_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.InvokeSummoningAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(
          AdminRequestCmd.Types.InvokeSummoningAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.DefinitionId == other.DefinitionId && this.OwnerEntityId == other.OwnerEntityId && this.SummoningLevel == other.SummoningLevel && object.Equals((object) this.Destination, (object) other.Destination) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.DefinitionId != 0)
          {
            int num2 = hashCode1;
            num1 = this.DefinitionId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.OwnerEntityId != 0)
          {
            int num3 = hashCode1;
            num1 = this.OwnerEntityId;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.SummoningLevel != 0)
          {
            int num4 = hashCode1;
            num1 = this.SummoningLevel;
            int hashCode4 = num1.GetHashCode();
            hashCode1 = num4 ^ hashCode4;
          }
          if (this.destination_ != null)
            hashCode1 ^= this.Destination.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.DefinitionId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.DefinitionId);
          }
          if (this.OwnerEntityId != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.OwnerEntityId);
          }
          if (this.SummoningLevel != 0)
          {
            output.WriteRawTag((byte) 24);
            output.WriteInt32(this.SummoningLevel);
          }
          if (this.destination_ != null)
          {
            output.WriteRawTag((byte) 34);
            output.WriteMessage((IMessage) this.Destination);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.DefinitionId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.DefinitionId);
          if (this.OwnerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.OwnerEntityId);
          if (this.SummoningLevel != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.SummoningLevel);
          if (this.destination_ != null)
            size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Destination);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(
          AdminRequestCmd.Types.InvokeSummoningAdminCmd other)
        {
          if (other == null)
            return;
          if (other.DefinitionId != 0)
            this.DefinitionId = other.DefinitionId;
          if (other.OwnerEntityId != 0)
            this.OwnerEntityId = other.OwnerEntityId;
          if (other.SummoningLevel != 0)
            this.SummoningLevel = other.SummoningLevel;
          if (other.destination_ != null)
          {
            if (this.destination_ == null)
              this.destination_ = new CellCoord();
            this.Destination.MergeFrom(other.Destination);
          }
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.DefinitionId = input.ReadInt32();
                continue;
              case 16:
                this.OwnerEntityId = input.ReadInt32();
                continue;
              case 24:
                this.SummoningLevel = input.ReadInt32();
                continue;
              case 34:
                if (this.destination_ == null)
                  this.destination_ = new CellCoord();
                input.ReadMessage((IMessage) this.destination_);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (InvokeSummoningAdminCmd);
      }

      public sealed class InvokeCompanionAdminCmd : 
        IMessage<AdminRequestCmd.Types.InvokeCompanionAdminCmd>,
        IMessage,
        IEquatable<AdminRequestCmd.Types.InvokeCompanionAdminCmd>,
        IDeepCloneable<AdminRequestCmd.Types.InvokeCompanionAdminCmd>,
        ICustomDiagnosticMessage
      {
        private static readonly MessageParser<AdminRequestCmd.Types.InvokeCompanionAdminCmd> _parser = new MessageParser<AdminRequestCmd.Types.InvokeCompanionAdminCmd>((Func<AdminRequestCmd.Types.InvokeCompanionAdminCmd>) (() => new AdminRequestCmd.Types.InvokeCompanionAdminCmd()));
        private UnknownFieldSet _unknownFields;
        public const int DefinitionIdFieldNumber = 1;
        private int definitionId_;
        public const int OwnerEntityIdFieldNumber = 2;
        private int ownerEntityId_;
        public const int CompanionLevelFieldNumber = 3;
        private int companionLevel_;
        public const int DestinationFieldNumber = 4;
        private CellCoord destination_;

        [DebuggerNonUserCode]
        public static MessageParser<AdminRequestCmd.Types.InvokeCompanionAdminCmd> Parser => AdminRequestCmd.Types.InvokeCompanionAdminCmd._parser;

        [DebuggerNonUserCode]
        public static MessageDescriptor Descriptor => AdminRequestCmd.Descriptor.NestedTypes[13];

        [DebuggerNonUserCode]
        MessageDescriptor IMessage.pb\u003A\u003AGoogle\u002EProtobuf\u002EIMessage\u002EDescriptor => AdminRequestCmd.Types.InvokeCompanionAdminCmd.Descriptor;

        [DebuggerNonUserCode]
        public InvokeCompanionAdminCmd()
        {
        }

        [DebuggerNonUserCode]
        public InvokeCompanionAdminCmd(
          AdminRequestCmd.Types.InvokeCompanionAdminCmd other)
          : this()
        {
          this.definitionId_ = other.definitionId_;
          this.ownerEntityId_ = other.ownerEntityId_;
          this.companionLevel_ = other.companionLevel_;
          this.destination_ = other.destination_ != null ? other.destination_.Clone() : (CellCoord) null;
          this._unknownFields = UnknownFieldSet.Clone(other._unknownFields);
        }

        [DebuggerNonUserCode]
        public AdminRequestCmd.Types.InvokeCompanionAdminCmd Clone() => new AdminRequestCmd.Types.InvokeCompanionAdminCmd(this);

        [DebuggerNonUserCode]
        public int DefinitionId
        {
          get => this.definitionId_;
          set => this.definitionId_ = value;
        }

        [DebuggerNonUserCode]
        public int OwnerEntityId
        {
          get => this.ownerEntityId_;
          set => this.ownerEntityId_ = value;
        }

        [DebuggerNonUserCode]
        public int CompanionLevel
        {
          get => this.companionLevel_;
          set => this.companionLevel_ = value;
        }

        [DebuggerNonUserCode]
        public CellCoord Destination
        {
          get => this.destination_;
          set => this.destination_ = value;
        }

        [DebuggerNonUserCode]
        public override bool Equals(object other) => this.Equals(other as AdminRequestCmd.Types.InvokeCompanionAdminCmd);

        [DebuggerNonUserCode]
        public bool Equals(
          AdminRequestCmd.Types.InvokeCompanionAdminCmd other)
        {
          if (other == null)
            return false;
          if (other == this)
            return true;
          return this.DefinitionId == other.DefinitionId && this.OwnerEntityId == other.OwnerEntityId && this.CompanionLevel == other.CompanionLevel && object.Equals((object) this.Destination, (object) other.Destination) && object.Equals((object) this._unknownFields, (object) other._unknownFields);
        }

        [DebuggerNonUserCode]
        public override int GetHashCode()
        {
          int hashCode1 = 1;
          int num1;
          if (this.DefinitionId != 0)
          {
            int num2 = hashCode1;
            num1 = this.DefinitionId;
            int hashCode2 = num1.GetHashCode();
            hashCode1 = num2 ^ hashCode2;
          }
          if (this.OwnerEntityId != 0)
          {
            int num3 = hashCode1;
            num1 = this.OwnerEntityId;
            int hashCode3 = num1.GetHashCode();
            hashCode1 = num3 ^ hashCode3;
          }
          if (this.CompanionLevel != 0)
          {
            int num4 = hashCode1;
            num1 = this.CompanionLevel;
            int hashCode4 = num1.GetHashCode();
            hashCode1 = num4 ^ hashCode4;
          }
          if (this.destination_ != null)
            hashCode1 ^= this.Destination.GetHashCode();
          if (this._unknownFields != null)
            hashCode1 ^= this._unknownFields.GetHashCode();
          return hashCode1;
        }

        [DebuggerNonUserCode]
        public override string ToString() => JsonFormatter.ToDiagnosticString((IMessage) this);

        [DebuggerNonUserCode]
        public void WriteTo(CodedOutputStream output)
        {
          if (this.DefinitionId != 0)
          {
            output.WriteRawTag((byte) 8);
            output.WriteInt32(this.DefinitionId);
          }
          if (this.OwnerEntityId != 0)
          {
            output.WriteRawTag((byte) 16);
            output.WriteInt32(this.OwnerEntityId);
          }
          if (this.CompanionLevel != 0)
          {
            output.WriteRawTag((byte) 24);
            output.WriteInt32(this.CompanionLevel);
          }
          if (this.destination_ != null)
          {
            output.WriteRawTag((byte) 34);
            output.WriteMessage((IMessage) this.Destination);
          }
          if (this._unknownFields == null)
            return;
          this._unknownFields.WriteTo(output);
        }

        [DebuggerNonUserCode]
        public int CalculateSize()
        {
          int size = 0;
          if (this.DefinitionId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.DefinitionId);
          if (this.OwnerEntityId != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.OwnerEntityId);
          if (this.CompanionLevel != 0)
            size += 1 + CodedOutputStream.ComputeInt32Size(this.CompanionLevel);
          if (this.destination_ != null)
            size += 1 + CodedOutputStream.ComputeMessageSize((IMessage) this.Destination);
          if (this._unknownFields != null)
            size += this._unknownFields.CalculateSize();
          return size;
        }

        [DebuggerNonUserCode]
        public void MergeFrom(
          AdminRequestCmd.Types.InvokeCompanionAdminCmd other)
        {
          if (other == null)
            return;
          if (other.DefinitionId != 0)
            this.DefinitionId = other.DefinitionId;
          if (other.OwnerEntityId != 0)
            this.OwnerEntityId = other.OwnerEntityId;
          if (other.CompanionLevel != 0)
            this.CompanionLevel = other.CompanionLevel;
          if (other.destination_ != null)
          {
            if (this.destination_ == null)
              this.destination_ = new CellCoord();
            this.Destination.MergeFrom(other.Destination);
          }
          this._unknownFields = UnknownFieldSet.MergeFrom(this._unknownFields, other._unknownFields);
        }

        [DebuggerNonUserCode]
        public void MergeFrom(CodedInputStream input)
        {
          uint num;
          while ((num = input.ReadTag()) != 0U)
          {
            switch (num)
            {
              case 8:
                this.DefinitionId = input.ReadInt32();
                continue;
              case 16:
                this.OwnerEntityId = input.ReadInt32();
                continue;
              case 24:
                this.CompanionLevel = input.ReadInt32();
                continue;
              case 34:
                if (this.destination_ == null)
                  this.destination_ = new CellCoord();
                input.ReadMessage((IMessage) this.destination_);
                continue;
              default:
                this._unknownFields = UnknownFieldSet.MergeFieldFrom(this._unknownFields, input);
                continue;
            }
          }
        }

        public string ToDiagnosticString() => nameof (InvokeCompanionAdminCmd);
      }
    }
  }
}
