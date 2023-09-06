// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.TextWebRequest
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine.Networking;

namespace Ankama.Cube.Network
{
  public static class TextWebRequest
  {
    public static IEnumerator ReadFile([NotNull] string url, [NotNull] TextWebRequest.AsyncResult result)
    {
      if (url.StartsWith("https://") || url.StartsWith("http://"))
        url = TextWebRequest.UrlNoCache(url);
      DownloadHandlerBuffer downloadHandler = new DownloadHandlerBuffer();
      UnityWebRequest request = new UnityWebRequest(url)
      {
        disposeDownloadHandlerOnDispose = true,
        downloadHandler = (DownloadHandler) downloadHandler
      };
      using (request)
      {
        request.SendWebRequest();
        while (!request.isDone)
          yield return (object) null;
        if (request.isHttpError || request.isNetworkError)
          result.exception = new TextWebRequest.Exception(request.responseCode, request.error);
        else
          result.value = downloadHandler.text;
      }
    }

    private static string UrlNoCache([NotNull] string url)
    {
      long fileTimeUtc = DateTime.Now.ToFileTimeUtc();
      return string.Format("{0}?t={1}", (object) url, (object) fileTimeUtc);
    }

    public class Exception : System.Exception
    {
      public readonly long responseCode;

      public Exception(long responseCode, string message)
        : base(message)
      {
        this.responseCode = responseCode;
      }
    }

    public class AsyncResult<T, E> where E : TextWebRequest.Exception
    {
      public T value;
      public E exception;

      public bool hasException => (object) this.exception != null;
    }

    public class AsyncResult : TextWebRequest.AsyncResult<string, TextWebRequest.Exception>
    {
    }
  }
}
