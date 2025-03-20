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
        private UI_MacroMECH_Manager macroMechUI;
        [SerializeField]
        private UI_Dialogue dialogueUI;
        #endregion

        // 속성
        public Stack<GameObject> StackUI => stackUI;
        public UI_Selector SelectorUI => selectorUI;
        public UI_MacroMECH_Manager MacroMECH => macroMechUI;
        public UI_Dialogue DialogueUI => dialogueUI;

        // 라이프 사이클
        private void Start()
        {
            selectorUI.gameObject.SetActive(true);
            selectorUI.ReadyToSelect();

            Manager_Initialize.Instance.Player.Condition.OnActionChange += CloseAll;
        }

        // 메서드
        // Node Selector 실행
        public void Selector(Type_Interaction[] Interactions)
        {
            if (selectorUI.gameObject.activeSelf) return;

            selectorUI.gameObject.SetActive(true);
            selectorUI.Open_Node(Interactions);
            stackUI.Push(selectorUI.gameObject);
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