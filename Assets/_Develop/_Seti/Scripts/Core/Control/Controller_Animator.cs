using UnityEngine;

namespace Seti
{
    public class Controller_Animator : MonoBehaviour
    {
        // 필드
        #region Variables
        protected Transform lookTarget;
        #endregion

        // 속성
        #region Properties
        public StateMachine<Controller_Animator> AniMachine { get; private set; }
        public Animator Animator { get; private set; }
        public Actor Actor { get; private set; }
        public Move Move { get; private set; }

        public float MoveInputX { get; set; }
        public float MouseDelta { get; set; }
        #endregion

        // 라이프 사이클
        private void Update()
        {
            // FSM 업데이트
            AniMachine.Update(Time.deltaTime);
        }

        // 메서드
        public void Initialize()
        {
            // 참조
            if (!TryGetComponent<Actor>(out var actor))
                actor = GetComponentInParent<Actor>();
            Actor = actor;

            lookTarget = Actor.transform.Find("Head_Root").GetChild(0);

            // 애니메이션 컨트롤러 초기화
            Animator = GetComponent<Animator>();
            AniMachine = new StateMachine<Controller_Animator>(
                this,
                new AniState_Idle()
            );

            // 상태 추가
            AddStates();
        }

        private void AddStates()
        {
            if (Actor.Controller.BehaviourMap.TryGetValue(typeof(Move), out var moveBehaviour))
            {
                Move = moveBehaviour as Move;
                if (Move.HasStrategy<Move_Normal>() || Move.HasStrategy<Move_Walk>() || Move.HasStrategy<Move_Run>())
                    AniMachine.AddState(new AniState_Move());
            }

            if (Actor.Controller.BehaviourMap.TryGetValue(typeof(Dash), out var _))
                AniMachine.AddState(new AniState_Dash());
        }


        // 애니메이션 IK

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