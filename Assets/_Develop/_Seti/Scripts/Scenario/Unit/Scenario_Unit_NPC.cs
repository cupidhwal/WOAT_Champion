using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 시나리오 Unit : AI
    /// </summary>
    public class Scenario_Unit_NPC : Scenario_Unit_Actor
    {
        private void Start()
        {
            Manager_UI.Instance.Scenario.Dialogue.OnScenarioEvent += Execute;
        }
    }
}