// Decompiled with JetBrains decompiler
// Type: com.ankama.zaap.ErrorCode
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace com.ankama.zaap
{
  public enum ErrorCode
  {
    UNKNOWN = 1,
    UNAUTHORIZED = 2,
    INVALID_GAME_SESSION = 3,
    CONNECTION_FAILED = 1001, // 0x000003E9
    INVALID_CREDENTIALS = 1002, // 0x000003EA
    AUTH_NOT_LOGGED_IN = 2001, // 0x000007D1
    AUTH_SERVER_UNAVAILABLE = 2002, // 0x000007D2
    AUTH_TOKEN_CREATION_FAILED = 2003, // 0x000007D3
    UPDATER_CODE_RANGE = 3001, // 0x00000BB9
    SETTINGS_KEY_NOT_FOUND = 4001, // 0x00000FA1
    SETTINGS_INVALID_VALUE = 4002, // 0x00000FA2
  }
}
