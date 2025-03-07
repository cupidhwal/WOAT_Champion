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
        [Header("DB : Parts")]
        [SerializeField]
        private DB_Receiver receiverDB;
        [SerializeField]
        private DB_Transducer transducerDB;
        [SerializeField]
        private DB_Propulsor propulsorDB;
        #endregion

        // 파츠는 아이템으로 취급 : 파츠 상자 등 인벤토리에 넣을 수 있어야 하니까
        // 파츠 거래
        // 파츠 제작
        // 파츠 탈착

        // 펄 거래
        // 빈 펄 판매
        // 완충 펄 구매

        // 라이프 사이클
        protected override void Awake()
        {
            base.Awake();

            InitializeManager.Instance.Set_Third += Initialize;
        }

        // 메서드
        private void Initialize()
        {

        }
    }
}