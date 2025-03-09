using System;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Move Behaviour의 Strategy Base
    /// </summary>
    public abstract class Move_Base : IMoveStrategy
    {
        // 필드
        #region Variables
        // 세팅
        protected Actor actor;
        protected Rigidbody rb;
        protected Vector2 lastMoveDirection;

        protected Vector2 dir;
        #endregion

        // 초기화
        public virtual void Initialize(Actor actor)
        {
            this.actor = actor;
            rb = actor.GetComponent<Rigidbody>();
        }

        public Type GetStrategyType() => typeof(IMoveStrategy);

        // QuaterView - World 기준 이동 로직
        public virtual void Move(Vector2 moveInput)
        {
            dir = MoveDirection(moveInput);

            if (!actor ||
                !actor.Condition.InAction)
            {
                QuaterView_Move(Vector2.zero);
                return;
            }

            if (actor is Player player)
                switch (player.View)
                {
                    case ViewType.Follow_Person:
                        Follow_Person_Move(moveInput);
                        break;

                    case ViewType.QuaterView:
                        QuaterView_Move(moveInput);
                        break;
                }
            else
            {
                QuaterView_Move(moveInput);
            }
        }

        protected virtual void QuaterView_Move(Vector2 moveInput)
        {
            //if (actor.Condition.IsAttack || actor.Condition.IsMagic) return;

            Vector3 moveDirection = new(dir.x, 0, dir.y);
            QuaterView_Dir(moveDirection);
        }
        protected void QuaterView_Dir(Vector3 moveDirection)
        {
            float moveEff = actor.Condition.CurrentAction == Action.Run ? actor.Magnification_WalkToRun : 1;
            Vector3 move = moveEff * actor.Rate_Movement * Time.deltaTime * moveDirection.normalized;
            Vector3 QuaterView = Quaternion.Euler(0f, 45f, 0f) * move;

            // Root Motion을 쓰지 않는 경우에만 실행
            if (!actor.Controller_Animator.Animator.applyRootMotion)
                actor.transform.Translate(QuaterView, Space.World);

            Rotation(QuaterView);
        }

        protected void Rotation(Vector3 moveDirection)
        {
            // 이동이 발생할 때만 회전
            if (moveDirection != Vector3.zero)
            {
                // 진행 방향으로 회전
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, targetRotation, actor.Rotation_Sensitivity * Time.deltaTime);
            }
        }

        // Local 기준 이동 로직
        public void Follow_Person_Move(Vector2 moveInput)
        {
            if (rb == null) return;

            Vector2 dir = MoveDirection(moveInput);
            //Vector3 moveDir = new(dir.x, 0f, dir.y);
            Vector3 moveDir = MathF.Abs(dir.y) > 0.2f ? new(0f, 0f, dir.y) : new(dir.x, 0f, 0f);

            Vector3 forward = actor.transform.forward * moveDir.z;
            Vector3 right = actor.transform.right * moveDir.x;

            float speed = actor.Condition.CurrentAction == Action.Run ? actor.Magnification_WalkToRun * actor.Rate_Movement : actor.Rate_Movement;
            Vector3 move = moveDir.magnitude * speed * Time.fixedDeltaTime * (forward + right).normalized;

            if (!actor.Controller_Animator.Animator.applyRootMotion)
                rb.MovePosition(actor.transform.position + move);

            //Rotation(move);
        }

        // 방지턱 보정
        public void GetOverCurb(Collision collision)
        {
            float height = 0;
            if (collision.transform.TryGetComponent<BoxCollider>(out var curb))
            {
                height = curb.size.y * collision.transform.localScale.y / 2 +
                         curb.center.y +
                         collision.transform.position.y -
                         actor.transform.position.y;
            }
            else if (collision.transform.TryGetComponent<MeshCollider>(out var meshCurb))
            {
                ContactPoint contact = collision.contacts[0];
                Bounds bounds = meshCurb.bounds;
                float sqrContactDis = (contact.point - bounds.center).sqrMagnitude;
                float sqrCenterDis = new Vector3(bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2).sqrMagnitude;

                if (sqrContactDis > sqrCenterDis / 2)
                {
                    height = bounds.size.y * collision.transform.localScale.y / 2 +
                             bounds.center.y +
                             collision.transform.position.y -
                             actor.transform.position.y;
                }
            }

            if (height > 0f && height < 0.5f)
                actor.transform.Translate(new Vector3(0, height, 0));
        }

        // 유틸리티
        #region Utilities
        // 공중 제어 금지 보정
        protected Vector2 MoveDirection(Vector2 moveInput)
        {
            if (actor.Condition.IsGrounded)
                lastMoveDirection = moveInput;
            return lastMoveDirection;
        }
        #endregion
    }
}