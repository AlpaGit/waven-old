// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.Utility;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  public static class AudioManager
  {
    private const string UIMusicTransitionParameterName = "Music_Menu";
    private static AudioManager.InitializationState s_initializationState = AudioManager.InitializationState.None;
    private static bool s_alreadyStartingMusic;
    private static FMODSettings s_settings;
    private static AudioManagerCallbackSource s_callbackSource;
    private static AudioListenerPosition s_listenerPosition;
    private static FMOD.Studio.System s_studioSystem;
    private static FMOD.System s_lowLevelSystem;
    private static readonly Dictionary<string, AudioManager.BankInfo> s_banks = new Dictionary<string, AudioManager.BankInfo>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, AudioManager.BankLoading> s_banksLoading = new Dictionary<string, AudioManager.BankLoading>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static readonly List<string> s_finishedBanksLoading = new List<string>();
    private static readonly Dictionary<Guid, EventDescription> s_cachedDescriptions = new Dictionary<Guid, EventDescription>((IEqualityComparer<Guid>) new AudioManager.GuidComparer());
    private static readonly List<AudioContext> s_audioContexts = new List<AudioContext>(16);
    private static AudioManager.MusicInstance s_worldMusicInstance;
    private static AudioManager.MusicInstance s_worldAmbianceInstance;
    private static readonly List<AudioManager.MusicInstance> s_uiMusicStack = new List<AudioManager.MusicInstance>(2);

    public static bool isReady { get; private set; }

    public static AssetManagerError error { get; private set; } = (AssetManagerError) 0;

    public static FMOD.Studio.System studioSystem => AudioManager.s_studioSystem;

    [CanBeNull]
    public static Coroutine StartCoroutine(IEnumerator routine)
    {
      MonoBehaviour monoBehaviour = (UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_callbackSource ? (MonoBehaviour) AudioManager.s_callbackSource : Main.monoBehaviour;
      return !((UnityEngine.Object) null != (UnityEngine.Object) monoBehaviour) ? (Coroutine) null : monoBehaviour.StartCoroutine(routine);
    }

    public static IEnumerator Load()
    {
      while (!AssetManager.isReady)
        yield return (object) null;
      string audioBundleVariant = AssetBundlesUtility.GetAudioBundleVariant();
      if (!AssetManager.AddActiveVariant(audioBundleVariant))
      {
        AudioManager.error = (AssetManagerError) 30;
        Ankama.Utilities.Log.Error("Could not initialize audio manager: could not add audio bundle variant '" + audioBundleVariant + "'.", 165, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      else
      {
        AudioManager.s_initializationState |= AudioManager.InitializationState.Variant;
        AssetBundleLoadRequest settingsBundleLoadRequest = AssetManager.LoadAssetBundle("core/gamedata");
        while (!settingsBundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) settingsBundleLoadRequest.error != 0)
        {
          AudioManager.error = settingsBundleLoadRequest.error;
          Ankama.Utilities.Log.Error(string.Format("Could not load settings bundle: {0}", (object) AudioManager.error), 182, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        }
        else
        {
          AudioManager.s_initializationState |= AudioManager.InitializationState.GameDataBundle;
          AllAssetsLoadRequest<FMODSettings> settingsLoadRequest = AssetManager.LoadAllAssetsAsync<FMODSettings>("core/gamedata");
          while (!settingsLoadRequest.isDone)
            yield return (object) null;
          if ((int) settingsLoadRequest.error != 0)
          {
            AudioManager.error = settingsLoadRequest.error;
            Ankama.Utilities.Log.Error(string.Format("Could not load settings from master bundle: {0}", (object) AudioManager.error), 199, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
          }
          else
          {
            FMODSettings[] assets = settingsLoadRequest.assets;
            if (assets.Length == 0)
            {
              AudioManager.error = (AssetManagerError) 30;
              Ankama.Utilities.Log.Error("Could not initialize audio manager: settings not found.", 207, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
            }
            else
            {
              FMODSettings settings = assets[0];
              FMODRuntimeCache runtimeCache = settings.runtimeCache;
              AudioManager.s_settings = settings;
              RESULT result1 = AudioManager.StartSystem();
              if (result1 != RESULT.OK)
              {
                AudioManager.error = (AssetManagerError) 30;
                Ankama.Utilities.Log.Error(string.Format("Audio manager did not initialize properly: {0}.", (object) result1), 219, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
              }
              else
              {
                AssetBundleLoadRequest masterBankBundleLoadRequest = AssetManager.LoadAssetBundle("core/audio/master");
                while (!masterBankBundleLoadRequest.isDone)
                  yield return (object) null;
                if ((int) masterBankBundleLoadRequest.error != 0)
                {
                  AudioManager.ReleaseSystemInternal();
                  AudioManager.error = masterBankBundleLoadRequest.error;
                  Ankama.Utilities.Log.Error(string.Format("Could not load master bank bundle: {0}", (object) AudioManager.error), 236, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                }
                else
                {
                  AudioManager.s_initializationState |= AudioManager.InitializationState.MasterBundle;
                  AssetReference assetReference1;
                  if (!runtimeCache.bankReferenceDictionary.TryGetValue(settings.masterBankName + ".strings", out assetReference1))
                  {
                    AudioManager.ReleaseSystemInternal();
                    AudioManager.error = (AssetManagerError) 10;
                    Ankama.Utilities.Log.Error("Could not get strings bank reference.", 249, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                  }
                  else
                  {
                    AssetLoadRequest<TextAsset> stringsBankLoadRequest = assetReference1.LoadFromAssetBundleAsync<TextAsset>("core/audio/master");
                    while (!stringsBankLoadRequest.isDone)
                      yield return (object) null;
                    if ((int) stringsBankLoadRequest.error != 0)
                    {
                      AudioManager.ReleaseSystemInternal();
                      AudioManager.error = stringsBankLoadRequest.error;
                      Ankama.Utilities.Log.Error(string.Format("Could not load strings bank asset: {0}", (object) AudioManager.error), 264, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                    }
                    else
                    {
                      Bank bank;
                      RESULT result2 = AudioManager.LoadMasterBank(stringsBankLoadRequest.asset, out bank);
                      if (result2 != RESULT.OK)
                      {
                        AudioManager.ReleaseSystemInternal();
                        AudioManager.error = (AssetManagerError) 30;
                        Ankama.Utilities.Log.Error(string.Format("Could not load strings bank: {0}", (object) result2), 274, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                      }
                      else
                      {
                        AssetReference assetReference2;
                        if (!runtimeCache.bankReferenceDictionary.TryGetValue(settings.masterBankName, out assetReference2))
                        {
                          AudioManager.ReleaseSystemInternal();
                          AudioManager.error = (AssetManagerError) 10;
                          Ankama.Utilities.Log.Error("Could not get master bank reference.", 285, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                        }
                        else
                        {
                          AssetLoadRequest<TextAsset> masterBankLoadRequest = assetReference2.LoadFromAssetBundleAsync<TextAsset>("core/audio/master");
                          while (!masterBankLoadRequest.isDone)
                            yield return (object) null;
                          if ((int) masterBankLoadRequest.error != 0)
                          {
                            AudioManager.ReleaseSystemInternal();
                            AudioManager.error = masterBankLoadRequest.error;
                            Ankama.Utilities.Log.Error(string.Format("Could not load master bank asset: {0}", (object) AudioManager.error), 300, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                          }
                          else
                          {
                            RESULT result3 = AudioManager.LoadMasterBank(masterBankLoadRequest.asset, out bank);
                            if (result3 != RESULT.OK)
                            {
                              AudioManager.ReleaseSystemInternal();
                              AudioManager.error = (AssetManagerError) 30;
                              Ankama.Utilities.Log.Error(string.Format("Could not load master bank: {0}", (object) result3), 310, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                            }
                            else
                            {
                              int num1 = (int) AudioManager.s_studioSystem.setNumListeners(1);
                              if ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition)
                              {
                                int num2 = (int) AudioManager.s_studioSystem.setListenerAttributes(0, FMODUtility.To3DAttributes(AudioManager.s_listenerPosition.transform));
                              }
                              else
                              {
                                int num3 = (int) AudioManager.s_studioSystem.setListenerAttributes(0, FMODUtility.To3DAttributes(Vector3.zero));
                              }
                              AudioManager.s_callbackSource = AudioManagerCallbackSource.Create();
                              AudioManager.isReady = true;
                              Ankama.Utilities.Log.Info("Initialization complete.", 334, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public static IEnumerator Unload()
    {
      foreach (AudioManager.BankLoading bankLoading in AudioManager.s_banksLoading.Values)
        bankLoading.loadRequest.Cancel();
      AudioManager.s_banksLoading.Clear();
      if (AudioManager.s_studioSystem.isValid())
      {
        AudioWorldMusicRequest.ClearAllRequests();
        List<AudioManager.MusicInstance> uiMusicStack = AudioManager.s_uiMusicStack;
        int count = uiMusicStack.Count;
        for (int index = 0; index < count; ++index)
        {
          FMOD.Studio.EventInstance eventInstance = uiMusicStack[index].eventInstance;
          if (eventInstance.isValid())
          {
            int num1 = (int) eventInstance.stop(STOP_MODE.IMMEDIATE);
            int num2 = (int) eventInstance.release();
            eventInstance.clearHandle();
          }
        }
        uiMusicStack.Clear();
        foreach (KeyValuePair<string, AudioManager.BankInfo> bank1 in AudioManager.s_banks)
        {
          Bank bank2 = bank1.Value.bank;
          RESULT result = bank2.unload();
          bank2.clearHandle();
          if (result != RESULT.OK)
            Ankama.Utilities.Log.Warning(string.Format("Error while unloading bank named '{0}' while unloading audio manager: {1}", (object) bank1.Key, (object) result), 386, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        }
      }
      AudioManager.s_banks.Clear();
      AudioManager.s_cachedDescriptions.Clear();
      AudioManager.ReleaseSystemInternal();
      if ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_callbackSource)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) AudioManager.s_callbackSource.gameObject);
        AudioManager.s_callbackSource = (AudioManagerCallbackSource) null;
      }
      AudioManager.s_settings = (FMODSettings) null;
      if (AudioManager.s_initializationState.HasFlag((Enum) AudioManager.InitializationState.MasterBundle))
      {
        yield return (object) AssetManager.UnloadAssetBundle("core/audio/master");
        AudioManager.s_initializationState &= ~AudioManager.InitializationState.MasterBundle;
      }
      if (AudioManager.s_initializationState.HasFlag((Enum) AudioManager.InitializationState.GameDataBundle))
      {
        yield return (object) AssetManager.UnloadAssetBundle("core/gamedata");
        AudioManager.s_initializationState &= ~AudioManager.InitializationState.GameDataBundle;
      }
      if (AudioManager.s_initializationState.HasFlag((Enum) AudioManager.InitializationState.Variant))
      {
        AssetManager.RemoveActiveVariant(AssetBundlesUtility.GetAudioBundleVariant());
        AudioManager.s_initializationState &= ~AudioManager.InitializationState.Variant;
      }
    }

    public static void UpdateInternal()
    {
      if (!AudioManager.s_studioSystem.isValid())
        return;
      int num = (int) AudioManager.s_studioSystem.update();
      List<AudioContext> audioContexts = AudioManager.s_audioContexts;
      int count1 = audioContexts.Count;
      for (int index = 0; index < count1; ++index)
        audioContexts[index].Cleanup();
      if (AudioManager.s_banksLoading.Count <= 0)
        return;
      foreach (KeyValuePair<string, AudioManager.BankLoading> keyValuePair in AudioManager.s_banksLoading)
      {
        AudioManager.BankLoading bankLoading = keyValuePair.Value;
        AudioBankLoadRequest loadRequest = bankLoading.loadRequest;
        if (loadRequest.UpdateInternal())
        {
          string key = keyValuePair.Key;
          if ((int) loadRequest.error == 0)
          {
            AudioManager.BankInfo bankInfo;
            bankInfo.bank = loadRequest.bank;
            bankInfo.referenceCount = bankLoading.referenceCount;
            bankInfo.bundleName = bankLoading.loadRequest.bundleName;
            bankInfo.completedRequest = loadRequest;
            AudioManager.s_banks.Add(key, bankInfo);
          }
          AudioManager.s_finishedBanksLoading.Add(key);
        }
      }
      List<string> finishedBanksLoading = AudioManager.s_finishedBanksLoading;
      int count2 = finishedBanksLoading.Count;
      for (int index = 0; index < count2; ++index)
      {
        string key = finishedBanksLoading[index];
        AudioManager.s_banksLoading.Remove(key);
      }
      AudioManager.s_finishedBanksLoading.Clear();
    }

    private static RESULT StartSystem()
    {
      RESULT result1 = RESULT.OK;
      try
      {
        if (!AudioManager.s_initializationState.HasFlag((Enum) AudioManager.InitializationState.Assembly))
        {
          FMODUtility.EnforceLibraryOrder();
          AudioManager.s_initializationState |= AudioManager.InitializationState.Assembly;
        }
        FMODSettings settings1 = AudioManager.s_settings;
        FMODPlatform currentPlatform = FMODUtility.currentPlatform;
        int sampleRate = AudioManager.s_settings.GetSampleRate(currentPlatform);
        int realChannels = settings1.GetRealChannels(currentPlatform);
        int virtualChannels = settings1.GetVirtualChannels(currentPlatform);
        SPEAKERMODE speakerMode = (SPEAKERMODE) settings1.GetSpeakerMode(currentPlatform);
        OUTPUTTYPE output = OUTPUTTYPE.AUTODETECT;
        FMOD.ADVANCEDSETTINGS settings2 = new FMOD.ADVANCEDSETTINGS()
        {
          randomSeed = (uint) DateTime.Now.Ticks,
          maxVorbisCodecs = realChannels
        };
        AudioManager.SetThreadAffinity();
        FMOD.Studio.INITFLAGS studioFlags = FMOD.Studio.INITFLAGS.DEFERRED_CALLBACKS;
        if (settings1.IsLiveUpdateEnabled(currentPlatform))
          studioFlags |= FMOD.Studio.INITFLAGS.LIVEUPDATE;
        while (true)
        {
          AudioManager.CheckInitResult(FMOD.Studio.System.create(out AudioManager.s_studioSystem), "FMOD.Studio.System.create");
          AudioManager.CheckInitResult(AudioManager.s_studioSystem.getLowLevelSystem(out AudioManager.s_lowLevelSystem), "FMOD.Studio.System.getLowLevelSystem");
          AudioManager.CheckInitResult(AudioManager.s_lowLevelSystem.setOutput(output), "FMOD.System.setOutput");
          AudioManager.CheckInitResult(AudioManager.s_lowLevelSystem.setSoftwareChannels(realChannels), "FMOD.System.setSoftwareChannels");
          AudioManager.CheckInitResult(AudioManager.s_lowLevelSystem.setSoftwareFormat(sampleRate, speakerMode, 0), "FMOD.System.setSoftwareFormat");
          AudioManager.CheckInitResult(AudioManager.s_lowLevelSystem.setAdvancedSettings(ref settings2), "FMOD.System.setAdvancedSettings");
          RESULT result2 = AudioManager.s_studioSystem.initialize(virtualChannels, studioFlags, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
          if (result2 != RESULT.OK && result1 == RESULT.OK)
          {
            result1 = result2;
            output = OUTPUTTYPE.NOSOUND;
            Ankama.Utilities.Log.Error(string.Format("[FMOD] Studio::System::initialize returned {0}, defaulting to no-sound mode.", (object) result2), 572, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
          }
          else
          {
            AudioManager.CheckInitResult(result2, "Studio::System::initialize");
            if ((studioFlags & FMOD.Studio.INITFLAGS.LIVEUPDATE) != FMOD.Studio.INITFLAGS.NORMAL)
            {
              int num = (int) AudioManager.s_studioSystem.flushCommands();
              if (AudioManager.s_studioSystem.update() == RESULT.ERR_NET_SOCKET_ERROR)
              {
                studioFlags &= ~FMOD.Studio.INITFLAGS.LIVEUPDATE;
                Ankama.Utilities.Log.Warning("[FMOD] Cannot open network port for Live Update (in-use), restarting with Live Update disabled.", 587, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                AudioManager.CheckInitResult(AudioManager.s_studioSystem.release(), "FMOD.Studio.System.Release");
              }
              else
                break;
            }
            else
              break;
          }
        }
      }
      catch (SystemNotInitializedException ex)
      {
        result1 = ex.result;
        Ankama.Utilities.Log.Error("Encountered an error while starting FMOD systems: " + ex.Message, 599, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      catch (Exception ex)
      {
        result1 = RESULT.ERR_INITIALIZATION;
        Ankama.Utilities.Log.Error("Encountered an exception while starting FMOD systems: " + ex.Message + ".", 604, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      return result1;
    }

    public static void BackupSystemInternal(
      out IntPtr studioHandle,
      out IntPtr lowLevelStudioHandle)
    {
      if (AudioManager.s_studioSystem.isValid())
      {
        studioHandle = AudioManager.s_studioSystem.handle;
        lowLevelStudioHandle = AudioManager.s_lowLevelSystem.handle;
      }
      else
      {
        studioHandle = IntPtr.Zero;
        lowLevelStudioHandle = IntPtr.Zero;
      }
    }

    public static void RestoreSystemInternal(IntPtr studioHandle, IntPtr lowLevelStudioHandle)
    {
      AudioManager.s_studioSystem.handle = studioHandle;
      AudioManager.s_lowLevelSystem.handle = lowLevelStudioHandle;
    }

    public static void PauseSystemInternal(bool paused)
    {
      if (!AudioManager.s_studioSystem.isValid())
        return;
      if (AudioManager.isReady)
        AudioManager.Pause(paused);
      if (paused)
      {
        int num1 = (int) AudioManager.s_lowLevelSystem.mixerSuspend();
      }
      else
      {
        int num2 = (int) AudioManager.s_lowLevelSystem.mixerResume();
      }
    }

    public static void ReleaseSystemInternal()
    {
      if (AudioManager.s_studioSystem.isValid())
      {
        RESULT result = AudioManager.s_studioSystem.release();
        AudioManager.s_studioSystem.clearHandle();
        if (result != RESULT.OK)
          Ankama.Utilities.Log.Warning(string.Format("Error while releasing system: {0}", (object) result), 661, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      AudioManager.isReady = false;
    }

    private static void CheckInitResult(RESULT result, string cause)
    {
      if (result != RESULT.OK)
      {
        int num = AudioManager.s_studioSystem.isValid() ? (int) AudioManager.s_studioSystem.release() : throw new SystemNotInitializedException(result, cause);
        AudioManager.s_studioSystem.clearHandle();
      }
    }

    private static void SetThreadAffinity()
    {
    }

    [PublicAPI]
    [NotNull]
    public static AudioBankLoadRequest LoadBankAsync([NotNull] string bankName, bool loadSamples = true)
    {
      AudioManager.BankInfo bankInfo;
      if (AudioManager.s_banks.TryGetValue(bankName, out bankInfo))
      {
        ++bankInfo.referenceCount;
        AudioManager.s_banks[bankName] = bankInfo;
        return bankInfo.completedRequest;
      }
      AudioManager.BankLoading bankLoading;
      if (AudioManager.s_banksLoading.TryGetValue(bankName, out bankLoading))
      {
        ++bankLoading.referenceCount;
        if (loadSamples)
          bankLoading.loadRequest.RequestSampleLoadingInternal();
        AudioManager.s_banksLoading[bankName] = bankLoading;
        return bankLoading.loadRequest;
      }
      AssetReference assetReference;
      if (!AudioManager.s_settings.runtimeCache.bankReferenceDictionary.TryGetValue(bankName, out assetReference))
        return new AudioBankLoadRequest(bankName, "Bank name '" + bankName + "' does not exist in the settings.");
      string bundleName;
      if (!AssetBundlesUtility.TryGetAudioBundleName(bankName, out bundleName))
        return new AudioBankLoadRequest(bankName, "Bank name '" + bankName + "' does not follow nomenclature.");
      AudioBankLoadRequest audioBankLoadRequest = new AudioBankLoadRequest(bankName, assetReference, bundleName, loadSamples);
      if (audioBankLoadRequest.isDone)
      {
        if ((int) audioBankLoadRequest.error == 0)
        {
          bankInfo.bank = audioBankLoadRequest.bank;
          bankInfo.referenceCount = 1;
          bankInfo.bundleName = bundleName;
          bankInfo.completedRequest = audioBankLoadRequest;
          AudioManager.s_banks.Add(bankName, bankInfo);
        }
      }
      else
      {
        bankLoading.loadRequest = audioBankLoadRequest;
        bankLoading.referenceCount = 1;
        AudioManager.s_banksLoading.Add(bankName, bankLoading);
      }
      return audioBankLoadRequest;
    }

    [PublicAPI]
    public static void UnloadBank([NotNull] string bankName)
    {
      AudioManager.BankInfo bankInfo;
      if (AudioManager.s_banks.TryGetValue(bankName, out bankInfo))
      {
        --bankInfo.referenceCount;
        if (bankInfo.referenceCount == 0)
        {
          Bank bank = bankInfo.bank;
          RESULT result = bank.unload();
          bank.clearHandle();
          AudioManager.s_banks.Remove(bankName);
          AssetManager.UnloadAssetBundle(bankInfo.bundleName);
          if (result == RESULT.OK)
            return;
          Ankama.Utilities.Log.Warning(string.Format("Error while unloading bank named '{0}': {1}", (object) bankName, (object) result), 807, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        }
        else
          AudioManager.s_banks[bankName] = bankInfo;
      }
      else
      {
        AudioManager.BankLoading bankLoading;
        if (AudioManager.s_banksLoading.TryGetValue(bankName, out bankLoading))
        {
          --bankLoading.referenceCount;
          if (bankLoading.referenceCount == 0)
          {
            bankLoading.loadRequest.Cancel();
            AudioManager.s_banksLoading.Remove(bankName);
          }
          else
            AudioManager.s_banksLoading[bankName] = bankLoading;
        }
        else
          Ankama.Utilities.Log.Warning("Could not unload bank named '" + bankName + "' because it is neither loaded nor loading.", 836, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
    }

    [PublicAPI]
    public static bool TryGetDefaultBankName(AudioReference audioReference, [NotNull] out string bankName)
    {
      if (!AudioManager.isReady)
      {
        Ankama.Utilities.Log.Error("Tried to get a default bank name but the AudioManager isn't ready.", 848, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        bankName = string.Empty;
        return false;
      }
      FMODRuntimeCache runtimeCache = AudioManager.s_settings.runtimeCache;
      int index;
      if (!runtimeCache.eventDefaultBankDictionary.TryGetValue(audioReference.eventGuidString, out index))
      {
        bankName = string.Empty;
        return false;
      }
      bankName = runtimeCache.bankNameList[index];
      return true;
    }

    [PublicAPI]
    public static bool TryGetDefaultBankName(Guid eventGuid, [NotNull] out string bankName)
    {
      if (!AudioManager.isReady)
      {
        Ankama.Utilities.Log.Error("Tried to get a default bank name but the AudioManager isn't ready.", 874, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        bankName = string.Empty;
        return false;
      }
      FMODRuntimeCache runtimeCache = AudioManager.s_settings.runtimeCache;
      int index;
      if (!runtimeCache.eventDefaultBankDictionary.TryGetValue(eventGuid.ToString("N"), out index))
      {
        bankName = string.Empty;
        return false;
      }
      bankName = runtimeCache.bankNameList[index];
      return true;
    }

    [PublicAPI]
    public static bool TryGetDefaultBankName([NotNull] string eventGuid, [NotNull] out string bankName)
    {
      FMODRuntimeCache runtimeCache = AudioManager.s_settings.runtimeCache;
      int index;
      if (!runtimeCache.eventDefaultBankDictionary.TryGetValue(eventGuid, out index))
      {
        bankName = string.Empty;
        return false;
      }
      bankName = runtimeCache.bankNameList[index];
      return true;
    }

    private static RESULT LoadMasterBank([NotNull] TextAsset asset, out Bank bank)
    {
      string name = asset.name;
      AudioManager.BankInfo bankInfo = new AudioManager.BankInfo();
      RESULT result = AudioManager.s_studioSystem.loadBankMemoryPoint(asset.bytes, LOAD_BANK_FLAGS.NORMAL, out bank);
      if (result == RESULT.OK)
      {
        bankInfo.bank = bank;
        bankInfo.referenceCount = 1;
        bankInfo.completedRequest = new AudioBankLoadRequest(name);
        AudioManager.s_banks.Add(name, bankInfo);
      }
      else
        Ankama.Utilities.Log.Error(string.Format("Could not load bank named '{0}': {1}", (object) name, (object) result), 932, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return result;
    }

    [PublicAPI]
    public static bool TryCreateInstance(string path, out FMOD.Studio.EventInstance eventInstance)
    {
      Guid guid;
      if (!AudioManager.TryGetGuidFromPath(path, out guid))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(guid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for path {0} (guid: {1}): {2}", (object) path, (object) guid, (object) instance), 962, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      string path,
      Transform transform,
      out FMOD.Studio.EventInstance eventInstance)
    {
      Guid guid;
      if (!AudioManager.TryGetGuidFromPath(path, out guid))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(guid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for path {0} (guid: {1}): {2}", (object) path, (object) guid, (object) instance), 989, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) transform ? transform.position : ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero);
      int num = (int) eventInstance.set3DAttributes(FMODUtility.To3DAttributes(pos));
      return true;
    }

    [PublicAPI]
    public static bool TryCreateInstance(Guid guid, out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(guid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) guid, (object) instance), 1019, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      Guid guid,
      Transform transform,
      out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(guid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) guid, (object) instance), 1040, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) transform ? transform.position : ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero);
      int num = (int) eventInstance.set3DAttributes(FMODUtility.To3DAttributes(pos));
      return true;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      AudioReference audioReference,
      out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(audioReference.eventGuid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) audioReference.eventGuid, (object) instance), 1070, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      AudioReference audioReference,
      Transform transform,
      out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(audioReference.eventGuid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) audioReference.eventGuid, (object) instance), 1091, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) transform ? transform.position : ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero);
      int num = (int) eventInstance.set3DAttributes(FMODUtility.To3DAttributes(pos));
      return true;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      AudioReferenceWithParameters audioReference,
      out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(audioReference.eventGuid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) audioReference.eventGuid, (object) instance), 1121, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      audioReference.ApplyParameters(eventInstance);
      return true;
    }

    [PublicAPI]
    public static bool TryCreateInstance(
      AudioReferenceWithParameters audioReference,
      Transform transform,
      out FMOD.Studio.EventInstance eventInstance)
    {
      EventDescription eventDescription;
      if (!AudioManager.TryGetEventDescription(audioReference.eventGuid, out eventDescription))
      {
        eventInstance = new FMOD.Studio.EventInstance();
        return false;
      }
      RESULT instance = eventDescription.createInstance(out eventInstance);
      if (instance != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not create event instance for guid {0}: {1}", (object) audioReference.eventGuid, (object) instance), 1144, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      audioReference.ApplyParameters(eventInstance);
      Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) transform ? transform.position : ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero);
      int num = (int) eventInstance.set3DAttributes(FMODUtility.To3DAttributes(pos));
      return true;
    }

    [PublicAPI]
    public static void PlayOneShot(Guid guid, Transform transform = null)
    {
      FMOD.Studio.EventInstance eventInstance;
      if (!AudioManager.isReady || !AudioManager.TryCreateInstance(guid, transform, out eventInstance))
        return;
      int num1 = (int) eventInstance.start();
      int num2 = (int) eventInstance.release();
      eventInstance.clearHandle();
    }

    [PublicAPI]
    public static void PlayOneShot([NotNull] string path, Transform transform = null)
    {
      Guid guid;
      FMOD.Studio.EventInstance eventInstance;
      if (!AudioManager.isReady || !AudioManager.TryGetGuidFromPath(path, out guid) || !AudioManager.TryCreateInstance(guid, transform, out eventInstance))
        return;
      int num1 = (int) eventInstance.start();
      int num2 = (int) eventInstance.release();
      eventInstance.clearHandle();
    }

    [PublicAPI]
    public static void PlayOneShot(AudioReference audioReference, Transform transform = null)
    {
      FMOD.Studio.EventInstance eventInstance;
      if (!AudioManager.isReady || !AudioManager.TryCreateInstance(audioReference.eventGuid, transform, out eventInstance))
        return;
      int num1 = (int) eventInstance.start();
      int num2 = (int) eventInstance.release();
      eventInstance.clearHandle();
    }

    [PublicAPI]
    public static void PlayOneShot(AudioReferenceWithParameters audioReference, Transform transform = null)
    {
      FMOD.Studio.EventInstance eventInstance;
      if (!AudioManager.isReady || !AudioManager.TryCreateInstance(audioReference.eventGuid, transform, out eventInstance))
        return;
      audioReference.ApplyParameters(eventInstance);
      int num1 = (int) eventInstance.start();
      int num2 = (int) eventInstance.release();
      eventInstance.clearHandle();
    }

    private static bool TryGetGuidFromPath([NotNull] string path, out Guid guid)
    {
      if (path.Length == 0)
      {
        Ankama.Utilities.Log.Warning("An empty path cannot be converted into a Guid.", 1253, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        guid = new Guid();
        return false;
      }
      if (path[0] == '{')
      {
        if (Util.ParseID(path, out guid) != RESULT.OK)
        {
          Ankama.Utilities.Log.Warning("Could not parse path '" + path + "' into a valid Guid.", 1263, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
          return false;
        }
      }
      else
      {
        RESULT result = AudioManager.s_studioSystem.lookupID(path, out guid);
        switch (result)
        {
          case RESULT.OK:
            goto label_9;
          case RESULT.ERR_EVENT_NOTFOUND:
            Ankama.Utilities.Log.Warning("Could not find an event for path '" + path + "'.", 1274, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
            break;
          default:
            Ankama.Utilities.Log.Warning(string.Format("Could not find guid for event path '{0}' : {1}.", (object) path, (object) result), 1278, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
            break;
        }
        return false;
      }
label_9:
      return true;
    }

    private static bool TryGetEventDescription(Guid guid, out EventDescription eventDescription)
    {
      if (AudioManager.s_cachedDescriptions.TryGetValue(guid, out eventDescription) && eventDescription.isValid())
        return true;
      RESULT eventById = AudioManager.s_studioSystem.getEventByID(guid, out eventDescription);
      if (eventById != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not find event description for guid {0}: {1}", (object) guid, (object) eventById), 1301, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        return false;
      }
      AudioManager.s_cachedDescriptions[guid] = eventDescription;
      return true;
    }

    [PublicAPI]
    public static AudioWorldMusicRequest LoadWorldMusic(
      AudioEventGroup musicAudioGroup,
      AudioEventGroup ambianceAudioGroup,
      [CanBeNull] AudioContext audioContext = null,
      bool playAutomatically = false)
    {
      musicAudioGroup.Collapse();
      AudioReferenceWithParameters music = musicAudioGroup.isValid ? musicAudioGroup.instance : new AudioReferenceWithParameters();
      ambianceAudioGroup.Collapse();
      AudioReferenceWithParameters ambiance = ambianceAudioGroup.isValid ? ambianceAudioGroup.instance : new AudioReferenceWithParameters();
      AudioContext context = audioContext;
      int num = playAutomatically ? 1 : 0;
      AudioWorldMusicRequest routine = new AudioWorldMusicRequest(music, ambiance, context, num != 0);
      AudioManager.StartCoroutine((IEnumerator) routine);
      return routine;
    }

    [PublicAPI]
    public static void StartWorldMusic([NotNull] AudioWorldMusicRequest request) => request.Start();

    [PublicAPI]
    public static void StopWorldMusic([NotNull] AudioWorldMusicRequest request)
    {
      request.Stop();
      AudioManager.StartCoroutine((IEnumerator) request);
    }

    [PublicAPI]
    public static IEnumerator StartUIMusic(AudioReferenceWithParameters audioReference)
    {
      Guid eventGuid = audioReference.eventGuid;
      if (!(eventGuid == Guid.Empty))
      {
        int count1 = AudioManager.s_uiMusicStack.Count;
        if (count1 > 0)
        {
          AudioManager.MusicInstance uiMusic = AudioManager.s_uiMusicStack[count1 - 1];
          if (uiMusic.guid == eventGuid)
          {
            ++uiMusic.referenceCounter;
            AudioManager.s_uiMusicStack[count1 - 1] = uiMusic;
            yield break;
          }
        }
        while (!AudioManager.isReady)
        {
          if ((int) AudioManager.error != 0)
            yield break;
          else
            yield return (object) null;
        }
        string bankName;
        if (!AudioManager.TryGetDefaultBankName((AudioReference) audioReference, out bankName))
        {
          Ankama.Utilities.Log.Warning(string.Format("Could not start requested UI music with guid {0} because no default bank could be found.", (object) eventGuid), 1376, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
        }
        else
        {
          AudioManager.MusicInstance musicInstance = new AudioManager.MusicInstance(eventGuid);
          AudioManager.s_uiMusicStack.Add(musicInstance);
          AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
          while (!bankLoadRequest.isDone)
            yield return (object) null;
          if ((int) bankLoadRequest.error != 0)
          {
            AudioManager.s_uiMusicStack.Remove(musicInstance);
          }
          else
          {
            int count2 = AudioManager.s_uiMusicStack.Count;
            for (int index = 0; index < count2; ++index)
            {
              if (AudioManager.s_uiMusicStack[index] == musicInstance)
              {
                FMOD.Studio.EventInstance eventInstance1;
                if (AudioManager.TryCreateInstance(eventGuid, out eventInstance1))
                {
                  audioReference.ApplyParameters(eventInstance1);
                  if (index == count2 - 1)
                  {
                    Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero;
                    int num1 = (int) eventInstance1.set3DAttributes(FMODUtility.To3DAttributes(pos));
                    int num2 = (int) eventInstance1.setParameterValue("Music_Menu", 0.0f);
                    int num3 = (int) eventInstance1.start();
                    if (index > 0)
                    {
                      FMOD.Studio.EventInstance eventInstance2 = AudioManager.s_uiMusicStack[index - 1].eventInstance;
                      if (eventInstance2.isValid())
                      {
                        PLAYBACK_STATE state;
                        int playbackState = (int) eventInstance2.getPlaybackState(out state);
                        int position;
                        int timelinePosition = (int) eventInstance2.getTimelinePosition(out position);
                        Ankama.Utilities.Log.Info(state.ToString() + ", " + (object) position, 1422, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
                        if (eventInstance2.setParameterValue("Music_Menu", 1f) != RESULT.OK)
                        {
                          int num4 = (int) eventInstance1.stop(STOP_MODE.ALLOWFADEOUT);
                        }
                      }
                    }
                  }
                  musicInstance.eventInstance = eventInstance1;
                  AudioManager.s_uiMusicStack[index] = musicInstance;
                  yield break;
                }
                else
                {
                  AudioManager.s_uiMusicStack.RemoveAt(index);
                  break;
                }
              }
            }
            AudioManager.UnloadBank(bankName);
          }
        }
      }
    }

    [PublicAPI]
    public static IEnumerator StopUIMusic(AudioReferenceWithParameters audioReference)
    {
      Guid eventGuid = audioReference.eventGuid;
      if (!(eventGuid == Guid.Empty) && AudioManager.isReady)
      {
        int count = AudioManager.s_uiMusicStack.Count;
        for (int index = 0; index < count; ++index)
        {
          AudioManager.MusicInstance musicInstance = AudioManager.s_uiMusicStack[index];
          if (musicInstance.guid == eventGuid)
          {
            --musicInstance.referenceCounter;
            if (musicInstance.referenceCounter > 0)
            {
              AudioManager.s_uiMusicStack[index] = musicInstance;
              break;
            }
            AudioManager.s_uiMusicStack.RemoveAt(index);
            FMOD.Studio.EventInstance eventInstance = musicInstance.eventInstance;
            if (index != count - 1)
              break;
            if (index > 0)
            {
              FMOD.Studio.EventInstance eventInstance1 = AudioManager.s_uiMusicStack[index - 1].eventInstance;
              if (eventInstance1.isValid())
              {
                Vector3 pos = (UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition ? AudioManager.s_listenerPosition.transform.position : Vector3.zero;
                int num1 = (int) eventInstance1.set3DAttributes(FMODUtility.To3DAttributes(pos));
                int num2 = (int) eventInstance1.setParameterValue("Music_Menu", 0.0f);
                int num3 = (int) eventInstance1.start();
              }
            }
            if (!eventInstance.isValid())
              break;
            if (eventInstance.setParameterValue("Music_Menu", 1f) != RESULT.OK)
            {
              int num4 = (int) eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
            PLAYBACK_STATE state;
            do
            {
              yield return (object) null;
              int playbackState = (int) eventInstance.getPlaybackState(out state);
            }
            while (state != PLAYBACK_STATE.STOPPED);
            int num5 = (int) eventInstance.release();
            eventInstance.clearHandle();
            string bankName;
            if (!AudioManager.TryGetDefaultBankName(musicInstance.guid, out bankName))
              break;
            AudioManager.UnloadBank(bankName);
            break;
          }
          musicInstance = new AudioManager.MusicInstance();
        }
      }
    }

    [PublicAPI]
    public static void AddAudioContext([NotNull] AudioContext context) => AudioManager.s_audioContexts.Add(context);

    [PublicAPI]
    public static void RemoveAudioContext([NotNull] AudioContext context) => AudioManager.s_audioContexts.Remove(context);

    [PublicAPI]
    public static void Pause(bool paused)
    {
      if (!AudioManager.isReady)
        return;
      Bus bus1;
      RESULT bus2 = AudioManager.s_studioSystem.getBus("bus:/", out bus1);
      if (bus2 != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not get bus: {0}", (object) bus2), 1574, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      else
      {
        int num = (int) bus1.setPaused(paused);
      }
    }

    [PublicAPI]
    public static void Mute(bool muted)
    {
      if (!AudioManager.isReady)
        return;
      Bus bus1;
      RESULT bus2 = AudioManager.s_studioSystem.getBus("bus:/", out bus1);
      if (bus2 != RESULT.OK)
      {
        Ankama.Utilities.Log.Warning(string.Format("Could not get bus: {0}", (object) bus2), 1594, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      }
      else
      {
        int num = (int) bus1.setMute(muted);
      }
    }

    [PublicAPI]
    public static bool TryGetVolume(AudioBusIdentifier busIdentifier, out float volume)
    {
      if (!AudioManager.isReady)
      {
        volume = 0.0f;
        return false;
      }
      Bus bus;
      if (!AudioManager.TryGetBus(busIdentifier, out bus))
      {
        volume = 0.0f;
        return false;
      }
      RESULT volume1 = bus.getVolume(out volume, out float _);
      if (volume1 == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not get volume for bus {0}: {1}", (object) busIdentifier, (object) volume1), 1619, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    [PublicAPI]
    public static bool SetVolume(AudioBusIdentifier busIdentifier, float volume)
    {
      Bus bus;
      if (!AudioManager.isReady || !AudioManager.TryGetBus(busIdentifier, out bus))
        return false;
      RESULT result = bus.setVolume(volume);
      if (result == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not set volume for bus {0}: {1}", (object) busIdentifier, (object) result), 1642, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    private static bool TryGetBus(AudioBusIdentifier busIdentifier, out Bus bus)
    {
      string path;
      switch (busIdentifier)
      {
        case AudioBusIdentifier.Master:
          path = "bus:/";
          break;
        case AudioBusIdentifier.Music:
          path = "bus:/MUSIC";
          break;
        case AudioBusIdentifier.SFX:
          path = "bus:/SFX";
          break;
        case AudioBusIdentifier.UI:
          path = "bus:/UI";
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (busIdentifier), (object) busIdentifier, (string) null);
      }
      RESULT bus1 = AudioManager.s_studioSystem.getBus(path, out bus);
      if (bus1 == RESULT.OK)
        return true;
      Ankama.Utilities.Log.Warning(string.Format("Could not get bus: {0}", (object) bus1), 1675, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      return false;
    }

    [PublicAPI]
    public static void RegisterListenerPosition([NotNull] AudioListenerPosition instance)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) AudioManager.s_listenerPosition && (UnityEngine.Object) instance != (UnityEngine.Object) AudioManager.s_listenerPosition)
        Ankama.Utilities.Log.Warning("Registering a listener while another listener is already registered, previous listener will be ignored.", 1693, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioManager.cs");
      AudioManager.s_listenerPosition = instance;
      if (!AudioManager.isReady)
        return;
      int num = (int) AudioManager.s_studioSystem.setListenerAttributes(0, FMODUtility.To3DAttributes(AudioManager.s_listenerPosition.transform));
    }

    [PublicAPI]
    public static void UpdateListenerPosition([NotNull] AudioListenerPosition instance)
    {
      if (!((UnityEngine.Object) instance == (UnityEngine.Object) AudioManager.s_listenerPosition))
        return;
      int num = (int) AudioManager.s_studioSystem.setListenerAttributes(0, FMODUtility.To3DAttributes(AudioManager.s_listenerPosition.transform));
    }

    [PublicAPI]
    public static void UnRegisterListenerPosition([NotNull] AudioListenerPosition instance)
    {
      if (!((UnityEngine.Object) instance == (UnityEngine.Object) AudioManager.s_listenerPosition))
        return;
      int num = (int) AudioManager.s_studioSystem.setListenerAttributes(0, FMODUtility.To3DAttributes(Vector3.zero));
      AudioManager.s_listenerPosition = (AudioListenerPosition) null;
    }

    [Conditional("UNITY_EDITOR")]
    private static void LogBankLoading(string bankName)
    {
    }

    [Conditional("UNITY_EDITOR")]
    private static void LogBankLoaded(string bankName)
    {
    }

    [Conditional("UNITY_EDITOR")]
    private static void LogBankUnloaded(string bankName)
    {
    }

    [Conditional("UNITY_EDITOR")]
    private static void LogBankCancelled(string bankName)
    {
    }

    private struct BankInfo
    {
      public Bank bank;
      public int referenceCount;
      public string bundleName;
      public AudioBankLoadRequest completedRequest;
    }

    private struct BankLoading
    {
      public AudioBankLoadRequest loadRequest;
      public int referenceCount;
    }

    private struct MusicInstance : IEquatable<AudioManager.MusicInstance>
    {
      private static int s_nextIdentifier;
      private readonly int m_identifier;
      public readonly Guid guid;
      public FMOD.Studio.EventInstance eventInstance;
      public int referenceCounter;

      public MusicInstance(Guid eventGuid)
      {
        this.m_identifier = ++AudioManager.MusicInstance.s_nextIdentifier;
        this.guid = eventGuid;
        this.eventInstance = new FMOD.Studio.EventInstance();
        this.referenceCounter = 1;
      }

      public bool Equals(AudioManager.MusicInstance other) => this.m_identifier == other.m_identifier;

      public override bool Equals(object obj) => obj != null && obj is AudioManager.MusicInstance musicInstance && this.m_identifier == musicInstance.m_identifier;

      public override int GetHashCode() => this.m_identifier;

      public static bool operator ==(AudioManager.MusicInstance lhs, AudioManager.MusicInstance rhs) => lhs.m_identifier == rhs.m_identifier;

      public static bool operator !=(AudioManager.MusicInstance lhs, AudioManager.MusicInstance rhs) => lhs.m_identifier != rhs.m_identifier;
    }

    private class GuidComparer : IEqualityComparer<Guid>
    {
      bool IEqualityComparer<Guid>.Equals(Guid x, Guid y) => x.Equals(y);

      int IEqualityComparer<Guid>.GetHashCode(Guid obj) => obj.GetHashCode();
    }

    [Flags]
    private enum InitializationState
    {
      None = 0,
      Assembly = 1,
      Variant = 2,
      GameDataBundle = 4,
      MasterBundle = 8,
    }
  }
}
