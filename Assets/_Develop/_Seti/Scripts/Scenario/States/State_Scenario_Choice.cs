using System;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Choice", menuName = "Scenario/States/Choice")]
    public class State_Scenario_Choice : State_Scenario
    {
        public override Type CheckTransition()
        {
            if (machine.Player.Condition.CurrentAction != Action.Idle)
                return typeof(State_Scenario_Idle);

            return machine.Player.Condition.CurrentInteraction switch
            {
                Interaction.Idle => typeof(State_Scenario_Idle),
                Interaction.Dialogue => typeof(State_Scenario_Dialogue),
                Interaction.Action => typeof(State_Scenario_Action),
                _ => null,
            };
        }
    }
}