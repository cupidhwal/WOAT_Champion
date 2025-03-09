//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace Seti
//{
//    // 플레이어의 탑승 제어를 관리하는 클래스
//    public class PlayerRide
//    {
//        // 필드
//        #region Variables
//        // 복합 변수
//        public List<RidingGear> ridableGears;

//        // 클래스 컴포넌트
//        private readonly Player player;
//        #endregion

//        // 생성자
//        #region Constructor
//        public PlayerRide(Player player)
//        {
//            this.player = player;
//            ridableGears = new List<RidingGear>();
//        }
//        #endregion

//        // 이벤트 핸들러
//        #region Event Handlers
//        public void OnRideOnStarted(InputAction.CallbackContext _)
//        {
//            if (player.ridingGear == null) return;
//            if (player.playerStates.isShopEnter)
//            {
//                player.DialogueUI.Dialogue("상점에서는 라이딩기어를 타면 안 돼! 라이딩기어를 몰수당할 수도 있다고!");
//                return;
//            }
//            player.ridingGear.RideOn();
//        }
//        #endregion

//        // 메서드
//        #region Methods
//        // 이벤트 핸들러 스위치
//        public void SwitchRideOn()
//        {
//            if (ridableGears.Count > 0)
//                player.control.Player.RideOn.Enable();

//            else
//                player.control.Player.RideOn.Disable();
//        }

//        // RidingSense 스위치
//        public void SwitchTrigger(bool flag)
//        {
//            // 트리거 콜라이더를 제어하기 위한 변수
//            SphereCollider Trigger = player.transform.Find("RidingSense").GetComponent<SphereCollider>();

//            if (flag == false)
//            {
//                DisableTrigger(Trigger);
//            }
//            else
//            {
//                // 트리거를 활성화 할 땐 OnTriggerEnter 메서드가 자동으로 호출되므로 트리거를 켜기만 하면 된다
//                Trigger.enabled = true;
//            }
//        }

//        // RidingSense가 비활성화 될 때 콜렉션 초기화
//        private void DisableTrigger(SphereCollider collider)
//        {
//            // 트리거 비활성화
//            collider.enabled = false;

//            // 리스트의 모든 라이딩기어 Player == null
//            foreach (RidingGear gear in ridableGears)
//            {
//                // 플레이어가 기본 상태라면
//                if (player.playerStates.isBoard == null)
//                    gear.SetPlayer(null);

//                // 라이딩기어에 탑승한 상태라면 해당 라이딩기어만 상호작용 유지
//                else if (gear == player.ridingGear)
//                    gear.SetPlayer(player);

//                // 나머지는 모두 null
//                else
//                    gear.SetPlayer(null);
//            }

//            // 리스트 초기화
//            ridableGears.Clear();

//            if (ridableGears.Count == 0)
//            {
//                // 리스트를 확인하고 탑승 기능 비활성화
//                SwitchRideOn();

//                // 주목하던 라이딩기어 null, 탑승했다면 유지
//                if (player.playerStates.isBoard != null) return;
//                player.ridingGear = null;
//            }
//        }

//        // 관심 있는 라이딩기어
//        public void RideInteraction(Collider other)
//        {
//            // 임시 변수
//            if (ComponentUtility.TryGetComponentAll<RidingGear>(other.gameObject.transform, out var lookGear))
//            {
//                // 해당 라이딩기어에 플레이어를 인식
//                lookGear.SetPlayer(player);

//                // 해당 라이딩기어를 리스트에 등록
//                ridableGears.Add(lookGear);

//                // 리스트를 확인하고 탑승 기능 활성화
//                SwitchRideOn();

//                // 탑승할 라이딩기어는 첫 번째에 확인한 것
//                player.ridingGear = ridableGears[0];
//            }
//        }

//        // 안중에도 없는 라이딩기어
//        public void NullInteraction(Collider other)
//        {
//            // 임시 변수
//            if (ComponentUtility.TryGetComponentAll<RidingGear>(other.gameObject.transform, out var nullGear))
//                ReleaseGear(nullGear);
//        }

//        public void ReleaseGear(RidingGear gear)
//        {
//            // 관심 없는 라이딩기어는 리스트에서 삭제
//            ridableGears.Remove(gear);

//            // 해당 라이딩기어는 플레이어 인식 해제
//            gear.SetPlayer(null);

//            // 탑승할 라이딩기어로 그 다음 것
//            if (ridableGears.Count > 0)
//                player.ridingGear = ridableGears[0];

//            else
//            {
//                // 리스트를 확인하고 탑승 기능 비활성화
//                SwitchRideOn();

//                if (player.playerStates.isBoard != null) return;
//                player.ridingGear = null;
//            }
//        }
//        #endregion
//    }
//}