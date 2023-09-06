// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PlayerAvatarDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class PlayerAvatarDemo : MonoBehaviour
  {
    private Coroutine m_loadingCoroutine;
    private WeaponDefinition m_weaponDefinition;
    [SerializeField]
    private Image m_illu;
    [SerializeField]
    private RawTextField m_name;

    public string nickname
    {
      set => this.m_name.SetText(value);
    }

    public int weaponId
    {
      set
      {
        WeaponDefinition weaponDefinition;
        if (RuntimeData.weaponDefinitions.TryGetValue(value, out weaponDefinition))
          this.weaponDefinition = weaponDefinition;
        else
          Log.Error(string.Format("Cannot find weapon definition with id {0}", (object) value), 29, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\UI\\PlayerAvatarDemo.cs");
      }
    }

    public WeaponDefinition weaponDefinition
    {
      set
      {
        this.m_weaponDefinition = value;
        this.UpdateSprite();
      }
    }

    private void OnEnable() => this.UpdateSprite();

    private void OnDisable()
    {
      if (this.m_loadingCoroutine == null)
        return;
      this.StopCoroutine(this.m_loadingCoroutine);
      this.m_loadingCoroutine = (Coroutine) null;
    }

    private void UpdateSprite()
    {
      if (!this.isActiveAndEnabled)
        return;
      if ((UnityEngine.Object) this.m_weaponDefinition == (UnityEngine.Object) null)
      {
        this.m_illu.sprite = (Sprite) null;
      }
      else
      {
        if (this.m_loadingCoroutine != null)
        {
          this.StopCoroutine(this.m_loadingCoroutine);
          this.m_illu.sprite = (Sprite) null;
        }
        this.m_loadingCoroutine = this.StartCoroutine(this.m_weaponDefinition.LoadIllustrationAsync<Sprite>(AssetBundlesUtility.GetUICharacterResourcesBundleName(), this.m_weaponDefinition.GetIllustrationReference(), new Action<Sprite, string>(this.UpdateSpriteCallback)));
      }
    }

    private void UpdateSpriteCallback(Sprite sprite, string loadedBundleName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_illu)
        this.m_illu.sprite = sprite;
      this.m_loadingCoroutine = (Coroutine) null;
    }
  }
}
