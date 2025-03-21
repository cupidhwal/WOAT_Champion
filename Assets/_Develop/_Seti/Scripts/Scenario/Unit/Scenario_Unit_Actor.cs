using UnityEngine;

namespace Seti
{
    public abstract class Scenario_Unit_Actor : Scenario_Unit
    {
        [Header("Scenario : Data")]
        [SerializeField]
        private ScenarioData[] datas;
        public ScenarioData[] Datas => datas;
    }
}