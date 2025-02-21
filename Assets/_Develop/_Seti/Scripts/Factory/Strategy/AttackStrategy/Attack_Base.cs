using System;

namespace Seti
{
    /// <summary>
    /// Attack Behaviour의 Strategy Base
    /// </summary>
    public abstract class Attack_Base : IAttackStrategy
    {
        // 필드
        #region Variables
        // 세팅
        protected Actor actor;
        protected Condition_Actor condition;
        #endregion

        // 인터페이스
        #region Interface
        // 초기화
        public void Initialize(Actor actor, float power = 10f)
        {
            this.actor = actor;
            condition = actor.Condition;
        }
        public Type GetStrategyType() => typeof(IAttackStrategy);
        public virtual void Attack() => condition.IsAttack = true;
        #endregion

        // 메서드
        #region Methods
        #endregion
    }
}