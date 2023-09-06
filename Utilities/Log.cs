// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.Log
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class Log
  {
    [PublicAPI]
    public static ILogRender logRender;

    static Log() => Log.InitializeForRuntime();

    [PublicAPI]
    public static bool logEnabled
    {
      get => Debug.unityLogger.logEnabled;
      set => Debug.unityLogger.logEnabled = value;
    }

    [PublicAPI]
    public static LogType filterLogType
    {
      get => Debug.unityLogger.filterLogType;
      set => Debug.unityLogger.filterLogType = value;
    }

    [PublicAPI]
    public static void InitializeForEditor()
    {
      Log.logRender = (ILogRender) new EditorLogRender();
      Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.ScriptOnly);
    }

    [PublicAPI]
    public static void InitializeForRuntime()
    {
      Log.logRender = (ILogRender) new DefaultLogRender();
      Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsLogTypeAllowed(LogType logType)
    {
      if (Log.logEnabled)
      {
        if (logType == LogType.Exception)
          return true;
        if (Log.filterLogType != LogType.Exception)
          return logType <= Log.filterLogType;
      }
      return false;
    }

    [PublicAPI]
    public static void Info(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(string tag, string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(string tag, object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, context, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, context, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(string tag, string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, context, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Info(string tag, object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Log))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Log, context, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Log, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string tag, string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string tag, object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, context, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, context, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string tag, string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, context, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Warning(string tag, object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Warning))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Warning, context, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Warning, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string tag, string message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string tag, object message, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, (UnityEngine.Object) null, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, context, "{0}", (object) Log.logRender.Render((string) null, message, file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, context, "{0}", (object) Log.logRender.Render((string) null, message?.ToString(), file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string tag, string message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, context, "{0}", (object) Log.logRender.Render(tag, message, file, line, LogType.Error, DateTime.Now));
    }

    [PublicAPI]
    public static void Error(string tag, object message, UnityEngine.Object context, [CallerLineNumber] int line = 0, [CallerFilePath] string file = "")
    {
      if (!Log.IsLogTypeAllowed(LogType.Error))
        return;
      Debug.unityLogger.logHandler.LogFormat(LogType.Error, context, "{0}", (object) Log.logRender.Render(tag, message?.ToString(), file, line, LogType.Error, DateTime.Now));
    }
  }
}
