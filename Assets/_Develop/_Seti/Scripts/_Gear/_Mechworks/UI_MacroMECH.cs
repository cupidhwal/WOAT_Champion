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
        [Header("UI : 집속부")]
        [SerializeField]
        private GameObject UI_Receivers;

        [Header("UI : 변환부")]
        [SerializeField]
        private GameObject UI_Transducers;

        [Header("UI : 구동부")]
        [SerializeField]
        private GameObject UI_Propulsors;
        #endregion

        // 메서드
    }
}