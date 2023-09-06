// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapCameraHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Maps
{
  public class MapCameraHandler : MonoBehaviour
  {
    public const float originalAspect = 1.77777779f;
    public const int ReferenceScreenHeight = 1080;
    private const float ReferenceMaxOrthoSize = 3.6f;
    private const float ReferenceMinOrthoSize = 10f;
    private const float AbsoluteMaxOrthoSize = 2.5f;
    private const float ReferenceScreenDpi = 96f;
    private const float ReferenceMaxUnitSize = 150f;
    private const float ReferenceMaxPhysicalSize = 1.5625f;
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private Transform m_cameraContainer;
    [Header("Audio")]
    [SerializeField]
    private AudioListenerPosition m_audioListener;
    [Header("Zoom")]
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_zoomLevel;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_zoomIncrement = 0.2f;
    [SerializeField]
    private float m_zoomTweenDuration = 0.4f;
    [SerializeField]
    private Ease m_zoomEase = Ease.Linear;
    [Header("Move")]
    [SerializeField]
    private Vector3 m_targetOffset = Vector3.zero;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_minZoomLerp = 0.1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_deadZoneWidth = 0.5f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_deadZoneHeight = 0.5f;
    [SerializeField]
    private float m_damp = 0.5f;
    [SerializeField]
    private float m_unZoomDamp = 1f;
    [Header("Enter Anim")]
    [SerializeField]
    private PlayableDirector m_playableDirector;
    [SerializeField]
    private float m_animStartOrthoSize = 10f;
    [SerializeField]
    private float m_animEndZoomLevel;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_animZoomFactor;
    private bool m_allowInteraction;
    private float m_targetZoomLevel;
    private TweenerCore<float, float, FloatOptions> m_zoomTween;
    private float m_maxOrthoSize;
    private float m_minOrthoSize;
    private Transform m_target;
    private MapData m_mapData;
    private Vector3 m_characterCameraPosition;
    private bool m_isInitialized;
    private float m_originalOrthoSize;
    private Vector3 m_originalCenterPosition;
    private Rect m_clampRect;
    private Rect m_viewClampRect;
    private Rect m_viewMovableAreaRect;
    private Rect m_viewRect;
    private Vector3 m_dampVelocity = Vector3.zero;
    private bool m_dampUntilCenterReached;

    public Camera camera => this.m_camera;

    public Transform target => this.m_target;

    public Rect softRect => new Rect((float) (0.5 - (double) this.m_deadZoneWidth / 2.0), (float) (0.5 - (double) this.m_deadZoneHeight / 2.0), this.m_deadZoneWidth, this.m_deadZoneHeight);

    public void Initialize(MapData mapData, Transform targetCharacter)
    {
      this.m_mapData = mapData;
      this.m_target = targetCharacter;
      this.m_zoomLevel = 0.0f;
      this.m_targetZoomLevel = 0.0f;
      this.Setup();
    }

    public void InitEnterAnimFirstFrame()
    {
      if (!this.m_isInitialized)
        Log.Error("Camera must be initialized before.", 116, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\HavreMap\\MapCameraHandler.cs");
      this.m_allowInteraction = false;
      this.m_playableDirector.time = 0.0;
      this.m_playableDirector.Evaluate();
    }

    public IEnumerator PlayEnterAnim()
    {
      this.m_allowInteraction = false;
      this.m_playableDirector.Play();
      PlayableGraph graph = this.m_playableDirector.playableGraph;
      while (graph.IsValid() && !graph.IsDone())
        yield return (object) null;
      this.m_allowInteraction = true;
    }

    private void Setup()
    {
      if (!this.m_isInitialized)
      {
        this.m_originalOrthoSize = this.m_camera.orthographicSize;
        this.m_originalCenterPosition = this.m_cameraContainer.position;
        this.m_isInitialized = true;
      }
      this.m_minOrthoSize = (double) this.m_camera.aspect < 1.7777777910232544 ? 1.77777779f * this.m_originalOrthoSize / this.m_camera.aspect : this.m_originalOrthoSize;
      this.m_minOrthoSize = Mathf.Min(this.m_minOrthoSize, 10f);
      float dpi = Device.dpi;
      this.m_maxOrthoSize = (double) dpi > 0.0 ? Mathf.Clamp((float) ((double) this.m_camera.pixelHeight / (double) (25f / 16f * dpi) / 2.0), 2.5f, 10f) : 3.6f;
      this.m_maxOrthoSize = Mathf.Max(this.m_maxOrthoSize, 3.6f);
      float height = this.m_minOrthoSize * 2f;
      float width = height * this.m_camera.aspect;
      this.m_clampRect = new Rect((float) (-(double) width / 2.0), (float) (-(double) height / 2.0), width, height);
      this.m_camera.orthographicSize = Mathf.Lerp(this.m_minOrthoSize, this.m_maxOrthoSize, this.m_zoomLevel);
      this.m_characterCameraPosition = this.m_originalCenterPosition;
      if ((Object) this.m_target != (Object) null)
        this.m_characterCameraPosition = this.m_target.position;
      this.m_cameraContainer.position = Vector3.Lerp(this.m_originalCenterPosition, this.m_characterCameraPosition, this.m_zoomLevel);
      this.UpdateAudioListenerPosition();
    }

    private void Update()
    {
      if (!this.m_isInitialized || (Object) this.m_target == (Object) null)
        return;
      this.m_characterCameraPosition = Vector3.SmoothDamp(this.m_characterCameraPosition, this.m_target.position, ref this.m_dampVelocity, Mathf.Lerp(this.m_unZoomDamp, this.m_damp, this.m_zoomLevel));
      this.m_cameraContainer.position = Vector3.Lerp(Vector3.Lerp(this.m_originalCenterPosition, this.m_characterCameraPosition, this.m_minZoomLerp), this.m_characterCameraPosition, this.m_zoomLevel);
    }

    private Vector3 ProjectOnPlane(Vector3 worldPos)
    {
      Plane plane = new Plane(Vector3.up, this.m_cameraContainer.transform.position.y);
      Ray ray = new Ray(worldPos, this.m_camera.transform.forward);
      float enter;
      return !plane.Raycast(ray, out enter) ? Vector3.zero : ray.GetPoint(enter);
    }

    private Vector3 ClampWorldPositionToViewRect(Vector3 worldPos, Rect viewRect)
    {
      Vector2 intersection;
      if (!MapCameraHandler.Intersect((Vector2) (this.m_camera.transform.InverseTransformPoint(worldPos) with
      {
        z = 0.0f
      }), viewRect, out intersection))
        return worldPos;
      Plane plane = new Plane(Vector3.up, worldPos.y);
      Ray ray = new Ray(this.m_camera.transform.TransformPoint(new Vector3(intersection.x, intersection.y, 0.0f)), this.m_camera.transform.forward);
      float enter;
      return !plane.Raycast(ray, out enter) ? worldPos : ray.GetPoint(enter);
    }

    private static bool Intersect(Vector2 a, Rect rect, out Vector2 intersection)
    {
      intersection = Vector2.zero;
      if ((double) rect.width <= 0.0 || (double) rect.height <= 0.0)
      {
        intersection = rect.center;
        return true;
      }
      if ((double) a.x > (double) rect.xMin && (double) a.x < (double) rect.xMax && (double) a.y > (double) rect.yMin && (double) a.y < (double) rect.yMax)
        return false;
      Vector2 center = rect.center;
      Vector2 vector2_1 = new Vector2(rect.xMin, rect.yMax);
      Vector2 vector2_2 = new Vector2(rect.xMax, rect.yMax);
      Vector2 vector2_3 = new Vector2(rect.xMin, rect.yMin);
      Vector2 vector2_4 = new Vector2(rect.xMax, rect.yMin);
      return MapCameraHandler.IntersectsSegment(center, a, vector2_1, vector2_2, out intersection) || MapCameraHandler.IntersectsSegment(center, a, vector2_2, vector2_4, out intersection) || MapCameraHandler.IntersectsSegment(center, a, vector2_4, vector2_3, out intersection) || MapCameraHandler.IntersectsSegment(center, a, vector2_3, vector2_1, out intersection);
    }

    private static bool IntersectsSegment(
      Vector2 a1,
      Vector2 a2,
      Vector2 b1,
      Vector2 b2,
      out Vector2 intersection)
    {
      intersection = Vector2.zero;
      Vector2 vector2_1 = a2 - a1;
      Vector2 vector2_2 = b2 - b1;
      float num1 = (float) ((double) vector2_1.x * (double) vector2_2.y - (double) vector2_1.y * (double) vector2_2.x);
      if ((double) num1 == 0.0)
        return false;
      Vector2 vector2_3 = b1 - a1;
      float num2 = (float) ((double) vector2_3.x * (double) vector2_2.y - (double) vector2_3.y * (double) vector2_2.x) / num1;
      if ((double) num2 < 0.0 || (double) num2 > 1.0)
        return false;
      float num3 = (float) ((double) vector2_3.x * (double) vector2_1.y - (double) vector2_3.y * (double) vector2_1.x) / num1;
      if ((double) num3 < 0.0 || (double) num3 > 1.0)
        return false;
      intersection = a1 + num2 * vector2_1;
      return true;
    }

    private void UpdateViewRects()
    {
      Vector3 vector3 = this.m_camera.transform.InverseTransformPoint(this.m_originalCenterPosition);
      this.m_viewClampRect = this.m_clampRect;
      this.m_viewClampRect.x += vector3.x;
      this.m_viewClampRect.y += vector3.y;
      float height = this.m_camera.orthographicSize * 2f;
      float width = height * this.m_camera.aspect;
      this.m_viewRect = new Rect((float) (-(double) width / 2.0), (float) (-(double) height / 2.0), width, height);
      Vector2 size = Vector2.Max(Vector2.zero, this.m_viewClampRect.size - this.m_viewRect.size);
      this.m_viewMovableAreaRect = new Rect(this.m_viewClampRect.center - size / 2f, size);
    }

    private bool IsOutsideRect(Vector3 pos, Rect clampViewRect, out Vector3 delta)
    {
      bool flag = false;
      delta = Vector3.zero;
      if ((double) pos.x < (double) clampViewRect.xMin)
      {
        delta.x += pos.x - clampViewRect.xMin;
        flag = true;
      }
      if ((double) pos.x > (double) clampViewRect.xMax)
      {
        delta.x += pos.x - clampViewRect.xMax;
        flag = true;
      }
      if ((double) pos.y < (double) clampViewRect.yMin)
      {
        delta.y += pos.y - clampViewRect.yMin;
        flag = true;
      }
      if ((double) pos.y > (double) clampViewRect.yMax)
      {
        delta.y += pos.y - clampViewRect.yMax;
        flag = true;
      }
      return flag;
    }

    private Rect ViewportToViewSpace(Rect rect, float orthoSize, float aspect) => new Rect()
    {
      yMax = (float) (2.0 * (double) orthoSize * (1.0 - (double) rect.yMin - 0.5)),
      yMin = (float) (2.0 * (double) orthoSize * (1.0 - (double) rect.yMax - 0.5)),
      xMin = (float) (2.0 * (double) orthoSize * (double) aspect * ((double) rect.xMin - 0.5)),
      xMax = (float) (2.0 * (double) orthoSize * (double) aspect * ((double) rect.xMax - 0.5))
    };

    public void TweenZoom(float scrollDelta)
    {
      if (!this.m_allowInteraction)
        return;
      float num1 = Mathf.Sign(scrollDelta) * this.m_zoomIncrement;
      float num2 = Mathf.Clamp01(this.m_targetZoomLevel + num1);
      if ((double) Mathf.Abs(num2 - this.m_targetZoomLevel) < 1.4012984643248171E-45)
        return;
      float num3 = Mathf.Lerp(0.0f, this.m_zoomTweenDuration, (num2 - this.m_zoomLevel) / num1);
      this.m_targetZoomLevel = num2;
      if (this.m_zoomTween != null && this.m_zoomTween.IsPlaying())
        this.m_zoomTween.ChangeEndValue((object) num2, num3, true);
      else
        this.m_zoomTween = DOTween.To(new DOGetter<float>(this.ZoomTweenGetter), new DOSetter<float>(this.ZoomTweenSetter), num2, num3).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_zoomEase);
    }

    private float ZoomTweenGetter() => this.m_zoomLevel;

    private void ZoomTweenSetter(float value)
    {
      this.m_zoomLevel = value;
      this.m_camera.orthographicSize = Mathf.LerpUnclamped(this.m_minOrthoSize, this.m_maxOrthoSize, this.m_zoomLevel);
      this.UpdateAudioListenerPosition();
    }

    private void OnDidApplyAnimationProperties()
    {
      float num = Mathf.Lerp(Mathf.Max(this.m_animStartOrthoSize, this.m_minOrthoSize), Mathf.Lerp(this.m_minOrthoSize, this.m_maxOrthoSize, this.m_animEndZoomLevel), this.m_animZoomFactor);
      this.m_zoomLevel = Mathf.Clamp01(Mathf.InverseLerp(this.m_minOrthoSize, this.m_maxOrthoSize, num));
      this.m_targetZoomLevel = this.m_zoomLevel;
      this.m_camera.orthographicSize = num;
      this.UpdateAudioListenerPosition();
    }

    private void UpdateAudioListenerPosition() => this.m_audioListener.UpdatePosition(this.m_zoomLevel);

    private void OnScreenStateChange() => this.Setup();

    private void OnEnable()
    {
      this.m_targetZoomLevel = this.m_zoomLevel;
      this.Setup();
      Device.ScreenStateChanged += new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
    }

    private void OnDisable()
    {
      if (this.m_zoomTween != null)
      {
        this.m_zoomTween.Kill();
        this.m_zoomTween = (TweenerCore<float, float, FloatOptions>) null;
      }
      Device.ScreenStateChanged -= new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
    }
  }
}
