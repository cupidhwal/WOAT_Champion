using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        private MacroMECH macroMechUI;
        #endregion

        // 라이프 사이클
        private void Start()
        {
            selectorUI.gameObject.SetActive(true);
            selectorUI.ReadyToSelect();
        }

        // 메서드
        public void Selector(Type_AI type)
        {
            if (selectorUI.gameObject.activeSelf) return;

            UI_Root root = type switch
            {
                Type_AI.MacroMECH => macroMechUI,
                _ => macroMechUI
            };

            selectorUI.gameObject.SetActive(true);
            selectorUI.Open(root);
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
            }
        }
    }
}