using UnityEngine;

namespace Seti
{
    public abstract class AniState_Base : MonoState<Controller_Animator>
    {
        // 필드
        #region Variables
        // Animator parameter
        protected int OnDash = Animator.StringToHash("OnDash");
        protected int isDeath = Animator.StringToHash("IsDeath");

        // float
        protected int Hash_ForwardSpeed = Animator.StringToHash("ForwardSpeed");
        protected int Hash_VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        protected int Hash_AirborneVerticalSpeed = Animator.StringToHash("AirborneVerticalSpeed");
        protected int Hash_AngleDeltaRad = Animator.StringToHash("AngleDeltaRad");
        protected int Hash_HurtFromX = Animator.StringToHash("HurtFromX");
        protected int Hash_HurtFromY = Animator.StringToHash("HurtFromY");
        protected int Hash_StateTime = Animator.StringToHash("StateTime");
        protected int Hash_FootFall = Animator.StringToHash("FootFall");
        protected int Hash_MouseDelta = Animator.StringToHash("MouseDelta");

        // int
        protected int Hash_RandomIdle = Animator.StringToHash("RandomIdle");
        protected int Hash_RandomMagic = Animator.StringToHash("RandomMagic");

        // bool
        protected int Hash_Grounded = Animator.StringToHash("Grounded");
        protected int Hash_InputDetected = Animator.StringToHash("InputDetected");

        // trigger
        protected int Hash_TimeoutToIdle = Animator.StringToHash("TimeoutToIdle");
        protected int Hash_MeleeAttack = Animator.StringToHash("MeleeAttack");
        protected int Hash_MagicAttack = Animator.StringToHash("MagicAttack");
        protected int Hash_Hurt = Animator.StringToHash("Hurt");
        protected int Hash_Death = Animator.StringToHash("Death");
        protected int Hash_Respawn = Animator.StringToHash("Respawn");
        #endregion

        // 메서드
        public override void Update(float deltaTime)
        {
            if (context.Move != null)
            {
                CurrentSpeed();

                context.Animator.SetFloat(Hash_ForwardSpeed, speed * context.Move.MoveInput.y);
                context.Animator.SetFloat(Hash_VerticalSpeed, speed * context.Move.MoveInput.x);
                context.Animator.SetFloat(Hash_MouseDelta, context.MouseDelta);
            }
        }

        float speed = 0f;
        protected void CurrentSpeed()
        {
            if (context.Actor && context.Actor.Condition.InAction)
            {
                float velovity = context.Actor.Condition.CurrentAction != Action.Idle ? context.Actor.Rate_Movement : 0f;

                float magnification = context.Actor.Condition.CurrentAction == Action.Run ? context.Actor.Magnification_WalkToRun : 1;

                speed = Mathf.Lerp(speed, magnification * velovity, 10f * Time.deltaTime);
            }
        }
    }
}