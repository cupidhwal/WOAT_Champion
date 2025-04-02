using System;
using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    public class FSM_Scenario : Singleton<FSM_Scenario>
    {
        // 필드
        #region Variables
        private Player player;

        [Header("States : Current")]
        [SerializeField]
        private State_Object currentState;
        private Dictionary<Type, State_Object> states = new();  // 등록된 상태와 상태의 타입을 저장
        [SerializeField, ReadOnly]
        private float elapsed = 0.0f;                           // 현재 상태 지속 시간

        [Header("States : Able")]
        [SerializeField]
        private State_Object[] ableStates;

        // 이벤트
        public event Action<State_Object> OnStateChanged;
        #endregion

        // 속성
        #region Properties
        public Player Player => player;
        public float ElapsedTime => elapsed;                    // 현재 상태에서 경과한 시간
        public State_Object CurrentState
        {
            get => currentState;
            private set
            {
                if (currentState != value)
                {
                    currentState = value;
                    OnStateChanged?.Invoke(currentState);
                }
                if (value == currentState) return;
            }
        }
        public State_Object PreviousState { get; private set; }
        public State_Object[] AbleStates => ableStates;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            player = Manager_Initialize.Instance.Player;

            foreach (var state in ableStates)
            {
                AddState(state);
            }

            CurrentState = ableStates[0];
            CurrentState.OnEnter();
        }

        private void Update()
        {
            elapsed += Time.deltaTime;
            CurrentState.OnUpdate(Time.deltaTime);

            // 현재 상태의 전환 조건 검사
            var nextStateType = CurrentState.CheckTransition();
            if (nextStateType != null && states.ContainsKey(nextStateType))
                ChangeState(nextStateType);
        }
        #endregion

        // State 등록
        public void AddState(State_Object state)
        {
            state.Initialize(this);
            states[state.GetType()] = state;
        }

        // State 변경
        private void ChangeState(Type nextStateType)
        {
            CurrentState.OnExit();
            PreviousState = CurrentState;

            CurrentState = states[nextStateType];
            CurrentState.OnEnter();
            elapsed = 0.0f;

            OnStateChanged?.Invoke(CurrentState);
        }
    }
}