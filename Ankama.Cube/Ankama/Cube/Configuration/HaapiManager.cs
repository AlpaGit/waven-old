// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.HaapiManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using Com.Ankama.Haapi.Swagger.Api;
using IO.Swagger.Client;
using System;
using System.Net;
using System.Net.Security;
using UnityEngine;

namespace Ankama.Cube.Configuration
{
  public class HaapiManager : MonoBehaviour
  {
    public static readonly AccountApi accountApi = new AccountApi();
    private static HaapiManager s_instance;
    private static bool s_initialized;
    private static readonly RemoteCertificateValidationCallback DefaultServerCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
    private static readonly RemoteCertificateValidationCallback AlwaysValidateCertificateCallback = (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true);

    public static void Initialize()
    {
      if ((UnityEngine.Object) HaapiManager.s_instance == (UnityEngine.Object) null)
      {
        Log.Info("Adding HaapiManager to scene.", 26, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\Haapi\\HaapiManager.cs");
        HaapiManager.s_instance = new GameObject(nameof (HaapiManager)).AddComponent<HaapiManager>();
        UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) HaapiManager.s_instance);
      }
      string basePath = ApplicationConfig.haapiServerUrl;
      if (string.IsNullOrWhiteSpace(basePath))
        basePath = "https://haapi.ankama.lan/json/Ankama/v2";
      IO.Swagger.Client.Configuration.DefaultApiClient = new ApiClient(basePath);
      ServicePointManager.ServerCertificateValidationCallback = HaapiManager.NeedCertificateValidation() ? HaapiManager.DefaultServerCertificateValidationCallback : HaapiManager.AlwaysValidateCertificateCallback;
      HaapiManager.accountApi.ApiClient = IO.Swagger.Client.Configuration.DefaultApiClient;
      HaapiManager.s_initialized = true;
    }

    private static bool NeedCertificateValidation()
    {
      string basePath = IO.Swagger.Client.Configuration.DefaultApiClient.BasePath;
      return (basePath.StartsWithFast("https://haapi.ankama.tst") ? 1 : (basePath.StartsWithFast("https://haapi.ankama.lan") ? 1 : 0)) == 0;
    }

    public static void ExecuteRequest<T>(
      Func<T> func,
      Action<T> onResult,
      Action<Exception> onException)
    {
      HaapiManager.CheckInit();
      GameObject gameObject = new GameObject();
      gameObject.transform.SetParent(HaapiManager.s_instance.transform);
      gameObject.AddComponent<HaapiRequestBehaviour>().ExecuteRequest<T>(func, onResult, onException);
    }

    private static void CheckInit()
    {
      if (!HaapiManager.s_initialized)
      {
        Log.Error("HaapiManager.Initialize should have been called.", 71, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Configuration\\Haapi\\HaapiManager.cs");
        throw new Exception("HaapiManager.Initialize should have been called.");
      }
    }
  }
}
