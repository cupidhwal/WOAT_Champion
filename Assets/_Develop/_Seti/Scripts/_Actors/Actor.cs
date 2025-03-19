using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    // Actor가 가져야 할 Component
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    /// <summary>
    /// Actor의 기본 정의
    /// </summary>
    public abstract class Actor : Character
    {
        // 필드
        #region Variables
        [Header("Self Definition")]
        [SerializeField]
        protected Blueprint_Actor blueprint;
        protected Condition_Actor condition;
        protected Controller_Base controller;
        protected Controller_Animator animator;

        [Header("Riding Gear")]
        [SerializeField]
        protected RidingGear gearCurrent;
        [SerializeField]
        protected RidingGear gearNear;

        // 스탯
        [Header("Current Status")]
        [SerializeField]
        protected float rate_Movement = 2f;

        [Header("Variables: Common")]
        [SerializeField]
        protected float mag_WalkToRun = 1.5f;
        [SerializeField]
        protected float rotation_Sense = 5f;

        [Header("Variables: Dash")]
        [SerializeField]
        protected float dashSpeed = 20f;
        [SerializeField]
        protected float dashCooldown = 1f;
        [SerializeField]
        protected float dashDuration = 0.2f;

        // 일반
        protected IControl control;

        // 이벤트
        public UnityAction<Actor> OnMeetAnother;
        #endregion

        // 속성
        #region Properties
        public Blueprint_Actor Blueprint => blueprint;
        public Condition_Actor Condition
        {
            get
            {
                if (!condition)
                {
                    condition = GetComponent<Condition_Actor>();
                    condition.Initialize();
                }
                return condition;
            }
        }
        public Controller_Base Controller
        {
            get
            {
                if (!controller)
                    controller = GetComponent<Controller_Base>();
                return controller;
            }
        }
        public Controller_Animator Controller_Animator => animator;
        public RidingGear CurrentGear => gearCurrent;
        public RidingGear NearGear => gearNear;
        public float Rate_Movement => rate_Movement;
        public float Magnification_WalkToRun => mag_WalkToRun;  // 걷기/달리기
        public float Rotation_Sensitivity => rotation_Sense;

        public float Dash_Speed => dashSpeed;
        public float Dash_Cooldown => dashCooldown;
        public float Dash_Duration => dashDuration;
        #endregion

        // 라이프 사이클
        protected virtual void Start()
        {
            // Check Animator Controller
            animator = GetComponentInChildren<Controller_Animator>();
            animator.Initialize();

            // 초기화
            Manager_Channel.Instance.Register(this);
        }

        // 추상화
        protected abstract Condition_Actor CreateState();

        public void Accept_StanceChange()
        {
            Debug.Log("다른 액터의 상태 변화 수신!");
        }

        public void Accept_ActionChange()
        {
            Debug.Log("다른 액터의 행동 변화 수신!");
        }

        public void SetGear(RidingGear gear) => gearCurrent = gear;

        // 이벤트 메서드
        #region Event Methods
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Actor>(out var otherActor))
            {
                Debug.Log($"{name}이(가) {otherActor.name}을(를) 만났다!");
                OnMeetAnother?.Invoke(otherActor);
            }

            if (other.TryGetComponent<RidingGear>(out var gear))
            {
                gearNear = gear;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<RidingGear>())
            {
                gearNear = null;
            }
        }
        #endregion

        // 유틸리티
        #region Utilities
        public void CoroutineExecutor(IEnumerator cor)
        {
            if (cor != null)
                StopCoroutine(cor);
            StartCoroutine(cor);
        }
        public void CoroutineStopper() => StopAllCoroutines();
        #endregion
    }
}