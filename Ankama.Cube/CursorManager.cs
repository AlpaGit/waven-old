// Decompiled with JetBrains decompiler
// Type: CursorManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

public class CursorManager : MonoBehaviour
{
  [SerializeField]
  protected Texture2D m_cursorTexture;
  [SerializeField]
  protected Vector2 m_cursorHotspot;
  protected Texture2D m_currentTexture;
  protected Vector2 m_currentHotspot;

  public static CursorManager instance { get; private set; }

  public void SetCursor(Texture2D texture, Vector2 hotspot) => Cursor.SetCursor(texture, hotspot, CursorMode.Auto);

  public void ResetCursor() => Cursor.SetCursor(this.m_cursorTexture, this.m_cursorHotspot, CursorMode.Auto);

  public static bool hasInstance => (Object) null != (Object) CursorManager.instance;

  private void Awake()
  {
    CursorManager.instance = this;
    if (SystemInfo.deviceType != DeviceType.Handheld)
      return;
    Cursor.visible = false;
  }

  private void OnApplicationFocus(bool focus)
  {
    if (focus)
      Cursor.SetCursor(this.m_cursorTexture, this.m_cursorHotspot, CursorMode.Auto);
    else
      Cursor.SetCursor((Texture2D) null, Vector2.zero, CursorMode.Auto);
  }
}
