// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.BasicAccountLoadingHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;

namespace Ankama.Cube.Network
{
  public class BasicAccountLoadingHandler : IAccountLoadingHandler, IDisposable
  {
    private readonly PlayerDataFrame m_playerDataFrame;

    public event Action OnAccountLoaded;

    public BasicAccountLoadingHandler()
    {
      this.m_playerDataFrame = new PlayerDataFrame();
      this.m_playerDataFrame.OnPlayerAccountLoaded += new PlayerAccountLoaded(this.OnPlayerAccountLoaded);
    }

    private void OnPlayerAccountLoaded(bool oldFightFound)
    {
      Action onAccountLoaded = this.OnAccountLoaded;
      if (onAccountLoaded == null)
        return;
      onAccountLoaded();
    }

    public void LoadAccount()
    {
      PlayerData.Clean();
      this.m_playerDataFrame.GetPlayerInitialData();
    }

    public void Dispose() => this.m_playerDataFrame.Dispose();
  }
}
