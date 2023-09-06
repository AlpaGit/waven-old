// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUILoader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using FMODUnity;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Audio.UI
{
  public abstract class AudioEventUILoader : MonoBehaviour
  {
    protected AudioEventUILoader.InitializationState m_initializationState;
    private AudioBankLoadRequest[] m_bankLoadRequests;

    protected IEnumerator Load(AudioReferenceWithParameters audioReference)
    {
      string bankName;
      if (!AudioManager.TryGetDefaultBankName((AudioReference) audioReference, out bankName))
      {
        this.m_initializationState = AudioEventUILoader.InitializationState.Error;
      }
      else
      {
        AudioBankLoadRequest bankLoadRequest = AudioManager.LoadBankAsync(bankName);
        this.m_bankLoadRequests = new AudioBankLoadRequest[1]
        {
          bankLoadRequest
        };
        this.m_initializationState = AudioEventUILoader.InitializationState.Loading;
        while (!bankLoadRequest.isDone)
          yield return (object) null;
        this.m_initializationState = (int) bankLoadRequest.error == 0 ? AudioEventUILoader.InitializationState.Loaded : AudioEventUILoader.InitializationState.Error;
      }
    }

    protected IEnumerator Load(
      params AudioReferenceWithParameters[] audioReferences)
    {
      int length = audioReferences.Length;
      int bankCount = 0;
      if (length == 0)
      {
        this.m_initializationState = AudioEventUILoader.InitializationState.Loaded;
      }
      else
      {
        string[] strArray = new string[length];
label_10:
        for (int index1 = 0; index1 < length; ++index1)
        {
          AudioReferenceWithParameters audioReference = audioReferences[index1];
          string bankName;
          if (audioReference.isValid && AudioManager.TryGetDefaultBankName((AudioReference) audioReference, out bankName))
          {
            for (int index2 = 0; index2 < bankCount; ++index2)
            {
              if (bankName.Equals(strArray[index2]))
                goto label_10;
            }
            strArray[bankCount] = bankName;
            ++bankCount;
          }
        }
        if (bankCount == 0)
        {
          this.m_initializationState = AudioEventUILoader.InitializationState.Error;
        }
        else
        {
          AudioBankLoadRequest[] bankLoadRequests = new AudioBankLoadRequest[bankCount];
          for (int index = 0; index < bankCount; ++index)
            bankLoadRequests[index] = AudioManager.LoadBankAsync(strArray[index]);
          this.m_bankLoadRequests = bankLoadRequests;
          this.m_initializationState = AudioEventUILoader.InitializationState.Loading;
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution((IEnumerator[]) bankLoadRequests);
          for (int index = 0; index < bankCount; ++index)
          {
            if ((int) bankLoadRequests[index].error == 0)
            {
              this.m_initializationState = AudioEventUILoader.InitializationState.Loaded;
              yield break;
            }
          }
          this.m_initializationState = AudioEventUILoader.InitializationState.Error;
        }
      }
    }

    protected void Unload()
    {
      if (!AudioManager.isReady)
        return;
      AudioBankLoadRequest[] bankLoadRequests = this.m_bankLoadRequests;
      if (bankLoadRequests == null)
        return;
      int length = bankLoadRequests.Length;
      for (int index = 0; index < length; ++index)
      {
        AudioBankLoadRequest bankLoadRequest = this.m_bankLoadRequests[index];
        if (!bankLoadRequest.isDone)
          bankLoadRequest.Cancel();
        else if ((int) bankLoadRequest.error == 0)
          AudioManager.UnloadBank(bankLoadRequest.bankName);
      }
      this.m_bankLoadRequests = (AudioBankLoadRequest[]) null;
    }

    protected virtual void OnDestroy() => this.Unload();

    protected enum InitializationState
    {
      None,
      Loading,
      Loaded,
      Error,
    }
  }
}
