using UnityEngine;

namespace Seti
{
    /// <summary>
    /// AI Actor
    /// </summary>
    [RequireComponent(typeof(Scenario_Unit_AI))]
    [RequireComponent(typeof(Condition_AI))]
    public class AI : NPC
    {
    }
}