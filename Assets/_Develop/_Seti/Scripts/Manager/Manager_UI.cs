using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    public class Manager_UI : Singleton<Manager_UI>
    {
        // 필드
        #region Variables
        // 일반
        private readonly Stack<GameObject> stackUI = new();

        [Header("Selector")]
        [SerializeField]
        private UI_Selector selectorUI;

        [Header("UI")]
        [SerializeField]
        private UI_Scenario scenarioUI;
        [SerializeField]
        private UI_Trade_Root tradeUI;
        [SerializeField]
        private UI_MacroMECH_Root macroMechUI;
        #endregion

        // 속성
        public UI_Selector SelectorUI => selectorUI;
        public UI_Scenario Scenario => scenarioUI;
        public UI_Trade_Root Trade => tradeUI;
        public UI_MacroMECH_Root MacroMECH => macroMechUI;

        // 라이프 사이클
        private void Start()
        {
            Manager_Initialize.Instance.Player.Condition.OnActionChange += CloseAll;
        }

        // Node Selector 실행
        public void Selector(Type_Interaction[] Interactions)
        {
            if (selectorUI.gameObject.activeSelf) return;

            selectorUI.Open_Node(Interactions);
        }

        public void Open(GameObject selected)
        {
            selected.SetActive(true);
            stackUI.Push(selected);
        }

        public void Close()
        {
            if (stackUI.Count > 0)
            {
                GameObject temp = stackUI?.Pop();
                if (temp == selectorUI.gameObject)
                {
                    selectorUI.Close();
                }
                temp.SetActive(false);

                if (stackUI.Count == 0)
                {
                    Manager_Initialize.Instance.Player.Condition.InteractionChange(Interaction.Idle);
                }
            }
        }

        private void CloseAll()
        {
            int count = stackUI.Count;
            while (count > 0)
            {
                Close();
                count--;
            }
        }
    }
}