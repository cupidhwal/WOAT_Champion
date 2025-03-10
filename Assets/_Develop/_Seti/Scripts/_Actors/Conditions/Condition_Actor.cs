using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    /// <summary>
    /// Actor 추상 클래스
    /// </summary>
    public abstract class Condition_Actor : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Action")]
        [SerializeField]
        private Stance currentStance;
        [SerializeField]
        private Action currentAction;
        [SerializeField]
        protected bool inAction = false;

        // 일반
        protected Actor actor;
        protected Rigidbody rb;

        // 이벤트
        public UnityAction OnStanceChange;
        public UnityAction OnActionChange;
        #endregion

        // 속성
        #region Properties
        public Stance CurrentStance => currentStance;
        public Action CurrentAction => currentAction;
        public bool InAction => inAction;
        public bool IsGrounded { get; protected set; } = true;
        #endregion

        // 라이프 사이클
        protected virtual void Start()
        {
            // 초기화
            Initialize();
        }

        // 메서드
        public virtual void Initialize()
        {
            actor = GetComponent<Actor>();
            rb = GetComponent<Rigidbody>();

            inAction = true;
        }

        public void StanceChange(Stance stance)
        {
            switch (stance)
            {
                case Stance.Normal:
                    currentStance = Stance.Normal;
                    break;

                case Stance.Board:
                    currentStance = Stance.Board;
                    break;

                case Stance.Boots:
                    currentStance = Stance.Boots;
                    break;
            }
            OnStanceChange?.Invoke();
        }

        public void ActionChange(Action action)
        {
            if (currentStance == Stance.Normal)
            {
                currentAction = action;
            }
            else
            {
                currentAction = action switch
                {
                    Action.Idle => Action.Idle,
                    _ => Action.Drive,
                };
            }
            OnActionChange?.Invoke();
        }

        // 이벤트 메서드
        private void OnCollisionChange(Collision collision, bool groundedState)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                IsGrounded = groundedState;
            }
        }
        private void OnCollisionEnter(Collision collision) => OnCollisionChange(collision, true);
        private void OnCollisionStay(Collision collision) => OnCollisionChange(collision, true);
        private void OnCollisionExit(Collision collision) => OnCollisionChange(collision, false);
    }
}