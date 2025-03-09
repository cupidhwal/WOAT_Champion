//using UnityEngine;

//namespace Seti
//{
//    /// <summary>
//    /// PlayerEnergy 클래스로부터 정보를 받아 라이딩기어를 통해 에너지를 소비하는 클래스
//    /// RidingGear 계층 클래스가 라이딩기어 오브젝트에 컴포넌트로 설정될 때 반드시 이 클래스도 강제되어야 한다
//    /// </summary>
//    public class RidingEngine : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        private IStrategy_Energy energyStrategy;
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        private void OnDisable() => enabled = true;
//        #endregion

//        // 메서드
//        #region Methods
//        public void SetStrategy(IStrategy_Energy energyStrategy)
//        {
//            this.energyStrategy = energyStrategy;
//        }

//        public void ConsumeEnergy(PlayerEnergy energy)
//        {
//            if (energyStrategy == null)
//            {
//                Debug.LogWarning("Set Energy Strategy");
//                return;
//            }
//            energyStrategy.ConsumeEnergy(energy);
//        }
//        #endregion
//    }
//}