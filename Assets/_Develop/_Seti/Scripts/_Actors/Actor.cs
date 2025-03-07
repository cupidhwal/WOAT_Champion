using System.Collections;
using UnityEngine;

namespace Seti
{
    // Actor가 가져야 할 Component
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]

    /// <summary>
    /// Actor의 기본 정의
    /// </summary>
    public abstract class Actor : MonoBehaviour
    {
        // 필드
        #region Variables
        protected IControl control;
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
        //[SerializeField]
        //protected float health = 100f;
        //[SerializeField]
        //protected float attack = 10f;
        //[SerializeField]
        //protected float defend = 1f;
        //[SerializeField]
        //protected float rate_Attack = 1f;
        [SerializeField]
        protected float rate_Movement = 2f;

        [Header("Variables: Common")]
        [SerializeField]
        protected float mag_WalkToRun = 1.5f;
        [SerializeField]
        protected float attackProgressive = 2.5f;

        [Header("Variables: Dash")]
        [SerializeField]
        private float dashSpeed = 20f;
        [SerializeField]
        private float dashCooldown = 1f;
        [SerializeField]
        private float dashDuration = 0.2f;
        #endregion

        // 속성
        #region Properties
        public Blueprint_Actor Blueprint => blueprint;
        public Condition_Actor Condition => condition;
        public Controller_Base Controller => controller;
        public Controller_Animator Controller_Animator => animator;
        public RidingGear CurrentGear => gearCurrent;
        public RidingGear NearGear => gearNear;

        //public float Health => health;
        //public float Attack => attack;
        //public float Defend => defend;
        //public float Rate_Attack => rate_Attack;
        public float Rate_Movement => rate_Movement;
        public float Magnification_WalkToRun => mag_WalkToRun;  // 걷기/달리기

        public float Dash_Speed => dashSpeed;
        public float Dash_Cooldown => dashCooldown;
        public float Dash_Duration => dashDuration;
        #endregion

        // 라이프 사이클
        protected virtual void Start()
        {
            Initialize();
        }

        // 추상화
        protected abstract Condition_Actor CreateState();

        // 초기화
        public void Initialize()
        {
            // Check Controller
            controller = GetComponent<Controller_Base>();

            // Check Actor Condition
            condition = GetComponent<Condition_Actor>();
            condition.Initialize();

            // Check Animator Controller
            animator = GetComponentInChildren<Controller_Animator>();
            animator.Initialize();
        }

        public void SetGear(RidingGear gear) => gearCurrent = gear;

        private void SwitchController()
        {
            switch (blueprint.controlType)
            {
                case ControlType.Input:
                    SwitchControlType(new Control_Input());
                    break;

                case ControlType.AI:
                    SwitchControlType(new Control_FSM());
                    break;
            }
        }

        private void SwitchControlType(IControl newControl)
        {
            if (controller)
            {
                control = controller.GetControlType() as IControl;
                control?.OnExit(this);
            }

            control = newControl;
            control.OnEnter(this);
        }

        // 이벤트 메서드
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Gear"))
            {
                if (ComponentUtility.TryGetComponentAll<RidingGear>(other.transform, out var gear))
                    gearNear = gear;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
        }

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