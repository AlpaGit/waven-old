// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.InactivityHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;
using Ankama.Utilities;
using UnityEngine;

namespace Ankama.Cube.Demo
{
  public class InactivityHandler : MonoBehaviour
  {
    private const float MAX_INACTIVITY_TIME = 300f;
    private float m_lastActivityTime;

    public static InactivityHandler instance { get; private set; }

    public static void UpdateActivity() => InactivityHandler.instance.UpdateActivityTime();

    private void Awake()
    {
      if ((Object) InactivityHandler.instance != (Object) null)
        Log.Error("Another instance exist", 25, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\InactivityHandler.cs");
      else
        InactivityHandler.instance = this;
    }

    private void OnDestroy()
    {
      if ((Object) InactivityHandler.instance != (Object) this)
        return;
      InactivityHandler.instance = (InactivityHandler) null;
    }

    private void OnEnable() => this.m_lastActivityTime = Time.time;

    private void Update()
    {
      if ((double) Time.time - (double) this.m_lastActivityTime <= 300.0)
        return;
      this.m_lastActivityTime = float.MaxValue;
      StatesUtility.GotoLoginState();
    }

    private void UpdateActivityTime() => this.m_lastActivityTime = Time.time;
  }
}
