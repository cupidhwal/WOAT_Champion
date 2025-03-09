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
        [Header("Parts : 구동부")]
        [SerializeField]
        protected Propulsor_Electronic propulsor;

        [Header("Enhance Mode : Boots")]
        [SerializeField]
        protected EnhanceMode_Boots enhance;

        [Header("Spec : Boots")]
        [SerializeField, ReadOnly]
        private float maxPower;
        #endregion

        // 속성
        public float MaxPower => maxPower;

        // Spec
        #region Spec
        public bool Parts_Change_Propulsor(Propulsor_Electronic propulsor)
        {
            Debug.Log("파츠 교체 : 구동부");
            this.propulsor = propulsor;

            OnSpecUpdate?.Invoke();
            return true;
        }

        public bool Enhance_Change(EnhanceMode_Boots enhance)
        {
            this.enhance = enhance;

            OnSpecUpdate?.Invoke();
            return true;
        }

        protected override void SpecUpdate()
        {
            if (receiver && transducer && propulsor)
                maxPower = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
            else maxPower = 0f;
        }

        private void OnValidate()
        {
            if (receiver && transducer && propulsor)
                maxPower = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
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
            enhance.Activate();
        }
    }
}