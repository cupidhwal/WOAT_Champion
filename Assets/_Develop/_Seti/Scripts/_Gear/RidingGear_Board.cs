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

        [Header("Enhance Mode : Board")]
        [SerializeField]
        protected EnhanceMode_Board enhance;

        [Header("Spec : Board")]
        [SerializeField]
        private float maxSpeed;

        private FixedJoint joint;
        #endregion

        // 속성
        public float MaxSpeed => maxSpeed;

        // Spec
        #region Spec
        public bool Parts_Change_Propulsor(Propulsor_Kinetic propulsor)
        {
            Debug.Log("파츠 교체 : 구동부");
            this.propulsor = propulsor;

            OnSpecUpdate?.Invoke();
            return true;
        }

        public bool Enhance_Change(EnhanceMode_Board enhance)
        {
            this.enhance = enhance;

            OnSpecUpdate?.Invoke();
            return true;
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

        // 메서드
        public override void RideOn(Actor actor)
        {
            joint.anchor = actor.GetComponent<Rigidbody>().transform.position;
            joint.connectedAnchor = this.transform.position;
        }

        public override void TakeOff()
        {
            throw new System.NotImplementedException();
        }

        public override void EnhanceMode()
        {
            enhance.Activate();
        }

        // Break
        private void OnJointBreak(float breakForce)
        {

        }
    }
}