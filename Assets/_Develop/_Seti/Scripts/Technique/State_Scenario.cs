using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Scenario Object의 기본 상태 정의
    /// </summary>
    public abstract class State_Scenario : State_Object
    {
        protected float elapsed = 0f;

        public override void OnEnter()
        {
            elapsed = 0f;
        }

        public override void OnExit()
        {
            Debug.Log($"{name} 상태 종료!");
        }

        public override void OnUpdate(float deltaTime)
        {
            elapsed += deltaTime;

            // 플레이어가 일정 시간 이상 움직이지 않으면 상태 유지
            if (elapsed > 5f)
            {
                //Debug.Log($"플레이어가 일정 시간 이상 {name}를 유지함!");
            }
        }
    }
}