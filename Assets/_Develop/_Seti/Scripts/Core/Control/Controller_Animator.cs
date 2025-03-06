using UnityEngine;

namespace Seti
{
    public class Controller_Animator : MonoBehaviour
    {
        // 필드
        #region Variables
        [SerializeField]
        protected Transform lookTarget;

        // 컴포넌트
        protected Controller_Base controller;

        [SerializeField]
        protected float forwardSpeed;
        #endregion

        // 속성
        #region Properties
        public StateMachine<Controller_Animator> AniMachine { get; private set; }
        public Animator Animator { get; private set; }
        public Actor Actor { get; private set; }

        public float MoveSpeed { get; private set; }
        #endregion

        // 라이프 사이클
        #region Life Cycle
        protected virtual void Start()
        {
            // 참조
            if (!TryGetComponent<Actor>(out var actor))
                actor = GetComponentInParent<Actor>();
            Actor = actor;

            if (!TryGetComponent<Controller_Base>(out var control))
                control = GetComponentInParent<Controller_Base>();
            controller = control;

            // 애니메이션 컨트롤러 초기화
            Animator = GetComponent<Animator>();
            AniMachine = new StateMachine<Controller_Animator>(
                this,
                new AniState_Idle()
            );

            // 상태 추가
            AddStates();
        }

        private void Update()
        {
            MoveSpeed = CurrentSpeed();

            // FSM 업데이트
            AniMachine.Update(Time.deltaTime);
        }
        #endregion

        // 메서드
        private void AddStates()
        {
            if (controller.BehaviourMap.TryGetValue(typeof(Move), out var moveBehaviour))
            {
                Move move = moveBehaviour as Move;
                if (move.HasStrategy<Move_Normal>() || move.HasStrategy<Move_Walk>() || move.HasStrategy<Move_Run>())
                    AniMachine.AddState(new AniState_Move());
            }

            if (controller.BehaviourMap.TryGetValue(typeof(Dash), out var _))
                AniMachine.AddState(new AniState_Dash());
        }

        protected float CurrentSpeed()
        {
            if (Actor && Actor.Condition.InAction)
            {
                if (Actor.Condition.CurrentAction != Action.Idle)
                    forwardSpeed = Mathf.Lerp(forwardSpeed, Actor.Rate_Movement, 20f * Time.deltaTime);
                else
                    forwardSpeed = forwardSpeed > 0.01f ? Mathf.Lerp(forwardSpeed, 0f, 10f * Time.deltaTime) : 0f;

                float moveEff = Actor.Condition.CurrentAction != Action.Run ? Actor.Magnification_WalkToRun : 1;
                return moveEff * forwardSpeed;
            }
            return 0f;
        }

        public void OnAnimatorIK(int layerIndex)
        {
            if (Animator)
            {
                // IK 활성화
                Animator.SetLookAtWeight(1.0f); // 값이 클수록 강하게 바라봄
                Animator.SetLookAtPosition(lookTarget.position);
            }
        }
    }
}