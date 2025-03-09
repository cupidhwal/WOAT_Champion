using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    public class Manager_UI : Singleton<Manager_UI>
    {
        // 필드
        #region Variables
        // 일반
        private readonly Stack<GameObject> openUIs = new();

        [Header("UI")]
        [SerializeField]
        private MacroMECH macroMechUI;
        #endregion

        // 라이프 사이클
        private void Start()
        {
            Open(Type_UI.MacroMECH_Receiver);
            Open(Type_UI.MacroMECH_Transducer);
            Open(Type_UI.MacroMECH_Propulsor);
        }

        // 메서드
        public void Open(Type_UI type)
        {
            GameObject temp = type switch
            {
                Type_UI.MacroMECH_Receiver => macroMechUI.OpenUI(0),
                Type_UI.MacroMECH_Transducer => macroMechUI.OpenUI(1),
                Type_UI.MacroMECH_Propulsor => macroMechUI.OpenUI(2),
                _ => null
            };
            if (temp)
            {
                openUIs.Push(temp);
            }
        }

        public void Close()
        {
            if (openUIs.Count > 0)
            {
                GameObject temp = openUIs?.Pop();
                temp.SetActive(false);
            }
        }
    }
}