using UnityEngine;

namespace Seti
{
    public abstract class Scenario_Unit_Actor : Scenario_Unit
    {
        // ÇÊµå
        #region Variables
        [Header("Scenario : Data")]
        [SerializeField]
        private ScenarioData[] datas;
        #endregion
    }
}