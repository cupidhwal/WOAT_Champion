//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;
//using System.Collections.Generic;

//namespace Seti
//{
//    /// <summary>
//    /// 플레이어의 아이템 사용 기능을 관리하는 클래스
//    /// 인벤토리 개폐, 클릭을 통한 사용, 퀵슬롯을 통한 사용을 활성화한다
//    /// </summary>
//    public class PlayerUse : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        // 컨트롤러 획득
//        private Control control;

//        // 퀵슬롯
//        private QuickSlot quickSlot;
//        private Button[] quickSlots;

//        // 클래스 컴포넌트
//        private Player player;
//        #endregion

//        // 속성
//        #region Properties
//        public Manager_Inventory InventoryManager { get; private set; }
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        private void Start()
//        {
//            // 초기화
//            player = GetComponent<Player>();
//            InventoryManager.SetPlayer(player);

//            // 사용할 퀵슬롯을 모두 가져오기
//            quickSlot = InventoryManager.GetComponentInChildren<QuickSlot>();
//            quickSlots = quickSlot.GetComponentsInChildren<Button>();
//        }

//        private void Awake()
//        {
//            control = new();
//            InventoryManager = FindFirstObjectByType<Manager_Inventory>();
//        }

//        private void OnEnable()
//        {
//            // 퀵슬롯 이벤트 핸들러 구독
//            control.Player.QuickSlot1.started += OnQuickSlot1DownStarted;
//            control.Player.QuickSlot2.started += OnQuickSlot2DownStarted;
//            control.Player.QuickSlot3.started += OnQuickSlot3DownStarted;
//            control.Player.Inventory.started += InventoryManager.OnInventoryStarted;
//            control.Player.CursorClick.started += InventoryManager.OnCursorClickStarted;
//            control.Player.CursorClick.canceled += InventoryManager.OnCursorClickCanceled;

//            // 실행취소 이벤트 핸들러 구독
//            control.UI.Cancel.started += InventoryManager.OnCancelStarted;

//            // 컨트롤 제어 활성화
//            control.Player.Enable();
//            control.UI.Enable();
//        }

//        private void OnDisable()
//        {
//            // 컨트롤 제어 비활성화
//            control.UI.Disable();
//            control.Player.Disable();

//            // 실행취소 이벤트 핸들러 구독 해제
//            control.UI.Cancel.started -= InventoryManager.OnCancelStarted;

//            // 퀵슬롯 이벤트 핸들러 구독 해제
//            control.Player.QuickSlot1.started -= OnQuickSlot1DownStarted;
//            control.Player.QuickSlot2.started -= OnQuickSlot2DownStarted;
//            control.Player.QuickSlot3.started -= OnQuickSlot3DownStarted;
//            control.Player.Inventory.started -= InventoryManager.OnInventoryStarted;
//            control.Player.CursorClick.started -= InventoryManager.OnCursorClickStarted;
//            control.Player.CursorClick.canceled -= InventoryManager.OnCursorClickCanceled;
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        public void OnQuickSlot1DownStarted(InputAction.CallbackContext _) => quickSlots[0].onClick.Invoke();
//        public void OnQuickSlot2DownStarted(InputAction.CallbackContext _) => quickSlots[1].onClick.Invoke();
//        public void OnQuickSlot3DownStarted(InputAction.CallbackContext _) => quickSlots[2].onClick.Invoke();
//        #endregion

//        // 메서드
//        #region Methods
//        // 아이템을 사용하는 메서드
//        public void UseItem(KeyValuePair<ItemKey, ItemValue> pair)
//        {
//            // 인벤토리에 아이템이 없다면 메서드 종료
//            if (!InventoryManager.Inventory.invenDict.ContainsKey(pair.Key)) return;

//            // 우선 입력된 아이템 정보를 찾고
//            ItemKey thisItem = CollectionUtility.FirstOrDefault(InventoryManager.Inventory.invenDict, kvp => pair.Key == kvp.Key).Key;

//            // 해당 아이템이 사용 가능한지 여부를 확인한 뒤
//            if (thisItem.itemPrefab.TryGetComponent<IUsable>(out var usableItem))
//            {
//                if (usableItem.CanUse(player))
//                {
//                    // 아이템을 사용
//                    usableItem.UseItem();

//                    // 수량 갱신
//                    InventoryManager.DecreaseItem(pair);
//                }
//            }

//            // 사용할 수 없는 아이템이라면 메서드 종료
//            else return;
//        }
//        #endregion
//    }
//}