using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 말풍선 UI 관리
    /// </summary>
    public class UI_Bubbles : UI_Target
    {
        // 필드
        #region Variables
        [Header("Scenario : Bubbles")]
        [SerializeField]
        private GameObject bubblePrefab;
        private readonly Queue<GameObject> bubblePool = new();
        private readonly Dictionary<Actor, GameObject> bubbleMap = new();
        [SerializeField, ReadOnly]
        private int bubbleCount;
        #endregion

        public override void SetTarget(object data)
        {
            throw new System.NotImplementedException();
        }

        public void OpenBubble(Actor actor)
        {
            // 말풍선 꺼내기
            if (!bubblePool.TryDequeue(out var result))
            {
                result = Instantiate(bubblePrefab, transform);
                result.SetActive(false);
            }
            if (!bubbleMap.ContainsKey(actor))
                bubbleMap[actor] = result;

            // 세팅
            Scenario_Unit_Actor unit = actor.GetComponent<Scenario_Unit_Actor>();
            Scenario_Bubble bubble = result.GetComponent<Scenario_Bubble>();
            unit.OnDialogue += bubble.Speak;
            unit.OnNext += bubble.Next;

            // 첫 인사
            Manager_UI.Instance.Scenario.Dialogue.OnStart();

            bubbleCount = bubbleMap.Count;
        }

        public void ExitBubble(Actor actor)
        {
            if (bubbleMap.ContainsKey(actor))
            {
                bubblePool.Enqueue(bubbleMap[actor]);
                bubbleMap.Remove(actor);
            }

            bubbleCount = bubbleMap.Count;
        }
    }
}