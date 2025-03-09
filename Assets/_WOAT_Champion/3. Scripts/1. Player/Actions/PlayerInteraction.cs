//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace Seti
//{
//    // 플레이어의 상호작용을 담당하는 클래스
//    // 기능 클래스지만 상호작용 전용 트리거를 전담하여 처리하기 위해 MonoBehaviour를 쓴다
//    public class PlayerInteraction : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        // 컨트롤러 획득
//        private InputSystem_Actions control;

//        // Dictionary를 사용해 상호작용 오브젝트와 UI를 관리
//        private readonly Dictionary<GameObject, GameObject> interactables = new();

//        // 상호작용 UI 프리팹
//        private GameObject VFXInteractable;
//        [SerializeField] private GameObject VFXUnique;
//        [SerializeField] private GameObject VFXLegend;
//        [SerializeField] private GameObject VFXCommon;

//        // 무시 콜라이더
//        private List<Collider> ignoreThese = new();

//        // 클래스 컴포넌트
//        private Player player;
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
//            // 상호작용 이벤트 핸들러 구독
//            control.Player.Interaction.started += OnInteractionStarted;

//            // 컨트롤 제어 활성화
//            control.Player.Enable();
//        }

//        private void OnDisable()
//        {
//            // 컨트롤 제어 비활성화
//            control.Player.Disable();

//            // 상호작용 이벤트 핸들러 구독 해제
//            control.Player.Interaction.started -= OnInteractionStarted;
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        public void OnInteractionStarted(InputAction.CallbackContext _) => ThisIsMine();
//        #endregion

//        // 메서드
//        #region Methods
//        // 상호작용이 가능한 오브젝트 줍기, UI 제거 및 리스트 최신화
//        public void ThisIsMine()
//        {
//            GameObject closestObject = MathUtility.MinDistance(this.transform.gameObject, interactables);

//            // 상호작용 대상이 실제로 존재하면
//            if (closestObject != null)
//            {
//                // 인터페이스로부터 itemData 가져오기
//                if (closestObject.TryGetComponent<IInteractable>(out var interactable))
//                {
//                    if (interactable is Item item)
//                    {
//                        ItemKey itemData = item.GetData();
//                        player.PlayerUse.InventoryManager.AddItem(itemData, 1); // 인벤토리에 더하는 메서드 호출

//                        // 해당 오브젝트 줍기
//                        Factory.Instance.Destroy(closestObject);
//                    }
//                }

//                else if (ComponentUtility.TryGetComponentInParent<RidingGear>(closestObject.transform, out var gear))
//                {
//                    if (Manager_GearSlot.Instance.GearKey == null)
//                    {
//                        if (gear.Owner == player)
//                        {
//                            GearSocket(gear);
//                        }
//                        else
//                        {
//                            player.DialogueUI.Dialogue($"내 것도 아닌 라이딩기어를 함부로 가져갈 수는 없어.");
//                            return;
//                        }
//                    }
//                    else
//                    {
//                        player.DialogueUI.Dialogue($"라이딩기어라면 이미 {Manager_GearSlot.Instance.GearKey.gearNameKor}를 가지고 있잖아!");
//                        return;
//                    }
//                }

//                // UI 오브젝트 제거
//                Factory.Instance.Destroy(interactables[closestObject]);
                
//                // Dictionary에서 해당 오브젝트와 UI 제거
//                interactables.Remove(closestObject);
//            }
//        }

//        // "저게 뭐지?" 상호작용 화살표 생성 메서드
//        private void WhatIsThat(Collider other)
//        {
//            // 인터페이스로 상호작용 가능한 오브젝트만 구별
//            if (other.gameObject.GetComponent<IInteractable>() != null)
//            {
//                // 해당 오브젝트가 이미 리스트에 있는지 확인
//                if (!interactables.ContainsKey(other.gameObject))
//                {
//                    if (other.gameObject.GetComponent<Cartridge>() ||
//                        other.gameObject.GetComponent<Pearl>()) VFXInteractable = VFXLegend;
//                    else VFXInteractable = VFXCommon;

