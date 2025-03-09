//using UnityEngine;

//namespace Seti
//{
//    // RidingGear 클래스를 상속받은 보드 타입 라이딩기어의 중심 클래스
//    // RidingGear 클래스의 탑승, 하차 로직과 대리자를 오버라이드 하고
//    // 보드 타입에 필수적인 기능 클래스의 매개변수와 구동 방식 로직을 갖는다
//    public class RidingBoard : RidingGear
//    {
//        // 필드
//        #region Variables
//        private bool isGrounded = true;      // 지면 판정
//        private bool isRight = false;        // 탑승 직전 위치 판정 ? 플레이어가 보드의 오른쪽 : 왼쪽

//        protected int maxSpeed;
//        protected int reverseSpeed;
//        protected int turnSpeed;
//        protected int tiltSpeed;
//        protected int initialForce;
//        protected int moveForce;
//        protected int downForce;
//        protected int breakForce;
//        protected float brakeCoefficient;

//        protected BoardDrive boardDrive;
//        protected Enhance_Board boardEnhance;
//        #endregion

//        // 속성
//        #region Properties
//        public bool IsRight => isRight;
//        public BoardDrive BoardDrive => boardDrive;
//        public Enhance_Board BoardEnhance => boardEnhance;
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        protected override void Start()
//        {
//            base.Start();
//            boardEnhance = GetComponent<Enhance_Board>();
//        }

//        private void FixedUpdate()
//        {
//            if (isBoard != true) return;

//            if (isGrounded)
//            {
//                if (boardDrive.MoveInput.y > 0)
//                {
//                    boardDrive.Drive();
//                    boardDrive.Turn();
//                    boardDrive.Tilt();
//                    boardDrive.GyroStabilize();
//                    RidingEngine.ConsumeEnergy(player.PlayerEnergy);
//                }
//                else
//                {
//                    boardDrive.Drive();
//                    boardDrive.Tilt();
//                }
//            }
//        }

//        private void OnEnable()
//        {
//            // Control에 하차 입력 구독
//            control.RidingGear.RideOff.performed += OnRideOffPerformed;

//            // 보드 타입 라이딩기어의 이벤트 연결
//            control.RidingGear.Drive.performed += boardDrive.OnDrivePerformed;
//            control.RidingGear.Drive.canceled += boardDrive.OnDriveCanceled;

//            // 대리자 이벤트에 메서드 구독
//            OnStanceChanged += StanceCheck;

//            // 라이딩기어 컨트롤 활성화
//            control.RidingGear.Enable();
//        }

//        private void OnDisable()
//        {
//            // 라이딩기어 컨트롤 비활성화
//            control.RidingGear.Disable();

//            // 대리자 이벤트에 메서드 구독 해제
//            OnStanceChanged -= StanceCheck;

//            // 보드 타입 라이딩기어의 운전 이벤트 연결 해제
//            control.RidingGear.Drive.performed -= boardDrive.OnDrivePerformed;
//            control.RidingGear.Drive.canceled -= boardDrive.OnDriveCanceled;

//            // Control에 하차 입력 구독 해제
//            control.RidingGear.RideOff.performed -= OnRideOffPerformed;
//        }
//        #endregion

//        // 메서드
//        #region Methods
//        private bool WhereIsPlayer()
//        {
//            Vector3 playerLocalPos = this.transform.InverseTransformPoint(player.transform.position);

//            if (playerLocalPos.x > 0)
//                return true;    // 플레이어가 보드의 오른쪽에 있음

//            else
//                return false;   // 플레이어가 보드의 왼쪽에 있음
//        }

//        public void EnhanceMode(bool onEnhance)
//        {
//            OnEnhance = onEnhance;
//        }
//        #endregion

//        // 오버라이드
//        #region Override
//        protected override void StanceCheck()
//        {
//            if (joint != null)
//            {
//                // 타입 판정 플래그
//                isBoard = true;
//            }
//            else
//            {
//                // 하차
//                isBoard = null;
//            }

//            base.StanceCheck();
//        }

//        public override void RideOn()
//        {
//            // 탑승 직전, 플레이어가 보드의 ? 오른쪽 : 왼쪽
//            isRight = WhereIsPlayer();

//            base.RideOn();
//            player.playerLook.SyncRotationBoard();

//            joint.breakForce = breakForce;                     // 조인트 파괴 강도를 무한대로 설정
//            joint.breakTorque = breakForce;                    // 조인트 파괴 회전력을 무한대로 설정
//        }

//        public override void RideOff()
//        {
//            base.RideOff();

//            rbGear.constraints = RigidbodyConstraints.None;
//        }

//        public override void CrashAccident()
//        {
//            base.CrashAccident();
//            if (TryGetComponent<Enhance_AutomaticAcrobat>(out var boardAA))
//                boardAA.Initialize();

//            if (boardDrive.MoveInput != Vector2.zero)
//                boardDrive.MoveInput = Vector2.zero;
//        }

//        // 하차 방향 오버라이드
//        protected override Vector3 OffDirection()
//        {
//            float playerPos = isRight ? 1f : -1f;
//            float dir = (boardDrive.MoveInput == Vector2.zero) ? playerPos : boardDrive.MoveInput.x;

//            Vector3 direction = (Vector3.up + new Vector3(dir, 0, 0).normalized).normalized;
//            Vector3 realDirection = this.transform.TransformDirection(direction);
//            return realDirection;
//        }
//        #endregion

//        // 이벤트 메서드
//        #region Event Methods
//        //Collision 시리즈
//        #region Collision
//        protected virtual void OnCollisionChange(Collision collision, bool groundedState)
//        {
//            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
//                isGrounded = groundedState; // 지면에 닿았는지 여부를 설정
//        }

//        private void OnCollisionEnter(Collision collision)
//        {
//            OnCollisionChange(collision, true);
//            boardDrive.GetOverCurb(collision);
//        }

//        private void OnCollisionStay(Collision collision) => OnCollisionChange(collision, true);

//        private void OnCollisionExit(Collision collision) => OnCollisionChange(collision, false);

//        protected override void SpecUpdate()
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RideOn(Actor actor)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void RideOff(Actor actor)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void EnhanceMode()
//        {
//            throw new System.NotImplementedException();
//        }
//        #endregion
//        #endregion
//    }
//}