//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Seti
//{
//    /// <summary>
//    /// 플레이어의 포만감과 충전량을 관리하는 클래스
//    /// </summary>
//    /// 소화 효율: 0.98
//    /// 전기 세포의 에너지 전환 효율: 0.5
//    /// 양쪽 허벅지의 최대 충전 용량: 약 3,920 kcal (= 4.5kWh, 남성의 경우)
//    /// 완전 충전에 필요한 열량: 약 8,000 kcal
//    /// 
//    /// 게임 설정 상 포만감은 충전량으로 비가역적인 변환 과정을 수행한다.
//    /// 포만감은 음식을 먹어 회복하며, 100%까지 명확한 한계를 갖는다.
//    /// 또한 포만감은 기초대사량과 전기전환, 두 가지 소모 요소를 갖는다.
//    /// 그리고 포만감의 소모는 절대로 멈추지 않는다.
//    /// 
//    /// 충전량은 포만감이 천천히 전환된 결과로 획득하며, 100%는 존재하지만 과충전도 가능하다.
//    /// 과충전은 150%까지 가능하며, 과충전 중에 충돌사고가 발생하면 접지를 통해 방전이 이루어진다.
//    /// 따라서 절대로 과충전되는 일이 없도록 막는 부적 장치가 필요한데, 그게 바로 펄이다.
//    /// 펄은 가지고 있기만 해도 100%를 초과하는 전기를 포집하며, 완전히 충전된 펄은 약간이지만 콜(돈)이 된다.
//    /// 또한 도시의 특정 장소에는 전기를 넣을 수 있는 포인트가 있으며, 경우에 따라 이벤트가 발생하거나 돈이 나온다.
//    /// 
//    /// 충전량은 주로 라이딩기어의 운용에 쓰인다.
//    /// 부츠 타입은 카트리지 격발과 평형 유지에만 전기를 쓰기 때문에 보드 타입의 전기 소모량이 훨씬 크다.
//    /// 
//    /// 소화 시스템
//    /// 음식에는 세 가지 중대요소가 있는데, 맛, 칼로리, 영양 밸런스
//    /// 맛은 기본적으로 취향에 따르는데, 취향에 맞으면 한 번에 먹을 수 있는 양이 더 많고
//    /// (예를 들어 보통은 80% 먹고 남기지만 맛있으면 100% 다 먹고 맛없으면 반만 먹고 버린다)
//    /// 칼로리가 높으면 당연히 얻을 수 있는 에너지가 더 많고
//    /// 영양 밸런스가 좋을수록 포만감의 소비 속도가 빨라진다 => 충전이 빨라진다
//    public class PlayerEnergy : MonoBehaviour
//    {
//        // 필드
//        #region Variables
//        // 전용 변수
//        private float satiety;                      // 포만감
//        private float currentEnergy;                // 충전량
//        private float digestTime = 5;               // 기본 소화 시간
//        private float flavourEff;                   // 맛 계수
//        private const float hungerEff = 0.2f;       // 굶주림 추가 보정, 시장이 반찬
//        private const float digestEff = 0.98f;      // 소화 효율
//        private const float basicConvertEff = 0.5f; // 에너지 전환비
//        [SerializeField]
//        private IStrategy_Energy strategy;
//        //[SerializeField]
//        //private List<Food> whatIAte = new();

//        // 배고픔 호소
//        private float hungryStamp;
//        private bool hungryAppeal = false;

//        // 클래스
//        private Player player;
//        //private UI_Energy energyUI;

//        // 에너지 전략 패턴
//        IStrategy_Energy strategy_Normal;
//        IStrategy_Energy strategy_Board;
//        IStrategy_Energy strategy_Boots;
//        #endregion

//        // 속성
//        #region Properties
//        public float SatietyRatio => satiety / 100; // 1.0까지 가능
//        public float EnergyRatio => currentEnergy / 3920;  // 1.5까지 가능
//        public IStrategy_Energy Strategy => strategy;
//        #endregion

//        // 라이프 사이클
//        #region Life Cycle
//        private void Start()
//        {
//            satiety = 0f;
//            currentEnergy = 1000f;

//            // 기본적으로 Normal
//            strategy = strategy_Normal;
//        }

//        private void Update()
//        {
//            Digest();
//        }

//        private void Awake()
//        {
//            player = GetComponent<Player>();

//            // 에너지 전략 패턴
//            strategy_Normal = new Strategy_Normal();
//            strategy_Board = new Strategy_Board();
//            strategy_Boots = new Strategy_Boots();
//        }

//        //private void OnEnable()
//        //{
//        //    // 대리자 이벤트에 메서드 구독
//        //    player.OnRideOn += SetStrategy;
//        //}

//        //private void OnDisable()
//        //{
//        //    // 대리자 이벤트에 메서드 구독 해제
//        //    player.OnRideOn -= SetStrategy;
//        //}
//        #endregion

//        // 메서드
//        #region Methods
//        //public void SetUI(UI_Energy energyUI)
//        //{
//        //    this.energyUI = energyUI;

//        //    // 임시
//        //    energyUI.ReadEnergyRatio();
//        //}

//        //public void SetStrategy()
//        //{
//        //    if (player.playerStates.isBoard == null)
//        //    {
//        //        strategy = strategy_Normal;
//        //    }
//        //    else
//        //    {
//        //        if (player.playerStates.isBoard == true)
//        //            strategy = strategy_Board;
//        //        else strategy = strategy_Boots;

//        //        player.ridingGear.RidingEngine.SetStrategy(strategy);
//        //    }
//        //}

//        // 충전량 소모
//        public void UseElectricity(float amount)
//        {
//            // amount는 W(와트) 단위이므로 칼로리로 변환
//            /*
//            Wh = W * 1h
//            => W = Wh/1h = Wh/3600s

