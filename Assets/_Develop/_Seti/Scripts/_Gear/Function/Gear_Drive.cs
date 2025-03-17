using UnityEngine;
using UnityEngine.InputSystem;

namespace Seti
{
    /// <summary>
    /// 
    /// </summary>
    public class Gear_Drive : MonoBehaviour
    {
        // 필드
        #region Variables        
        // 복합 변수
        private float stabAngle;
        private Vector2 moveInput;              // 이동 입력
        private Vector3 moveDirection;          // 이동 방향
        private Vector3 currentVelocity;        // 현재 속도

        // 컴포넌트
        private Transform deckTransform;
        private Rigidbody rbGear;

        // 클래스 컴포넌트
        public RidingGear_Board board;

        // 일반
        private InputSystem_Actions control;
        #endregion

        // 속성
        #region Properties
        public Vector2 MoveInput
        {
            get { return moveInput; }
            set { moveInput = value; }
        }
        #endregion

        public void Start()
        {
            // 컴포넌트 초기화
            board = GetComponent<RidingGear_Board>();
            rbGear = GetComponent<Rigidbody>();
            deckTransform = board.transform.Find("Deck");

            control.RidingGear.Enable();
        }

        public void FixedUpdate()
        {
            if (board.OnPower)
            {
                Drive();
                Tilt();

                if (moveInput.y > 0.5f)
                    Turn();
            }
        }

        public void Awake()
        {
            control = new();
        }

        public void OnEnable()
        {
            control.RidingGear.Move.performed += OnDrivePerformed;
            control.RidingGear.Move.canceled += OnDriveCanceled;
        }

        public void OnDisable()
        {
            control.RidingGear.Move.performed -= OnDrivePerformed;
            control.RidingGear.Move.canceled -= OnDriveCanceled;
        }

        // 이벤트 핸들러
        #region Event Handlers
        public void OnDrivePerformed(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();

        public void OnDriveCanceled(InputAction.CallbackContext _) => moveInput = Vector2.zero;
        #endregion

        // 메서드
        #region Methods
        // 전진 및 후진
        public void Drive()
        {
            currentVelocity = rbGear.linearVelocity;

            if (moveInput.y >= 0.1)
            {
                if (currentVelocity.magnitude <= 0.1)
                    rbGear.AddForce((board.transform.forward * moveInput.y).normalized * board.Acceleration, ForceMode.Impulse);

                if (currentVelocity.magnitude > 0.1)
                    rbGear.AddForce((board.transform.forward * moveInput.y).normalized * board.Momentum, ForceMode.Force);

                if (currentVelocity.magnitude >= board.MaxSpeed)
                    rbGear.linearVelocity = rbGear.linearVelocity.normalized * board.MaxSpeed;

                if (moveDirection == Vector3.zero)
                    rbGear.linearVelocity *= board.BrakeCoefficient;
            }
            else
            {
                if (currentVelocity.magnitude <= 0.1)
                    rbGear.AddForce((board.transform.forward * moveInput.y).normalized * board.Acceleration, ForceMode.Impulse);

                if (currentVelocity.magnitude > 0.1)
                    rbGear.AddForce((board.transform.forward * moveInput.y).normalized * board.Momentum, ForceMode.Force);

                if (currentVelocity.magnitude >= board.ReverseSpeed)
                    rbGear.linearVelocity = rbGear.linearVelocity.normalized * board.ReverseSpeed;

                if (moveDirection == Vector3.zero)
                    rbGear.linearVelocity *= board.BrakeCoefficient;
            }
        }

        // 턴 - Y축 회전값 반환
        public void Turn()
        {
            if (MoveDirection() != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(MoveDirection());
                Quaternion smoothBoardRotation = Quaternion.Slerp(rbGear.rotation, targetRotation, board.TurnSpeed * Time.fixedDeltaTime);
                rbGear.MoveRotation(smoothBoardRotation);

                // 턴의 미끄러짐을 방지하기 위해 다운포스를 작성
                rbGear.AddForce(board.DownForce * Vector3.down, ForceMode.Force);
            }
        }

        // 턴 시 기울기 - Z축 회전값 반환
        public void Tilt()
        {
            float tiltAngle;

            // 이동 중일 때 기울기를 크게 하고, 정지 상태에서는 기울기를 작게 한다.
            if (MoveInput.y > 0)
                tiltAngle = Mathf.Lerp(0, 60f, Mathf.Abs(moveInput.x)) * -Mathf.Sign(moveInput.x); // 이동 중에는 최대 45도까지 기울기
            else
                tiltAngle = Mathf.Lerp(0, 15f, Mathf.Abs(moveInput.x)) * -Mathf.Sign(moveInput.x); // 정지 상태에서는 최대 15도까지 기울기

            Quaternion currentRotation = rbGear.rotation;
            Quaternion targetTiltRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, tiltAngle);
            Quaternion smoothTiltRotation = Quaternion.Slerp(currentRotation, targetTiltRotation, board.TiltSpeed * Time.fixedDeltaTime);
            rbGear.MoveRotation(smoothTiltRotation);
        }

