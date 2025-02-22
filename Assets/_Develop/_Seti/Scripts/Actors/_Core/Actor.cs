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
        [SerializeField]
        [HideInInspector]
        protected Condition_Actor condition;
        [SerializeField]
        [HideInInspector]
        protected Controller_Base controller;

        // 일반
        protected Controller_Animator animator;

        // 스탯
        [Header("Current Status")]
        [SerializeField]
        protected float health = 100f;
        [SerializeField]
        protected float attack = 10f;
        [SerializeField]
        protected float defend = 1f;
        [SerializeField]
        protected float rate_Attack = 1f;
        [SerializeField]
        protected float rate_Movement = 2f;

        // 부가 기능
        [Header("CC Status")]
        [SerializeField]
        protected float stagger = 0.5f;   // 경직 시간

        [Header("Variables: Common")]
        [SerializeField]
        protected float mag_WalkToRun = 1.5f;
        [SerializeField]
        protected float attackProgressive = 2.5f;
        #endregion

        // 속성
        #region Properties
        public Blueprint_Actor Blueprint => blueprint;
        public Condition_Actor Condition => condition;
        public Controller_Base Controller => controller;
        public Controller_Animator Controller_Animator => animator;

        public float Health => health;
        public float Attack => attack;
        public float Defend => defend;
        public float Rate_Attack => rate_Attack;
        public float Rate_Movement => rate_Movement;
        public float Magnification_WalkToRun => mag_WalkToRun;  // 걷기/달리기
        #endregion

        // 초기화
        public void Initialize()
        {
            // Check Actor Condition
            condition = GetComponent<Condition_Actor>();
            condition.Initialize();

            // Check Animator Controller
            if (ComponentUtility.TryGetComponentAll<Animator>(transform, out var anim))
            {
                animator = anim.transform.GetComponent<Controller_Animator>();
                if (!animator)
                    animator = anim.transform.gameObject.AddComponent<Controller_Animator>();
            }

            // Define Control
            controller = GetComponent<Controller_Base>();
            controller.Initialize();
        }

        // 추상화
        #region Abstract
        protected abstract Condition_Actor CreateState();
        #endregion

        // 라이프 사이클
        #region Life Cycle
        protected virtual void Awake()
        {
            Initialize();
        }
        #endregion

        // Controller 교체
        #region Switch Controller
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