//                    // 상호작용 가능한 오브젝트와 대응하는 UI 오브젝트 생성 및 추가
//                    GameObject VFXInstance = Factory.Instance.Instantiate(VFXInteractable,
//                                                                          other.transform.position,
//                                                                          Quaternion.identity);
//                    VFXInstance.transform.SetParent(other.transform);
//                    interactables.Add(other.gameObject, VFXInstance);
//                }
//            }

//            // 라이딩기어 추상 클래스로 상호작용이 가능한 라이딩기어 구별
//            else if (ComponentUtility.TryGetComponentInParent<RidingGear>(other.transform, out var gear))
//            {
//                if (!interactables.ContainsKey(gear.gameObject))
//                {
//                    GameObject VFXInstance = null;
//                    if (!ignoreThese.Contains(gear.GetComponentInChildren<Collider>()))
//                    {
//                        // 상호작용 가능한 오브젝트와 대응하는 UI 오브젝트 생성 및 추가
//                        VFXInteractable = VFXUnique;
//                        VFXInstance = Factory.Instance.Instantiate(VFXInteractable,
//                                                                   other.transform.position,
//                                                                   Quaternion.identity);
//                        VFXInstance.transform.SetParent(gear.transform);
//                    }
//                    interactables.Add(gear.gameObject, VFXInstance);
//                }
//            }
//        }

//        // "쓰레기네." 상호작용 화살표 제거 메서드
//        private void Sucks(Collider other)
//        {
//            // 인터페이스로 상호작용 가능한 오브젝트만 구별
//            if (other.gameObject.GetComponent<IInteractable>() != null)
//            {
//                // 해당 오브젝트가 리스트에 있는지 확인
//                if (interactables.ContainsKey(other.gameObject))
//                {
//                    // UI 오브젝트 제거
//                    Factory.Instance.Destroy(interactables[other.gameObject]);

//                    // Dictionary에서 해당 오브젝트와 VFX 제거
//                    interactables.Remove(other.gameObject);
//                }
//            }

//            // 라이딩기어 추상 클래스로 상호작용이 가능한 라이딩기어 구별
//            else if (ComponentUtility.TryGetComponentInParent<RidingGear>(other.transform, out var gear))
//            {
//                if (interactables.ContainsKey(gear.gameObject))
//                {
//                    // UI 오브젝트 제거
//                    Factory.Instance.Destroy(interactables[gear.gameObject]);

//                    // Dictionary에서 해당 라이딩기어와 VFX 제거
//                    interactables.Remove(gear.gameObject);
//                }
//            }
//        }

//        public void RideOn()
//        {
//            if (interactables.ContainsKey(player.ridingGear.gameObject))
//            {
//                // UI 오브젝트 제거
//                Factory.Instance.Destroy(interactables[player.ridingGear.gameObject]);

//                // Dictionary에서 해당 라이딩기어와 VFX 제거
//                interactables.Remove(player.ridingGear.gameObject);

//                // 해당 라이딩기어와의 VFX 차단
//                AddIgnore();
//            }
//        }

//        public void GearSocket(RidingGear gear)
//        {
//            gear.RbGear.linearVelocity = Vector3.zero;

//            gear.gameObject.SetActive(false);
//            GearKey gearData = gear.GetData();
//            Manager_GearSlot.Instance.SetGear(gear);    // 기어 소켓에 라이딩기어 장착
//            player.playerRide.ridableGears.Remove(gear);// 인지 중인 라이딩기어 목록에서 이 라이딩기어를 제거
//        }
//        #endregion

//        // 이벤트 메서드
//        #region Event Methods
//        private void OnTriggerEnter(Collider other)
//        {
//            WhatIsThat(other);
//            player.PlayerTrade.CheckInShop(other);
//        }

//        private void OnTriggerStay(Collider _)
//        {
//            //TakeALook();
//        }

//        private void OnTriggerExit(Collider other)
//        {
//            Sucks(other);
//            player.PlayerTrade.CheckOutShop(other);
//        }
//        #endregion

//        // 기타 유틸리티
//        #region Utilities
//        void AddIgnore()
//        {
//            if (player.playerStates.isBoard != null)
//            {
//                ignoreThese.AddRange(player.ridingGear.GearColliders);
//            }
//        }

//        void RemoveIgnore()
//        {

//        }
//        #endregion
//    }
//}