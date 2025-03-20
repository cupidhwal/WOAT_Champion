using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    /// <summary>
    /// RidingGear 최상위 클래스
    /// </summary>
    /// 라이딩기어의 주요 기능:
    /// 탑승/하차, 에너지 전환, 운전, 인핸스 모드, 개조, 강화, 조립, 등등...
    /// 1. 타입 (부츠/보드)
    /// 2. 탑승/하차
    /// 3. 파츠
    /// 4. 인핸스 모드
    public abstract class RidingGear : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Core")]
        [SerializeField]
        protected Core_Gear core;

        //[Header("Parts : 집속부")]
        //[SerializeField]
        //protected Receiver receiver;
        //[Header("Parts : 변환부")]
        //[SerializeField]
        //protected Transducer transducer;

        // 일반
        protected Rigidbody rbGear;

        // 이벤트
        protected UnityAction OnSpecUpdate;
        #endregion

        // Spec
        #region Spec
        public bool Parts_Change_Receiver(Receiver receiver)
        {
            Debug.Log("파츠 교체 : 집속부");

            core.receiver = receiver;

            OnSpecUpdate?.Invoke();
            return true;
        }
        public bool Parts_Change_Transducer(Transducer transducer)
        {
            Debug.Log("파츠 교체 : 변환부");

            core.transducer = transducer;

            OnSpecUpdate?.Invoke();
            return true;
        }
        protected abstract void SpecUpdate();
        #endregion

        // 라이프 사이클
        protected virtual void Start()
        {
            // 참조
            rbGear = GetComponent<Rigidbody>();
        }
        protected virtual void OnEnable()
        {
            OnSpecUpdate += SpecUpdate;
        }
        protected virtual void OnDisable()
        {
            OnSpecUpdate -= SpecUpdate;
        }

        // 메서드
        public abstract void RideOn(Actor actor);
        public abstract void RideOff(Actor actor);
        public abstract void EnhanceMode();

        // 하차 방향 정하기
        protected virtual Vector3 OffDirection()
        {
            // RidingBoard와 RidingBoots에서 반드시 Override 할 것
            return Vector3.up; // 기본 하차 방향은 위쪽
        }
    }
}