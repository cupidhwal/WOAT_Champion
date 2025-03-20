using System;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Action", menuName = "Scenario/States/Action")]
    public class State_Scenario_Action : State_Scenario
    {
        public override Type CheckTransition()
        {
            if (machine.Player.Condition.CurrentAction != Action.Idle)
                return typeof(State_Scenario_Idle);

            return machine.Player.Condition.CurrentInteraction switch
            {
                Interaction.Idle => typeof(State_Scenario_Idle),
                Interaction.Dialogue => typeof(State_Scenario_Dialogue),
                Interaction.Choice => typeof(State_Scenario_Choice),
                _ => null,
            };
        }
    }
}