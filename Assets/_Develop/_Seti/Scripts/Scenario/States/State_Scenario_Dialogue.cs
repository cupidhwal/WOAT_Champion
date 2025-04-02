using System;
using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scenario/States/Dialogue")]
    public class State_Scenario_Dialogue : State_Scenario
    {
        // ¿Ã∫•∆Æ
        public UnityAction<ScenarioData> OnScenarioEvent;

        public override void OnEnter()
        {
            base.OnEnter();

            OnScenarioEvent?.Invoke(Manager_Scenario.Instance.Mechanic.Datas[0]);
        }

        public override void OnExit()
        {
            base.OnExit();

            OnScenarioEvent?.Invoke(Manager_Scenario.Instance.Mechanic.Exit);
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
        }

        public override Type CheckTransition()
        {
            if (machine.Player.Condition.CurrentAction != Action.Idle)
                return typeof(State_Scenario_Idle);

            return machine.Player.Condition.CurrentInteraction switch
            {
                Interaction.Idle => typeof(State_Scenario_Idle),
                Interaction.Choice => typeof(State_Scenario_Choice),
                Interaction.Action => typeof(State_Scenario_Action),
                _ => null,
            };
        }

        public void OnNext()
        {

        }

        public void OnStart()
        {
            OnScenarioEvent?.Invoke(Manager_Scenario.Instance.Mechanic.Enter);
        }
    }
}