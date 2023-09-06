// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MonsterSpawnCellDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using FMODUnity;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Maps/Monster Spawn Cell Definition")]
  public sealed class MonsterSpawnCellDefinition : ScriptableObject
  {
    private const int PoolCapacity = 8;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private VisualEffect m_appearanceEffect;
    [SerializeField]
    private AudioReference m_appearanceSound;
    [SerializeField]
    private float m_appearanceDelay = 0.25f;
    [SerializeField]
    private VisualEffect m_disappearanceEffect;
    [SerializeField]
    private AudioReference m_disappearanceSound;
    [SerializeField]
    private float m_disappearanceDelay = 0.25f;
    private GameObjectPool m_prefabPool;
    private bool m_loadedAudioBank;

    public VisualEffect appearanceEffect => this.m_appearanceEffect;

    public AudioReference appearanceSound => this.m_appearanceSound;

    public float appearanceDelay => this.m_appearanceDelay;

    public VisualEffect disappearanceEffect => this.m_disappearanceEffect;

    public AudioReference disappearanceSound => this.m_disappearanceSound;

    public float disappearanceDelay => this.m_disappearanceDelay;

    public IEnumerator Initialize()
    {
      if (!((Object) null == (Object) this.m_prefab))
      {
        this.m_prefabPool = new GameObjectPool(this.m_prefab, 8);
        if (this.m_appearanceSound.isValid && AudioManager.isReady)
        {
          string bankName;
          if (AudioManager.TryGetDefaultBankName(this.m_appearanceSound, out bankName))
          {
            AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
            while (!bankLoadRequest.isDone)
              yield return (object) null;
            if ((int) bankLoadRequest.error != 0)
            {
              Log.Error(string.Format("Failed to load bank named '{0}': {1}", (object) bankName, (object) bankLoadRequest.error), 105, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Maps\\MonsterSpawnCellDefinition.cs");
              yield break;
            }
            else
            {
              this.m_loadedAudioBank = true;
              bankLoadRequest = (AudioBankLoadRequest) null;
            }
          }
          else
            Log.Warning(string.Format("Could not find a bank to load for event '{0}'.", (object) this.m_appearanceSound), 113, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Maps\\MonsterSpawnCellDefinition.cs");
          bankName = (string) null;
        }
      }
    }

    public void Release()
    {
      if (this.m_prefabPool != null)
      {
        this.m_prefabPool.Dispose();
        this.m_prefabPool = (GameObjectPool) null;
      }
      string bankName;
      if (!this.m_loadedAudioBank || !this.m_appearanceSound.isValid || !AudioManager.isReady || !AudioManager.TryGetDefaultBankName(this.m_appearanceSound, out bankName))
        return;
      AudioManager.UnloadBank(bankName);
    }

    [CanBeNull]
    public GameObject Instantiate(Vector3 position, Quaternion rotation, Transform parent)
    {
      if (this.m_prefabPool != null)
        return this.m_prefabPool.Instantiate(position, rotation, parent);
      Log.Error("Missing prefab for Monster SpawnCell Definition named '" + this.name + "'.", 145, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Maps\\MonsterSpawnCellDefinition.cs");
      return (GameObject) null;
    }

    public void DestroyInstance([NotNull] GameObject instance)
    {
      if (this.m_prefabPool == null)
        Object.Destroy((Object) instance);
      else
        this.m_prefabPool.Release(instance);
    }
  }
}
