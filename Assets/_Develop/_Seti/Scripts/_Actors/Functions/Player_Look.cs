using UnityEngine;
using UnityEngine.InputSystem;

namespace Seti
{
    // 플레이어의 시야 제어를 관리하는 클래스
    public class Player_Look : MonoBehaviour
    {
        // 필드
        #region Variables
        // 조정값
        [Header("Status")]
        [SerializeField]
        private bool keepGoing = false;
        [SerializeField]
        private bool attention = false;
        [Header("Properties")]
        [SerializeField]
        private float mouseSensitivity;     // 마우스 감도
        [SerializeField]
        private float syncSensitivity;      // Lerp, Slerp 보간 감도

        // 일반
        private float headXRotation;        // head X축 회전값
        private float headYRotation;        // head Y축 회전값
        private float bodyYRotation;        // body Y축 회전값
        private Vector2 lookInput;          // 마우스 입력
        private InputSystem_Actions control;

        // 컴포넌트
        private Player player;              // 플레이어
        private Rigidbody rb;               // 플레이어 Rigidbody
        private Transform headTransform;    // 플레이어의 머리 부분 Transform
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            player = GetComponent<Player>();
            rb = GetComponent<Rigidbody>();

            headTransform = transform.Find("Head_Root");

            control.Player.Look.Enable();
            control.Player.KeepGoing.Enable();
        }

        private void Update()
        {
            SyncRotationHead();
        }

        private void FixedUpdate()
        {
            SyncRotationBody();
        }

        private void Awake()
        {
            control = new();
        }

        private void OnEnable()
        {
            control.Player.Look.performed += OnLookPerformed;
            control.Player.Look.canceled += OnLookCanceled;
            control.Player.KeepGoing.started += OnKeepGoingStarted;
            control.Player.KeepGoing.canceled += OnKeepGoingCanceled;
        }

        private void OnDisable()
        {
            control.Player.Look.performed -= OnLookPerformed;
            control.Player.Look.canceled -= OnLookCanceled;
            control.Player.KeepGoing.started -= OnKeepGoingStarted;
            control.Player.KeepGoing.canceled -= OnKeepGoingCanceled;
        }
        #endregion

        // 이벤트 핸들러
        #region Event Handlers
        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
            Rotation();
        }
        private void OnLookCanceled(InputAction.CallbackContext _) => lookInput = Vector2.zero;
        private void OnKeepGoingStarted(InputAction.CallbackContext _) => keepGoing = true;
        private void OnKeepGoingCanceled(InputAction.CallbackContext _) => keepGoing = false;
        #endregion

        // 메서드
        private void InitializeRotation(float headX, float headY, float bodyY)
        {
            headXRotation = headX;
            headYRotation = headY;
            bodyYRotation = bodyY;
        }

        private void Rotation()
        {
            // 각 축의 Delta 값
            headXRotation -= lookInput.y * mouseSensitivity;

            // 각 축의 한계 회전각
            headXRotation = Mathf.Clamp(headXRotation, -50f, 50f);
            headYRotation = Mathf.Clamp(headYRotation, -80f, 80f);

            //if (player.PlayerAni.Animator.GetFloat(AniString.xVelocity) <= -0.5f)
            //    headYRotation = Mathf.Clamp(headYRotation, -100f, 13f);
            //else if (player.PlayerAni.Animator.GetFloat(AniString.xVelocity) >= 0.5f)
            //    headYRotation = Mathf.Clamp(headYRotation, -13f, 100f);
            //else headYRotation = Mathf.Clamp(headYRotation, -80f, 80f);

            // 플레이어가 부츠에 탑승 중이거나 기본 상태일 경우
            if (player.Condition.CurrentStance != Stance.Board)
            {
                // KeepGoing == true일 때, head만 회전 가능
                if (keepGoing)
                {
                    headYRotation += lookInput.x * mouseSensitivity;
                    headTransform.localRotation = Quaternion.Euler(headXRotation, headYRotation, 0f);
                }
                // KeepGoing == false일 때, head 상하 회전, body 좌우 회전
                else
                {
                    // body 회전 시작
                    if (lookInput != Vector2.zero)
                        bodyYRotation = lookInput.x * mouseSensitivity;
                    else
                        bodyYRotation = 0;

                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, bodyYRotation, 0f));
                }
            }

            // 플레이어가 보드에 탑승 중일 경우
            else if (keepGoing)
            {
                headYRotation += lookInput.x * mouseSensitivity;
                headTransform.localRotation = Quaternion.Euler(headXRotation, headYRotation, 0f);
            }
        }

        public void SyncRotationHead()
        {
            if (keepGoing)
            {
                // 주목 기능이 실행 중이었다면 종료
                if (attention == true && player.Condition.CurrentStance != Stance.Board)
                    OnAttentionExit(headTransform.rotation, rb.transform.rotation);
                return;
            }

            // 플레이어가 기본 상태이거나 부츠에 탑승 중인 경우
            if (player.Condition.CurrentStance != Stance.Board)
            {
                headYRotation = Mathf.Lerp(headYRotation, 0, syncSensitivity * Time.deltaTime);
                headTransform.localRotation = Quaternion.Euler(headXRotation, headYRotation, 0f);
            }

            // 플레이어가 보드에 탑승 중일 경우
            else
            {
                InitializeRotation(0f, -DefineSync(), 0f);
                Quaternion targetRotation = Quaternion.Euler(0f, -DefineSync(), 0f);
                headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, targetRotation, 0.1f);
            }
        }

        public void SyncRotationBody()
        {
            if (!keepGoing)
            {
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0), 0.2f));
            }

            // 플레이어가 보드에 탑승 중일 경우
            if (player.Condition.CurrentStance == Stance.Board)
            {
                Quaternion targetRotation = player.CurrentGear.transform.localRotation * Quaternion.Euler(0f, DefineSync(), 0f);
                rb.MoveRotation(targetRotation);
            }
        }

        // 주목 기능 - 특정 오브젝트를 바라보는 메서드
        #region Attention Methods
        private void OnAttentionEnter(Vector3 gearDir, Quaternion gearRot)
        {
            if (Vector3.Angle(headTransform.forward, gearDir) > 10)
                headTransform.rotation = Quaternion.Slerp(headTransform.rotation, gearRot, 0.1f);
            else
                attention = true;
        }

        private void OnAttentionStay(Quaternion gear)
        {
            headTransform.rotation = gear;
        }

        private void OnAttentionExit(Quaternion head, Quaternion body)
        {
            // 주목 기능이 해제될 경우 각 축의 회전값 초기화
            InitializeRotation(0f, 0f, 0f);

            if (Vector3.Angle(headTransform.forward, rb.transform.forward) > 2.5)
                headTransform.rotation = Quaternion.Slerp(head, body, 0.1f);
            else
                attention = false;
        }
        #endregion

        // 보드 탑승 직전 플레이어의 위치에 따라 동기화 방향을 결정하는 메서드
        protected float DefineSync()
        {
            if (player.CurrentGear is RidingGear_Board board)
                return board.BoardDir ? 80f : -80f;
            return 0f;
        }
    }
}