// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Protocols.CommonProtocol.DamageReductionType
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Google.Protobuf.Reflection;

namespace Ankama.Cube.Protocols.CommonProtocol
{
  public enum DamageReductionType
  {
    [OriginalName("UNKNOWN")] Unknown,
    [OriginalName("SHIELD")] Shield,
    [OriginalName("COUNTER")] Counter,
    [OriginalName("PROTECTOR")] Protector,
    [OriginalName("REFLECTION")] Reflection,
    [OriginalName("DAMAGE_PROOF")] DamageProof,
    [OriginalName("RESISTANCE")] Resistance,
    [OriginalName("PETRIFICATION")] Petrification,
  }
}
