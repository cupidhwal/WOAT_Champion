using System;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Jump Behaviour�� Strategy Base
    /// </summary>
    public abstract class Jump_Base : IJumpStrategy
    {
        // �ʵ�
        #region Variables
        // ����
        protected float jumpForce;
        protected Actor actor;
        protected Rigidbody rb;     // �÷��̾� Rigidbody
        #endregion

        // �޼���
        #region Methods
        public void Initialize(Actor actor, float jumpForce)
        {
            this.jumpForce = jumpForce;
            this.actor = actor;
            rb = actor.GetComponent<Rigidbody>();
        }

        public Type GetStrategyType() => typeof(IJumpStrategy);

        // �÷��̾��� �⺻ ���� �޼���
        public abstract void Jump();
        #endregion
    }
}