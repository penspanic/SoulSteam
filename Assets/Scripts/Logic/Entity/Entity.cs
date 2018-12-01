using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Entity
{
    public enum EntityType
    {
        Undefined = 0,
        Entity,
        Dust,
        Planet,
        Star,
        BlackHole,
    }

    public enum MoveType
    {
        Undefined = 0,
        Holded,     // 
        Linear,
        Curve,
        Cycle,
        Impacted
    }

    public class Entity : MonoBehaviour, IPoolable, Input.ITouchable
    {
        public virtual EntityType Type => EntityType.Entity;
        public override string ToString() => $"{Id}({Serial})";
        public Common.StaticData.EntityInfo Info { get; private set; }
        public bool IsPressed { get; private set; }
        public string Id => _id;
        private string _id;
        public int Serial => _serial;
        private int _serial;
        public int Level => level;
        protected int level;

        public MoveType MoveState = MoveType.Undefined;
        public float impactedGravity;
        public float curveGravity;

        protected Coroutine dragLerpCoroutine;
        protected Vector3 dragPosDiff;
        protected Vector3 dragDestPos;
        protected Vector3 dragDir;

        public virtual void Init(string id, int serial)
        {
            _id = id;
            _serial = serial;
            level = 1;
            name = $"{Type}_{ToString()}";
            IsPressed = false;
            StopAllCoroutines();
            Info = Common.StaticInfo.StaticInfoManager.Instance.EntityInfos[_id];

            // 무작위 자전
            angleRotate.x = 0f;
            angleRotate.y = 0f;
            angleRotate.z = UnityEngine.Random.value;

            affectedEntities.Clear();
        }

        protected virtual void OnChangeLevel()
        {

        }

        #region IPoolable
        public void OnInit()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnRelease()
        {
            gameObject.SetActive(false);
        }
        #endregion
        #region Input.ITouchable

        public virtual void OnPressDown()
        {
            IsPressed = true;
        }

        public virtual void OnPressUp()
        {
            IsPressed = false;
        }

        public virtual void OnStartDrag(Vector3 pos)
        {
            dragLerpCoroutine = StartCoroutine(DragLerpProcess());
            dragPosDiff = transform.position - pos;
        }

        private IEnumerator DragLerpProcess()
        {
            while (true)
            {
                transform.position = Vector3.Lerp(transform.position, dragDestPos, Time.deltaTime * 2f);
                yield return null;
            }
        }

        public virtual void OnDrag(Vector3 pos, Vector3 dir)
        {
            dragDestPos = pos;
            dragDir = dir;
        }

        public virtual void OnEndDrag()
        {
            StopCoroutine(dragLerpCoroutine);
        }

        #endregion


        public Vector3 GetAffectVector()
        {
            return Vector3.zero;
        }

        // 컴포넌트
        [Header("Component")]
        public SpriteRenderer _renderer;
        public TrailRenderer _trail;

        // 이동 파라미터
        [Header("Movement")]
        public Vector3 affectedVector;
        public Vector3 moveDirection;
        public Vector3 moveSpeedRate;     // 이동 가속도
        public float moveSpeedTotal;      // 이동속도 - 실제 이동속도
        public float moveSpeedBase;       // 이동속도 - 기본
        public float moveSpeedLevelRate;  // 이동속도 - 단계비율
        protected delegate void Delegate_Move();
        protected Delegate_Move Move;

        public Dictionary<Entity, float> affectedEntities = new Dictionary<Entity, float>();

        // 자전 파라미터
        [Header("Rotate")]
        public Vector3 angleRotate;
        public float rotateSpeed;

        // 스케일 파라미터
        [Header("Scale")]
        public float scaleBase;         // 기본 크기
        public float scaleRate;         // 단계별 크기 비율

        // 공전 파라미터
        [Header("Settlate")]
        public float cycleNowRange = 1f;
        public float cycleNextRange = 0f;
        public float cyclePrevRange = 0f;
        public int cycleCount = 0;
        public int cycleCountMax = 5;
        public float cycleRotateSpeed = 72f;

        public virtual void ChangeMoveState(Entity hole, MoveType movetype) { }

        // 영역 밖으로 이동
        protected readonly static string LayerMaskWall = "Wall";
        protected RaycastHit2D[] hit;
        public void WallOutReset(Transform hitWall)
        {
            hit = Physics2D.RaycastAll(transform.position, -moveDirection, 100f, 1 << LayerMask.NameToLayer(LayerMaskWall), -1, 1);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform != hitWall)
                {
                    transform.position = hit[i].point;
                }
            }
        }
    }
}