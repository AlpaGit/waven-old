// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.CameraHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [RequireComponent(typeof (Camera))]
  public sealed class CameraHandler : MonoBehaviour
  {
    public const int ReferenceScreenHeight = 1080;
    private const float ReferenceMinOrthoSize = 6f;
    private const float AbsoluteMaxOrthoSize = 2.5f;
    private const float ReferenceScreenDpi = 96f;
    private const float ReferenceMaxOrthoSize = 3f;
    private const float ReferenceMaxUnitSize = 180f;
    private const float ReferenceMaxPhysicalSize = 1.875f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_zoomLevel;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_zoomIncrement = 0.2f;
    [SerializeField]
    private float m_zoomTweenDuration = 0.4f;
    [SerializeField]
    private float m_panTweenDuration = 0.25f;
    [SerializeField]
    private Vector3 m_cameraPositionOffset = new Vector3(-0.5f, 0.0f, -0.5f);
    [SerializeField]
    private Vector2 m_cameraBoundsMargin = new Vector2(-5f, -5f);
    [SerializeField]
    private Vector2 m_regionFocusMargin = new Vector2(2f, 2f);
    [SerializeField]
    private CameraShakeParameters m_shakeParameters;
    [SerializeField]
    private CameraControlParameters m_defaultControlParameters;
    [SerializeField]
    private CameraControlParameters m_cinematicControlParameters;
    public Action<CameraHandler> onMoved;
    public Action<CameraHandler> onZoomChanged;
    private Transform m_cameraContainer;
    private Bounds m_mapWorldBounds;
    private CameraHandler.CameraWorldRect m_cameraWorldRect;
    private float m_maxOrthoSize;
    private float m_targetZoomLevel;
    private Vector3 m_targetCameraPosition;
    private TweenerCore<Vector3, Vector3, VectorOptions> m_panTween;
    private TweenerCore<float, float, FloatOptions> m_zoomTween;
    private bool m_cameraIsShaking;
    private float m_cameraShakeStrength;
    private bool m_userHasControl;
    private Coroutine m_regionFocusCoroutine;

    public static CameraHandler current { get; private set; }

    [PublicAPI]
    public Camera camera { get; private set; }

    [PublicAPI]
    public DirectionAngle mapRotation { get; private set; }

    [PublicAPI]
    public float zoomLevel => this.m_zoomLevel;

    [PublicAPI]
    public float zoomScale => 6f / this.camera.orthographicSize;

    [PublicAPI]
    public bool hasZoomRange => (double) this.m_maxOrthoSize < 6.0;

    [PublicAPI]
    public bool userHasControl => this.m_userHasControl;

    [PublicAPI]
    public CameraControlParameters cinematicControlParameters => this.m_cinematicControlParameters;

    public void Initialize(
      [NotNull] IMapDefinition mapDefinition,
      Bounds mapWorldBounds,
      bool giveUserControl)
    {
      Camera camera = this.camera;
      if ((UnityEngine.Object) null == (UnityEngine.Object) camera)
        throw new NullReferenceException("A camera used by a CameraHandler was destroyed.");
      Transform parent1 = camera.transform.parent;
      if ((UnityEngine.Object) null == (UnityEngine.Object) parent1)
      {
        Log.Error("Camera doesn't have a pivot transform.", 151, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\CameraHandler.cs");
      }
      else
      {
        Transform parent2 = parent1.parent;
        if ((UnityEngine.Object) null == (UnityEngine.Object) parent2)
        {
          Log.Error("Camera doesn't have a container transform.", 158, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\CameraHandler.cs");
        }
        else
        {
          CameraHandler.CameraWorldRect cameraWorldRect = new CameraHandler.CameraWorldRect(mapDefinition, this.m_cameraBoundsMargin);
          Vector3 vector3 = cameraWorldRect.center + parent2.rotation * this.m_cameraPositionOffset;
          parent2.position = vector3;
          float orthographicSize = this.GetInitializationOrthographicSize(cameraWorldRect, this.m_cameraBoundsMargin);
          float num = MathUtility.InverseLerpUnclamped(6f, this.m_maxOrthoSize, orthographicSize);
          camera.orthographicSize = orthographicSize;
          this.m_zoomLevel = num;
          this.m_targetZoomLevel = num;
          this.m_targetCameraPosition = vector3;
          this.m_cameraContainer = parent2;
          this.m_mapWorldBounds = mapWorldBounds;
          this.m_cameraWorldRect = cameraWorldRect;
          this.m_userHasControl = giveUserControl;
        }
      }
    }

    private float GetInitializationOrthographicSize(
      CameraHandler.CameraWorldRect cameraWorldRect,
      Vector2 margin)
    {
      CameraHandler.CameraWorldRect cameraWorldRect1 = cameraWorldRect.RemoveMargin(margin);
      float num = 2f * this.m_cameraPositionOffset.magnitude;
      return Mathf.Max(Mathf.Max((cameraWorldRect1.top - cameraWorldRect1.bottom + this.m_regionFocusMargin.x + num) / 4f, (float) (((double) cameraWorldRect1.right - (double) cameraWorldRect1.left + (double) this.m_regionFocusMargin.y + (double) num) / 2.0) / this.camera.aspect), 6f);
    }

    public void StartFocusOnMapRegion(
      [NotNull] IMapDefinition mapDefinition,
      int regionIndex,
      [CanBeNull] CameraControlParameters parameters = null)
    {
      this.m_regionFocusCoroutine = this.StartCoroutine(this.FocusOnMapRegion(mapDefinition, regionIndex, parameters));
    }

    public IEnumerator FocusOnMapRegion(
      [NotNull] IMapDefinition mapDefinition,
      int regionIndex,
      [CanBeNull] CameraControlParameters parameters = null)
    {
      CameraHandler cameraHandler = this;
      if (cameraHandler.m_regionFocusCoroutine != null)
      {
        cameraHandler.StopCoroutine(cameraHandler.m_regionFocusCoroutine);
        cameraHandler.m_regionFocusCoroutine = (Coroutine) null;
      }
      Camera camera = cameraHandler.camera;
      Transform cameraContainer = cameraHandler.m_cameraContainer;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) camera) && !((UnityEngine.Object) null == (UnityEngine.Object) cameraContainer))
      {
        float panTweenDuration;
        Ease ease1;
        float b1;
        Ease ease2;
        if ((UnityEngine.Object) null == (UnityEngine.Object) parameters)
        {
          CameraControlParameters controlParameters = cameraHandler.m_defaultControlParameters;
          if ((UnityEngine.Object) null == (UnityEngine.Object) controlParameters)
          {
            panTweenDuration = cameraHandler.m_panTweenDuration;
            ease1 = Ease.OutCubic;
            b1 = cameraHandler.m_zoomTweenDuration;
            ease2 = Ease.OutCubic;
          }
          else
          {
            panTweenDuration = controlParameters.panTweenDuration;
            ease1 = controlParameters.panTweenEase;
            b1 = controlParameters.zoomTweenMaxDuration;
            ease2 = controlParameters.zoomTweenEase;
          }
        }
        else
        {
          panTweenDuration = parameters.panTweenDuration;
          ease1 = parameters.panTweenEase;
          b1 = parameters.zoomTweenMaxDuration;
          ease2 = parameters.zoomTweenEase;
        }
        FightMapRegionDefinition region = mapDefinition.GetRegion(regionIndex);
        Vector3 origin = (Vector3) mapDefinition.origin;
        Vector2 sizeMin = (Vector2) region.sizeMin;
        Vector2 sizeMax = (Vector2) region.sizeMax;
        Vector3 vector3_1 = cameraContainer.rotation * cameraHandler.m_cameraPositionOffset;
        Vector2 vector2_1 = sizeMin + 0.5f * (sizeMax - Vector2.one - sizeMin);
        Vector3 targetCameraPosition = new Vector3(vector2_1.x, 0.0f, vector2_1.y) + origin + vector3_1;
        Vector3 a1 = new Vector3(sizeMin.x, 0.0f, sizeMin.y);
        Vector3 b2 = new Vector3(sizeMax.x, 0.0f, sizeMax.y);
        Vector3 a2 = new Vector3(a1.x, 0.0f, b2.z);
        Vector3 b3 = new Vector3(b2.x, 0.0f, a1.z);
        Vector3 vector3_2 = cameraContainer.rotation * new Vector3(1f, 0.0f, 1f);
        Vector3 normalized1 = vector3_2.normalized;
        Vector3 normalized2 = vector3_1.normalized;
        vector3_2 = b2 - a1;
        float num1 = Mathf.Abs(Vector3.Dot(vector3_2.normalized, normalized1));
        vector3_2 = b3 - a2;
        float num2 = Mathf.Abs(Vector3.Dot(vector3_2.normalized, normalized1));
        Vector3 rhs = normalized1;
        float num3 = Mathf.Abs(Vector3.Dot(normalized2, rhs));
        float num4 = Vector3.Distance(a1, b2);
        float num5 = Vector3.Distance(a2, b3);
        float num6 = 2f * vector3_1.magnitude;
        Vector2 vector2_2 = 2.828427f * cameraHandler.m_regionFocusMargin;
        float num7 = Mathf.Max(((float) ((double) num1 * (double) num4 + (double) num2 * (double) num5 + (double) num3 * (double) num6) + vector2_2.y) / 4f, (float) (((1.0 - (double) num1) * (double) num4 + (1.0 - (double) num2) * (double) num5 + (1.0 - (double) num3) * (double) num6 + (double) vector2_2.x) / 2.0) / camera.aspect);
        float targetZoomLevel = Mathf.InverseLerp(6f, cameraHandler.m_maxOrthoSize, num7);
        targetZoomLevel = Mathf.Floor(targetZoomLevel / cameraHandler.m_zoomIncrement) * cameraHandler.m_zoomIncrement;
        cameraHandler.m_targetCameraPosition = targetCameraPosition;
        cameraHandler.m_targetZoomLevel = targetZoomLevel;
        cameraHandler.m_userHasControl = false;
        if (cameraHandler.m_panTween != null && cameraHandler.m_panTween.IsPlaying())
          cameraHandler.m_panTween.ChangeEndValue((object) targetCameraPosition, panTweenDuration, true).SetEase<Tweener>(ease1);
        else
          cameraHandler.m_panTween = DOTween.To(new DOGetter<Vector3>(cameraHandler.PanTweenGetter), new DOSetter<Vector3>(cameraHandler.PanTweenSetter), targetCameraPosition, panTweenDuration).SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(ease1);
        float num8 = Mathf.Lerp(0.0f, b1, Mathf.Abs(targetZoomLevel - cameraHandler.m_zoomLevel) / cameraHandler.m_zoomIncrement);
        if (cameraHandler.m_zoomTween != null && cameraHandler.m_zoomTween.IsPlaying())
          cameraHandler.m_zoomTween.ChangeEndValue((object) targetZoomLevel, num8, true).SetEase<Tweener>(ease2);
        else
          cameraHandler.m_zoomTween = DOTween.To(new DOGetter<float>(cameraHandler.ZoomTweenGetter), new DOSetter<float>(cameraHandler.ZoomTweenSetter), targetZoomLevel, num8).SetEase<TweenerCore<float, float, FloatOptions>>(ease2);
        while (cameraHandler.m_panTween.IsPlaying() || cameraHandler.m_zoomTween.IsPlaying())
        {
          yield return (object) null;
          if (cameraHandler.m_targetCameraPosition != targetCameraPosition || (double) cameraHandler.m_targetZoomLevel != (double) targetZoomLevel)
            break;
        }
        cameraHandler.m_regionFocusCoroutine = (Coroutine) null;
        cameraHandler.m_userHasControl = true;
      }
    }

    public void Pan(Vector2 screenPosition, Vector2 previousScreenPosition)
    {
      Camera camera = this.camera;
      Transform cameraContainer = this.m_cameraContainer;
      if ((UnityEngine.Object) null == (UnityEngine.Object) camera || (UnityEngine.Object) null == (UnityEngine.Object) cameraContainer)
        return;
      Plane plane = new Plane(Vector3.up, this.m_mapWorldBounds.center);
      Ray ray1 = camera.ScreenPointToRay((Vector3) screenPosition);
      float enter;
      plane.Raycast(ray1, out enter);
      Vector3 point = ray1.GetPoint(enter);
      Ray ray2 = camera.ScreenPointToRay((Vector3) previousScreenPosition);
      plane.Raycast(ray2, out enter);
      Vector3 vector3_1 = ray2.GetPoint(enter) - point;
      Vector3 vector3_2 = cameraContainer.rotation * this.m_cameraPositionOffset;
      Vector3 position = this.m_targetCameraPosition + vector3_1 - vector3_2;
      Vector3 vector3_3 = vector3_2 + this.m_cameraWorldRect.ClosestPoint(position);
      this.m_targetCameraPosition = vector3_3;
      if (this.m_panTween != null && this.m_panTween.IsPlaying())
        this.m_panTween.ChangeEndValue((object) vector3_3, this.m_panTweenDuration, true);
      else
        this.m_panTween = DOTween.To(new DOGetter<Vector3>(this.PanTweenGetter), new DOSetter<Vector3>(this.PanTweenSetter), vector3_3, this.m_panTweenDuration);
    }

    public void TweenZoom(float scrollDelta)
    {
      float num1 = Mathf.Sign(scrollDelta) * this.m_zoomIncrement;
      float num2 = Mathf.Clamp01(this.m_targetZoomLevel + num1);
      if ((double) Math.Abs(num2 - this.m_targetZoomLevel) < 1.4012984643248171E-45)
        return;
      float num3 = Mathf.Lerp(0.0f, this.m_zoomTweenDuration, (num2 - this.m_zoomLevel) / num1);
      this.m_targetZoomLevel = num2;
      if (this.m_zoomTween != null && this.m_zoomTween.IsPlaying())
        this.m_zoomTween.ChangeEndValue((object) num2, num3, true);
      else
        this.m_zoomTween = DOTween.To(new DOGetter<float>(this.ZoomTweenGetter), new DOSetter<float>(this.ZoomTweenSetter), num2, num3);
    }

    private static event CameraHandler.MapRotationChangedDelegate MapRotationChanged;

    public static void AddMapRotationListener([NotNull] CameraHandler.MapRotationChangedDelegate callback)
    {
      CameraHandler.MapRotationChanged += callback;
      if ((UnityEngine.Object) null == (UnityEngine.Object) CameraHandler.current)
        callback(DirectionAngle.None, DirectionAngle.None);
      else
        callback(DirectionAngle.None, CameraHandler.current.mapRotation);
    }

    public static void RemoveMapRotationListener([NotNull] CameraHandler.MapRotationChangedDelegate callback) => CameraHandler.MapRotationChanged -= callback;

    public void ChangeRotation(DirectionAngle directionAngle)
    {
      if (this.mapRotation == directionAngle)
        return;
      DirectionAngle mapRotation = this.mapRotation;
      this.mapRotation = directionAngle;
      Transform cameraContainer = this.m_cameraContainer;
      if ((UnityEngine.Object) null != (UnityEngine.Object) cameraContainer)
      {
        Quaternion inverseRotation = directionAngle.GetInverseRotation();
        Vector3 vector3 = mapRotation.GetInverseRotation() * -this.m_cameraPositionOffset + inverseRotation * this.m_cameraPositionOffset;
        cameraContainer.SetPositionAndRotation(cameraContainer.position + vector3, inverseRotation);
      }
      CameraHandler.MapRotationChangedDelegate mapRotationChanged = CameraHandler.MapRotationChanged;
      if (mapRotationChanged == null)
        return;
      mapRotationChanged(mapRotation, directionAngle);
    }

    public void AddShake(float value) => this.m_cameraShakeStrength = Mathf.Clamp01(this.m_cameraShakeStrength + value);

    private void Awake()
    {
      CameraHandler.current = this;
      this.camera = this.GetComponent<Camera>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.camera)
        throw new NullReferenceException("A camera used by a CameraHandler was destroyed.");
      this.m_targetZoomLevel = this.m_zoomLevel;
      this.Setup();
      Device.ScreenStateChanged += new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
    }

    private void LateUpdate()
    {
      float cameraShakeStrength = this.m_cameraShakeStrength;
      if ((double) cameraShakeStrength > 0.0)
      {
        CameraShakeParameters shakeParameters = this.m_shakeParameters;
        if ((UnityEngine.Object) null != (UnityEngine.Object) shakeParameters)
        {
          Transform transform = this.transform;
          float time = Time.time;
          Vector2 translation = shakeParameters.GetTranslation(time, cameraShakeStrength);
          double angle = (double) shakeParameters.GetAngle(time, cameraShakeStrength);
          Vector3 vector3 = new Vector3(translation.x, translation.y, transform.localPosition.z);
          Vector3 forward = Vector3.forward;
          Quaternion quaternion = Quaternion.AngleAxis((float) angle, forward);
          transform.localPosition = vector3;
          transform.localRotation = quaternion;
          this.m_cameraIsShaking = true;
        }
      }
      else if (this.m_cameraIsShaking)
      {
        Transform transform = this.transform;
        transform.localPosition = transform.localPosition with
        {
          x = 0.0f,
          y = 0.0f
        };
        transform.localRotation = Quaternion.identity;
        this.m_cameraIsShaking = false;
      }
      this.m_cameraShakeStrength = 0.0f;
    }

    private void OnDestroy()
    {
      if ((UnityEngine.Object) CameraHandler.current == (UnityEngine.Object) this)
        CameraHandler.current = (CameraHandler) null;
      if (this.m_panTween != null)
      {
        this.m_panTween.Kill();
        this.m_panTween = (TweenerCore<Vector3, Vector3, VectorOptions>) null;
      }
      if (this.m_zoomTween != null)
      {
        this.m_zoomTween.Kill();
        this.m_zoomTween = (TweenerCore<float, float, FloatOptions>) null;
      }
      Device.ScreenStateChanged -= new Device.ScreenSateChangedDelegate(this.OnScreenStateChange);
    }

    private void Setup()
    {
      float dpi = Device.dpi;
      this.m_maxOrthoSize = (double) dpi > 0.0 ? Mathf.Clamp((float) ((double) this.camera.pixelHeight / (double) (1.875f * dpi) / 2.0), 2.5f, 6f) : 3f;
      this.camera.orthographicSize = Mathf.Lerp(6f, this.m_maxOrthoSize, this.m_zoomLevel);
    }

    private void OnScreenStateChange() => this.Setup();

    private Vector3 PanTweenGetter() => this.m_cameraContainer.position;

    private void PanTweenSetter(Vector3 value)
    {
      this.m_cameraContainer.position = value;
      Action<CameraHandler> onMoved = this.onMoved;
      if (onMoved == null)
        return;
      onMoved(this);
    }

    private float ZoomTweenGetter() => this.m_zoomLevel;

    private void ZoomTweenSetter(float value)
    {
      this.camera.orthographicSize = Mathf.LerpUnclamped(6f, this.m_maxOrthoSize, value);
      this.m_zoomLevel = value;
      Action<CameraHandler> onZoomChanged = this.onZoomChanged;
      if (onZoomChanged == null)
        return;
      onZoomChanged(this);
    }

    private struct CameraWorldRect
    {
      private const float OneOverSqrtTwo = 0.707106769f;
      public readonly float top;
      public readonly float left;
      public readonly float bottom;
      public readonly float right;

      public Vector3 center
      {
        [Pure] get
        {
          float num1 = this.left + (float) (0.5 * ((double) this.right - (double) this.left));
          double num2 = (double) this.bottom + 0.5 * ((double) this.top - (double) this.bottom);
          float num3 = num1 * 0.707106769f;
          float num4 = (float) (num2 * 0.70710676908493042);
          return (Vector3) new Vector2(num3 + num4, num4 - num3);
        }
      }

      public CameraWorldRect(IMapDefinition mapDefinition, Vector2 margin)
      {
        float num1 = 0.0f;
        float num2 = 0.0f;
        float num3 = 0.0f;
        float num4 = 0.0f;
        int regionCount = mapDefinition.regionCount;
        for (int index = 0; index < regionCount; ++index)
        {
          FightMapRegionDefinition region = mapDefinition.GetRegion(index);
          Vector2Int sizeMin = region.sizeMin;
          Vector2Int v1 = region.sizeMax - Vector2Int.one;
          Vector2Int v2 = new Vector2Int(sizeMin.x, v1.y);
          Vector2Int v3 = new Vector2Int(v1.x, sizeMin.y);
          Vector2 vector2_1 = CameraHandler.CameraWorldRect.Translate((Vector2) sizeMin);
          Vector2 vector2_2 = CameraHandler.CameraWorldRect.Translate((Vector2) v1);
          Vector2 vector2_3 = CameraHandler.CameraWorldRect.Translate((Vector2) v2);
          Vector2 vector2_4 = CameraHandler.CameraWorldRect.Translate((Vector2) v3);
          num1 = Mathf.Max(num1, vector2_1.y, vector2_2.y, vector2_3.y, vector2_4.y);
          num3 = Mathf.Min(num3, vector2_1.y, vector2_2.y, vector2_3.y, vector2_4.y);
          num2 = Mathf.Min(num2, vector2_1.x, vector2_2.x, vector2_3.x, vector2_4.x);
          num4 = Mathf.Max(num4, vector2_1.x, vector2_2.x, vector2_3.x, vector2_4.x);
        }
        Vector3 vector3 = (Vector3) CameraHandler.CameraWorldRect.Translate((Vector3) mapDefinition.origin);
        float num5 = (float) (((double) margin.x + 1.0) * 0.70710676908493042);
        float num6 = (float) (((double) margin.y + 1.0) * 0.70710676908493042);
        this.left = vector3.x + num2 - num5;
        this.right = vector3.x + num4 + num5;
        this.top = vector3.z + num1 + num6;
        this.bottom = vector3.z + num3 - num6;
        if ((double) this.bottom > (double) this.top)
          this.top = this.bottom = vector3.z;
        if ((double) this.left <= (double) this.right)
          return;
        this.left = this.right = vector3.x;
      }

      private CameraWorldRect(float top, float left, float bottom, float right)
      {
        this.top = top;
        this.left = left;
        this.bottom = bottom;
        this.right = right;
      }

      [PublicAPI]
      [Pure]
      public static Vector2 Translate(Vector2 v)
      {
        float num1 = v.x * 0.707106769f;
        float num2 = v.y * 0.707106769f;
        return new Vector2(num1 - num2, num1 + num2);
      }

      [PublicAPI]
      [Pure]
      public static Vector2 Translate(Vector3 v)
      {
        float num1 = v.x * 0.707106769f;
        float num2 = v.z * 0.707106769f;
        return (Vector2) new Vector3(num1 - num2, v.y, num1 + num2);
      }

      [PublicAPI]
      [Pure]
      public static Vector2 TranslateInverse(Vector2 v)
      {
        float num1 = v.x * 0.707106769f;
        float num2 = v.y * 0.707106769f;
        return new Vector2(num1 + num2, num2 - num1);
      }

      [PublicAPI]
      [Pure]
      public static Vector3 TranslateInverse(Vector3 v)
      {
        float num1 = v.x * 0.707106769f;
        float num2 = v.z * 0.707106769f;
        return new Vector3(num1 + num2, v.y, num2 - num1);
      }

      [PublicAPI]
      [Pure]
      public bool ContainsPoint(Vector2 position)
      {
        double num1 = (double) position.x * 0.70710676908493042;
        float num2 = position.y * 0.707106769f;
        float num3 = (float) num1 - num2;
        float num4 = (float) num1 + num2;
        return (double) num3 >= (double) this.left && (double) num3 <= (double) this.right && (double) num4 >= (double) this.bottom && (double) num4 <= (double) this.top;
      }

      [PublicAPI]
      [Pure]
      public bool ContainsPoint(Vector3 position)
      {
        double num1 = (double) position.x * 0.70710676908493042;
        float num2 = position.z * 0.707106769f;
        float num3 = (float) num1 - num2;
        float num4 = (float) num1 + num2;
        return (double) num3 >= (double) this.left && (double) num3 <= (double) this.right && (double) num4 >= (double) this.bottom && (double) num4 <= (double) this.top;
      }

      [PublicAPI]
      [Pure]
      public Vector2 ClosestPoint(Vector2 position)
      {
        double num1 = (double) position.x * 0.70710676908493042;
        float num2 = position.y * 0.707106769f;
        float num3 = Mathf.Clamp((float) num1 - num2, this.left, this.right);
        double num4 = (double) Mathf.Clamp((float) num1 + num2, this.bottom, this.top);
        float num5 = num3 * 0.707106769f;
        float num6 = (float) (num4 * 0.70710676908493042);
        return new Vector2(num5 + num6, num6 - num5);
      }

      [PublicAPI]
      [Pure]
      public Vector3 ClosestPoint(Vector3 position)
      {
        double num1 = (double) position.x * 0.70710676908493042;
        float num2 = position.z * 0.707106769f;
        float num3 = Mathf.Clamp((float) num1 - num2, this.left, this.right);
        double num4 = (double) Mathf.Clamp((float) num1 + num2, this.bottom, this.top);
        float num5 = num3 * 0.707106769f;
        float num6 = (float) (num4 * 0.70710676908493042);
        return new Vector3(num5 + num6, position.y, num6 - num5);
      }

      [Pure]
      public CameraHandler.CameraWorldRect RemoveMargin(Vector2 margin)
      {
        float num1 = (float) (((double) margin.x + 1.0) * 0.70710676908493042);
        float num2 = (float) (((double) margin.y + 1.0) * 0.70710676908493042);
        float left = this.left + num1;
        float right = this.right - num1;
        float top = this.top - num2;
        float bottom = this.bottom + num2;
        if ((double) bottom > (double) top)
          top = bottom = this.bottom + (float) (((double) this.top - (double) this.bottom) * 0.5);
        if ((double) this.left > (double) this.right)
          left = right = this.left + (float) (((double) this.right - (double) this.left) * 0.5);
        return new CameraHandler.CameraWorldRect(top, left, bottom, right);
      }
    }

    public delegate void MapRotationChangedDelegate(
      DirectionAngle previousDirectionAngle,
      DirectionAngle newDirectionAngle);
  }
}
