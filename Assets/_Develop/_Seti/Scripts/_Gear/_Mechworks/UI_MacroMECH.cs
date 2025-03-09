using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 매크로멕 시스템 관련 UI 총괄
    /// </summary>
    public class UI_MacroMECH : MonoBehaviour
    {
        // 필드
        #region Variables
        private MacroMECH macroMECH;

        [Header("Variables")]
        [SerializeField]
        private int codeID;
        [SerializeField]
        private GameObject partsModule;
        [SerializeField]
        private Transform contents;
        #endregion

        // 라이프 사이클
        private void OnEnable()
        {
            if (!macroMECH)
                macroMECH = GetComponentInParent<MacroMECH>();

            AddModule();
        }

        // 메서드
        public void AddModule()
        {
            IEnumerable<Parts> partsList = codeID switch
            {
                0 => macroMECH.ReceiverDB.receivers,
                1 => macroMECH.TransducerDB.transducers,
                2 => macroMECH.PropulsorDB.propulsors,
                _ => null
            };
            foreach (var parts in partsList)
            {
                if (partsModule)
                {
                    GameObject moduleObj = Instantiate(partsModule, contents);
                    Module module = moduleObj.GetComponent<Module>();
                    module.SetModule(parts);
                }
            }
        }

        public void DelModule()
        {

        }
    }
}