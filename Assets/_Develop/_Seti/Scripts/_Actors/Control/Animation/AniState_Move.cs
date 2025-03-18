using System;
using UnityEngine;

namespace Seti
{
    public class AniState_Move : AniState_Base
    {
        // 초기화 메서드 - 생성 후 1회 실행
        public override void OnInitialized() { }

        // 상태 전환 시 State Enter에 1회 실행
        public override void OnEnter()
        {
            base.OnEnter();

            //context.Animator.SetBool(isMove, true);
            context.Animator.SetBool(Hash_InputDetected, true);
        }

        // 상태 전환 시 State Exit에 1회 실행
        public override void OnExit()
        {
            base.OnExit();

            //context.Animator.SetBool(isMove, false);
            context.Animator.SetBool(Hash_InputDetected, false);
        }

        // 상태 전환 조건 메서드
        public override Type CheckTransitions()
        {
            switch (context.Actor.Condition.CurrentAction)
            {
                case Action.Idle:
                    return typeof(AniState_Idle);

                case Action.Dash:
                    return typeof(AniState_Dash);

                default:
                    return null;
            }
        }

        // 상태 실행 중
        public override void Update(float deltaTime) => base.Update(deltaTime);
    }
}