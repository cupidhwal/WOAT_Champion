using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 매크로멕 시스템 관련 UI 총괄
    /// </summary>
    public class UI_MacroMECH : UI_Target
    {
        // 필드
        #region Variables
        private MacroMECH macroMECH;
        private UI_Detail detail;

        [Header("View : Module")]
        [SerializeField]
        private int codeID;
        [SerializeField]
        private GameObject partsModule;
        [SerializeField]
        private Transform contents;

        private Queue<GameObject> modules = new();
        #endregion

        // 라이프 사이클
        private void OnEnable()
        {
            if (!macroMECH)
                macroMECH = GetComponentInParent<MacroMECH>();

            if (!detail)
                detail = GetComponentInChildren<UI_Detail>();

            AddModule();
        }

        private void OnDisable()
        {
            DelModule();
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
                if (!modules.TryDequeue(out var result))
                {
                    result = Instantiate(partsModule, contents);
                }
                Module module = result.GetComponent<Module>();
                module.SetModule(parts);
            }
        }

        public void DelModule()
        {
            for (int i = 0; i < contents.childCount; i++)
            {
                modules.Enqueue(contents.GetChild(i).gameObject);
            }
        }

        public override void SetModule(Parts parts) => detail.SetModule(parts);
    }
}