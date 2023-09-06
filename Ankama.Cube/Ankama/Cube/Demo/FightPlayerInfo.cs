// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.FightPlayerInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.PlayerProtocol;

namespace Ankama.Cube.Demo
{
  public class FightPlayerInfo
  {
    public long Uid;
    public PlayerPublicInfo Info = new PlayerPublicInfo();
    public int? WeaponId;
    public int Level = 1;
  }
}
