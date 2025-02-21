using System;
using System.Threading;

namespace Seti
{
    public class AniState_Stagger : AniState_Base
    {
        // 오버라이드
        #region Override
        // 초기화 메서드 - 생성 후 1회 실행
        public override void OnInitialized() { }

        // 상태 전환 시 State Enter에 1회 실행
        public override void OnEnter()
        {
            base.OnEnter();
            context.Animator.SetTrigger(Hash_Hurt);
            context.Animator.SetFloat(Hash_HurtFromX, context.Actor.Condition.HitDirection.x);
            context.Animator.SetFloat(Hash_HurtFromY, context.Actor.Condition.HitDirection.z);
            context.Actor.Controller_Animator.CantMoveDurAtk();
            context.currentState = AniState.Stagger;
        }

        // 상태 전환 시 State Exit에 1회 실행
        public override void OnExit()
        {
            base.OnExit();
            context.Actor.Controller_Animator.CanMoveAfterAtk();
        }

        // 상태 전환 조건 메서드
        public override Type CheckTransitions()
        {
            if (context.Actor.Condition.IsDead)
                return typeof(AniState_Die);

            if (!context.Actor.Condition.IsStagger && !context.Actor.Condition.IsMove)
                return typeof(AniState_Idle);

            if (!context.Actor.Condition.IsStagger && context.Actor.Condition.IsMove)
                return typeof(AniState_Move);

            if (!context.Actor.Condition.IsStagger && Condition_Player.IsDash)
                return typeof(AniState_Dash);

            if (!context.Actor.Condition.IsStagger && context.Actor.Condition.IsAttack)
                return typeof(AniState_Attack_Melee);

            return null;
        }

        // 상태 실행 중
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
        #endregion
    }
}