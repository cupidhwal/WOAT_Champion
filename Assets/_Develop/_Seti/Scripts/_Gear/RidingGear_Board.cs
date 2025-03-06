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

        // 일반
        protected FixedJoint joint;
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
            if (receiver && transducer && propulsor)
                maxSpeed = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
            else maxSpeed = 0f;
        }

        private void OnValidate()
        {
            if (receiver && transducer && propulsor)
                maxSpeed = receiver.Efficiency * transducer.Efficiency * propulsor.Performance;
            else maxSpeed = 0f;
        }
        #endregion

        // 메서드
        public override void RideOn(Actor actor)
        {
            // 쓰러진 라이딩기어를 바르게 놓기
            float yRotation = this.gameObject.transform.localRotation.eulerAngles.y;
            this.gameObject.transform.localRotation = Quaternion.Euler(0, yRotation, 0);

            // 원활한 운전을 위해 리지드바디 회전 고정
            rbGear.constraints = RigidbodyConstraints.FreezeRotation;

            // 플레이어와 라이딩기어의 충돌 무시
            //foreach (var parts in GearColliders)
            //    Physics.IgnoreCollision(playerCollider, parts, true);

            // 라이딩기어의 로컬 좌표 (0, 0, 0)를 월드 좌표로 변환
            Vector3 targetPosition = transform.TransformPoint(new Vector3(0, 0, 0));

            // 플레이어의 로컬 좌표 (0, 0, 0)를 월드 좌표로 변환하고 그 차이를 계산
            Vector3 offset = actor.transform.TransformPoint(new Vector3(0, 0, 0)) - actor.transform.position;

            // 플레이어의 월드 좌표를 보드의 해당 위치로 이동
            actor.transform.position = targetPosition - offset;

            if (joint == null)
            {
                // 고정 조인트를 플레이어와 연결
                joint = actor.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = rbGear;
                joint.autoConfigureConnectedAnchor = false;
            }

            joint.autoConfigureConnectedAnchor = false;         // 조인트 오토 해제
            joint.anchor = new Vector3(0, 0, 0);                // 조인트 주체 위치
            joint.connectedAnchor = new Vector3(0, 0, 0);       // 조인트 표적 위치

            // joint 상태가 변했으므로 이벤트 호출
            //OnStanceChanged?.Invoke();
        }

        public override void RideOff()
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