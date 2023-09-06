// Decompiled with JetBrains decompiler
// Type: SpellFXTest
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;

public class SpellFXTest : MonoBehaviour
{
  [SerializeField]
  private SpellFXTest.Effect[] effects;
  [SerializeField]
  private Camera m_camera;
  [SerializeField]
  private Transform m_cameraRoot;
  [SerializeField]
  private Light m_directionalLight;
  [SerializeField]
  private Light m_light;
  [SerializeField]
  private float margin;
  private Color m_baseLightColor;
  private Color m_baseAmbiantColor;
  private Coroutine m_animationCoroutine;
  private int m_index;

  private void Awake()
  {
    this.m_baseLightColor = this.m_directionalLight.color;
    this.m_baseAmbiantColor = RenderSettings.ambientLight;
    this.m_index = 0;
    this.m_light.enabled = false;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      ++this.m_index;
      this.m_index = Mathf.Clamp(this.m_index, 0, this.effects.Length - 1);
      Debug.Log((object) this.m_index);
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      --this.m_index;
      this.m_index = Mathf.Clamp(this.m_index, 0, this.effects.Length - 1);
      Debug.Log((object) this.m_index);
    }
    if (!Input.GetKeyDown(KeyCode.Space) || this.m_animationCoroutine != null)
      return;
    this.m_animationCoroutine = this.StartCoroutine(this.AnimationCoroutine(this.effects[this.m_index]));
  }

  private IEnumerator AnimationCoroutine(SpellFXTest.Effect e)
  {
    SpellFXTest spellFxTest = this;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    Vector3 cameraStartingPosition = spellFxTest.m_cameraRoot.position;
    float cameraStartingSize = spellFxTest.m_camera.orthographicSize;
    Vector3 vector3_1 = new Vector3(0.0f, 0.0f, 0.0f);
    float a1 = 0.0f;
    float a2 = 0.0f;
    float a3 = 0.0f;
    float a4 = 0.0f;
    for (int index = 0; index < e.targets.Length; ++index)
    {
      vector3_1 += e.targets[index].position;
      Vector3 vector3_2 = spellFxTest.m_camera.transform.InverseTransformPoint(e.targets[index].position);
      a1 = Mathf.Min(a1, vector3_2.x);
      a2 = Mathf.Max(a2, vector3_2.x);
      a3 = Mathf.Min(a3, vector3_2.y);
      a4 = Mathf.Max(a4, vector3_2.y);
    }
    float num1 = (float) (((double) a2 - (double) a1) / 2.0) + spellFxTest.margin;
    float a5 = (float) (((double) a4 - (double) a3) / 2.0) + spellFxTest.margin;
    float num2 = (float) Screen.width / (float) Screen.height * e.cameraTargetSize;
    float targetSize;
    if ((double) num1 - (double) num2 < (double) (a5 - e.cameraTargetSize))
    {
      targetSize = Mathf.Max(num1 * ((float) Screen.height / (float) Screen.width), e.cameraTargetSize);
      Debug.Log((object) ("X = " + (object) (float) ((double) num1 * ((double) Screen.height / (double) Screen.width))));
    }
    else
    {
      targetSize = Mathf.Max(a5, e.cameraTargetSize);
      Debug.Log((object) ("Y = " + (object) a5));
    }
    Debug.Log((object) ("targetSize = " + (object) targetSize));
    float length = (float) e.targets.Length;
    vector3_1.x /= length;
    vector3_1.y /= length;
    vector3_1.z /= length;
    Vector3 targetPosition = vector3_1 - spellFxTest.m_cameraRoot.forward * 20f;
    Color targetLightColor = spellFxTest.m_baseLightColor * e.lightMultiplier;
    Color targetAmbiantColor = spellFxTest.m_baseAmbiantColor * e.lightMultiplier;
    float f;
    for (f = 0.0f; (double) f < 1.0; f += Time.deltaTime / e.inDuration)
    {
      float t = e.inCurve.Evaluate(f);
      if (e.moveCamera)
      {
        spellFxTest.m_cameraRoot.position = Vector3.LerpUnclamped(cameraStartingPosition, targetPosition, t);
        spellFxTest.m_camera.orthographicSize = Mathf.LerpUnclamped(cameraStartingSize, targetSize, t);
      }
      spellFxTest.m_directionalLight.color = Color.Lerp(spellFxTest.m_baseLightColor, targetLightColor, t);
      RenderSettings.ambientLight = Color.Lerp(spellFxTest.m_baseAmbiantColor, targetAmbiantColor, t);
      yield return (object) wait;
    }
    spellFxTest.StartCoroutine(spellFxTest.PlayFXCoroutine(e));
    spellFxTest.StartCoroutine(spellFxTest.ShakeCoroutine(e));
    if (e.light)
      spellFxTest.StartCoroutine(spellFxTest.LightCoroutine(e));
    for (f = 0.0f; (double) f < (double) e.animationDuration; f += Time.deltaTime)
      yield return (object) wait;
    for (f = 0.0f; (double) f < 1.0; f += Time.deltaTime / e.outDuration)
    {
      float t = e.outCurve.Evaluate(f);
      if (e.moveCamera)
      {
        spellFxTest.m_cameraRoot.position = Vector3.LerpUnclamped(cameraStartingPosition, targetPosition, t);
        spellFxTest.m_camera.orthographicSize = Mathf.LerpUnclamped(cameraStartingSize, targetSize, t);
      }
      spellFxTest.m_directionalLight.color = Color.Lerp(spellFxTest.m_baseLightColor, targetLightColor, t);
      RenderSettings.ambientLight = Color.Lerp(spellFxTest.m_baseAmbiantColor, targetAmbiantColor, t);
      yield return (object) wait;
    }
    if (e.moveCamera)
    {
      spellFxTest.m_cameraRoot.position = cameraStartingPosition;
      spellFxTest.m_camera.orthographicSize = cameraStartingSize;
    }
    spellFxTest.m_directionalLight.color = spellFxTest.m_baseLightColor;
    RenderSettings.ambientLight = spellFxTest.m_baseAmbiantColor;
    spellFxTest.m_animationCoroutine = (Coroutine) null;
  }

  private IEnumerator PlayFXCoroutine(SpellFXTest.Effect e)
  {
    for (int i = 0; i < e.targets.Length; ++i)
    {
      yield return (object) new WaitForTime(e.fxDelay);
      UnityEngine.Object.Instantiate<GameObject>(e.fxGameObject, e.targets[i].position + e.fxOffset, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
  }

  private IEnumerator ShakeCoroutine(SpellFXTest.Effect e)
  {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime / e.shakeDuration)
    {
      float num = e.shakeCurve.Evaluate(f);
      this.m_camera.transform.localPosition = new Vector3(UnityEngine.Random.Range(-e.shakeAmplitude.x, e.shakeAmplitude.x), UnityEngine.Random.Range(-e.shakeAmplitude.y, e.shakeAmplitude.y), UnityEngine.Random.Range(-e.shakeAmplitude.z, e.shakeAmplitude.z)) * num;
      yield return (object) wait;
    }
  }

  private IEnumerator LightCoroutine(SpellFXTest.Effect e)
  {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    this.m_light.color = e.lightColor;
    this.m_light.transform.position = e.targets[0].position + e.lightOffset;
    this.m_light.enabled = true;
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime / e.lightDuration)
    {
      this.m_light.intensity = e.lightCurve.Evaluate(f) * e.lightIntensity;
      yield return (object) wait;
    }
    this.m_light.enabled = false;
  }

  [Serializable]
  public class Effect
  {
    [Header("Animation")]
    public AnimationCurve inCurve;
    public float inDuration;
    public AnimationCurve outCurve;
    public float outDuration;
    public float animationDuration;
    [Header("Lighting")]
    public float lightMultiplier;
    [Header("FXs")]
    public GameObject fxGameObject;
    public Vector3 fxOffset;
    public float fxDelay;
    [Header("Camera")]
    public bool moveCamera;
    public Transform[] targets;
    public float cameraTargetSize;
    [Header("Shake")]
    public AnimationCurve shakeCurve;
    public float shakeDuration;
    public Vector3 shakeAmplitude;
    [Header("Light")]
    public bool light;
    public Color lightColor;
    public AnimationCurve lightCurve;
    public float lightDuration;
    public float lightIntensity;
    public Vector3 lightOffset;
  }
}
