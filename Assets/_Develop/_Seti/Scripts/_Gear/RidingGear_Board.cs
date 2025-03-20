using System;
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
        [Header("Spec : Board")]
        [SerializeField]
        protected bool onPower = false;
        [SerializeField, ReadOnly]
        protected float maxSpeed;
        [SerializeField, ReadOnly]
        protected float turnSpeed;
        [SerializeField, ReadOnly]
        protected float tiltSpeed;
        [SerializeField, ReadOnly]
        protected float reverseSpeed;
        [SerializeField, ReadOnly]
        protected float acceleration;
        [SerializeField, ReadOnly]
        protected float momentum;
        [SerializeField, ReadOnly]
        protected float downForce;
        [SerializeField, ReadOnly]
        protected float brakeCoefficient;

        [Header("Direction")]
        [SerializeField]
        protected BoardDirection boardDirection;

        // 일반
        protected FixedJoint joint;
        #endregion

        // 속성
        #region Properties
        public Core_Board Core => core as Core_Board;
        public bool OnPower => onPower;
        public bool BoardDir
        {
            get
            {
                bool isRight = boardDirection switch
                {
                    BoardDirection.Right => true,
                    BoardDirection.Left => false,
                    _ => false
                };
                return isRight;
            }
        }
        public float MaxSpeed => maxSpeed;
        public float TurnSpeed => turnSpeed;
        public float TiltSpeed => tiltSpeed;
        public float ReverseSpeed => reverseSpeed;
        public float Acceleration => acceleration;
        public float Momentum => momentum;
        public float DownForce => downForce;
        public float BrakeCoefficient => brakeCoefficient;
        #endregion

        // Spec
        #region Spec
        public bool Parts_Change_Propulsor(Propulsor_Kinetic propulsor)
        {
            Debug.Log("파츠 교체 : 구동부");
            Core.propulsor = propulsor;

            OnSpecUpdate?.Invoke();
            return true;
        }

        public bool Enhance_Change(EnhanceMode_Board enhance)
        {
            Core.enhance = enhance;

            OnSpecUpdate?.Invoke();
            return true;
        }

        private void Spec()
        {
            if (Core.receiver && Core.transducer && Core.propulsor)
            {
                maxSpeed = Core.receiver.Efficiency * Core.transducer.Efficiency * Core.propulsor.Performance;
                turnSpeed = Core.propulsor.Agility;
                tiltSpeed = Core.propulsor.Agility + 1;
                reverseSpeed = maxSpeed * 0.4f;
                acceleration = Core.transducer.Efficiency * Core.propulsor.Acceleration;
                momentum = Core.transducer.Efficiency * Core.propulsor.Momentum;
                downForce = momentum * 0.4f;
                brakeCoefficient = 0.5f;
            }
            else
            {
                maxSpeed = 0f;
                turnSpeed = 0f;
                tiltSpeed = 0f;
                reverseSpeed = 0f;
                acceleration = 0f;
                momentum = 0f;
                downForce = 0f;
                brakeCoefficient = 0f;
            }
        }
        private void OnValidate() => Spec();
        protected override void SpecUpdate() => Spec();
        #endregion

        // 메서드
        public override void RideOn(Actor actor)
        {
            // Actor 방향 파악
            WhereIsPlayer(actor);

            // 쓰러진 라이딩기어를 바르게 놓기
            float yRotation = gameObject.transform.localRotation.eulerAngles.y;
            rbGear.MoveRotation(Quaternion.Euler(0, yRotation, 0));

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

            onPower = true;
        }

        public override void RideOff(Actor actor)
        {
            if (joint != null)
            {
                Destroy(joint);
                joint = null;

                // 라이딩기어 옆으로 내리기
                actor.transform.GetComponent<Rigidbody>().AddForce(OffDirection() * 250, ForceMode.Impulse);
            }

            rbGear.constraints = RigidbodyConstraints.None;

            // 플레이어와 라이딩기어의 충돌 활성화
            //foreach (var parts in GearColliders)
            //    Physics.IgnoreCollision(playerCollider, parts, false);

            onPower = false;
        }

        public override void EnhanceMode()
        {
            Core.enhance.Activate();
        }

        // 기타
        #region ETC
        private void WhereIsPlayer(Actor actor)
        {
            Vector3 localPos = transform.InverseTransformPoint(actor.transform.position);

            if (localPos.x > 0)
                // 플레이어가 보드의 오른쪽에 있음
                boardDirection = BoardDirection.Left;

            else
                // 플레이어가 보드의 왼쪽에 있음
                boardDirection = BoardDirection.Right;
        }

        // Break
        private void OnJointBreak(float breakForce)
        {

        }

        // 하차 방향 오버라이드
        protected override Vector3 OffDirection()
        {
            //float playerPos = isRight ? 1f : -1f;
            //float dir = (boardDrive.MoveInput == Vector2.zero) ? playerPos : boardDrive.MoveInput.x;
            float dir = -1f;

            Vector3 direction = (Vector3.up + new Vector3(dir, 0, 0).normalized).normalized;
            Vector3 realDirection = this.transform.TransformDirection(direction);
            return realDirection;
        }
        #endregion
    }
}