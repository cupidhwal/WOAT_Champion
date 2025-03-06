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

        [Header("Spec : Boots")]
        [SerializeField]
        private float maxPower;
        #endregion

        // 속성
        public float MaxPower => maxPower;

        // Spec
        #region Spec
        public override bool Parts_Change_Propulsor(Propulsor propulsor)
        {
            bool succeed = false;
            switch (propulsor)
            {
                case Propulsor_Electronic:
                    Debug.Log("파츠 교체 : 구동부");
                    this.propulsor = propulsor as Propulsor_Electronic;
                    succeed = true;
                    break;

                case Propulsor_Kinetic:
                    Debug.Log("부츠 타입 라이딩기어는 역학 구동 타입 구동부를 사용할 수 없습니다.");
                    succeed = false;
                    break;
            }

            OnSpecUpdate?.Invoke();
            return succeed;
        }
        protected override void SpecUpdate()
        {
            maxPower = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
        }
        private void OnValidate()
        {
            maxPower = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
        }
        #endregion
    }
}