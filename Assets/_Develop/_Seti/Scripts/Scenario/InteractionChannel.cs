using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    /// <summary>
    /// 대화 채널
    /// </summary>
    /// Dialogue System의 새 지평
    /// 유연하고 느슨한 구조, Condition Event의 리스너 체계로 상호작용 강화
    public class InteractionChannel : MonoBehaviour
    {
        // 필드
        #region Variables
        [SerializeField]
        private List<Actor> actors = new();
        private Dictionary<Actor, UnityAction> stanceChangeHandlers = new();
        private Dictionary<Actor, UnityAction> actionChangeHandlers = new();
        #endregion

        // 속성
        #region Properties
        public List<Actor> Actors => actors;
        #endregion

        private void Signal_StanceChange(Actor trigger)
        {
            foreach (var actor in actors)
            {
                if (actor == trigger) continue;
                actor.Accept_StanceChange();
            }
        }

        private void Signal_ActionChange(Actor trigger)
        {
            foreach (var actor in actors)
            {
                if (actor == trigger) continue;
                actor.Accept_ActionChange();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Actor"))
            {
                Debug.Log($"Actor : {other.name} 입장");

                Actor actor = other.GetComponent<Actor>();

                // 직접 참조로 이벤트 핸들러 저장
                void stanceHandler() => Signal_StanceChange(actor);
                void actionHandler() => Signal_ActionChange(actor);

                // Dictionary에 저장하여 정확한 참조 유지
                stanceChangeHandlers[actor] = stanceHandler;
                actionChangeHandlers[actor] = actionHandler;

                // 이벤트 등록
                actor.Condition.OnStanceChange += stanceHandler;
                actor.Condition.OnActionChange += actionHandler;

                actors.Add(actor);

                actor.Condition.IsInteraction = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Actor"))
            {
                Debug.Log($"Actor : {other.name} 퇴장");

                Actor actor = other.GetComponent<Actor>();

                // 저장된 참조를 사용해 이벤트 정확히 해제
                if (stanceChangeHandlers.ContainsKey(actor))
                {
                    actor.Condition.OnStanceChange -= stanceChangeHandlers[actor];
                    stanceChangeHandlers.Remove(actor);
                }
                if (actionChangeHandlers.ContainsKey(actor))
                {
                    actor.Condition.OnActionChange -= actionChangeHandlers[actor];
                    actionChangeHandlers.Remove(actor);
                }

                actors.Remove(actor);

                actor.Condition.IsInteraction = false;

                if (actors.Count < 2)
                {
                    Manager_Channel.Instance.DelChannel(gameObject);
                    actors.Clear();
                }
            }
        }
    }
}