// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioBankLoadRequest
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using FMOD;
using FMOD.Studio;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Audio
{
  public class AudioBankLoadRequest : CustomYieldInstruction
  {
    public readonly string bankName;
    public readonly string bundleName;
    private AssetReference m_assetReference;
    private AssetBundleLoadRequest m_bundleLoadRequest;
    private AssetLoadRequest<TextAsset> m_assetLoadRequest;
    private bool m_loadingSamples;

    public bool loadSamples { get; private set; }

    public Bank bank { get; private set; }

    public bool isDone { get; private set; }

    public AssetManagerError error { [NotNull] get; private set; } = (AssetManagerError) 0;

    public override bool keepWaiting => !this.isDone;

    public AudioBankLoadRequest(string bankName)
    {
      this.bankName = bankName;
      this.bundleName = string.Empty;
      this.isDone = true;
    }

    public AudioBankLoadRequest(string bankName, string error)
    {
      this.bankName = bankName;
      this.error = new AssetManagerError(10, error);
      this.bundleName = string.Empty;
      this.isDone = true;
    }

    public AudioBankLoadRequest(
      string bankName,
      AssetReference assetReference,
      string bundleName,
      bool loadSamples)
    {
      this.bankName = bankName;
      this.bundleName = bundleName;
      this.loadSamples = loadSamples;
      this.m_assetReference = assetReference;
      this.m_bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      this.isDone = this.Update();
    }

    public void RequestSampleLoadingInternal() => this.loadSamples = true;

    public bool UpdateInternal()
    {
      this.isDone = this.Update();
      return this.isDone;
    }

    private bool Update()
    {
      if (this.m_bundleLoadRequest != null)
      {
        if (!this.m_bundleLoadRequest.isDone)
          return false;
        if ((int) this.m_bundleLoadRequest.error != 0)
        {
          this.error = this.m_bundleLoadRequest.error;
          return true;
        }
        this.m_bundleLoadRequest = (AssetBundleLoadRequest) null;
        this.m_assetLoadRequest = this.m_assetReference.LoadFromAssetBundleAsync<TextAsset>(this.bundleName);
      }
      if (this.m_assetLoadRequest != null)
      {
        if (!this.m_assetLoadRequest.isDone)
          return false;
        if ((int) this.m_assetLoadRequest.error != 0)
        {
          this.error = this.m_assetLoadRequest.error;
          return true;
        }
        TextAsset asset = this.m_assetLoadRequest.asset;
        this.m_assetLoadRequest = (AssetLoadRequest<TextAsset>) null;
        Bank bank;
        int num = (int) AudioManager.studioSystem.loadBankMemoryPoint(asset.bytes, LOAD_BANK_FLAGS.NONBLOCKING, out bank);
        this.bank = bank;
      }
      LOADING_STATE state1;
      RESULT loadingState = this.bank.getLoadingState(out state1);
      if (loadingState != RESULT.OK)
      {
        Log.Error(string.Format("Failed to retrieve loading state for bank named '{0}' loaded from bundle named '{1}': {2}.", (object) this.bankName, (object) this.bundleName, (object) loadingState), 121, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
        this.error = (AssetManagerError) 30;
        Bank bank = this.bank;
        int num = (int) bank.unload();
        bank = this.bank;
        bank.clearHandle();
        return true;
      }
      switch (state1)
      {
        case LOADING_STATE.UNLOADING:
        case LOADING_STATE.UNLOADED:
          Log.Error("Bank named '" + this.bankName + "' is being either unloaded or being unloaded instead of being loaded.", 132, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
          this.error = (AssetManagerError) 30;
          return true;
        case LOADING_STATE.LOADING:
          return false;
        case LOADING_STATE.LOADED:
          if (this.loadSamples)
          {
            Bank bank;
            if (!this.m_loadingSamples)
            {
              bank = this.bank;
              if (bank.loadSampleData() != RESULT.OK)
              {
                Log.Warning("Could not load samples for bank named '" + this.bankName + "'.", 147, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
                return true;
              }
              this.m_loadingSamples = true;
            }
            bank = this.bank;
            LOADING_STATE state2;
            if (bank.getSampleLoadingState(out state2) == RESULT.OK)
            {
              switch (state2)
              {
                case LOADING_STATE.LOADING:
                  return false;
                case LOADING_STATE.ERROR:
                  Log.Error("Failed to load samples for bank named '" + this.bankName + "'.", 165, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
                  bank = this.bank;
                  int num = (int) bank.unloadSampleData();
                  break;
              }
            }
            else
              Log.Warning("Could not get sample loading state for bank named '" + this.bankName + "'.", 172, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
            this.m_loadingSamples = false;
          }
          return true;
        case LOADING_STATE.ERROR:
          Log.Error("Failed to load bank named '" + this.bankName + "'.", 180, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Audio\\AudioBankLoadRequest.cs");
          this.error = (AssetManagerError) 30;
          int num1 = (int) this.bank.unload();
          this.bank.clearHandle();
          return true;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void Cancel()
    {
      if (this.m_bundleLoadRequest != null)
      {
        AssetManager.UnloadAssetBundle(this.bundleName);
        this.m_bundleLoadRequest = (AssetBundleLoadRequest) null;
      }
      else
      {
        LOADING_STATE state;
        if (this.m_assetLoadRequest == null && this.bank.getLoadingState(out state) == RESULT.OK)
        {
          Bank bank;
          if (this.m_loadingSamples)
          {
            bank = this.bank;
            int num = (int) bank.unloadSampleData();
          }
          if (state == LOADING_STATE.LOADING || state == LOADING_STATE.LOADED)
          {
            bank = this.bank;
            int num = (int) bank.unload();
            bank = this.bank;
            bank.clearHandle();
          }
        }
      }
      this.error = (AssetManagerError) 50;
      this.isDone = true;
    }
  }
}
