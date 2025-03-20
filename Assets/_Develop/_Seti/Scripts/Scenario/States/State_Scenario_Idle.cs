using System;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Scenario/States/Idle")]
    public class State_Scenario_Idle : State_Scenario
    {
        public override Type CheckTransition()
        {
            return machine.Player.Condition.CurrentInteraction switch
            {
                Interaction.Dialogue => typeof(State_Scenario_Dialogue),
                Interaction.Choice => typeof(State_Scenario_Choice),
                Interaction.Action => typeof(State_Scenario_Action),
                _ => null,
            };
        }
    }
}