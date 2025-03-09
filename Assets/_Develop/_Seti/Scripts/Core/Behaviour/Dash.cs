using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seti
{
    public class Dash : IBehaviour
    {
        // 필드
        #region Variables
        // 행동 관리
        private Actor actor;
        private Move move;
        private Vector3 moveDirection = Vector3.zero;
        private bool canDash = false;
        #endregion

        // 인터페이스
        public void Initialize(Actor actor)
        {
            this.actor = actor;
        }
        public Type GetBehaviourType() => typeof(Dash);

        // 이벤트 핸들러
        public void OnDashStarted(InputAction.CallbackContext _) => OnDash();

        // 메서드
        private void OnDash()
        {
            if (actor.Condition.InAction && 
                actor.Condition.IsGrounded && 
                actor.Condition.CurrentAction != Action.Dash)
                actor.CoroutineExecutor(Dash_Cor());
        }

        // 반복기
        #region Coroutines
        private IEnumerator Dash_Cor()
        {
            if (actor.Controller.BehaviourMap.TryGetValue(typeof(Move), out var moveBehaviour))
            {
                move = moveBehaviour as Move;
            }
            else
            {
                Debug.Log("Dash Behaviour는 Move Behaviour와 함께 사용해야 합니다.");
                yield break;
            }

            // 대시 중 충돌 무시
            actor.CoroutineExecutor(GameUtility.Timer_Collision(actor.transform,
                                                                LayerMask.NameToLayer("Actor"),
                                                                actor.Dash_Duration));

            // 대시 기능
            actor.CoroutineExecutor(Dash_Excute(move.MoveInput));

            // 대시 사용 가능
            canDash = false;
            yield return new WaitForSeconds(actor.Dash_Cooldown);
            canDash = true;

            yield break;
        }

        private IEnumerator Dash_Excute(Vector2 moveInput)
        {
            if (!canDash) yield break;

            // 대시 기능
            //if (!isDashing)    // 대시 중이 아닐 때에만 방향 갱신
            //{
            //    //moveDirection = (moveInput == Vector2.zero) ?
            //    //                player.transform.forward :
            //    //                new(moveInput.x, 0, moveInput.y);

            //    // 진행 방향으로 회전
            //    //player.transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);


            //    isDashing = true;
            //}

            // 대시 시작
            actor.Condition.ActionChange(Action.Dash);
            moveDirection = actor.transform.forward;

            // 초기 속도 설정
            float elapsedTime = 0f;
            float currentSpeed = 0f;
            while (actor.Condition.InAction && elapsedTime < actor.Dash_Duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / actor.Dash_Duration;

                // Ease In-Out 적용
                currentSpeed = elapsedTime > (actor.Dash_Duration / 4f) ? Mathf.Lerp(currentSpeed, actor.Dash_Speed, Mathf.SmoothStep(0f, 1f, t)) : 0f;
                actor.transform.Translate(currentSpeed * Time.deltaTime * moveDirection, Space.World);

                yield return null;
            }

            // 대시 끝
            if (move.MoveInput != Vector2.zero)
            {
                if (move.IsRunning)
                    actor.Condition.ActionChange(Action.Run);
                else actor.Condition.ActionChange(Action.Walk);
            }
            else
            {
                actor.Condition.ActionChange(Action.Idle);
            }
            
            yield break;
        }
        #endregion
    }
}