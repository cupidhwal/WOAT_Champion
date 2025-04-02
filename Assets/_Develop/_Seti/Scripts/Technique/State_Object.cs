using System;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// FSM Object의 기본 상태 정의
    /// </summary>
    public abstract class State_Object : ScriptableObject
    {
        protected FSM_Scenario machine;

        public void Initialize(FSM_Scenario machine) => this.machine = machine;
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate(float deltaTime);
        public abstract Type CheckTransition();
    }
}