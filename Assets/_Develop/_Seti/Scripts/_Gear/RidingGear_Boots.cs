using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Boots : 추상 클래스
    /// </summary>
    /// 탑승 로직 : 부모-자식
    /// 주요 기능 : 점프, 대시, 인핸스 모드
    public abstract class RidingGear_Boots : RidingGear
    {
        // 필드
        #region Variables
        [Header("Spec : Boots")]
        [SerializeField, ReadOnly]
        private float maxPower;
        #endregion

        // 속성
        #region Properties
        public Core_Boots Core => core as Core_Boots;
        public float MaxPower => maxPower;
        #endregion

        // Spec
        #region Spec
        public bool Parts_Change_Propulsor(Propulsor_Electronic propulsor)
        {
            Debug.Log("파츠 교체 : 구동부");
            Core.propulsor = propulsor;

            OnSpecUpdate?.Invoke();
            return true;
        }

        public bool Enhance_Change(EnhanceMode_Boots enhance)
        {
            Core.enhance = enhance;

            OnSpecUpdate?.Invoke();
            return true;
        }

        protected override void SpecUpdate()
        {
            if (Core.receiver && Core.transducer && Core.propulsor)
                maxPower = Core.receiver.Efficiency * Core.transducer.Efficiency * Core.propulsor.Performance;
            else maxPower = 0f;
        }

        private void OnValidate()
        {
            if (Core.receiver && Core.transducer && Core.propulsor)
                maxPower = Core.receiver.Efficiency * Core.transducer.Efficiency * Core.propulsor.Performance;
            else maxPower = 0f;
        }
        #endregion

        // 메서드
        public override void RideOn(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        public override void RideOff(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        public override void EnhanceMode()
        {
            Core.enhance.Activate();
        }
    }
}