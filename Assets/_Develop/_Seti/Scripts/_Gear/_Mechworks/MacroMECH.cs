using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 라이딩기어의 제작, 개조, 파츠 거래 등을 총괄
    /// </summary>
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

        [Header("Parts : UI")]
        [SerializeField]
        private GameObject UI_Receivers;
        [SerializeField]
        private GameObject UI_Transducers;
        [SerializeField]
        private GameObject UI_Propulsors;
        #endregion

        // 속성
        public DB_Receiver ReceiverDB => receiverDB;
        public DB_Transducer TransducerDB => transducerDB;
        public DB_Propulsor PropulsorDB => propulsorDB;

        // 파츠는 아이템으로 취급 : 파츠 상자 등 인벤토리에 넣을 수 있어야 하니까
        // 파츠 거래
        // 파츠 제작
        // 파츠 탈착

        // 펄 거래
        // 빈 펄 판매
        // 완충 펄 구매

        // 라이프 사이클

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