// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.HavreMap
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.UI;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class HavreMap : MonoBehaviour
  {
    [Header("Map Information")]
    [SerializeField]
    private ZaapObject m_pvpZaap;
    [SerializeField]
    private ZaapGodObject m_godZaap;
    [SerializeField]
    private MapPathfindingActor m_mapCharacterObject;
    [SerializeField]
    private MapData m_mapData;
    [SerializeField]
    private Color m_ambientColor;
    [Header("Camera")]
    [SerializeField]
    private MapCameraHandler m_cameraHandler;
    [Header("Audio")]
    [SerializeField]
    private AudioEventGroup m_musicGroup;
    [SerializeField]
    private AudioEventGroup m_ambianceGroup;
    public Action onPvPTrigger;
    public Action onGodTrigger;
    private readonly MapAudioContext m_audioContext = new MapAudioContext();
    private AudioWorldMusicRequest m_worldMusicRequest;
    private List<Vector3> m_path = new List<Vector3>();
    private MapQuadTreePathfinding m_quadTreePathFinding = new MapQuadTreePathfinding();
    private bool m_interactable = true;

    public MapPathfindingActor character => this.m_mapCharacterObject;

    public ZaapGodObject godZaap => this.m_godZaap;

    private void Awake()
    {
      this.m_pvpZaap.onClick += new Action<ZaapObject>(this.OnZaapClick);
      this.m_pvpZaap.onPortalBeginOpen += new Action<ZaapObject>(this.OnZaapBeginOpen);
      this.m_pvpZaap.onPortalEndOpen += new Action<ZaapObject>(this.OnZaapEndOpen);
      ZaapGodObject godZaap1 = this.m_godZaap;
      godZaap1.onClick = godZaap1.onClick + new Action<ZaapObject>(this.OnZaapClick);
      ZaapGodObject godZaap2 = this.m_godZaap;
      godZaap2.onPortalBeginOpen = godZaap2.onPortalBeginOpen + new Action<ZaapObject>(this.OnZaapBeginOpen);
      ZaapGodObject godZaap3 = this.m_godZaap;
      godZaap3.onPortalEndOpen = godZaap3.onPortalEndOpen + new Action<ZaapObject>(this.OnZaapEndOpen);
    }

    public IEnumerator Initialize()
    {
      RenderSettings.ambientLight = this.m_ambientColor;
      this.m_cameraHandler.Initialize(this.m_mapData, this.m_mapCharacterObject.transform);
      if (AudioManager.isReady)
      {
        this.m_audioContext.Initialize();
        this.m_worldMusicRequest = AudioManager.LoadWorldMusic(this.m_musicGroup, this.m_ambianceGroup, (AudioContext) this.m_audioContext);
        while (this.m_worldMusicRequest.state == AudioWorldMusicRequest.State.Loading)
          yield return (object) null;
      }
    }

    public void InitEnterAnimFirstFrame() => this.m_cameraHandler.InitEnterAnimFirstFrame();

    public IEnumerator PlayEnerAnim()
    {
      yield return (object) this.m_cameraHandler.PlayEnterAnim();
    }

    public void Begin()
    {
      if (this.m_worldMusicRequest == null)
        return;
      AudioManager.StartWorldMusic(this.m_worldMusicRequest);
    }

    public void Release()
    {
      if (AudioManager.isReady)
      {
        this.m_audioContext.Release();
        if (this.m_worldMusicRequest != null)
          AudioManager.StopWorldMusic(this.m_worldMusicRequest);
      }
      this.m_worldMusicRequest = (AudioWorldMusicRequest) null;
    }

    public void SetInteractable(bool value)
    {
      this.m_interactable = value;
      this.m_pvpZaap.interactable = value;
      this.m_godZaap.interactable = value;
    }

    public void MoveCharacterOutsideZaap()
    {
      ZaapObject zaapObject = this.m_pvpZaap;
      if (this.m_godZaap.state == ZaapObject.ZaapState.Open)
        zaapObject = (ZaapObject) this.m_godZaap;
      zaapObject.ClosePortal();
      MapData mapFromWorldPos = MapData.GetMapFromWorldPos(this.m_mapCharacterObject.transform.position);
      if ((UnityEngine.Object) mapFromWorldPos == (UnityEngine.Object) null)
      {
        Log.Error("Actor is not on MapData", 135, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\HavreMap\\HavreMap.cs");
      }
      else
      {
        if (!this.m_quadTreePathFinding.FindPath(mapFromWorldPos, this.m_mapCharacterObject.transform.position, zaapObject.outsideDestination, this.m_path))
          return;
        this.m_mapCharacterObject.FollowPath(this.m_path);
      }
    }

    private void OnZaapClick(ZaapObject zaap)
    {
      if (!this.m_interactable)
        return;
      MapData mapFromWorldPos = MapData.GetMapFromWorldPos(this.m_mapCharacterObject.transform.position);
      if ((UnityEngine.Object) mapFromWorldPos == (UnityEngine.Object) null)
      {
        Log.Error("Actor is not on MapData", 153, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\HavreMap\\HavreMap.cs");
      }
      else
      {
        if (this.m_quadTreePathFinding.FindPath(mapFromWorldPos, this.m_mapCharacterObject.transform.position, zaap.destination, this.m_path))
          this.m_mapCharacterObject.FollowPath(this.m_path, zaap.destinationLookAt);
        if ((UnityEngine.Object) this.m_pvpZaap == (UnityEngine.Object) zaap)
          this.m_godZaap.OnClickOutside();
        else
          this.m_pvpZaap.OnClickOutside();
      }
    }

    private void OnZaapBeginOpen(ZaapObject zaap) => this.SetInteractable(false);

    private void OnZaapEndOpen(ZaapObject zaap)
    {
      if ((UnityEngine.Object) this.m_pvpZaap == (UnityEngine.Object) zaap)
      {
        Action onPvPtrigger = this.onPvPTrigger;
        if (onPvPtrigger == null)
          return;
        onPvPtrigger();
      }
      else
      {
        Action onGodTrigger = this.onGodTrigger;
        if (onGodTrigger == null)
          return;
        onGodTrigger();
      }
    }

    private void Update()
    {
      this.m_pvpZaap.UpdateCharacterPos(this.m_mapCharacterObject.transform.position);
      this.m_godZaap.UpdateCharacterPos(this.m_mapCharacterObject.transform.position);
      if (Input.GetMouseButtonDown(0) && !InputUtility.IsMouseOverUI && this.m_interactable)
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
          GameObject gameObject = hitInfo.collider.gameObject;
          if ((UnityEngine.Object) gameObject == (UnityEngine.Object) this.m_pvpZaap.gameObject || (UnityEngine.Object) gameObject == (UnityEngine.Object) this.m_godZaap.gameObject)
            return;
        }
        this.m_pvpZaap.OnClickOutside();
        this.m_godZaap.OnClickOutside();
        MapData mapFromWorldPos = MapData.GetMapFromWorldPos(this.m_mapCharacterObject.transform.position);
        if ((UnityEngine.Object) mapFromWorldPos == (UnityEngine.Object) null)
        {
          Log.Error("Actor is not on MapData", 210, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\HavreMap\\HavreMap.cs");
          return;
        }
        Vector3 hit;
        if (mapFromWorldPos.RayCast(ray, out hit) && this.m_quadTreePathFinding.FindPath(mapFromWorldPos, this.m_mapCharacterObject.transform.position, hit, this.m_path))
          this.m_mapCharacterObject.FollowPath(this.m_path);
      }
      if (!this.m_cameraHandler.camera.pixelRect.Contains(InputUtility.pointerPosition) || InputUtility.IsMouseOverUI)
        return;
      float y = Input.mouseScrollDelta.y;
      if ((double) Math.Abs(y) <= 1.4012984643248171E-45)
        return;
      this.m_cameraHandler.TweenZoom(y);
    }

    private void OnDrawGizmos()
    {
      if (!Application.isPlaying || this.m_path == null)
        return;
      Vector3 vector3_1 = Vector3.up * 0.01f;
      Gizmos.color = Color.blue;
      if (this.m_path == null || this.m_path.Count <= 0)
        return;
      Vector3 vector3_2 = this.m_path[0];
      Gizmos.DrawSphere(vector3_2 + vector3_1, 0.05f);
      for (int index = 1; index < this.m_path.Count; ++index)
      {
        Gizmos.DrawSphere(this.m_path[index] + vector3_1, 0.05f);
        Gizmos.DrawLine(vector3_2 + vector3_1, this.m_path[index] + vector3_1);
        vector3_2 = this.m_path[index];
      }
    }
  }
}
