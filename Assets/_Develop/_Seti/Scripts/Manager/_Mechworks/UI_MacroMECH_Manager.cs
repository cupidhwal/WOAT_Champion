using UnityEngine;

namespace Seti
{
    public class UI_MacroMECH_Manager : UI_Root
    {
        // 필드
        #region Variables
        [Header("Parts : UI")]
        [SerializeField]
        private GameObject UI_Receivers;
        [SerializeField]
        private GameObject UI_Transducers;
        [SerializeField]
        private GameObject UI_Propulsors;

        [Header("임시")]
        [SerializeField]
        private RidingGear gear;
        public RidingGear Gear => gear;
        #endregion

        // 라이프 사이클
        private void Start()
        {
            ui_Parts.Add(UI_Receivers);
            ui_Parts.Add(UI_Transducers);
            ui_Parts.Add(UI_Propulsors);
        }

        // 메서드
        public GameObject OpenUI(int index)
        {
            GameObject temp = index switch
            {
                0 => UI_Receivers,
                1 => UI_Transducers,
                2 => UI_Propulsors,
                _ => null,
            };
            if (!temp.activeSelf)
            {
                temp.SetActive(true);
            }
            return temp;
        }
    }
}