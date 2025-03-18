using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seti
{
    /// <summary>
    /// Look Function
    /// </summary>
    [System.Serializable]
    public class Look : IBehaviour, IHasStrategy
    {
        // 필드
        #region Variables
        // 전략 관리
        private Actor actor;
        [SerializeReference]
        private List<Strategy> strategies;
        private ILookStrategy currentStrategy;
        private Vector2 lookInput;

        // 제어 관리
        public float headXRotation;
        public float headYRotation;
        public float bodyYRotation;
        #endregion

        // 인터페이스
        #region Interface
        // 초기화
        public void Initialize(Actor actor)
        {
            this.actor = actor;
            actor.Condition.OnStanceChange += SwitchStrategy;
            strategies = actor.Blueprint.GetStrategies(this);

            foreach (var mapping in strategies)
            {
                ILookStrategy lookStrategy = mapping.strategy as ILookStrategy;
                switch (lookStrategy)
                {
                    case Look_Normal:
                        lookStrategy.Initialize(actor, 0.1f);
                        break;

                    case Look_Attention:
                        lookStrategy.Initialize(actor);
                        break;

                    case Look_Ride:
                        lookStrategy.Initialize(actor);
                        break;

                    case Look_KeepGoing:
                        lookStrategy.Initialize(actor, 0.1f);
                        break;

                    case Look_Watch:
                        lookStrategy.Initialize(actor);
                        break;
                }
            }

            // 초기 전략 설정
            var defaultStrategy = CollectionUtility.FirstOrNull(strategies, s => s.strategy is Look_Normal);
            if (defaultStrategy != null)
            {
                ChangeStrategy(typeof(Look_Normal));
            }
            else if (strategies.Count > 0)
            {
                ChangeStrategy(strategies[0].strategy.GetType());
            }
            else
            {
                //Debug.LogWarning("Look 전략이 없어 초기 전략을 설정하지 못했습니다.");
                ChangeStrategy(null);
            }
        }

        public Type GetBehaviourType() => typeof(Look);
        public Type GetStrategyType() => typeof(ILookStrategy);

        // 보유 전략 확인
        public bool HasStrategy<T>() where T : class, IStrategy => strategies.Any(strategy => strategy.strategy is T);

        // 행동 전략 설정
        public void SetStrategies(IEnumerable<Strategy> strategies)
        {
            this.strategies = strategies.ToList(); // 전달받은 전략 리스트 저장
        }

        // 행동 전략 변경
        public void ChangeStrategy(Type strategyType)
        {
            var lookStrategy = CollectionUtility.FirstOrNull(strategies, s => s.strategy.GetType() == strategyType);
            if (lookStrategy != null)
            {
                currentStrategy = lookStrategy.strategy as ILookStrategy;
            }
        }

        public void SwitchStrategy()
        {
            switch (actor.Condition.CurrentStance)
            {
                case Stance.Normal:
                    ChangeStrategy(typeof(Look_Normal));
                    break;

                case Stance.Boots:
                    ChangeStrategy(typeof(Look_Normal));
                    break;

                case Stance.Board:
                    ChangeStrategy(typeof(Look_Ride));
                    break;
            }
        }
        #endregion

        // 라이프 사이클
        public void Update()
        {
            currentStrategy?.Look();
        }

        // 이벤트 핸들러
        public void OnLookPerformed(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
            currentStrategy?.Look(lookInput);
        }

        public void OnLookCanceled(InputAction.CallbackContext _)
        {
            lookInput = Vector2.zero;
            currentStrategy?.Look(lookInput);
        }

        public void OnKeepGoingStarted(InputAction.CallbackContext _)
        {
            ChangeStrategy(typeof(Look_KeepGoing));
        }

        public void OnKeepGoingCanceled(InputAction.CallbackContext _)
        {
            SwitchStrategy();
        }

        public void FSM_LookInput() => currentStrategy?.Look();
    }
}