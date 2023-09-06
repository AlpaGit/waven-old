// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Player.PlayerPreferences
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Utility;
using JetBrains.Annotations;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Ankama.Cube.Player
{
  [PublicAPI]
  public static class PlayerPreferences
  {
    private const string EncryptionKey = "a585c7e0-8291-4f8e-b1c8-a3dc05ca0b49";
    private static bool s_dirty;
    private static bool s_useGuest;
    private static bool s_autoLogin;
    private static string s_lastServer;
    private static string s_lastLogin;
    private static string s_lastPassword;
    private static string s_guestLogin;
    private static string s_guestPassword;
    private static bool s_rememberPassword;
    private static int s_startState;
    private static int s_preferredFightDef;
    private static int s_graphicPresetIndex;
    private static float s_audioMasterVolume;
    private static float s_audioMusicVolume;
    private static float s_audioFxVolume;
    private static float s_audioUiVolume;

    public static bool useGuest
    {
      get => PlayerPreferences.s_useGuest;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_useGuest, value);
    }

    public static bool autoLogin
    {
      get => PlayerPreferences.s_autoLogin;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_autoLogin, value);
    }

    public static string lastServer
    {
      get => PlayerPreferences.s_lastServer;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_lastServer, value);
    }

    public static string lastLogin
    {
      get => PlayerPreferences.Decrypt(PlayerPreferences.s_lastLogin);
      set => PlayerPreferences.EncryptAndUpdate(value, ref PlayerPreferences.s_lastLogin);
    }

    public static string lastPassword
    {
      get => PlayerPreferences.Decrypt(PlayerPreferences.s_lastPassword);
      set => PlayerPreferences.EncryptAndUpdate(value, ref PlayerPreferences.s_lastPassword);
    }

    public static string guestLogin
    {
      get => PlayerPreferences.Decrypt(PlayerPreferences.s_guestLogin);
      set => PlayerPreferences.EncryptAndUpdate(value, ref PlayerPreferences.s_guestLogin);
    }

    public static string guestPassword
    {
      get => PlayerPreferences.Decrypt(PlayerPreferences.s_guestPassword);
      set => PlayerPreferences.EncryptAndUpdate(value, ref PlayerPreferences.s_guestPassword);
    }

    public static bool rememberPassword
    {
      get => PlayerPreferences.s_rememberPassword;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_rememberPassword, value);
    }

    public static int startState
    {
      get => PlayerPreferences.s_startState;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_startState, value);
    }

    public static int preferredFightDef
    {
      get => PlayerPreferences.s_preferredFightDef;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_preferredFightDef, value);
    }

    public static int graphicPresetIndex
    {
      get => PlayerPreferences.s_graphicPresetIndex;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_graphicPresetIndex, value);
    }

    public static float audioMasterVolume
    {
      get => PlayerPreferences.s_audioMasterVolume;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_audioMasterVolume, value);
    }

    public static float audioMusicVolume
    {
      get => PlayerPreferences.s_audioMusicVolume;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_audioMusicVolume, value);
    }

    public static float audioFxVolume
    {
      get => PlayerPreferences.s_audioFxVolume;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_audioFxVolume, value);
    }

    public static float audioUiVolume
    {
      get => PlayerPreferences.s_audioUiVolume;
      set => PlayerPreferences.UpdatePref(ref PlayerPreferences.s_audioUiVolume, value);
    }

    public static void Load()
    {
      PlayerPreferences.s_useGuest = PlayerPrefs.GetInt("Waven.Authentication.UseGuest", 1) != 0;
      PlayerPreferences.s_autoLogin = PlayerPrefs.GetInt("Waven.Authentication.AutoLogin", 0) != 0;
      PlayerPreferences.s_lastServer = PlayerPrefs.GetString("Waven.Authentication.LastServer", "localhost");
      PlayerPreferences.s_lastLogin = PlayerPrefs.GetString("Waven.Authentication.LastLogin", string.Empty);
      PlayerPreferences.s_lastPassword = PlayerPrefs.GetString("Waven.Authentication.LastPassword", string.Empty);
      PlayerPreferences.s_guestLogin = PlayerPrefs.GetString("Waven.Authentication.GuestLogin", string.Empty);
      PlayerPreferences.s_guestPassword = PlayerPrefs.GetString("Waven.Authentication.GuestPassword", string.Empty);
      PlayerPreferences.s_rememberPassword = PlayerPrefs.GetInt("Waven.Authentication.RememberPassword", 0) != 0;
      PlayerPreferences.s_startState = PlayerPrefs.GetInt("Waven.Game.StartState", 1);
      PlayerPreferences.s_preferredFightDef = PlayerPrefs.GetInt("Waven.Game.PreferredFightDef", -1);
      PlayerPreferences.s_graphicPresetIndex = PlayerPrefs.GetInt("Waven.Graphics.Alpha.PreferredGraphicQualityIndexV2", -1);
      PlayerPreferences.s_audioMasterVolume = PlayerPrefs.GetFloat("Waven.Audio.MasterVolume", 1f);
      PlayerPreferences.s_audioMusicVolume = PlayerPrefs.GetFloat("Waven.Audio.MusicVolume", 1f);
      PlayerPreferences.s_audioFxVolume = PlayerPrefs.GetFloat("Waven.Audio.FxVolume", 1f);
      PlayerPreferences.s_audioUiVolume = PlayerPrefs.GetFloat("Waven.Audio.UiVolume", 1f);
    }

    public static void Save()
    {
      if (!PlayerPreferences.s_dirty)
        return;
      PlayerPrefs.SetInt("Waven.Authentication.UseGuest", PlayerPreferences.s_useGuest ? 1 : 0);
      PlayerPrefs.SetInt("Waven.Authentication.AutoLogin", PlayerPreferences.s_autoLogin ? 1 : 0);
      PlayerPrefs.SetString("Waven.Authentication.LastServer", PlayerPreferences.s_lastServer ?? "localhost");
      PlayerPrefs.SetString("Waven.Authentication.LastLogin", PlayerPreferences.s_lastLogin);
      PlayerPrefs.SetString("Waven.Authentication.LastPassword", PlayerPreferences.s_lastPassword);
      PlayerPrefs.SetString("Waven.Authentication.GuestLogin", PlayerPreferences.s_guestLogin);
      PlayerPrefs.SetString("Waven.Authentication.GuestPassword", PlayerPreferences.s_guestPassword);
      PlayerPrefs.SetInt("Waven.Authentication.RememberPassword", PlayerPreferences.s_rememberPassword ? 1 : 0);
      PlayerPrefs.SetInt("Waven.Game.StartState", PlayerPreferences.s_startState);
      PlayerPrefs.SetInt("Waven.Game.PreferredFightDef", PlayerPreferences.s_preferredFightDef);
      PlayerPrefs.SetInt("Waven.Graphics.Alpha.PreferredGraphicQualityIndexV2", PlayerPreferences.s_graphicPresetIndex);
      PlayerPrefs.SetFloat("Waven.Audio.MasterVolume", PlayerPreferences.s_audioMasterVolume);
      PlayerPrefs.SetFloat("Waven.Audio.MusicVolume", PlayerPreferences.s_audioMusicVolume);
      PlayerPrefs.SetFloat("Waven.Audio.FxVolume", PlayerPreferences.s_audioFxVolume);
      PlayerPrefs.SetFloat("Waven.Audio.UiVolume", PlayerPreferences.s_audioUiVolume);
    }

    public static void InitializeAudioPreference()
    {
      AudioManager.SetVolume(AudioBusIdentifier.Master, PlayerPreferences.audioMasterVolume);
      AudioManager.SetVolume(AudioBusIdentifier.Music, PlayerPreferences.audioMusicVolume);
      AudioManager.SetVolume(AudioBusIdentifier.SFX, PlayerPreferences.audioFxVolume);
      AudioManager.SetVolume(AudioBusIdentifier.UI, PlayerPreferences.audioUiVolume);
    }

    private static void UpdatePref(ref string value, string update)
    {
      if (string.Equals(update, value))
        return;
      value = update;
      PlayerPreferences.s_dirty = true;
    }

    private static void UpdatePref(ref float value, float update)
    {
      if ((double) update == (double) value)
        return;
      value = update;
      PlayerPreferences.s_dirty = true;
    }

    private static void UpdatePref(ref int value, int update)
    {
      if (update == value)
        return;
      value = update;
      PlayerPreferences.s_dirty = true;
    }

    private static void UpdatePref(ref bool value, bool update)
    {
      if (update == value)
        return;
      value = update;
      PlayerPreferences.s_dirty = true;
    }

    private static string Decrypt(string word) => !string.IsNullOrEmpty(word) ? StringCipher.Decrypt(word, "a585c7e0-8291-4f8e-b1c8-a3dc05ca0b49") : word;

    private static void EncryptAndUpdate(string value, ref string previousValue)
    {
      string a = StringCipher.Encrypt(value, "a585c7e0-8291-4f8e-b1c8-a3dc05ca0b49");
      if (string.Equals(a, previousValue))
        return;
      previousValue = a;
      PlayerPreferences.s_dirty = true;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct PrefKeys
    {
      public const string AuthenticationUseGuest = "Waven.Authentication.UseGuest";
      public const string AuthenticationAutoLogin = "Waven.Authentication.AutoLogin";
      public const string AuthenticationLastServer = "Waven.Authentication.LastServer";
      public const string AuthenticationLastLogin = "Waven.Authentication.LastLogin";
      public const string AuthenticationLastPassword = "Waven.Authentication.LastPassword";
      public const string AuthenticationGuestLogin = "Waven.Authentication.GuestLogin";
      public const string AuthenticationGuestPassword = "Waven.Authentication.GuestPassword";
      public const string AuthenticationRememberPassword = "Waven.Authentication.RememberPassword";
      public const string GameStartState = "Waven.Game.StartState";
      public const string GameSelectedSquadIdentifier = "Waven.Game.SelectedSquadIdentifier";
      public const string GamePreferredSquad = "Waven.Game.PreferredSquad";
      public const string GamePreferredFightDef = "Waven.Game.PreferredFightDef";
      public const string PreferredGraphicQualityIndex = "Waven.Graphics.Alpha.PreferredGraphicQualityIndexV2";
      public const string AudioMasterVolume = "Waven.Audio.MasterVolume";
      public const string AudioMusicVolume = "Waven.Audio.MusicVolume";
      public const string AudioFxVolume = "Waven.Audio.FxVolume";
      public const string AudioUiVolume = "Waven.Audio.UiVolume";
    }
  }
}
