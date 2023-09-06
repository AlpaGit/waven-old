// Decompiled with JetBrains decompiler
// Type: UIZaapPlayerPreview
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube;
using Ankama.Cube.Data;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using System.Collections;
using UnityEngine;

public class UIZaapPlayerPreview : MonoBehaviour
{
  [SerializeField]
  private ImageLoader m_imageLoader;
  [SerializeField]
  private RawTextField m_playerField;
  [SerializeField]
  private RawTextField m_playerLevelField;

  public IEnumerator SetPlayerData(PlayerData data)
  {
    WeaponDefinition weaponDefinition = RuntimeData.weaponDefinitions[data.GetCurrentWeapon()];
    this.m_playerField.SetText(data.nickName);
    int? currentWeaponLevel = data.GetCurrentWeaponLevel();
    yield return (object) this.FillInformation(weaponDefinition, currentWeaponLevel.Value);
  }

  public IEnumerator SetPlayerData(FightInfo.Types.Player data)
  {
    WeaponDefinition weaponDefinition = RuntimeData.weaponDefinitions[data.WeaponId.Value];
    this.m_playerField.SetText(data.Name);
    int? nullable = new int?(data.Level);
    yield return (object) this.FillInformation(weaponDefinition, nullable.Value);
  }

  private IEnumerator FillInformation(WeaponDefinition weapon, int level)
  {
    this.m_imageLoader.Setup(weapon.GetIlluMatchmakingReference(), "core/ui/matchmakingui");
    while (this.m_imageLoader.loadState == UIResourceLoadState.Loading)
      yield return (object) null;
    this.m_playerLevelField.SetText(string.Format("Niveau {0}", (object) level));
  }
}
