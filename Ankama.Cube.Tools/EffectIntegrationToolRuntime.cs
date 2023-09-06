// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.EffectIntegrationToolRuntime
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Configuration;
using Ankama.Cube.Fight;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Tools
{
  [SelectionBase]
  public class EffectIntegrationToolRuntime : MonoBehaviour
  {
    public static bool isReady { get; private set; }

    private void Awake() => ApplicationStarter.InitializeAssetManager();

    private IEnumerator Start()
    {
      yield return (object) ApplicationStarter.ConfigureLocalAssetManager();
      yield return (object) AudioManager.Load();
      yield return (object) FightObjectFactory.Load();
      EffectIntegrationToolRuntime.isReady = true;
    }
  }
}
