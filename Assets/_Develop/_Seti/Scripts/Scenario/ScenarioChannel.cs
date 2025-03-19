using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 대화 채널
    /// </summary>
    /// Dialogue System의 새 지평
    /// 유연하고 느슨한 구조, Condition Event의 리스너 체계로 상호작용 강화
    public class ScenarioChannel : MonoBehaviour
    {
        // 필드
        #region Variables
        [SerializeField]
        private List<Actor> actors = new();
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
                actor.Condition.OnStanceChange += () => Signal_StanceChange(actor);
                actor.Condition.OnActionChange += () => Signal_ActionChange(actor);
                actors.Add(actor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Actor"))
            {
                Debug.Log($"Actor : {other.name} 퇴장");

                Actor actor = other.GetComponent<Actor>();
                actor.Condition.OnStanceChange -= () => Signal_StanceChange(actor);
                actor.Condition.OnActionChange -= () => Signal_ActionChange(actor);
                actors.Remove(actor);

                if (actors.Count < 2)
                {
                    actors.Clear();
                    Manager_Channel.Instance.DelChannel(gameObject);
                }
            }
        }
    }
}