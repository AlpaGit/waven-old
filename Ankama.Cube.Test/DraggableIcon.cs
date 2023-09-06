// Decompiled with JetBrains decompiler
// Type: DraggableIcon
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DraggableIcon : MonoBehaviour
{
  public RectTransform trsf;
  public RectTransform FXtrsf;
  [SerializeField]
  private Image image;
  [SerializeField]
  private Image highLightImage;
  [SerializeField]
  private Image selectedImage;
  [SerializeField]
  private float moveSpeed;
  [SerializeField]
  private float rotationSpeed;
  [SerializeField]
  private float scaleSpeed;
  [SerializeField]
  private float selectedSpeed;
  [SerializeField]
  private float highlightSpeed;
  [SerializeField]
  private float noiseAmount;
  [SerializeField]
  private float noiseSpeed;
  private Vector3 targetPosition;
  private Vector3 targetScale;
  private float targetRotation;
  private Vector2 targetPivot;
  private float currentRotation;
  [HideInInspector]
  public Vector3 startPosition;
  private float targetSelected;
  private float currentSelected;
  private float targetHighlight;
  private float currentHighlight;

  private void OnEnable()
  {
    this.startPosition = this.trsf.anchoredPosition3D;
    this.ResetObject();
  }

  private void ResetObject()
  {
    this.currentRotation = 0.0f;
    this.targetPosition = this.startPosition;
    this.targetRotation = 0.0f;
    this.targetScale = new Vector3(1f, 1f, 1f);
    this.targetPivot = new Vector2(0.5f, 0.5f);
    this.currentSelected = 0.0f;
    this.targetSelected = this.currentSelected;
    this.currentHighlight = 1f;
    this.targetHighlight = this.currentHighlight;
    this.trsf.anchoredPosition3D = this.targetPosition;
    this.trsf.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
    this.trsf.localScale = this.targetScale;
    this.trsf.pivot = this.targetPivot;
  }

  public void SetNewTarget(Vector3 position) => this.targetPosition = position;

  public void SetNewRotation(float angle)
  {
    this.targetRotation = angle;
    Debug.Log((object) angle);
  }

  public void SetNewScale(Vector3 scale) => this.targetScale = scale;

  public void SetNewPivot(Vector3 pivot) => this.targetPivot = (Vector2) pivot;

  public void SetNewSelected(float value) => this.targetSelected = value;

  public void SetNewHighLight(float value) => this.targetHighlight = value;

  private void Update()
  {
    this.trsf.anchoredPosition3D = Vector3.Lerp(this.trsf.anchoredPosition3D, this.targetPosition, this.moveSpeed * Time.deltaTime);
    this.currentRotation = Mathf.Lerp(this.currentRotation, this.targetRotation, this.rotationSpeed * Time.deltaTime);
    this.trsf.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, this.currentRotation));
    this.trsf.localScale = Vector3.Lerp(this.trsf.localScale, this.targetScale, this.scaleSpeed * Time.deltaTime);
    this.trsf.pivot = Vector2.Lerp(this.trsf.pivot, this.targetPivot, this.moveSpeed * Time.deltaTime);
    this.currentSelected = Mathf.Lerp(this.currentSelected, this.targetSelected, this.selectedSpeed * Time.deltaTime);
    this.selectedImage.color = new Color(1f, 1f, 1f, this.currentSelected);
    this.currentHighlight = Mathf.Lerp(this.currentHighlight, this.targetHighlight, this.highlightSpeed * Time.deltaTime);
    this.highLightImage.color = new Color(this.highLightImage.color.r, this.highLightImage.color.g, this.highLightImage.color.b, this.targetHighlight);
  }

  public void LaunchSpell() => this.StartCoroutine(this.LaunchSpellCoroutine());

  private IEnumerator LaunchSpellCoroutine()
  {
    DraggableIcon draggableIcon = this;
    draggableIcon.image.raycastTarget = false;
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    Vector3 pos = draggableIcon.trsf.anchoredPosition3D;
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime * 2f)
    {
      draggableIcon.image.color = new Color(1f, 1f, 1f, 1f - f);
      float x = Mathf.PerlinNoise(f * draggableIcon.noiseSpeed, 0.0f) - 0.5f;
      float y = Mathf.PerlinNoise(f * draggableIcon.noiseSpeed, 0.5f) - 0.5f;
      draggableIcon.trsf.anchoredPosition3D = pos + new Vector3(x, y, 0.0f) * f * draggableIcon.noiseAmount;
      yield return (object) wait;
    }
    yield return (object) new WaitForTime(2f);
    draggableIcon.StartCoroutine(draggableIcon.RespawnCoroutine());
  }

  private IEnumerator RespawnCoroutine()
  {
    this.ResetObject();
    WaitForEndOfFrame wait = new WaitForEndOfFrame();
    for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime * 2f)
    {
      this.image.color = new Color(1f, 1f, 1f, f);
      yield return (object) wait;
    }
    this.image.raycastTarget = true;
  }
}
