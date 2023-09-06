// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.AssetBundlesUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using FMODUnity;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Utility
{
  public static class AssetBundlesUtility
  {
    public const string AudioMasterBundleName = "core/audio/master";
    public const string DataBundleName = "core/data";
    public const string GameDataBundleName = "core/gamedata";
    public const string TextCollectionsBundleName = "core/localization";
    public const string FightMapWrapperBundleName = "core/scenes/maps/fight_maps";
    public const string HavreMapWrapperBundleName = "core/scenes/maps/havre_maps";
    public const string CommonUIBundleName = "core/ui/common";
    public const string FightUIBundleName = "core/scenes/ui/fight";
    public const string TransitionUIBundleName = "core/scenes/ui/transition";
    public const string ExampleUIBundleName = "core/scenes/ui/examples";
    public const string LoginUIBundleName = "core/scenes/ui/login";
    public const string MatchmakingUIBundleName = "core/scenes/ui/matchmaking";
    public const string PopupInfoUIBundleName = "core/scenes/ui/popup";
    public const string WorldUIBundleName = "core/scenes/ui/world";
    public const string DeckUIBundleName = "core/scenes/ui/deck";
    public const string PlayerUIBundleName = "core/scenes/ui/player";
    public const string OptionUIBundleName = "core/scenes/ui/option";
    public const string FightObjectFactoryBundleName = "core/factories/fight_object_factory";
    public const string MapObjectFactoryBundleName = "core/factories/map_object_factory";
    public const string CharacterResourcesBundleFolder = "core/characters/";
    public const string SpellEffectsBundleName = "core/spells/effects";
    public const string UIGodResourcesBundleName = "core/ui/gods";
    public const string UIMatchmakingIlluBundleName = "core/ui/matchmakingui";
    public const string UICharacterResourcesBundleFolder = "core/ui/characters/";
    public const string UICompanionResourcesBundleName = "core/ui/characters/companions";
    public const string UIWeaponResourcesBundleName = "core/ui/characters/heroes";
    public const string UIAnimationResourcesBundleName = "core/ui/characters/animatedcharacters";
    public const string UIWeaponMaterialButtonBundleName = "core/ui/characters/weaponbutton";
    public const string UIObjectMechanismResourcesBundleName = "core/ui/characters/objectmechanisms";
    public const string UISummoningResourcesBundleName = "core/ui/characters/summonings";
    public const string UISpellResourcesBundleName = "core/ui/spells";

    public static bool TryGetAudioBundleName([NotNull] string fileName, [NotNull] out string bundleName)
    {
      string[] strArray = fileName.Split('_');
      if (strArray.Length >= 2)
      {
        string str = strArray[0];
        if (str.Equals("Core", StringComparison.OrdinalIgnoreCase))
        {
          bundleName = "core/audio/" + string.Join("/", strArray, 1, Math.Max(1, strArray.Length - 2)).ToLowerInvariant();
          return true;
        }
        if (str.Equals("OpenWorld", StringComparison.OrdinalIgnoreCase))
        {
          bundleName = "openworld/audio/" + string.Join("/", strArray, 1, Math.Max(1, strArray.Length - 2)).ToLowerInvariant();
          return true;
        }
      }
      bundleName = string.Empty;
      return false;
    }

    [NotNull]
    public static string GetAudioBundleVariant()
    {
      switch (FMODUtility.currentPlatform)
      {
        case FMODPlatform.None:
        case FMODPlatform.Default:
        case FMODPlatform.Count:
          throw new ArgumentException("fmodPlatform");
        case FMODPlatform.PlayInEditor:
        case FMODPlatform.Desktop:
        case FMODPlatform.Windows:
        case FMODPlatform.Mac:
        case FMODPlatform.Linux:
        case FMODPlatform.UWP:
          return "desktop";
        case FMODPlatform.Mobile:
        case FMODPlatform.MobileHigh:
        case FMODPlatform.MobileLow:
        case FMODPlatform.iOS:
        case FMODPlatform.Android:
        case FMODPlatform.WindowsPhone:
          return "mobile";
        case FMODPlatform.Console:
        case FMODPlatform.XboxOne:
        case FMODPlatform.PS4:
        case FMODPlatform.WiiU:
        case FMODPlatform.PSVita:
        case FMODPlatform.AppleTV:
        case FMODPlatform.Switch:
          throw new NotSupportedException();
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static string GetAnimatedCharacterDataBundle(BundleCategory bundleCategory) => "core/characters/" + bundleCategory.GetBundleName();

    public static string GetUICharacterResourcesBundleName() => "core/ui/characters/heroes";

    public static string GetUIAnimatedCharacterResourcesBundleName() => "core/ui/characters/animatedcharacters";

    public static string GetUICharacterResourcesBundleName(CompanionDefinition definition) => "core/ui/characters/companions";

    public static string GetUICharacterResourcesBundleName(SummoningDefinition definition) => "core/ui/characters/summonings";

    public static string GetUICharacterResourcesBundleName(ObjectMechanismDefinition definition) => "core/ui/characters/objectmechanisms";

    public static string GetFightMapAssetBundleName(int fightMapId) => "core/scenes/maps/fight_maps";

    public static string GetUIGodsResourcesBundleName() => "core/ui/gods";
  }
}
