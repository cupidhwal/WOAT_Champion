//using System.Collections;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace Seti
//{
//    /// <summary>
//    /// 보드 타입 라이딩기어의 인핸스모드
//    /// </summary>
//    public abstract class Enhance_Board : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        protected Control control;          // 인핸스 모드 제어 획득
//        protected IEnumerator enhanceCor;   // 코루틴 정의

//        // 단순 변수
//        [SerializeField]
//        protected int coolTime = 3;

//        // 불리언 변수
//        protected bool isCoolDown = false;

//        // 클래스 컴포넌트
//        protected RidingBoard board;
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        protected virtual void Start()
//        {
//            board = GetComponent<RidingBoard>();

//            // 인핸스 모드 입력만 활성화
//            control.RidingGear.Disable();
//            control.RidingGear.EnhanceModeOn.Enable();
//        }

//        private void Awake()
//        {
//            control = new();
//        }

//        private void OnEnable()
//        {
//            // 인핸스 모드 이벤트 구독
//            control.RidingGear.EnhanceModeOn.started += OnEnhanceModeStarted;
//        }

//        private void OnDisable()
//        {
//            // 인핸스 모드 이벤트 구독 해제
//            control.RidingGear.EnhanceModeOn.started -= OnEnhanceModeStarted;
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        public void OnEnhanceModeStarted(InputAction.CallbackContext _)
//        {
//            if (board.Player == null) return;
//            if (board.Player.playerStates.isBoard == true && !isCoolDown)
//            {
//                enhanceCor = EnhanceMode(coolTime);
//                StartCoroutine(enhanceCor);
//            }
//        }
//        #endregion

//        // 메서드
//        #region Methods
//        // 초기화
//        public void Initialize()
//        {
//            if (enhanceCor != null)
//            {
//                StopCoroutine(EnhanceMode(coolTime));
//                enhanceCor = null;
//            }
//            EnhanceSwitch(false);
//            isCoolDown = false;
//        }
//        #endregion

//        // 추상 메서드
//        #region Abstract Methods
//        protected abstract void EnhanceSwitch(bool isOn);
//        protected abstract void EnhanceUpdate();
//        #endregion

//        // 기타 유틸리티
//        #region Utilities
//        protected IEnumerator EnhanceMode(int coolTime)
//        {
//            board.Player.DialogueUI.Dialogue("인핸스 모드 ON!!", 1f);
//            isCoolDown = true;

//            // 인핸스 모드 시작
//            board.EnhanceMode(true);
//            EnhanceSwitch(true);

//            float enhanceStamp = Time.time;
//            while (enhanceStamp + 3 > Time.time)
//            {
//                EnhanceUpdate();

//                yield return null;
//            }

//            // 인핸스 모드 종료
//            EnhanceSwitch(false);
//            board.EnhanceMode(false);

//            if (board.Player != null)
//                board.Player.DialogueUI.Dialogue("푸슈---.......", 1f);

//            // 쿨다운
//            yield return new WaitForSeconds(coolTime - 3);
//            isCoolDown = false;

//            yield break;
//        }
//        #endregion
//    }
//}