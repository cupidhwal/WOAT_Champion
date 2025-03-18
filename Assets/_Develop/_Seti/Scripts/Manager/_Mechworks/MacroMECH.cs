using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 핵심 : 매크로멕
    /// </summary>
    /// 라이딩기어의 제작, 개조, 파츠 거래 등을 총괄
    public class MacroMECH : Singleton<MacroMECH>
    {
        // 필드
        #region Variables
        // 데이터베이스 3종
        [Header("Parts : DB")]
        [SerializeField]
        private DB_Receiver receiverDB;
        [SerializeField]
        private DB_Transducer transducerDB;
        [SerializeField]
        private DB_Propulsor propulsorDB;
        #endregion

        // 속성
        #region Properties
        public DB_Receiver ReceiverDB => receiverDB;
        public DB_Transducer TransducerDB => transducerDB;
        public DB_Propulsor PropulsorDB => propulsorDB;
        #endregion


        // 파츠는 아이템으로 취급 : 파츠 상자 등 인벤토리에 넣을 수 있어야 하니까
        // 파츠 거래
        // 파츠 제작
        // 파츠 탈착

        // 펄 거래
        // 빈 펄 판매
        // 완충 펄 구매

        // 스펙 비교
        #region Spec

        #endregion
    }
}