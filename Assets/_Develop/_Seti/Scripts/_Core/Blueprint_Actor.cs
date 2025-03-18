using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    public enum ControlType
    {
        Input,
        AI
    }

    [Serializable]
    public class BehaviourStrategyMapping
    {
        [SerializeReference]
        public IBehaviour behaviour;
        [SerializeReference]
        public List<Strategy> strategies;
    }

    /// <summary>
    /// Actor의 Blueprint
    /// </summary>
    [CreateAssetMenu(fileName = "New Actor", menuName = "Blueprint/Actor")]
    public class Blueprint_Actor : ScriptableObject
    {
        // 필드
        #region Variables
        [SerializeField]
        private string actorName;           // 액터 이름
        public GameObject actorPrefab;      // 액터 오브젝트
        public ControlType controlType;     // Actor 타입(enum)
        public Box_Strategy strategyBox;    // Box_Strategy 참조
        public Box_Behaviour behaviourBox;  // Box_Behaviour 참조

        [HideInInspector]
        [SerializeReference]
        public List<BehaviourStrategyMapping> behaviourStrategies = new();

        public string ActorName => actorName;
        #endregion

        // 특정 행동에 대한 전략 가져오기
        public List<Strategy> GetStrategies(IBehaviour behaviour)
        {
            return behaviourStrategies
                .FirstOrDefault(mapping => mapping.behaviour == behaviour)?
                .strategies ?? new List<Strategy>();
        }

        // 특정 전략의 활성 상태 업데이트
        public void UpdateStrategy(IBehaviour behaviour, IStrategy strategy, bool isActive)
        {
            var mapping = behaviourStrategies.FirstOrDefault(m => m.behaviour == behaviour);
            if (mapping != null)
            {
                var target = mapping.strategies.FirstOrDefault(s => s.strategy == strategy);
                if (target != null)
                {
                    target.isActive = isActive;
#if UNITY_EDITOR
                    EditorUtility.SetDirty(this); // 변경 사항 저장
#endif
                }
            }
        }
    }
}

#region Dummy
/*
// 런타임에서의 행동 추가
        public void AddBehaviour(Type behaviourType)
        {
            if (behaviourType == null || !typeof(IBehaviour).IsAssignableFrom(behaviourType))
            {
                Debug.LogWarning("유효하지 않은 행동 타입입니다!");
                return;
            }

            if (behaviourBox == null || behaviourBox.behaviours == null)
            {
                Debug.LogWarning("행동상자가 존재하지 않습니다!");
                return;
            }

            // 행동상자에서 해당 타입과 일치하는 행동 찾기
            IBehaviour behaviour = behaviourBox.behaviours.FirstOrDefault(b => b.GetType() == behaviourType);

            if (behaviour == null)
            {
                Debug.LogWarning($"{behaviourType.Name} 행동을 찾을 수 없습니다!");
                return;
            }

            // 중복 추가 방지
            if (behaviourStrategies.Any(mapping => mapping.behaviour == behaviour))
            {
                Debug.LogWarning($"{behaviourType.Name}은 이미 추가되었습니다!");
                return;
            }

            var strategies = new List<Strategy>();

            if (behaviour is IHasStrategy hasStrategy)
            {
                var strategyType = hasStrategy.GetStrategyType();
                if (strategyType != null)
                {
                    var method = typeof(Box_Strategy).GetMethod(nameof(Box_Strategy.GetStrategies));
                    if (method != null)
                    {
                        var genericMethod = method.MakeGenericMethod(strategyType);
                        if (genericMethod.Invoke(strategyBox, null) is IEnumerable<IStrategy> allStrategies)
                        {
                            strategies = allStrategies
                                .Select(strategy => new Strategy
                                {
                                    strategy = strategy,
                                    isActive = true
                                })
                                .ToList();
                        }
                    }
                }
            }

            behaviourStrategies.Add(new BehaviourStrategyMapping
            {
                behaviour = behaviour,
                strategies = strategies
            });

            Debug.Log($"{behaviourType.Name} 행동이 추가되었습니다!");
        }

        /// <summary>
        /// 갱신된 설계도의 행동-전략 주입
        /// </summary>
        public void UpdateBehaviours(Blueprint_Actor blueprint, Actor actor)
        {
            // 기존 행동 초기화
            actor.Behaviours.Clear();

            // 행동-전략 생성 및 주입
            foreach (var beSt in blueprint.behaviourStrategies)
            {
                var newBehaviour = CreateNewBehaviour(beSt); // 완전히 새로운 행동 생성
                actor.AddBehaviour(newBehaviour);
            }

            // 액터 초기화
            actor.Initialize();
#if UNITY_EDITOR
            EditorUtility.SetDirty(actor);
#endif
        }

        /// <summary>
        /// 갱신된 설계도의 행동-전략 매핑
        /// </summary>
        private IBehaviour CreateNewBehaviour(BehaviourStrategyMapping mapping)
        {
            // 새로운 행동 객체 생성
            var newBehaviour = Activator.CreateInstance(mapping.behaviour.GetType()) as IBehaviour;

            if (newBehaviour is IHasStrategy behaviourWithStrategy)
            {
                // 활성화된 전략만 새로 생성
                var activeStrategies = mapping.strategies
                    .Where(s => s.isActive)
                    .Select(s => new Strategy
                    {
                        strategy = s.strategy,
                        isActive = s.isActive
                    }).ToList();

                behaviourWithStrategy.SetStrategies(activeStrategies);
            }

            return newBehaviour;
        }
 */
#endregion