//            1Wh = 3600J, 1kcal = 4184J
//            => Wh/3600 = cal/4184

//            therefore, W = cal/4184
//             */

//            currentEnergy -= amount * Time.fixedDeltaTime / 4184;

//            if (EnergyRatio > 1)
//                Debug.Log("오버차지");
//        }

//        // 음식을 먹고 포만감을 획득
//        public void EatFood(Food food)
//        {
//            // 소화 중인 음식 리스트에 지금 먹은 음식을 추가
//            whatIAte.Add(food);

//            // 꼬리표 출현
//            energyUI.ShowTooltip();

//            // 맛 판정
//            HowFlavour(food);

//            // 소화 시작
//            StartCoroutine(Digest(food));
//        }

//        // 음식 타입에 따라 소화 시간 변경
//        private float DigestTime(Food food)
//        {
//            float digestPeriod = digestTime;    // 기본 소화 시간: 1시간

//            switch (food.Type)
//            {
//                case Food.FoodType.Meal:
//                    digestPeriod *= 4f;
//                    break;

//                case Food.FoodType.Bento:
//                    digestPeriod *= 3f;
//                    break;

//                case Food.FoodType.Snack:
//                    digestPeriod *= 2f;
//                    break;

//                case Food.FoodType.Drink:
//                    digestPeriod *= 1f;
//                    break;
//            }

//            return digestPeriod;
//        }

//        // 실제 포만감 : 맛 계수 * 음식 포만감
//        private void HowSatiety(Food food)
//        {
//            satiety += flavourEff * food.Satiety;
//        }

//        private void HowFlavour(Food food)
//        {
//            switch(food.Flavour)
//            {
//                case Food.FoodFlavour.Good:
//                    flavourEff = 1f;
//                    if (SatietyRatio >= 0.1f)
//                        player.DialogueUI.Dialogue("완전 맛있는데?");
//                    else player.DialogueUI.Dialogue("역시 " +
//                        LetterUtility.GetLastString(food.database_Item.itemList[food.ItemID].itemNameKor) + "!");
//                    // 배고플 때 맛있는 음식의 보너스는 소화 기능 반복기에 있다
//                    break;

//                case Food.FoodFlavour.Soso:
//                    flavourEff = 0.8f;
//                    if (SatietyRatio < 0.1f)
//                    {
//                        flavourEff += hungerEff;
//                        player.DialogueUI.Dialogue("배고파서 그런지 평소보다 맛있어!");
//                    }
//                    else player.DialogueUI.Dialogue("그냥 그래.\n다음엔 더 맛있는 걸 먹어야지.");
//                    break;

//                case Food.FoodFlavour.Bad:
//                    flavourEff = 0.5f;
//                    if (SatietyRatio < 0.1f)
//                    {
//                        flavourEff += hungerEff;
//                        player.DialogueUI.Dialogue("배고프니까 조금만 더 먹자…….");
//                    }
//                    else player.DialogueUI.Dialogue("너무 맛 없어…….\n대충 먹고 버리자.");
//                    break;
//            }
//        }

//        // 밸런스 계수 : 밸런스 판정 bool? 전환비 0.8f, 1f, 1.25f
//        private float HowBalance(Food food)
//        {
//            if (food.Balance == Food.FoodBalance.Good) return 0.8f;
//            else if (food.Balance == Food.FoodBalance.Soso) return 1f;
//            else return 1.25f;
//        }

//        // 획득 칼로리 : 맛 계수 * 소화 효율 * 에너지 전환비 * 음식 칼로리
//        private float HowCalorie(Food food)
//        {
//            return flavourEff * digestEff * basicConvertEff * food.Calorie;
//        }

//        // 과충전
//        private void OverCharge()
//        {
//            // 펄이 있다면: 펄 충전
//            // 펄이 없다면: 과충전
//        }
//        #endregion

//        // 소화 기능
//        #region Digest
//        // 포만감을 감소시키는 카운트 메서드
//        private void Digest()
//        {
//            satiety -= 0.01f * Time.deltaTime;
//            satiety = Mathf.Clamp(satiety, 0, 100);

//            energyUI.ReadSatietyRatio();

//            if (SatietyRatio < 0.1f && !hungryAppeal)
//            {
//                player.DialogueUI.Dialogue("아우, 배고파!");
//                hungryStamp = Time.time;
//                hungryAppeal = true;
//            }

//            if (hungryStamp + 300 < Time.time)
//                hungryAppeal = false;
//        }

//        // 칼로리를 충전량으로 전환하는 반복기
//        private IEnumerator Digest(Food food)
//        {
//            float timeStamp = Time.time;
//            float thisEnergy = HowCalorie(food) / HowBalance(food);  // 실제 전기로 전환되는 칼로리, 섭취한 칼로리의 40% ~ 62.5%
//            float cycleEnergy = thisEnergy / DigestTime(food);

//            // 배고플 때 맛있는 음식의 보너스
//            if (food.Flavour == Food.FoodFlavour.Good && SatietyRatio < 0.1f)
//                cycleEnergy *= (1 + (hungerEff / 2));
            
//            // 포만감 추가
//            HowSatiety(food);

//            while (timeStamp + DigestTime(food) * HowBalance(food) > Time.time)
//            {
//                // 매 사이클마다 전력을 획득
//                currentEnergy += cycleEnergy;
//                currentEnergy = Mathf.Clamp(currentEnergy, 0, 5880);

//                energyUI.ReadEnergyRatio();

//                yield return new WaitForSeconds(digestEff);
//            }

//            // 소화가 끝나면 리스트에서 해당 음식을 삭제
//            whatIAte.Remove(food);
//            yield break;
//        }
//        #endregion
//    }
//}