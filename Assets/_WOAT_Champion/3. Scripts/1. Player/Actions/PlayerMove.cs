//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace Seti
//{
//    // 플레이어의 이동 제어를 관리하는 클래스
//    public class PlayerMove
//    {
//        // 필드
//        #region Variables
//        // 단순 변수
//        private float moveSpeed;
//        private readonly float walkSpeed;
//        private readonly float runSpeed;
//        private readonly float jumpForce;

//        // 복합 변수
//        private Vector2 moveInput;          // 이동 입력
//        private Vector2 lastMoveDirection;  // 마지막 이동 방향

//        // 컴포넌트
//        private readonly Rigidbody rb;

//        // 클래스 컴포넌트
//        private readonly Player player;
//        #endregion

//        // 속성
//        #region Properties
//        public Vector2 MoveInput { get { return moveInput; } }
//        public Vector2 LastMoveDirection { get { return lastMoveDirection; } set { lastMoveDirection = value; } }
//        #endregion

//        // 생성자
//        #region Constructor
//        public PlayerMove(Player player, float walkSpeed, float runSpeed, float jumpForce)
//        {
//            this.player = player;
//            this.rb = player.transform.GetComponent<Rigidbody>();
//            this.walkSpeed = walkSpeed;
//            this.runSpeed = runSpeed;
//            this.jumpForce = jumpForce;
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        public void OnMovePerformed(InputAction.CallbackContext context)
//        {
//            moveInput = context.ReadValue<Vector2>();
//            player.PlayerAni.Animator.SetBool(AniString.IsMove, true);
//        }

//        public void OnMoveCanceled(InputAction.CallbackContext _)
//        {
//            moveInput = Vector2.zero;
//            player.PlayerAni.Animator.SetBool(AniString.IsMove, false);
//        }

//        public void OnRunSwitch(InputAction.CallbackContext _) => player.PlayerAni.ChangeState(AniStates.MOVE_RUN_SWITCH);

//        public void OnJumpStarted(InputAction.CallbackContext _)
//        {
//            if (!player.PlayerAni.Animator.GetBool(AniString.IsJump))
//            {
//                Jump();
//                player.PlayerAni.ChangeState(AniStates.JUMP);
//            }
//        }
//        #endregion

//        // 메서드
//        #region Methods
//        // 플레이어의 기본 이동 메서드
//        public void Move()
//        {
//            if (rb == null) return;

//            if (player.PlayerAni.Animator.GetBool(AniString.IsRun)) moveSpeed = runSpeed;
//            else moveSpeed = walkSpeed;

//            Vector3 moveDirection = new(lastMoveDirection.x, 0, lastMoveDirection.y);

//            Vector3 forward = player.transform.forward * moveDirection.z;
//            Vector3 right = player.transform.right * moveDirection.x;

//            Vector3 move = moveSpeed * Time.fixedDeltaTime * (forward + right).normalized;

//            rb.MovePosition(player.transform.position + move);
//        }

//        // 플레이어의 기본 점프 메서드
//        public async void Jump()
//        {
//            if (rb == null) return;

//            if (player.PlayerAni.Animator.GetBool(AniString.IsMove))
//                await Task.Delay(100);
//            else await Task.Delay(500);

//            rb.AddForce(rb.transform.up * jumpForce, ForceMode.Impulse);
//        }

//        // 방지턱 보정
//        public void GetOverCurb(Collision collision)
//        {
//            float height = 0;
//            if (collision.transform.TryGetComponent<BoxCollider>(out var curb))
//            {
//                height = curb.size.y / 2 + curb.center.y + 
//                         collision.transform.position.y - 
//                         player.transform.position.y;
//            }
//            else if (collision.transform.TryGetComponent<MeshCollider>(out var meshCurb))
//            {
//                ContactPoint contact = collision.contacts[0];
//                Bounds bounds = meshCurb.bounds;
//                float sqrContactDis = (contact.point - bounds.center).sqrMagnitude;
//                float sqrCenterDis = new Vector3(bounds.size.x / 2, bounds.size.y / 2, bounds.size.z / 2).sqrMagnitude;

//                if (sqrContactDis > sqrCenterDis / 2)
//                {
//                    height = bounds.size.y / 2 + bounds.center.y +
//                             collision.transform.position.y -
//                             player.transform.position.y;
//                }
//            }

//            if (height > 0f && height < 0.5f)
//                rb.MovePosition(player.transform.position + new Vector3(0, height, 0));
//        }
//        #endregion
//    }
//}