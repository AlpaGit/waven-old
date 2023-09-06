// Decompiled with JetBrains decompiler
// Type: Portal
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio.UI;
using Ankama.Cube.Maps;
using System;
using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
  [SerializeField]
  private ParticleSystem[] m_highLightParticleSystems;
  [SerializeField]
  private ParticleSystem[] m_openParticleSystems;
  [SerializeField]
  private Portal.AnimatedSprite[] m_animatedSprites;
  [SerializeField]
  private SpriteRenderer m_groundIcon;
  [SerializeField]
  private Transform m_scaleTransform;
  [SerializeField]
  private Vector3 m_defaultScale = new Vector3(1f, 1f, 1f);
  [SerializeField]
  private Vector3 m_highLigthScale = new Vector3(1.25f, 1.25f, 1.25f);
  [SerializeField]
  private AnimationCurve m_scaleCurve;
  [SerializeField]
  private float m_animationDuration;
  [SerializeField]
  private float m_groundEffectDuration;
  [Header("Audio")]
  [SerializeField]
  private AudioEventUIPlayWhileEnabled m_highlightLoopAudio;
  [SerializeField]
  private AudioEventUITriggerOnEnable m_openingAudio;
  private MaterialPropertyBlock m_materialPropertyBlock;
  private int m_openID;
  private int m_highLightID;
  private Vector3 m_currentScale;
  private Coroutine m_scaleCoroutine;
  private Coroutine m_animateGroundFXCoroutine;
  private float m_openCurrentValue;
  private float m_highLightCurrentValue;

  private MaterialPropertyBlock materialPropertyBlock
  {
    get
    {
      if (this.m_materialPropertyBlock == null)
      {
        this.m_materialPropertyBlock = new MaterialPropertyBlock();
        this.m_groundIcon.GetPropertyBlock(this.m_materialPropertyBlock);
      }
      return this.m_materialPropertyBlock;
    }
  }

  private void Awake()
  {
    this.m_openID = Shader.PropertyToID("_Open");
    this.m_highLightID = Shader.PropertyToID("_HighLight");
    this.m_currentScale = this.m_defaultScale;
    this.m_scaleTransform.localScale = this.m_currentScale;
    this.m_openCurrentValue = 0.0f;
    this.m_highLightCurrentValue = 0.0f;
    for (int index = 0; index < this.m_animatedSprites.Length; ++index)
    {
      this.m_animatedSprites[index].currentColor = this.m_animatedSprites[index].defaultColor;
      this.m_animatedSprites[index].spriteRenderer.color = this.m_animatedSprites[index].currentColor;
    }
    this.m_highlightLoopAudio.gameObject.SetActive(false);
    this.m_openingAudio.gameObject.SetActive(false);
  }

  public void SetState(ZaapObject.ZaapState zaapState)
  {
    bool highLight = false;
    switch (zaapState)
    {
      case ZaapObject.ZaapState.Normal:
        this.PlayParticles(this.m_highLightParticleSystems, false);
        this.PlayParticles(this.m_openParticleSystems, false);
        this.m_openingAudio.gameObject.SetActive(false);
        this.m_highlightLoopAudio.gameObject.SetActive(false);
        break;
      case ZaapObject.ZaapState.Highlight:
      case ZaapObject.ZaapState.Clicked:
        this.PlayParticles(this.m_highLightParticleSystems, true);
        this.PlayParticles(this.m_openParticleSystems, false);
        highLight = true;
        this.m_openingAudio.gameObject.SetActive(false);
        this.m_highlightLoopAudio.gameObject.SetActive(true);
        break;
      case ZaapObject.ZaapState.Open:
        this.PlayParticles(this.m_highLightParticleSystems, true);
        this.PlayParticles(this.m_openParticleSystems, true);
        highLight = true;
        this.m_openingAudio.gameObject.SetActive(true);
        this.m_highlightLoopAudio.gameObject.SetActive(false);
        break;
    }
    for (int index = 0; index < this.m_animatedSprites.Length; ++index)
    {
      if (this.m_animatedSprites[index].animationCoroutine != null)
        this.StopCoroutine(this.m_animatedSprites[index].animationCoroutine);
      this.m_animatedSprites[index].animationCoroutine = this.StartCoroutine(this.AnimateSpriteColorCoroutine(this.m_animatedSprites[index], highLight));
    }
    if (this.m_scaleCoroutine != null)
      this.StopCoroutine(this.m_scaleCoroutine);
    this.m_scaleCoroutine = this.StartCoroutine(this.ScaleCoroutine(highLight));
    if (this.m_animateGroundFXCoroutine != null)
      this.StopCoroutine(this.m_animateGroundFXCoroutine);
    this.m_animateGroundFXCoroutine = this.StartCoroutine(this.AnimateGroundFXCoroutine(zaapState));
  }

  private IEnumerator AnimateSpriteColorCoroutine(
    Portal.AnimatedSprite animatedSprite,
    bool highLight)
  {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    Color startColor;
    float f;
    if (highLight)
    {
      startColor = animatedSprite.currentColor;
      for (f = 0.0f; (double) f < 1.0; f += Time.deltaTime / this.m_animationDuration)
      {
        animatedSprite.currentColor = Color.Lerp(startColor, animatedSprite.highlightColor, f);
        animatedSprite.spriteRenderer.color = animatedSprite.currentColor;
        yield return (object) wait;
      }
      animatedSprite.currentColor = animatedSprite.highlightColor;
      animatedSprite.spriteRenderer.color = animatedSprite.currentColor;
      startColor = new Color();
    }
    else
    {
      startColor = animatedSprite.currentColor;
      for (f = 0.0f; (double) f < 1.0; f += Time.deltaTime)
      {
        animatedSprite.currentColor = Color.Lerp(startColor, animatedSprite.defaultColor, f);
        animatedSprite.spriteRenderer.color = animatedSprite.currentColor;
        yield return (object) wait;
      }
      animatedSprite.currentColor = animatedSprite.defaultColor;
      animatedSprite.spriteRenderer.color = animatedSprite.currentColor;
      startColor = new Color();
    }
    animatedSprite.animationCoroutine = (Coroutine) null;
  }

  private IEnumerator ScaleCoroutine(bool highLight)
  {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    Vector3 startScale = this.m_currentScale;
    Vector3 endScale = !highLight ? this.m_defaultScale : this.m_highLigthScale;
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime / this.m_animationDuration)
    {
      float t = this.m_scaleCurve.Evaluate(f);
      this.m_currentScale = Vector3.LerpUnclamped(startScale, endScale, t);
      this.m_scaleTransform.localScale = this.m_currentScale;
      yield return (object) wait;
    }
    this.m_currentScale = endScale;
    this.m_scaleTransform.localScale = this.m_currentScale;
    this.m_scaleCoroutine = (Coroutine) null;
  }

  private IEnumerator AnimateGroundFXCoroutine(ZaapObject.ZaapState zaapState)
  {
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    float highLightStartValue = this.m_highLightCurrentValue;
    float openStartValue = this.m_openCurrentValue;
    float highLightEndValue = 0.0f;
    float openEndValue = 0.0f;
    if (zaapState == ZaapObject.ZaapState.Highlight || zaapState == ZaapObject.ZaapState.Clicked)
      highLightEndValue = 1f;
    else if (zaapState == ZaapObject.ZaapState.Open)
    {
      highLightEndValue = 1f;
      openEndValue = 1f;
    }
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime / this.m_groundEffectDuration)
    {
      this.m_highLightCurrentValue = Mathf.Lerp(highLightStartValue, highLightEndValue, f);
      this.m_openCurrentValue = Mathf.Lerp(openStartValue, openEndValue, f);
      this.materialPropertyBlock.SetFloat(this.m_highLightID, this.m_highLightCurrentValue);
      this.materialPropertyBlock.SetFloat(this.m_openID, this.m_openCurrentValue);
      this.m_groundIcon.SetPropertyBlock(this.materialPropertyBlock);
      yield return (object) wait;
    }
    this.m_highLightCurrentValue = highLightEndValue;
    this.m_openCurrentValue = openEndValue;
    this.materialPropertyBlock.SetFloat(this.m_highLightID, this.m_highLightCurrentValue);
    this.materialPropertyBlock.SetFloat(this.m_openID, this.m_openCurrentValue);
    this.m_groundIcon.SetPropertyBlock(this.materialPropertyBlock);
    this.m_animateGroundFXCoroutine = (Coroutine) null;
  }

  private void PlayParticles(ParticleSystem[] particleSystems, bool play)
  {
    if (play)
    {
      for (int index = 0; index < particleSystems.Length; ++index)
        particleSystems[index].Play(false);
    }
    else
    {
      for (int index = 0; index < particleSystems.Length; ++index)
        particleSystems[index].Stop(false);
    }
  }

  [Serializable]
  private class AnimatedSprite
  {
    public SpriteRenderer spriteRenderer;
    public Color defaultColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public Color highlightColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public Color currentColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public Coroutine animationCoroutine;
  }
}