        // 경사면 안정화 - X축 회전값 반환
        public void GyroStabilize()
        {
            // 앞쪽과 뒤쪽에서 레이캐스트
            Vector3 frontRayOrigin = board.transform.position + board.transform.forward * 0.777f;
            Vector3 backRayOrigin = board.transform.position - board.transform.forward * 0.777f;

            bool frontHit = Physics.Raycast(frontRayOrigin, board.transform.forward, 1f);
            bool backHit = Physics.Raycast(backRayOrigin, -board.transform.forward, 1f);

            // 전방에 경사로
            if (frontHit && !backHit)
            {
                stabAngle = Mathf.Lerp(stabAngle, -50, Time.fixedDeltaTime);
            }
            // 후방에 경사로
            else if (!frontHit && backHit)
            {
                stabAngle = Mathf.Lerp(stabAngle, 50, Time.fixedDeltaTime);
            }
            // 그 외의 경우는 수평
            else
            {
                stabAngle = Mathf.Lerp(stabAngle, 0, 0.5f * Time.fixedDeltaTime);
            }

            Quaternion currentRotation = rbGear.rotation;
            Quaternion targetStabRotation = Quaternion.Euler(stabAngle, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);
            rbGear.MoveRotation(targetStabRotation);
        }

        // 로컬 주행 방향 계산
        public Vector3 MoveDirection()
        {
            Vector3 forward = board.transform.forward;
            Vector3 right = board.transform.right;

            forward.y = 0;
            right.y = 0;

            return moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;
        }

        // 방지턱 보정
        public void GetOverCurb(Collision collision)
        {
            if (board == null) return;
            Transform curbSense = board.transform.Find("Sense_Curb");

            float height = 0;
            if (collision.transform.TryGetComponent<BoxCollider>(out var curb))
            {
                height = curb.size.y / 2 + curb.center.y +
                         collision.transform.position.y -
                         curbSense.position.y;
            }
            else if (collision.transform.TryGetComponent<MeshCollider>(out var meshCurb))
            {
                ContactPoint contact = collision.contacts[0];
                Bounds bounds = meshCurb.bounds;
                float sqrContactDis = (contact.point - bounds.center).sqrMagnitude;
                float sqrCenterDis = new Vector3(bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2).sqrMagnitude;

                if (sqrContactDis > sqrCenterDis / 2)
                {
                    height = bounds.size.y / 2 + bounds.center.y +
                             collision.transform.position.y -
                             curbSense.position.y;
                }
            }

            if (height > 0f && height < 0.5f)
                rbGear.MovePosition(board.transform.position + new Vector3(0, height + 0.01f, 0));
        }
        #endregion
    }
}