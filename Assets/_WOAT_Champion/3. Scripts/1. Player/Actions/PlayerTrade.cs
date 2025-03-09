//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace Seti
//{
//    // NPC와의 거래 기능을 총괄하는 기능 클래스
//    public class PlayerTrade : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        // 컨트롤러 획득
//        private Control control;

//        // 미확인 아이템을 확인하는 기능용 필드
//        private readonly List<ItemForTrade> tradableItems = new();

//        // 클래스 컴포넌트
//        private Player player;
//        private ShopBag shopBag;
//        #endregion

//        // 속성
//        #region Properties
//        public Player Player => player;
//        public ShopBag ShopBag => shopBag;
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        private void Start()
//        {
//            player = GetComponent<Player>();
//        }

//        private void Awake()
//        {
//            control = new();
//        }

//        private void OnEnable()
//        {
//            // NPC 상점에서 아이템 선택 이벤트 핸들러 구독
//            control.Player.CursorClick.started += OnLeftCursorClickStarted;
            
//            // 컨트롤 제어 활성화
//            control.Player.Enable();
//        }

//        private void OnDisable()
//        {
//            // 컨트롤 제어 비활성화
//            control.Player.Disable();

//            // NPC 상점에서 아이템 선택 이벤트 핸들러 구독
//            control.Player.CursorClick.started -= OnLeftCursorClickStarted;
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        // 액션을 취하면 아이템을 장바구니에 담는다
//        public void OnLeftCursorClickStarted(InputAction.CallbackContext _)
//        {
//            CallNPC();
//            ChooseItem();
//        }

//        public void OnRightCursorClickStarted(InputAction.CallbackContext _)
//        {

//        }
//        #endregion

//        // 메서드
//        #region Methods
//        // ShopBag 세팅 메서드
//        #region Set ShopBag
//        public void GetShopBag()
//        {
//            shopBag = player.PlayerUse.InventoryManager.GetComponentInChildren<ShopBag>();

//            if (shopBag.PlayerTrade == null)
//            {
//                shopBag.SetPlayer(this);
//                shopBag.transform.GetChild(0).gameObject.SetActive(true);
//            }
//        }

//        public void TakeOffShopBag()
//        {
//            if (shopBag.PlayerTrade != null)
//            {
//                if (shopBag.shopDict.Count != 0)
//                {
//                    foreach (var item in shopBag.shopItems)
//                        Factory.Instance.Destroy(item);
//                    shopBag.shopItems.Clear();
//                    shopBag.shopDict.Clear();
//                }

//                shopBag.transform.GetChild(0).gameObject.SetActive(false);
//                shopBag.SetPlayer(null);
//            }
//        }
//        #endregion

//        // PlayerInteraction이 관리하는 트리거 메서드와의 연결고리
//        #region CheckShop
//        public void CheckInShop(Collider other)
//        {
//            if (ComponentUtility.TryGetComponentInParent<ItemForTrade>(other.transform, out var tradableItem))
//            {
//                if (!tradableItems.Contains(tradableItem))
//                    tradableItems.Add(tradableItem);

//                tradableItem.MarkItem();
//            }

//            if (other.transform.CompareTag("Shop"))
//                StartCoroutine(SearchForTrade());
//        }
//        public void CheckOutShop(Collider other)
//        {
//            if (ComponentUtility.TryGetComponentInParent<ItemForTrade>(other.transform, out var tradableItem))
//            {
//                tradableItem.ForgetItem();

//                if (tradableItems.Contains(tradableItem))
//                    tradableItems.Remove(tradableItem);
//            }
//        }
//        #endregion

//        // NPC를 선택하는 메서드
//        private void CallNPC()
//        {
//            if (player.PlayerUse.InventoryManager.ThisSlot != null) return;

//            if (player.CursorUtility.CursorSelect() == null) return;

//            if (player.CursorUtility.CursorSelect().
//                TryGetComponent<NPC_Merchant>(out var merchant))
//                merchant.Trade(player);
//        }

//        // 아이템을 선택하는 메서드
//        private void ChooseItem()
//        {
//            if (player.CursorUtility.CursorSelect() == null) return;

//            // 구매 가능한 아이템을 표시
//            if (ComponentUtility.TryGetComponentInParent<ItemForTrade>(player.CursorUtility.CursorSelect(), out var tradableItem))
//            {
//                if (!tradableItem.IsIdentify) return;

//                ItemKey shopItem = tradableItem.GetItemData();
//                AddItem(shopItem);
//            }
//        }

//        // NPC 상점의 매대에 진열된 아이템을 장바구니에 저장하는 메서드
//        private void AddItem(ItemKey itemKey)
//        {
//            // 현재 장바구니에 해당 아이템이 있으면
//            if (shopBag.shopDict.ContainsKey(itemKey))
//            {
//                // 개수를 늘린다
//                shopBag.shopDict[itemKey].Count(1);
//                shopBag.CountItem(itemKey, shopBag.shopDict[itemKey].itemIndex);
//            }

//            // 현재 장바구니에 해당 아이템이 없으면
//            else
//            {
//                // 장바구니에 담은 아이템의 itemValue를 인스턴스로 만들고
//                ItemValue itemValue = new();

//                // 새 슬롯을 생성해서 아이템 할당
//                shopBag.MakeSlot(itemKey, itemValue);
//            }
//        }
//        #endregion

//        // 기타 유틸리티
//        #region Utilities
//        // NPC 상점 안의 미확인 아이템을 확인하는 동기 반복기
//        private IEnumerator SearchForTrade()
//        {
//            Transform head = player.transform.Find("Head_Root");

//            while (tradableItems.Count != 0)
//            {
//                if (player == null) continue;

//                if (Physics.Raycast(head.position, head.forward, out RaycastHit hit, 7.5f))
//                    if (ComponentUtility.TryGetComponentInParent<ItemForTrade>(hit.transform, out var tradableItem))
//                    {
//                        tradableItem.DefineItem();
//                        tradableItems.Remove(tradableItem);
//                    }
//                yield return null;
//            }
//        }
//        #endregion
//    }
//}