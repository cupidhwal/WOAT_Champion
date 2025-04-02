using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Root : Mark, Scenario
    /// </summary>
    public class UI_Scenario : UI_Root
    {
        // 필드
        [Header("Scenario : Common")]
        [SerializeField]
        private State_Scenario_Dialogue dialogueState;
        [SerializeField]
        private UI_Bubbles bubbles;

        // 속성
        public State_Scenario_Dialogue Dialogue => dialogueState;
        public UI_Bubbles Bubbles
        {
            get
            {
                if (!bubbles)
                    bubbles = GetComponentInChildren<UI_Bubbles>();

                return bubbles;
            }
        }
    }
}