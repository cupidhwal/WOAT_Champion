using System;
using UnityEngine;

namespace Seti
{
    public abstract class Look_Base : ILookStrategy
    {
        // 필드
        #region Variables
        // 조정값
        protected float mouseSensitivity;     // 마우스 감도

        // 세팅
        protected Actor actor;
        protected Rigidbody rb;               // 플레이어 Rigidbody
        protected Transform headTransform;    // 플레이어의 머리 부분 Transform

        // 일반 필드
        protected float headXRotation;
        protected float headYRotation;
        protected float bodyYRotation;
        #endregion

        // 메서드
        public void Initialize(Actor actor, float mouseSensitivity = 0f)
        {
            this.actor = actor;
            this.mouseSensitivity = mouseSensitivity;

            rb = actor.GetComponent<Rigidbody>();
            headTransform = actor.transform.Find("Head_Root");
        }
        public Type GetStrategyType() => typeof(ILookStrategy);

        public abstract void Look(Vector2 readValue = default);

        // 보드 탑승 직전 플레이어의 위치에 따라 동기화 방향을 결정하는 메서드
        protected float DefineSync()
        {
            if (actor.CurrentGear is RidingGear_Board board)
                return board.BoardDir ? 80f : -80f;
            return 0f;
        }
    }
}