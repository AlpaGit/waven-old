// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.BaseOpenCloseUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Ankama.Cube.UI
{
  public class BaseOpenCloseUI : AbstractUI
  {
    [Header("OpenCloseUI")]
    [SerializeField]
    private PlayableDirector m_openDirector;
    [SerializeField]
    private PlayableDirector m_closeDirector;
    [Header("Sounds")]
    [SerializeField]
    private UnityEvent m_openSound;
    protected Coroutine m_playCoroutine;
    protected PlayableDirector m_currentPlayingDirector;

    public bool isPlaying => (UnityEngine.Object) this.m_currentPlayingDirector != (UnityEngine.Object) null;

    public void Open(Action completeCallback = null)
    {
      this.CancelCurrentAnim();
      this.m_playCoroutine = this.StartCoroutine(this.PlayDirectorCoroutine(this.m_openDirector, completeCallback));
      this.m_openSound.Invoke();
    }

    public void Close(Action completeCallback = null)
    {
      this.CancelCurrentAnim();
      this.m_playCoroutine = this.StartCoroutine(this.PlayDirectorCoroutine(this.m_closeDirector, completeCallback));
    }

    private void CancelCurrentAnim()
    {
      if (this.m_playCoroutine != null)
      {
        this.StopCoroutine(this.m_playCoroutine);
        this.m_playCoroutine = (Coroutine) null;
      }
      if (!((UnityEngine.Object) this.m_currentPlayingDirector != (UnityEngine.Object) null))
        return;
      this.m_currentPlayingDirector.Stop();
      this.m_currentPlayingDirector = (PlayableDirector) null;
    }

    public virtual IEnumerator OpenCoroutine()
    {
      this.CancelCurrentAnim();
      this.m_openSound.Invoke();
      yield return (object) this.PlayDirectorCoroutine(this.m_openDirector);
    }

    public IEnumerator CloseCoroutine()
    {
      this.CancelCurrentAnim();
      yield return (object) this.PlayDirectorCoroutine(this.m_closeDirector);
    }

    private IEnumerator PlayDirectorCoroutine(PlayableDirector director, Action completeCallback = null)
    {
      if (!((UnityEngine.Object) null == (UnityEngine.Object) director))
      {
        this.m_currentPlayingDirector = director;
        director.time = 0.0;
        director.Play();
        for (PlayableGraph playableGraph = director.playableGraph; playableGraph.IsValid() && !playableGraph.IsDone(); playableGraph = director.playableGraph)
        {
          yield return (object) null;
          if ((UnityEngine.Object) null == (UnityEngine.Object) director)
            yield break;
        }
        Action action = completeCallback;
        if (action != null)
          action();
        this.m_playCoroutine = (Coroutine) null;
        this.m_currentPlayingDirector = (PlayableDirector) null;
      }
    }

    public static IEnumerator PlayDirector(PlayableDirector director)
    {
      director.time = 0.0;
      director.Play();
      while (true)
      {
        PlayableGraph playableGraph = director.playableGraph;
        if (playableGraph.IsValid())
        {
          playableGraph = director.playableGraph;
          if (!playableGraph.IsDone())
            yield return (object) null;
          else
            goto label_5;
        }
        else
          break;
      }
      yield break;
label_5:;
    }
  }
}
