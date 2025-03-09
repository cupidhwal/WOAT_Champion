//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace Seti
//{
//    // 장애물의 정보
//    public class ObsInfo
//    {
//        public int leftOrRight;
//        public float acrobatFactor;
//        public float acrobatX;      // 최종 액션 Input.x
//        public Vector3 closestPoint;

//        public float CalObsInfo(Transform boardTransform)
//        {
//            // acrobatFactor 계산
//            Vector3 boardToObs = closestPoint - boardTransform.position;
//            acrobatFactor = MathUtility.CalDisExp(boardToObs.magnitude, 10, -0.25f, 2);

//            // 회피 방향 판정
//            Vector3 obsLocalPos = boardTransform.InverseTransformPoint(closestPoint);
//            leftOrRight = (obsLocalPos.x >= 0) ? -1 : 1;

//            // acrobatDir 계산
//            float angleBetween = Mathf.Abs(Vector3.Angle(boardTransform.forward, boardToObs));
//            angleBetween = Mathf.Clamp(angleBetween, 0, 90);
//            float acrobatDirX = leftOrRight * Mathf.Cos(Mathf.Deg2Rad * angleBetween);

//            return acrobatFactor * acrobatDirX;
//        }
//    }

//    /// <summary>
//    /// 보드 타입 라이딩기어의 인핸스 모드:AA를 담당하는 클래스
//    /// </summary>
//    public class Enhance_AutomaticAcrobat : Enhance_Board
//    {
//        // 필드
//        #region Variables
//        // Automatic Acrobat 전용 필드
//        private int originalSpeed;
//        private int turnSpeed;
//        private float detectRadius = 15;                        // 탐지 반경
//        private float acrobatFactor;
//        private Vector2 acrobatInput;                           // 회피 방향 / 왼쪽: -1, 오른쪽: 1
//        private Dictionary<Collider, ObsInfo> obstacles = new();// 감지 중인 장애물 리스트

//        // Automatic Acrobat 강화 파라미터
//        [SerializeField]
//        private int enhanceSpeed;

//        // 컴포넌트
//        [SerializeField]
//        private Collider currentGround;
//        [SerializeField]
//        private List<Collider> ignoreColliders = new();
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        protected override void Start()
//        {
//            base.Start();

//            // 초기화
//            originalSpeed = board.BoardDrive.MaxSpeed;
//            turnSpeed = board.BoardDrive.TurnSpeed;

//            // 장애물 판정에 제외할 콜라이더
//            GetComponent<RidingGear>().OnStanceChanged += SetPlayer;
//            ignoreColliders.AddRange(GetComponent<RidingGear>().GearColliders);
//        }
//        #endregion

//        // 메서드
//        #region Methods
//        // 장애물 정보 연산 시작
//        private void EnterObsInfo(Collider obstacle)
//        {
//            if (!obstacles.ContainsKey(obstacle))
//            {
//                ObsInfo obsInfo = new()
//                {
//                    closestPoint = obstacle.ClosestPoint(board.transform.position)
//                };

//                // 장애물 관리용 딕셔너리에 추가
//                obstacles.Add(obstacle, obsInfo);
//            }
//        }

//        // 장애물 정보 갱신
//        private void UpdateObsInfo()
//        {
//            if (obstacles.Count == 0) return;

//            acrobatFactor = 0;
//            float acrobatDirX = 0;

//            foreach (var pair in obstacles)
//            {
//                pair.Value.closestPoint = pair.Key.ClosestPoint(board.transform.position);
//                pair.Value.acrobatX = pair.Value.CalObsInfo(board.transform);

//                acrobatFactor += pair.Value.acrobatFactor;
//                acrobatDirX += pair.Value.acrobatX;
//            }

//            acrobatInput.x = Mathf.Lerp(acrobatInput.x,
//                                        acrobatDirX,
//                                        acrobatFactor * Time.deltaTime);
//        }

//        // 장애물 정보 연산 종료
//        private void ExitObsInfo(Collider obstacle)
//        {
//            if (obstacles.Count == 0) return;
//            if (obstacles.ContainsKey(obstacle))
//                obstacles.Remove(obstacle);
//        }
//        #endregion

//        // 오버라이드
//        #region Override
//        // 인핸스 모드 운전 제어
//        protected override void EnhanceSwitch(bool isOn)
//        {
//            SphereCollider obstacleSense = transform.Find("Sense_Obstacle").GetComponent<SphereCollider>();
//            obstacleSense.enabled = isOn;

//            if (isOn)
//            {
//                acrobatInput = new(0, 1);

//                // 장애물 탐지 기능 On
//                obstacleSense.radius = detectRadius;

//                // 자동 회피 기동을 위해 입력 차단
//                board.Control.RidingGear.Disable();
//                board.BoardDrive.MoveInput = acrobatInput;

//                // 최대 속력 증폭
//                board.BoardDrive.MaxSpeed = enhanceSpeed;

//                // 회전 속도 증폭
//                board.BoardDrive.TurnSpeed = 3 * turnSpeed;
//            }
//            else
//            {
//                // 회전 속도 복구
//                board.BoardDrive.TurnSpeed = turnSpeed;

//                // 최대 속력 복구
//                board.BoardDrive.MaxSpeed = originalSpeed;

//                // 입력 차단 해제
//                board.Control.RidingGear.Enable();
//                board.Control.RidingGear.EnhanceModeOn.Disable();

//                // 장애물 탐지 기능 Off
//                obstacleSense.radius = 0.1f;
//                obstacles.Clear();
//            }
//        }

//        protected override void EnhanceUpdate()
//        {
//            UpdateObsInfo();

//            board.BoardDrive.MoveInput = acrobatInput;

//            // 회전 속도 증폭
//            //board.BoardDrive.TurnSpeed = (int)acrobatFactor * turnSpeed;
//        }
//        #endregion

//        // 이벤트 메서드
//        #region Event Methods
//        #region Trigger
//        private void OnTriggerEnter(Collider other)
//        {
//            if (other != currentGround &&
//                !ignoreColliders.Contains(other))
//            {
//                EnterObsInfo(other);
//            }
//        }

//        private void OnTriggerExit(Collider other)
//        {
//            ExitObsInfo(other);
//        }
//        #endregion

//        #region Collision
//        private void OnCollisionEnter(Collision collision)
//        {
//            Transform curbSense = board.transform.Find("Sense_Curb");
//            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") &&
//                collision.transform.TryGetComponent<BoxCollider>(out var ground))
//            {
//                float height = ground.size.y / 2 + ground.center.y +
//                               collision.transform.position.y -
//                               curbSense.position.y;

//                if (height <= 0) currentGround = collision.collider;
//            }
//        }
//        #endregion
//        #endregion

//        // 기타 유틸리티
//        #region Utilities
//        public void SetPlayer()
//        {
//            List<Collider> playerCol = new();
//            playerCol.Add(GetComponent<RidingGear>().Player.GetComponent<Collider>());
//            playerCol.AddRange(GetComponent<RidingGear>().Player.GetComponentsInChildren<Collider>());

//            foreach (var col in playerCol)
//            {
//                if (!ignoreColliders.Contains(col))
//                    ignoreColliders.Add(col);
//            }
//        }
//        #endregion
//    }
//}