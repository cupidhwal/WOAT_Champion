using System;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Scenario/States/Dialogue")]
    public class State_Scenario_Dialogue : State_Scenario
    {
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
    }
}