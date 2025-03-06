using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    public enum Action
    {
        Idle,
        Walk,
        Run,
        Dash,
        Ride
    }

    /// <summary>
    /// Actor 추상 클래스
    /// </summary>
    public abstract class Condition_Actor : MonoBehaviour
    {
        // 필드
        #region Variables
        [Header("Action")]
        [SerializeField]
        private Action currentAction;
        [SerializeField]
        protected bool inAction = false;

        // 일반
        protected Actor actor;
        protected Rigidbody rb;

        // 이벤트
        public UnityAction OnActionChange;
        #endregion

        // 속성
        #region Properties
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

        public void ActionChange(Action action)
        {
            switch (action)
            {
                case Action.Idle:
                    currentAction = Action.Idle;
                    break;

                case Action.Walk:
                    currentAction = Action.Walk;
                    break;

                case Action.Run:
                    currentAction = Action.Run;
                    break;

                case Action.Dash:
                    currentAction = Action.Dash;
                    break;

                case Action.Ride:
                    currentAction = Action.Ride;
                    break;
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