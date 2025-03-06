using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Board : 추상 클래스
    /// </summary>
    /// 탑승 로직 : 조인트
    /// 주요 기능 : 운전, 충돌 사고, 인핸스 모드
    public abstract class RidingGear_Board : RidingGear
    {
        // 필드
        #region Variables
        [Header("Parts : 구동부")]
        [SerializeField]
        protected Propulsor_Kinetic propulsor;

        [Header("Spec : Board")]
        [SerializeField]
        private float maxSpeed;
        #endregion

        // 속성
        public float MaxSpeed => maxSpeed;

        // Spec
        #region Spec
        public override bool Parts_Change_Propulsor(Propulsor propulsor)
        {
            bool succeed = false;
            switch (propulsor)
            {
                case Propulsor_Electronic:
                    Debug.Log("보드 타입 라이딩기어는 전력 방출 타입 구동부를 사용할 수 없습니다.");
                    succeed = false;
                    break;

                case Propulsor_Kinetic:
                    Debug.Log("파츠 교체 : 구동부");
                    this.propulsor = propulsor as Propulsor_Kinetic;
                    succeed = true;
                    break;
            }

            OnSpecUpdate?.Invoke();
            return succeed;
        }
        protected override void SpecUpdate()
        {
            maxSpeed = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
        }
        private void OnValidate()
        {
            maxSpeed = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
        }
        #endregion
    }
}