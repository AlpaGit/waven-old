// Decompiled with JetBrains decompiler
// Type: CharacterNavMesh
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using Ankama.Animations;
using Ankama.Cube.Configuration;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavMesh : MonoBehaviour
{
  [SerializeField]
  private Animator2D m_animator2d;
  [SerializeField]
  private float m_speedFactor = 1f;
  private NavMeshPath m_navMeshPath;
  protected Sequence m_currentTweenSequence;
  private Vector3 m_originalAnimatorScale;
  private const float MovementSingleCellTraversalTime = 5f;
  private const float MovementMultipleCellTraversalTime = 4f;
  private CharacterNavMesh.AnimInfo[] m_anims = new CharacterNavMesh.AnimInfo[4]
  {
    new CharacterNavMesh.AnimInfo()
    {
      direction = new Vector3(1f, 0.0f, 0.0f),
      scale = new Vector3(-1f, 1f, 1f),
      animSuffix = "5"
    },
    new CharacterNavMesh.AnimInfo()
    {
      direction = new Vector3(-1f, 0.0f, 0.0f),
      scale = new Vector3(-1f, 1f, 1f),
      animSuffix = "1"
    },
    new CharacterNavMesh.AnimInfo()
    {
      direction = new Vector3(0.0f, 0.0f, 1f),
      scale = Vector3.one,
      animSuffix = "5"
    },
    new CharacterNavMesh.AnimInfo()
    {
      direction = new Vector3(0.0f, 0.0f, -1f),
      scale = Vector3.one,
      animSuffix = "1"
    }
  };

  private void Awake()
  {
    ApplicationStarter.InitializeAssetManager();
    this.m_originalAnimatorScale = this.m_animator2d.transform.localScale;
  }

  private IEnumerator Start()
  {
    yield return (object) ApplicationStarter.ConfigureLocalAssetManager();
  }

  private void OnEnable() => this.m_navMeshPath = new NavMeshPath();

  private void Update()
  {
    this.m_animator2d.transform.rotation = Quaternion.Euler(0.0f, 45f, 0.0f);
    RaycastHit hitInfo;
    if (!Input.GetMouseButtonDown(0) || !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
      return;
    NavMesh.CalculatePath(this.transform.position, hitInfo.point with
    {
      x = (float) Mathf.RoundToInt(hitInfo.point.x),
      z = (float) Mathf.RoundToInt(hitInfo.point.z)
    }, -1, this.m_navMeshPath);
    if (this.m_navMeshPath.status != NavMeshPathStatus.PathComplete)
      return;
    this.FollowPath(this.m_navMeshPath);
  }

  private void FollowPath(NavMeshPath path)
  {
    Sequence currentTweenSequence = this.m_currentTweenSequence;
    if (currentTweenSequence != null)
      currentTweenSequence.Kill();
    this.m_currentTweenSequence = DOTween.Sequence();
    Vector3[] corners = this.m_navMeshPath.corners;
    if (corners == null || corners.Length <= 1)
      return;
    Vector3 vector3 = corners[0];
    for (int index = 1; index < corners.Length; ++index)
    {
      Vector3 pos = corners[index];
      pos.x = (float) Mathf.RoundToInt(pos.x);
      pos.z = (float) Mathf.RoundToInt(pos.z);
      if (!(pos == vector3))
      {
        float magnitude = (pos - vector3).magnitude;
        float num = ((double) magnitude <= 2.0 ? 5f : 4f) / (float) this.m_animator2d.frameRate;
        this.m_currentTweenSequence.AppendCallback((TweenCallback) (() => this.OnPathOrientationUpdate(pos)));
        this.m_currentTweenSequence.Append((Tween) this.transform.DOMove(pos, (float) ((double) num * (double) magnitude * (1.0 / (double) this.m_speedFactor))).SetEase<Tweener>(Ease.Linear));
        vector3 = pos;
      }
    }
    this.m_currentTweenSequence.AppendCallback(new TweenCallback(this.RetrunToIdle));
  }

  private void OnPathOrientationUpdate(Vector3 target)
  {
    this.transform.LookAt(target);
    this.UpdateAnim(CharacterNavMesh.AnimType.Run, this.transform.forward);
  }

  private void RetrunToIdle() => this.UpdateAnim(CharacterNavMesh.AnimType.Idle, this.transform.forward);

  private void OnDrawGizmos()
  {
    if (!Application.isPlaying || this.m_navMeshPath == null)
      return;
    Gizmos.color = Color.blue;
    Vector3[] corners = this.m_navMeshPath.corners;
    if (corners == null || corners.Length <= 1)
      return;
    Vector3 vector3 = corners[0];
    Gizmos.DrawSphere(vector3, 0.05f);
    for (int index = 1; index < corners.Length; ++index)
    {
      Gizmos.DrawSphere(corners[index], 0.05f);
      Gizmos.DrawLine(vector3, corners[index]);
      vector3 = corners[index];
    }
  }

  private void UpdateAnim(CharacterNavMesh.AnimType type, Vector3 direction)
  {
    CharacterNavMesh.AnimInfo animInfo = this.m_anims[0];
    float num1 = Vector3.Dot(direction, animInfo.direction);
    for (int index = 1; index < this.m_anims.Length; ++index)
    {
      CharacterNavMesh.AnimInfo anim = this.m_anims[index];
      float num2 = Vector3.Dot(direction, anim.direction);
      if ((double) num2 > (double) num1)
      {
        num1 = num2;
        animInfo = anim;
      }
    }
    this.m_animator2d.transform.localScale = animInfo.scale * this.m_originalAnimatorScale.x;
    this.m_animator2d.transform.rotation = Quaternion.Euler(0.0f, 45f, 0.0f);
    string animName = (type == CharacterNavMesh.AnimType.Idle ? "idle" : "run") + animInfo.animSuffix;
    if (!(animName != this.m_animator2d.animationName))
      return;
    this.m_animator2d.SetAnimation(animName, true, true, true);
  }

  private enum AnimType
  {
    Idle,
    Run,
  }

  private struct AnimInfo
  {
    public Vector3 direction;
    public Vector3 scale;
    public string animSuffix;
  }
}
